using ZeroPlayersOnline.Managers;
using ZeroPlayersOnline.UI;

namespace ZeroPlayersOnline.DataTypes {
    public class Connection { 
        public string Destination = "";
        public string CheckFailDest = "";
        public string AltName = "";
        public int MinimumFailDamage = 0;
        public int MaximumFailDamage = 0;

        public List<Requirement> Requirements = new();
        public bool OnlyNeedOneReq = false;
        public bool HideIfReqsNotMet = false;

        public int ExpGranted = 0;
        public string ExpTo = "";

        public bool SkillCheck = false;
        public int Level = 1;
        public string WorldStateChange = "";
        public string WorldStateID = "";
        public int WorldStateNum = 0;

        public List<string> CutscenesOnTraverse = new();
        public string SetsQuest = "";
        public int QuestStage = -1;

        public Connection(string dest, List<Requirement>? req = null, bool onlyOne = false, int exp = 0, string skill = "", string alt = "", bool check = false, int lv = 1, string checkFailDest = "", int minFailDmg = 0, int maxFailDmg = 0, bool hideIfNoReqs = false) { 
            Destination = dest;

            if (req != null)
                Requirements = req;

            OnlyNeedOneReq = onlyOne;

            ExpGranted = exp;
            ExpTo = skill;

            AltName = alt;

            SkillCheck = check;
            Level = lv;
            CheckFailDest = checkFailDest;

            MinimumFailDamage = minFailDmg;
            MaximumFailDamage = maxFailDmg;

            HideIfReqsNotMet = hideIfNoReqs;
        }


        public bool CanTraverse(Player p) {
            bool allPassed = true;
            bool anyPassed = false;

            if (!GameLoop.ZPO.Atlas.ContainsKey(Destination)) {
                return false;
            }

            if (Requirements != null) {
                for (int i = 0; i < Requirements.Count; i++) {
                    if (!Requirements[i].CheckRequirement(p, false, true)) {
                        allPassed = false;
                    } else {
                        anyPassed = true;
                    }
                }
            }

            if (OnlyNeedOneReq && anyPassed)
                return true;

            if (Level != 0 && ExpTo != "") {
                if (p.GetEffectiveSkillLevel(ExpTo) < Level) {
                    allPassed = false;
                }
            }

            return allPassed;
        }

        public void Traverse(Player p) {
            if (Requirements != null) {
                for (int i = 0; i < Requirements.Count; i++) {
                    bool passed = false;

                    if (Requirements[i].CheckRequirement(p, false, true)) {
                        if (Requirements[i].RequirementType == "Item" && Requirements[i].ConsumeItem) {
                            if (Requirements[i].MiscString == "Gold") {
                                p.TakeGold(Requirements[i].MiscInt);
                            } else {
                                p.ConsumeItems([Requirements[i].MiscString + "," + Requirements[i].MiscInt], false, true);
                            }
                        }

                        passed = true;
                    }

                    if (passed && OnlyNeedOneReq) {
                        break;
                    }
                }
            }

            if (SkillCheck && ExpTo != "") {
                int toBeat = 50 + (p.GetEffectiveSkillLevel(ExpTo) - Level);

                if (GameLoop.rand.Next(100) > toBeat) {
                    if (CheckFailDest != "" && GameLoop.ZPO.Atlas.ContainsKey(CheckFailDest)) {
                        GameLoop.ZPO.Log.AddMessage("You fail the " + ExpTo + " check and end up somewhere else.", Color.Crimson);
                        p.NavLoc = CheckFailDest;
                    } else {
                        GameLoop.ZPO.Log.AddMessage("You fail the " + ExpTo + " check and gain no experience.", Color.Crimson);
                    }

                    if (MaximumFailDamage > 0) {
                        int dmgRoll = MinimumFailDamage;

                        if (MaximumFailDamage > MinimumFailDamage) {
                            dmgRoll = GameLoop.rand.Next(MaximumFailDamage - MinimumFailDamage) + MinimumFailDamage;
                        }

                        if (dmgRoll > 0) {
                            p.TakeDamage(dmgRoll, GameLoop.ZPO.Log);
                            GameLoop.ZPO.Log.AddMessage("You take " + dmgRoll + " damage from failing.", Color.Crimson);
                        }
                    }

                    return;
                }
            }

            if (WorldStateChange != "" && WorldStateID != "") {
                Helper.AlterWorldState(WorldStateID, WorldStateChange, WorldStateNum);
            }

            if (SetsQuest != "") { p.TryProgressQuest(SetsQuest, QuestStage); }

            if (ExpTo != "")
                p.TryGrantExp(ExpTo, ExpGranted, GameLoop.ZPO.Log, SidebarManager.RecentlyTrainedSkills, false); 
            p.NavLoc = Destination;

            if (GameLoop.SoundManager.MusicEnabled && p.MusicMode == "Auto") {
                if (GameLoop.ZPO.Atlas.TryGetValue(Destination, out Location? dest)) {
                    if (dest.UPCsong != "" && GameLoop.SoundManager.CurrentSong != dest.UPCsong) {
                        GameLoop.SoundManager.CurrentSong = "";
                        GameLoop.SoundManager.music.StopAllSounds();
                        GameLoop.SoundManager.PickMusic(dest.UPCsong);
                    }
                }
            }

            if (ExtraWindows.CompassRoute.Count > 0 && ExtraWindows.CompassRoute[0] == Destination) {
                ExtraWindows.CompassRoute.RemoveAt(0);

                if (ExtraWindows.CompassRoute.Count == 0) {
                    GameLoop.ZPO.Log.AddMessage("You have arrived at your destination. Set a new one at any time.", Color.Lime);
                }
            }

            MinigameManager.POH_ViewingRoom = 0;

            UI_LocCombat.AttackingBoss = false;
            UI_LocCombat.AttackingMonster = null;

            foreach (var kv in CutscenesOnTraverse) {
                if (GameLoop.ZPO.CutsceneLibrary.TryGetValue(kv, out Cutscene? cut)) {
                    bool allReqsMet = true;
                    foreach (var req in cut.RequirementsToStart) {
                        if (!req.CheckRequirement(p, false, true)) {
                            allReqsMet = false;
                        }
                    }

                    if (allReqsMet) {
                        ExtraWindows.Cutscene.IsVisible = true;
                        ExtraWindows.CutsceneID = kv; 
                        ExtraWindows.CutsceneScene = 0;
                        ExtraWindows.CutsceneDialogue = 0;
                        
                        break;
                    }
                }
            }
        }
    }
}
