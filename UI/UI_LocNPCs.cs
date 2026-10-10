using SadConsole.Input; 
using ZeroPlayersOnline.DataTypes;
using ZeroPlayersOnline.Managers;
using Key = SadConsole.Input.Keys;

namespace ZeroPlayersOnline.UI {
    public static class UI_LocNPCs {  
        public static Rectangle ActivityRect = new Rectangle(new Point(111, 10), new Point(147, 34));
        public static int ActivityItemTop = 0; 
         
        public static int CurrDialogueStage = -1;
        public static NPC? ConversationPartner = null;

        public static void DrawNPCs(Location curr, Player player, UI_EmbeddedMini mini, int resourceX, int resourceY, MessageLog Log) { 
            Point mousePos = new MouseScreenObjectState(mini.Con, GameHost.Instance.Mouse).CellPosition;

            mini.Con.Print(resourceX + 2, resourceY++, "NPCs Here");
            int startY = resourceY;

            for (int i = 0; i < curr.NPCsHere.Count; i++) {
                if (GameLoop.ZPO.NPCLibrary.TryGetValue(curr.NPCsHere[i], out NPC? thisOne) && thisOne != null) {  
                    if (thisOne.ReqsToSee != null && thisOne.ReqsToSee.Count > 0) {
                        bool reqsMet = true;
                        foreach (var req in thisOne.ReqsToSee) {
                            if (!req.CheckRequirement(player, true, true)) {
                                reqsMet = false;
                            }
                        }

                        if (!reqsMet) {
                            continue;
                        }
                    }

                    mini.Con.PrintClickable(resourceX + 2, resourceY, "| " + thisOne.Name, () => { 
                        if (!ClueLogic.GenericStep(player, Log, "Speak", thisOne.ID) && !ClueLogic.GenericStep(player, Log, "Anagram", thisOne.ID)) {
                            CurrDialogueStage = 0;
                            ConversationPartner = thisOne;

                            if (ConversationPartner.Dialogue.ContainsKey(CurrDialogueStage)) {
                                string altSpeaker = ConversationPartner.Dialogue[CurrDialogueStage].AltSpeaker;
                                if (altSpeaker == "") {
                                    foreach (var text in ConversationPartner.Dialogue[CurrDialogueStage].Text) {
                                        if (text.CheckReq()) {
                                            if (text.Text != "")
                                                Log.AddMessage(ConversationPartner.Name + ": " + text.Text.Replace("@", player.Name));
                                            break;
                                        }
                                    } 
                                } else {
                                    if (altSpeaker == "-") { 
                                        foreach (var text in ConversationPartner.Dialogue[CurrDialogueStage].Text) {
                                            if (text.CheckReq()) { 
                                                if (text.Text != "")
                                                    Log.AddMessage(text.Text.Replace("@", player.Name));
                                                break;
                                            }
                                        }
                                    } else { 
                                        foreach (var text in ConversationPartner.Dialogue[CurrDialogueStage].Text) {
                                            if (text.CheckReq()) { 
                                                if (text.Text != "")
                                                    Log.AddMessage(altSpeaker + ": " + text.Text.Replace("@", player.Name));
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            GameLoop.ZPO.SelectedMenu = "Chat";
                        }
                    });

                    int extraButtons = 146;
                    if (thisOne.SlayerTasks.Count > 0) {
                        mini.Con.PrintClickable(extraButtons, resourceY, "S", () => {
                            if (player.GetEffectiveSkillLevel("Slayer") < thisOne.MinSlayerLevel) {
                                Log.AddMessage("You need at least " + thisOne.MinSlayerLevel + " Slayer to receive a task from them (have " + player.GetEffectiveSkillLevel("Slayer") + ").", Color.Crimson);
                                return;
                            }

                            if (player.GetEffectiveSkillLevel("Slayer") < thisOne.MinCombatLevel) {
                                Log.AddMessage("You need at least " + thisOne.MinCombatLevel + " Combat to receive a task from them (have " + player.GetCombatLevel() + ").", Color.Crimson);
                                return;
                            }

                            if (player.SlayerTask == "") { 
                                SlayerTask? task = Helper.ChooseWeighted(thisOne.SlayerTasks);
                                while (task != null && player.SlayerBlocked.Contains(task.TargetID)) {
                                    task = Helper.ChooseWeighted(thisOne.SlayerTasks);
                                }

                                if (task != null) { 
                                    player.SlayerTask = task.TargetID;
                                    player.SlayerTaskFrom = thisOne.ID;
                                    player.SlayerKillsRemaining = (task.KillMin >= task.KillMax) ? task.KillMin : GameLoop.rand.Next(task.KillMax - task.KillMin) + task.KillMin;
                                    player.SlayerAssignedKills = player.SlayerKillsRemaining;

                                    if (player.SlayerPrefer.Contains(player.SlayerTask)) {
                                        int extendCount = (int) Math.Ceiling(player.SlayerAssignedKills * 0.2);
                                        player.SlayerKillsRemaining += extendCount;   
                                        player.SlayerTaskExtended = true;
                                        Log.AddMessage("Your new task is to kill " + player.SlayerKillsRemaining + " " + GameLoop.ZPO.ResolveMonsterName(player.SlayerTask) + "(s). Due to your preference, the task was automatically extended.", Color.MediumPurple);
                                    } else {
                                        Log.AddMessage("Your new task is to kill " + player.SlayerKillsRemaining + " " + GameLoop.ZPO.ResolveMonsterName(player.SlayerTask) + "(s).", Color.MediumPurple);
                                    }

                                }
                            } else { 
                                Log.AddMessage("You've already got a task to kill " + player.SlayerKillsRemaining + " " + GameLoop.ZPO.ResolveMonsterName(player.SlayerTask) + "(s).", Color.Crimson);
                            }
                        });

                        extraButtons -=2 ;
                    }

                    if (thisOne.PickpocketLevel > 0) {
                        mini.Con.PrintClickable(extraButtons, resourceY, "P", () => {
                            thisOne.TryPickpocket(player, SidebarManager.RecentlyTrainedSkills, GameLoop.ZPO.ItemLibrary, Log);
                        });

                        extraButtons -= 2;
                    }
                                
                    resourceY++; 
                }
            } 

            if (resourceY == startY) {
                mini.Con.Print(resourceX + 2, resourceY, "|");
                mini.Con.Print(resourceX + 4, resourceY++, "(no NPCs here)", Color.DarkSlateGray);
            }
        }
    }
}
