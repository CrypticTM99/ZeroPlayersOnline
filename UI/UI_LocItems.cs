using SadConsole.Input; 
using ZeroPlayersOnline.DataTypes;
using ZeroPlayersOnline.Managers;
using Key = SadConsole.Input.Keys;

namespace ZeroPlayersOnline.UI {
    public static class UI_LocItems {  
        public static Rectangle ActivityRect = new Rectangle(new Point(111, 10), new Point(147, 34));
        public static int ActivityItemTop = 0; 
        public static bool WithdrawingNotes = false;

        public static void DrawItems(Location curr, Player player, UI_EmbeddedMini mini, int resourceX, int resourceY, MessageLog Log) { 
            Point mousePos = new MouseScreenObjectState(mini.Con, GameHost.Instance.Mouse).CellPosition;

            if (curr.IsBank && player.CanUseBanks) {
                mini.Con.Print(resourceX + 2, resourceY, "Items in Bank"); 

                mini.Con.PrintClickable(resourceX + 23, resourceY, "(sort)", () => { player.BankedItems = player.BankedItems.OrderBy(o => GameLoop.ZPO.ResolveItemName(o.ID)).ToList(); });
                         
                mini.Con.PrintClickable(resourceX + 30, resourceY++, new ColoredString("(noted)", WithdrawingNotes ? Color.Lime : Color.DarkSlateGray, Color.Black), () => { WithdrawingNotes = !WithdrawingNotes; }); 

                ActivityItemTop = Math.Clamp(ActivityItemTop, 0, player.BankedItems.Count);

                if (ActivityRect.Contains(mousePos)) {
                    int qty = 1;
                    if (Helper.EitherShift()) {
                        qty *= 5;
                    }
                    if (Helper.EitherControl()) {
                        qty *= 10;
                    }

                    if (player.BankedItems.Count > 19) {
                        if (Helper.ScrolledUp()) { ActivityItemTop = Math.Clamp(ActivityItemTop - qty, 0, player.BankedItems.Count - 19); }
                        if (Helper.ScrolledDown()) { ActivityItemTop = Math.Clamp(ActivityItemTop + qty, 0, player.BankedItems.Count - 19); }
                    } else {
                        ActivityItemTop = 0;
                    }
                }

                if (player.BankedItems.Count > 0) {
                    for (int i = ActivityItemTop; i < player.BankedItems.Count && resourceY < 35; i++) { 
                        if (player.BankedItems[i].GetRef() is Item item) { 
                            string name = item.Name;
                            if (name.Length > 25)
                                name = name[..25];

                            name += item.Quantity > 1 ? " x" + item.Quantity : "";

                            if (item.Noted) {
                                name += " (n)";
                            }

                            if (item.Name.Contains("potion") && item.UseInt4 != 0) {
                                name += " (" + item.UseInt4 + " doses)";
                            }

                            bool picked = false;

                            mini.Con.Print(resourceX + 2, resourceY, "|");
                            mini.Con.PrintClickable(resourceX + 4, resourceY, new ColoredString(name, item.GetColor(), item.ColorSum() < 50 ? Color.White : Color.Black), () => { 
                                int qty = 1;

                                if (Helper.EitherShift())
                                    qty *= 5;
                                if (Helper.EitherControl())
                                    qty *= 10;

                                if (qty >= item.Quantity || Helper.EitherAlt())
                                    qty = item.Quantity;


                                if (player.TryPickup(player.BankedItems[i], qty, item.Noteable ? WithdrawingNotes : false, fromGround: true)) { 
                                    if (player.BankedItems[i].Quantity <= 0) {
                                        player.BankedItems.RemoveAt(i); 
                                        picked = true;
                                    }
                                }
                            }); 

                            if (picked)
                                break;

                            mini.Con.PrintClickable(147, resourceY, new ColoredString("X", Color.Crimson, Color.Black), () => { 
                                if (Helper.KeyDown(Key.Space)) { 
                                    player.BankedItems.RemoveAt(i); picked = true; 
                                } else {
                                    Log.AddMessage("This button permanently deletes the item from your bank. If you wish to do this, hold SPACE while clicking the X.", Color.Crimson);
                                }
                            });

                            resourceY++;

                            if (picked)
                                break;
                        }
                    }
                }
                else {
                    mini.Con.Print(resourceX + 2, resourceY, "|");
                    mini.Con.Print(resourceX + 4, resourceY++, "(no items banked)", Color.DarkSlateGray);
                }
            }
            else {
                mini.Con.Print(resourceX + 2, resourceY++, "Items on Ground Here"); 
                ActivityItemTop = Math.Clamp(ActivityItemTop, 0, curr.ItemsHere.Count);

                if (ActivityRect.Contains(mousePos)) {
                    int qty = 1;
                    if (Helper.EitherShift()) {
                        qty *= 5;
                    }
                    if (Helper.EitherControl()) {
                        qty *= 10;
                    }

                    if (curr.ItemsHere.Count > 19) {
                        if (Helper.ScrolledUp()) { ActivityItemTop = Math.Clamp(ActivityItemTop - qty, 0, curr.ItemsHere.Count - 19); }
                        if (Helper.ScrolledDown()) { ActivityItemTop = Math.Clamp(ActivityItemTop + qty, 0, curr.ItemsHere.Count - 19); }
                    } else {
                        ActivityItemTop = 0;
                    }
                }

                if (curr.ItemsHere.Count > 0) {
                    for (int i = ActivityItemTop; i < curr.ItemsHere.Count && resourceY < 35; i++) { 
                        Item item = curr.ItemsHere[i];

                        string name = item.Name;
                        if (name.Length > 25)
                            name = name[..25];

                        name = name + (item.Quantity > 1 ? " x" + item.Quantity : "");

                        if (item.Noted) {
                            name += " (n)";
                        }

                        if (item.Name.Contains("potion") && item.UseInt4 != 0) {
                            name += " (" + item.UseInt4 + " doses)";
                        }

                        bool picked = false;
                                 
                        mini.Con.Print(resourceX + 2, resourceY, (item.Inaccessible ? "X" : "|"));

                        if (GameLoop.ZPO.SpellLibrary.TryGetValue("utilTelegrab", out Spell? telegrab) && telegrab != null) { 
                            if (player.CanCast(telegrab) == "") {
                                mini.Con.PrintClickable(resourceX + 2, resourceY, new ColoredString("~", Color.MediumPurple, Color.Black), () => { 
                                    int qty = 1;

                                    if (Helper.EitherShift())
                                        qty *= 5;
                                    if (Helper.EitherControl())
                                        qty *= 10;

                                    if (qty >= item.Quantity || Helper.EitherAlt())
                                        qty = item.Quantity;
                                                 
                                    if (player.TryPickup(item, qty, item.Noted, fromGround: true)) {  
                                        if (item.Quantity <= 0) {
                                            curr.ItemsHere.RemoveAt(i); 
                                            picked = true;

                                            for (int ground = 0; ground < curr.ItemSpawns.Count; ground++) {
                                                if (curr.ItemSpawns[ground].ItemID == item.ID) {
                                                    curr.ItemSpawns[ground].LastPickedUp = Helper.Time();
                                                }
                                            }
                                        }

                                        Log.AddMessage("You cast telekinetic grab and pick up the " + item.Name + ".", Color.MediumPurple); 
                                        telegrab.Cast(player, Log, SidebarManager.RecentlyTrainedSkills); 
                                    }  
                                }); 

                                if (picked)
                                    break; 
                            }
                        }

                        mini.Con.PrintClickable(resourceX + 4, resourceY, new ColoredString(name, item.GetColor(), item.ColorSum() < 50 ? Color.White : Color.Black), () => { 
                            int qty = 1;

                            if (Helper.EitherShift())
                                qty *= 5;
                            if (Helper.EitherControl())
                                qty *= 10;

                            if (qty >= item.Quantity || Helper.EitherAlt())
                                qty = item.Quantity;

                            if (item.Inaccessible) {
                                Log.AddMessage("That item is out of reach! You could maybe pick it up with Telekinetic Grab though.", Color.Crimson);
                            } else {
                                if (player.TryPickup(item, qty, item.Noted, fromGround: true)) {  
                                    if (item.Quantity <= 0) {
                                        curr.ItemsHere.RemoveAt(i); 
                                        picked = true;

                                        for (int ground = 0; ground < curr.ItemSpawns.Count; ground++) {
                                            if (curr.ItemSpawns[ground].ItemID == item.ID) {
                                                curr.ItemSpawns[ground].LastPickedUp = Helper.Time();
                                            }
                                        }
                                    }
                                } 
                            }
                        });
                        mini.Con.PrintClickable(147, resourceY, new ColoredString("X", Color.Crimson, Color.Black), () => { curr.ItemsHere.RemoveAt(i); picked = true; });

                        resourceY++;

                        if (picked)
                            break;
                    }
                }
                else {
                    mini.Con.Print(resourceX + 2, resourceY, "|");
                    mini.Con.Print(resourceX + 4, resourceY++, "(no items here)", Color.DarkSlateGray);
                }
            }
        }
    }
}
