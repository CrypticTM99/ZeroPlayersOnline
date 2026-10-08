using Newtonsoft.Json;

namespace ZeroPlayersOnline.DataTypes {
    public class POH_Room {
        public string RoomName = "";
        public string RoomType = "";

        public List<string> HotspotData = new(); // format: "type,(number of spots)". eg for garden "Tree,2" would allow two BuiltObjects with the Tree hotspot type, if this is a garden.

        public List<string> BuiltObjects = new();

        public int CostToBuy = 0;
        public int LevelToBuy = 1;

        public bool EverFinished = false;

        public FarmingPatch Patch = new("POH", "");

        [JsonConstructor]
        public POH_Room() {}

        public POH_Room(POH_Room other) {
            RoomType = other.RoomType;
            HotspotData = new(other.HotspotData);
            CostToBuy = other.CostToBuy;
        }

        public POH_Room(string room, List<string> hotspots, int cost, int level) {
            RoomType = room;
            HotspotData = hotspots;

            CostToBuy = cost;
            LevelToBuy = level;
        }

        public int MaxObjects() {
            int count = 0;

            foreach (var str in HotspotData) {
                string[] split = str.Split(",");
                if (split.Length > 1) {
                    int.TryParse(split[1], out int amt);

                    count += amt;
                }
            }

            return count;
        }

        public bool AnyStations() { 
            foreach (var obj in BuiltObjects) {
                if (GameLoop.ZPO.POH_ObjectLib.TryGetValue(obj, out POH_Object? actual) && actual != null) {
                    if (actual.AddsStation != "") {
                        return true;
                    }
                } 
            }

            return false;
        }

        public bool AnyGathers() { 
            foreach (var obj in BuiltObjects) {
                if (GameLoop.ZPO.POH_ObjectLib.TryGetValue(obj, out POH_Object? actual) && actual != null) {
                    if (actual.AddsGather != "") {
                        return true;
                    }
                } 
            }

            return false;
        }

        public bool AnyShops() { 
            foreach (var obj in BuiltObjects) {
                if (GameLoop.ZPO.POH_ObjectLib.TryGetValue(obj, out POH_Object? actual) && actual != null) {
                    if (actual.ShopItems.Count > 0) {
                        return true;
                    }
                } 
            }

            return false;
        }
    }
}
