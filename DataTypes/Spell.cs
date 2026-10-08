using Newtonsoft.Json;
using ZeroPlayersOnline.Managers;

namespace ZeroPlayersOnline.DataTypes {
    public class Spell {
        public string ID = "";
        public string Name = "";
        public string Book = "";
        public string Category = "";
        public string Description = "";

        public int Level = 0;
        public int ExpOnCast = 0;

        public int Tier = 1;
        public string MiscString = "";
        public string MiscString2 = "";


        public double CooldownInMS = 0; 
        
        public List<string> Runes = new();

        public List<Requirement> ToCast = new();

        public List<RunAction> CastActions = new();

        
        [JsonIgnore]
        public double TimeLastCast = 0;


        public Spell(string id, string name, string book, int level, int exp, List<string> runes, string desc = "", string cat = "", int tier = 1, string misc = "", string misc2 = "", double cd = 0, List<Requirement>? reqs = null, List<RunAction>? acts = null) {
            ID = id;
            Name = name;
            Book = book;
            Level = level;
            ExpOnCast = exp;
            Runes = runes;
            Category = cat;
            Tier = tier;
            MiscString = misc;
            MiscString2 = misc2;
            Description = desc;

            CooldownInMS = cd;

            if (reqs != null)
                ToCast = reqs;

            if (acts != null)
                CastActions = acts;
        }

        public void Cast(Player p, MessageLog Log, List<Skill> RecentlyTrained) {
            if (p.HasAllItems(["tab" + ID])) {
                p.ConsumeItems(["tab" + ID]);
            } else {
                p.ConsumeItems(Runes);
            } 

            TimeLastCast = Helper.Time();
            p.TryGrantExp("Magic", ExpOnCast, Log, RecentlyTrained);
        }

    }
}
