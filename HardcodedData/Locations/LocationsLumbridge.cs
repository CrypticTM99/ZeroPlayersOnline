using ZeroPlayersOnline.DataTypes; 

namespace ZeroPlayersOnline.Hardcodes {
    public class LocationsLumbridge {
        public static void InitLocs(Dictionary<string, Location> Atlas, Dictionary<string, GatheringTile> Gathers, Dictionary<string, AreaMonster> Monsters) {
            List<Location> locsToAdd = new();
             
            if (GameLoop.DemoMode) {
                locsToAdd.Add(new Location("MIST_LumbridgeCastleBailey", "Lumbridge - Castle Bailey", "Misthalin") {
                    UPCsong = "Welcome Home",
                    Description = "That's the end of the demo! Thank you for taking the time to try out 'my' game. I hope you enjoyed your time, and if you encountered any bugs, want to make suggestions for future content or tweaks to existing content, want to follow the development progress as I continue working my way into mainland Gielinor, or even just want to talk about the game feel free to join the discord linked on itch! /n /n If you wish to continue playing this character, a teleport back to Tutorial Island is provided for your convenience. /n /n Thanks again!",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("TI_Main")
                    }
                });
            } else {
                locsToAdd.Add(new Location("MIST_LumbridgeCastleBailey", "Lumbridge - Castle Bailey", "Misthalin") {
                    UPCsong = "Welcome Home",
                    Description = "A simple but elegant bailey surrounding a castle, encircled by a stone wall with a large gate set into the east wall, flanked by two guard towers. Around the back of a castle is a smaller tower with a door leading out to the west. To either side of the path leading from the castle doors out of the gate is a beautiful fountain burbling water endlessly. Neatly manicured bushes and flowers line the paths and edges of the walls.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeCastleFoyer"),
                        new Connection("MIST_LumbridgeCastleGatehouse"),
                        new Connection("MIST_Lumbridge"),
                        new Connection("MIST_LumbridgeBehindCastle")
                    },
                    NPCsHere = new() { "mistLumHans", "mistLumJacquelyn", "man", "man", "man", "woman", "woman" },
                    AreaMonsters = new() { "rat", "rat", "rat", "imp" },
                    GatheringSpots = new() { "treeOak", "treePine", "treePine" }
                }); 
            } 

            locsToAdd.Add(new Location("MIST_LumbridgeCastleGatehouse", "Lumbridge - Castle Gatehouse", "Misthalin") {
                Description = "There's not really much in this gatehouse except for some crates, mostly full of spare guard equipment. There's a ladder going up to the next floor.",
                    ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeCastleBailey"),
                    new Connection("MIST_LumbridgeCastleGatehouse2", alt: "(Climb Ladder)")
                }
            });
            
            locsToAdd.Add(new Location("MIST_LumbridgeCastleGatehouse2", "Lumbridge - Castle Gatehouse Second Floor", "Misthalin") {
                Description = "This floor of the gatehouse is even more stuffed full of crates of spare and old equipment, which seems impractical given you'd have to carry things up a ladder to get here. There's a ladder going up to the roof and back down to the ground floor.",
                    ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeCastleGatehouse", alt: "(Climb Ladder Down)"),
                    new Connection("MIST_LumbridgeCastleGatehouse3", alt: "(Climb Ladder Up)")
                },
                GatheringSpots = { "clueCrates" }
            });
            
            locsToAdd.Add(new Location("MIST_LumbridgeCastleGatehouse3", "Lumbridge - Castle Gatehouse Roof", "Misthalin") {
                Description = "Climbing the leader leads you to the roof, where there are a few more barrels stacked up. Some flags are flying banners bearing the Lumbridge colors. For some reason there are a couple cannons up here, though they wouldn't really be able to aim at much. There's a bronze pickaxe up here that seems out of place, so you could probably take it without issue. You have a decent view of the surroundings from up here of the nearby farms to the north, the swamp to the south, and Al Kharid to the east.",
                    ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeCastleGatehouse2", alt: "(Climb Ladder Down)")
                },
                ItemSpawns = new List<ItemSpot>() {
                    new ItemSpot("pickaxeBronze", 1)
                }
            }); 

            locsToAdd.Add(new Location("MIST_LumbridgeCastleFoyer", "Lumbridge - Castle Foyer", "Misthalin") {
                Description = "The castle foyer is well lit by torches and windows. A couple paintings hang on the wall, one of a former King and one of Ice Mountain. Some banners and tapestries bearing the Lumbridge heraldry also adorn the walls, a blue-and-white rug edged with gold covers part of the floor, and a few decorative suits of armor are strategically placed along the edge of the room. There are staircases on the north and south sides of the castle, but the one to the south seems much more heavily trafficked.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeCastleBailey"),
                    new Connection("MIST_LumbridgeCastleKitchen"),
                    new Connection("MIST_LumbridgeCastleFloor2"), 
                    new Connection("MIST_LumbridgeCastleDiningHall")
                },
                ItemSpawns = new List<ItemSpot>() {
                    new ItemSpot("runeMind", 1),
                    new ItemSpot("arrowsBronze", 1)
                }
            }); 

            locsToAdd.Add(new Location("MIST_LumbridgeCastleFloor2", "Lumbridge - Castle Second Floor", "Misthalin") {
                Description = "There are two rooms on this floor of the Castle. The northern room is the bedroom of the Lumbridge Duke, Duke Horacio. It is elegantly furnished as befitting his status, the four-posted bed and other furniture in the room of exquisite construction. The other room is home to the Duke's advisor, Sigmund, and is much more modest. Sigmund's room is fairly sparse other than a bed, set of drawers, spinning wheel, and a fairly nice rug spread across the floor.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeCastleFoyer"),
                    new Connection("MIST_LumbridgeCastleFloor3")
                },
                NPCsHere = new() { "mistLumDukeHoracio", "mistLumSigmund" },
                ProcessingStations = new() { "Spinning Wheel" },
                GatheringSpots = new() { "clueChest" }
            });

            locsToAdd.Add(new Location("MIST_LumbridgeCastleFloor3", "Lumbridge - Castle Third Floor", "Misthalin") {
                Description = "There is little up here except for, strangely, a free-standing building on top of the Castle containing a bank. The inside of the bank is fairly nice, though spartan by necessity due to the small size of the room. Besides the bank booths it has a small desk and a couple chairs, plus another Saradomin rug spread on the floor. There's a ladder leaning against one side of the bank building allowing you to climb up even higher if desired.",
                IsBank = true,
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeCastleFloor2"),
                    new Connection("MIST_LumbridgeCastleFloor4", alt: "(Climb Ladder)")
                },
                ProcessingStations = new() { "GE Bank" }
            });

            locsToAdd.Add(new Location("MIST_LumbridgeCastleFloor4", "Lumbridge - Top of the Castle", "Misthalin") {
                Description = "It's quite breezy up here, but you've reached the absolute highest point in Lumbridge. You can see the windmill and the city of Varrock to the north, Al Kharid to the west, and Draynor Village to the east. There is a small flagpole here with a crank to raise the flag.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeCastleFloor3", alt: "(Climb Ladder)")
                },
                GatheringSpots = new() { "lumbridgeFlag" }
            });

            locsToAdd.Add(new Location("MIST_LumbridgeCastleKitchen", "Lumbridge - Castle Kitchen", "Misthalin") {
                Description = "A surprisingly humble kitchen, considering it's in a castle. There are a couple tables and crates scattered around almost haphazardly, along with a sink, range, and pile of pots and pans. Various cooking implements hang from racks on the walls, and a trapdoor set into the floor leads to the basement. A door to the side of the kitchen leads directly into the Dining Hall.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeCastleFoyer"), 
                    new Connection("MIST_LumbridgeCastleDiningHall"),
                    new Connection("MIST_LumbridgeCastleCellar", alt: "(Climb Ladder)")
                },
                ItemSpawns = new List<ItemSpot>() {
                    new ItemSpot("potEmpty", 1),
                    new ItemSpot("jugEmpty", 1),
                    new ItemSpot("bowlEmpty", 1),
                    new ItemSpot("knife", 1)
                },
                ProcessingStations = new() { "Range", "Sink" },
                NPCsHere = new() { "mistLumCook" }
            }); 

            locsToAdd.Add(new Location("MIST_LumbridgeCastleDiningHall", "Lumbridge - Castle Dining Hall", "Misthalin") {
                Description = "An ornately, bordering on gaudily, decorated dining hall. Well, there's only ten chairs, so it's not really much of a 'hall', but it's certainly a place to eat in a castle. The walls are decorated with tapestries and portraits. Suits of armor and standing candelabras line the walls. There's a fireplace on the west side of the room with candles on the mantle and a painting above it. It, inexplicably, has a fire roaring in it despite the room being empty. There's a plush red rug covering most of the floor, and the center of the room is occupied by a rectangular table with ten chairs around it.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeCastleFoyer"),
                    new Connection("MIST_LumbridgeCastleKitchen")
                }
            }); 

            locsToAdd.Add(new Location("MIST_LumbridgeCastleCellar", "Lumbridge - Castle Cellar", "Misthalin") {
                Description = "Yet more cookery items litter the shelves down here, and for some reason there's another sink. A few items are strewn about on the ground that you could take, but otherwise there isn't much interesting going on in this dark and dank basement.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeCastleKitchen", alt: "(Climb Ladder)")
                },
                ItemSpawns = new List<ItemSpot>() {
                    new ItemSpot("bootsLeather", 1),
                    new ItemSpot("knife", 1),
                    new ItemSpot("cabbage", 1),
                    new ItemSpot("jugEmpty", 1),
                    new ItemSpot("bucketEmpty", 1)
                },
                ProcessingStations = new() { "Sink" },
                GatheringSpots = new() { "clueCrates" }
            }); 

            locsToAdd.Add(new Location("MIST_Lumbridge", "Lumbridge", "Misthalin") {
                Description = "A quaint town along the banks of the River Lum. A street meanders through the town, past the castle, ending in a small cul-de-sac with the graveyard. A small church is across the street from the castle, adjacent the graveyard. Nearby at the end of the road are a few houses and Bob's Brilliant Axes. There's a bridge across the Lum, and farther north lies the rest of Lumbridge.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeCastleBailey"),
                    new Connection("MIST_LumbridgeNorth"),
                    new Connection("MIST_LumbridgePOH", [ new("POHOwned", 0, "MIST_Lumbridge"), new("POHActive", 0, "MIST_Lumbridge") ], true, hideIfNoReqs: true),
                    new Connection("MIST_LumbridgeAcrossLum"),
                    new Connection("MIST_LumbridgeChurch"),
                    new Connection("MIST_LumbridgeGraveyard"),
                    new Connection("MIST_LumbridgeBobsAxes"),
                    new Connection("MIST_LumbridgeVictoria"),
                    new Connection("MIST_LumbridgeEmptyHouse", [ new("POHNotOwned", 0, "MIST_Lumbridge")], hideIfNoReqs: true)
                },
                GatheringSpots = new() { "treeOak", "treeOak", "treePine", "treePine", "treePine" }
            });
            
            locsToAdd.Add(new Location("MIST_LumbridgeBobsAxes", "Bob's Brilliant Axes", "Misthalin") {
                Description = "A cozy little storefront with shelves of various types of axes on display. There's a clock ticking in one corner but the hands don't actually appear to be moving at all. A few crates and barrels are stacked in the corners of the rooms, and a small painting of a port hangs on the wall. Bob, the proprietor, stands nearly motionless behind the counter.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_Lumbridge")
                },
                ShopItemsHere = new() { "pickaxeBronze", "pickaxeIron", "pickaxeSteel", "pickaxeMithril", "pickaxeAdamant", "hatchetBronze", "hatchetIron", "hatchetSteel", "hatchetMithril", "hatchetAdamant", "battleaxeBronze", "battleaxeIron", "battleaxeSteel", "battleaxeMithril", "battleaxeAdamant" },
                MahoganyTown = "Lumbridge",
                MahoganyHomesHotspots = new() { "shelf", "shelf", "shelf", "drawers", "bedLarge", "drawers", "table", "wardrobe", "table", "bookcase", "bookcase"  }
            });
            
            locsToAdd.Add(new Location("MIST_LumbridgeChurch", "Lumbridge Church", "Misthalin") {
                Description = "The church is decorated simply but expertly in dedication to Saradomin, God of Order. Two rows of cushioned wooden pews face the altar and the walls are lined with stained glass windows. Against the wall behind the altar are a few stands of candles and a pipe organ. A couple plush rugs coat the floor, though strangely in red instead of the traditional blue of Saradomin.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_Lumbridge")
                },
                NPCsHere = new() { "mistLumAereck", "tutorPrayer", "woman" },
                AreaMonsters = new() { "woman" },
                MahoganyTown = "Lumbridge",
                MahoganyHomesHotspots = new() { "votary", "organ", "votary", "ladder", "ladder", "lighting", "pew", "pew", "lighting", "pew", "pew", "lighting", "lighting", "pew", "pew", "lighting", "lighting", "altar", "stand", "stand" }
            }); 

            locsToAdd.Add(new Location("MIST_LumbridgeGraveyard", "Lumbridge Graveyard", "Misthalin") {
                Description = "A low fog clings to the ground around the headstones here. The graveyard is enclosed by a short wrought-iron fence, grass and weeds growing slightly rampant due to the lack of a gardener. An imposing building holds a mausoleum and the stairs down to the catacombs.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_Lumbridge"),
                    new Connection("MIST_LumbridgeCatacombsUpper", new() { new("QuestPast", 0, "MI_BloodPact", summ: "Must have started The Blood Pact.") }) { CutscenesOnTraverse = new() { "MI_BloodPact1" } },
                    new Connection("MIST_LumbridgeSwamp5")
                },
                NPCsHere = new() { "mistLumXenia", "mistLumXenia1", "mistLumXenia8", "mistLumXenia9", "mistLumRestlessGhost" },
                GatheringSpots = new() { "treeYew", "mistLumCoffin" }
            });

            // Lumbridge Catacombs Maps
            {
                locsToAdd.Add(new Location("MIST_LumbridgeCatacombsUpper", "Catacombs - Upper Main Room", "Misthalin") {
                    Description = "The inside of the catacombs are surprisingly spacious. There are two main rows of sarcophagi in the middle of the hall, and various pots of offerings and remains everywhere. Set into some of the walls are more stone coffins, and there are Saradomin banners hanging from the walls.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeGraveyard"),
                        new Connection("MIST_LumbridgeCatacombsUpper", new() { new("QuestAt", 10, "MI_BloodPact") }, hideIfNoReqs: true) { CutscenesOnTraverse = new() { "MI_BloodPact2" } },
                        new Connection("MIST_LumbridgeCatacombsUpper1", new() { new("QuestPast", 20, "MI_BloodPact") }, hideIfNoReqs: true),
                        new Connection("MIST_CatacombsEntrance", new() { new("QuestPast", 80, "MI_BloodPact") }, hideIfNoReqs: true)
                    },
                    NPCsHere = new() { "mistLumXenia2", "mistLumXenia3" }
                });

                locsToAdd.Add(new Location("MIST_LumbridgeCatacombsUpper1", "Catacombs - Upper Main Room", "Misthalin") {
                    Description = "The inside of the catacombs are surprisingly spacious. There are two main rows of sarcophagi in the middle of the hall, and various pots of offerings and remains everywhere. Set into some of the walls are more stone coffins, and there are Saradomin banners hanging from the walls.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeGraveyard"),
                        new Connection("MIST_LumbridgeCatacombsUpper"),
                        new Connection("MIST_LumbridgeCatacombsUpper2", new() { new("QuestPast", 40, "MI_BloodPact", summ: "You must deal with the ranger before you advance further into the catacombs.") })
                    },
                    NPCsHere = new() { "mistLumXenia4", "mistLumXenia5", "mistLumKayle" },
                    AreaMonsters = new() { "questBloodPactKayle" },
                    ItemSpawns = new() { new("chargebowKayle", 1, new("QuestAt", 40, "MI_BloodPact")) }
                });

                locsToAdd.Add(new Location("MIST_LumbridgeCatacombsUpper2", "Catacombs - Upper Side Room", "Misthalin") {
                    Description = "This side room of the catacombs is, for some reason, essentially two large balconies facing eachother with a pit across the middle of the room. There are railings on either side of the hole that leads down, and the two halves of the room are connected by a walkway with a gate. ",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeCatacombsUpper1"),
                        new Connection("MIST_LumbridgeCatacombsUpper3", new() { new("Data", 1, "CatacombsWinch", misc3: "equals", summ: "You have to use the winch to get to the other side of the room.") })
                    },
                    NPCsHere = new() { "mistLumXenia6" },
                    GatheringSpots = new() { "mistLumWinch" },
                    AreaMonsters = new() { "questBloodPactCaitlin" }
                });

                locsToAdd.Add(new Location("MIST_LumbridgeCatacombsUpper3", "Catacombs - Upper Side Room - Across", "Misthalin") {
                    Description = "This side room of the catacombs is, for some reason, essentially two large balconies facing eachother with a pit across the middle of the room. There are railings on either side of the hole that leads down, and the two halves of the room are connected by a walkway with a gate. You are on the far side from the entrance.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeCatacombsUpper1"),
                        new Connection("MIST_LumbridgeCatacombsUpper2"),
                        new Connection("MIST_LumbridgeCatacombsLower", new() { new("QuestAt", 60, "MI_BloodPact", summ: "You should deal with the wizard before going downstairs.") }) { CutscenesOnTraverse = new() { "MI_BloodPact5" } },
                    },
                    NPCsHere = new() { "mistLumXenia7", "mistLumCaitlin" },
                    ItemSpawns = new() { new("staffCaitlin", 1, new("QuestAt", 60, "MI_BloodPact")) }
                });

                locsToAdd.Add(new Location("MIST_LumbridgeCatacombsLower", "Catacombs - Lower", "Misthalin") {
                    Description = "A large room supported by pillars, with banners hanging from the walls and more urns spread around the room in piles. One large sarcophagus, the resting place of Dragith Nurn, rests at the south end of the hall. Beneath a grate in the floor, some water flows through the center of the room.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeCatacombsUpper1"),
                        new Connection("MIST_LumbridgeCatacombsUpper2"),
                        new Connection("MIST_LumbridgeCatacombsUpper3"),
                        new Connection("MIST_CatacombsEntrance", new() { new("QuestPast", 100, "MI_BloodPact", summ: "You should take Ilona back to the surface and speak to Xenia before venturing into the dungeon.")})
                    },
                    NPCsHere = new() { "mistLumReese" },
                    GatheringSpots = new() { "mistLumIlona" },
                    ItemSpawns = new() { new("swordReese", 1, new("QuestAt", 80, "MI_BloodPact")) },
                    AreaMonsters = new() { "questBloodPactReese" }
                });

                locsToAdd.Add(new Location("MIST_CatacombsEntrance", "Catacombs - Dungeon Entrance", "Misthalin") {
                    DungeoneeringLevel = 1,
                    Description = "The entrance to the catacombs, all clean stone tiles with torches to provide light. There's nothing here but the stairs back up and the path forward.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeGraveyard"),
                        new Connection("MIST_LumbridgeCatacombsUpper1"),
                        new Connection("MIST_LumbridgeCatacombsLower"),
                        new Connection("MIST_CatacombsRoom1")
                    }
                });

                locsToAdd.Add(new Location("MIST_CatacombsRoom1", "Catacombs - Warped Cockroaches", "Misthalin") {
                    DungeoneeringLevel = 1,
                    Description = "The first room of the catacombs, all clean stone tiles with torches to provide light. This room is inhabited by some warped cockroaches that skitter about unpleasantly.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_CatacombsEntrance"),
                        new Connection("MIST_CatacombsRoom2")
                    },
                    AreaMonsters = new() { "warpedCockroach", "warpedCockroach", "warpedCockroach", "warpedCockroach", "warpedCockroach" }
                });

                locsToAdd.Add(new Location("MIST_CatacombsRoom2", "Catacombs - Corpse Spiders", "Misthalin") {
                    DungeoneeringLevel = 1,
                    Description = "The second room of the catacombs, all clean stone tiles with torches to provide light. This room is inhabited by some corpse spiders skittering about. The cockroachers really weren't so bad compared to these nasty things.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_CatacombsRoom1"),
                        new Connection("MIST_CatacombsRoom3")
                    },
                    AreaMonsters = new() { "spiderCorpse", "spiderCorpse", "spiderCorpse", "spiderCorpse", "spiderCorpse" }
                });

                locsToAdd.Add(new Location("MIST_CatacombsRoom3", "Catacombs - Warped Flies", "Misthalin") {
                    DungeoneeringLevel = 1,
                    Description = "The third room of the catacombs, all clean stone tiles with torches to provide light. This room is inhabited by some warped flies buzzing back and forth through the air. There is a plinth here that looks like it could have a jade demon statuette on it.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_CatacombsRoom2"),
                        new Connection("MIST_CatacombsRoom4")
                    },
                    AreaMonsters = new() { "warpedFly", "warpedFly", "warpedFly", "warpedFly", "warpedFly" },
                    ItemSpawns = new() { new("statuetteJade", 1, new("NotItem", 1, "statuetteJade")) }
                });

                locsToAdd.Add(new Location("MIST_CatacombsRoom4", "Catacombs - Corpse Torsos", "Misthalin") {
                    DungeoneeringLevel = 1,
                    Description = "The fourth room of the catacombs, all clean stone tiles with torches to provide light. This room is inhabited by the torsos of corpses dragging themselves along the ground.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_CatacombsRoom3"),
                        new Connection("MIST_CatacombsRoom5")
                    },
                    AreaMonsters = new() { "corpseTorso", "corpseTorso", "corpseTorso", "corpseTorso", "corpseTorso" }
                });

                locsToAdd.Add(new Location("MIST_CatacombsRoom5", "Catacombs - Warped Rats", "Misthalin") {
                    DungeoneeringLevel = 1,
                    Description = "The fifth room of the catacombs, all clean stone tiles with torches to provide light. This room is inhabited by a bunch of chittering warped rats.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_CatacombsRoom4"),
                        new Connection("MIST_CatacombsRoom6")
                    },
                    AreaMonsters = new() { "warpedRat", "warpedRat", "warpedRat", "warpedRat", "warpedRat" }
                });

                locsToAdd.Add(new Location("MIST_CatacombsRoom6", "Catacombs - Skeletons", "Misthalin") {
                    DungeoneeringLevel = 1,
                    Description = "The sixth room of the catacombs, all clean stone tiles with torches to provide light. This room is inhabited by some fairly normal skeletons, just the animated decayed remains of former Lumbridge citizens. There is a plinth here that looks like it could have a topaz demon statuette on it.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_CatacombsRoom5"),
                        new Connection("MIST_CatacombsRoom7")
                    },
                    AreaMonsters = new() { "cataSkeleton", "cataSkeleton", "cataSkeleton", "cataSkeleton", "cataSkeleton" },
                    ItemSpawns = new() { new("statuetteTopaz", 1, new("NotItem", 1, "statuetteTopaz")) }
                });

                locsToAdd.Add(new Location("MIST_CatacombsRoom7", "Catacombs - Warped Bats", "Misthalin") {
                    DungeoneeringLevel = 1,
                    Description = "The seventh room of the catacombs, all clean stone tiles with torches to provide light. This room is inhabited by many warped bats flapping and screeching.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_CatacombsRoom6"),
                        new Connection("MIST_CatacombsRoom8")
                    },
                    AreaMonsters = new() { "warpedBat", "warpedBat", "warpedBat", "warpedBat", "warpedBat" }
                });

                locsToAdd.Add(new Location("MIST_CatacombsRoom8", "Catacombs - Corpse Archers", "Misthalin") {
                    DungeoneeringLevel = 1,
                    Description = "The eighth room of the catacombs, all clean stone tiles with torches to provide light. This room is inhabited by shambling corpses of former archers, still holding and using their bows. There is a plinth here that looks like it could have a sapphire demon statuette on it.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_CatacombsRoom7"),
                        new Connection("MIST_CatacombsRoom9")
                    },
                    AreaMonsters = new() { "warpedBat", "warpedBat", "warpedBat", "warpedBat", "warpedBat" },
                    ItemSpawns = new() { new("statuetteSapphire", 1, new("NotItem", 1, "statuetteSapphire")) }
                });

                locsToAdd.Add(new Location("MIST_CatacombsRoom9", "Catacombs - Skoblins", "Misthalin") {
                    DungeoneeringLevel = 1,
                    Description = "The ninth room of the catacombs, all clean stone tiles with torches to provide light. This room is inhabited by skeletal goblins. I guess Dragith Nurn didn't just reanimate human remains. There is a plinth here that looks like it could have an emerald demon statuette on it.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_CatacombsRoom8"),
                        new Connection("MIST_CatacombsRoom10")
                    },
                    AreaMonsters = new() { "skoblin", "skoblin", "skoblin", "skoblin", "skoblin" },
                    ItemSpawns = new() { new("statuetteEmerald", 1, new("NotItem", 1, "statuetteEmerald")) }
                });

                locsToAdd.Add(new Location("MIST_CatacombsRoom10", "Catacombs - Corpse Mages", "Misthalin") {
                    DungeoneeringLevel = 1,
                    Description = "The tenth room of the catacombs, all clean stone tiles with torches to provide light. This room is inhabited by shambling corpses that have an aura of magic about them. There is a plinth here that looks like it could have a ruby demon statuette on it.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_CatacombsRoom9"),
                        new Connection("MIST_CatacombsRoom11")
                    },
                    AreaMonsters = new() { "corpseMage", "corpseMage", "corpseMage", "corpseMage", "corpseMage" },
                    ItemSpawns = new() { new("statuetteRuby", 1, new("NotItem", 1, "statuetteRuby")) }
                });

                locsToAdd.Add(new Location("MIST_CatacombsRoom11", "Catacombs - Dragith Nurn", "Misthalin") {
                    DungeoneeringLevel = 1,
                    Description = "The final room of the catacombs, all clean stone tiles with torches to provide light. There is a plinth here that looks like it could have a diamond demon statuette on it.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_CatacombsRoom10")
                    },
                    AreaMonsters = new() { "dragithNurn" },
                    ItemSpawns = new() { new("statuetteDiamond", 1, new("NotItem", 1, "statuetteDiamond")) }
                });
            }


            locsToAdd.Add(new Location("MIST_LumbridgePOH", "Lumbridge Cottage", "Misthalin") {
                Description = "Home sweet home.".Align(HorizontalAlignment.Center, 92),
                PlayerHomeHere = "MIST_Lumbridge",
                ShopPriceMultiplier = 0,
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_Lumbridge")
                }
            });

            locsToAdd.Add(new Location("MIST_LumbridgeEmptyHouse", "Empty House", "Misthalin") {
                Description = "It seems like someone used to live here, but they aren't around anymore. There are a couple shelves up against one wall, some more shelves hanging on another wall with cooking implements stacked on them, and a small table with two chairs in the middle of the room. A bed stuffed in one corner of the room and a couple brown rugs make the shack feel a little more homely. The last thing in the house is a range that still seems functional.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_Lumbridge")
                },
                ProcessingStations = new() { "Range" }
            });

            locsToAdd.Add(new Location("MIST_LumbridgeVictoria", "Victoria's Home", "Misthalin") {
                Description = "A cozy little cramped house just off bustling downtown Lumbridge! For just a few hundred thousand gold a month this home could be yours!... If Victoria wasn't already living here, and looking somewhat displeased with you having barged in. There's a bookshelf up against one wall, some shelves holding various knick-knacks, a small table and chair, a bed, and a sink. Also one adventurer, and one woman who would prefer there was not an adventurer.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_Lumbridge")
                },
                NPCsHere = new() { "mistLumVictoria" },
                ProcessingStations = new() { "Sink" },
                MahoganyTown = "Lumbridge",
                MahoganyHomesHotspots = new() { "bookcase", "sink", "chair", "table", "chair", "wardrobe", "drawers", "bedLarge" }
            });

            locsToAdd.Add(new Location("MIST_LumbridgeNorth", "North Lumbridge", "Misthalin") {
                Description = "The more active part of Lumbridge, north of the castle. The main town road extends through this section farther north to Farmer Fred's farm, and south past the castle. There is a market, general store, forge, fishing shop, and tavern here. ",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_Lumbridge"), 
                    new Connection("MIST_LumbridgeTowardsDraynor"),
                    new Connection("MIST_LumbridgeFredsFarm"),
                    new Connection("MIST_LumbridgeGeneralStore"),
                    new Connection("MIST_LumbridgeMarket"),
                    new Connection("MIST_LumbridgeFishingStore"),
                    new Connection("MIST_LumbridgeShearedRam"),
                    new Connection("MIST_LumbridgeCommunalForge"),
                    new Connection("MIST_GroatsFarm")
                },
                NPCsHere = new() { "mistLumDoomsayer", "man", "man", "man", "man" },
                AreaMonsters = new() { "man", "man", "man", "man" },
                GatheringSpots = new() { "treeOak", "treeOak", "treePine", "treePine", "treePine", "treeWillow", "treeWillow", "fishBaitLow", "fishBaitLow", "fishBaitLow", "fishLure", "fishLure", "fishLure" }
            }); 

            locsToAdd.Add(new Location("MIST_LumbridgeMarket", "Lumbridge Market", "Misthalin") {
                Description = "The small market is bustling, people wandering from stall to stall to peruse the goods on sale. A few sets of tables and chairs allow people to take a break and eat the things they've purchased. Some guards are walking around to guard the stalls but aren't doing a particularly good job, and an enterprising individual could easily steal without their notice.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeNorth"),
                    new Connection("MIST_LumbridgeFishingStore")
                },
                NPCsHere = new() { "mistLumHarlan", "man", "man", "woman", "woman", "woman", "woman" },
                AreaMonsters = new() { "man", "man", "woman", "woman", "woman", "woman" },
                GatheringSpots = new() { "stallVegetable", "stallVegetable", "stallBakery", "stallBakery", "stallCrafting", "stallCrafting", "stallWine", "stallWine", "stallSeed", "stallSeed"}
                // TODO: Add some shop items here? It IS a market
            });

            locsToAdd.Add(new Location("MIST_LumbridgeShearedRam", "The Sheared Ram", "Misthalin") {
                UPCsong = "Tubthumping",
                Description = "This small bar is fairly cozy, with windows looking out over the River Lum and rustic decorations around the room. There are a small handful of tables available, or you could take a seat at the bar. The occupants are a little rowdy but the bar is not overly full in such a small town.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeNorth")
                },
                NPCsHere = new() { "mistVarRomeoShearedRam", "mistLumBartender", "mistLumVeos", "clueArthur", "man", "man", "man" },
                AreaMonsters = new() { "man", "man", "man" },
                ShopItemsHere = new() { "beer" }
            });

            locsToAdd.Add(new Location("MIST_LumbridgeCommunalForge", "Communal Forge", "Misthalin") {
                Description = "This small shack isn't even really enclosed, more of a stone canopy. It has some of the basic tools for smelting and smithing. A few small workbenches with stools are pushed up against the few walls the canopy does have, and a dirty brown rug covers part of the floor. Most of the floorspace is taken up by the furnace and anvil.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeNorth")
                },
                ProcessingStations = new() { "Casting", "Furnace", "Anvil" }
            });

            locsToAdd.Add(new Location("MIST_LumbridgeGeneralStore", "General Store", "Misthalin") {
                Description = "An assortment of products one could almost mistake for knick-knacks line the shelves of this small building. Most of it doesn't appear to be too useful, but there are a few items of interest. Seems like business here isn't quite as good since the market and fishing store moved in.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeNorth")
                },
                ShopItemsHere = new() { "potEmpty", "jugEmpty", "shears", "knife", "bucketEmpty", "bowlEmpty", "tinCakeEmpty", "tinderbox", "chisel", "shovel", "hammer", "plantPotEmpty", "candle", "compass" },
                NPCsHere = new() { "man", "man" },
                AreaMonsters = new() { "man", "man" },
                MahoganyTown = "Lumbridge",
                MahoganyHomesHotspots = new() { "shelf", "shelf", "counter", "counter", "shelf", "shelf", "shelf", "shelf", "range", "shelf", "bed", "drawers", "drawers", "bedLarge", "desk" }
            });

            locsToAdd.Add(new Location("MIST_LumbridgeFishingStore", "Fishing Store", "Misthalin") {
                Description = "There's a fishy smell lingering in here, permeating the walls and floors. A cooler has a variety of fish available, and the tools to catch your own fish line the shelves while the disgruntled store owner lounges in a chair behind the counter.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeNorth")
                },
                ShopItemsHere = new() { "baitFish", "feather", "fishingNetSmall", "fishingNetBig", "fishingRod", "fishingRodFly", "fishingPotLobster", "fishingHarpoon", "fishRawShrimp", "fishRawAnchovies", "fishRawSardine", "fishRawHerring", "fishRawTrout", "fishRawPike", "fishRawSalmon", "fishRawTuna", "fishRawSwordfish" },
                NPCsHere = new() { "man", "man" },
                AreaMonsters = new() { "man", "man" }
            });

            locsToAdd.Add(new Location("MIST_LumbridgeFredsFarm", "Fred's Farm", "Misthalin") {
                Description = "A fairly large farmstead owned and operated by Fred the Farmer. There are some sheep here, plus a cow pen and chicken coop that you could get into. A huge windmill spins endlessly atop a small hill, and there are wheat and potato fields here. An axe in front of Fred's house has a bronze hatchet stuck in it.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeNorth"),
                    new Connection("MIST_DraynorCrossroads"),
                    new Connection("MIST_DraynorFarm"),
                    new Connection("MIST_LumbridgeFredsFarmChickens"),
                    new Connection("MIST_LumbridgeFredsFarmCows")
                },
                ItemSpawns = new List<ItemSpot>() {
                    new ItemSpot("hatchetBronze", 1)
                },
                GatheringSpots = new() { "clueChest", "treePine", "treePine", "treePine", "sheep", "sheep", "sheep", "plantOnion", "plantOnion", "plantGrain", "plantGrain", "plantGrain"},
                ProcessingStations = new() { "Windmill" },
                NPCsHere = new() { "mistLumFred", "farmer" },
                AreaMonsters = new() { "farmer" }
            }); 

            locsToAdd.Add(new Location("MIST_LumbridgeFredsFarmChickens", "Fred's Chicken Coop", "Misthalin") {
                Description = "It's a little cramped inside this coop, what with all the chickens wandering around and the sacks of seed and grain. One of Fred's farmhands is here keeping an eye on the chickens, but apparently isn't being paid enough to stop you from harming them.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeFredsFarm")
                },
                ItemSpawns = new List<ItemSpot>() {
                    new ItemSpot("eggChicken", 1)
                },
                AreaMonsters = new() { "farmer", "chicken", "chicken", "chicken", "chicken" },
                NPCsHere = new() { "farmer" }
            }); 

            locsToAdd.Add(new Location("MIST_LumbridgeFredsFarmCows", "Fred's Cow Pen", "Misthalin") {
                Description = "This is a spacious pen full of cows and their calves right on the banks of the River Lum. A few thistles grow around the pen, occasionally being eaten by the cows and then reappearing a few seconds later.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeFredsFarm")
                },
                AreaMonsters = new() { "farmer", "cow", "cow", "cow", "cow" },
                ProcessingStations = new() { "Dairy Cow" },
                NPCsHere = new() { "farmer" }
            }); 

            locsToAdd.Add(new Location("MIST_LumbridgeBehindCastle", "Lumbridge - Behind the Castle", "Misthalin") {
                Description = "The area behind Lumbridge Castle is lightly forested. For some reason there's an unlocked door leading inside the walls, and nobody is guarding it. A few more poles hang the Lumbridge banner back here. There are a few stray sheep, some rodents of unusual size, some rodents of usual size, and a farming patch to grow trees as well.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeCastleBailey"), 
                    new Connection("MIST_LumbridgeTowardsDraynor")
                },
                GatheringSpots = new() { "treeOak", "treeOak", "treeYew", "treePine", "treePine", "treePine", "sheep", "sheep" },
                FarmingPatchesHere = new() { "MIST_LumbTree" },
                AreaMonsters = new() { "rat", "rat", "rat", "ratGiant", "ratGiant" }
            });

            locsToAdd.Add(new Location("MIST_LumbridgeTowardsDraynor", "Between Lumbridge and Draynor", "Misthalin") {
                Description = "Just outside Lumbridge on the path leading from there to Draynor Village. The path winds back and forth between the small hills of the countryside. A small smattering of monsters and trees can be found here, and a crumbling ruin of a building, but not a lot else of interest. There's a suspicious locked trapdoor in the ruin.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeNorth"),
                    new Connection("MIST_LumbridgeBehindCastle"),
                    new Connection("MIST_HamHideout", new() { new("Skill", 5, "Thieving") }, false, 5, "Thieving", "(Pick-lock Trapdoor)"),
                    new Connection("MIST_DraynorOutskirtsSouth")
                },
                GatheringSpots = new() { "treeOak", "treeOak", "treePine", "treePine", "treePine" },
                AreaMonsters = new() { "spiderGiant", "spiderGiant", "goblin", "goblin", "goblin" }
            });

            locsToAdd.Add(new Location("MIST_LumbridgeAcrossLum", "Across the River Lum", "Misthalin") {
                Description = "There is a small ruined shack here surrounded by goblins and a couple giant spiders. A signpost here says 'North: farms and Varrock', 'East: Al Kharid toll gate', 'South: the River Lum', 'West: Lumbridge'.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_GroatsFarm"),
                    new Connection("DES_AlKharidOutskirts", new() { new("QuestAt", 100, "DES_PrinceAliRescue"), new("Item", 10, "Gold", true) }, true), // TODO: When Prince Ali Rescue is implemented, update this to the actual complete stage
                    new Connection("MIST_Lumbridge"),
                    new Connection("MIST_VarrockMineWest", null, false, 30, "Woodcutting", "(Canoe to the Champion's Guild)", false, 12),
                    new Connection("MIST_BarbarianVillage", null, false, 60, "Woodcutting", "(Canoe to Barbarian Village)", false, 27),
                    new Connection("MIST_Edgeville", null, false, 90, "Woodcutting", "(Canoe to Edgeville)", false, 42),
                    new Connection("WILD_FeroxEnclave", null, false, 150, "Woodcutting", "(Canoe to Ferox Enclave)", false, 57),
                    new Connection("WILD_WildernessPond", null, false, 150, "Woodcutting", "(Canoe to Wilderness Pond)", false, 57)
                },
                ItemSpawns = new List<ItemSpot>() {
                    new ItemSpot("daggerIron", 1)
                },
                NPCsHere = new() { "desBorderGuard" },
                GatheringSpots = new() { "clueBoxes", "treeOak", "treeOak", "treePine", "treePine", "treePine", "treeDead", "treeDead", "treeDead" },
                AreaMonsters = new() { "goblin", "goblin", "goblin", "goblin", "goblin", "spiderGiant", "spiderGiant" }
            });

            locsToAdd.Add(new Location("MIST_GroatsFarm", "Groats' Farm", "Misthalin") {
                Description = "A sprawling farmstead on the banks of the River Lum, just east of Lumbridge. There are a variety of trees here, a small potato field, a chicken coop, and a modest house where Seth and his daughter live. Some farmhands walk around managing the chores. There's a cow field taking up the east half of the farm, and a farm patch suitable for growing hops.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockCrossroadsSouth"),
                    new Connection("MIST_GroatsFarmCows"),
                    new Connection("MIST_LumbridgeNorth"),
                    new Connection("MIST_LumbridgeAcrossLum")
                },
                ItemSpawns = new List<ItemSpot>() {
                    new ItemSpot("eggChicken", 1)
                },
                NPCsHere = new() { "mistLumGroatsSeth", "farmer", "farmer", "farmer" },
                ProcessingStations = new() { "Range", "Dairy Churn" },
                FarmingPatchesHere = new() { "MIST_LumbHops" },
                GatheringSpots = new() { "bookshelfGroats", "treeWillow", "treeWillow", "treeWillow", "treeOak", "treeOak", "treePine", "treePine", "treePine", "plantPotato", "plantPotato", "plantPotato" },
                AreaMonsters = new() { "farmer", "farmer", "farmer", "chicken", "chicken", "chicken", "chicken", "chicken" }
            });

            locsToAdd.Add(new Location("MIST_GroatsFarmCows", "Groats' Farm - Cow Pen", "Misthalin") {
                Description = "A spacious pen holding many cows, which are grazing passively on the short grass coating the ground of the pen. It is surrounded by a wooden fence, with the Lum and Groats' Farm to the west and a spot north of Al Kharid to the east.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_GroatsFarm"),
                    new Connection("MIST_GroatsFarmBrutus", [ new("QuestPast", 80, "MI_IdesOfMilk")])
                },
                ItemSpawns = new List<ItemSpot>() {
                    new ItemSpot("bucketEmpty", 1)
                },
                ProcessingStations = new() { "Dairy Cow" }, 
                NPCsHere = new() { "mistLumGroatsGillie" },
                AreaMonsters = new() { "cow", "cow", "cow", "cow", "cow", "cow", "cow", "cow" }
            });

            locsToAdd.Add(new Location("MIST_GroatsFarmBrutus", "Groats' Farm - Brutus", "Misthalin") {
                UPCsong = "MILK",
                Description = "This part of the field has a huge angry-looking bull with huge horns and red eyes whose name seems to be Brutus. He snorts and stomps around like he owns the place which, at least by the standards of animals, he essentially does. Brutus looks a bit intimidating. Definitely doesn't skip leg day.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_GroatsFarmCows"), 
                },
                BossHere = "bossBrutus"
            });

            locsToAdd.Add(new Location("MIST_HamHideout", "H.A.M. Hideout", "Misthalin") {
                Description = "This is a surprisingly large cavern lit by braziers and torches. Banners are hung haphazardly everywhere bearing a logo consisting of the letters 'HAM' stylized in purple, green, and red. A handful of wooden benches are facing a small stage that a fanatic is preaching about the evils of 'monsters' on. Other fanatics are scattered around the cavern speaking energetically with eachother. They are all dressed in nearly identical magenta clothing.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_LumbridgeTowardsDraynor", alt: "(Climb Ladder)"),
                    new Connection("MIST_HamHideoutCells")
                }, 
                NPCsHere = new() { "hamFemale", "hamFemale", "hamFemale", "hamMale", "hamMale", "hamMale" },
                AreaMonsters = new() { "hamGuard", "hamGuard", "hamGuard", "hamGuard", "hamGuard" }
            });

            locsToAdd.Add(new Location("MIST_HamHideoutCells", "H.A.M. Hideout - Cells", "Misthalin") {
                Description = "This small branch off the main hideout is less populated but still lit by braziers and torches. Banners are hung haphazardly everywhere bearing a logo consisting of the letters 'HAM' stylized in purple, green, and red. A few small jail cells are set up with the metal bars sunk into the floor of the cavern.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_HamHideout")
                }, 
                NPCsHere = new() { "mistHamJimmy", "hamFemale" },
                AreaMonsters = new() { "hamGuard", "hamGuard" }
            });
        

            // Lumbridge Swamp
            {
                int index = 0;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp Coast", "Misthalin") {
                    Description = "The marshy ground of the swamp fades to the sand and harder packed dirt of the river banks here. Across the water is the Wizard Tower island, the tower spearing up into the clouds. The path out to just south of Draynor Village is clearly visible from this part of the swamp.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp", MazeLandmark = true,
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_DraynorOutskirtsSouth"),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    GatheringSpots = new() { "treePine", "treePine", "treePine", "treePine" }, 
                    AreaMonsters = new() { "ratGiant", "ratGiant" }
                });

                index = 1;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp", "Misthalin") {
                    Description = "The ground here is spongy and the footing unstable due to all the moisture. A constant shroud of fog and darkness hangs over this place, making it very hard to see where you're going. Puddles of stagnant water are everywhere and the trees press close. A small fence separates the swamp from the area behind Lumbridge Castle to the north.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    GatheringSpots = new() { "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead" }, 
                    AreaMonsters = new() { "ratGiant", "ratGiant", "frog", "goblin", "frogBig", "frogBig" },
                    ItemSpawns = new() { new("swampTar", 1, null, 3) }
                });

                index = 2;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp", "Misthalin") {
                    Description = "The ground here is spongy and the footing unstable due to all the moisture. A constant shroud of fog and darkness hangs over this place, making it very hard to see where you're going. Puddles of stagnant water are everywhere and the trees press close. A small fence separates the swamp from the area behind Lumbridge Castle to the north.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    GatheringSpots = new() { "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead" }, 
                    AreaMonsters = new() { "ratGiant", "ratGiant", "frog", "frogBig", "frogBig" },
                    ItemSpawns = new() { new("swampTar", 1, null, 3) }
                });

                index = 3;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp", "Misthalin") {
                    Description = "The ground here is spongy and the footing unstable due to all the moisture. A constant shroud of fog and darkness hangs over this place, making it very hard to see where you're going. Puddles of stagnant water are everywhere and the trees press close. A small fence separates the swamp from the area behind Lumbridge Castle to the north.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    GatheringSpots = new() { "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead" }, 
                    AreaMonsters = new() { "ratGiant", "ratGiant", "frog", "frogBig", "frogBig", "frogGiant" },
                    ItemSpawns = new() { new("swampTar", 1, null, 3) }
                });

                index = 4;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp", "Misthalin") {
                    Description = "The ground here is spongy and the footing unstable due to all the moisture. A constant shroud of fog and darkness hangs over this place, making it very hard to see where you're going. Puddles of stagnant water are everywhere and the trees press close. A small fence separates the swamp from the area behind Lumbridge Castle to the north.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    GatheringSpots = new() { "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead" }, 
                    AreaMonsters = new() { "ratGiant", "ratGiant", "frog", "frog", "frog" },
                    ItemSpawns = new() { new("swampTar", 1, null, 3) }
                });

                index = 5;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp Coast", "Misthalin") {
                    Description = "The marshy ground of the swamp fades to the sand and harder packed dirt of the river banks here. Across the river is the desert town of Al Kharid, and there are trees such that you could probably grapple across here with the right tools and skills.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp", MazeLandmark = true,
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeGraveyard"),
                        new Connection("DES_AlKharidBank", new() { new("Item", 1, "grapple", false), new("Skill", 37, "Ranged"), new("Skill", 8, "Agility"), new("Skill", 19, "Strength") }, alt: "(Grapple Across River)"),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    }
                });

                index = 6;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp Coast", "Misthalin") {
                    Description = "The marshy ground of the swamp fades to the sand and harder packed dirt of the river banks here. Across the water is the Wizard Tower island, the tower spearing up into the clouds.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp",
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    GatheringSpots = new() { "treeDead", "treeDead", "treeDead", "treeDead" }, 
                    AreaMonsters = new() { "frog", "frog", "frog" }
                });

                index = 7;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp", "Misthalin") {
                    Description = "The ground here is spongy and the footing unstable due to all the moisture. A constant shroud of fog and darkness hangs over this place, making it very hard to see where you're going. Puddles of stagnant water are everywhere and the trees press close. There is a tree here with a dark hole at the base. With a rope you could climb down, but you should probably also have a light of some kind.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp", MazeLandmark = true,
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwampCaveEntrance", new() { new("Item", 1, "rope", true), new("Data", 1, "SwampCaveRope", false, "equals", "Already attached rope.") }, true) { WorldStateChange = "set", WorldStateID = "SwampCaveRope", WorldStateNum = 1 },
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    GatheringSpots = new() { "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead" }, 
                    AreaMonsters = new() { "ratGiant", "ratGiant", "frog", "frog", "frog" },
                    ItemSpawns = new() { new("swampTar", 1, null, 3) },
                    NPCsHere = new() { "mistLumCandles" }
                });

                index = 8;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp", "Misthalin") {
                    Description = "The ground here is spongy and the footing unstable due to all the moisture. A constant shroud of fog and darkness hangs over this place, making it very hard to see where you're going. Puddles of stagnant water are everywhere and the trees press close.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp",
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    GatheringSpots = new() { "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead" }, 
                    AreaMonsters = new() { "ratGiant", "ratGiant", "frog", "frog", "frog" },
                    ItemSpawns = new() { new("swampTar", 1, null, 3) }
                });

                index = 9;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp", "Misthalin") {
                    Description = "The ground here is spongy and the footing unstable due to all the moisture. A constant shroud of fog and darkness hangs over this place, making it very hard to see where you're going. Puddles of stagnant water are everywhere and the trees press close. There's a small wooden shed here.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp", MazeLandmark = true,
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_LumbridgeSwampShed"),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    GatheringSpots = new() { "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead" }, 
                    AreaMonsters = new() { "ratGiant", "ratGiant", "frog", "frog", "frogGiant", "frogGiant" },
                    ItemSpawns = new() { new("swampTar", 1, null, 3) }
                });

                index = 10;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp", "Misthalin") {
                    Description = "The ground here is spongy and the footing unstable due to all the moisture. A constant shroud of fog and darkness hangs over this place, making it very hard to see where you're going. Puddles of stagnant water are everywhere and the trees press close.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp",
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    GatheringSpots = new() { "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead" }, 
                    AreaMonsters = new() { "ratGiant", "ratGiant", "frog", "frog", "frog", "frogBig", "frogBig" },
                    ItemSpawns = new() { new("swampTar", 1, null, 3) }
                });

                index = 11;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp Coast", "Misthalin") {
                    Description = "The marshy ground of the swamp fades to the sand and harder packed dirt of the river banks here. Across the river is the desert town of Al Kharid. Fish seem to be gathering at a few spots along the banks.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    GatheringSpots = new() { "treeDead", "treeDead", "treeDead", "treeDead", "fishNetSmall", "fishNetSmall", "fishBaitLow", "fishBaitLow" } 
                });

                index = 12;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp Coast", "Misthalin") {
                    Description = "The marshy ground of the swamp fades to the sand and harder packed dirt of the river banks here. Across the water is the Wizard Tower island, the tower spearing up into the clouds.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp",
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    GatheringSpots = new() { "treeDead", "treeDead" }
                });

                index = 13;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp", "Misthalin") {
                    Description = "The ground here is spongy and the footing unstable due to all the moisture. A constant shroud of fog and darkness hangs over this place, making it very hard to see where you're going. Puddles of stagnant water are everywhere and the trees press close.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp",
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    AreaMonsters = new() { "ratGiant", "ratGiant" }
                });

                index = 14;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp", "Misthalin") {
                    Description = "The ground here is spongy and the footing unstable due to all the moisture. A constant shroud of fog and darkness hangs over this place, making it very hard to see where you're going. Puddles of stagnant water are everywhere and the trees press close. An ancient stone altar emits a mysterious blue light here, surrounded by some crumbling stone pillars.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp", MazeLandmark = true,
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("RunecraftAltarWater", new() { new("Item", 1, "talismanWater", false) }),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    GatheringSpots = new() { "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead", "treeDead" }, 
                    AreaMonsters = new() { "ratGiant", "ratGiant", "frog", "frog", "frog", "frogBig", "frogBig" },
                    ItemSpawns = new() { new("swampTar", 1, null, 3) }
                });

                index = 15;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp", "Misthalin") {
                    Description = "The ground here is spongy and the footing unstable due to all the moisture. A constant shroud of fog and darkness hangs over this place, making it very hard to see where you're going. Puddles of stagnant water are everywhere and the trees press close.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp",
                    ConnectedLocations = new List<Connection>() {  
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    }, 
                    AreaMonsters = new() { "ratGiant", "ratGiant", "frog", "frog", "frog" },
                    ItemSpawns = new() { new("swampTar", 1, null, 3) }
                });

                index = 16;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp", "Misthalin") {
                    Description = "The ground here is spongy and the footing unstable due to all the moisture. A constant shroud of fog and darkness hangs over this place, making it very hard to see where you're going. Puddles of stagnant water are everywhere and the trees press close.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp",
                    ConnectedLocations = new List<Connection>() {  
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    }, 
                    AreaMonsters = new() { "ratGiant", "ratGiant", "frog", "frog", "frog", "frogBig", "frogBig" }
                });

                index = 17;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp Coast", "Misthalin") {
                    Description = "The marshy ground of the swamp fades to the sand and harder packed dirt of the river banks here. Across the river is the desert town of Al Kharid. Fish seem to be gathering at a few spots along the banks.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    GatheringSpots = new() { "treeDead", "treeDead", "treeDead", "treeDead", "fishNetSmall", "fishNetSmall", "fishBaitLow", "fishBaitLow" } 
                });

                index = 18;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp Coast", "Misthalin") {
                    Description = "The marshy ground of the swamp fades to the sand and harder packed dirt of the river banks here. Across the water is the Wizard Tower island, the tower spearing up into the clouds. There's a small mine here offering a few useful ore nodes.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp", MazeLandmark = true,
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    GatheringSpots = new() { "oreIron", "oreIron", "oreIron", "oreCoal", "oreCoal", "oreCoal", "oreMithril", "oreMithril", "oreAdamant", "oreAdamant" },
                    ItemSpawns = new() { new("seaweed", 1) }
                });

                index = 19;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp Coast", "Misthalin") {
                    Description = "The marshy ground of the swamp fades to the sand and harder packed dirt of the river banks here. This part of the coast borders a bay, and both the Wizard Tower and part of the desert to the south are visible here.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp",
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    ItemSpawns = new() { new("seaweed", 1) }
                });

                index = 20;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp Coast", "Misthalin") {
                    Description = "The marshy ground of the swamp fades to the sand and harder packed dirt of the river banks here. This part of the coast borders a bay, and both the Wizard Tower and part of the desert to the south are visible here.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp",
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    ItemSpawns = new() { new("seaweed", 1) }
                });

                index = 21;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp Coast", "Misthalin") {
                    Description = "The marshy ground of the swamp fades to the sand and harder packed dirt of the river banks here. This part of the coast borders a bay, and both the Wizard Tower and part of the desert to the south are visible here. A small wooden shack is on the coast. Seems like a terrible place to live, unless you're seeking isolation from anyone who might pass by.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp", MazeLandmark = true,
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_LumbridgeSwampUrhney"),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    ItemSpawns = new() { new("seaweed", 1) }
                });

                index = 22;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp Coast", "Misthalin") {
                    Description = "The marshy ground of the swamp fades to the sand and harder packed dirt of the river banks here. This part of the coast borders a bay, and both the Wizard Tower and part of the desert to the south are visible here.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp",
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    }, 
                    AreaMonsters = new() { "ratGiant", "ratGiant" },
                    ItemSpawns = new() { new("seaweed", 1) }
                });
                 
                index = 23;
                locsToAdd.Add(new Location("MIST_LumbridgeSwamp" + index, "Lumbridge Swamp Coast", "Misthalin") {
                    Description = "The marshy ground of the swamp fades to the sand and harder packed dirt of the river banks here. Across the water is the Wizard Tower island, the tower spearing up into the clouds. There's a small mine here offering a few useful ore nodes.",
                    MazeTile = index, MazeMap = "Lumbridge Swamp", MazeLandmark = true,
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 0, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -1, 0, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, -1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, 2, 1, 6, 4)),
                        new Connection("MIST_LumbridgeSwamp" + Helper.MapSpot(index, -2, 1, 6, 4)),
                    },
                    GatheringSpots = new() { "oreCopper", "oreCopper", "oreCopper", "oreTin", "oreTin", "oreTin", "rockClay", "rockClay" },
                    ItemSpawns = new() { new("seaweed", 1) }
                });

                locsToAdd.Add(new Location("MIST_LumbridgeSwampUrhney", "Father Urhney's Shack", "Misthalin") {
                    Description = "Despite being located in a swamp, the inside of this shack is very cozy. It is well lit by torches with a nice rug spread on the floor. There are some crates and barrels stacked against the walls. One bookshelf holds various dishes and cooking implements, while two others are stuffed with ragged books. There's a desk with a spare pair of leather gloves on it and a padded chair to sit while you work, plus a rocking chair in the corner and a small but comfortable cot in another corner. A portrait of a mysterious figure hangs on one wall.",
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_LumbridgeSwamp21")
                    },
                    ItemSpawns = new() { new("vambracesLeather", 1) }, 
                    NPCsHere = new() { "mistLumUrhney" },
                    GatheringSpots = new() { "clueBookcase" }
                });

                locsToAdd.Add(new Location("MIST_LumbridgeSwampShed", "Swamp Shed", "Misthalin") {
                    Description = "There's not really much in this shed, which appears to have been used to hold farming tools at some point. There's still a spade here among the empty crates, barrels, and sacks. A shelf is pushed up against one wall but holds nothing. A few windows look out onto the... beautiful... surroundings and let in all that lovely swampy air.",
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_LumbridgeSwamp9"),
                        new Connection("MIST_Zanaris")
                    },
                    ItemSpawns = new() { new("shovel", 1) },
                    GatheringSpots = new() { "clueCrates" }
                });
            }

            // Lumbridge Swamp Cave
            {
                locsToAdd.Add(new Location("MIST_LumbridgeSwampCaveEntrance", "Lumbridge Swamp Cave Entrance", "Misthalin") {
                    Description = "A very dim and disgusting underground cave. There aren't any pools of muck or monsters here but you can already smell the swamp gas getting stronger. There's a narrow passage you can squeeze through to get into a larger cavern, and your rope is dangling down from the surface. The hole is so far up that what little light it provides isn't illuminating anything down here.",
                    DungeoneeringLevel = 20, Dark = true,
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwamp7"),
                        new Connection("MIST_LumbridgeSwampCaveCavern") // TODO: Add wall beasts that hurt you if you aren't wearing a spiky helm or whatever
                    }
                });

                locsToAdd.Add(new Location("MIST_LumbridgeSwampCaveCavern", "Lumbridge Swamp Cavern", "Misthalin") {
                    Description = "This part of the cavern branches off with tunnels in a few directions, the monsters that reside here largely leaving eachothers territory alone. There are a few pools of stagnant muck you need to avoid falling in, for the good of your equipment. One passage leading off the cavern - little more than a crack - leads towards the entrance of the caves.",
                    DungeoneeringLevel = 20, Dark = true, Hazard = "Gas",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwampCaveEntrance"),
                        new Connection("MIST_LumbridgeSwampCaveFrog"),
                        new Connection("MIST_LumbridgeSwampCaveBug"),
                        new Connection("MIST_LumbridgeSwampCaveSlime"),
                        new Connection("MIST_LumbridgeSwampCaveCrawler"),
                        new Connection("MIST_LumbridgeSwampCaveSlug"),
                        new Connection("MIST_LumbridgeSwampCaveSecondary")
                    }
                });

                locsToAdd.Add(new Location("MIST_LumbridgeSwampCaveFrog", "Lumbridge Swamp Cave - Spawning Ground", "Misthalin") {
                    Description = "A small dank part of the cavern system largely dominated by a large pond that the hordes of frogs here appear to be spawning in. Their croaks bounce off the walls and echo to create a deafening cacophany that is quite unpleasant.",
                    DungeoneeringLevel = 20, Dark = true,
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwampCaveCavern")
                    },
                    AreaMonsters = new() { "frog", "frog", "frogBig24", "frogBig24", "frogBig24", "frogBig24", "frogGiant99" },
                    GatheringSpots = new() { "fishNetSmallCave", "fishNetSmallCave" }
                });

                locsToAdd.Add(new Location("MIST_LumbridgeSwampCaveBug", "Lumbridge Swamp Cave - Bug Hive", "Misthalin") {
                    Description = "This part of the cave is crawling with cave bugs roaming around on every surface, periodically coming out of or going into a big pile that looks sort of like an anthill. In addition to the passage leading back to the main cavern, there is a smaller passage leading to part of a mine.",
                    DungeoneeringLevel = 20, Dark = true,
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwampCaveCavern"),
                        new Connection("MIST_DorgeshuunMine")
                    },
                    AreaMonsters = new() { "caveBug", "caveBug", "caveBug", "caveBug", "caveBug" }
                });

                locsToAdd.Add(new Location("MIST_LumbridgeSwampCaveSlime", "Lumbridge Swamp Cave - Slime Pit", "Misthalin") {
                    Description = "Less of a room or cavern or even a tunnel, this is more of a dent in the ground swarming with living blobs of slime oozing around. The smell of swamp gas is particularly strong here, giving you a feeling that these slimes may be at least partially responsible for the miasma.",
                    DungeoneeringLevel = 20, Dark = true, Hazard = "Gas",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwampCaveCavern")
                    },
                    AreaMonsters = new() { "caveSlime", "caveSlime", "caveSlime", "caveSlime", "caveSlime" }
                });

                locsToAdd.Add(new Location("MIST_LumbridgeSwampCaveCrawler", "Lumbridge Swamp Cave - Crawler Den", "Misthalin") {
                    Description = "The cave crawlers appear to have laid claim to one of the larger ponds in the area, sliding in and out of it as they patrol their territory. Even without getting close you can see the poison dripping from their slavering maws, and the barbed spines coming from their backs seem like they'd hurt to get stuck with.",
                    DungeoneeringLevel = 20, Dark = true,
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwampCaveCavern")
                    },
                    AreaMonsters = new() { "caveBug", "caveBug", "caveBug" }
                });

                locsToAdd.Add(new Location("MIST_LumbridgeSwampCaveSlug", "Lumbridge Swamp Cave - Crawler Den", "Misthalin") {
                    Description = "This territory in the caves is at least slightly cleaner than the others, by virtue of a lack of water features and the resident monster being slugs composed mostly of rocks. You still can't fathom why Father Urhney would want to live anywhere near this, but it's a nice reprieve from how disgusting the rest of the cave has been. Besides the large passage leading to the main cavern, there is a smaller passage leading somewhere deeper in the caves.",
                    DungeoneeringLevel = 20, Dark = true,
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwampCaveCavern")
                    },
                    AreaMonsters = new() { "rockSlug", "rockSlug", "rockSlug" }
                });

                locsToAdd.Add(new Location("MIST_LumbridgeSwampCaveSecondary", "Lumbridge Swamp Cave - Secondary Cavern", "Misthalin") {
                    Description = "This farther back section of the cave is quite distant from the exit at this point, and a cave goblin seems to be here minding a writhing pool of eels. The smell of swamp gas is quite strong back here.",
                    DungeoneeringLevel = 20, Dark = true, Hazard = "Gas",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwampCaveCavern")
                    },
                    AreaMonsters = new() { "goblinCave", "caveSlime" },
                    GatheringSpots = new() { "fishBaitLowCave", "fishBaitLowCave" }
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
