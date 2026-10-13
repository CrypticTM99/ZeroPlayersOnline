using Newtonsoft.Json;

namespace ZeroPlayersOnline.DataTypes {
    public class BossFight {
        public string ID = ""; 
         
        public int Level = 1;

        public string SpecialCategory = "";
        public List<string> CountsAsSlayer = new(); 
        public string KillItem = "";

        public int AggroLevel = 0; // If the players combat level is below this, the monster will automatically attack them 
        public bool AlwaysAggro = false;
         
        public int LanesHere = 3;
        public int RespawnTime = 1; 

        public List<ItemDrop> DropTable = new();
        public double ExpMultiplier = 1.0; 

        public List<BossPhase> Phases = new();

        [JsonIgnore]
        public int CurrentPhase = 0;
        [JsonIgnore]
        public int CurrentLane = 0;
        [JsonIgnore]
        public int UsingMove = -1;
        [JsonIgnore]
        public double TimeLastKilled = 0;
        [JsonIgnore]
        public bool AttackingPlayer = false;
        [JsonIgnore]
        public double TimeLastAttacked = 0;
        [JsonIgnore]
        public double MovesSinceSpecial = 0;
        [JsonIgnore]
        public int LaneWhenMoveSelected = -1;

        public BossFight(string id, int level, int respawn, List<BossPhase> phases, int lanes = 3, bool aggro = false, string specialCat = "") {
            ID = id;  
            Level = level; 
            RespawnTime = respawn;
            
            if (aggro)
                AggroLevel = (level * 2) + 1;
            AlwaysAggro = aggro; 

            LanesHere = lanes;
            SpecialCategory = specialCat;

            Phases = phases;
            
        }
    }
}
