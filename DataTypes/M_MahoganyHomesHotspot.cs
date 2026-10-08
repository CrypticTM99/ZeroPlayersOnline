using Newtonsoft.Json;

namespace ZeroPlayersOnline.DataTypes {
    public class M_MahoganyHomesHotspot {
        public string Name = "";
        public string ID = "";

        public bool Repaired = false;
        public int PlanksNeeded = 0; // Also implicitly requires 2x this many nails of the same tier
        public int BarsNeeded = 0;
        public int FabricNeeded = 0; // Come up with some scheme of tiered fabrics, maybe the things to craft mage armor like spidersilk and batwing etc. For now just requires bolts of cloth
        public int CandlesNeeded = 0; // Maybe tier this somehow too, for now just always default to normal white candles
        
        [JsonConstructor]
        public M_MahoganyHomesHotspot() { }

        public M_MahoganyHomesHotspot(string id, string name, int planks, int bars, int fabric, int candles) {
            Name = name;
            ID = id;
            
            PlanksNeeded = planks;
            BarsNeeded = bars;
            FabricNeeded = fabric;
            CandlesNeeded = candles;
        }

        public M_MahoganyHomesHotspot(M_MahoganyHomesHotspot other) {
            Name = other.Name;
            ID = other.ID;
            
            PlanksNeeded = other.PlanksNeeded;
            BarsNeeded = other.BarsNeeded;
            FabricNeeded = other.FabricNeeded;
            CandlesNeeded = other.CandlesNeeded;
        }


        // 1 - Pine plank, bronze nails, bronze bar
        // 10 - Oak plank, iron nails, iron bar
        // 20 - Willow plank, steel nails, steel bar
        // 30 - Teak plank, mithril nails, mithril bar
        // 40 - Maple plank, adamant nails, adamant bar
        // 50 - Acadia plank, rune nails, rune bar
        // 60 - Mahogany plank, orichalcum nails, orichalcum bar
        // 70 - Yew plank, necronium nails, necronium bar
        // 80 - Magic plank, bane nails, bane bar
        // 90 - Elder plank, elder rune nails, elder rune bar

        // Exp drops: Total hotspot points, divide by 5 to get average points per hotspot, divide by 10 to get average tier, floor, add 1, multiply by 450 exp.
        // Thus doing all level 1 materials gives you 5 / 5 = 1 / 10 = 0.1, floor = 0 + 1 base = 1 * 450 = 450 exp, approximately 0.49 gp/exp if buying ingredients at market value
        // All level 20 materials gives you 100 / 5 = 20 / 10 = 2, floor = 2 + 1 base = 3 * 300 = 1350 exp, approximately 2.11 gp/exp if buying ingredients at market value
        // All level 90 materials gives you 450 / 5 = 90 / 10 = 9, floor = 9 + 1 base = 10 * 300 = 4500 exp, for approximately 4.62 gp/exp if buying ingredients at market value

        public List<string> NeededItems(int tier) {
            List<string> mats = new(); 

            List<string> planks = [ "plankPine", "plankOak", "plankWillow", "plankTeak", "plankMaple", "plankAcadia", "plankMahogany", "plankYew", "plankMagic", "plankElder" ];
            List<string> bars = [ "barBronze", "barIron", "barSteel", "barMithril", "barAdamant", "barRune", "barOrichalcum", "barNecronium", "barBane", "barElderRune" ];
            List<string> nails = [ "nailsBronze", "nailsIron", "nailsSteel", "nailsMithril", "nailsAdamant", "nailsRune", "nailsOrichalcum", "nailsNecronium", "nailsBane", "nailsElderRune" ];

            tier = Math.Clamp(tier, 0, planks.Count - 1);

            if (PlanksNeeded > 0) { mats.Add(planks[tier] + "," + PlanksNeeded); }
            if (PlanksNeeded > 0) { mats.Add(nails[tier] + "," + (PlanksNeeded * 2)); }
            if (BarsNeeded > 0) { mats.Add(bars[tier] + "," + BarsNeeded); }
            if (FabricNeeded > 0) { mats.Add("clothBolt," + FabricNeeded); }
            if (CandlesNeeded > 0) { mats.Add("candle," + CandlesNeeded); }

            return mats;
        }
    }
}
