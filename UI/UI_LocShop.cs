using SadConsole.Input; 
using ZeroPlayersOnline.DataTypes;
using ZeroPlayersOnline.Managers;
using Key = SadConsole.Input.Keys;

namespace ZeroPlayersOnline.UI {
    public static class UI_LocShop {  
        public static Rectangle ActivityRect = new Rectangle(new Point(111, 10), new Point(147, 34));
        public static int ActivityItemTop = 0; 
        public static bool WithdrawingNotes = false;

        public static void DrawShop(Location curr, Player player, UI_EmbeddedMini mini, int resourceX, int resourceY, MessageLog Log, PlayerOwnedHome? home) { 
            Point mousePos = new MouseScreenObjectState(mini.Con, GameHost.Instance.Mouse).CellPosition;

            mini.Con.Print(resourceX + 2, resourceY++, "Shop Items Here");
            int startY = resourceY;

            if (ActivityRect.Contains(mousePos)) {
                int scroll = 1;
                if (Helper.EitherShift()) {
                    scroll *= 5;
                }
                if (Helper.EitherControl()) {
                    scroll *= 10;
                }

                if (curr.ShopItemsHere.Count > 19) {
                    if (Helper.ScrolledUp()) { ActivityItemTop = Math.Clamp(ActivityItemTop - scroll, 0, curr.ShopItemsHere.Count - 19); }
                    if (Helper.ScrolledDown()) { ActivityItemTop = Math.Clamp(ActivityItemTop + scroll, 0, curr.ShopItemsHere.Count - 19); }
                } else {
                    ActivityItemTop = 0;
                }
            }

            for (int i = ActivityItemTop; i < curr.ShopItemsHere.Count && resourceY < 35; i++) {
                if (GameLoop.ZPO.ItemLibrary.ContainsKey(curr.ShopItemsHere[i])) {
                    Item shop = new(GameLoop.ZPO.ItemLibrary[curr.ShopItemsHere[i]]); 
                    mini.Con.Print(resourceX + 2, resourceY, "|");
                    int spaceAfterName = 33 - shop.GetName(1, 0, shop.UseInt4).Length;

                    int qty = 1;
                    if (Helper.EitherShift()) { qty *= 5; }
                    if (Helper.EitherControl()) { qty *= 10; }

                    int adjustedPrice = (int) (shop.Value * curr.ShopPriceMultiplier);

                    mini.Con.PrintClickable(resourceX + 4, resourceY, shop.GetNameCS(1, 0, shop.UseInt4) + new ColoredString(("(" + adjustedPrice + "gp)").Align(HorizontalAlignment.Right, spaceAfterName)), () => {
                        if (player.CanUseShops) {
                            int totalPrice =  adjustedPrice * qty;
                            if (player.GoldTotal() >= totalPrice) {
                                player.TakeGold(totalPrice); 

                                if (qty == 1)
                                    Log.AddMessage("You purchased a" + (Helper.VowelStart(shop.Name.ToLower()) ? "n " : " ") + shop.Name + " for " + shop.Value + " gp.", Color.Goldenrod);
                                else 
                                    Log.AddMessage("You purchased " + qty + "x " + shop.Name + " for " + totalPrice + " gp.", Color.Goldenrod);

                                if (!player.TryPickup(shop, qty, false, true)) {
                                    Log.AddMessage("Your inventory is full and the "  + shop.Name + " falls to the ground.", Color.Crimson);
                                }
                            } else {
                                Log.AddMessage(new ColoredString("You don't have enough gold to buy that!", Color.Crimson, Color.Black));
                            }
                        } else {
                            Log.AddMessage(new ColoredString("You aren't allowed to use shops.", Color.Crimson, Color.Black));
                        }
                    }); 

                    resourceY++;
                }
                else {
                    mini.Con.Print(resourceX + 2, resourceY, "|");
                    mini.Con.Print(resourceX + 4, resourceY++, curr.ShopItemsHere[i], Color.DarkSlateGray);
                }
            }
                    
            if (home != null) {
                POH_Room room = home.Rooms[MinigameManager.POH_ViewingRoom];

                foreach (var obj in room.BuiltObjects) {
                    if (GameLoop.ZPO.POH_ObjectLib.TryGetValue(obj, out POH_Object? actual) && actual != null) {
                        for (int i = ActivityItemTop; i < actual.ShopItems.Count && resourceY < 35; i++) {
                            if (GameLoop.ZPO.ItemLibrary.ContainsKey(actual.ShopItems[i])) {
                                Item shop = new(GameLoop.ZPO.ItemLibrary[actual.ShopItems[i]]); 
                                mini.Con.Print(resourceX + 2, resourceY, "|"); 

                                int qty = 1;
                                if (Helper.EitherShift()) { qty *= 5; }
                                if (Helper.EitherControl()) { qty *= 10; } 

                                mini.Con.PrintClickable(resourceX + 4, resourceY, shop.GetNameCS(1, 0, shop.UseInt4), () => {
                                    if (qty == 1)
                                        Log.AddMessage("You take a" + (Helper.VowelStart(shop.Name.ToLower()) ? "n " : " ") + shop.Name + " from the " + actual.Name + ".", Color.Goldenrod);
                                    else 
                                        Log.AddMessage("You take " + qty + "x " + shop.Name + " from the " + actual.Name + ".", Color.Goldenrod);

                                    if (!player.TryPickup(shop, qty, false, true)) {
                                        Log.AddMessage("Your inventory is full and the "  + shop.Name + " falls to the ground.", Color.Crimson);
                                    } 
                                }); 

                                resourceY++;
                            }
                            else {
                                mini.Con.Print(resourceX + 2, resourceY, "|");
                                mini.Con.Print(resourceX + 4, resourceY++, actual.ShopItems[i], Color.DarkSlateGray);
                            }
                        }
                    }
                }
            }

            if (resourceY == startY) {
                mini.Con.Print(resourceX + 2, resourceY, "|");
                mini.Con.Print(resourceX + 4, resourceY++, "(no shop items here)", Color.DarkSlateGray);
            }
        }
    }
}
