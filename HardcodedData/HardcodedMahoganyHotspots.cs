using ZeroPlayersOnline.DataTypes;

namespace ZeroPlayersOnline.Hardcodes {
    public static class HardcodedMahoganyHotspots {
        public static void InitSpots(Dictionary<string, M_MahoganyHomesHotspot> hotspotLib) {
            List<M_MahoganyHomesHotspot> toAdd = new();

            toAdd.Add(new("altar", "Altar", 5, 0, 1, 2));
            toAdd.Add(new("bedLarge", "Large bed", 5, 0, 2, 0));
            toAdd.Add(new("bedSmall", "Bed", 4, 0, 1, 0));
            toAdd.Add(new("bench", "Bench", 3, 0, 0, 0));
            toAdd.Add(new("bookcase", "Bookcase", 4, 0, 0, 0));
            toAdd.Add(new("chair", "Chair", 2, 0, 0, 0));
            toAdd.Add(new("chest", "Chest", 2, 0, 0, 0));
            toAdd.Add(new("counter", "Counter", 3, 0, 0, 0));
            toAdd.Add(new("desk", "Desk", 4, 0, 0, 0));
            toAdd.Add(new("drawers", "Drawers",  2, 0, 0, 0));
            toAdd.Add(new("ladder", "Ladder", 2, 0, 0, 0));
            toAdd.Add(new("lighting", "Lighting", 1, 0, 0, 1));
            toAdd.Add(new("organ", "Organ", 6, 4, 0, 0));
            toAdd.Add(new("pew", "Pew", 3, 0, 0, 0));
            toAdd.Add(new("portrait", "Portrait", 1, 0, 0, 0));
            toAdd.Add(new("shelf", "Shelf", 2, 0, 0, 0));
            toAdd.Add(new("sink", "Sink", 2, 2, 0, 0));
            toAdd.Add(new("stand", "Stand", 1, 0, 0, 1));
            toAdd.Add(new("stool", "Stool", 2, 0, 0, 0));
            toAdd.Add(new("range", "Range", 1, 2, 0, 0));
            toAdd.Add(new("table", "Table", 3, 0, 0, 0));
            toAdd.Add(new("tableLarge", "Large table", 5, 0, 0, 0));
            toAdd.Add(new("votary", "Votary", 1, 0, 0, 1));
            toAdd.Add(new("wardrobe", "Wardrobe", 4, 0, 0, 0));
            toAdd.Add(new("wheel", "Wheel", 1, 0, 0, 0));

            for (int i = 0; i < toAdd.Count; i++) {
                hotspotLib.TryAdd(toAdd[i].ID, toAdd[i]);
            }
        }
    }
}
