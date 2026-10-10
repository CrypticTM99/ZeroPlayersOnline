using ZeroPlayersOnline.DataTypes; 

namespace ZeroPlayersOnline.Hardcodes {
    public class LocationsDesert {
        public static void InitLocs(Dictionary<string, Location> Atlas, Dictionary<string, GatheringTile> Gathers, Dictionary<string, AreaMonster> Monsters) {
            List<Location> locsToAdd = new();
             
            // Al Kharid / North of Shantay Pass
            {
                locsToAdd.Add(new Location("DES_AlKharidBank", "Al Kharid Bank", "Desert") {
                    Description = "A nice white stone building on the banks of the River Lum containing the Al Kharid bank. There are some palm trees and cacti outside the building, while the inside is crowded with bank booths, chairs, and a table. Behind the row of bank booths stand tellers ready to assist customers, plus a few desks covered in ledgers and scales.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwamp5", new() { new("Item", 1, "grapple", false), new("Skill", 37, "Ranged"), new("Skill", 8, "Agility"), new("Skill", 19, "Strength") }, alt: "(Grapple Across River)"),
                        new Connection("DES_AlKharid"),
                        new Connection("DES_AlKharidSouth")
                    },
                    IsBank = true,
                    ProcessingStations = new() { "GE Bank" }
                });

                locsToAdd.Add(new Location("DES_AlKharid", "Al Kharid", "Desert") {
                    UPCsong = "Sandstorm",
                    Description = "A sort of plaza, relatively centrally located in front of the palace. There are a variety of useful stores and services offered here, in addition to a set of handholds to climb up onto the roofs to train agility. The ground here is fairly hard packed sand, with a more defined path leading from the palace through the plaza and off to the north. There are a few stalls offering goods, but they are being watched too closely to be stolen from.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharidOutskirts"),
                        new Connection("DES_AlKharidAgility1", new() { new("Skill", 20, "Agility") }, false, 12, "Agility"),
                        new Connection("DES_AlKharidBank"),
                        new Connection("DES_AlKharidZeke"),
                        new Connection("DES_AlKharidDommik"),
                        new Connection("DES_AlKharidGeneralStore"),
                        new Connection("DES_AlKharidLouie"),
                        new Connection("DES_AlKharidRanael"),
                        new Connection("DES_AlKharidKebab"),
                        new Connection("DES_AlKharidAli1"),
                        new Connection("DES_AlKharidTanner"),
                        new Connection("DES_AlKharidHouse"),
                        new Connection("DES_AlKharidTent"), 
                        new Connection("DES_AlKharidPalaceCourtyard"),
                        new Connection("DES_AlKharidSouth")
                    },
                    ProcessingStations = new() { "Sand" },
                    NPCsHere = new() { "desAlKharidAyesha", "man", "man", "man", "man", "woman", "woman", "woman" },
                    ShopItemsHere = new() { "bucketCompost", "plantPotEmpty", "uncutSapphire", "uncutEmerald", "uncutRuby", "uncutDiamond", "cutSapphire", "cutEmerald", "cutRuby", "cutDiamond", "clothSilk" }, 
                    FarmingPatchesHere = new() { "DES_AlKharidCactus" }
                });

                locsToAdd.Add(new Location("DES_AlKharidZeke", "Zeke's Superior Scimitars", "Desert") {
                    Description = "A cramped storefront owned and operated by Zeke. There are a few small rugs on the floor and some potted plants to make the place feel a little more inviting. Some crates are stacked against one wall, with a shelf holding boxes of scimitars against another wall. A table is placed a little off the center of the room but appears to be unused.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid")
                    },
                    NPCsHere = new() { "desAlKharidZeke" },
                    ShopItemsHere = new() { "scimitarBronze", "scimitarIron", "scimitarSteel", "scimitarBlack", "scimitarMithril", "scimitarAdamant" },
                    GatheringSpots = new() { "clueCrates" },
                    ItemSpawns = new() { new("runeBody", 1) }
                });

                locsToAdd.Add(new Location("DES_AlKharidDommik", "Dommik's Crafting Store", "Desert") {
                    Description = "A cramped storefront owned and operated by Dommik. There are a few small rugs on the floor and some potted plants to make the place feel a little more inviting. Many shelves line the walls filled with various crafting bits and bobs. A table is placed a little off the center of the room and acts as both the store counter and a place for Dommik to work on projects.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid")
                    },
                    NPCsHere = new() { "desAlKharidDommik" },
                    ShopItemsHere = new() { "glassblowingPipe", "chisel", "mouldRing", "mouldNecklace", "mouldAmulet", "needle", "mouldHoly", "mouldSickle", "mouldTiara", "mouldBolt", "mouldBracelet" }
                });

                locsToAdd.Add(new Location("DES_AlKharidGeneralStore", "Al Kharid General Store", "Desert") {
                    Description = "A cramped storefront owned and operated by Dommik. There are a few small rugs on the floor and some potted plants to make the place feel a little more inviting. Many shelves line the walls filled with various goods and sundry. There are a few stacks of crates holding even more inventory. A table is placed a little off the center of the room and acts as the store counter.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid")
                    },
                    NPCsHere = new() { "desAlKharidShopkeeper" },
                    ShopItemsHere = new() { "potEmpty", "jugEmpty", "shears", "bucketEmpty", "bowlEmpty", "tinCakeEmpty", "tinderbox", "chisel", "hammer", "compass" },
                    GatheringSpots = new() { "clueCrates" }
                });

                locsToAdd.Add(new Location("DES_AlKharidLouie", "Louie's Armoured Legs Bazaar", "Desert") {
                    Description = "There is nothing more than a few shelves and stacks of crates in this very small building. This operation seems... more than a little sketchy.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid")
                    },
                    NPCsHere = new() { "desAlKharidLouie" },
                    ShopItemsHere = new() { "platelegsBronze", "platelegsIron", "platelegsSteel", "platelegsBlack", "platelegsMithril", "platelegsAdamant" },
                    GatheringSpots = new() { "clueCrates" }
                });

                locsToAdd.Add(new Location("DES_AlKharidRanael", "Ranael's Super Skirt Store", "Desert") {
                    Description = "This is a lovely little shop with the inside covered in greenery, and some nice statues outside the door. The floor is actual stone, and there are some benches for customers waiting to purchase from Ranael. There's a nice large rug in front of the stone table that serves as the counter for the store.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid"),
                        new Connection("DES_AlKharidSouth")
                    },
                    NPCsHere = new() { "desAlKharidRanael" },
                    ShopItemsHere = new() { "plateskirtBronze", "plateskirtIron", "plateskirtSteel", "plateskirtBlack", "plateskirtMithril", "plateskirtAdamant" }
                });

                locsToAdd.Add(new Location("DES_AlKharidKebab", "Kebab Shop", "Desert") {
                    Description = "This shop is little more than a kitchen with barely enough room for Karim and yourself to stand in without being crowded. The kebabs he has on offer smell good, but some of them look a bit dodgy. You could probably use his range if you wanted.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid")
                    },
                    NPCsHere = new() { "desAlKharidKarim" },
                    ProcessingStations = new() { "Range" },
                    ShopItemsHere = new() { "kebab" }
                });

                locsToAdd.Add(new Location("DES_AlKharidTanner", "Al Kharid Tannery", "Desert") {
                    Description = "The smell of the vats in here is absolutely dreadful. There are a few barrels, likely holding tanning chemicals, and some drying lines. Also a mangle, the vats, a table, and a chair in the corner of the room. There are some potted plants around the room in an attempt to lighten the place up but they're wilting in the miasma of the tanning process. ",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid")
                    },
                    NPCsHere = new() { "desAlKharidEllis" },
                    ProcessingStations = new() { "Tannery" }
                });

                locsToAdd.Add(new Location("DES_AlKharidAli1", "Ali's Discount Wares", "Desert") {
                    Description = "This is more of a stall than a dedicated storefront, but the way Ali Morrisane can draw and hold your attention with his rapid speech and sales tactics make the environment seem to fade away. He has a couple crates full of a variety of very different things of questionable use.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid")
                    },
                    NPCsHere = new() { "desAlKharidAli" },
                    ProcessingStations = new() { "Sand" },
                    ShopItemsHere = new() { "potEmpty", "jugEmpty", "waterskin3", "desertShirt", "desertBoots", "bucketEmpty", "beardFake", "kharidianHeadpiece", "papyrus", "knife", "tinderbox", "pickaxeBronze", "meatRawChicken" }
                });

                locsToAdd.Add(new Location("DES_AlKharidHouse", "A House in Al Kharid", "Desert") {
                    Description = "A cozy little house with a bearskin rug on the floor. There are a few crates to store the man's belongings, along with a table and chairs to eat at. The only other things in the house are a range and a bedroll on the ground. There are a weirdly large number of windows opening to the outside. This building is not large enough to justify the eight windows it has.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid")
                    },
                    NPCsHere = new() { "man" },
                    ProcessingStations = new() { "Range" },
                    GatheringSpots = new() { "clueCrates" }
                });

                locsToAdd.Add(new Location("DES_AlKharidTent", "A Tent in Al Kharid", "Desert") {
                    Description = "Seems to be some kind of storage tent for one of the nearby vendors, or possible a shared storage space. There are some crates, boxes, a small table with two stools, an a couple potted plants. Also for some reason there's a chaise lounge. Looks comfortable.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid")
                    },
                    GatheringSpots = new() { "clueCrates", "clueBoxes" },
                    ItemSpawns = new() { new("bucketEmpty", 1) }
                });

                locsToAdd.Add(new Location("DES_AlKharidSouth", "Al Kharid South", "Desert") {
                    Description = "Just south of the palace in Al Kharid. There's not a lot back here except sand, cacti, rocks, dead bushes, and more sand. ",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid"),
                        new Connection("DES_AlKharidBank"),
                        new Connection("DES_AlKharidRanael"),
                        new Connection("DES_AlKharidSorceress"),
                        new Connection("DES_GiantsPlateau"), // Should go to citharede abbey and emir's arena side entrance
                        new Connection("DES_ShantayPass")
                    },
                    NPCsHere = new() { "desAlKharidFerrymanSathwood" }, // TODO: Make this dude when wanting to implement Tempeross
                    ProcessingStations = new() { "Sand" },
                    AreaMonsters = new() { "scorpion" }
                });

                locsToAdd.Add(new Location("DES_AlKharidOutskirts", "Al Kharid Outskirts", "Desert") {
                    Description = "Just north of the town of Al Kharid. There are a few small ponds with trees and rocks surrounding them, and some cacti and dead bushes scattered around. To the north is the Al Kharid mine, to the east is the Emir's Arena, to the west is the gate to Lumbridge.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid"),
                        new Connection("MIST_LumbridgeAcrossLum"),
                        new Connection("DES_AlKharidMineOutside"),
                        new Connection("DES_AlKharidEmirsArena")
                    },
                    ProcessingStations = new() { "Sand" }
                });

                locsToAdd.Add(new Location("DES_AlKharidMineOutside", "Outside the Al Kharid Mine", "Desert") {
                    Description = "This is as close as you can safely get to the Al Kharid mine without potentially enraging the scorpions within. North from here is the crossroads just south of Varrock, while to the east is the Mage Training Arena. South leads to the outskirts of Al Kharid itself. There are many cacti, rocks, and dead bushes here. There's also a strange stone altar surrounded by crumbling pillars emitting a red light.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_VarrockCrossroadsSouth"),
                        new Connection("DES_MageTrainingArena"),
                        new Connection("DES_AlKharidMine"),
                        new Connection("DES_AlKharidOutskirts"),
                        new Connection("RunecraftAltarFire", new() { new("Item", 1, "talismanFire", false) })
                    },
                    ProcessingStations = new() { "Sand" }
                });

                locsToAdd.Add(new Location("DES_AlKharidMine", "Al Kharid Mine", "Desert") {
                    Description = "This place is crawling with scorpions, but there are a wide variety of ores available here. If it was harder to get out of the mine to the surrounding area, some of the rockslides on the sides of the mine would make for convenient handholds to climb out. ",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharidMineOutside")
                    },
                    AreaMonsters = new() { "scorpion", "scorpion", "scorpion", "scorpion", "scorpion", "scorpion" },
                    GatheringSpots = new() { "oreCopper", "oreCopper", "oreTin", "oreTin", "oreIron", "oreIron", "oreSilver", "oreSilver", "oreCoal", "oreCoal", "oreMithril", "oreMithril", "oreGold", "oreGold", "oreAdamant", "oreAdamant", "rockGemCommon", "rockGemCommon", "rockGemUncommon", "rockGemUncommon" },
                    ItemSpawns = new() { new("runeWater", 1), new("runeFire", 1) },
                    ProcessingStations = new() { "Sand" }
                });

                locsToAdd.Add(new Location("DES_AlKharidAgility1", "Al Kharid - Rooftop Agility Start", "Desert") {
                    Description = "The start of the Al Kharid Rooftop Agility Course, on top of the tannery. There is a tightrope leading over to the roof of the bank but it looks a little sketchy. If you fall off you could get hurt.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid"),
                        new Connection("DES_AlKharidAgility2", null, false, 36, "Agility", "(Walk Tightrope)", true, 20, "DES_AlKharid", 1, 5)
                    }
                });

                locsToAdd.Add(new Location("DES_AlKharidAgility2", "Al Kharid - Bank Roof", "Desert") {
                    Description = "Atop the Al Kharid bank. The view here is somewhat nice, overlooking the town of Al Kharid but also across the Lum into the swamp. There is a dubious looking rope swing that is supposed to somehow get you over to the roof of the palace.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid"),
                        new Connection("DES_AlKharidAgility3", null, false, 48, "Agility", "(Swing Rope)")
                    }
                });

                locsToAdd.Add(new Location("DES_AlKharidAgility3", "Al Kharid - Palace Roof", "Desert") {
                    Description = "Atop the Al Kharid palace. You get the feeling that you are probably not supposed to be up here, for the security of the Emir. On the other side of the building from where you landed with the rope swing is a zip line down to the roof of Ranael's Super Skirt Store.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid"),
                        new Connection("DES_AlKharidAgility4", null, false, 48, "Agility", "(Slide Down Zip Line)", true, 20, "DES_AlKharid", 1, 5)
                    }
                });

                locsToAdd.Add(new Location("DES_AlKharidAgility4", "Al Kharid - Ranael's Roof", "Desert") {
                    Description = "That zip line did *not* feel like it's been checked for safety by some kind of health inspector. You're on top of Ranael's Super Skirt Store right now, and there's yet another suspicious agility obstacle ahead. A palm tree with fronds that you hope are strong enough to swing across to Louie's Legs.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid"),
                        new Connection("DES_AlKharidAgility5", null, false, 12, "Agility", "(Tarzan It)")
                    }
                });

                locsToAdd.Add(new Location("DES_AlKharidAgility5", "Al Kharid - Louie's Roof", "Desert") {
                    Description = "Okay, that was actually pretty cool. You wonder for a second if anyone saw, but then remember nobody else is online. There are some beams sticking out of the adjoining roof to get up onto the General Store's part of the building.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid"),
                        new Connection("DES_AlKharidAgility6", null, false, 12, "Agility", "(Climb Beams)")
                    }
                });

                locsToAdd.Add(new Location("DES_AlKharidAgility6", "Al Kharid - General Store Roof", "Desert") {
                    Description = "Not quite as exciting as the other bits of the course, but the end is in sight. There's a tightrope across to the roof of a residential home. This one looks significantly safer than the earlier tightrope.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid"),
                        new Connection("DES_AlKharidAgility7", null, false, 18, "Agility", "(Walk Tightrope)")
                    }
                });

                locsToAdd.Add(new Location("DES_AlKharidAgility7", "Al Kharid - House Roof", "Desert") {
                    Description = "The finish line! There's a small arrow-shaped sign here at the edge of the roof pointing down to the ground, indicating a spot near Zeke's Superior Scimitars as the landing spot.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharid"),
                        new Connection("DES_AlKharid", null, false, 36, "Agility", "(Hop Down)")
                    }
                });

                locsToAdd.Add(new Location("DES_MageTrainingArena", "Mage Training Arena", "Desert") {
                    Description = "This place certainly looks magical. There are a number of books and golems and brooms moving about the hall, and on the walls the paintings slowly move up and down. A huge white rug with an elaborate pattern covers much of the floor and there are some candelabras providing light. There are portals to the different training areas here.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharidMineOutside"),
                        new Connection("DES_MageTrainingArenaTelekinetic"),
                        new Connection("DES_MageTrainingArenaGraveyard"),
                        new Connection("DES_MageTrainingArenaEnchanting"),
                        new Connection("DES_MageTrainingArenaAlchemist")
                    },
                    NPCsHere = new() { "desMTAGuardianRewards" }
                });

                locsToAdd.Add(new Location("DES_MageTrainingArenaTelekinetic", "Telekinetic Theater", "Desert") {
                    Description = "The Telekinetic Theater is a large mostly empty room, with the center containing a fenced-in maze of walls. There is a statue you can cast Telekinetic Grab on to move in cardinal directions, and a clearly indicated tile that you need to move the statue onto. The Telekinetic Guardian stands observing the proceedings from opposite the portal.",
                    MinigameID = "MageTelekinetic",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_MageTrainingArena")
                    },
                    NPCsHere = new() { "desMTAGuardianTelekinetic" }
                });

                locsToAdd.Add(new Location("DES_MageTrainingArenaGraveyard", "Creature Graveyard", "Desert") {
                    Description = "The Creature Graveyard has four piles of bones, one in each corner of the square room. Strange bones, odd bones, weird bones, and unusual bones. No matter how many bones you take from the piles there always seems to be more available. Every five seconds bones fall from the ceiling and will damage you. The Graveyard Guardian stands observing the proceedings from opposite the portal.",
                    MinigameID = "MageGraveyard",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_MageTrainingArena", new() { new("NotItem", 1, "fruitBanana"), new("NotItem", 1, "fruitPeach"), new("NotItem", 1, "mtaBone1"), new("NotItem", 1, "mtaBone2"), new("NotItem", 1, "mtaBone3"), new("NotItem", 1, "mtaBone4") })
                    },
                    NPCsHere = new() { "desMTAGuardianGraveyard" },
                    ProcessingStations = new() { "Fruit Chute" },
                    GatheringSpots = new() { "mtaGraveyardStrange", "mtaGraveyardOdd", "mtaGraveyardWeird", "mtaGraveyardUnusual" }
                });

                locsToAdd.Add(new Location("DES_MageTrainingArenaEnchanting", "Enchanting Chamber", "Desert") {
                    Description = "The Enchanting Chamber has four piles of objects, one in each corner of the square room. Blue icosahedrons, yellow cubes, red pentamids, and green cylinders. No matter how many shapes you take from the piles there always seems to be more available. Every five seconds or so a dragonstone appears on the ground, or teleports to another place if you haven't picked it up. The Enchantment Guardian stands observing the proceedings from opposite the portal.",
                    MinigameID = "MageEnchanting",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_MageTrainingArena")
                    },
                    NPCsHere = new() { "desMTAGuardianEnchanting" },
                    ItemSpawns = new() { new("mtaDragonstone", 5) },
                    ProcessingStations = new() { "Orb Depository" },
                    GatheringSpots = new() { "mtaEnchantIcosahedron", "mtaEnchantCube", "mtaEnchantPentamid", "mtaEnchantCylinder" }
                });

                locsToAdd.Add(new Location("DES_MageTrainingArenaAlchemist", "Alchemist's Playground", "Desert") {
                    Description = "The Alchemist's Playground has five shelves of items, arranged in a loose circle around the center of the room. Leather boots, adamant kiteshields, adamant helmets, emeralds, and rune swords. No matter how many items you take from the shelves there always seems to be more available. The feeling of value you get from each item seems to change periodically. The Alchemy Guardian stands observing the proceedings from opposite the portal.",
                    MinigameID = "MageAlchemist",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_MageTrainingArena", new() { new("NotItem", 1, "mtaAlchCoin"), new("NotItem", 1, "mtaAlch1"), new("NotItem", 1, "mtaAlch2"), new("NotItem", 1, "mtaAlch3"), new("NotItem", 1, "mtaAlch4"), new("NotItem", 1, "mtaAlch5") })
                    },
                    NPCsHere = new() { "desMTAGuardianAlchemy" },
                    ProcessingStations = new() { "Coin Slot" },
                    GatheringSpots = new() { "mtaAlchemyBoots", "mtaAlchemyShield", "mtaAlchemyHelmet", "mtaAlchemyEmerald", "mtaAlchemySword" }
                });

                locsToAdd.Add(new Location("DES_ShantayPass", "Shantay Pass", "Desert") {
                    Description = "A small camp at the entrance to the Kharidian Desert, serving as both a toll booth and a way of warning the unprepared off entering the desert. Signs at the arch leading into the desert proclaim loudly and repeatedly that due to the heat, you are risking your life if you don't take water with you. Besides the sign there are some guards here to enforce the toll, a small jail cell for outlaws, and a canopy that Shantay stands under. There is also a bank chest here that anyone is free to use.",
                    IsBank = true,
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharidSouth"),
                        new Connection("DES_ShantayPassCell", [ new("Data", 999, "CanEnterShantayCell", false, "equals", "Why would you want to go in there? It's locked anyways.")])
                    },
                    NPCsHere = new() { "desShantay" },
                    AreaMonsters = new() { "guardShantay", "guardShantay", "guardShantay" },
                    GatheringSpots = new() { "clueCrates" },
                    ShopItemsHere = new() { "waterskin4", "waterskin3", "waterskin2", "waterskin1", "waterskin0", "jugWater", "bowlWater", "bucketWater", "knife", "desertShirt", "desertRobe", "desertBoots", "barBronze", "feather", "hammer", "bucketEmpty", "bowlEmpty", "jugEmpty", "rope", "passShantay" },
                    ProcessingStations = new() { "Sand", "GE Bank" }
                });
            }

            for (int i = 0; i < locsToAdd.Count; i++) {
                for (int j = 0; j < locsToAdd[i].GatheringSpots.Count; j++) {
                    if (Gathers.ContainsKey(locsToAdd[i].GatheringSpots[j])) {
                        locsToAdd[i].LocalGathers.Add(Helper.Clone(Gathers[locsToAdd[i].GatheringSpots[j]]));
                    }
                }

                for (int j = 0; j < locsToAdd[i].AreaMonsters.Count; j++) {
                    if (Monsters.ContainsKey(locsToAdd[i].AreaMonsters[j])) {
                        locsToAdd[i].MonstersHere.Add(Helper.Clone(Monsters[locsToAdd[i].AreaMonsters[j]]));
                    }
                }


                Atlas.Add(locsToAdd[i].ID, locsToAdd[i]);
            }
        }
    }
}
