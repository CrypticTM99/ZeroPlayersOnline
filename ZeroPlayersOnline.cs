using GoRogue.DiceNotation.Terms;
using SadConsole.Entities;
using SadConsole.Input;
using SadConsole.UI; 
using System.Diagnostics;
using System.Reflection;
using ZeroPlayersOnline.DataTypes; 
using ZeroPlayersOnline.HardcodedData;
using ZeroPlayersOnline.Hardcodes;
using ZeroPlayersOnline.Managers;
using ZeroPlayersOnline.UI; 
using Key = SadConsole.Input.Keys;

namespace ZeroPlayersOnline {
    public class ZeroPlayersOnline : MiniDream {
        public Player player;

        public Dictionary<string, Location> Atlas = new();
        public Dictionary<string, GatheringTile> GatherSpots = new();
        public Dictionary<string, Item> ItemLibrary = new();
        public Dictionary<string, ProcessingStation> ProcessingStations = new();
        public Dictionary<TwoWayString, Recipe> UseRecipes = new();
        public Dictionary<string, NPC> NPCLibrary = new();
        public Dictionary<string, AreaMonster> MonsterLibrary = new();
        public Dictionary<string, Prayer> PrayerLibrary = new();
        public Dictionary<string, List<CraftRecipe>> CraftLib = new();
        public Dictionary<string, Spell> SpellLibrary = new();
        public Dictionary<string, BossFight> BossLibrary = new();
        public Dictionary<string, HunterCreature> HunterLibrary = new();

        public Dictionary<string, ClueStep> ClueStepLibrary = new();
        public Dictionary<string, Quest> QuestLibrary = new();
        public Dictionary<string, Book> BookLibrary = new();
        public Dictionary<string, Cutscene> CutsceneLibrary = new();
        public Dictionary<string, M_MahoganyHomesHotspot> M_HotspotLib = new();
        public Dictionary<string, POH_Object> POH_ObjectLib = new();

        public MessageLog Log = new();
        public int LastPrinted = 0;
        
        public string SelectedMenu = "Resources";


        public int SecondsSinceAutosave = 0;
        public double TimeLastTicked = 0;
        public static double HazardWarningTime = 0;

        public Rectangle ActivityRect = new Rectangle(new Point(111, 10), new Point(147, 34));
        public int ActivityItemTop = 0;


        public ZeroPlayersOnline() {
            ExtraWindows.SetupWindows();

            player = new();

            RebuildLibraries();

            TryAddSkills();
            TryAddPrayers(); 
            TryAddQuests();

            player.CurrentHP = 10;


            Log.AddMessage(new ColoredString("Press F1 at any time to open the guidebook, or F2 to open the compendium.", Color.Turquoise, Color.Black));
        }
         
        public void LocationDraw(UI_EmbeddedMini mini) { 
            Point mousePos = new MouseScreenObjectState(mini.Con, GameHost.Instance.Mouse).CellPosition;

            if (Atlas.TryGetValue(player.NavLoc, out Location? curr) && curr != null) { 
                PlayerOwnedHome? home = null;

                if (curr.PlayerHomeHere != "" && player.Homes.TryGetValue(curr.PlayerHomeHere, out PlayerOwnedHome? tempHome)) {
                    home = tempHome;
                }

                if (curr.UPCsong != "") {
                    if (!player.AllSongsUnlocked && !player.UnlockedSongs.Contains(curr.UPCsong)) { 
                        player.UnlockedSongs.Add(curr.UPCsong); 
                        Log.AddMessage("You have unlocked the song: " + curr.UPCsong, Color.Turquoise);
                    }

                    if (player.MusicMode == "Auto" && GameLoop.SoundManager.CurrentSong != curr.UPCsong && GameLoop.SoundManager.PlayingSounds.Count == 0) {
                        GameLoop.SoundManager.PickMusic(curr.UPCsong);
                    }
                }

                string dispName = curr.DisplayName;

                if (curr.MazeMap != "") {
                    foreach (var kv in player.Inventory) {
                        if (kv.GetRef() is Item item) {
                            if (item.UseString == "Map" && item.UseString2 == curr.MazeMap) {
                                dispName = curr.DisplayName + " (" + curr.MazeTile + ")";
                            }
                        }
                    }

                    foreach (var kv in player.Equipment) {
                        if (kv.Value.GetRef() is Item item) {
                            if (item.UseString == "Map" && item.UseString2 == curr.MazeMap) {
                                dispName = curr.DisplayName + " (" + curr.MazeTile + ")";
                            }
                        }
                    }
                }


                if (home != null) {
                    POH_Room room = home.Rooms[MinigameManager.POH_ViewingRoom];
                    dispName += " - " + (room.RoomName != "" ? room.RoomName : room.RoomType + " (" + MinigameManager.POH_ViewingRoom + ")");
                }

                if (curr.Dark && !player.HasLightSource()) {
                    dispName = "Inky Darkness";
                }

                if (curr.Hazard == "Gas" && player.HasOpenFlame()) {
                    if (HazardWarningTime == 0) {
                        HazardWarningTime = Helper.Time();
                        Log.AddMessage("Your light flares brightly!", Color.Crimson);
                    } else if (HazardWarningTime + 5000 < Helper.Time()) {
                        Log.AddMessage("The exposed flame of your light causes the swamp gas to ignite!", Color.Crimson);
                        player.TakeDamage(12, Log);
                        player.ExtinguishLights(false);
                        HazardWarningTime = 0;
                    }
                } else {
                    HazardWarningTime = 0;
                }
                 
                mini.Con.Print(57, 0, dispName.Align(HorizontalAlignment.Center, 91));
                mini.Con.DrawLine(new Point(56, 1), new Point(148, 1), 196); 

                int descY = 3;

                if (!curr.Dark || (curr.Dark && player.HasLightSource())) {
                    descY = mini.Con.PrintMultiLine(57, 3, curr.Description, 92);
                } else {
                    descY = mini.Con.PrintMultiLine(57, 3, ("With a light source you could see more here.").Align(HorizontalAlignment.Center, 92) + " /n /n " + ("It is pitch black.").Align(HorizontalAlignment.Center, 92) + " /n /n " + ("You are likely to be eaten by a grue.").Align(HorizontalAlignment.Center, 92), 92);
                }
                int printY = descY;

                printY += 1;

                mini.Con.DrawLine(new Point(56, printY), new Point(148, printY++), 196);

                mini.Con.Print(57, printY, "Other Players Here: ");
                mini.Con.Print(77, printY++, "(none)", Color.DarkSlateGray);

                mini.Con.DrawLine(new Point(56, printY), new Point(148, printY++), 196);

                int resourceY = printY;
                 
                if (curr.ConnectedLocations.Count > 0) {
                    mini.Con.Print(57, printY++, "Connected Locations: ");

                    List<Connection> conns = curr.ConnectedLocations.Where(o => (o.CanTraverse(player) && o.HideIfReqsNotMet) || !o.HideIfReqsNotMet).ToList();

                    for (int i = 0; i < conns.Count; i++) {
                        if (Atlas.TryGetValue(conns[i].Destination, out Location? dest) && dest != null) { 
                            string name = dest.DisplayName; 

                            if (conns[i].AltName != "") {
                                name = conns[i].AltName;
                            }

                            if (dest.Region != curr.Region)
                                name += " [" + dest.Region + "]";

                            
                            if (dest.MazeMap != "") {
                                foreach (var kv in player.Inventory) {
                                    if (kv.GetRef() is Item item) {
                                        if (item.UseString == "Map" && item.UseString2 == dest.MazeMap) {
                                            name += " (" + dest.MazeTile + ")";
                                        }
                                    }
                                }

                                foreach (var kv in player.Equipment) {
                                    if (kv.Value.GetRef() is Item item) {
                                        if (item.UseString == "Map" && item.UseString2 == dest.MazeMap) {
                                            name += " (" + dest.MazeTile + ")";
                                        }
                                    }
                                }
                            }

                            if (curr.Dark && !player.HasLightSource()) {
                                if (dest.Dark) {
                                    name = "Inky Darkness";
                                }
                            }

                            mini.Con.PrintClickable(57, printY++, new ColoredString("| " + name, conns[i].CanTraverse(player) ? (ExtraWindows.CompassRoute.FirstOrDefault() == conns[i].Destination ? Color.Turquoise : Color.White) : Color.Crimson, Color.Black), () => {
                                if (conns[i].CanTraverse(player)) {
                                    conns[i].Traverse(player); 
                                    UI_LocCombat.GraceTimeStart = Helper.Time();
                                    UI_LocCombat.AttackingMonster = null;

                                    UI_LocNPCs.CurrDialogueStage = -1;
                                    UI_LocNPCs.ConversationPartner = null;

                                    if (curr.MonstersHere.Count > 0) {
                                        for (int j = 0; j < curr.MonstersHere.Count; j++) {
                                            curr.MonstersHere[j].AttackingPlayer = false;
                                        }
                                    }
                                } else { 
                                    if (conns[i].Requirements is List<Requirement> req) {
                                        Log.AddMessage("Missing some requirements to go there: " + (conns[i].OnlyNeedOneReq ? "(only need one)" : ""), Color.Crimson);

                                        for (int j = 0; j < req.Count; j++) {
                                            Log.AddMessage("| " + req[j].GetSummary(), req[j].CheckRequirement(player, false, true) ? Color.Lime : Color.Crimson); 
                                        }

                                        if (conns[i].ExpTo != "" && conns[i].Level > 0) {
                                            Log.AddMessage("| " + conns[i].Level + " " + conns[i].ExpTo, player.GetEffectiveSkillLevel(conns[i].ExpTo) >= conns[i].Level ? Color.Lime : Color.Crimson);
                                        }
                                    }
                                }
                            });
                        }
                        else {
                            mini.Con.Print(57, printY++, "| " + conns[i].Destination, Color.DarkSlateGray);
                        }
                    }

                    if (home != null) {
                        for (int idx = 0; idx < home.Rooms.Count; idx++) {
                            POH_Room room = home.Rooms[idx];
                            mini.Con.PrintClickable(57, printY++, new ColoredString("| " + (room.RoomName != "" ? room.RoomName : room.RoomType + " (" + idx + ")"), Color.White, Color.Black), () => {
                                MinigameManager.POH_ViewingRoom = idx;
                            });
                        }
                    }

                } 

                // Draw all the combat stuff
                printY = UI_LocCombat.DrawCombat(curr, player, mini, printY, Log);

                int resourceX = 110;

                mini.Con.DrawLine(new Point(resourceX, resourceY), new Point(resourceX, 34), 179);

                if (curr.IsBank && player.CanUseBanks)
                    mini.Con.PrintClickable(resourceX + 2, resourceY, new ColoredString("B", SelectedMenu == "Items" ? Color.Yellow : Color.White, Color.Black), () => { SelectedMenu = "Items"; });
                else
                    mini.Con.PrintClickable(resourceX + 2, resourceY, new ColoredString("I", SelectedMenu == "Items" ? Color.Yellow : curr.ItemsHere.Count > 0 ? Color.White : Color.DarkSlateGray, Color.Black), () => { SelectedMenu = "Items"; });
                
                
                mini.Con.Print(resourceX + 4, resourceY, "|");

                int npcCount = 0;

                foreach (var str in curr.NPCsHere) {
                    if (NPCLibrary.TryGetValue(str, out NPC? npc) && npc != null) {
                        if (npc.ReqsToSee != null && npc.ReqsToSee.Count > 0) {
                            npcCount++;

                            foreach (var req in npc.ReqsToSee) {
                                if (!req.CheckRequirement(player, false, true)) {
                                    npcCount--;
                                    break;
                                }
                            }
                        } else {
                            npcCount++;
                        }
                    }
                }


                mini.Con.PrintClickable(resourceX + 6, resourceY, new ColoredString("N", SelectedMenu == "NPCs" ? Color.Yellow : npcCount > 0 ? Color.White : Color.DarkSlateGray, Color.Black), () => { SelectedMenu = "NPCs"; });
                mini.Con.Print(resourceX + 8, resourceY, "|");
                mini.Con.PrintClickable(resourceX + 10, resourceY, new ColoredString("P", SelectedMenu == "Processing" ? Color.Yellow : curr.ProcessingStations.Count > 0 || curr.TempStations.Count > 0 || (home != null && home.Rooms[MinigameManager.POH_ViewingRoom].AnyStations()) ? Color.White : Color.DarkSlateGray, Color.Black), () => { SelectedMenu = "Processing"; });
                mini.Con.Print(resourceX + 12, resourceY, "|");
                mini.Con.PrintClickable(resourceX + 14, resourceY, new ColoredString("R", SelectedMenu == "Resources" ? Color.Yellow : curr.LocalGathers.Where(o => (o.CanGather(player) == "" && !o.ShowAnyways) || o.ShowAnyways).ToList().Count > 0 || (home != null && home.Rooms[MinigameManager.POH_ViewingRoom].AnyGathers()) ? Color.White : Color.DarkSlateGray, Color.Black), () => { SelectedMenu = "Resources"; });
                mini.Con.Print(resourceX + 16, resourceY, "|");
                mini.Con.PrintClickable(resourceX + 18, resourceY, new ColoredString("C", SelectedMenu == "Chat" ? Color.Yellow : UI_LocNPCs.ConversationPartner != null ? Color.White : Color.DarkSlateGray, Color.Black), () => { SelectedMenu = "Chat"; });
                mini.Con.Print(resourceX + 20, resourceY, "|");
                mini.Con.PrintClickable(resourceX + 22, resourceY, new ColoredString("S", SelectedMenu == "Shop" ? Color.Yellow : curr.ShopItemsHere.Count > 0 || (home != null && home.Rooms[MinigameManager.POH_ViewingRoom].AnyShops()) ? Color.White : Color.DarkSlateGray, Color.Black), () => { SelectedMenu = "Shop"; });
                mini.Con.Print(resourceX + 24, resourceY, "|");
                mini.Con.PrintClickable(resourceX + 26, resourceY, new ColoredString("F", SelectedMenu == "Farming" ? Color.Yellow : curr.FarmingPatchesHere.Count > 0 || (home != null && home.Rooms[MinigameManager.POH_ViewingRoom].Patch.PatchType != "") ? Color.White : Color.DarkSlateGray, Color.Black), () => { SelectedMenu = "Farming"; });
                mini.Con.Print(resourceX + 28, resourceY, "|");
                mini.Con.PrintClickable(resourceX + 30, resourceY, new ColoredString("H", SelectedMenu == "Hunter" ? Color.Yellow : (curr.HunterSpots.Count > 0 || player.SpawnedCreatures.Count > 0) ? Color.White : Color.DarkSlateGray, Color.Black), () => { SelectedMenu = "Hunter"; });
                mini.Con.Print(resourceX + 32, resourceY, "|");
                mini.Con.PrintClickable(resourceX + 34, resourceY, new ColoredString("*", SelectedMenu == "Minigame" ? Color.Yellow : MinigameManager.AnyMinigame(player) ? Color.White : Color.DarkSlateGray, Color.Black), () => { SelectedMenu = "Minigame"; });

                resourceY++;
                mini.Con.DrawLine(new Point(resourceX + 1, resourceY), new Point(148, resourceY), 196);
                resourceY++;

                if (SelectedMenu == "Resources") {
                    UI_LocResources.DrawResources(curr, player, mini, resourceX, resourceY, Log, home); 
                }
                else if (SelectedMenu == "Items") { 
                    UI_LocItems.DrawItems(curr, player, mini, resourceX, resourceY, Log);
                }
                else if (SelectedMenu == "Processing") {
                    UI_LocProcessing.DrawProcessing(curr, player, mini, resourceX, resourceY, Log, home);
                }
                else if (SelectedMenu == "NPCs") { 
                    UI_LocNPCs.DrawNPCs(curr, player, mini, resourceX, resourceY, Log);
                }
                else if (SelectedMenu == "Chat") {
                    UI_LocChat.DrawChat(curr, player, mini, resourceX, resourceY, Log);
                }
                else if (SelectedMenu == "Shop") {
                    UI_LocShop.DrawShop(curr, player, mini, resourceX, resourceY, Log, home);
                }
                else if (SelectedMenu == "Farming") {
                    UI_LocFarming.DrawFarming(curr, player, mini, resourceX, resourceY, Log, home);
                } 
                else if (SelectedMenu == "Hunter") { 
                    mini.Con.Print(resourceX + 2, resourceY++, "Hunter Creatures Here"); 

                    if (curr.CreaturesHere.Count < curr.HunterSpots.Count) { 
                        for (int i = 0; i < curr.HunterSpots.Count; i++) {
                            if (HunterLibrary.TryGetValue(curr.HunterSpots[i], out HunterCreature? hunt) && hunt != null) {
                                HunterCreature clone = Helper.Clone(hunt);
                                clone.CurrentLane = GameLoop.rand.Next(10);
                                clone.TimeSpawned = Helper.Time();
                                curr.CreaturesHere.Add(clone);
                            }
                        }
                    }

                    int placedTraps = 0;

                    if (curr.CreaturesHere.Count > 0) {
                        List<HunterCreature> uniques = new();

                        for (int i = 0; i < 10; i++) {
                            if (!curr.TrapsDown.ContainsKey(i))
                                curr.TrapsDown.Add(i, "");
                            
                            if (curr.TrapsDown[i] != "")
                                placedTraps++;
                        }

                        for (int i = 0; i < 10; i++) {
                            ColoredString line = new ColoredString("| ", Color.White, Color.Black);

                            if (curr.TrapsDown[i] != "") {
                                if (ItemLibrary.TryGetValue(curr.TrapsDown[i], out Item? trap)) {
                                    line += new ColoredString(trap.Name + " ", Color.White, Color.Black);
                                } else {
                                    line += new ColoredString(curr.TrapsDown[i] + " ", Color.DarkSlateGray, Color.Black);
                                }
                            } else {
                                line += new ColoredString("(no trap) ", Color.DarkSlateGray, Color.Black);
                            }

                            for (int j = 0; j < curr.CreaturesHere.Count; j++) {
                                bool added = false;
                                for (int checkToAdd = 0; checkToAdd < uniques.Count; checkToAdd++) {
                                    if (uniques[checkToAdd].ID == curr.CreaturesHere[j].ID)
                                        added = true;
                                }
                                if (!added)
                                    uniques.Add(curr.CreaturesHere[j]);

                                if (curr.CreaturesHere[j].TimeLastCaught != 0 && curr.CreaturesHere[j].TimeLastCaught + (curr.CreaturesHere[j].RespawnTime * 1000) > Helper.Time()) {
                                    continue;
                                }

                                if (curr.CreaturesHere[j].TimeLastMoved + 1000 < Helper.Time()) {
                                    int move = GameLoop.rand.Next(3);

                                    if (move == 0) {
                                        if (curr.CreaturesHere[j].CurrentLane > 0) {
                                            curr.CreaturesHere[j].CurrentLane--;
                                        } else {
                                            curr.CreaturesHere[j].CurrentLane++;
                                        }
                                    } else if (move == 1) {
                                        if (curr.CreaturesHere[j].CurrentLane < 9) {
                                            curr.CreaturesHere[j].CurrentLane++;
                                        } else {
                                            curr.CreaturesHere[j].CurrentLane--;
                                        }
                                    }

                                    curr.CreaturesHere[j].TimeLastMoved = Helper.Time();


                                    if (curr.TrapsDown.TryGetValue(curr.CreaturesHere[j].CurrentLane, out string? trapID) && trapID != "") {
                                        if (curr.CreaturesHere[j].CatchID == trapID) {
                                            if (player.GetEffectiveSkillLevel("Hunter") >= curr.CreaturesHere[j].CatchLevel) {
                                                int skillMod = player.GetEffectiveSkillLevel("Hunter") - curr.CreaturesHere[j].CatchLevel;

                                                if (player.Equipment.TryGetValue("Ring", out ItemWrapper? eqp) && eqp != null && eqp.ID == "ringPursuit" && GameLoop.rand.Next(4) == 0) {
                                                    eqp.Charges -= 1; 
                                                    Log.AddMessage(new ColoredString("Your ring of pursuit empowered the trap!", Color.AntiqueWhite, Color.Black));

                                                    if (eqp.Charges <= 0) { 
                                                        Log.AddMessage(new ColoredString("Your ring of pursuit runs out of charge and shatters.", Color.Crimson, Color.Black));
                                                        player.Equipment.Remove("Ring");
                                                    }
                                                    skillMod += 25;
                                                }

                                                int roll = GameLoop.rand.Next(100);

                                                if (roll < 50 + skillMod) {
                                                    player.TryGrantExp("Hunter", curr.CreaturesHere[j].CatchEXP, Log, SidebarManager.RecentlyTrainedSkills);
                                                    
                                                    foreach (var kv in curr.CreaturesHere[j].Drops) {
                                                        kv.RollDrop(player, null);
                                                    } 

                                                    curr.CreaturesHere[j].TimeLastCaught = Helper.Time();
                                                }

                                                if (ItemLibrary.TryGetValue(trapID, out Item? trap) && trap != null) {
                                                    player.TryPickup(new Item(trap), 1);
                                                }

                                                curr.TrapsDown[curr.CreaturesHere[j].CurrentLane] = "";
                                            } else {
                                                Log.AddMessage("You need " + curr.CreaturesHere[j].CatchLevel + " Hunter to catch these.", Color.Crimson);
                                            }
                                        }
                                    } 
                                }


                                if (curr.CreaturesHere[j].CurrentLane == i) {
                                    line += new ColoredString("*", curr.CreaturesHere[j].GetColor(), Color.Black);
                                }
                            }

                            if (curr.TrapsDown[i] == "") {
                                mini.Con.PrintClickable(resourceX + 2, resourceY++, line, () => {
                                    if (ItemUseLogic.UsingSlot != -1) {
                                        if (player.Inventory.Count > ItemUseLogic.UsingSlot) {
                                            if (placedTraps < 3) {
                                                curr.TrapsDown[i] = player.Inventory[ItemUseLogic.UsingSlot].ID;
                                                player.Inventory.RemoveAt(ItemUseLogic.UsingSlot);
                                                ItemUseLogic.UsingSlot = -1;
                                            } else {
                                                Log.AddMessage("You can only place up to 3 traps in a location.", Color.Crimson);
                                            }
                                        }
                                    }
                                });
                            } else {
                                mini.Con.PrintClickable(resourceX + 2, resourceY++, line, () => {
                                    if (ItemLibrary.TryGetValue(curr.TrapsDown[i], out Item? trap) && trap != null) {
                                        player.TryPickup(new Item(trap), 1);
                                    }

                                    curr.TrapsDown[i] = "";
                                });
                            }
                        }

                        for (int i = 0; i < uniques.Count; i++) {
                            ColoredString line = new ColoredString("* " + uniques[i].Name + " [" + uniques[i].CatchLevel + ", " + ResolveItemName(uniques[i].CatchID) + "]", uniques[i].GetColor(), Color.Black);
                            mini.Con.Print(resourceX + 2, 34 - i, line);
                        }
                    } else {
                        mini.Con.Print(resourceX + 2, resourceY, "|");
                        mini.Con.Print(resourceX + 4, resourceY++, "(no hunter creatures here)", Color.DarkSlateGray);
                    }
                }
                else if (SelectedMenu == "Minigame") {
                    MinigameManager.Draw(mini, player, resourceX, resourceY);
                }

                if (curr.MinigameID == "MageEnchanting") {
                    if (MinigameManager.MTA_Timer + 60000 < Helper.Time()) { MinigameManager.MTA_Timer = Helper.Time(); MinigameManager.MTA_Special = MinigameManager.MTA_Types[GameLoop.rand.Next(MinigameManager.MTA_Types.Count)]; }
                }

                if (curr.MinigameID == "MageGraveyard") { 
                    if (MinigameManager.MTA_Timer + 60000 < Helper.Time()) { MinigameManager.MTA_Timer = Helper.Time(); MinigameManager.MTA_Bones.Shuffle(); }
                    if (MinigameManager.MTA_BoneTimer + 5000 < Helper.Time()) { MinigameManager.MTA_BoneTimer = Helper.Time(); player.TakeDamage(2, GameLoop.ZPO.Log); GameLoop.ZPO.Log.AddMessage("You take two damage from the falling bones!", Color.Crimson); }
                } 

                if (curr.MinigameID == "MageAlchemist") {
                    if (MinigameManager.MTA_Timer + 60000 < Helper.Time()) { MinigameManager.MTA_Timer = Helper.Time(); MinigameManager.MTA_Alchs.Shuffle(); MinigameManager.MTA_FreeAlch = GameLoop.rand.Next(MinigameManager.MTA_Alchs.Count); }
                }
            }

            if (player.Inventory.Count > 1) {
                if (SidebarManager.LastPerformedRecipe == null) { 
                    mini.Con.Print(0, 35, "(no recent recipe)", Color.DarkSlateGray);
                } else {
                    if (GameLoop.ZPO.UseRecipes.TryGetValue(SidebarManager.LastPerformedRecipe, out Recipe? rec)) {
                        if (GameLoop.ZPO.ResolveItem(rec.OutputItem) is Item output) {
                            mini.Con.PrintClickable(0, 35, new ColoredString(236.AsString(), Color.MediumPurple, Color.Black), () => {
                                bool anySuccess = false;
                                while (ItemUseLogic.TryCombineItems(player, rec.FirstItem, rec.SecondItem)) { anySuccess = true; }
                                
                                if (!anySuccess)
                                    GameLoop.ZPO.Log.AddMessage("You don't seem to have those items anymore.", Color.Crimson);    
                            });

                            mini.Con.PrintClickable(2, 35, new ColoredString("?", Color.MediumPurple, Color.Black), () => { 
                                Log.AddMessage("Last recipe: " + (rec.FirstQty > 1 ? rec.FirstQty + "x " : "") + ResolveItemName(rec.FirstItem) + " + " + (rec.SecondQty > 1 ? rec.SecondQty + "x " : "") + ResolveItemName(rec.SecondItem) + " = " + (rec.OutputQty > 1 ? rec.OutputQty + "x " : "") + ResolveItemName(rec.OutputItem));    
                            });

                            mini.Con.PrintClickable(4, 35, "Repeat Last Recipe", () => { 
                                if (!ItemUseLogic.TryCombineItems(player, rec.FirstItem, rec.SecondItem)) {
                                    GameLoop.ZPO.Log.AddMessage("You don't seem to have those items anymore.", Color.Crimson);    
                                }
                            });
                        } else {
                            mini.Con.Print(0, 35, "(recipe broken)", Color.DarkSlateGray);
                        }
                    } else {
                        mini.Con.Print(0, 35, "(recipe not found)", Color.DarkSlateGray);
                    }
                }

                 if (SidebarManager.LastFoundRecipe != null && GameLoop.ZPO.UseRecipes.TryGetValue(SidebarManager.LastFoundRecipe, out Recipe? rec2)) {
                    if (GameLoop.ZPO.ResolveItem(rec2.OutputItem) is Item output) {
                        mini.Con.PrintClickable(33, 35, new ColoredString(236.AsString(), Color.MediumPurple, Color.Black), () => {
                                bool anySuccess = false;
                                while (ItemUseLogic.TryCombineItems(player, rec2.FirstItem, rec2.SecondItem)) { anySuccess = true; }
                                
                                if (!anySuccess)
                                    GameLoop.ZPO.Log.AddMessage("You don't seem to have those items anymore.", Color.Crimson);
                            });

                            mini.Con.PrintClickable(35, 35, new ColoredString("?", Color.MediumPurple, Color.Black), () => { 
                                Log.AddMessage("Last recipe: " + (rec2.FirstQty > 1 ? rec2.FirstQty + "x " : "") + ResolveItemName(rec2.FirstItem) + " + " + (rec2.SecondQty > 1 ? rec2.SecondQty + "x " : "") + ResolveItemName(rec2.SecondItem) + " = " + (rec2.OutputQty > 1 ? rec2.OutputQty + "x " : "") + ResolveItemName(rec2.OutputItem));    
                            });

                            mini.Con.PrintClickable(37, 35, "First Found Recipe", () => { 
                                if (!ItemUseLogic.TryCombineItems(player, rec2.FirstItem, rec2.SecondItem)) {
                                    GameLoop.ZPO.Log.AddMessage("You don't seem to have those items anymore.", Color.Crimson);
                                }    
                            });
                    } else {
                        mini.Con.Print(40, 35, "(recipe broken)", Color.DarkSlateGray);
                    }
                } else {
                    mini.Con.Print(37, 35, "(no recipes found)", Color.DarkSlateGray);
                }
            }

            mini.Win.PrintClickable(85, 49, ("[" + GameLoop.SoundManager.CurrentSong + "]").Align(HorizontalAlignment.Right, 30, (char) 196), () => {
                player.MusicMode = "Shuffle UPC";
                GameLoop.SoundManager.CurrentSong = "";
                GameLoop.SoundManager.music.StopAllSounds();
                GameLoop.SoundManager.PickMusic();
            });

            mini.Win.Print(120, 49, "[VOLUME:         ]");
            mini.Win.PrintClickable(129, 49, new ColoredString("-", Color.White, Color.Transparent), () => {
                double qty = 0.01;
                if (Helper.EitherShift()) { qty *= 5.0; }
                if (Helper.EitherControl()) { qty *= 10.0; }
                
                GameLoop.GlobalOptions.MusicVolume = Math.Clamp(GameLoop.GlobalOptions.MusicVolume - qty, 0, GameLoop.GlobalOptions.MusicVolume);
                GameLoop.SaveGlobalPrefs();
            });

            mini.Win.Print(131, 49, $"{GameLoop.GlobalOptions.MusicVolume:0.00}".ToString());

            mini.Win.PrintClickable(136, 49, new ColoredString("+", Color.White, Color.Transparent), () => {
                double qty = 0.01;
                if (Helper.EitherShift()) { qty *= 5.0; }
                if (Helper.EitherControl()) { qty *= 10.0; }
                
                GameLoop.GlobalOptions.MusicVolume = Math.Clamp(GameLoop.GlobalOptions.MusicVolume + qty, GameLoop.GlobalOptions.MusicVolume, 1.0);
                GameLoop.SaveGlobalPrefs();
            });


            mini.Win.PrintClickable(143, 49, "[SAVE]", () => { ManualSave(); });
        }

        public void LogDraw(UI_EmbeddedMini mini) {
            Point mousePos = new MouseScreenObjectState(mini.Con, GameHost.Instance.Mouse).CellPosition; 
            mini.Con.DrawLine(new Point(0, 35), new Point(148, 35), 196); 

            int printY = 47;
            for (int i = Log.TopIndex; i >= 0 && printY > 34; i--) {
                if (Log.Log[i].Message.Length >= 148) {
                    printY -= (Log.Log[i].Message.Length / 148);

                    if (printY <= 34)
                        break;
                } 

                if (Log.Log[i].Count == 1)
                    mini.Con.PrintMultiLine(0, printY, Log.Log[i].Message, 148);
                else
                    mini.Con.PrintMultiLine(0, printY, Log.Log[i].Message + " (x" + Log.Log[i].Count.ToString() + ")", 148);
                  
                printY -= 1; 
            } 
            
            mini.Con.DrawLine(new Point(0, 35), new Point(148, 35), 196, Color.White);

            int qty = 1;
            if (Helper.EitherShift())
                qty *= 5;
            if (Helper.EitherControl())
                qty *= 10;

            if (mousePos.Y > 34 && !ExtraWindows.AnyVisible()) {
                if (Helper.ScrolledDown()) { Log.TopIndex = Math.Clamp(Log.TopIndex + qty, 0, Log.Log.Count - 1); }
                if (Helper.ScrolledUp()) { Log.TopIndex = Math.Clamp(Log.TopIndex - qty, 0, Log.Log.Count - 1); }
            }
        }


        public void Update(UI_EmbeddedMini mini) {
            //Point mousePos = new MouseScreenObjectState(mini.Con, GameHost.Instance.Mouse).CellPosition;  

            mini.Con.Clear();
            mini.SingleSquare.Clear();
            mini.DoubleSquare.Clear();
            mini.QuadSquare.Clear();

            mini.Win.Clear();
            Helper.DrawBox(mini.Win, 0, 0, 148, 48);

              
            SidebarManager.Draw(mini, player); 
            LogDraw(mini);
            LocationDraw(mini);  

            if (ExtraWindows.CollectionLog.IsVisible)
                ExtraWindows.CollectionLogDraw();

            if (ExtraWindows.Guide.IsVisible)
                ExtraWindows.GuideDraw();
             
            if (ExtraWindows.CraftingMenu.IsVisible)
                ExtraWindows.CraftingMenuDraw();

            if (ExtraWindows.Quests.IsVisible)
                ExtraWindows.QuestDraw();
            
            if (ExtraWindows.Compendium.IsVisible)
                ExtraWindows.CompendiumDraw();

            if (ExtraWindows.Map.IsVisible)
                ExtraWindows.MapDraw();

            if (ExtraWindows.Debug.IsVisible)
                ExtraWindows.DebugDraw();

            if (ExtraWindows.Clue.IsVisible)
                ExtraWindows.ClueDraw();

            if (ExtraWindows.Teleport.IsVisible)
                ExtraWindows.TeleportDraw();

            if (ExtraWindows.InventoryContainer.IsVisible)
                ExtraWindows.InventoryContainerDraw();

            if (ExtraWindows.Book.IsVisible)
                ExtraWindows.BookDraw();

            if (ExtraWindows.Cutscene.IsVisible)
                ExtraWindows.CutsceneDraw();

            if (ExtraWindows.Shop.IsVisible)
                ExtraWindows.ShopDraw();

            if (ExtraWindows.LampMenu.IsVisible)
                ExtraWindows.LampDraw();

            if (ExtraWindows.GrandExchange.IsVisible)
                ExtraWindows.GrandExchangeDraw();

            if (ExtraWindows.CatMenu.IsVisible)
                ExtraWindows.CatDraw();

            if (ExtraWindows.HouseMenu.IsVisible)
                ExtraWindows.HouseDraw();

            if (ExtraWindows.Compass.IsVisible)
                ExtraWindows.CompassDraw();

            if (TimeLastTicked + 1000 < Helper.Time()) {
                TickTime();
            } 
        }

        List<string> activityTabs = new() { "Items", "NPCs", "Processing", "Resources", "Chat", "Shop", "Farming", "Hunter", "Minigame" };

        public void Input(UI_EmbeddedMini mini) {
            Point mousePos = new MouseScreenObjectState(mini.Con, GameHost.Instance.Mouse).CellPosition;
            if (Helper.HotkeyDown(Key.Escape)) {
                if (ExtraWindows.AnyVisible()) {
                    ExtraWindows.HideAll();
                    return;
                }

                if (Helper.EitherShift()) {
                    Close(mini);
                } else {
                    Log.AddMessage("To return to the menu, hold either SHIFT and press ESC again.", Color.Crimson);
                }
            }

            if (Helper.HotkeyDown(Key.Tab)) {
                for (int i = 0; i < activityTabs.Count; i++) {
                    if (activityTabs[i] == SelectedMenu) {
                        if (Helper.EitherShift()) {
                            if (i == 0) {
                                SelectedMenu = activityTabs[activityTabs.Count - 1];
                            }
                            else {
                                SelectedMenu = activityTabs[i - 1];
                            }
                        } else {
                            if (i == activityTabs.Count - 1) {
                                SelectedMenu = activityTabs[0];
                            }
                            else {
                                SelectedMenu = activityTabs[i + 1];
                            }
                        }

                        break;
                    }
                }
            }

            if (Helper.HotkeyDown(Key.C) && !ExtraWindows.AnyVisible("Collection") && !TypingAnywhere()) {
                if (ExtraWindows.CollectionLog.IsVisible) { 
                    ExtraWindows.CollectionLog.IsVisible = false; 
                } else {
                    ExtraWindows.HideAll();
                    ExtraWindows.CollectionLog.IsVisible = true; 
                }
            }

            if (Helper.HotkeyDown(Key.F1) && !ExtraWindows.AnyVisible("Guidebook")) {
                if (ExtraWindows.Guide.IsVisible) { 
                    ExtraWindows.Guide.IsVisible = false; 
                } else {
                    ExtraWindows.HideAll();
                    ExtraWindows.Guide.IsVisible = true; 
                }
            }

            if (Helper.HotkeyDown(Key.F2) && !ExtraWindows.AnyVisible("Compendium")) {
                if (ExtraWindows.Compendium.IsVisible) { 
                    ExtraWindows.Compendium.IsVisible = false; 
                } else {
                    ExtraWindows.HideAll();
                    ExtraWindows.Compendium.IsVisible = true; 
                }
            }
            
            if (Helper.HotkeyDown(Key.F12) && !ExtraWindows.AnyVisible("Debug")) {
                if (ExtraWindows.Debug.IsVisible) { 
                    ExtraWindows.Debug.IsVisible = false; 
                } else {
                    ExtraWindows.HideAll();
                    ExtraWindows.Debug.IsVisible = true; 
                }
            }

            if (Helper.HotkeyDown(Key.F5) && !ExtraWindows.AnyVisible("")) {
                ManualSave();
            }

            if (Helper.HotkeyDown(Key.Q) && !ExtraWindows.AnyVisible("Quests") && !TypingAnywhere()) {
                if (ExtraWindows.Quests.IsVisible) { 
                    ExtraWindows.Quests.IsVisible = false; 
                } else {
                    ExtraWindows.HideAll();
                    ExtraWindows.Quests.IsVisible = true; 
                } 
            } 


            if (!ExtraWindows.AnyVisible() && !TypingAnywhere()) {
                if (Atlas.TryGetValue(player.NavLoc, out Location? curr) && curr != null) { 
                    List<Connection> conns = curr.ConnectedLocations.Where(o => (o.CanTraverse(player) && o.HideIfReqsNotMet) || !o.HideIfReqsNotMet).ToList();

                    if (Helper.HotkeyDown(Key.NumPad1) && conns.Count > 0 && conns[0].CanTraverse(player)) { conns[0].Traverse(player); } 
                    if (Helper.HotkeyDown(Key.NumPad2) && conns.Count > 1 && conns[1].CanTraverse(player)) { conns[1].Traverse(player); } 
                    if (Helper.HotkeyDown(Key.NumPad3) && conns.Count > 2 && conns[2].CanTraverse(player)) { conns[2].Traverse(player); } 
                    if (Helper.HotkeyDown(Key.NumPad4) && conns.Count > 3 && conns[3].CanTraverse(player)) { conns[3].Traverse(player); } 
                    if (Helper.HotkeyDown(Key.NumPad5) && conns.Count > 4 && conns[4].CanTraverse(player)) { conns[4].Traverse(player); } 
                    if (Helper.HotkeyDown(Key.NumPad6) && conns.Count > 5 && conns[5].CanTraverse(player)) { conns[5].Traverse(player); } 
                    if (Helper.HotkeyDown(Key.NumPad7) && conns.Count > 6 && conns[6].CanTraverse(player)) { conns[6].Traverse(player); } 
                    if (Helper.HotkeyDown(Key.NumPad8) && conns.Count > 7 && conns[7].CanTraverse(player)) { conns[7].Traverse(player); } 
                    if (Helper.HotkeyDown(Key.NumPad9) && conns.Count > 8 && conns[8].CanTraverse(player)) { conns[8].Traverse(player); } 
                }
            }


            if (GameHost.Instance.Mouse.RightClicked) {
                // Leaving this here just in case

                //player.GiveGold(10000);

                //Item knives = new Item(ItemLibrary["knivesBronze"]);
                //knives.Quantity = 500;
                //player.TryPickup(new Item(ItemLibrary["potionRestore"]), 1);
                //player.TryPickup(new Item(ItemLibrary["fleshRotten"]), 5);
                //player.TryPickup(new Item(ItemLibrary["beadYellow"]), 1);
                //player.TryPickup(new Item(ItemLibrary["beadBlack"]), 1);
                //player.TryPickup(new Item(ItemLibrary["hatchetBronze"]), 1);
                 //player.TryPickup(new Item(ItemLibrary["staffAir"]), 1);
                 //player.TryPickup(new Item(ItemLibrary["pouchRune"]), 1);                       
                
               // player.TryPickup(new Item(ItemLibrary["wizardBlueRobeG"]), 1); 

               //player.TryPickup(new Item(ItemLibrary["tabstrikeWind"]) { Quantity = 5}, 5);   
                

              //  player.CurrentClueBeginner = "B_CharlieMining";
                //player.TryPickup(new Item(ItemLibrary["vialWater"]), 1);  
                //player.TryPickup(new Item(ItemLibrary["cadava"]), 1);  
                

                //player.NavLoc = "MIST_VarrockPalaceBailey";
                //player.TryPickup(new Item(ItemLibrary["tabteleHome"]), 1);
                //player.TryGrantExp("Magic", 50000, Log, SidebarManager.RecentlyTrainedSkills);



               // player.TryPickup(new Item(ItemLibrary["tiaraWater"]), 1);   
               //   player.TryPickup(new Item(ItemLibrary["logPine"]), 1);   
               // player.TryGrantExp("Runecrafting", 10000, Log, SidebarManager.RecentlyTrainedSkills);

                //player.TryGrantExp("Agility", 1000, Log, SidebarManager.RecentlyTrainedSkills);

                /*if (ItemLibrary.TryGetValue("casketEasy", out Item? cask) && cask != null) {
                    int totalCycles = 0;
                    double simCount = 100;
                    for (int i = 0; i < simCount; i++) {
                        totalCycles += Helper.SimulateDropTable(cask.DropTable, 5, "", true);
                    }

                    Log.AddMessage(cask.Name + " log completed in average " + (totalCycles / simCount) + " opens over " + simCount + " simulations.");
                }*/ 

                ItemUseLogic.UsingSlot = -1;
            }
        }

        public void Close(UI_EmbeddedMini mini) {
            ManualSave(false);
            Reset();
            mini.Toggle();
        }

        public void Reset() {
            HardResetPlayer();
        }


        public void ManualSave(bool announce = true) {
            if (!Directory.Exists("./saves/")) {
                Directory.CreateDirectory("./saves/");
            }

            Helper.SerializeToFile(player, "./saves/" + player.Name + ".json"); 
            SecondsSinceAutosave = 0;

            if (announce)
                Log.AddMessage("Player data saved!");
        }

        public void TickTime() {
            TimeLastTicked = Helper.Time();

            SecondsSinceAutosave++;

            player.SecondsPlayed++;

            if (SecondsSinceAutosave >= 500) { 
                ManualSave();

                SecondsSinceAutosave = 0;

                Log.AddMessage("Player autosave complete.");
            }

            foreach (var kv in player.FarmingPatches) {
                if (kv.Value.SeedPlanted != "") { 
                    if (kv.Value.TimeLeft > 0) {
                        kv.Value.TimeLeft -= player.FarmGrowthIncrement;
                    }

                    if (kv.Value.RegrowTimeLeft > 0 && kv.Value.Regrown < 5) {
                        kv.Value.RegrowTimeLeft -= player.FarmGrowthIncrement;

                        if (kv.Value.RegrowTimeLeft <= 0) {
                            kv.Value.Regrown++;
                            kv.Value.RegrowTimeLeft = kv.Value.RegrowTime;
                        }
                    }
                }
            }



            for (int i = player.ActivePotions.Count - 1; i >= 0; i--) {
                if (player.ActivePotions[i].Stat != "Well Rested") {
                    player.ActivePotions[i].SecondsSinceWeaken++;

                    if (player.ActivePotions[i].SecondsSinceWeaken >= 60) {
                        player.ActivePotions[i].SecondsSinceWeaken = 0;

                        if (player.ActivePotions[i].Change > 0)
                            player.ActivePotions[i].Change--;
                        else if (player.ActivePotions[i].Change < 0)
                            player.ActivePotions[i].Change++;

                        if (player.ActivePotions[i].Change == 0) {
                            player.ActivePotions.RemoveAt(i);
                        }
                    }
                }
            }

            if (player.TotalActivePrayers() > player.GetEffectiveSkillLevel("Prayer")) {
                List<Prayer> Active = player.Prayers.Values.Where(o => o.Active).OrderBy(o => o.Level).ToList();
                
                if (Active.Count > 0) {
                    Active[0].Active = false;
                }
            }

            if (player.Equipment.TryGetValue("Pet", out ItemWrapper? petWrap) && petWrap.GetRef() is Item pet) {
                if (GameLoop.rand.Next(100) == 0 && pet.PetBlurbs != null && pet.PetBlurbs.Count > 0) {
                    Log.AddMessage(pet.PetBlurbs[GameLoop.rand.Next(pet.PetBlurbs.Count)], pet.GetColor());
                }

                List<string> kittenColors = [ "kittenBlack", "kittenGray", "kittenWhite", "kittenOrange", "kittenBrown", "kittenCalico" ];
                List<string> catColors = [ "catBlack", "catGray", "catWhite", "catOrange", "catBrown", "catCalico" ];
                if (kittenColors.Contains(petWrap.ID)) {
                    petWrap.MiscInt++; // Time since last fed
                    if (petWrap.MiscInt == 1440) { // 24 minutes without being fed
                        Log.AddMessage("Your kitten meows loudly. Seems like it might be hungry.", Color.Orange);
                    } else if (petWrap.MiscInt == 1620) { // 27 minutes without being fed
                        Log.AddMessage("Your kitten meows loudly. Seems like it might be VERY hungry.", Color.Crimson);
                    } else if (petWrap.MiscInt == 1800) { // 30 minutes without being fed, run away
                        Log.AddMessage("Your kitten gets tired of starving and runs away.", Color.Crimson);
                        player.Equipment.Remove("Pet");
                    }

                    petWrap.MiscInt2++; // Time since last played
                    if (petWrap.MiscInt2 == 1440) { // 24 minutes without attention
                        Log.AddMessage("Your kitten meows loudly. Seems like it wants attention.", Color.Orange);
                    } else if (petWrap.MiscInt2 == 1620) { // 27 minutes without attention
                        Log.AddMessage("Your kitten meows loudly. Seems like it NEEDS attention.", Color.Crimson);
                    } else if (petWrap.MiscInt2 == 1800) { // 30 minutes without attention, run away
                        Log.AddMessage("Your kitten gets tired of being ignored and runs away.", Color.Crimson);
                        player.Equipment.Remove("Pet");
                    }

                    petWrap.MiscInt3++; // Time spent on ground, aka age
                    if (petWrap.MiscInt3 >= 10800) { // 3 hours aging
                        petWrap.ID = petWrap.ID.Replace("kitten", "cat"); 
                        Log.AddMessage("Your kitten has grown up into a cat!", Color.Lime);
                    }
                } else if (catColors.Contains(petWrap.ID)) {
                    petWrap.MiscInt3++; // Time spent on ground, aka age
                    if (petWrap.MiscInt3 >= 28800) { // A further 6 hours aging, 9 hours total
                        petWrap.ID += "Overgrown";
                        Log.AddMessage("Your cat has become overgrown.", Color.Green);
                    }
                }
            }


            if (player.Inventory.Count > 1) { 
                bool found = false;
                foreach (var kv in UseRecipes) {
                    if (player.HasAllItems(new() { kv.Value.FirstItem + "," + Math.Max(1, kv.Value.FirstQty), kv.Value.SecondItem + "," + Math.Max(1, kv.Value.SecondQty)})) {
                        if (kv.Value.NeededTool == "" || player.HasAllItems([kv.Value.NeededTool + ",1"])) {
                            SidebarManager.LastFoundRecipe = new(kv.Key.first, kv.Key.second); 
                            found = true;
                            break;
                        }
                    }
                }

                if (!found)
                    SidebarManager.LastFoundRecipe = null;
            }

            if (player.QuestLog.TryGetValue("MI_ShieldOfArrav", out QuestStatus? arrav) && arrav.CurrentStage != 50 && arrav.CurrentStage != -1) {
                if (arrav.CurrentStage < 20 && !player.HasAllItems(["arravShieldLeft,1", "arravShieldRight,1"]) && (player.HasAllItems(["arravShieldLeft,1"]) || player.HasAllItems(["arravShieldRight,1"]))) {
                    player.TryProgressQuest("MI_ShieldOfArrav", 20);
                } 
                if (arrav.CurrentStage < 30 && player.HasAllItems(["arravShieldLeft,1", "arravShieldRight,1"])) { player.TryProgressQuest("MI_ShieldOfArrav", 30); } 
                if (arrav.CurrentStage < 40 && player.HasAllItems(["arravShield,1"])) { player.TryProgressQuest("MI_ShieldOfArrav", 40); }
            }

            if (player.ActiveGE.Count > 0) {
                foreach (var ge in player.ActiveGE) {
                    if (!ge.Resolved) {
                        if (ge.PerSecondDenom() > 0) {
                            if (GameLoop.rand.Next(ge.PerSecondDenom()) == 0) {
                                ge.Resolved = true;
                                if (ge.Buying) {
                                    Log.AddMessage("Grand Exchange: Bought " + ge.Wrap.Quantity + "x " + ResolveItemName(ge.Wrap.ID) + " for " + (ge.Wrap.Quantity * ge.ListPrice) + "gp.", Color.Lime);
                                } else {
                                    Log.AddMessage("Grand Exchange: Sold " + ge.Wrap.Quantity + "x " + ResolveItemName(ge.Wrap.ID) + " for " + (ge.Wrap.Quantity * ge.ListPrice) + "gp.", Color.Lime);
                                }
                            }
                        }
                    }
                }
            }

            if (player.MahoganyDestID != "" ) {
                if (Atlas.TryGetValue(player.MahoganyDestID, out Location? dest) && dest != null) {
                    if (Atlas.TryGetValue(player.NavLoc, out Location? curr) && curr != null) {
                        if (dest.MahoganyTown == "Tutorial Island" && curr.Region != "Tutorial Island") { // If they had a contract active when they left tutorial island, clear it
                            player.MahoganyDestID = "";
                            player.MahoganyContractSpots.Clear();
                        }

                        if (player.MahoganyContractSpots.Count > 0 && player.MahoganyContractSpots.Where(o => !o.Repaired).ToList().Count == 0) { // All spots are repaired, resolve contract
                            double avg = player.MahoganyConstructionTotal / 5.0; // get average level spent on each spot, in case the player didn't do the same level across the board for some reason
                            avg = avg / 10.0; // turn it into a tier;
                            avg = Math.Floor(avg) + 1; // floor it and add 1. Level 1 at this point is now worth 1, Level 10 worth 2, up to Level 90 materials worth 10.

                            player.TryGrantExp("Construction", (int) avg * 450, Log, SidebarManager.RecentlyTrainedSkills); // Grant 450 exp per tier of resource spent
                            player.MahoganyDestID = ""; // Clear the destination ID
                            player.MahoganyContractSpots.Clear(); // Clear their hotspot list so a new contract can be taken

                            player.MahoganyPoints += ((int) avg + 1); // Gain reward points equal to tier plus 1.

                            Log.AddMessage("You finished your Mahogany Homes contract, earning " + ((int) avg * 450) + " Construction experience and " + (int) avg + " reward points.", Color.Green);
                        }
                    }
                } 
            }

            if (player.CurrentRestedBuff != 0 && player.TimeLastRested + 1800 < Helper.Time()) { // Rested wears off after 30 minutes
                player.CurrentRestedBuff = 0;
                
                for (int i = player.ActivePotions.Count - 1; i >= 0; i--) {
                    if (player.ActivePotions[i].Stat == "Well Rested") {
                        player.ActivePotions.RemoveAt(i);
                    }
                }

                Log.AddMessage("You no longer feel quite as well rested.", Color.Crimson);
            }

            if (player.TimeLastStar == 0 || player.TimeLastStar + 5400 < player.SecondsPlayed) { // At the start and every 90 minutes played, crumble the old star (if it exists) and drop a new one.
                if (player.CurrentStarLoc != "") { // Star already exists, check if a notif is needed and then wipe it
                    if (player.CurrentStarLoc == player.NavLoc) {
                        Log.AddMessage("The crashed star crumbles away to dust, leaving nothing behind.", Color.Turquoise);
                        player.CurrentStarLoc = ""; 
                    }
                }

                List<string> PossibleLandingSites = [ // TODO: Add more shooting star lands as the regions are added
                    "DES_AlKharidMine",
                    "DES_AlKharidBank",
                    //"DES_UzerMine",
                    //"DES_DesertQuarry",
                    //"DES_AgilityPyramidMine",
                    //"DES_Nardah", 
                    "DES_AlKharidMineOutside",

                    "MIST_LumbridgeSwamp23",
                    "MIST_LumbridgeSwamp18",
                    "MIST_DraynorOutskirtsSouth",
                    "MIST_VarrockSouthEast",
                    "MIST_VarrockMineEast",
                    "MIST_VarrockMineWest"
                ];

                player.CurrentStarLoc = PossibleLandingSites[GameLoop.rand.Next(PossibleLandingSites.Count)];
                player.CurrentStarTier = 0;
                player.TimeLastStar = player.SecondsPlayed;

                if (player.CurrentStarLoc == player.NavLoc) {
                    Log.AddMessage("You hear a dull roar from above, then a small explosion as a shooting star crashes into the ground nearby.", Color.Turquoise);
                }
            }

            if (player.CurrentStarLoc != "") {
                int secondsToNext = (player.TimeLastStar + 4200) - player.SecondsPlayed;

                int oldTier = player.CurrentStarTier;
                player.CurrentStarTier = 10 - (int) Math.Round(secondsToNext / 420.0); // This will hopefully result in tier 0 when it falls, down to tier 10 at less than 6 minutes left

                if (oldTier != player.CurrentStarTier && player.CurrentStarLoc == player.NavLoc) {
                    if (oldTier <= 10) {
                        Log.AddMessage("The outermost layer of the crashed star crumbles away, exposing a harder layer.", Color.Yellow);
                    } else { 
                        Log.AddMessage("The crashed star crumbles away to dust, leaving nothing behind.", Color.Turquoise);
                        player.CurrentStarLoc = "";  
                    }
                }
            }

            if (player.CurrentStarLoc != "" && player.CurrentStarLoc == player.NavLoc) {
                if (player.MiningStar) {
                    int currLevel = (player.CurrentStarTier == 0 ? 1 : player.CurrentStarTier * 10);

                    bool hasPick = false;
                    foreach (var kv in player.Equipment) {
                        if (kv.Value.GetRef() is Item eqp && eqp.MiscString == "Pickaxe") {
                            if (eqp.EquipLevel <= player.GetEffectiveSkillLevel("Mining")) { 
                                hasPick = true;
                            }
                        }
                    }

                    for (int i = 0; i < player.Inventory.Count; i++) {
                        if (player.Inventory[i].GetRef() is Item inv && inv.MiscString == "Pickaxe") {
                            if (inv.EquipLevel <= player.GetEffectiveSkillLevel("Mining")) {
                                hasPick = true;
                            }
                        }
                    } 

                    if (!hasPick) {
                        Log.AddMessage("You need a pickaxe to keep mining the crashed star.", Color.Crimson);
                        player.MiningStar = false;
                    }

                    if (player.GetEffectiveSkillLevel("Mining") < currLevel) { 
                        Log.AddMessage("You need level " + currLevel + " Mining to keep mining the crashed star.", Color.Crimson);
                        player.MiningStar = false;
                    }

                    if (player.MiningStar) {
                        if (GameLoop.rand.Next(100) < 30 + (player.GetEffectiveSkillLevel("Mining") / 6)) {
                            player.TryGrantExp("Mining", 32, Log, SidebarManager.RecentlyTrainedSkills);
                            Log.AddMessage("You get a bit of stardust from the crashed star.", Color.Crimson);
                            
                            if (ResolveItem("stardust") is Item stardust) {
                                player.TryPickup(stardust, 1);
                            }
                        }
                    }
                }
            } else {
                player.MiningStar = false;
            }
        }

        public void HardResetPlayer() {
            player = new();
            TryAddSkills();
            TryAddPrayers(); 
            player.CurrentHP = 10;

            TrySetupLogs();

            RebuildLibraries();

            Log.Log.Clear();
            Log.AddMessage(new ColoredString("Press F1 at any time to open/close the guidebook.", Color.Turquoise, Color.Black));
        }

        public void SoftResetPlayer() {
            player.Inventory.Clear();
            player.Equipment.Clear();

            player.Skills.Clear();
            TryAddSkills();
            player.Prayers.Clear();
            TryAddPrayers(); 
            player.CurrentHP = 10;


            player.CollectionLog.Clear();
            player.CollectionLogClues.Clear();
            player.CollectionLogBoss.Clear();
            player.BankedItems.Clear();
            player.ItemsEverObtained.Clear();
            player.ActivePotions.Clear();

            player.QuestLog.Clear();
            TryAddQuests();
            
            foreach (var patch in player.FarmingPatches) {
                patch.Value.ClearPatch();
            }

            foreach (var prayer in player.Prayers) {
                prayer.Value.Active = false;
            }
             
            Log.AddMessage(new ColoredString("Character soft-reset complete.", Color.Turquoise, Color.Black));
        }

        public void RemapItems(bool justApply = false) {
            if (!justApply) {
                List<string> itemIDs = ItemLibrary.Keys.ToList();
                int mapTo = 0;

                foreach (var kv in ItemLibrary) {
                    mapTo = GameLoop.rand.Next(itemIDs.Count);
                    player.ItemIDRemaps.Add(kv.Key, itemIDs[mapTo]);
                    itemIDs.RemoveAt(mapTo);
                }
            }

            Dictionary<string, Item> cloneLib = ItemLibrary.Clone();

            ItemLibrary.Clear();

            foreach (var kv in player.ItemIDRemaps) {
                ItemLibrary.Add(kv.Value, cloneLib[kv.Key]);
            }
        } 

        public void RebuildLibraries() {
            ItemLibrary.Clear();
            GatherSpots.Clear();
            ProcessingStations.Clear();
            UseRecipes.Clear();
            MonsterLibrary.Clear();
            Atlas.Clear();
            NPCLibrary.Clear();
            PrayerLibrary.Clear();
            player.FarmingPatches.Clear();
            ClueStepLibrary.Clear();
            CraftLib.Clear();
            QuestLibrary.Clear();
            BossLibrary.Clear();
            HunterLibrary.Clear();
            BookLibrary.Clear();
            CutsceneLibrary.Clear();
            M_HotspotLib.Clear();
            POH_ObjectLib.Clear();
             
            SpellLibrary.Clear();
            HardcodedSpells.InitSpells(SpellLibrary);

            HardcodedItems.InitItems(ItemLibrary, SpellLibrary);
            HardcodedItemsCooking.InitItems(ItemLibrary);
            HardcodedItemsClues.InitItems(ItemLibrary);
            HardcodedItemsFarming.InitItems(ItemLibrary);
            HardcodedItemsCrafting.InitItems(ItemLibrary);
            HardcodedItemsQuests.InitItems(ItemLibrary);
            HardcodedItemsSmithing.InitItems(ItemLibrary);
            HardcodedItemsMinigames.InitItems(ItemLibrary);
            HardcodedItemsHoliday.InitItems(ItemLibrary);
            HardcodedItemsPets.InitItems(ItemLibrary);


            HardcodedGathering.InitGathers(GatherSpots);
            HardcodedProcessing.InitProcessors(ProcessingStations);
            HardcodedUseRecipes.InitUseRecipes(UseRecipes);
            HardcodedMonsters.InitMonsters(MonsterLibrary);

            HardcodedLocations.InitLocs(Atlas, GatherSpots, MonsterLibrary);
            HardcodedLocationsTutorial.InitLocs(Atlas, GatherSpots, MonsterLibrary);
            LocationsDesert.InitLocs(Atlas, GatherSpots, MonsterLibrary);
            LocationsVarrock.InitLocs(Atlas, GatherSpots, MonsterLibrary);
            LocationsLumbridge.InitLocs(Atlas, GatherSpots, MonsterLibrary);
            LocationsDraynor.InitLocs(Atlas, GatherSpots, MonsterLibrary);

            HardcodedNPCs.InitNPCs(NPCLibrary);
            HardcodedNPCsLumbridgeDraynor.InitNPCs(NPCLibrary);
            HardcodedNPCsVarrock.InitNPCs(NPCLibrary);
            HardcodedNPCsAlKharid.InitNPCs(NPCLibrary);
            HardcodedNPCsTutorial.InitNPCs(NPCLibrary);

            HardcodedPrayers.InitPrayers(PrayerLibrary);
            HardcodedFarmPatches.InitPatches(player.FarmingPatches);
            HardcodedClueSteps.InitClues(ClueStepLibrary);
            HardcodedCraftRecipes.InitCrafts(CraftLib, SpellLibrary);
            HardcodedQuests.InitQuests(QuestLibrary);
            HardcodedBosses.InitBosses(BossLibrary);
            HardcodedHunter.InitHunter(HunterLibrary);
            HardcodedBooks.InitBooks(BookLibrary);
            HardcodedCutscenes.InitCutscenes(CutsceneLibrary);

            HardcodedMahoganyHotspots.InitSpots(M_HotspotLib);
            HardcodedPOHObjects.InitPOHObjects(POH_ObjectLib, SpellLibrary, Atlas);
        }

        public Item? ResolveItem(string ID) {
            if (player.RandomItems == 0) {
                if (ItemLibrary.ContainsKey(ID)) {
                    return new Item(ItemLibrary[ID]);
                }
            } else {
                if (player.ItemIDRemaps.ContainsKey(ID)) {
                    if (ItemLibrary.ContainsKey(player.ItemIDRemaps[ID])) {
                        return new Item(ItemLibrary[player.ItemIDRemaps[ID]]);
                    }
                }
            } 

            return null;
        }

        public string ResolveItemName(string ID) {
            if (ResolveItem(ID) is Item item && item != null) {
                return item.GetName(1, 0, item.UseInt4);
            }

            return ID;
        }

        public string ResolveMonsterName(string ID) {
            if (MonsterLibrary.TryGetValue(ID, out AreaMonster? mon) && mon != null) {
                return mon.Name;
            }

            return ID;
        }

        public string ResolveNPCName(string ID) {
            if (NPCLibrary.TryGetValue(ID, out NPC? npc) && npc != null) {
                return npc.Name;
            }

            return ID;
        }

        public string ResolveLocationName(string ID) {
            if (Atlas.TryGetValue(ID, out Location? loc) && loc != null) {
                return loc.DisplayName;
            }

            return ID;
        }

        public string ResolveGatherName(string ID) {
            if (GatherSpots.TryGetValue(ID, out GatheringTile? gather) && gather != null) {
                return gather.Name;
            }

            return ID;
        }

        public string ResolveBossName(string ID) {
            if (BossLibrary.TryGetValue(ID, out BossFight? mon) && mon != null) {
                return mon.Name;
            }

            return ID;
        }

        public void TryPlaceItem(string loc, ItemWrapper wrap) {
            if (Atlas.TryGetValue(loc, out Location? curr)) {
                if (wrap.GetRef() is Item item) {
                    bool found = false;
                    for (int i = 0; i < curr.ItemsHere.Count; i++) {
                        if (curr.ItemsHere[i].ID == wrap.ID && wrap.Noted == curr.ItemsHere[i].Noted && curr.ItemsHere[i].Inaccessible == wrap.Inaccessible) {
                            curr.ItemsHere[i].Quantity += wrap.Quantity;
                            found = true;
                            break;
                        }
                    } 

                    if (!found) {
                        item.Inaccessible = wrap.Inaccessible;

                        foreach (var con in wrap.Containing) {
                            item.Containing.Add(new(con));
                        }

                        item.Quantity = wrap.Quantity;
                        
                        if (wrap.Charges != 0) {
                            item.UseInt4 = wrap.Charges;
                        }

                        item.Noted = wrap.Noted;

                        curr.ItemsHere.Add(item);
                    }
                }
            }
        }

        public void TryAddQuests() {
            foreach (var kv in QuestLibrary) {
                player.QuestLog.TryAdd(kv.Key, new(kv.Key, -1));
            }
        }

         public void TryAddSkills() {
            player.Skills.TryAdd("Woodcutting", new Skill("Woodcutting"));
            player.Skills.TryAdd("Mining", new Skill("Mining"));
            player.Skills.TryAdd("Smithing", new Skill("Smithing"));
            player.Skills.TryAdd("Thieving", new Skill("Thieving"));
            player.Skills.TryAdd("Cooking", new Skill("Cooking"));
            player.Skills.TryAdd("Fishing", new Skill("Fishing"));
            player.Skills.TryAdd("Runecrafting", new Skill("Runecrafting"));
            player.Skills.TryAdd("Crafting", new Skill("Crafting"));
            player.Skills.TryAdd("Farming", new Skill("Farming"));
            player.Skills.TryAdd("Herblore", new Skill("Herblore"));
            player.Skills.TryAdd("Agility", new Skill("Agility"));
            player.Skills.TryAdd("Firemaking", new Skill("Firemaking"));
            player.Skills.TryAdd("Fletching", new Skill("Fletching"));
            player.Skills.TryAdd("Dungeoneering", new Skill("Dungeoneering"));
            player.Skills.TryAdd("Hunter", new Skill("Hunter"));
            player.Skills.TryAdd("Slayer", new Skill("Slayer"));
            player.Skills.TryAdd("Construction", new Skill("Construction"));

            player.Skills.TryAdd("Constitution", new Skill("Constitution") { Level = 10, Exp = 1155 });
            player.Skills.TryAdd("Attack", new Skill("Attack"));
            player.Skills.TryAdd("Strength", new Skill("Strength"));
            player.Skills.TryAdd("Defense", new Skill("Defense"));
            player.Skills.TryAdd("Prayer", new Skill("Prayer"));
            player.Skills.TryAdd("Ranged", new Skill("Ranged"));
            player.Skills.TryAdd("Magic", new Skill("Magic"));
        }

        public void TryAddPrayers() { 
            foreach (var kv in PrayerLibrary) {
                player.Prayers.TryAdd(kv.Key, kv.Value);
            }
        } 

        public void TrySetupLogs() { 
            player.CollectionLogClues.TryAdd("casketTutorial", new("casketTutorial"));
        }

        public bool TypingAnywhere() {
            if (MinigameManager.POH_Typing != "") {
                return true;
            }

            return false;
        } 
    }
}
