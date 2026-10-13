using Newtonsoft.Json;
using ZeroPlayersOnline.Managers;

namespace ZeroPlayersOnline.DataTypes {
    public class ProcessingStation {
        public string Name = ""; 

        public List<ProcessingRecipe> Recipes = new();

        public bool OpensUI = false;
        public bool DontIncludeInCounts = false;
        public int MaxLevel = 99; // Basically only going to be used to gate POH stations so that you can't do everything for very little investment in Construction
        public string AltUI = "";

        [JsonIgnore]
        public int TimeLeft = -1;

        [JsonIgnore]
        public double TimeMade = 0;

        [JsonIgnore]
        public string ItemOnExpire = "";

        [JsonIgnore]
        public string LastWorked = "";

        public ProcessingStation(string n, bool ui = false, bool dont = false, int maxLevel = 99, string alt = "") {
            Name = n;  
            OpensUI = ui;
            DontIncludeInCounts = dont;
            MaxLevel = maxLevel;
            AltUI = alt;
        }

        public bool TryProcessItem(Player p, MessageLog log, Dictionary<string, Item> ItemLibrary, List<Skill> RecentSkills) { 
            for (int i = 0; i < p.Inventory.Count; i++) {
                bool changedInv = false;
                for (int j = 0; j < Recipes.Count; j++) {
                    if (p.Inventory[i].ID == Recipes[j].InputID && !p.Inventory[i].Noted) {
                        if (Recipes[j].SkillUsed != "") {
                            if (p.Skills.ContainsKey(Recipes[j].SkillUsed) && p.Skills[Recipes[j].SkillUsed].Level < Recipes[j].SkillLevel) {
                                log.AddMessage(new ColoredString("You get the feeling you should have " + Recipes[j].SkillLevel + " " + Recipes[j].SkillUsed + " to make " + GameLoop.ZPO.ResolveItemName(Recipes[j].OutputID) + ".", Color.Crimson, Color.Black));
                                continue;
                            } 
                        } 

                        if (Recipes[j].SkillLevel > MaxLevel) {
                            log.AddMessage(new ColoredString(Name + " can only handle recipes up to level " + Recipes[j].SkillLevel + " " + Recipes[j].SkillUsed + ".", Color.Crimson, Color.Black));
                            continue;
                        }

                        if (Recipes[j].SecondaryIn != "" && !p.HasAllItems([Recipes[j].SecondaryIn + ",1" ])) {
                            continue;
                        } 

                        if (Recipes[j].TertiaryIn != "" && !p.HasAllItems([Recipes[j].TertiaryIn + ",1"], false, true)) {
                            continue;
                        }

                        if (LastWorked == "") {
                            LastWorked = p.Inventory[i].ID; 
                        } else {
                            if (LastWorked != p.Inventory[i].ID)
                                continue;
                        }

                        if (Recipes[j].SkillUsed == "Runecrafting" && Recipes[j].TertiaryIn != "") {
                            int pureEssCount = 0;

                            foreach (var kv in p.Inventory) {
                                if (kv.ID == "pureEssence") {
                                    pureEssCount++;
                                }
                            }

                            if (p.Inventory[i].Quantity < pureEssCount) {
                                pureEssCount = p.Inventory[i].Quantity;
                            }

                            p.Inventory[i].Quantity -= pureEssCount;
                            if (p.Inventory[i].Quantity <= 0) {
                                p.Inventory.RemoveAt(i);
                                changedInv = true;
                            }

                            int invCount = p.Inventory.Count;
                            if (Recipes[j].SecondaryIn != "") {
                                p.ConsumeItems([Recipes[j].SecondaryIn + "," + pureEssCount]);
                            }
                            if (invCount != p.Inventory.Count) {
                                changedInv = true;
                            }

                            invCount = p.Inventory.Count;
                            if (Recipes[j].TertiaryIn != "") {
                                p.ConsumeItems([Recipes[j].TertiaryIn + "," + pureEssCount], false, true);
                            }
                            if (invCount != p.Inventory.Count) {
                                changedInv = true;
                            } 


                            int extra = 0;

                            if (ItemLibrary.ContainsKey(Recipes[j].OutputID)) {
                                Item item = new(ItemLibrary[Recipes[j].OutputID]);
                                item.Quantity = pureEssCount;

                                if (Recipes[j].HighSkillExtraOutputs) {
                                    int skillDiff = p.Skills[Recipes[j].SkillUsed].Level - Recipes[j].SkillLevel;
                                    extra = (int) Math.Floor(skillDiff / 10.0);
                                    item.Quantity += extra * pureEssCount;
                                }

                                if (GameLoop.rand.Next(2) == 0) {
                                    if (p.Equipment.TryGetValue("Amulet", out ItemWrapper? eqp) && eqp != null && eqp.ID == "necklaceBinding") {
                                        eqp.Charges -= 1; 
                                        log.AddMessage(new ColoredString("Your binding necklace lets you combine all the runes successfully.", Color.AntiqueWhite, Color.Black));

                                        if (eqp.Charges <= 0) { 
                                            log.AddMessage(new ColoredString("Your binding necklace runs out of charge and shatters.", Color.Crimson, Color.Black));
                                            p.Equipment.Remove("Amulet");
                                        }
                                    } else {
                                        item.Quantity /= 2;
                                        if (item.Quantity == 0)
                                            item.Quantity = 1;
                                    }
                                }

                                p.TryPickup(item, item.Quantity);
                            }

                            p.TryGrantExp(Recipes[j].SkillUsed, (Recipes[j].SkillEXP * (1 + extra)) * pureEssCount, log, RecentSkills);
                            return true;
                        } else if (Recipes[j].SkillUsed == "Smithing" && Recipes[j].TertiaryIn != "") { // AKA: Smelting bars and they've got a ring of forging
                            if (!p.HasAllItems([Recipes[j].TertiaryIn+",1"], false, true, true)) {
                                return false;
                            }

                            p.Inventory[i].Quantity -= 1;
                            if (p.Inventory[i].Quantity <= 0) {
                                p.Inventory.RemoveAt(i);
                                changedInv = true;
                            }

                            int invCount = p.Inventory.Count;
                            if (Recipes[j].SecondaryIn != "") {
                                p.ConsumeItems([Recipes[j].SecondaryIn+",1"]);
                            }
                            if (invCount != p.Inventory.Count) {
                                changedInv = true;
                            }

                            int extra = 0;

                            if (ItemLibrary.ContainsKey(Recipes[j].OutputID)) {
                                Item item = new(ItemLibrary[Recipes[j].OutputID]);

                                if (Recipes[j].HighSkillExtraOutputs) {
                                    int skillDiff = p.Skills[Recipes[j].SkillUsed].Level - Recipes[j].SkillLevel;
                                    extra = (int) Math.Floor(skillDiff / 10.0);
                                    item.Quantity += extra;
                                }

                                p.TryPickup(item, item.Quantity);
                            }

                            if (Recipes[j].SecondaryOut != "") {
                                if (ItemLibrary.ContainsKey(Recipes[j].SecondaryOut)) {
                                    Item item = new(ItemLibrary[Recipes[j].SecondaryOut]); 

                                    p.TryPickup(item, item.Quantity);
                                }
                            }
                            string nameOre = GameLoop.ZPO.ResolveItemName(Recipes[j].InputID).ToLower();
                            string nameBar = GameLoop.ZPO.ResolveItemName(Recipes[j].OutputID).ToLower();
                            log.AddMessage("Your ring of forging allows you to smelt a" + (Helper.VowelStart(nameOre) ? "n " : " ") + nameOre + " directly into a" + (Helper.VowelStart(nameBar) ? "n " : " ") + nameBar + ".", Color.Crimson);

                            if (p.Equipment.TryGetValue("Ring", out ItemWrapper? eqp) && eqp != null && eqp.ID == "ringForging") {
                                eqp.Charges -= 1;  

                                if (eqp.Charges <= 0) { 
                                    log.AddMessage(new ColoredString("Your ring of forging runs out of charge and shatters.", Color.Crimson, Color.Black));
                                    p.Equipment.Remove("Ring");
                                }
                            }

                            p.TryGrantExp(Recipes[j].SkillUsed, Recipes[j].SkillEXP * (1 + extra), log, RecentSkills);
                            return true;
                        } else {
                            p.Inventory[i].Quantity -= 1;
                            if (p.Inventory[i].Quantity <= 0) {
                                p.Inventory.RemoveAt(i);
                                changedInv = true;
                            }

                            int invCount = p.Inventory.Count;
                            if (Recipes[j].SecondaryIn != "") {
                                p.ConsumeItems([Recipes[j].SecondaryIn]);
                            }
                            if (invCount != p.Inventory.Count) {
                                changedInv = true;
                            }

                            int extra = 0;

                            if (Recipes[j].FailOutput != "" && p.GetEffectiveSkillLevel(Recipes[j].SkillUsed) < Recipes[j].StopFailingLevel) {
                                int failRoll = GameLoop.rand.Next(100);  
                                // TODO: Make cooking gauntlets lower fail level after Family Crest is implemented

                                int skillForRoll = 50 + (p.GetEffectiveSkillLevel(Recipes[j].SkillUsed) - Recipes[j].SkillLevel);

                                if (failRoll <= skillForRoll || (p.Equipment.TryGetValue("Cape", out ItemWrapper? cape) && cape != null && cape.ID == "capeSkillCooking")) {
                                    if (ItemLibrary.ContainsKey(Recipes[j].OutputID)) {
                                        Item item = new(ItemLibrary[Recipes[j].OutputID]);
                                        item.Quantity = Recipes[j].OutputQtyBase;
                                        if (Recipes[j].NoteOutput) { item.Noted = true; }

                                        if (Recipes[j].HighSkillExtraOutputs) {
                                            int skillDiff = p.Skills[Recipes[j].SkillUsed].Level - Recipes[j].SkillLevel;
                                            extra = (int) Math.Floor(skillDiff / 10.0);
                                            item.Quantity += extra;
                                        }

                                        p.TryPickup(item, item.Quantity, item.Noted);
                                    }
                                
                                    p.TryGrantExp(Recipes[j].SkillUsed, Recipes[j].SkillEXP * (1 + extra), log, RecentSkills);
                                } else {
                                    if (ItemLibrary.ContainsKey(Recipes[j].FailOutput)) {
                                        Item item = new(ItemLibrary[Recipes[j].FailOutput]);
                                        item.Quantity = Recipes[j].OutputQtyBase;
                                        if (Recipes[j].NoteOutput) { item.Noted = true; }

                                        if (Recipes[j].HighSkillExtraOutputs) {
                                            int skillDiff = p.Skills[Recipes[j].SkillUsed].Level - Recipes[j].SkillLevel;
                                            extra = (int) Math.Floor(skillDiff / 10.0);
                                            item.Quantity += extra;
                                        }

                                        p.TryPickup(item, item.Quantity, item.Noted);
                                    }
                                
                                    p.TryGrantExp(Recipes[j].SkillUsed, 1 * (1 + extra), log, RecentSkills);
                                }
                            } else { 
                                if (ItemLibrary.ContainsKey(Recipes[j].OutputID)) {
                                    Item item = new(ItemLibrary[Recipes[j].OutputID]);
                                    item.Quantity = Recipes[j].OutputQtyBase;

                                    if (Recipes[j].NoteOutput) { item.Noted = true; }

                                    if (Recipes[j].HighSkillExtraOutputs) {
                                        int skillDiff = p.Skills[Recipes[j].SkillUsed].Level - Recipes[j].SkillLevel;
                                        extra = (int) Math.Floor(skillDiff / 10.0);
                                        item.Quantity += extra;
                                    }

                                    p.TryPickup(item, item.Quantity, item.Noted);
                                }
                                
                                p.TryGrantExp(Recipes[j].SkillUsed, Recipes[j].SkillEXP * (1 + extra), log, RecentSkills);
                            }

                            if (Recipes[j].OutputID[0] == '*') { // Aka shorthand for random output, mostly for POH repair benches
                                if (Recipes[j].OutputID == "*Arrow") {
                                    if (GameLoop.rand.Next(100) < 70 + (p.GetEffectiveSkillLevel("Fletching") / 4)) {
                                        if (GameLoop.rand.Next(25) == 0) { if (GameLoop.ZPO.ResolveItem("arrowsMithril") is Item arrow) { p.TryPickup(arrow, 1); } }
                                        else if (GameLoop.rand.Next(20) == 0) { if (GameLoop.ZPO.ResolveItem("headlessShaft") is Item arrow) { p.TryPickup(arrow, 1); } }
                                        else if (GameLoop.rand.Next(10) == 0) { if (GameLoop.ZPO.ResolveItem("arrowsSteel") is Item arrow) { p.TryPickup(arrow, 1); } }
                                        else if (GameLoop.rand.Next(4) == 0) { if (GameLoop.ZPO.ResolveItem("arrowsBronze") is Item arrow) { p.TryPickup(arrow, 1); } }
                                        else { if (GameLoop.ZPO.ResolveItem("arrowsIron") is Item arrow) { p.TryPickup(arrow, 1); } }
                                    }
                                } 
                                else if (Recipes[j].OutputID == "*Staff") {
                                    if (GameLoop.rand.Next(100) < 60 + (p.GetEffectiveSkillLevel("Crafting") / 3)) {
                                        if (GameLoop.rand.Next(25) == 0) { if (GameLoop.ZPO.ResolveItem("staffFire") is Item staff) { p.TryPickup(staff, 1); } }
                                        else if (GameLoop.rand.Next(25) == 0) { if (GameLoop.ZPO.ResolveItem("staffEarth") is Item staff) { p.TryPickup(staff, 1); } }
                                        else if (GameLoop.rand.Next(25) == 0) { if (GameLoop.ZPO.ResolveItem("staffAir") is Item staff) { p.TryPickup(staff, 1); } }
                                        else if (GameLoop.rand.Next(20) == 0) { if (GameLoop.ZPO.ResolveItem("staffWater") is Item staff) { p.TryPickup(staff, 1); } }
                                        else if (GameLoop.rand.Next(4) == 0) { if (GameLoop.ZPO.ResolveItem("staffMagic") is Item staff) { p.TryPickup(staff, 1); } }
                                        else { if (GameLoop.ZPO.ResolveItem("staff") is Item staff) { p.TryPickup(staff, 1); } }
                                    }
                                }
                                else if (Recipes[j].OutputID == "*Sword") {
                                    if (GameLoop.rand.Next(100) < 40 + (p.GetEffectiveSkillLevel("Smithing") / 2)) {
                                        if (GameLoop.rand.Next(50) == 0) { if (GameLoop.ZPO.ResolveItem("swordBlack") is Item sword) { p.TryPickup(sword, 1); } }
                                        else if (GameLoop.rand.Next(25) == 0) { if (GameLoop.ZPO.ResolveItem("swordMithril") is Item sword) { p.TryPickup(sword, 1); } }
                                        else if (GameLoop.rand.Next(17) == 0) { if (GameLoop.ZPO.ResolveItem("swordSteel") is Item sword) { p.TryPickup(sword, 1); } }
                                        else if (GameLoop.rand.Next(6) == 0) { if (GameLoop.ZPO.ResolveItem("swordBronze") is Item sword) { p.TryPickup(sword, 1); } } 
                                        else { if (GameLoop.ZPO.ResolveItem("swordIron") is Item sword) { p.TryPickup(sword, 1); } }
                                    }
                                }
                                
                            }

                            if (Recipes[j].SecondaryOut != "") {
                                if (ItemLibrary.ContainsKey(Recipes[j].SecondaryOut)) {
                                    Item item = new(ItemLibrary[Recipes[j].SecondaryOut]); 

                                    p.TryPickup(item, item.Quantity);
                                }
                            } 


                            if (Recipes[j].MinigameAction != "") {
                                if (Recipes[j].MinigameAction == "mtaPizazz") {
                                    MinigameManager.MTA_Count++;

                                    int reward = 1;
                                    if (Name.Contains("2")) { reward = 2; }
                                    if (Name.Contains("3")) { reward = 3; }
                                    if (Name.Contains("4")) { reward = 4; }
                                    if (Name.Contains("5")) { reward = 5; }
                                    if (Name.Contains("6")) { reward = 6; }
                                    if (Name.Contains("7")) { reward = 7; }
                                    
                                    if (Recipes[j].InputID == "mtaDragonstone") { reward *= 2; } 
                                    if (MinigameManager.MTA_Count >= 10) { reward *= 2; MinigameManager.MTA_Count = 0; } 
                                    if (Recipes[j].InputID.Contains(MinigameManager.MTA_Special)) { reward += 2; } 

                                    int oldDiv = p.PizazzEnchantment / 100;
                                    p.PizazzEnchantment += reward;
                                    int newDiv = p.PizazzEnchantment / 100;
                                    
                                    if (oldDiv != newDiv) { log.AddMessage("You have passed " + (newDiv * 100) + " Enchantment Pizazz points.", Color.Lime); }
                                }

                                if (Recipes[j].MinigameAction == "mtaOrb") {
                                    MinigameManager.MTA_Orbs++;
                                    if (MinigameManager.MTA_Orbs >= 10) {
                                        List<string> rewards = [ "runeBlood,3", "runeDeath,3", "runeCosmic,3" ];
                                        string[] reward = rewards[GameLoop.rand.Next(3)].Split(",");
                                        int.TryParse(reward[1], out int qty);

                                        if (GameLoop.ZPO.ItemLibrary.TryGetValue(reward[0], out Item? rewardItem) && rewardItem != null) {
                                            p.TryPickup(new Item(rewardItem), qty);
                                            log.AddMessage("For depositing 10 orbs, you receive a reward of " + qty + " " + rewardItem.Name.ToLower() + "s.", Color.Lime);
                                        } 
                                        MinigameManager.MTA_Orbs = 0;
                                    }
                                }

                                if (Recipes[j].MinigameAction == "mtaFruit") {
                                    if (Recipes[j].InputID == "fruitBanana") { MinigameManager.MTA_FruitCount += 1;}
                                    if (Recipes[j].InputID == "fruitPeach") { MinigameManager.MTA_FruitCount += 2;}
                                    if (MinigameManager.MTA_FruitCount >= 16) { 
                                        MinigameManager.MTA_FruitCount -= 16; 
                                        p.PizazzGraveyard++; 

                                        List<string> rewards = [ "runeWater,1", "runeEarth,1", "runeNature,1", "runeDeath,1", "runeBlood,1" ];
                                        string[] reward = rewards[GameLoop.rand.Next(3)].Split(",");
                                        int.TryParse(reward[1], out int qty);

                                        if (GameLoop.ZPO.ItemLibrary.TryGetValue(reward[0], out Item? rewardItem) && rewardItem != null) {
                                            p.TryPickup(new Item(rewardItem), qty);
                                            log.AddMessage("For depositing 16 points of fruit, you receive a reward of one " + rewardItem.Name.ToLower() + ".", Color.Lime);
                                        }
                                    }
                                }

                                if (Recipes[j].MinigameAction == "mtaCoin") { 
                                    MinigameManager.MTA_CoinCount++;
                                    if (MinigameManager.MTA_CoinCount >= 100) { 
                                        MinigameManager.MTA_CoinCount -= 100; 
                                        p.PizazzAlchemist++; 
                                    }
                                } 
                            }

                            if (Recipes[j].Actions.Count > 0) {
                                foreach (var act in Recipes[j].Actions) {
                                    act.Execute();
                                }
                            }

                            return true;
                        }
                    } 
                    if (changedInv)
                        break;
                }
                if (changedInv)
                    break;
            }

            return false;
        }
    }
}
