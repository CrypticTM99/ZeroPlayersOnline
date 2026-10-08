using System;
using System.Collections.Generic;
using System.Text;
using ZeroPlayersOnline.DataTypes;
using ZeroPlayersOnline.Managers;
using static SadConsole.UI.Controls.Table.TableCells.Layout;

namespace ZeroPlayersOnline.UI {
    public static class UI_LocResources {  

        public static void DrawResources(Location curr, Player player, UI_EmbeddedMini mini, int resourceX, int resourceY, MessageLog Log, PlayerOwnedHome? home) { 
            mini.Con.Print(resourceX + 2, resourceY++, "Resource Nodes Here");
            int startY = resourceY;

            if (curr.LocalGathers.Where(o => (o.CanGather(player) == "" && !o.ShowAnyways) || o.ShowAnyways).ToList().Count > 0) {
                for (int i = 0; i < curr.LocalGathers.Count; i++) {
                    GatheringTile tile = curr.LocalGathers[i];
                    mini.Con.Print(resourceX + 2, resourceY, "|");

                    Color interact = Color.White;

                    if (tile.LastGathered + (tile.RestockTime * 1000) < Helper.Time() || tile.LastGathered == 0) {
                        if (tile.CanGather(player) == "") {
                            interact = Color.Green;
                        }
                        else {
                            interact = Color.Crimson;
                        }

                        mini.Con.PrintClickable(resourceX + 4, resourceY++, new ColoredString(tile.InteractVerb + " " + tile.Name, interact, Color.Black), () => { tile.Gather(player, Log, GameLoop.ZPO.ItemLibrary, curr, SidebarManager.RecentlyTrainedSkills); });
                    }
                    else {
                        int secondsToRestock = (int)Math.Floor((tile.LastGathered + (tile.RestockTime * 1000) - Helper.Time()) / 1000f);
                        mini.Con.Print(resourceX + 4, resourceY++, tile.InteractVerb + " " + tile.Name + " [" + secondsToRestock + "]");
                    }
                }
            }

            if (player.CurrentStarLoc != "" && player.CurrentStarLoc == player.NavLoc) { 
                mini.Con.Print(resourceX + 2, resourceY, "|");
                
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

                Color interact = (player.GetEffectiveSkillLevel("Mining") >= currLevel && hasPick) ? Color.Green : Color.Crimson;

                mini.Con.PrintClickable(resourceX + 4, resourceY++, new ColoredString("Mine Crashed Star", interact, Color.Black), () => {
                    if (player.GetEffectiveSkillLevel("Mining") >= currLevel && hasPick) {
                        player.MiningStar = true;
                        GameLoop.ZPO.Log.AddMessage("You start mining the crashed star.", Color.Lime);
                    } else {
                        if (hasPick) { // Then failed because of mining level
                            GameLoop.ZPO.Log.AddMessage("You need a mining level of " + currLevel + " to mine this layer of the crashed star.",Color.Crimson);
                        } else {
                            GameLoop.ZPO.Log.AddMessage("You need a pickaxe to start mining the crashed star.", Color.Crimson);
                        }
                    }
                });
            }

            if (home != null) {
                foreach (var obj in home.Rooms[MinigameManager.POH_ViewingRoom].BuiltObjects) {
                    if (GameLoop.ZPO.POH_ObjectLib.TryGetValue(obj, out POH_Object? actual) && actual != null) {
                        if (actual.AddsGather != "") { 
                            if (GameLoop.ZPO.GatherSpots.TryGetValue(actual.AddsGather, out GatheringTile? tile) && tile != null) { 
                                mini.Con.Print(resourceX + 2, resourceY, "|");

                                Color interact = Color.White;

                                if (tile.LastGathered + (tile.RestockTime * 1000) < Helper.Time() || tile.LastGathered == 0) {
                                    if (tile.CanGather(player) == "") {
                                        interact = Color.Green;
                                    }
                                    else {
                                        interact = Color.Crimson;
                                    }

                                    mini.Con.PrintClickable(resourceX + 4, resourceY++, new ColoredString(tile.InteractVerb + " " + tile.Name, interact, Color.Black), () => { tile.Gather(player, Log, GameLoop.ZPO.ItemLibrary, curr, SidebarManager.RecentlyTrainedSkills); });
                                }
                                else {
                                    int secondsToRestock = (int)Math.Floor((tile.LastGathered + (tile.RestockTime * 1000) - Helper.Time()) / 1000f);
                                    mini.Con.Print(resourceX + 4, resourceY++, tile.InteractVerb + " " + tile.Name + " [" + secondsToRestock + "]");
                                }
                            } else { 
                                mini.Con.Print(resourceX + 2, resourceY++, "|" + actual.AddsGather, Color.DarkSlateGray);
                            } 
                        }
                    }
                }
            }

            if (startY == resourceY) {
                mini.Con.Print(resourceX + 2, resourceY, "|");
                mini.Con.Print(resourceX + 4, resourceY++, "(no resources here)", Color.DarkSlateGray);
            } 
        }
    }
}
