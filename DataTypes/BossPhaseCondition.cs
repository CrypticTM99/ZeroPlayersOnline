namespace ZeroPlayersOnline.DataTypes {
    public class BossPhaseCondition {
        public int NewPhase = 0;
        public string When = ""; // HealthAbove, HealthBelow, Hit, Attack, TimeMod
        public int TriggerNum = 0;

        public int HealOnTransition = 0; // Clamped to their max HP, so just put their max HP or whatever to do full heals

        public string ChangeMessage = "";
        public int colR = 255;
        public int colG = 255;
        public int colB = 255;

        public BossPhaseCondition(int toPhase, string when, int num, string msg, int r = 255, int g = 255, int b = 255, int heal = 0) {
            NewPhase = toPhase;
            When = when;
            TriggerNum = num;

            ChangeMessage = msg;
            colR = r;
            colG = g;
            colB = b;

            HealOnTransition = heal;
        }

        public BossPhaseCondition(int toPhase, string when, int num, string msg, Color col, int heal = 0) {
            NewPhase = toPhase;
            When = when;
            TriggerNum = num;

            ChangeMessage = msg;
            colR = col.R;
            colG = col.G;
            colB = col.B;

            HealOnTransition = heal;
        }

        public ColoredString GetMessage() {
            return new ColoredString(ChangeMessage, new Color(colR, colG, colB), Color.Black);
        }
    }
}
