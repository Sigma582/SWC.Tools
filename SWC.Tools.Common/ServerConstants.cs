namespace SWC.Tools.Common
{
    public static class ServerConstants
    {
        public const int STATUS_CODE_NOT_MODIFIED = 14;
        public const int STATUS_CODE_BAD_INPUT = 701;
        public const int STATUS_AUTHENTICATION_FAILED = 800;
        public const int STATUS_AUTHORIZATION_FAILED = 801;
        public const int LOGIN_TIME_MISMATCH = 917;
        public const int STATUS_CODE_EXTERNAL_ACCOUNT_AUTH_FAILURE = 1318;
        public const int INACTIVE_PLAYER_IDENTITY = 1900;
        public const int DESYNC_BANNED = 1999;
        public const int STATUS_CODE_PVP_TARGET_IS_UNDER_PROTECTION = 2100;
        public const int STATUS_CODE_PVP_TARGET_IS_UNDER_ATTACK = 2101;
        public const int STATUS_CODE_PVP_TARGET_IS_ONLINE = 2102;
        public const int STATUS_CODE_PVP_TARGET_NOT_FOUND = 2103;
        public const int STATUS_CODE_PVP_TARGET_BANNED = 2104;
        public const int STATUS_CODE_PVP_TARGET_IS_INVALID = 2105;
        public const int STATUS_CODE_PVP_TARGET_PLANET_MISMATCH = 2106;
        public const int STATUS_CODE_PVP_TARGET_HAS_RELOCATED = 2107;
        public const int REPLAY_DATA_NOT_FOUND = 2110;
        public const int STATUS_CODE_ALREADY_REGISTERED = 2200;
        public const int STATUS_CODE_PERMANENTLY_LINKED = 2201;
        public const int STATUS_CODE_GUILD_MIN = 2300;
        public const int STATUS_CODE_ALREADY_IN_A_GUILD = 2300;
        public const int STATUS_CODE_GUILD_NAME_TAKEN = 2301;
        public const int STATUS_CODE_GUILD_IS_FULL = 2302;
        public const int STATUS_CODE_GUILD_NOT_OPEN_ENROLLMENT = 2303;
        public const int STATUS_CODE_GUILD_SCORE_REQ_NOT_MET = 2304;
        public const int STATUS_CODE_GUILD_WRONG_FACTION = 2305;
        public const int STATUS_CODE_NOT_IN_GUILD = 2306;
        public const int STATUS_CODE_NOT_ENOUGH_GUILD_RANK = 2309;
        public const int STATUS_CODE_NOT_IN_SAME_GUILD = 2315;
        public const int STATUS_CODE_CANNOT_DEDUCT_NEGATIVE_AMOUNT = 2316;
        public const int STATUS_CODE_CAN_ONLY_DONATE_TROOPS = 2318;
        public const int STATUS_CODE_NOT_ENOUGH_GUILD_TROOP_CAPACITY = 2319;
        public const int STATUS_CODE_TOO_SOON_TO_REQUEST_TROOPS_AGAIN = 2320;
        public const int STATUS_CODE_PLAYER_IS_IN_SQUAD_WAR = 2321;
        public const int STATUS_CODE_GUILD_MAX = 2322;
        public const int STATUS_CODE_GUILD_WAR_BASE_ALREADY_OWNED = 2402;
        public const int STATUS_CODE_GUILD_WAR_BASE_UNDER_ATTACK = 2403;
        public const int STATUS_CODE_GUILD_WAR_PARTICIPANT_IN_ATTACK = 2404;
        public const int STATUS_CODE_GUILD_WAR_NOT_ENOUGH_TURNS = 2406;
        public const int STATUS_CODE_GUILD_WAR_NOT_ENOUGH_VICTORY_POINTS = 2407;
        public const int STATUS_CODE_GUILD_WAR_WRONG_PHASE = 2409;
        public const int STATUS_CODE_GUILD_WAR_CANNOT_CLAIM_EXPIRED_REWARD = 2413;
        public const int STATUS_CODE_GUILD_WAR_EXPIRED = 2414;
        public const int STATUS_CODE_GUILD_WAR_PLAYER_UNDER_ATTACK = 2418;
        public const int STATUS_CODE_GUILD_WAR_BUFF_BASE_OWNER_CHANGE = 2421;
        public const int STATUS_CODE_CANNOT_UNLOCK_ALREADY_AVAILABLE_PERK = 2502;
        public const int STATUS_CODE_CANNOT_UPGRADE_PERK_NONSEQUENTIALLY = 2504;
        public const int DEACTIVATE_EQUIPMENT_FAILED = 2604;
        public const int RECEIPT_STATUS_COMPLETE = 0;
        public const int RECEIPT_STATUS_INITIATED = 6300;
        
        //discovered - may be inaccurate
        public const int ZERO = 0;
        public const int SUCCESS = 5001;
        /// <summary>
        /// Timestamp specified is too small
        /// </summary>
        public const int COMMAND_TIMESTAMP_ERROR = 909;
        /// <summary>
        /// Timestamp specified is too big
        /// </summary>
        public const int COMMAND_TIMESTAMP_ERROR2 = 911;
        public const int BUILDING_DOESNT_EXIST = 1008;
        public const int BUILDING_OVERLAP_WITH_ANOTHER = 1010;
        public const int BUILDING_OUT_OF_MAP = 1011;
    }
}