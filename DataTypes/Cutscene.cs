using Microsoft.Xna.Framework.Audio;

namespace ZeroPlayersOnline.DataTypes {
    public class Cutscene {
        public string ID = "";
        public string DisplayTitle = "";

        public List<CutsceneScene> Scenes = new();

        public List<RunAction> Actions = new();

        public List<string> ItemsGiven = new();

        public List<Requirement> RequirementsToStart = new();

        public Cutscene(string id, string title, List<CutsceneScene> scenes, List<RunAction>? actions = null, List<string>? items = null, List<Requirement>? reqs = null) {
            ID = id;
            DisplayTitle = title;
            Scenes = scenes;

            if (actions != null) {
                Actions = actions;
            }

            if (items != null) {
                ItemsGiven = items;
            }

            if (reqs != null) {
                RequirementsToStart = reqs;
            }
        }

        public void ExecuteChanges() {
            foreach (var act in Actions) {
                act.Execute();
            }

            foreach (var item in ItemsGiven) {
                string[] split = item.Split(",");
                if (GameLoop.ZPO.ResolveItem(split[0]) is Item give) {
                    int qty = 1;
                    if (split.Length > 1) {
                        int.TryParse(split[1], out qty);
                    }
                    GameLoop.ZPO.player.TryPickup(give, qty);
                }
            }

            foreach (var req in RequirementsToStart) {
                if (req.RequirementType == "Item" && req.ConsumeItem) {
                    GameLoop.ZPO.player.ConsumeItems([req.MiscString + "," + req.MiscInt]);
                }
            }
        }
    }
}
