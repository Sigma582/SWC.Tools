using SWC.Tools.Common.Networking.Json.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SWC.Tools.Common.Util
{
    public static class MapUtil
    {
        //Item1 - file's building; Item2 - user's building.
        public static List<Tuple<Building, Building>> Match(IEnumerable<Building> newLayout, IEnumerable<Building> currentLayout)
        {
            var nl = new LinkedList<Building>(newLayout);
            var cl = new LinkedList<Building>(currentLayout);
            var map = new List<Tuple<Building, Building>>();

            //same type, same level
            Match(map, nl, cl, (b1, b2) => IsSameType(b1, b2, 0));

            if (cl.Any())
            {
                //same type, level +/- 1
                Match(map, nl, cl, (b1, b2) => IsSameType(b1, b2, 1));
            }

            if (cl.Any())
            {
                //same type, level +/- 2
                Match(map, nl, cl, (b1, b2) => IsSameType(b1, b2, 2));
            }

            if (cl.Any())
            {
                //same type, any level
                Match(map, nl, cl, (b1, b2) => IsSameType(b1, b2, 99));
            }

            if (cl.Any())
            {
                Match(map, nl, cl, IsReplaceableType);
            }

            map.AddRange(cl.Select(b => new Tuple<Building, Building>(null, b)));
            return map;
        }

        private static void Match(List<Tuple<Building, Building>> map, LinkedList<Building> newLayout, LinkedList<Building> currentLayout, Func<Building, Building, bool> comparator)
        {
            var c = currentLayout.First;

            while (c != null)
            {
                var n = newLayout.First;
                var next = c.Next;

                while (n != null)
                {
                    if (comparator(c.Value, n.Value))
                    {
                        map.Add(new Tuple<Building, Building>(n.Value, c.Value));
                        newLayout.Remove(n);
                        currentLayout.Remove(c);
                        break;
                    }

                    n = n.Next;
                }

                c = next;
            }

        }

        private static bool IsSameType(Building b1, Building b2, int maxLevelDifference)
        {
            return b1.Type == b2.Type && Math.Abs(b1.Level - b2.Level) <= maxLevelDifference;
        }

        private static bool IsReplaceableType(Building b1, Building b2)
        {
            return b1.IsReplaceable(b2);
        }


    }
}
