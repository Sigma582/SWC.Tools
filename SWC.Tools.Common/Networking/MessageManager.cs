using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using SWC.Tools.Common.Networking.Exception;
using SWC.Tools.Common.Networking.Json.CommandArgs;
using SWC.Tools.Common.Networking.Json.Entities;
using SWC.Tools.Common.Networking.Json.Messages;
using SWC.Tools.Common.Networking.Json.Responses;
using SWC.Tools.Common.Util;

namespace SWC.Tools.Common.Networking
{
    public class MessageManager
    {
        private readonly string _url;
        private readonly string _playerId;
        private readonly string _playerSecret;
        private string _authToken;
        private readonly MessageSender _messageSender;
        private readonly DataContractJsonSerializer _authResponseSerializer;
        private readonly DataContractJsonSerializer _loginResponseSerializer;
        private Player _loginData;
        private bool _isLive;
        private int _lastLoginTimeSec;
        private const int RETRY_COUNT_DEFAULT = 3;

        public bool SkipTimestamp { get; set; }

        public int TimestampAdj { get; set; }

        public MessageManager(string url, string playerId, string playerSecret)
        {
            _url = url;
            _playerId = playerId;
            _playerSecret = playerSecret;
            _messageSender = new MessageSender(_url);
            _authResponseSerializer = new DataContractJsonSerializer(typeof(Response<string>));
            _loginResponseSerializer = new DataContractJsonSerializer(typeof(Response<Player>), new DataContractJsonSerializerSettings{UseSimpleDictionaryFormat = true});
        }

        public void Init()
        {
            _authToken = GetAuthToken();
            _loginData = Login();
            _isLive = true;
        }

        private string GetAuthToken()
        {
            var message = new GetAuthTokenMessage(_playerId, _playerSecret);
            var rawResponse = _messageSender.Send(message);
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(rawResponse)))
            {
                var response = (Response<string>) _authResponseSerializer.ReadObject(stream);
                return response.Data[0].Result;
            }
        }

        private Player Login()
        {
            var message = new PlayerLoginMessage(_playerId, _authToken);
            var rawResponse = _messageSender.Send(message);
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(rawResponse)))
            {
                var response = (Response<Player>) _loginResponseSerializer.ReadObject(stream);
                var player = response.Data[0].Result;
                _lastLoginTimeSec = player.Liveness.LastLoginTime;
                return player;
            }
        }

        private void CheckInit()
        {
            if (_isLive)
            {
                return;
            }
            Init();
            Thread.Sleep(2000);
        }

        private TResult Send<TResult>(Message message, int retryCount = RETRY_COUNT_DEFAULT)
        {
            CheckInit();
            PrepareMessage(message);

            var rawResponse = _messageSender.Send(message);

            Response<TResult> response;
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(rawResponse)))
            {
                var serializer = new DataContractJsonSerializer(typeof(Response<TResult>), new DataContractJsonSerializerSettings{UseSimpleDictionaryFormat = true});
                response = (Response<TResult>) serializer.ReadObject(stream);
            }

            switch (response.Data[0].Status)
            {
                case ServerConstants.ZERO:
                case ServerConstants.SUCCESS:
                    return response.Data[0].Result;

                case ServerConstants.STATUS_AUTHENTICATION_FAILED:
                case ServerConstants.STATUS_AUTHORIZATION_FAILED:
                case ServerConstants.LOGIN_TIME_MISMATCH:
                case ServerConstants.COMMAND_TIMESTAMP_ERROR:
                case ServerConstants.COMMAND_TIMESTAMP_ERROR2:
                    if (retryCount > 0)
                    {
                        Init();
                        // ReSharper disable once TailRecursiveCall
                        return Send<TResult>(message, retryCount - 1);
                    }
                    else
                    {
                        _isLive = false;
                        throw new StatusException(response.Data[0].Status);
                    }

                default:
                    throw new StatusException(response.Data[0].Status);
            }
        }

        private void PrepareMessage(Message message)
        {
            message.AuthKey = _authToken;
            if (!message.NeedsTime) return;

            message.LastLoginTime = _lastLoginTimeSec;
            message.Commands.ForEach(c =>
            {
                c.Token = Guid.NewGuid().ToString();
                if (c.NeedsTime)
                {
                    c.TimeSec = SkipTimestamp? 0 : TimeHelper.GetTimestampSec() + TimestampAdj;
                }
            });
        }

        public void Refresh()
        {
            //todo check keep-alive status and relogin only when necessary
            Init();
        }

        public Player GetLoginData()
        {
            CheckInit();
            return _loginData;
        }

        public IEnumerable<Building> GetOwnBase()
        {
            return _loginData.PlayerModel.Map.Buildings;
        }

        public WarParticipant GetWarParticipant()
        {
            var message = new GetWarParticipantMessage(_playerId);
            var result = Send<WarParticipant>(message);
            return result;
        }

        public Player VisitNeighbor(string neighborId)
        {
            var message = new VisitNeighborMessage(_playerId, neighborId);
            var result = Send<PlayerWrapper>(message);
            return result.Player;
        }

        public IList<Squad> SearchSquads(string searchString)
        {
            var message = new SearchSquadsMessage(_playerId, searchString);
            var result = Send<Squad[]>(message);
            return result.ToList();
        }

        public SquadDetails GetSquadDetails(string squadId)
        {
            var message = new GetSquadDetailsMessage(_playerId, squadId);
            var result = Send<SquadDetails>(message);
            return result;
        }

        public Dictionary<string, Building> UpldateLayout(Dictionary<string, Position> positions)
        {
            var message = new UpldateLayoutMessage(_playerId, positions);
            var result = Send<Dictionary<string, Building>>(message);
            return result;
        }

        public Dictionary<string, Building> UpdateWarLayout(Dictionary<string, Position> positions)
        {
            var message = new UpldateWarLayoutMessage(_playerId, positions);
            var result = Send<Dictionary<string, Building>>(message);
            return result;
        }
    }
}