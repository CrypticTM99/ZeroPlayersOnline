using ZeroPlayersOnline.DataTypes;
using ZeroPlayersOnline.HardcodedData;

namespace ZeroPlayersOnline.Hardcodes {
    public static class HardcodedBosses {
        public static void InitBosses(Dictionary<string, BossFight> BossLib) {
            List<BossFight> toAdd = new();

            toAdd.Add(new("Huge Zombie", "bossZombie", 10, 20, "Slash", 0, 5, "1d3", "Melee", 2000, 3, 3, true) { 
                CountsAsSlayer = ["zombie", "undead"],
                Specials = new() {
                    new("The huge zombie roars and rears back, preparing to slam the huge club into the ground.", "1d6+2", "Melee", [ 1 ]),
                    new("The huge zombie groans angrily and prepares to swipe to either side.", "1d6+2", "Melee", [ 0, 2 ])
                },
                DropTable = new() { 
                    new("petBabyZombie", 1, 50, 1, 1),
                    new("clubHuger", 1, 20, 1, 1), 
                    new("clubHuge", 1, 10, 1, 1),
                    new("runeMind", 1, 4, 10, 20),  
                    new("runeAir", 1, 4, 10, 20), 
                    new("runeWater", 1, 4, 10, 20), 
                    new("runeEarth", 1, 4, 10, 20), 
                    new("runeFire", 1, 4, 10, 20),  
                    new("arrowsBronze", 1, 4, 10, 20), 
                    new("fleshRotten", 1, 1, 1, 1),
                    new("bonesBig", 1, 1, 1, 1) 
                }
            });

            toAdd.Add(new("Brutus", "bossBrutus", 30, 58, "Slash", 0, 20, "1d3", "Melee", 3000, 3, 5, true) { 
                CountsAsSlayer = [ "cow" ], ExpMultiplier = 2.0,
                Specials = new() {
                    new("Brutus growls. Move out of the highlighted lanes, quickly!", "1d4+15", "Melee", [ 1, 2, 3 ]),
                    new("Brutus snorts. Move out of the highlighted lanes, quickly!", "1d4+15", "Melee", [ -1, 0, 1 ], true)
                },
                DropTable = new() { 
                    new("idesNameTag", 1, 1, 1, 1, req: new("QuestAt", 90, "MI_IdesOfMilk")),
                    new("meatRawTbone", 1, 1, 1, 1),
                    new("bonesBull", 1, 1, 1, 1), 
                    new("mooleta", 1, 30, 1, 1), 
                    new("bucketMilkBottomless", 1, 37.5, 1, 1), 
                    new("slippersCow", 1, 150, 1, 1), 
                    new("helmIron", 1, 40.5, 1, 1), 
                    new("platebodyIron", 1, 40.5, 1, 1), 
                    new("platelegsIron", 1, 81, 1, 1),
                    new("plateskirtIron", 1, 81, 1, 1),  
                    new("arrowsIron", 1, 8.1, 14, 14),
                    new("runeAir", 1, 8.1, 29, 29),
                    new("runeMind", 1, 10.13, 18, 18),
                    new("runeChaos", 1, 40.5, 12, 12),  
                    new("seedPotato", 1, 8.1, 3, 3),
                    new("seedTreePine", 1, 8.1, 2, 2),
                    new("seedTreeOak", 1, 16.2, 2, 2),
                    new("meatRawTbone", 1, 8.1, 3, 3, true),
                    new("cowhide", 1, 8.1, 1, 1),
                    new("logOak", 1, 16.2, 2, 2),
                    new("logPine", 1, 16.2, 2, 2),
                    new("coins", 1, 5.4, 60, 80),
                    new("coins", 1, 16.2, 80, 100),
                    new("coins", 1, 16.2, 100, 120),
                    new("clueScrollBeginner", 1, 15, 1, 1), 
                    new("clueScrollEasy", 1, 40, 1, 1), 
                    new("petBeef", 1, 1000, 1, 1) 
                }
            });
             
            toAdd.Add(new("Scurrius, Rat King", "bossScurrius", 200, 500, "Slash", 0, 20, "1d5-1", "Melee", 3000, 3, 5, true) { 
                SpecialCategory = "Ratlike", CountsAsSlayer = [ "rat" ], ExpMultiplier = 1.2, 
                Specials = new() {
                    new("Scurrius swipes his tail back and forth quickly, preparing to swing it at you.", "1d13", "Melee", [ 1, 2, 3 ]),
                    new("Blue light arcs along Scurrius' tail.", "1d8", "Magic", [ -1, 0, 1 ], true),
                    new("Scurrius reaches back to grab a clump of fur and rolls it in the mud to form a stinky projectile.", "1d7", "Ranged", [ 0 ], true),
                    new("Scurrius jumps up and comes down hard, causing the room to shake. Some bricks fall loose from the ceiling!", "1d10+14", "Ranged", [ 0, 2, 4 ], false),
                    new("Scurrius screeches loudly, summoning a horde of giant rats to assist him.", "0", "Typeless", [ 0, 1, 2, 3, 4 ], false) { SummonID = "ratGiantScurrius", SummonDice = "6" }
                },
                DropTable = new() { 
                    new("bonesBig", 1, 1, 1, 1),
                    new("meatRawRat", 1, 1, 1, 1), 
                    new("platebodyAdamant", 1, 17, 1, 1), 
                    new("helmRune", 1, 17, 1, 1), 
                    new("sqShieldRune", 1, 17, 1, 1), 
                    new("chainmailRune", 1, 17, 1, 1), 
                    new("battleaxeRune", 1, 17, 1, 1), 
                    new("arrowsAdamant", 1, 17, 20, 50),
                    new("arrowsRune", 1, 17, 20, 50),  
                    new("runeChaos", 1, 17, 70, 125),
                    new("runeDeath", 1, 33, 40, 90),
                    new("runeLaw", 1, 33, 10, 30),
                    new("fishCookedTrout", 1, 17, 1, 3),  
                    new("fishCookedTuna", 1, 17, 1, 3),
                    new("fishCookedLobster", 1, 17, 1, 3),
                    new("potionPrayer", 1, 17, 1, 1),
                    new("fishCookedShark", 1, 33, 1, 1),
                    new("potionRanging", 1, 33, 1, 1, doses: 1),
                    new("potionStrengthSuper", 1, 33, 2, 2, doses: 1),
                    new("cheese", 1, 100, 1, 1),
                    new("coins", 1, 17, 1000, 9000),
                    new("clueScrollMedium", 1, 25, 1, 1),
                    new("spineScurrius", 1, 33, 1, 1),
                    new("boneLong", 1, 400, 1, 1), 
                    new("petScurry", 1, 3000, 1, 1), 
                    new("boneCurved", 1, 5013, 1, 1) 
                }
            });

            toAdd.Add(new("Bryophyta", "bossBryophyta", 128, 115, "Fire", 0, 20, "1d5-1", "Melee", 3000, 3, 5, true) { 
                CountsAsSlayer = [ "giantMoss" ], ExpMultiplier = 1.025, 
                Specials = new() {
                    new("Scurrius swipes his tail back and forth quickly, preparing to swing it at you.", "1d13", "Melee", [ 1, 2, 3 ]),
                    new("Blue light arcs along Scurrius' tail.", "1d8", "Magic", [ -1, 0, 1 ], true),
                    new("Scurrius reaches back to grab a clump of fur and rolls it in the mud to form a stinky projectile.", "1d7", "Ranged", [ 0 ], true),
                    new("Scurrius jumps up and comes down hard, causing the room to shake. Some bricks fall loose from the ceiling!", "1d10+14", "Ranged", [ 0, 2, 4 ], false),
                    new("Scurrius screeches loudly, summoning a horde of giant rats to assist him.", "0", "Typeless", [ 0, 1, 2, 3, 4 ], false) { SummonID = "ratGiantScurrius", SummonDice = "6" }
                },
                DropTable = new() { 
                    new("bonesGiant", 1, 1, 1, 1), 
                    new("sqShieldBlack", 1, 26, 1, 1), new("staffMagic", 1, 64, 1, 1), new("helmSteel", 1, 64, 1, 1), new("swordMithril", 1, 64, 1, 1), new("spearMithril", 1, 64, 1, 1), new("kiteshieldSteel", 1, 128, 1, 1),
                    new("runeLaw", 1, 32, 3, 3), new("runeAir", 1, 43, 18, 18), new("runeEarth", 1, 43, 27, 27), new("runeChaos", 1, 43, 7, 7), new("runeNature", 1, 43, 6, 6), new("runeCosmic", 1, 43, 3, 3), new("arrowsIron", 1, 64, 15, 15), new("arrowsSteel", 1, 128, 30, 30), new("runeDeath", 1, 128, 3, 3), new("runeBlood", 1, 128, 1, 1),
                    DropTables.HerbSeeds.But(128.0/5.0),
                    DropTables.UncommonSeeds.But(128.0/35.0),
                    new("coins", 1, 32, 100, 200), 
                    new("barSteel", 1, 21.33, 1, 1), new("spiritOreCoal", 1, 128, 1, 1), new("rollSpinach", 1, 128, 1, 1),
                    DropTables.GemDrop.But(128.0/4.0),
                    new("clueScrollBeginner", 1, 45, 1, 1),
                    new("keyMossy", 1, 150, 1, 1),
                    new("boneLong", 1, 400, 1, 1),
                    new("scrollChampionGiant", 1, 5000, 1, 1),
                    new("boneCurved", 1, 5013, 1, 1)
                }
            });
            

            for (int i = 0; i < toAdd.Count; i++) { 
                BossLib.TryAdd(toAdd[i].ID, toAdd[i]);
            }
        }
    }
}
