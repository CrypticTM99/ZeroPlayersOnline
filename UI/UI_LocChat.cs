using SadConsole.Input; 
using ZeroPlayersOnline.DataTypes;
using ZeroPlayersOnline.Managers;
using Key = SadConsole.Input.Keys;

namespace ZeroPlayersOnline.UI {
    public static class UI_LocChat {  
        public static Rectangle ActivityRect = new Rectangle(new Point(111, 10), new Point(147, 34));
        public static int ActivityItemTop = 0; 
        public static bool WithdrawingNotes = false;

        public static void DrawChat(Location curr, Player player, UI_EmbeddedMini mini, int resourceX, int resourceY, MessageLog Log) { 
            Point mousePos = new MouseScreenObjectState(mini.Con, GameHost.Instance.Mouse).CellPosition; 

            if (UI_LocNPCs.ConversationPartner != null) {
                if (UI_LocNPCs.ConversationPartner.Dialogue.ContainsKey(UI_LocNPCs.CurrDialogueStage)) {
                    DialogueStage dia = UI_LocNPCs.ConversationPartner.Dialogue[UI_LocNPCs.CurrDialogueStage];

                    if (dia.Choices != null && dia.Choices.Count > 0) {
                        List<DialogueChoice> preCheck = dia.Choices.Where(o => (o.CanClick() || o.ShowAnyways)).ToList();
                                
                        for (int i = 0; i < preCheck.Count; i++) {
                            DialogueChoice choice = preCheck[i];


                            mini.Con.Print(resourceX + 2, resourceY, "|");

                            Color col = choice.CanClick() ? Color.White : Color.Crimson;

                            bool clickedResponse = false;

                            resourceY = mini.Con.PrintMultiLineClickable(resourceX + 4, resourceY++, choice.Text, 35, () => {
                                if (choice.CanClick()) {
                                    UI_LocNPCs.CurrDialogueStage = choice.LeadsToStage;

                                    if (UI_LocNPCs.ConversationPartner.Dialogue.ContainsKey(UI_LocNPCs.CurrDialogueStage)) {
                                        string altSpeaker = UI_LocNPCs.ConversationPartner.Dialogue[UI_LocNPCs.CurrDialogueStage].AltSpeaker;
                                        if (altSpeaker == "") {
                                            foreach (var text in UI_LocNPCs.ConversationPartner.Dialogue[UI_LocNPCs.CurrDialogueStage].Text) {
                                                if (text.CheckReq()) {
                                                    if (text.Text != "")
                                                        Log.AddMessage(UI_LocNPCs.ConversationPartner.Name + ": " + text.Text.Replace("@", player.Name));
                                                    break;
                                                }
                                            } 
                                        } else {
                                            if (altSpeaker == "-") { 
                                                foreach (var text in UI_LocNPCs.ConversationPartner.Dialogue[UI_LocNPCs.CurrDialogueStage].Text) {
                                                    if (text.CheckReq()) {
                                                        if (text.Text != "")
                                                            Log.AddMessage(text.Text.Replace("@", player.Name));
                                                        break;
                                                    }
                                                }
                                            } else { 
                                                foreach (var text in UI_LocNPCs.ConversationPartner.Dialogue[UI_LocNPCs.CurrDialogueStage].Text) {
                                                    if (text.CheckReq()) {
                                                        if (text.Text != "")
                                                            Log.AddMessage(altSpeaker + ": " + text.Text.Replace("@", player.Name));
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    bool teleported = false;

                                    if (choice.TeleportTo != "") {
                                        player.NavLoc = choice.TeleportTo;

                                        if (choice.SetSpawnToo) {
                                            player.NavRespawn = choice.TeleportTo;
                                        }

                                        teleported = true;
                                    }   

                                    choice.ConsumeItemsIfNeeded(player); 

                                    foreach (var kv in choice.Cutscenes) {
                                        if (GameLoop.ZPO.CutsceneLibrary.TryGetValue(kv, out Cutscene? cut)) {
                                            foreach (var req in cut.RequirementsToStart) {
                                                if (!req.CheckRequirement(player, false, true)) {
                                                    continue;
                                                }
                        
                                                ExtraWindows.Cutscene.IsVisible = true;
                                                ExtraWindows.CutsceneID = kv; 
                                                ExtraWindows.CutsceneScene = 0;
                                                ExtraWindows.CutsceneDialogue = 0;

                                                foreach (var act in cut.Actions) {
                                                    act.Execute();
                                                }

                                                foreach (var item in cut.ItemsGiven) {
                                                    string[] split = item.Split(",");
                                                    if (GameLoop.ZPO.ResolveItem(split[0]) is Item give) {
                                                        int qty = 1;
                                                        if (split.Length > 1) {
                                                            int.TryParse(split[1], out qty);
                                                        }
                                                        player.TryPickup(give, qty);
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }

                                    if (UI_LocNPCs.ConversationPartner.Dialogue.ContainsKey(UI_LocNPCs.CurrDialogueStage)) {
                                        DialogueStage newDia = UI_LocNPCs.ConversationPartner.Dialogue[UI_LocNPCs.CurrDialogueStage];

                                        if (newDia.SetsQuest != "") { player.TryProgressQuest(newDia.SetsQuest, newDia.SetsQuestStageTo); }

                                        if (newDia.ItemsGiven != null) {
                                            for (int i = 0; i < newDia.ItemsGiven.Count; i++) {
                                                if (newDia.ItemsGiven[i].Contains(",")) {
                                                    string[] split = newDia.ItemsGiven[i].Split(",");
                                                    if (split[0] == "Gold") {
                                                        int.TryParse(split[1], out int qty);
                                                        player.GiveGold(qty); 
                                                    } else {
                                                        if (GameLoop.ZPO.ItemLibrary.TryGetValue(split[0], out Item? give)) {
                                                            if (give != null) {
                                                                Item actualGive = new(give);

                                                                if (int.TryParse(split[1], out int qty)) {
                                                                    actualGive.Quantity = qty;
                                                                }

                                                                player.TryPickup(new Item(actualGive), actualGive.Quantity);
                                                            }
                                                        }
                                                    }
                                                } else {
                                                    if (GameLoop.ZPO.ItemLibrary.TryGetValue(newDia.ItemsGiven[i], out Item? give)) {
                                                        if (give != null) {
                                                            player.TryPickup(new Item(give), give.Quantity);
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        foreach (var act in newDia.Actions) {
                                            act.Execute();
                                        } 
                                    }

                                    if (teleported) {
                                        UI_LocNPCs.ConversationPartner = null;
                                    }

                                    clickedResponse = true;
                                } else {
                                    if (choice.ClickReqs != null) {
                                        Log.AddMessage(new ColoredString("Requirement(s) not met: ", Color.Crimson, Color.Black));
                                        for (int i = 0; i < choice.ClickReqs.Count; i++) {
                                            Log.AddMessage("| " + choice.ClickReqs[i].GetSummary(), choice.ClickReqs[i].CheckRequirement(player, true, true) ? Color.Lime : Color.Crimson);
                                        }
                                    }
                                }
                            }, col.R, col.G, col.B);
                        }
                    }

                    if (UI_LocNPCs.CurrDialogueStage == -1) {
                        UI_LocNPCs.ConversationPartner = null;
                        GameLoop.ZPO.SelectedMenu = "NPCs";
                    }
                }
                else {
                    mini.Con.Print(resourceX + 2, resourceY, "| (invalid dialogue stage)", Color.DarkSlateGray);
                }
            }
            else {
                mini.Con.Print(resourceX + 2, resourceY, "| (not speaking to anyone)", Color.DarkSlateGray);
            }
        }
    }
}
