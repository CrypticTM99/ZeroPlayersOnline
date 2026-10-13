using Newtonsoft.Json;

namespace ZeroPlayersOnline.DataTypes {
    public class BossPhase { 
        public string Name = "";
        public bool ProperName = false;
        public int CurrentHP = 1;
        public int MaxHP = 1;

        public string WeakType = "Slash";
        public List<string> ImmuneTypes = new();

        public int DamageReduction = 0; // Reduce damage by this as a percent unless using their weakness

        public string DefaultDmgDice = "1d3";  
        public string DefaultDmgType = "Typeless";
        public int DefaultPoisonSeverity = 0;
        public List<BossAttack> Specials = new();
          
        public double AttackSpeedInMS = 1000;
        public int AttacksBetweenSpecials = 3; 

        public List<BossPhaseCondition> PhaseConditions = new();

        public int HitActionOdds = 0; // 1 in this that hit actions will be triggeredd
        public List<RunAction> HitActions = new();

        public List<RunAction> DeathActions = new();

        public List<Requirement> InvincibleIfAll = new();

        public BossPhase(string name, int maxHP, string weak, int dr, string defDmgDice, string defDmgType, double attackSpeed = 1000, int attacksBetween = 3, bool properName = true, List<string>? immunes = null) {
            Name = name;
            ProperName = properName;
            MaxHP = maxHP;
            CurrentHP = maxHP;

            WeakType = weak;
            DamageReduction = dr;

            DefaultDmgDice = defDmgDice;
            DefaultDmgType = defDmgType;
            AttackSpeedInMS = attackSpeed;
            AttacksBetweenSpecials = attacksBetween;

            if (immunes != null) {
                ImmuneTypes = immunes;
            }
        }
    }
}
