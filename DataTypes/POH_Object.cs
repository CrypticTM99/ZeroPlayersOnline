using Newtonsoft.Json;

namespace ZeroPlayersOnline.DataTypes {
    public class POH_Object {
        public string HotspotType = "";

        public string Name = "";
        public string Examine = "";

        public List<string> MaterialsNeeded = new();
        public List<string> ToolsNeeded = new();

        public int ExpGiven = 0;
        // Exp guidelines: 30 * tier for planks, nails no bonus exp, cloth +15, bar 5 * tier, clay +10, gold leaf +300, condensed gold and magic stone +1000, limestone +10, marble +500

        public List<Requirement> RequirementsToBuild = new();

        public int ComfortAdded = 0;
        
        public string AddsGather = "";
        public string AddsStation = "";
        public string SetsPatch = "";
        public List<string> ShopItems = new();

        public string ActivateText = "";
        public List<RunAction> Activation = new();  

        public int colR = 255;
        public int colG = 255;
        public int colB = 255;

        [JsonConstructor]
        public POH_Object() {}


        public POH_Object(string hotspot, string name, string desc, List<string> mats, List<string> tools, int exp, int r, int g, int b, List<Requirement>? reqs = null, int comfort = 0, string gather = "", string station = "", string patch = "", string activateText = "", List<RunAction>? acts = null, List<string>? shops = null) {
            HotspotType = hotspot;
            Name = name;
            Examine = desc;

            MaterialsNeeded = mats;
            ToolsNeeded = tools;

            ExpGiven = exp;
            
            ComfortAdded = comfort;
            AddsGather = gather;
            AddsStation = station;
            SetsPatch = patch;

            if (reqs != null) {
                RequirementsToBuild = reqs;
            }

            ActivateText = activateText;
            if (acts != null) { 
                Activation = acts;
            }

            if (shops != null) {
                ShopItems = shops;
            }

            colR = r;
            colG = g;
            colB = b;
        }

        public POH_Object(string hotspot, string name, string desc, List<string> mats, List<string> tools, int exp, Color col, List<Requirement>? reqs = null, int comfort = 0, string gather = "", string station = "", string patch = "", string activateText = "", List<RunAction>? acts = null, List<string>? shops = null) {
            HotspotType = hotspot;
            Name = name;
            Examine = desc;

            MaterialsNeeded = mats;
            ToolsNeeded = tools;

            ExpGiven = exp;
            
            ComfortAdded = comfort;
            AddsGather = gather;
            AddsStation = station;
            SetsPatch = patch;

            if (reqs != null) {
                RequirementsToBuild = reqs;
            }

            ActivateText = activateText;
            if (acts != null) { 
                Activation = acts;
            }

            if (shops != null) {
                ShopItems = shops;
            }

            colR = col.R;
            colG = col.G;
            colB = col.B;
        }

        public ColoredString GetName() {
            return new ColoredString(Name, new Color(colR, colG, colB), Color.Black);
        }

        public bool CanBuild() {
            Player p = GameLoop.ZPO.player;

            if (!p.HasAllItems(MaterialsNeeded)) { return false; }
            if (!p.HasAllItems(ToolsNeeded)) { return false; }
            
            foreach (var req in RequirementsToBuild) {
                if (!req.CheckRequirement(p, false, false)) { return false; }
            }

            return true;
        }
    }
}
