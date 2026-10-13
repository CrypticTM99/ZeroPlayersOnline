using Microsoft.Xna.Framework.Audio;
using ZeroPlayersOnline.HardcodedData;
using ZeroPlayersOnline.Hardcodes;
using ZeroPlayersOnline.Managers;
using ZeroPlayersOnline.UI;

namespace ZeroPlayersOnline.DataTypes {
    public class RunAction {
        public string ActionID = "";
        public string MiscString = "";
        public string MiscString2 = "";
        public int MiscInt = 0;

        public RunAction(string id, string misc, string misc2, int miscint) {
            ActionID = id;
            MiscString = misc;
            MiscString2 = misc2;
            MiscInt = miscint;
        }

        public void Execute() {
            Player player = GameLoop.ZPO.player;
            MessageLog Log = GameLoop.ZPO.Log;

            if (ActionID != "") {
                if (ActionID == "Quest") {
                    player.TryProgressQuest(MiscString, MiscInt);
                } 
                            
                if (ActionID == "Data") {
                    Helper.AlterWorldState(MiscString, MiscString2, MiscInt);
                }

                if (ActionID == "TempSkillBoost") { 
                    player.TryAddPotionEffect(MiscString, MiscInt); 
                }

                if (ActionID == "OpenUI") {
                    if (MiscString == "Shop") {
                        ExtraWindows.Shop.IsVisible = true;
                        ExtraWindows.ShopID = MiscString2;
                        ExtraWindows.BuildShop();
                    } else if (MiscString == "ManagePOH") {
                        ExtraWindows.HouseMenu.IsVisible = true;
                        ExtraWindows.HouseTeleport = false;
                    } else if (MiscString == "TelePOH") {
                        ExtraWindows.HouseMenu.IsVisible = true;
                        ExtraWindows.HouseTeleport = true;
                        ExtraWindows.HouseTablet = false;
                    }
                }

                if (ActionID == "GiveItem") {
                    if (GameLoop.ZPO.ResolveItem(MiscString) is Item given) {
                        player.TryPickup(given, MiscInt);
                    }
                }

                if (ActionID == "EndDialogue") {
                    UI_LocNPCs.ConversationPartner = null;
                    GameLoop.ZPO.SelectedMenu = "NPCs";
                }

                if (ActionID == "UnlockSong") {
                    if (!player.AllSongsUnlocked && !player.UnlockedSongs.Contains(MiscString)) { 
                        player.UnlockedSongs.Add(MiscString); 
                        Log.AddMessage("You have unlocked the song: " + MiscString, Color.Turquoise);
                    }

                    if (player.MusicMode == "Auto" && GameLoop.SoundManager.CurrentSong != MiscString && GameLoop.SoundManager.PlayingSounds.Count == 0) {
                        GameLoop.SoundManager.PickMusic(MiscString);
                    }
                }

                if (ActionID == "CustomLog") {
                    Color col = Color.White;
                    if (MiscInt == 1) { col = Color.Lime; }
                    else if (MiscInt == 2) { col = Color.Crimson; }
                    else if (MiscInt == 3) { col = Color.Turquoise; }
                    else if (MiscInt == 4) { col = Color.Yellow; }
                    Log.AddMessage(MiscString, col);
                }

                if (ActionID == "SlayerTask") {
                    if (MiscString == "Cancel") {
                        player.SlayerTask = "";
                        player.SlayerTaskFrom = "";
                        player.SlayerKillsRemaining = 0;
                        player.SlayerAssignedKills = 0;
                        player.SlayerTaskExtended = false;
                        
                        Log.AddMessage("You have cancelled your current task and may now claim another.", Color.Lime);
                    }

                    if (MiscString == "Block") {
                        int maxBlocked = player.GetQuestPoints() / 50;
                        if (player.SlayerBlocked.Count < maxBlocked) { 
                            if (!player.SlayerBlocked.Contains(player.SlayerTask)) {
                                player.SlayerBlocked.Add(player.SlayerTask);
                                player.SlayerTask = "";
                                player.SlayerTaskFrom = "";
                                player.SlayerKillsRemaining = 0;
                                player.SlayerAssignedKills = 0;
                                player.SlayerTaskExtended = false;
                                Log.AddMessage("You have now blocked that monster from being assigned as a slayer task.", Color.Lime);
                            } else {
                                Log.AddMessage("You have already blocked that monster from being assigned as a slayer task.", Color.Crimson);
                            }
                        } else {
                            Log.AddMessage("You can't block any extra tasks at the moment, you need to unblock some first. Can block " + maxBlocked + ", have blocked " + player.SlayerBlocked.Count + ".", Color.Crimson);
                        }
                    }

                    if (MiscString == "Extend") {
                        int extendCount = (int) Math.Ceiling(player.SlayerAssignedKills * 0.2);
                        player.SlayerKillsRemaining += extendCount;
                        player.SlayerTaskExtended = true;
                        Log.AddMessage("Your task has been extended by " + extendCount + ", leaving you with " + player.SlayerKillsRemaining + " kills left.", Color.Lime);
                    }

                    if (MiscString == "Prefer") {
                        int maxPreferred = player.GetQuestPoints() / 50;
                        if (player.SlayerPrefer.Count < maxPreferred) { 
                            if (!player.SlayerPrefer.Contains(player.SlayerTask)) {
                                player.SlayerPrefer.Add(player.SlayerTask);
                                if (!player.SlayerTaskExtended) {
                                    int extendCount = (int) Math.Ceiling(player.SlayerAssignedKills * 0.2);
                                    player.SlayerKillsRemaining += extendCount;   
                                    player.SlayerTaskExtended = true;

                                    Log.AddMessage("Your task has been preferred and extended by " + extendCount + ", leaving you with " + player.SlayerKillsRemaining + " kills left.", Color.Lime);
                                } else {
                                    Log.AddMessage("Your task has been preferred but had already been extended, so your kills left are unchanged.", Color.Lime);
                                }
                            } else {
                                Log.AddMessage("You have already blocked that monster from being assigned as a slayer task.", Color.Crimson);
                            }
                        } else {
                            Log.AddMessage("You can't block any extra tasks at the moment, you need to unblock some first. Can block " + maxPreferred + ", have preferred " + player.SlayerPrefer.Count + ".", Color.Crimson);
                        }
                    }

                    if (MiscString == "Store") {
                        string swapTask = player.SlayerTask;
                        string swapFrom = player.SlayerTaskFrom;
                        int swapKillsLeft = player.SlayerKillsRemaining;
                        int swapKillsAssigned = player.SlayerAssignedKills;
                        bool swapExtended = player.SlayerTaskExtended;

                        player.SlayerTask = player.StoredTask;
                        player.SlayerTaskFrom = player.StoredTaskFrom;
                        player.SlayerKillsRemaining = player.StoredKillsLeft;
                        player.SlayerAssignedKills = player.StoredKillsAssigned;
                        player.SlayerTaskExtended = player.StoredTaskExtended;

                        player.StoredTask = swapTask;
                        player.StoredTaskFrom = swapFrom;
                        player.StoredKillsLeft = swapKillsLeft;
                        player.StoredKillsAssigned = swapKillsAssigned;
                        player.StoredTaskExtended = swapExtended;

                        if (player.SlayerTask == "") { 
                            Log.AddMessage("You store your slayer task, but did not already have one stored and may take another.", Color.Turquoise);
                        } else { 
                            Log.AddMessage("You store your slayer task, retrieving your previously stored task to kill " + player.SlayerKillsRemaining + " " + GameLoop.ZPO.ResolveMonsterName(player.SlayerTask) + "(s).", Color.Turquoise);
                        }
                    }
                }

                if (ActionID == "ClueHelp") {
                    bool printedAny = false;
                    if (player.CurrentClueBeginner != "") { Log.AddMessage("Beginner: " + ClueLogic.HelpLog("Beginner", player), ColorLib.Mithril); printedAny = true; }
                    if (player.CurrentClueEasy != "") {     Log.AddMessage("    Easy: " + ClueLogic.HelpLog("Easy", player), ColorLib.Mithril); printedAny = true; }
                    if (player.CurrentClueMedium != "") {   Log.AddMessage("  Medium: " + ClueLogic.HelpLog("Medium", player), ColorLib.Mithril); printedAny = true; }
                    if (player.CurrentClueHard != "") {     Log.AddMessage("    Hard: " + ClueLogic.HelpLog("Hard", player), ColorLib.Mithril); printedAny = true; }
                    if (player.CurrentClueElite != "") {    Log.AddMessage("   Elite: " + ClueLogic.HelpLog("Elite", player), ColorLib.Mithril); printedAny = true; }
                    if (player.CurrentClueMaster != "") {   Log.AddMessage("  Master: " + ClueLogic.HelpLog("Master", player), ColorLib.Mithril); printedAny = true; }
                                                        
                    if (!printedAny) {
                        Log.AddMessage("Looks like you haven't got any clue steps active right now!", ColorLib.Mithril);
                    }
                }

                if (ActionID == "HansTime") {
                    int totalSeconds = player.SecondsPlayed;
                    int days = (totalSeconds > 86400 ? totalSeconds / 86400 : 0);
                    totalSeconds -= (days * 86400);
                    int hours = (totalSeconds > 3600 ? totalSeconds / 3600 : 0);
                    totalSeconds -= (hours * 3600);
                    int minutes = (totalSeconds > 60 ? totalSeconds / 60 : 0);
                    totalSeconds -= (minutes * 60);

                    string time = (days > 1 ? days + " days, " : days > 0 ? days + " day, " : "") +
                                    (hours > 1 ? hours + " hours, " : hours > 0 ? hours + " hour, " : "") +
                                    (minutes > 1 ? minutes + " minutes, " : minutes > 0 ? minutes + " minute, " : "") +
                                    (totalSeconds > 1 ? totalSeconds + " seconds" : totalSeconds > 0 ? totalSeconds + " second" : "") + ".";

                    Log.AddMessage("You've played for a total of " + time, ColorLib.Mithril);
                }

                if (ActionID == "Minigame") {
                    if (MiscString == "GetMahoganyContract") {
                        if (player.MahoganyDestID == "") {
                            player.MahoganyContractSpots.Clear();
                            MinigameManager.MH_GetContract();

                            if (!player.HasAllItems(["mahoganyHomesContract"])) {
                                if (GameLoop.ZPO.ResolveItem("mahoganyHomesContract") is Item contract) {
                                    player.TryPickup(contract, 1);
                                }
                            }

                            if (GameLoop.ZPO.Atlas.TryGetValue(player.MahoganyDestID, out Location? mhDest) && mhDest != null) {
                                string build = "";
                                foreach (var kv in player.MahoganyContractSpots) {
                                    if (!kv.Repaired) {
                                        if (build != "") {
                                            build += ", ";
                                        }

                                        build += kv.Name;
                                    }
                                }

                                build += ".";

                                GameLoop.ZPO.Log.AddMessage("Your new contract is " + mhDest.DisplayName + " in " + mhDest.MahoganyTown + ". Remaining: " + build, Color.SandyBrown);
                            }
                        } else {
                            GameLoop.ZPO.Log.AddMessage("You already have a contract and should finish that before getting a new one.", Color.Crimson);
                        }
                    } else if (MiscString == "ViewMahoganyContract") {
                        if (player.MahoganyDestID != "") {
                            if (GameLoop.ZPO.Atlas.TryGetValue(player.MahoganyDestID, out Location? mhDest) && mhDest != null) {
                                string build = "";
                                foreach (var kv in player.MahoganyContractSpots) {
                                    if (!kv.Repaired) {
                                        if (build != "") {
                                            build += ", ";
                                        }

                                        build += kv.Name;
                                    }
                                }

                                build += ".";

                                GameLoop.ZPO.Log.AddMessage("Your current contract is " + mhDest.DisplayName + " in " + mhDest.MahoganyTown + ". Remaining: " + build, Color.SandyBrown);
                            } else { 
                                GameLoop.ZPO.Log.AddMessage("Malformed Mahogany Homes destination ID: " + player.MahoganyDestID + ". Report this, contract cleared for now.", Color.Crimson);
                                GameLoop.ZPO.player.MahoganyDestID = "";
                                GameLoop.ZPO.player.MahoganyContractSpots.Clear();
                            }
                        } else {
                            GameLoop.ZPO.Log.AddMessage("You do not currently have a Mahogany Homes contract.", Color.Crimson);
                        }
                    }
                }

                if (ActionID == "BoughtHouse") {
                    PlayerOwnedHome newHouse = new(MiscString2);
                    newHouse.Rooms.Add(new(HardcodedPOHObjects.RoomTypes[0]));

                    if (player.Homes.Values.Where(o => o.Active).ToList().Count < player.GetSkillTier("Construction")) {
                        newHouse.Active = true;
                    }

                    player.Homes.TryAdd(MiscString, newHouse);
                }

                if (ActionID == "POH") {
                    if (MiscString == "Rest") {
                        if (player.TimeLastRested + 3600 < Helper.Time()) { // Can only rest if it's been at least an hour of playtime since last rest
                            if (GameLoop.ZPO.Atlas.TryGetValue(player.NavLoc, out Location? curr) && curr != null) { 
                                PlayerOwnedHome? home = null;

                                if (curr.PlayerHomeHere != "" && player.Homes.TryGetValue(curr.PlayerHomeHere, out PlayerOwnedHome? tempHome)) {
                                    home = tempHome;
                                }

                                if (home != null) {
                                    int restBuff = Math.Clamp(home.TotalComfort() / 30, 1, 5); 
                                    player.CurrentRestedBuff = restBuff; 
                                    player.TimeLastRested = player.SecondsPlayed;
                                    player.ActivePotions.Add(new("Well Rested", restBuff));  
                                    Log.AddMessage("You take a nap and wake up feeling well rested. (+" + restBuff + ")", Color.Lime);
                                } else {
                                    Log.AddMessage("You don't seem to be in a home at the moment.", Color.Crimson);
                                }
                            }
                        } else {
                            double ms = (player.TimeLastRested + 3600) - Helper.Time();
                            int minutes = (int) (ms / 60);

                            Log.AddMessage("You aren't tired yet. Try again in " + minutes + " minute" + (minutes > 1 ? "s." : "."), Color.Crimson);
                        }
                    }

                    if (MiscString == "Telescope") {
                        if (player.CurrentStarLoc != "") {
                            double secsToNext = (player.TimeLastStar + 5400) - player.SecondsPlayed;
                            int minutes = (int) (secsToNext / 60);

                            Log.AddMessage("The last shooting star landed in " + GameLoop.ZPO.ResolveLocationName(player.CurrentStarLoc) + ". Another will land in around " + minutes + " minute" + (minutes > 1 ? "s." : "."), Color.Yellow);
                        }
                    }

                    if (MiscString == "OfferBones") {  
                        foreach (var inv in player.Inventory) {
                            if (!inv.Noted && inv.GetRef() is Item bone) {
                                if (bone.UseString == "Bones") {
                                    int exp = bone.UseInt;
                                    double multi = MiscInt / 100.0;

                                    if (player.LeftIncense > 0) { multi += 0.5; }
                                    if (player.RightIncense > 0) { multi += 0.5; }

                                    player.TryGrantExp("Prayer", (int) Math.Ceiling(exp * multi), Log, SidebarManager.RecentlyTrainedSkills);

                                    player.ConsumeItems([inv.ID + ",1"]);
                                    break;
                                }
                            }
                        }
                    }

                    if (MiscString == "LightIncense") {
                        if (player.LeftIncense == 0 || player.RightIncense == 0) {
                            if (player.HasAllItems(["tinderbox,1", "herbCleanMarrentill,1"], false, true)) {
                                int fmLv = player.GetEffectiveSkillLevel("Firemaking");
                                if (fmLv >= 30) {
                                    player.ConsumeItems(["herbCleanMarrentill,1"], false, true);
                                
                                    if (player.LeftIncense == 0) {
                                        player.LeftIncense = 140 + fmLv;
                                    } else {
                                        player.RightIncense = 140 + fmLv;
                                    }
                                    
                                    Log.AddMessage("You light an incense burner.", Color.Yellow);
                                } else {
                                    Log.AddMessage("You need at least level 30 Firemaking to light incense burners.", Color.Crimson);
                                }
                            } else {
                                Log.AddMessage("You need a tinderbox and a clean marrentill to light an incense burner.", Color.Crimson);
                            }
                        } else {
                            Log.AddMessage("Both incense burners are already lit. One will burn for " + player.LeftIncense + " seconds more, and the other " + player.RightIncense + " seconds.", Color.Crimson);
                        }
                    }
                }

                if (ActionID == "Teleport") { 
                    if (player.GetRegion() == "Tutorial Island") { 
                        player.NavLoc = "TI_Main";
                        GameLoop.ZPO.Log.AddMessage("The pull of the island distorts your magic and you appear in the center of Tutorial Island.", Color.MediumPurple);
                    } else {
                        if (GameLoop.ZPO.Atlas.TryGetValue(MiscString, out Location? dest)) { 
                            player.NavLoc = MiscString;
                            if (MiscInt != 1)
                                GameLoop.ZPO.Log.AddMessage("You teleport to " + dest.DisplayName + ".", Color.MediumPurple);
                            else
                                GameLoop.ZPO.Log.AddMessage("You travel to " + dest.DisplayName + ".", Color.MediumPurple);
                        } else {
                            GameLoop.ZPO.Log.AddMessage("Destination does not exist.", Color.Crimson);
                        } 
                    } 
                }

                if (ActionID == "TransmuteBones") {
                    bool anyBones = false;
                    int extras = 0;
                    foreach (var kv in player.Inventory) {
                        if (new List<string>() { "bonesRegular", "bonesWolf", "bonesBat", "bonesBig", "bonesBleached", "bonesBurnt", "bonesJogre", "bonesMonkey", "mtaBone1", "mtaBone2", "mtaBone3", "mtaBone4", "bonesAlan" }.Contains(kv.ID) && !kv.Noted) {
                            if (kv.ID.Contains("mtaBone")) {
                                int qty = 0;
                                if (MinigameManager.MTA_Bones[1] == kv.ID) { qty = 1; }
                                if (MinigameManager.MTA_Bones[2] == kv.ID) { qty = 2; }
                                if (MinigameManager.MTA_Bones[3] == kv.ID) { qty = 3; }
                                                    
                                extras += qty;
                            }
                            kv.ID = "fruit" + MiscString;
                            anyBones = true; 
                        }
                    }

                    if (extras > 0) {
                        if (GameLoop.ZPO.ItemLibrary.TryGetValue("fruit" + MiscString, out Item? banan) && banan != null) {
                            player.TryPickup(new Item(banan), extras);
                        }
                    }

                    if (anyBones) {
                        if (GameLoop.ZPO.SpellLibrary.TryGetValue(MiscString2, out Spell? bones) && bones != null) {
                            bones.Cast(player, GameLoop.ZPO.Log, SidebarManager.RecentlyTrainedSkills);
                            string plural = (MiscString == "Banana" ? "Bananas" : "Peaches");
                            GameLoop.ZPO.Log.AddMessage("You cast Bones to " + plural + " and all your bones become " + plural.ToLower() + ".", Color.MediumPurple);
                        }
                    } else {
                        GameLoop.ZPO.Log.AddMessage("You have no applicable bones to transmute.", Color.Crimson);
                    }
                }

                if (ActionID == "SuperheatItem") {
                    if (GameLoop.ZPO.ProcessingStations.TryGetValue("Furnace", out ProcessingStation? furnace) && furnace != null) {
                        furnace.LastWorked = "";
                        if (furnace.TryProcessItem(player, GameLoop.ZPO.Log, GameLoop.ZPO.ItemLibrary, SidebarManager.RecentlyTrainedSkills)) {
                            if (GameLoop.ZPO.SpellLibrary.TryGetValue(MiscString2, out Spell? superheat) && superheat != null) {
                                superheat.Cast(player, GameLoop.ZPO.Log, SidebarManager.RecentlyTrainedSkills);
                                GameLoop.ZPO.Log.AddMessage("You cast Superheat Item and smelt an ore mix into a bar.", Color.MediumPurple);
                            }
                        } else {
                            GameLoop.ZPO.Log.AddMessage("You have no ore mixes you have the smithing level to be able to smelt.", Color.Crimson);
                        }
                    }
                }

                if (ActionID == "EnchantItem") {
                    List<string> types = [ "sapphire/opal", "emerald/jade", "ruby/red topaz", "diamond", "dragonstone", "onyx", "zenyte" ];
                    if (GameLoop.ZPO.ProcessingStations.TryGetValue("Level " + MiscInt + " Enchanter", out ProcessingStation? enchant) && enchant != null) {
                        enchant.LastWorked = "";
                        if (enchant.TryProcessItem(player, GameLoop.ZPO.Log, GameLoop.ZPO.ItemLibrary, SidebarManager.RecentlyTrainedSkills)) {
                            if (GameLoop.ZPO.SpellLibrary.TryGetValue(MiscString2, out Spell? spell)) {
                                spell.Cast(player, GameLoop.ZPO.Log, SidebarManager.RecentlyTrainedSkills);
                                GameLoop.ZPO.Log.AddMessage("You enchant a piece of " + types[MiscInt - 1] + " jewellery.", Color.MediumPurple);
                            }
                        } else {
                            GameLoop.ZPO.Log.AddMessage("You have no unenchanted " + types[MiscInt - 1] + " jewellery to enchant.", Color.Crimson);
                        }
                    } 
                }

                if (ActionID == "LeptocClean") { 
                    Helper.AlterWorldState("LeptocTimesCleaned", "add", 1); 

                    GameLoop.ZPO.Log.AddMessage("You tidy up the room.", Color.Yellow);

                    if (Helper.ReadWorldState("LeptocTimesCleaned") == 3) { 
                        GameLoop.ZPO.Log.AddMessage("Wait, why am I actually cleaning? This was just an excuse to get into the mansion.", Color.Yellow);
                    }
                }

                if (ActionID == "SummonTempMonster") {
                    if (GameLoop.ZPO.Atlas.TryGetValue(player.NavLoc, out Location? curr) && curr != null) {
                        if (GameLoop.ZPO.MonsterLibrary.TryGetValue(MiscString, out AreaMonster? mon)) { 
                            for (int i = 0; i < MiscInt; i++) { 
                                Log.AddMessage(MiscString2 + " summons a" + (Helper.VowelStart(mon.Name.ToLower()) ? "n ": " ") + mon.Name.ToLower() + "!", Color.Crimson);
                                AreaMonster summonCopy = Helper.Clone(mon);
                                summonCopy.Temporary = true;
                                curr.MonstersHere.Add(summonCopy);
                            }
                        }
                    }
                }
            }
        }
    }
}
