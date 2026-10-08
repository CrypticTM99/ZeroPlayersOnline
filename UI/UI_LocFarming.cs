using SadConsole.Input; 
using ZeroPlayersOnline.DataTypes;
using ZeroPlayersOnline.Managers;
using Key = SadConsole.Input.Keys;

namespace ZeroPlayersOnline.UI {
    public static class UI_LocFarming {  
        public static Rectangle ActivityRect = new Rectangle(new Point(111, 10), new Point(147, 34));
        public static int ActivityItemTop = 0; 
        public static bool WithdrawingNotes = false;

        public static void DrawFarming(Location curr, Player player, UI_EmbeddedMini mini, int resourceX, int resourceY, MessageLog Log, PlayerOwnedHome? home) { 
            Point mousePos = new MouseScreenObjectState(mini.Con, GameHost.Instance.Mouse).CellPosition;

            mini.Con.Print(resourceX + 2, resourceY++, "Farming Patches Here"); 
            int startY = resourceY;
            for (int i = 0; i < curr.FarmingPatchesHere.Count; i++) {
                if (player.FarmingPatches.ContainsKey(curr.FarmingPatchesHere[i])) {
                    FarmingPatch patch = player.FarmingPatches[curr.FarmingPatchesHere[i]];

                    mini.Con.Print(resourceX + 2, resourceY, "|");

                    if (patch.SeedPlanted == "") { 
                        mini.Con.Print(resourceX + 4, resourceY, "(Empty " + patch.PatchType + " Patch)");
                    } else {
                        if (patch.Regrowing) {
                            if (patch.Regrown > 0) {
                                mini.Con.PrintClickable(resourceX + 4, resourceY, new ColoredString(GameLoop.ZPO.ResolveItemName(patch.SeedPlanted) + " [" + patch.RegrowTimeLeft / player.FarmGrowthIncrement + "]", Color.Lime, Color.Black), () => {
                                    Item? seed = GameLoop.ZPO.ResolveItem(patch.SeedPlanted);
                                    if (seed != null) {
                                        Item? temp = GameLoop.ZPO.ResolveItem(seed.UseString3);
                                        Item? output = null;
                                        if (temp != null)
                                            output = new(temp);

                                        if (output != null) {
                                            int qty = 1 + patch.Compost;
                                            patch.Regrown--;

                                            if (output.Stackable) {
                                                output.Quantity = qty; 
                                                player.TryGrantExp("Farming", (seed.UseInt2 / 10) * qty, Log, SidebarManager.RecentlyTrainedSkills);
                                                if (patch.PatchType == "Tree") { 
                                                    player.TryGrantExp("Woodcutting", (seed.UseInt2 / 4) * qty, Log, SidebarManager.RecentlyTrainedSkills); 
                                                }
                                                if (!player.TryPickup(output, output.Quantity)) {
                                                    Log.AddMessage(new ColoredString("Your inventory is full, so the " + output.Name + "s fall to the ground." , Color.Crimson, Color.Black));
                                                }
                                            } else {
                                                for (int i = 0; i < qty; i++) {
                                                    player.TryGrantExp("Farming", seed.UseInt2 / 10, Log, SidebarManager.RecentlyTrainedSkills);
                                                    if (patch.PatchType == "Tree") { 
                                                        player.TryGrantExp("Woodcutting", (seed.UseInt2 / 4), Log, SidebarManager.RecentlyTrainedSkills); 
                                                    }

                                                    Item clone = new(output);
                                                    clone.Quantity = 1;

                                                    output.Quantity--;

                                                    if (!player.TryPickup(clone, 1)) {
                                                        Log.AddMessage(new ColoredString("Your inventory is full, so the " + output.Name + " falls to the ground.", Color.Crimson, Color.Black)); 
                                                    }
                                                }
                                            }
                                        } else {
                                            Log.AddMessage(new ColoredString("The harvested item crumbles away in your hands, this should be reported as a bug.", Color.Crimson, Color.Black));
                                        }
                                    } else { 
                                        Log.AddMessage(new ColoredString("The harvested seed crumbles away in your hands, this should be reported as a bug.", Color.Crimson, Color.Black));
                                    }
                                });
                            } else {
                                mini.Con.Print(resourceX + 4, resourceY, GameLoop.ZPO.ResolveItemName(patch.SeedPlanted) + " [" + (patch.RegrowTimeLeft / player.FarmGrowthIncrement) + "]");
                            }
                        } else {
                            if (patch.TimeLeft <= 0) {
                                patch.TimeLeft = 0;
                                mini.Con.PrintClickable(resourceX + 4, resourceY, new ColoredString(GameLoop.ZPO.ResolveItemName(patch.SeedPlanted) + " [" + patch.TimeLeft + "]", Color.Lime, Color.Black), () => {
                                    Item? seed = GameLoop.ZPO.ResolveItem(patch.SeedPlanted);
                                    if (seed != null) {
                                        Item? temp = Helper.Clone(GameLoop.ZPO.ResolveItem(seed.UseString3));
                                        Item? output = null;
                                        if (temp != null)
                                            output = new(temp);
                                        if (output != null) {
                                            int qty = 5 + (int) Math.Floor((player.Skills["Farming"].Level - seed.UseInt) / 5.0) + patch.Compost;

                                            if (output.Stackable) {
                                                output.Quantity = qty; 
                                                player.TryGrantExp("Farming", seed.UseInt2 * qty, Log, SidebarManager.RecentlyTrainedSkills);

                                                if (patch.PatchType == "Tree") { 
                                                    player.TryGrantExp("Woodcutting", (seed.UseInt2 / 4) * qty, Log, SidebarManager.RecentlyTrainedSkills); 
                                                }

                                                if (!player.TryPickup(output, output.Quantity)) {
                                                    Log.AddMessage(new ColoredString("Your inventory is full, so the " + output.Name + "s fall to the ground." , Color.Crimson, Color.Black));
                                                }
                                            } else {
                                                for (int i = 0; i < qty; i++) {
                                                    player.TryGrantExp("Farming", seed.UseInt2, Log, SidebarManager.RecentlyTrainedSkills);

                                                    if (patch.PatchType == "Tree") { 
                                                        player.TryGrantExp("Woodcutting", seed.UseInt2 / 4, Log, SidebarManager.RecentlyTrainedSkills); 
                                                    }

                                                    Item clone = new(output);
                                                    clone.Quantity = 1;

                                                    output.Quantity--;

                                                    if (!player.TryPickup(clone, 1)) {
                                                        Log.AddMessage(new ColoredString("Your inventory is full, so the " + output.Name + " falls to the ground.", Color.Crimson, Color.Black)); 
                                                    }
                                                }
                                            }
                                        } else {
                                            Log.AddMessage(new ColoredString("The harvested item crumbles away in your hands, this should be reported as a bug.", Color.Crimson, Color.Black));
                                        }
                                    } else { 
                                        Log.AddMessage(new ColoredString("The harvested seed crumbles away in your hands, this should be reported as a bug.", Color.Crimson, Color.Black));
                                    } 
                                                 
                                    if (patch.RegrowTime > 0) {
                                        patch.Regrowing = true;
                                        patch.RegrowTimeLeft = patch.RegrowTime;
                                    } else { 
                                        patch.ClearPatch();
                                    }
                                });
                            } else {
                                mini.Con.Print(resourceX + 4, resourceY, GameLoop.ZPO.ResolveItemName(patch.SeedPlanted) + " [" + (patch.TimeLeft / player.FarmGrowthIncrement) + "]");
                            }
                        }
                        mini.Con.PrintClickable(resourceX + 2, resourceY, new ColoredString("X", Color.Crimson, Color.Black), () => { patch.ClearPatch(); });

                    } 
                                 

                    resourceY++;
                } else {
                    mini.Con.Print(resourceX + 2, resourceY, "|");
                    mini.Con.Print(resourceX + 4, resourceY++, curr.FarmingPatchesHere[i], Color.DarkSlateGray);
                }
            }

            if (home != null) {
                FarmingPatch patch = home.Rooms[MinigameManager.POH_ViewingRoom].Patch;

                mini.Con.Print(resourceX + 2, resourceY, "|");

                if (patch.PatchType != "") {
                    if (patch.SeedPlanted == "") { 
                        mini.Con.Print(resourceX + 4, resourceY, "(Empty " + patch.PatchType + " Patch)");
                    } else {
                        if (patch.Regrowing) {
                            if (patch.Regrown > 0) {
                                mini.Con.PrintClickable(resourceX + 4, resourceY, new ColoredString(GameLoop.ZPO.ResolveItemName(patch.SeedPlanted) + " [" + patch.RegrowTimeLeft / player.FarmGrowthIncrement + "]", Color.Lime, Color.Black), () => {
                                    Item? seed = GameLoop.ZPO.ResolveItem(patch.SeedPlanted);
                                    if (seed != null) {
                                        Item? temp = GameLoop.ZPO.ResolveItem(seed.UseString3);
                                        Item? output = null;
                                        if (temp != null)
                                            output = new(temp);

                                        if (output != null) {
                                            int qty = 1 + patch.Compost;
                                            patch.Regrown--;

                                            if (output.Stackable) {
                                                output.Quantity = qty; 
                                                player.TryGrantExp("Farming", (seed.UseInt2 / 10) * qty, Log, SidebarManager.RecentlyTrainedSkills);
                                                if (patch.PatchType == "Tree") { 
                                                    player.TryGrantExp("Woodcutting", (seed.UseInt2 / 4) * qty, Log, SidebarManager.RecentlyTrainedSkills); 
                                                }
                                                if (!player.TryPickup(output, output.Quantity)) {
                                                    Log.AddMessage(new ColoredString("Your inventory is full, so the " + output.Name + "s fall to the ground." , Color.Crimson, Color.Black));
                                                }
                                            } else {
                                                for (int i = 0; i < qty; i++) {
                                                    player.TryGrantExp("Farming", seed.UseInt2 / 10, Log, SidebarManager.RecentlyTrainedSkills);
                                                    if (patch.PatchType == "Tree") { 
                                                        player.TryGrantExp("Woodcutting", (seed.UseInt2 / 4), Log, SidebarManager.RecentlyTrainedSkills); 
                                                    }

                                                    Item clone = new(output);
                                                    clone.Quantity = 1;

                                                    output.Quantity--;

                                                    if (!player.TryPickup(clone, 1)) {
                                                        Log.AddMessage(new ColoredString("Your inventory is full, so the " + output.Name + " falls to the ground.", Color.Crimson, Color.Black)); 
                                                    }
                                                }
                                            }
                                        } else {
                                            Log.AddMessage(new ColoredString("The harvested item crumbles away in your hands, this should be reported as a bug.", Color.Crimson, Color.Black));
                                        }
                                    } else { 
                                        Log.AddMessage(new ColoredString("The harvested seed crumbles away in your hands, this should be reported as a bug.", Color.Crimson, Color.Black));
                                    }
                                });
                            } else {
                                mini.Con.Print(resourceX + 4, resourceY, GameLoop.ZPO.ResolveItemName(patch.SeedPlanted) + " [" + (patch.RegrowTimeLeft / player.FarmGrowthIncrement) + "]");
                            }
                        } else {
                            if (patch.TimeLeft <= 0) {
                                patch.TimeLeft = 0;
                                mini.Con.PrintClickable(resourceX + 4, resourceY, new ColoredString(GameLoop.ZPO.ResolveItemName(patch.SeedPlanted) + " [" + patch.TimeLeft + "]", Color.Lime, Color.Black), () => {
                                    Item? seed = GameLoop.ZPO.ResolveItem(patch.SeedPlanted);
                                    if (seed != null) {
                                        Item? temp = Helper.Clone(GameLoop.ZPO.ResolveItem(seed.UseString3));
                                        Item? output = null;
                                        if (temp != null)
                                            output = new(temp);
                                        if (output != null) {
                                            int qty = 5 + (int) Math.Floor((player.Skills["Farming"].Level - seed.UseInt) / 5.0) + patch.Compost;

                                            if (output.Stackable) {
                                                output.Quantity = qty; 
                                                player.TryGrantExp("Farming", seed.UseInt2 * qty, Log, SidebarManager.RecentlyTrainedSkills);

                                                if (patch.PatchType == "Tree") { 
                                                    player.TryGrantExp("Woodcutting", (seed.UseInt2 / 4) * qty, Log, SidebarManager.RecentlyTrainedSkills); 
                                                }

                                                if (!player.TryPickup(output, output.Quantity)) {
                                                    Log.AddMessage(new ColoredString("Your inventory is full, so the " + output.Name + "s fall to the ground." , Color.Crimson, Color.Black));
                                                }
                                            } else {
                                                for (int i = 0; i < qty; i++) {
                                                    player.TryGrantExp("Farming", seed.UseInt2, Log, SidebarManager.RecentlyTrainedSkills);

                                                    if (patch.PatchType == "Tree") { 
                                                        player.TryGrantExp("Woodcutting", seed.UseInt2 / 4, Log, SidebarManager.RecentlyTrainedSkills); 
                                                    }

                                                    Item clone = new(output);
                                                    clone.Quantity = 1;

                                                    output.Quantity--;

                                                    if (!player.TryPickup(clone, 1)) {
                                                        Log.AddMessage(new ColoredString("Your inventory is full, so the " + output.Name + " falls to the ground.", Color.Crimson, Color.Black)); 
                                                    }
                                                }
                                            }
                                        } else {
                                            Log.AddMessage(new ColoredString("The harvested item crumbles away in your hands, this should be reported as a bug.", Color.Crimson, Color.Black));
                                        }
                                    } else { 
                                        Log.AddMessage(new ColoredString("The harvested seed crumbles away in your hands, this should be reported as a bug.", Color.Crimson, Color.Black));
                                    } 
                                                 
                                    if (patch.RegrowTime > 0) {
                                        patch.Regrowing = true;
                                        patch.RegrowTimeLeft = patch.RegrowTime;
                                    } else { 
                                        patch.ClearPatch();
                                    }
                                });
                            } else {
                                mini.Con.Print(resourceX + 4, resourceY, GameLoop.ZPO.ResolveItemName(patch.SeedPlanted) + " [" + (patch.TimeLeft / player.FarmGrowthIncrement) + "]");
                            }
                        }
                        mini.Con.PrintClickable(resourceX + 2, resourceY, new ColoredString("X", Color.Crimson, Color.Black), () => { patch.ClearPatch(); });

                    } 
                }
                                 

                resourceY++;
            }

            if (resourceY == startY) {
                mini.Con.Print(resourceX + 2, resourceY, "|");
                mini.Con.Print(resourceX + 4, resourceY++, "(no farming patches here)", Color.DarkSlateGray);
            }
        }
    }
}
