using System;
using System.Collections.Generic;
using System.Text;
using ZeroPlayersOnline.DataTypes;
using ZeroPlayersOnline.Managers;

namespace ZeroPlayersOnline.UI {
    public static class UI_LocCombat { 
        public static AreaMonster AttackingMonster = null;
        public static bool AttackingBoss = false;
        public static double LastHitTime = 0;
        public static double GraceTimeStart = 0;
        public static string Targetting = "Single";  

        public static double LastClickTime = 0;

        public static int DrawCombat(Location curr, Player player, UI_EmbeddedMini mini, int printY, MessageLog Log) {
            if ((AttackingBoss || AttackingMonster != null) && LastClickTime + 600_000 < Helper.Time()) { // if player hasn't clicked an enemy in 10 minutes, stop fighting
                AttackingBoss = false;
                AttackingMonster = null; 

                GameLoop.ZPO.Log.AddMessage("You feel your arms get tired of this repetitive motion. To prove your determination and continue attacking, click to attack an enemy again.");
            }



            if (curr.BossHere != "" && GameLoop.ZPO.BossLibrary.TryGetValue(curr.BossHere, out BossFight? boss) && boss != null) {  
                mini.Con.DrawLine(new Point(56, printY), new Point(109, printY++), 196);
                mini.Con.Print(57, printY, "Boss Here: ");

                mini.Con.PrintClickable(68, printY, "(Log)", () => {
                    ExtraWindows.CollectionLog.IsVisible = true;
                    ExtraWindows.CollectionID = boss.ID;
                    ExtraWindows.CollectionCat = "Boss";
                    ExtraWindows.CollectionDropTop = 0;
                });

                mini.Con.PrintClickable(74, printY, new ColoredString("(Attack)", AttackingBoss ? Color.Crimson : Color.DarkSlateGray, Color.Black), () => {
                    if (!AttackingBoss && curr.Dark && !player.HasLightSource()) {
                        Log.AddMessage("It may be a bad idea to aggravate a monster you can't currently see to hit.");
                    } else {
                        AttackingBoss = !AttackingBoss;

                        if (AttackingBoss) {
                            LastClickTime = Helper.Time();
                        }
                    }
                });
                    
                double timeToAttack = boss.TimeLastAttacked + boss.AttackSpeedInMS - Helper.Time(); 
                string bossDice = boss.DefaultDmgDice;
                string bossType = boss.DefaultDmgType;
                int bossPoisonSeverity = boss.DefaultPoisonSeverity;

                if (boss.UsingMove != -1 && boss.Specials.Count > boss.UsingMove) {
                    bossDice = boss.Specials[boss.UsingMove].DamageDice;
                    bossType = boss.Specials[boss.UsingMove].DamageType;
                    bossPoisonSeverity = boss.Specials[boss.UsingMove].PoisonSeverity;
                }

                bool reachedKillLimit = player.KillLimit == 0 ? true : false;
                if (player.CollectionLogBoss.ContainsKey(boss.ID)) {
                    reachedKillLimit = player.KillLimit != -1 && player.CollectionLogBoss[boss.ID].KillCount >= player.KillLimit; 
                }

                if (GraceTimeStart + 5000 > Helper.Time()) {
                    mini.Con.Print(98, printY, "Grace: " + Math.Floor(GraceTimeStart + 5000 - Helper.Time()), Color.DarkSlateGray);
                } else { 
                    if (timeToAttack > 0 && boss.CurrentHP > 0)
                        mini.Con.Print(95, printY, "Attack:  " + (Math.Floor(timeToAttack)).ToString().Align(HorizontalAlignment.Right, 5));
                } 

                printY++; 

                mini.Con.Print(57, printY, "| " + boss.Name + " (" + boss.CurrentHP + "/" + boss.MaxHP + " hp)", (boss.AggroLevel > player.GetCombatLevel() || boss.AlwaysAggro || boss.AttackingPlayer) ? Color.Crimson : Color.White);
                mini.Con.Print(97, printY, "Next: " + bossType.Align(HorizontalAlignment.Right, 6));
                    

                printY += 2;


                    
                mini.Con.Print(57, printY++, "Lanes ");
                for (int i = 0; i < boss.LanesHere; i++) {
                    mini.Con.PrintClickable(57, printY, "| Lane: ", () => { boss.CurrentLane = i; });

                    if (boss.CurrentLane == i) {
                        Color col = Color.White;

                        if (boss.UsingMove != -1) {
                            if (boss.Specials.Count > boss.UsingMove) {
                                if (boss.Specials[boss.UsingMove].AdjustedLanes(boss.LaneWhenMoveSelected, boss.LanesHere).Contains(i)) {
                                    if (timeToAttack > boss.AttackSpeedInMS / 2.0) {
                                        col = Color.Yellow;
                                    } else {
                                        col = Color.Crimson;
                                    }
                                }
                            }
                        }

                        mini.Con.PrintClickable(65, printY++, new ColoredString("(standing here)", col, Color.Black), () => { boss.CurrentLane = i; });
                    } else {
                        Color col = Color.DarkSlateGray;

                        if (boss.UsingMove != -1) {
                            if (boss.Specials.Count > boss.UsingMove) {
                                if (boss.Specials[boss.UsingMove].AdjustedLanes(boss.LaneWhenMoveSelected, boss.LanesHere).Contains(i)) {
                                    if (timeToAttack > boss.AttackSpeedInMS / 2.0) {
                                        col = Color.Yellow;
                                    } else {
                                        col = Color.Crimson;
                                    }
                                }
                            }
                        }

                        mini.Con.PrintClickable(65, printY++, new ColoredString("(empty)", col, Color.Black), () => { boss.CurrentLane = i; });
                    }
                }

                if (GraceTimeStart + 5000 < Helper.Time() && boss.TimeLastAttacked + boss.AttackSpeedInMS < Helper.Time() && boss.CurrentHP > 0) {
                    if (boss.AggroLevel > player.GetCombatLevel() || boss.AlwaysAggro || boss.AttackingPlayer) {
                        boss.TimeLastAttacked = Helper.Time();

                        int dmg = GoRogue.DiceNotation.Dice.Roll(bossDice);
                        if (dmg < 0)
                            dmg = 0;

                        int modified = dmg;
                             

                        int hitChance = GameLoop.rand.Next(100);

                        bool safespotting = false;

                        if (player.Equipment.TryGetValue("Weapon", out ItemWrapper? weaponWrap) && weaponWrap.GetRef() is Item weapon) {
                            if (weapon.EquipSkill == "Ranged") {
                                if (bossType == "Melee") {
                                    safespotting = true;
                                }
                            }
                        }

                        if (player.IsMaging() && bossType == "Melee") {
                            safespotting = true;
                        }

                        if (boss.UsingMove != -1 && boss.Specials.Count > boss.UsingMove && boss.Specials[boss.UsingMove].SummonID != "") {
                            if (GameLoop.ZPO.MonsterLibrary.TryGetValue(boss.Specials[boss.UsingMove].SummonID, out AreaMonster? summon) && summon != null) {
                                int amount = GoRogue.DiceNotation.Dice.Roll(boss.Specials[boss.UsingMove].SummonDice);

                                for (int i = 0; i < amount; i++) {
                                    Log.AddMessage(boss.Name + " summons a" + (Helper.VowelStart(summon.Name.ToLower()) ? "n ": " ") + summon.Name.ToLower() + "!", Color.Crimson);
                                    AreaMonster summonCopy = Helper.Clone(summon);
                                    summonCopy.Temporary = true;
                                    curr.MonstersHere.Add(summonCopy);
                                }
                            }
                        }

                        if (!safespotting && boss.UsingMove != -1 && boss.Specials.Count > boss.UsingMove && !boss.Specials[boss.UsingMove].AdjustedLanes(boss.LaneWhenMoveSelected, boss.LanesHere).Contains(boss.CurrentLane)) {
                            safespotting = true;
                            Log.AddMessage(new ColoredString("You deftly dodge out of the way of the boss' special attack.", Color.Green, Color.Black));
                        } 

                        if (!safespotting) {
                            if (hitChance < 25 + (player.GetEffectiveSkillLevel("Defense") / 4.0)) {
                                Log.AddMessage(new ColoredString(boss.Name + " tried to hit you but missed!", Color.Yellow, Color.Black));
                            } else {
                                modified -= player.TotalArmorValue(bossType);

                                if ((player.PrayerActive("Protect from Magic") && bossType == "Magic") || (player.PrayerActive("Protect from Melee") && bossType == "Melee") || (player.PrayerActive("Protect from Range") && bossType == "Ranged")) {
                                    modified /= 2;
                                } 
                                         
                                if (dmg != modified) {
                                    if (modified <= 0) {
                                        Log.AddMessage(new ColoredString(boss.Name + " hit you for " + dmg + ", but you took no damage!", Color.Crimson, Color.Black));
                                    } else {
                                        Log.AddMessage(new ColoredString(boss.Name + " hit you for " + dmg + ", reduced to " + modified + "!", Color.Crimson, Color.Black));
                                    }
                                } else {
                                    Log.AddMessage(new ColoredString(boss.Name + " hit you for " + dmg + "!", Color.Crimson, Color.Black));
                                }

                                if (modified > 0) {
                                    if (bossPoisonSeverity > 0) {
                                        int odds = bossType == "Melee" ? 4 : bossType == "Ranged" ? 8 : 1;

                                        if (GameLoop.rand.Next(odds) == 0 && player.PoisonStatus < bossPoisonSeverity) {
                                            bool antipoison = false;
                                            foreach (var pcheck in player.ActivePotions) {
                                                if (pcheck.Stat == "Antipoison" && pcheck.Change > 0) {
                                                    antipoison = true;
                                                }
                                            }
                                            if (!antipoison) {
                                                player.PoisonStatus = bossPoisonSeverity;
                                                Log.AddMessage("You have been poisoned!", Color.Lime);
                                            }
                                        }
                                    }

                                    if (player.Equipment.TryGetValue("Ring", out ItemWrapper? ring) && ring != null && ring.ID == "ringRecoil") {
                                        int reflect = (modified / 10) + 1;

                                        if (reflect > ring.Charges)
                                            reflect = ring.Charges;

                                        ring.Charges -= reflect; 
                                        Log.AddMessage(new ColoredString("Your ring of recoil reflected " + reflect + " damage back!", Color.DeepSkyBlue, Color.Black));

                                        if (ring.Charges <= 0) { 
                                            Log.AddMessage(new ColoredString("Your ring of recoil runs out of charge and shatters.", Color.Crimson, Color.Black));
                                            player.Equipment.Remove("Ring");
                                        }
                                    }
                                }
                                    
                                bool died = modified > 0 ? player.TakeDamage(modified, Log) : false;

                                if (died) {
                                    AttackingBoss = false;
                                    return printY; 
                                }
                            }
                        }

                        if (boss.UsingMove != -1) {
                            boss.UsingMove = -1;
                            boss.MovesSinceSpecial = 0;
                        } else {
                            boss.MovesSinceSpecial++;

                            if (boss.MovesSinceSpecial == boss.AttacksBetweenSpecials && boss.Specials.Count > 0) {
                                boss.UsingMove = GameLoop.rand.Next(boss.Specials.Count);
                                boss.LaneWhenMoveSelected = boss.CurrentLane;
                                Log.AddMessage(boss.Specials[boss.UsingMove].WarningText, Color.Yellow);
                            }
                        }

                    }
                }

                double attackSpeed = 1.0;
                     
                if (player.Equipment.TryGetValue("Weapon", out ItemWrapper? wepWrap) && wepWrap.GetRef() is Item wep) { 
                    attackSpeed = wep.AttackSpeed;
                }

                bool hasAmmo = true;
                bool usedAmmo = false;

                if (LastHitTime + (1000 * attackSpeed) < Helper.Time() && boss.CurrentHP > 0 && AttackingBoss) {
                    LastHitTime = Helper.Time();
                    boss.AttackingPlayer = true;

                    int hitChance = GameLoop.rand.Next(100); 
                             
                    // Remove a unit of ammo if this is a ranged weapon
                    if (!player.IsMaging()) {
                        if (wepWrap != null && wepWrap.GetRef() is Item weapon) {
                            if (weapon.EquipAmmo == "Self") {
                                wepWrap.Quantity -= 1;
                                Item droppedAmmo = new(weapon);
                                droppedAmmo.Quantity = 1;
                                if (GameLoop.rand.Next(4) != 0)
                                    GameLoop.ZPO.TryPlaceItem(player.NavLoc, new(droppedAmmo));
                                usedAmmo = true;
                            } else if (weapon.EquipAmmo == "RangedStandard") {
                                if (player.Equipment.TryGetValue("Ammo", out ItemWrapper? arrowWrap) && arrowWrap.GetRef() is Item arrow && arrow.EquipDamageType == "RangedStandard") {
                                    arrowWrap.Quantity -= 1;
                                    Item droppedAmmo = new(arrow);
                                    droppedAmmo.Quantity = 1;
                                    if (GameLoop.rand.Next(4) != 0)
                                        GameLoop.ZPO.TryPlaceItem(player.NavLoc, new(droppedAmmo));
                                    usedAmmo = true;
                                } else {
                                    hasAmmo = false;
                                }
                            } else if (weapon.EquipAmmo == "RangedHeavy") {
                                if (player.Equipment.TryGetValue("Ammo", out ItemWrapper? boltWrap) && boltWrap.GetRef() is Item bolt && bolt.EquipDamageType == "RangedHeavy") {
                                    boltWrap.Quantity -= 1;
                                    Item droppedAmmo = new(bolt);
                                    droppedAmmo.Quantity = 1;
                                    if (GameLoop.rand.Next(4) != 0)
                                        GameLoop.ZPO.TryPlaceItem(player.NavLoc, new(droppedAmmo));
                                    usedAmmo = true;
                                } else {
                                    hasAmmo = false;
                                }
                            }
                        } 
                    }

                    if (hasAmmo) {
                        string whichSkill = player.IsMaging() ? "Magic" : usedAmmo ? "Ranged" : "Attack";

                        if (player.IsMaging()) {
                            if (player.HasAllItems(["tab" + player.CastingSpell])) { 
                                player.ConsumeItems(["tab" + player.CastingSpell]);
                            } else {
                                player.ConsumeItems(GameLoop.ZPO.SpellLibrary[player.CastingSpell].Runes, false, true);
                            }
                            int spellExp = GameLoop.ZPO.SpellLibrary[player.CastingSpell].ExpOnCast;
                            player.TryGrantExp("Magic", spellExp, Log, SidebarManager.RecentlyTrainedSkills);
                        }

                        double eqpAccuracy = 0; 
                        foreach (var kv in player.Equipment) {
                            if (kv.Value.GetRef() is Item eqp) {
                                if (eqp.MiscString == "HitChance") {
                                    eqpAccuracy += eqp.EquipTier;
                                }
                            }
                        }

                        bool efaritay = false;

                        if (player.Equipment.TryGetValue("Ring", out ItemWrapper? ring) && ring != null && ring.ID == "ringEfaritay" && boss.SpecialCategory == "Vampiric") { 
                            ring.Charges -= 1; 
                            efaritay = true;
                            eqpAccuracy += 10;

                            if (ring.Charges <= 0) { 
                                Log.AddMessage(new ColoredString("Your Efaritay's aid runs out of charge and shatters.", Color.Crimson, Color.Black));
                                player.Equipment.Remove("Ring");
                            }
                        }

                        if (curr.Dark && !player.HasLightSource()) { 
                            Log.AddMessage(new ColoredString("You can't see well enough to land an attack!", Color.Crimson, Color.Black));
                            hitChance = 999;
                        }

                        if (hitChance > 25 + (player.GetEffectiveSkillLevel(whichSkill) / 2.0) + eqpAccuracy) {
                            Log.AddMessage(new ColoredString("You tried to hit the " + boss.Name + " but missed!", Color.Crimson, Color.Black));
                        } else {
                            int pdmg = GoRogue.DiceNotation.Dice.Roll(player.GetDamageDice());
                            bool crit = false;

                            if (player.GetDamageType() == boss.WeakType) {
                                pdmg = (int)Math.Ceiling(pdmg * 1.5f);
                            }
                                         
                            bool eitherDamageSpecial = boss.SpecialCategory != "" && (player.GetDamageType() == boss.SpecialCategory || player.GetSecondaryDamageType() == boss.SpecialCategory);

                            if (eitherDamageSpecial) { // Mostly for Crumble Undead, but open to similar spells for Demons, Vamps, etc
                                pdmg = (int)Math.Ceiling(pdmg * 2f);
                            }

                            if (efaritay) {
                                pdmg = (int) Math.Ceiling(pdmg * 1.2f);
                            }

                            if (boss.SpecialCategory == "Leaf" && !eitherDamageSpecial) {
                                pdmg = 0;    
                                Log.AddMessage(new ColoredString("You need a special kind of weapon to damage this enemy!", Color.Crimson, Color.Black));
                            }

                            if (efaritay) {
                                pdmg = (int) Math.Ceiling(pdmg * 1.2f);
                            }

                            if ((player.GetDamageType() == "Undead" && boss.SpecialCategory != "Undead") || (player.GetDamageType() == "Ratlike" && boss.SpecialCategory != "Ratlike")) {
                                pdmg = 0;    
                                Log.AddMessage(new ColoredString("That kind of attack only works against the " + player.GetDamageType().ToLower() + "!", Color.Crimson, Color.Black));
                            } else { 
                                int critTarget = 20;

                                if (player.PrayerActive("Improved Critical I"))
                                    critTarget -= 1;
                                if (player.PrayerActive("Improved Critical II"))
                                    critTarget -= 2;
                                if (player.PrayerActive("Improved Critical III"))
                                    critTarget -= 3;

                                int critRoll = GameLoop.rand.Next(20) + 1;

                                if (critRoll >= critTarget) {
                                    crit = true;
                                    pdmg *= 2;
                                }



                                if (pdmg > boss.CurrentHP)
                                    pdmg = boss.CurrentHP;

                                boss.CurrentHP -= pdmg;
                                     
                                Log.AddMessage(new ColoredString("You hit " + (boss.ProperName ? "" : "the ") + boss.Name + " for " + pdmg + "." + (crit ? " Critical Hit!" : ""), crit ? Color.Lime : Color.Green, Color.Black));
                            }

                            if (player.IsMaging()) {  
                                player.TryGrantExp("Magic", (int) (pdmg * 4 * boss.ExpMultiplier), Log, SidebarManager.RecentlyTrainedSkills);

                                string cast = player.CanCast(GameLoop.ZPO.SpellLibrary[player.CastingSpell]);
                                if (cast != "") { 
                                    Log.AddMessage(new ColoredString("Cannot cast spell anymore: " + cast, Color.Crimson, Color.Black)); 
                                    player.CastingSpell = "";
                                }
                            } else if (usedAmmo) { 
                                player.TryGrantExp("Ranged", (int) (pdmg * 4 * boss.ExpMultiplier), Log, SidebarManager.RecentlyTrainedSkills);
                            } else {
                                if (player.OffenseExpSplit > 0 && !reachedKillLimit)
                                    player.TryGrantExp("Attack", (int) (pdmg * player.OffenseExpSplit * boss.ExpMultiplier), Log, SidebarManager.RecentlyTrainedSkills);

                                if (player.OffenseExpSplit < 4 && !reachedKillLimit)
                                    player.TryGrantExp("Strength", (int) (pdmg * (4 - player.OffenseExpSplit) * boss.ExpMultiplier), Log, SidebarManager.RecentlyTrainedSkills);
                            }

                            if (player.DefenseExpSplit > 0 && !reachedKillLimit)
                                player.TryGrantExp("Defense", (int) (pdmg * player.DefenseExpSplit * boss.ExpMultiplier), Log, SidebarManager.RecentlyTrainedSkills);

                            if (player.DefenseExpSplit < 4 && !reachedKillLimit)
                                player.TryGrantExp("Constitution", (int) (pdmg * (4 - player.DefenseExpSplit) * boss.ExpMultiplier), Log, SidebarManager.RecentlyTrainedSkills);

                            if (boss.ID == player.SlayerTask|| boss.CountsAsSlayer.Contains(player.SlayerTask)) {
                                player.TryGrantExp("Slayer", (int) (pdmg * 4 * boss.ExpMultiplier), Log, SidebarManager.RecentlyTrainedSkills);
                            } 

                            if (boss.CurrentHP <= 0) {
                                bool canKill = true;
                                if (boss.KillItem != "") {
                                    if (player.HasAllItems([boss.KillItem +",1"], false, true)) {
                                        player.ConsumeItems([boss.KillItem + ",1"], false, true);
                                    } else {
                                        canKill = false;
                                        boss.CurrentHP = 0;
                                        Log.AddMessage(new ColoredString("You need " + GameLoop.ZPO.ResolveItemName(boss.KillItem) + " to finish this enemy off.", Color.Crimson, Color.Black)); 
                                    }
                                }

                                if (canKill) {
                                    boss.TimeLastKilled = Helper.Time();
                                    boss.AttackingPlayer = false;

                                    foreach (var quest in GameLoop.ZPO.QuestLibrary) {
                                        if (player.QuestLog.TryGetValue(quest.Key, out QuestStatus? status)) {
                                            if (status.CurrentStage != quest.Value.CompleteStage && quest.Value.Stages.ContainsKey(status.CurrentStage)) {
                                                if (quest.Value.Stages[status.CurrentStage].ProgressType == "Kill" && quest.Value.Stages[status.CurrentStage].MiscString == boss.ID) {
                                                    status.KillTracker++;

                                                    if (status.KillTracker >= quest.Value.Stages[status.CurrentStage].MiscInt) {
                                                        status.CurrentStage = quest.Value.Stages[status.CurrentStage].LeadsToStage;
                                                        status.KillTracker = 0;

                                                        if (status.CurrentStage == quest.Value.CompleteStage) {
                                                            Log.AddMessage(new ColoredString("You have completed " + quest.Value.Name + "!", Color.Lime, Color.Black));
                                                            quest.Value.ProcessRewards(player);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (!player.CollectionLogBoss.ContainsKey(boss.ID))
                                        player.CollectionLogBoss.Add(boss.ID, new(boss.ID));

                                    player.CollectionLogBoss[boss.ID].KillCount += 1;
                                     
                                    player.ProgressSlayerTask(boss);

                                    if (player.KillLimit != -1 && player.CollectionLogBoss[boss.ID].KillCount == player.KillLimit) {
                                        Log.AddMessage(new ColoredString("You've killed " + player.KillLimit + " " + boss.Name + "s and will no longer receive drops or exp from them."));
                                    }

                                    if (boss.DropTable != null && boss.DropTable.Count > 0) {
                                        for (int j = 0; j < boss.DropTable.Count; j++) {
                                            ItemDrop drop = boss.DropTable[j];

                                            drop.RollDrop(player, player.CollectionLogBoss[boss.ID]);
                                        }
                                    }
                                }
                            }
                        }
                    } else { 
                        Log.AddMessage(new ColoredString("You haven't got any valid ammo for your weapon!", Color.Crimson, Color.Black));
                    }
                }

                if (boss.CurrentHP <= 0 && boss.TimeLastKilled + (boss.RespawnTime * 1000) < Helper.Time()) {
                    boss.CurrentHP = boss.MaxHP; 
                    boss.TimeLastAttacked = Helper.Time();
                    boss.AttackingPlayer = false;
                }  

                if (usedAmmo) {
                    List<string> empties = new();
                    foreach (var kv in player.Equipment) {
                        if (kv.Value.Quantity <= 0) {
                            empties.Add(kv.Key);
                        }
                    }

                    foreach (var rem in empties) {
                        player.Equipment.Remove(rem);
                    }
                }
            }

            if (curr.MonstersHere.Where(o => o.AllReqsMet(player)).ToList().Count > 0) {
                mini.Con.DrawLine(new Point(56, printY), new Point(109, printY++), 196);

                mini.Con.Print(57, printY, "Monsters Here: ");
                mini.Con.PrintClickable(72, printY, new ColoredString("(SINGLE)", Targetting == "Single" ? Color.Yellow : Color.White, Color.Black), () => { Targetting = "Single"; });
                mini.Con.PrintClickable(81, printY, new ColoredString("(ORDER)", Targetting == "Order" ? Color.Yellow : Color.White, Color.Black), () => { Targetting = "Order"; });
                mini.Con.PrintClickable(89, printY++, new ColoredString("(RANDOM)", Targetting == "Random" ? Color.Yellow : Color.White, Color.Black), () => { Targetting = "Random"; });

                if (GraceTimeStart + 5000 > Helper.Time()) {
                    mini.Con.Print(98, printY - 1, "Grace: " + Math.Floor(GraceTimeStart + 5000 - Helper.Time()), Color.DarkSlateGray);
                }

                if (AttackingMonster != null && AttackingMonster.CurrentHP <= 0) {
                    if (Targetting == "Order") {
                        for (int j = 0; j < curr.MonstersHere.Count; j++) {
                            if (curr.MonstersHere[j].CurrentHP > 0) { 
                                AttackingMonster = curr.MonstersHere[j];
                                break;
                            }
                        }
                    }
                    else if (Targetting == "Random") { 
                        List<AreaMonster> alive = new();

                        for (int mon = 0; mon < curr.MonstersHere.Count; mon++) {
                            if (curr.MonstersHere[mon].CurrentHP > 0) { 
                                alive.Add(curr.MonstersHere[mon]);
                            }
                        }

                        while (AttackingMonster.CurrentHP <= 0 && alive.Count > 0) {
                            AttackingMonster = alive[GameLoop.rand.Next(alive.Count)];
                        }
                    }
                } 

                for (int i = 0; i < curr.MonstersHere.Count; i++) {
                    AreaMonster thisOne = curr.MonstersHere[i];

                    bool reachedKillLimit = player.KillLimit == 0 ? true : false;
                    if (player.CollectionLog.ContainsKey(thisOne.ID)) {
                        reachedKillLimit = player.KillLimit != -1 && player.CollectionLog[thisOne.ID].KillCount >= player.KillLimit; 
                    }

                    Color nameCol = Color.White;

                    if ((thisOne.Level > player.GetCombatLevel() && thisOne.AlwaysAggro) || thisOne.AttackingPlayer)
                        nameCol = Color.Crimson;

                    if (thisOne == AttackingMonster)
                        nameCol = Color.Yellow;

                    if (thisOne.CurrentHP <= 0)
                        nameCol = nameCol.GetDarker();

                    if (!thisOne.AllReqsMet(player)) { 
                        nameCol = Color.DarkSlateGray; 
                        thisOne.CurrentHP = -1;
                        continue;
                    }

                    if (GraceTimeStart + 5000 < Helper.Time() && thisOne.TimeLastAttacked + (1000 * thisOne.AttackSpeedSeconds) < Helper.Time() && thisOne.CurrentHP > 0) {
                        if ((thisOne.AggroLevel > player.GetCombatLevel() && thisOne.AlwaysAggro) || thisOne.AttackingPlayer) {
                            thisOne.TimeLastAttacked = Helper.Time();

                            int dmg = GoRogue.DiceNotation.Dice.Roll(thisOne.DamageDice);
                            if (dmg < 0)
                                dmg = 0;
                            int modified = dmg;

                            int hitChance = GameLoop.rand.Next(100);

                            bool safespotting = false;

                            if (player.Equipment.TryGetValue("Weapon", out ItemWrapper? weaponWrap) && weaponWrap.GetRef() is Item wep2) {
                                if (wep2.EquipSkill == "Ranged") {
                                    if (thisOne.DamageType == "Melee") {
                                        safespotting = true;
                                    }
                                }
                            }

                            if (player.IsMaging() && thisOne.DamageType == "Melee") {
                                safespotting = true;
                            }

                            if (thisOne.Inaccessible && thisOne.DamageType == "Melee") {
                                safespotting = true;
                            }

                            if (!safespotting) {
                                if (hitChance < 25 + (player.GetEffectiveSkillLevel("Defense") / 4.0)) {
                                    Log.AddMessage(new ColoredString(thisOne.Name + " tried to hit you but missed!", Color.Yellow, Color.Black));
                                } else {
                                    modified -= player.TotalArmorValue(thisOne.DamageType);

                                    if ((player.PrayerActive("Protect from Magic") && thisOne.DamageType == "Magic") || (player.PrayerActive("Protect from Melee") && thisOne.DamageType == "Melee") || (player.PrayerActive("Protect from Range") && thisOne.DamageType == "Ranged")) {
                                        modified /= 2;
                                    } 
                                         
                                    if (dmg != modified) {
                                        if (modified <= 0) {
                                            Log.AddMessage(new ColoredString(thisOne.Name + " hit you for " + dmg + ", but you took no damage!", Color.Crimson, Color.Black));
                                        } else {
                                            Log.AddMessage(new ColoredString(thisOne.Name + " hit you for " + dmg + ", reduced to " + modified + "!", Color.Crimson, Color.Black));
                                        }
                                    } else {
                                        Log.AddMessage(new ColoredString(thisOne.Name + " hit you for " + dmg + "!", Color.Crimson, Color.Black));
                                    }

                                    if (modified > 0) {
                                        if (thisOne.PoisonSeverity > 0) {
                                            int odds = thisOne.DamageType == "Melee" ? 4 : thisOne.DamageType == "Ranged" ? 8 : 1;

                                            if (GameLoop.rand.Next(odds) == 0 && player.PoisonStatus < thisOne.PoisonSeverity) {
                                                bool antipoison = false;
                                                foreach (var pcheck in player.ActivePotions) {
                                                    if (pcheck.Stat == "Antipoison" && pcheck.Change > 0) {
                                                        antipoison = true;
                                                    }
                                                }
                                                if (!antipoison) {
                                                    player.PoisonStatus = thisOne.PoisonSeverity;
                                                    Log.AddMessage("You have been poisoned!", Color.Lime);
                                                }
                                            }
                                        }

                                        if (player.Equipment.TryGetValue("Ring", out ItemWrapper? ring) && ring != null && ring.ID == "ringRecoil") {
                                            int reflect = (modified / 10) + 1;

                                            if (reflect > ring.Charges)
                                                reflect = ring.Charges;

                                            ring.Charges -= reflect; 
                                            Log.AddMessage(new ColoredString("Your ring of recoil reflected " + reflect + " damage back!", Color.DeepSkyBlue, Color.Black));

                                            if (ring.Charges <= 0) { 
                                                Log.AddMessage(new ColoredString("Your ring of recoil runs out of charge and shatters.", Color.Crimson, Color.Black));
                                                player.Equipment.Remove("Ring");
                                            }
                                        }
                                    }

                                    bool died = modified > 0 ? player.TakeDamage(modified, Log) : false; 

                                    if (died) {
                                        AttackingMonster = null;
                                        return printY; 
                                    }
                                }
                            }
                        }
                    }

                    double attackSpeed = 1.0;

                    if (player.Equipment.TryGetValue("Weapon", out ItemWrapper? wepWrap) && wepWrap.GetRef() is Item wep) { 
                        attackSpeed = wep.AttackSpeed;
                    } 

                    bool hasAmmo = true;
                    bool usedAmmo = false;

                    if (LastHitTime + (1000 * attackSpeed) < Helper.Time() && AttackingMonster != null && player.GetEffectiveSkillLevel("Slayer") < AttackingMonster.SlayerReq) { 
                        Log.AddMessage(new ColoredString("You need " + AttackingMonster.SlayerReq + " Slayer to damage this enemy.", Color.Crimson, Color.Black)); 
                        LastHitTime = Helper.Time();
                    }

                    if (AttackingMonster != null && LastHitTime + (1000 * attackSpeed) < Helper.Time() && AttackingMonster.CurrentHP > 0 && player.GetEffectiveSkillLevel("Slayer") >= AttackingMonster.SlayerReq) {
                        LastHitTime = Helper.Time();
                        AttackingMonster.AttackingPlayer = true;

                        reachedKillLimit = player.KillLimit == 0 ? true : false;
                        if (player.CollectionLog.ContainsKey(AttackingMonster.ID)) {
                            reachedKillLimit = player.KillLimit != -1 && player.CollectionLog[AttackingMonster.ID].KillCount >= player.KillLimit;
                        }

                        int hitChance = GameLoop.rand.Next(100); 
                             
                        // Remove a unit of ammo if this is a ranged weapon
                        if (!player.IsMaging()) {
                            if (wepWrap != null && wepWrap.GetRef() is Item weapon) {
                                if (weapon.EquipAmmo == "Self") {
                                    wepWrap.Quantity -= 1;
                                    Item droppedAmmo = new(weapon);
                                    droppedAmmo.Quantity = 1;
                                    if (GameLoop.rand.Next(4) != 0)
                                        GameLoop.ZPO.TryPlaceItem(player.NavLoc, new(droppedAmmo, AttackingMonster.Inaccessible));
                                    usedAmmo = true;
                                } else if (weapon.EquipAmmo == "Chargebow") {
                                    usedAmmo = true;
                                } else if (weapon.EquipAmmo == "RangedStandard") {
                                    if (player.Equipment.TryGetValue("Ammo", out ItemWrapper? arrowWrap) && arrowWrap.GetRef() is Item arrow && arrow.EquipDamageType == "RangedStandard") {
                                        arrowWrap.Quantity -= 1;
                                        Item droppedAmmo = new(arrow);
                                        droppedAmmo.Quantity = 1;
                                        if (GameLoop.rand.Next(4) != 0)
                                            GameLoop.ZPO.TryPlaceItem(player.NavLoc, new(droppedAmmo, AttackingMonster.Inaccessible));
                                        usedAmmo = true;
                                    } else {
                                        hasAmmo = false;
                                    }
                                } else if (weapon.EquipAmmo == "RangedHeavy") {
                                    if (player.Equipment.TryGetValue("Ammo", out ItemWrapper? boltWrap) && boltWrap.GetRef() is Item bolt && bolt.EquipDamageType == "RangedHeavy") {
                                        boltWrap.Quantity -= 1;
                                        Item droppedAmmo = new(bolt);
                                        droppedAmmo.Quantity = 1;
                                        if (GameLoop.rand.Next(4) != 0)
                                            GameLoop.ZPO.TryPlaceItem(player.NavLoc, new(droppedAmmo, AttackingMonster.Inaccessible));
                                        usedAmmo = true;
                                    } else {
                                        hasAmmo = false;
                                    }
                                }
                            } 
                        }

                        if (hasAmmo) {
                            string whichSkill = player.IsMaging() ? "Magic" : usedAmmo ? "Ranged" : "Attack";

                            if (player.IsMaging()) {
                                if (player.HasAllItems(["tab" + player.CastingSpell])) { 
                                    player.ConsumeItems(["tab" + player.CastingSpell]);
                                } else {
                                    player.ConsumeItems(GameLoop.ZPO.SpellLibrary[player.CastingSpell].Runes, false, true);
                                }
                                    
                                int spellExp = GameLoop.ZPO.SpellLibrary[player.CastingSpell].ExpOnCast;
                                player.TryGrantExp("Magic", spellExp, Log, SidebarManager.RecentlyTrainedSkills);
                            }

                            double eqpAccuracy = 0; 
                            foreach (var kv in player.Equipment) {
                                if (kv.Value.GetRef() is Item eqp) {
                                    if (eqp.MiscString == "HitChance") {
                                        eqpAccuracy += eqp.EquipTier;
                                    }
                                }
                            }

                            bool efaritay = false;

                            if (player.Equipment.TryGetValue("Ring", out ItemWrapper? ring) && ring != null && ring.ID == "ringEfaritay" && AttackingMonster.SpecialCategory == "Vampiric") { 
                                ring.Charges -= 1; 
                                efaritay = true;
                                eqpAccuracy += 10;

                                if (ring.Charges <= 0) { 
                                    Log.AddMessage(new ColoredString("Your Efaritay's aid runs out of charge and shatters.", Color.Crimson, Color.Black));
                                    player.Equipment.Remove("Ring");
                                }
                            }

                            if (AttackingMonster.Inaccessible && (player.GetDamageType() == "Crush" || player.GetDamageType() == "Stab" || player.GetDamageType() == "Slash")) {
                                Log.AddMessage(new ColoredString("You can't reach this enemy with melee attacks. Try ranged or magic instead.", Color.Crimson, Color.Black));
                            } else {
                                if (curr.Dark && !player.HasLightSource()) { 
                                    Log.AddMessage(new ColoredString("You can't see well enough to land an attack!", Color.Crimson, Color.Black));
                                    hitChance = 999;
                                }

                                if (hitChance > 25 + (player.GetEffectiveSkillLevel(whichSkill) / 2.0) + eqpAccuracy) {
                                    Log.AddMessage(new ColoredString("You tried to hit " + (AttackingMonster.ProperName ? "" : "the ") + AttackingMonster.Name + " but missed!", Color.Crimson, Color.Black));
                                } else {
                                    int pdmg = GoRogue.DiceNotation.Dice.Roll(player.GetDamageDice());
                                    bool crit = false;

                                    if (player.GetDamageType() == AttackingMonster.WeakType) {
                                        pdmg = (int)Math.Ceiling(pdmg * 1.5f);
                                    }
                                         
                                    bool eitherDamageSpecial = AttackingMonster.SpecialCategory != "" && (player.GetDamageType() == AttackingMonster.SpecialCategory || player.GetSecondaryDamageType() == AttackingMonster.SpecialCategory);

                                    if (eitherDamageSpecial) { // Mostly for Crumble Undead, but open to similar spells for Demons, Vamps, etc
                                        pdmg = (int)Math.Ceiling(pdmg * 2f);
                                    }

                                    if (efaritay) {
                                        pdmg = (int) Math.Ceiling(pdmg * 1.2f);
                                    }

                                    if (AttackingMonster.SpecialCategory == "Leaf" && !eitherDamageSpecial) {
                                        pdmg = 0;    
                                        Log.AddMessage(new ColoredString("You need a special kind of weapon to damage this enemy!", Color.Crimson, Color.Black));
                                    }

                                    if (player.GetDamageType() == "Undead" && AttackingMonster.SpecialCategory != "Undead") {
                                        pdmg = 0;
                                        Log.AddMessage(new ColoredString("That kind of attack only works against the " + player.GetDamageType().ToLower() + "!", Color.Crimson, Color.Black));
                                    } else { 
                                        int critTarget = 20;

                                        if (player.PrayerActive("Improved Critical I"))
                                            critTarget -= 1;
                                        if (player.PrayerActive("Improved Critical II"))
                                            critTarget -= 2;
                                        if (player.PrayerActive("Improved Critical III"))
                                            critTarget -= 3;

                                        int critRoll = GameLoop.rand.Next(20) + 1;

                                        if (critRoll >= critTarget) {
                                            crit = true;
                                            pdmg *= 2;
                                        }



                                        if (pdmg > AttackingMonster.CurrentHP)
                                            pdmg = AttackingMonster.CurrentHP;

                                        AttackingMonster.CurrentHP -= pdmg;
                                     
                                        Log.AddMessage(new ColoredString("You hit " + (AttackingMonster.ProperName ? "" : "the ") + AttackingMonster.Name + " for " + pdmg + "." + (crit ? " Critical Hit!" : ""), crit ? Color.Lime : Color.Green, Color.Black));
                                    }

                                    if (player.IsMaging()) {  
                                        player.TryGrantExp("Magic", (pdmg * 4), Log, SidebarManager.RecentlyTrainedSkills);

                                        string cast = player.CanCast(GameLoop.ZPO.SpellLibrary[player.CastingSpell]);
                                        if (cast != "") { 
                                            Log.AddMessage(new ColoredString("Cannot cast spell anymore: " + cast, Color.Crimson, Color.Black)); 
                                            player.CastingSpell = "";
                                        }
                                    } else if (usedAmmo) { 
                                        player.TryGrantExp("Ranged", pdmg * 4, Log, SidebarManager.RecentlyTrainedSkills);
                                    } else {
                                        if (player.OffenseExpSplit > 0 && !reachedKillLimit)
                                            player.TryGrantExp("Attack", pdmg * player.OffenseExpSplit, Log, SidebarManager.RecentlyTrainedSkills);

                                        if (player.OffenseExpSplit < 4 && !reachedKillLimit)
                                            player.TryGrantExp("Strength", pdmg * (4 - player.OffenseExpSplit), Log, SidebarManager.RecentlyTrainedSkills);
                                    }

                                    if (player.DefenseExpSplit > 0 && !reachedKillLimit)
                                        player.TryGrantExp("Defense", pdmg * player.DefenseExpSplit, Log, SidebarManager.RecentlyTrainedSkills);

                                    if (player.DefenseExpSplit < 4 && !reachedKillLimit)
                                        player.TryGrantExp("Constitution", pdmg * (4 - player.DefenseExpSplit), Log, SidebarManager.RecentlyTrainedSkills);

                                    if (AttackingMonster.ID == player.SlayerTask|| AttackingMonster.CountsAsSlayer.Contains(player.SlayerTask)) {
                                        player.TryGrantExp("Slayer", pdmg * 4, Log, SidebarManager.RecentlyTrainedSkills);
                                    }  

                                    if (AttackingMonster.CurrentHP <= 0) {
                                        bool canKill = true;
                                        if (AttackingMonster.KillItem != "") {
                                            if (player.HasAllItems([AttackingMonster.KillItem +",1"], false, true)) {
                                                player.ConsumeItems([AttackingMonster.KillItem + "," + AttackingMonster.KillItemCount], false, true);
                                            } else {
                                                canKill = false;
                                                AttackingMonster.CurrentHP = 0;
                                                Log.AddMessage(new ColoredString("You need " + GameLoop.ZPO.ResolveItemName(AttackingMonster.KillItem) + " to finish this enemy off.", Color.Crimson, Color.Black)); 
                                            }
                                        }

                                        if (canKill) {
                                            AttackingMonster.TimeLastKilled = Helper.Time();
                                            AttackingMonster.AttackingPlayer = false;

                                            foreach (var quest in GameLoop.ZPO.QuestLibrary) {
                                                if (player.QuestLog.TryGetValue(quest.Key, out QuestStatus? status)) {
                                                    if (status.CurrentStage != quest.Value.CompleteStage && quest.Value.Stages.ContainsKey(status.CurrentStage)) {
                                                        if (quest.Value.Stages[status.CurrentStage].ProgressType == "Kill" && quest.Value.Stages[status.CurrentStage].MiscString == AttackingMonster.ID) {
                                                            status.KillTracker++;

                                                            if (status.KillTracker >= quest.Value.Stages[status.CurrentStage].MiscInt) {
                                                                status.CurrentStage = quest.Value.Stages[status.CurrentStage].LeadsToStage;
                                                                status.KillTracker = 0; 

                                                                if (status.CurrentStage == quest.Value.CompleteStage) {
                                                                    Log.AddMessage(new ColoredString("You have completed " + quest.Value.Name + "!", Color.Lime, Color.Black));
                                                                    quest.Value.ProcessRewards(player);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            player.ProgressSlayerTask(AttackingMonster);

                                            if (!player.CollectionLog.ContainsKey(AttackingMonster.ID))
                                                player.CollectionLog.Add(AttackingMonster.ID, new(AttackingMonster.ID));

                                            player.CollectionLog[AttackingMonster.ID].KillCount += 1;

                                            if (player.KillLimit != -1 && player.CollectionLog[AttackingMonster.ID].KillCount == player.KillLimit) {
                                                Log.AddMessage(new ColoredString("You've killed " + player.KillLimit + " " + AttackingMonster.Name + "s and will no longer receive drops or exp from them."));
                                            }

                                            if (AttackingMonster.DropTable != null && AttackingMonster.DropTable.Count > 0) {
                                                for (int j = 0; j < AttackingMonster.DropTable.Count; j++) {
                                                    ItemDrop drop = AttackingMonster.DropTable[j];

                                                    drop.RollDrop(player, player.CollectionLog[AttackingMonster.ID], AttackingMonster.Inaccessible);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        } else { 
                            Log.AddMessage(new ColoredString("You haven't got any valid ammo for your weapon!", Color.Crimson, Color.Black));
                        }
                    }

                    if (thisOne.CurrentHP <= 0 && thisOne.TimeLastKilled + (thisOne.RespawnTime * 1000) < Helper.Time()) {
                        thisOne.CurrentHP = thisOne.MaxHP; 
                        thisOne.TimeLastAttacked = Helper.Time();
                        thisOne.AttackingPlayer = false;
                    } 
                    
                    if (printY < 35) {
                        mini.Con.Print(57, printY, "|");

                        if (GameLoop.ZPO.SpellLibrary.TryGetValue("utilMonsterInspect", out Spell? inspect) && inspect != null) { 
                            if (player.CanCast(inspect) == "") {
                                mini.Con.PrintClickable(57, printY, new ColoredString("?", Color.MediumPurple, Color.Black), () => {
                                    Log.AddMessage("You cast monster inspect on the " + curr.MonstersHere[i].Name + ".", Color.MediumPurple);
                                    Log.AddMessage(curr.MonstersHere[i].GetSummary(), Color.Yellow);  
                                    inspect.Cast(player, Log, SidebarManager.RecentlyTrainedSkills);  
                                });  
                            }
                        } 
                     
                        mini.Con.PrintClickable(59, printY, new ColoredString(curr.MonstersHere[i].Name, nameCol, Color.Black), () => { 
                            if (curr.Dark && !player.HasLightSource()) { 
                                Log.AddMessage("It may be a bad idea to aggravate a monster you can't currently see to hit.");
                            } else {
                                AttackingMonster = thisOne; 
                                LastClickTime = Helper.Time();
                            }
                        }); 
                        mini.Con.Print(90, printY, ("(" + curr.MonstersHere[i].CurrentHP + "/" + curr.MonstersHere[i].MaxHP + " hp)").Align(HorizontalAlignment.Right, 15));
                        mini.Con.Print(80, printY, ("[Lv " + curr.MonstersHere[i].Level.ToString().Align(HorizontalAlignment.Right, 4) + "]"));

                        mini.Con.PrintClickable(106, printY++, "Log", () => {
                            ExtraWindows.CollectionLog.IsVisible = true;
                            ExtraWindows.CollectionID = curr.MonstersHere[i].ID;
                            ExtraWindows.CollectionDropTop = 0;
                            ExtraWindows.CollectionCat = "Monster";
                        });
                    }

                    if (usedAmmo) {
                        List<string> empties = new();
                        foreach (var kv in player.Equipment) {
                            if (kv.Value.Quantity <= 0) {
                                empties.Add(kv.Key);
                            }
                        }

                        foreach (var rem in empties) {
                            player.Equipment.Remove(rem);
                        }
                    }

                    if (thisOne.CurrentHP <= 0 && thisOne.Temporary) {
                        curr.MonstersHere.Remove(thisOne);
                        break;
                    }
                }
            } 


            return printY;
        }
    }
}
