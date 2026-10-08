using Newtonsoft.Json;

namespace ZeroPlayersOnline.DataTypes {
    public class PlayerOwnedHome {
        public string ID = "";
        
        // Player can own any number of houses, but only active houses equal to Construction tier (actual level divided by 10, floored, plus 1. so 1 house at level 1-9, then 2 houses at 10, up to 10 houses at 90)
        public bool Active = false; // Active houses can be teleported to with the Teleport to House spell. Can change active status at any Estate Agent
        
        public List<POH_Room> Rooms = new();
        public int MaxFinishedRooms = 0; // Max count of finished rooms you've done for this house, if you finish a room and the new count is over this then get 10 planks worth of experience for your current max tier (30 * tier per plank, so 300 exp at level 1 or 3k at level 90)

        [JsonConstructor]
        public PlayerOwnedHome() { }

        public PlayerOwnedHome(string id) {
            ID = id;
        }



        public int TotalComfort() {
            int comfort = 0;

            foreach (var room in Rooms) {
                foreach (var obj in room.BuiltObjects) {
                    if (GameLoop.ZPO.POH_ObjectLib.TryGetValue(obj, out POH_Object? actual) && actual != null) {
                        comfort += actual.ComfortAdded;
                    }
                }
            }

            return comfort;
        }

        public bool HasTeleportFocus() {
            foreach (var room in Rooms) {
                if (room.BuiltObjects.Contains("Teleportation focus")) {
                    return true;
                } 
            }

            return false;
        }
    }
}
