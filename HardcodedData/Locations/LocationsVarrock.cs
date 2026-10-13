using ZeroPlayersOnline.DataTypes; 

namespace ZeroPlayersOnline.Hardcodes {
    public class LocationsVarrock {
        public static void InitLocs(Dictionary<string, Location> Atlas, Dictionary<string, GatheringTile> Gathers, Dictionary<string, AreaMonster> Monsters) {
            List<Location> locsToAdd = new();
              
            locsToAdd.Add(new Location("MIST_VarrockCrossroadsSouth", "Varrock - South Crossroads", "Misthalin") {
                Description = "A crossroads leading between Varrock, Lumbridge, Al Kharid, and the Digsite. There's a broken cart of a traveling trader here, but it seems to contain suspiciously little of value for a traveling trader. A few tables are placed haphazardly near the crossroads for some reason but there is otherwise little here besides some trees and grass.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockOutskirtsSouth"),
                    new Connection("MIST_VarrockGowerFarm"),
                    new Connection("MIST_VarrockMineEast"),
                    new Connection("MIST_VarrockCrossroadsEast"),
                    new Connection("DES_AlKharidMineOutside"),
                    new Connection("MIST_GroatsFarm")
                },
                NPCsHere = new() { "mistVarBurgiss", "mistVarAliLeaflet" },
                AreaMonsters = new() { "goblin", "unicorn", "unicorn" },
                GatheringSpots = new() { "treeOak", "treeOak", "treePine", "treePine", "treePine", "treePine", "treePine" }
            });

            locsToAdd.Add(new Location("MIST_VarrockGowerFarm", "Varrock - Gower Farm", "Misthalin") {
                Description = "This is a small fenced in farmstead with a... giant rat pen? And a pond with some swans in it. There are a handful of troughs placed around the pen and a small grove of trees growing on the north side of the farmhouse. A path up the side of the farm leads to the Varrock East Mine.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockMineEast"),
                    new Connection("MIST_VarrockOutskirtsSouth"),
                    new Connection("MIST_VarrockCrossroadsSouth")
                },
                NPCsHere = new() { "farmerMaster" },
                AreaMonsters = new() { "ratGiant", "ratGiant", "ratGiant", "ratGiant" },
                GatheringSpots = new() { "treeWillow", "treeWillow", "treeMaple", "treeMaple", "treeMaple", "treeMaple", "treeYew", "treeElder", "plantCabbage", "plantCabbage" },
                ProcessingStations = new() { "Sink", "Spinning Wheel", "Dairy Churn" }
            });

            locsToAdd.Add(new Location("MIST_VarrockMineEast", "Varrock - East Mine", "Misthalin") {
                Description = "A small divot in the ground fenced in on three sides by a wooden fence. There are a few types of ores available here, plus a giant rat and a black bear wandering around a little outside the fence. A path passes by one side leading south to the Varrock South Crossroads and north to the Varrock East Crossroads.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockOutskirtsSouth"),
                    new Connection("MIST_VarrockCrossroadsEast"),
                    new Connection("MIST_VarrockGowerFarm"),
                    new Connection("MIST_VarrockCrossroadsSouth")
                }, 
                AreaMonsters = new() { "ratGiant", "bearBlack" },
                GatheringSpots = new() { "oreCopper", "oreCopper", "oreTin", "oreTin", "oreIron", "oreIron", "oreMithril", "oreMithril", "oreMithril", "oreAdamant" }
            });

            locsToAdd.Add(new Location("MIST_VarrockOutskirtsSouth", "Varrock - Outskirts South", "Misthalin") {
                Description = "Just outside the southern gates into the city of Varrock. For some reason some dark wizards are allowed to congregate at a small ritual stite, consisting of some stone pillars around a low table that might be a sacrificial altar. The stone walls surrounding the city are imposing, giving a feeling of security and civilization. You could enter through the gate, or skirt up around the west side of the city walls by passing through the mine.",
                ConnectedLocations = new List<Connection>() { 
                    new Connection("MIST_VarrockSouth"),
                    new Connection("MIST_VarrockMineWest"),
                    new Connection("MIST_VarrockMineEast"),
                    new Connection("MIST_VarrockGuildChampions"),
                    new Connection("MIST_VarrockGowerFarm"),
                    new Connection("MIST_VarrockCrossroadsSouth")
                }, 
                NPCsHere = new() { "guard", "guard", "guard" },
                AreaMonsters = new() { "guard", "guard", "guard", "wizardDark7", "wizardDark7", "wizardDark7", "wizardDark20", "wizardDark20" },
                GatheringSpots = new() { "treePine", "treePine", "treePine", "treePine", "treeOak", "treeOak", "plantNettles", "plantCadava", "plantCadava", "plantCadava" }
            }); 

            locsToAdd.Add(new Location("MIST_VarrockMineWest", "Varrock - West Mine", "Misthalin") {
                Description = "A small mine dug into the side of a hill, exposing some ores. There's a canoe station here to travel up and down the River Lum, the Champion's Guild, and a patch to grow bushes in. You could skirt up around the west side of the city walls.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockCrossroadsWest"), 
                    new Connection("MIST_VarrockGuildChampions"),
                    new Connection("MIST_VarrockOutskirtsSouth"),
                    new Connection("MIST_LumbridgeAcrossLum", null, false, 30, "Woodcutting", "(Canoe to Lumbridge)", false, 12),
                    new Connection("MIST_BarbarianVillage", null, false, 30, "Woodcutting", "(Canoe to Barbarian Village)", false, 12),
                    new Connection("MIST_Edgeville", null, false, 60, "Woodcutting", "(Canoe to Edgeville)", false, 27),
                    new Connection("WILD_FeroxEnclave", null, false, 150, "Woodcutting", "(Canoe to Ferox Enclave)", false, 57),
                    new Connection("WILD_WildernessPond", null, false, 150, "Woodcutting", "(Canoe to Wilderness Pond)", false, 57)
                }, 
                FarmingPatchesHere = new() { "MIST_VarBush" },
                GatheringSpots = new() { "oreCopper", "oreCopper", "oreCopper", "oreTin", "oreTin", "oreIron", "oreIron", "oreIron", "oreIron", "oreMithril", "oreMithril", "oreMithril" }
            }); 

            locsToAdd.Add(new Location("MIST_VarrockCrossroadsWest", "Varrock - West Crossroads", "Misthalin") {
                Description = "Just outside the western gates into the city of Varrock. There are a variety of buildings outside the walls here, including the Cook's Guild and a path to the Grand Exchange. Gertrude also lives here, across the road from the Leptoc Mansion, and there's a small locked shack. A bridge to the west leads to Barbarian Village. You could also pass through the gates into Varrock proper.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockWest"),
                    new Connection("MIST_VarrockGuildCooks", new() { new("Wearing", 1, "chefHat", false), new("Skill", 32, "Cooking") }),
                    new Connection("MIST_VarrockGrandExchange"),
                    new Connection("MIST_VarrockGertrude"),
                    new Connection("MIST_VarrockLeptocMansion", [ new("Wearing", 1, "disguiseCleaner"), new("QuestPast", 0, "MI_RomeoJuliet") ], hideIfNoReqs: true),
                    new Connection("MIST_VarrockCrossroadsWest", [ new("NotWearing", 1, "disguiseCleaner"), new("QuestPast", 0, "MI_RomeoJuliet") ], alt: "Leptoc Mansion", hideIfNoReqs: true) { CutscenesOnTraverse = [ "MI_rjButler" ]},
                    new Connection("MIST_VarrockCrossroadsWest", [ new("QuestAt", -1, "MI_RomeoJuliet") ], alt: "Leptoc Mansion2", hideIfNoReqs: true) { CutscenesOnTraverse = [ "MI_rjNotStarted" ]},
                    new Connection("MIST_VarrockLeptocBalcony") { CutscenesOnTraverse = [ "MI_rjSerenade", "MI_rjFinal" ]},
                    new Connection("MIST_VarrockGiantShack", new() { new("Item", 1, "keyVarrockShack", false) }),
                    new Connection("MIST_VarrockOutlawCamp"),
                    new Connection("MIST_BarbarianVillage"),
                    new Connection("MIST_VarrockMineWest")
                    // TODO: There can be a portal to Puro Puro in the wheat field here, add that later when Puro Puro is added
                }, 
                NPCsHere = new() { "guard", "guard", "guard", "dogStray" },
                AreaMonsters = new() { "guard", "guard", "guard" },
                GatheringSpots = new() { "treePine", "treePine", "treePine", "treePine", "treeOak", "treeOak", "plantGrain", "plantGrain" }
            });   

            locsToAdd.Add(new Location("MIST_VarrockGuildCooks", "Cook's Guild", "Misthalin") {
                Description = "A guild for master chefs... or at least chefs with a proper hat. The upper floors extend up to a windmill gearing system, allowing you to grind grain to flour inside the building. There are numerous free items for chefs to use, and some facilities to help with the cooking process. Romily Weaklax offers a selection of pies in his shop, and the Head Chef could sell you the Cooking Cape of Accomplishment if you have truly mastered the art of cooking.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockGuildCooksVIP", new() { new("Skill", 99, "Cooking"), new("DiaryComplete", 1, "Varrock", misc3: "Hard") }, true),
                    new Connection("MIST_VarrockCrossroadsWest")
                },
                ItemSpawns = new() { new("chocolateBar", 5), new("pieEmpty", 1), new("tinCakeEmpty", 1), new("bowlEmpty", 1), new("fruitApple", 3, count: 3), new("grapes", 5), new("potEmpty", 1), new("jugEmpty", 1) },
                ProcessingStations = new() { "Windmill", "Sink", "Dairy Churn" },
                NPCsHere = new() { "mistVarHeadChef" },
                ShopItemsHere = new() { "bookRecipesPie", "potFlour", "pieEmpty", "pieRedberry", "pieMeat", "pieMud", "pieApple", "pieGarden", "pieFish", "pieAdmiral", "pieWild", "pieSummer" }
            });

            locsToAdd.Add(new Location("MIST_VarrockGuildCooksVIP", "Cook's Guild - VIP Area", "Misthalin") {
                Description = "A luxury VIP area for true master chefs, or anyone to have earned the privilege by completing many tasks around Varrock. Okay it's not actually that luxurious, this is really just a small extra room off the main Guild room where there's a range and a bank.",
                IsBank = true,
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockGuildCooks")
                },
                ProcessingStations = new() { "Range", "GE Bank" }
            });

            locsToAdd.Add(new Location("MIST_VarrockGrandExchange", "The Grand Exchange", "Misthalin") {
                Description = "A large stone plaza enclosed by walls, with a large circular canopy in the middle covering some bank booths where trades are facilitated. The north end of the exchange has a grassy area with some trees that can be cut down. In the south-east corner is the Clan Hub - a little pointless, now. The south-west corner has the Game Zone where people could play any of a few board games together. The north-east corner has a spirit tree, and the north-west has both a tunnel under the wall to just outside Edgeville and a trapdoor leading to Keldagrim.",
                IsBank = true,
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockCrossroadsWest"),
                    new Connection("MIST_VarrockNWofGE", skill: "Agility", lv: 21, alt: "(Underwall tunnel)"),
                    new Connection("FREM_KeldagrimMinecarts")
                },
                ProcessingStations = new() { "Grand Exchange" },
                GatheringSpots = new() { "treePine", "treePine", "treePine", "treePine", "treePine" }
            });

            locsToAdd.Add(new Location("MIST_VarrockGertrude", "Gertrude's House", "Misthalin") {
                Description = "A small house just outside Varrock where Gertrude lives with her four sons. While it is nothing luxurious, it is well maintained and tidy. In the yard outside there are a swingset, some toys, and a washing line for clothes. Around back are some bushes of doogle leaves. The inside of the house is divided into four rooms. The common room is decorated with a fur rug and a painting on the wall, also having two bookshelves and a table with some chairs for people to eat at it. The kids bedroom has two bunkbeds and a chest of drawers, and toys all over the floor. The room where Gertrude and her husband sleep just has a bed and some drawers as a nightstand. The kitchen has a range, some shelves to hold pots and pans, and a barrel and crate to hold food. There are also a few cat beds around the house.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockCrossroadsWest")
                },
                GatheringSpots = new() { "plantDoogle", "plantDoogle", "plantDoogle", "clueDrawers", "clueCrates" },
                NPCsHere = new() { "mistVarGertrude" },
                ProcessingStations = new() { "Range" }
            });

            locsToAdd.Add(new Location("MIST_VarrockLeptocMansion", "Leptoc Mansion", "Misthalin") {
                Description = "The entry hall to a well appointed mansion. This particular room is sparsely decorated, containing little more than some shelves, a grandfather clock, a Varrockian rug, and some stuffed heads mounted on the wall. The heads include a red dragon, a black dragon, a kurask, and inexplicably a sea bass. ",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockCrossroadsWest"),
                    new Connection("MIST_VarrockLeptocKitchen"),
                    new Connection("MIST_VarrockLeptocDining"),
                    new Connection("MIST_VarrockLeptocUpstairs")
                },
                GatheringSpots = new() { "leptocClean" },
                NPCsHere = new() { "mistVarDraul" }
            });

            locsToAdd.Add(new Location("MIST_VarrockLeptocKitchen", "Leptoc Mansion - Kitchen", "Misthalin") {
                Description = "A surprisingly lived-in kitchen. Various shelves are stacked with cups, dishes, cutlery, and other odds and ends. There's a table with some chairs around it, but the table is currently covered with papers and ledgers. A few extra chairs for the dining room are stacked in a corner and a large plush green rug covers much of the floor. Seems like a bit of a fire hazard to have a rug next to a range.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockLeptocMansion"),
                    new Connection("MIST_VarrockLeptocDining")
                },
                GatheringSpots = new() { "leptocClean" },
                ProcessingStations = new() { "Range" }
            });

            locsToAdd.Add(new Location("MIST_VarrockLeptocDining", "Leptoc Mansion - Dining Room", "Misthalin") {
                Description = "This room is quite opulently decorated, with a fancy mahogany table and chairs dominating the center of the room. Some silver candelabras hang on the walls to provide light when it's too dark out for the windows to light the room. A few shelves hold vases and other knick-knacks. In one nook of the room there's a lovely little potted plant and a painting on the wall.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockLeptocMansion"),
                    new Connection("MIST_VarrockLeptocKitchen")
                },
                GatheringSpots = new() { "leptocClean" }
            });

            locsToAdd.Add(new Location("MIST_VarrockLeptocUpstairs", "Leptoc Mansion - Upstairs Hall", "Misthalin") {
                Description = "This room is slightly better decorated than the downstairs hall. It has some silver candelabras for light on the walls, a Varrockian rug, and a bookshelf. The walls also have a mounted swordfish and smaller shelves full of books, and there is a grand piano.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockLeptocMansion"),
                    new Connection("MIST_VarrockLeptocStorage"),
                    new Connection("MIST_VarrockLeptocJuliet")
                },
                GatheringSpots = new() { "leptocPiano", "leptocClean" }
            });

            locsToAdd.Add(new Location("MIST_VarrockLeptocJuliet", "Leptoc Mansion - Juliet's Room", "Misthalin") {
                Description = "This room is... *very* pink. Ping rugs, pink bedspread, pink fabric on the chairs. Lots of shelves holding books and knick-knacks, along with pretty potted plants. A small rack holds some pairs of shoes, and there's a wardrobe near the bed. There's a door leading out to a balcony, perfect for professions of undying love under the moonlight.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockLeptocUpstairs")
                },
                GatheringSpots = new() { "leptocClean" },
                NPCsHere = [ "mistVarJulietStart" ]
            });

            locsToAdd.Add(new Location("MIST_VarrockLeptocStorage", "Leptoc Mansion - Storage", "Misthalin") {
                Description = "Really more of a closet than a room. Contains nothing but a bookshelf containing some dusty tomes, a crate, and a barrel.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockLeptocUpstairs")
                },
                GatheringSpots = new() { "clueBarrel", "clueBookcase", "clueCrates", "leptocClean" }
            });

            locsToAdd.Add(new Location("MIST_VarrockLeptocBalcony", "Leptoc Mansion - Below the Balcony", "Misthalin") {
                UPCsong = "Everytime We Touch",
                Description = "Right below the balcony of Leptoc Mansion, among the flowers and tree of the garden. There's a short stone wall circling much of the garden.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockCrossroadsWest")
                },
                NPCsHere = [ "mistVarRomeoBalcony", "mistVarJulietBalcony" ]
            });

            locsToAdd.Add(new Location("MIST_VarrockCrossroadsEast", "Varrock - East Crossroads", "Misthalin") {
                Description = "Just outside the eastern gates into the city of Varrock. There are a variety of buildings outside the walls here, including the Lumberyard and a path to the Digsite. There's also a secluded bar with a depressing atmosphere, a path to Silvarea, and a strange stone ruin emitting a soft brown light. You could also pass through the gates into Varrock proper.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockEast"),
                    new Connection("MIST_VarrockCrossroadsNorth"),
                    new Connection("MIST_VarrockJollyBoar"),
                    new Connection("MIST_VarrockLumberyard"),
                    new Connection("RunecraftAltarEarth", new() { new("Item", 1, "talismanEarth", false) }),
                    new Connection("MIST_VarrockChaosTunnel", new() { new("Quest", 10, "MI_WhatLiesBelow", false) }, hideIfNoReqs: true),
                    new Connection("MIST_Silvarea"),
                    new Connection("MIST_Digsite"),
                    new Connection("MIST_VarrockMineEast"),
                    new Connection("MIST_VarrockCrossroadsSouth")
                }, 
                NPCsHere = new() { "mistVarAnnaJones", "guard", "guard", "guard" },
                AreaMonsters = new() { "guard", "guard", "guard", "imp" },
                GatheringSpots = new() { "treePine", "treePine", "treePine", "treePine", "treeOak", "treeOak", "treeYew", "treeYew" }
            });

            locsToAdd.Add(new Location("MIST_VarrockLumberyard", "Lumberyard", "Misthalin") {
                Description = "This place has definitely seen better days, though the store at the front is somehow still functional despite the rest of the lumberyard falling apart. The lumberyard is surrounded by a short wooden fence, broken at one point near the front where you could hop over into the crumbling ruins if you wanted.",
                ConnectedLocations = new List<Connection>() { 
                    new Connection("MIST_VarrockCrossroadsNorth"),
                    new Connection("MIST_VarrockCrossroadsEast"),
                    new Connection("MIST_Silvarea"),
                    new Connection("MIST_VarrockLumberyardInside", alt: "(Hop Broken Fence)")
                },
                ShopPriceMultiplier = 1.3,
                ShopItemsHere = new() { "saw", "hammer", "clothBolt", "candle", "nailsBronze", "plankPine", "barBronze" },
                ProcessingStations = new() { "Sawmill" }
            });

            locsToAdd.Add(new Location("MIST_VarrockLumberyardInside", "Lumberyard - Interior", "Misthalin") {
                Description = "This place has definitely seen better days. All of the buildings are crumbling and don't seem to really serve a purpose anymore. There are a bunch of crates and some dead trees in here, alongside stacks of rotting lumber. You can hop back over the broken fence to get back out of here.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockLumberyard", alt: "(Hop Broken Fence)")
                },
                GatheringSpots = new() { "treeDead", "treeDead", "treeDead", "clueCrates", "clueCrates", "clueCrates", "gertrudeCrate", "clueCrates", "clueCrates", "clueBarrel" },
                NPCsHere = new() { "mistVarFluffs" }
            });

            locsToAdd.Add(new Location("MIST_VarrockCrossroadsNorth", "Varrock - North Crossroads", "Misthalin") {
                Description = "Squeezed between the northern wall of Varrock and the border into the Wilderness, there's not much room for anything of interest here. If not entering Varrock through the gates or going to the Wilderness, the only other real options are to skirt around the city walls to the west or south-east, or to visit the Jolly Boar.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockAlongNorthWall"), 
                    new Connection("MIST_VarrockNorth"), 
                    new Connection("MIST_VarrockJollyBoar"),   
                    new Connection("MIST_VarrockCrossroadsEast")
                }, 
                NPCsHere = new() { "guard", "guard", "guard" },
                AreaMonsters = new() { "guard", "guard", "guard", "knightBlackNA", "knightBlackNA" },
                GatheringSpots = new() { "treePine", "treePine", "treePine", "treePine", "treeOak", "treeOak" },
                ItemSpawns = new() { new("runeEarth", 1) }
            });

            locsToAdd.Add(new Location("MIST_VarrockJollyBoar", "Jolly Boar", "Misthalin") {
                UPCsong = "Pink Pony Club",
                Description = "Given how out of the way it is and the general atmosphere of grime and sadness, it's not hard to see why the Jolly Boar sees so little foot traffic. The common room is fairly spacious, all things considered, with a lot of empty tables and chairs scattered around. A bored bartender is on duty behind the bar, one of the few places largely free of dust in the establishment. Despite them not offering food of any kind, there's a kitchen with a cook who claims to be perpetually busy.",
                ConnectedLocations = new List<Connection>() { 
                    new Connection("MIST_VarrockCrossroadsNorth"),
                    new Connection("MIST_VarrockCrossroadsEast"),
                    new Connection("MIST_VarrockJollyBoarKitchen")
                }, 
                NPCsHere = new() { "mistVarJollyBartender", "man", "woman", "woman" },
                AreaMonsters = new() { "knightBlackNA", "thiefNA", "man", "woman", "woman" },
                ShopItemsHere = new() { "beer" }
            });

            locsToAdd.Add(new Location("MIST_VarrockJollyBoarKitchen", "Jolly Boar Kitchen", "Misthalin") {
                Description = "...Okay, having seen the kitchen it's probably a good thing they don't offer food here. The room is small, cramped, dirty, and too hot from the two ovens running constantly. A handful of shelves are bolted to the walls and stacked with dishes and cutlery, dubious spice jars, and a small vase of wilted flowers and dirty water.",
                ConnectedLocations = new List<Connection>() { 
                    new Connection("MIST_VarrockJollyBoar")
                }, 
                NPCsHere = new() { "mistVarJollyCook" },
                ProcessingStations = new() { "Range" }
            });

            locsToAdd.Add(new Location("MIST_VarrockPalaceBailey", "Varrock Palace Bailey", "Misthalin") {
                Description = "The bailey is spacious, containing a small path around back and a tree patch. Trees line the path up to the palace courtyard, and a few benches are off to the side to sit and enjoy the view. There's a broken cart off to one side of the bailey with some crates next to it. Banners bearing the Varrock heraldry hang everywhere.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockPalaceCourtyard"),
                    new Connection("MIST_VarrockPalaceBaileyBack"),
                    //new Connection("MIST_VarrockPalaceGarden", [ new("Skill", 35, "Agility")]), TODO: After Garden of Tranquility is implemented which is ages away
                    new Connection("MIST_VarrockNorth"),
                    new Connection("MIST_VarrockGrandExchange"),
                    new Connection("MIST_VarrockPlaza")
                },
                GatheringSpots = new() { "treePine", "treePine", "treeOak", "clueCrates" },
                ProcessingStations = new() { "Fountain" },
                NPCsHere = new() { "mistVarTreznor" },
                FarmingPatchesHere = new() { "MIST_VarTree" }
            });

            locsToAdd.Add(new Location("MIST_VarrockPalaceBaileyBack", "Behind Varrock Palace", "Misthalin") {
                Description = "Around back behind the Varrock Palace, within the bailey. There are just a few yew trees here, plus an enclosure containing a grizzly bear. There's a human skeleton in the enclosure... hopefully just decorative.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockPalaceBailey"),
                    new Connection("MIST_VarrockPalaceGarden"),
                    new Connection("MIST_VarrockGrandExchange")
                },
                GatheringSpots = new() { "treeYew", "treeYew", "treeYew" },
                ProcessingStations = new() { "Fountain" },
                AreaMonsters = new() { "imp", "bearZoo" },
                ItemSpawns = new() { new("coins", 1, null, 10, true), new("runeBody", 1) }
            });

            locsToAdd.Add(new Location("MIST_VarrockPalaceCourtyard", "Varrock Palace Courtyard", "Misthalin") {
                Description = "A small courtyard enclosed by stone walls attached to the front of the palace, with a couple fountains and a bunch of barrels and crates in it. Some guards patrol the area on the lookout for intruders. There's a small staircase up to the palace itself, with an overhang held up by stone pillars. Banners bearing the Varrock heraldry hang everywhere.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockPalaceGroundFloor"),
                    new Connection("MIST_VarrockPalaceBailey")
                },
                GatheringSpots = new() { "clueCrates" },
                ProcessingStations = new() { "Fountain" },
                AreaMonsters = new() { "guard", "guard", "guard", "guard" }
            });

            locsToAdd.Add(new Location("MIST_VarrockPalaceGroundFloor", "Varrock Palace - Ground Floor", "Misthalin") {
                Description = "The layout of this floor is... very strange. It's a long curving hallway leading around the palace, touching each room except the banquet hall. Right in front of the entrance is a staircase up to the second floor, behind which is a door to the inner courtyard. There's not even a guard blocking the door into the throne room, besides the ones out in the main courtyard. The hallway is adorned with many suits of armor and sets of swords and shields in the Varrock colors, black and yellow.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockPalace2F"),
                    new Connection("MIST_VarrockPalaceCourtyardInner"),
                    new Connection("MIST_VarrockPalaceThroneRoom"),
                    new Connection("MIST_VarrockPalaceBunkRoom"),
                    new Connection("MIST_VarrockPalaceRoom"),
                    new Connection("MIST_VarrockPalacePrysin"),
                    new Connection("MIST_VarrockPalaceStairwell1F"),
                    new Connection("MIST_VarrockPalaceLibrary"),
                    new Connection("MIST_VarrockPalaceKitchen"),
                    new Connection("MIST_VarrockPalaceCourtyard")
                },
                AreaMonsters = new() { "monkZamorakVarrock" }
            });

            locsToAdd.Add(new Location("MIST_VarrockPalaceCourtyardInner", "Varrock Palace - Inner Courtyard", "Misthalin") {
                Description = "A lovely little courtyard inside Varrock Palace. It just has a few small plants and a magic tree in the middle, with some narrow windows looking into the hallway of the ground floor of the palace.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockPalaceGroundFloor")
                },
                GatheringSpots = new() { "treeMagic" }
            });

            locsToAdd.Add(new Location("MIST_VarrockPalaceThroneRoom", "Varrock Palace - Throne Room", "Misthalin") {
                UPCsong = "Black and Yellow",
                Description = "The throne room is small but opulent. The walls are decked with banners and shields bearing the Varrock crest and colors. Several beautiful candelabras provide the light for the room, and there are high quality suits of armor placed evenly. A large rug extends out from in front of the throne with a repeating pattern on it, again in the Varrock colors. They really love black and yellow around here.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockPalaceGroundFloor"),
                    new Connection("MIST_VarrockPalaceBanquetHall")
                },
                GatheringSpots = new() { "clueChest" },
                NPCsHere = new() { "mistVarKingRoald", "mistVarAeonisig" }
            });

            locsToAdd.Add(new Location("MIST_VarrockPalaceBanquetHall", "Varrock Palace - Banquet Hall", "Misthalin") {
                Description = "The banquet hall is perhaps even more ornate than the throne room, bordering on gaudy. Many banners and shields hang from the walls, several suits of armor are placed around the edge of the room, and there are a full six cabinets of fine porcelain dishes and silver cutlery. The center of the room is dominated by a long wide table covered in a black and yellow tablecloth.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockPalaceKitchen"),
                    new Connection("MIST_VarrockPalaceThroneRoom")
                }
            });

            locsToAdd.Add(new Location("MIST_VarrockPalaceRoom", "Varrock Palace - Room", "Misthalin") {
                Description = "A small room with a single bed and a couple chests. There are paintings of a water mill and the moon rising over a river hanging on the wall, and some shelves holding various knick-knacks. A sword and shield bearing the Varrock colors hang on the walls above the bed. There's a nice fur rug dyed green spread on the floor that really ties the room together.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockPalaceGroundFloor")
                },
                GatheringSpots = new() { "clueChest" }
            });

            locsToAdd.Add(new Location("MIST_VarrockPalaceBunkRoom", "Varrock Palace - Bunk Room", "Misthalin") {
                Description = "A small room with a couple bunk beds and some chests. This room is very spartan with no decorations, but it has some shelves holding various knick-knacks. A sword and shield bearing the Varrock colors hang on the walls above each bed. There's a nice fur rug dyed green spread on the floor that really ties the room together.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockPalaceGroundFloor")
                },
                GatheringSpots = new() { "clueChest" }
            });

            locsToAdd.Add(new Location("MIST_VarrockPalaceLibrary", "Varrock Palace - Library", "Misthalin") {
                Description = "The library is full of bookshelves, stuffed with book and more books stacked on top. A few small tables hold more books for projects in progress or that patrons forgot to put away. One harried-looking bespectacled man walks between the shelves trying to organize them.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockPalaceGroundFloor")
                },
                NPCsHere = new() { "mistVarReldo", "mistVarSurokMagis" },
                GatheringSpots = new() { "bookshelfWizard", "bookshelfWizard", "bookshelfArrav", "bookshelfWizard", "bookshelfWizard", "bookshelfWizard", "bookshelfWizard" }
            });

            locsToAdd.Add(new Location("MIST_VarrockPalaceKitchen", "Varrock Palace - Kitchen", "Misthalin") {
                Description = "The palace kitchen is cramped but functional. There's a larder containing the food the castle has on hand, plus a range and sink. A table rests in the middle of the room to prepare foods on. Some shelves holding dishes and cutlery hang on the walls. There are some crates stacked in one corner next to the door out to the garden.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockPalaceGroundFloor"),
                    new Connection("MIST_VarrockPalaceBanquetHall"),
                    new Connection("MIST_VarrockPalaceBaileyBack")
                },
                ItemSpawns = new() { new("pieEmpty", 1), new("bucketEmpty", 1) },
                ProcessingStations = new() { "Range", "Sink" },
                GatheringSpots = new() { "clueCrates" }
            });

            locsToAdd.Add(new Location("MIST_VarrockPlaza", "Varrock Square", "Misthalin") {
                Description = "The beating heart of the kindom of Misthalin! The square has a nice fountain with a statue looking in each ordinal direction, and a number of small stalls hawk various goods that they won't actually let you buy.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockPalaceBailey"),
                    new Connection("MIST_VarrockPlaza", [ new("QuestAt", 110, "MI_RomeoJuliet") ], alt: "(Romeo and Tybalt's Duel)", hideIfNoReqs: true) { CutscenesOnTraverse = [ "MI_rjDuel" ]},
                    new Connection("MIST_VarrockWest"),
                    new Connection("MIST_VarrockZaff"),
                    new Connection("MIST_VarrockFortuneTeller"),
                    new Connection("MIST_VarrockGeneralStore"),
                    new Connection("MIST_VarrockThessalia"),
                    new Connection("MIST_VarrockEast"),
                    new Connection("MIST_VarrockSouth")
                }, 
                NPCsHere = new() { "mistVarRomeoStart", "mistVarBaraek", "mistVarBenny", "mistVarShilop", "mistVarWilough", "mistVarToby" },
                GatheringSpots = new() { "treePine", "treePine", "treeOak" },
                ProcessingStations = new() { "Fountain" }
            });

            locsToAdd.Add(new Location("MIST_VarrockZaff", "Zaff's Superior Staves", "Misthalin") {
                Description = "A small shop just off the Varrock Plaza, containing a few barrels and racks of staves for display. Some tools Zaff uses to make and repair the staves hang on tool racks on the walls. There's a nice blue rug covering most of the floor and some lovely green stained glass windows looking out onto the plaza. A ladder in the corner of the room leads up to the second floor, which is mostly just filled with more crates and barrels of merchandise.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockPlaza")
                }, 
                NPCsHere = new() { "mistVarZaff" },
                GatheringSpots = new() { "clueCrates" },
                ShopItemsHere = new() { "staff", "staffAir", "staffWater", "staffEarth", "staffFire", "staffMagic", "battlestaff" }
            });

            locsToAdd.Add(new Location("MIST_VarrockGeneralStore", "Varrock General Store", "Misthalin") {
                Description = "A fairly spacious storefront adjoining the Varrock Plaza - or at least it would be, if not stuffed with shelves displaying a wide variety of merchandise. Two lines of shelves are out in the open, and some more are up against the wall behind the counter. Some crates and boxes are stacked in two of the corners of the shop, and some sacks are hanging from hooks dangling from the ceiling. The two sides of the building that face the plaza have nice floor to ceiling green stained glass windows. On the counter are some scales, a ledger, and a simple cash register. There's also a ladder up to the sleeping quarters of the employees.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockPlaza")
                }, 
                NPCsHere = new() { "mistVarShopkeeperGeneral", "mistVarAssistantGeneral" },
                GatheringSpots = new() { "clueCrates", "clueBoxes" },
                ShopItemsHere = new() { "potEmpty", "jugEmpty", "shears", "bucketEmpty", "bowlEmpty", "tinCakeEmpty", "tinderbox", "chisel", "hammer", "compass" },
                MahoganyTown = "Varrock",
                MahoganyHomesHotspots = new() { "shelf", "shelf", "shelf", "shelf", "shelf", "lighting", "stool", "stool", "table", "stool", "bed", "portrait", "lighting", "portrait"  }
            });

            locsToAdd.Add(new Location("MIST_VarrockWest", "West Varrock", "Misthalin") {
                Description = "A majority of west Varrock is in ruins, most of the buildings crumbling and unoccupied except by the unfortunately large homeless population. The only buildings here worthy of note are the apothecary and the bank, though the smithy is still technically usable. The roads also lead to the plaza, the western crossroads, and the Grand Exchange, any of which are more desirable places to be than this.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockPlaza"),
                    new Connection("MIST_VarrockApothecary"), 
                    new Connection("MIST_VarrockRuinedSmithy"), 
                    new Connection("MIST_VarrockBankWest"),
                    new Connection("MIST_VarrockGrandExchange"), 
                    new Connection("MIST_VarrockCrossroadsWest")
                }, 
                NPCsHere = new() { "guard", "dogStray" },
                AreaMonsters = new() { "guard", "imp" }
            });

            locsToAdd.Add(new Location("MIST_VarrockApothecary", "Apothecary's Potions", "Misthalin") {
                Description = "One of the few functional places in west Varrock, this shop is crammed with the tools and supplies to create potions. A few bookshelves hold treatises on alchemy, while racks on the walls hold some drying herbs. A cabinet has some... questionable... things preserved in jars on it. The desk where the apothecary does most of his work acts as the store counter, though it has a large book open on it next to a pestle and mortar. At the back of the room is a table with some in-progress potions and a chart roughly approximating the periodic table of elements hangs on the wall above.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockWest")
                }, 
                NPCsHere = new() { "mistVarIngald" },
                GatheringSpots = new() { "clueCrates", "clueBoxes" },
                ShopItemsHere = new() { "potionAttack", "potionStrength", "potionAntipoison", "vialEmpty", "vialWater", "pestleMortar", "vialEmptyPack", "vialWaterPack", "eyeNewtPack"  }
            });

            locsToAdd.Add(new Location("MIST_VarrockRuinedSmithy", "Ruined Smithy", "Misthalin") {
                Description = "A crumbling shack that has seen better days. There are still some tool racks hanging on the walls. There's a barely standing table, and a few upturned or broken chairs scattered around the room. There are also a few crates but the only real thing of value in here are the anvils.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockWest")
                }, 
                ProcessingStations = new() { "Anvil", "Anvil", "Anvil" }
            });

            locsToAdd.Add(new Location("MIST_VarrockBankWest", "Varrock West Bank", "Misthalin") {
                Description = "The only well maintained building in west Varrock. The lobby is quite spacious, featuring some chairs for people to sit on as they wait for a teller to be available. There are stanchions connected by green velvet ropes separate the queues for each teller. Part of the floor is covered by a nice blue rug, and the many windows in the building looking out on the surroundings are made of lovely green stained glass. Visible behind the teller line is part of their storage area plus a few desks stacked with ledgers and weights and scales. There's a staircase leading to the basement vault area, and several tastefully placed potted plants livening up the place.",
                IsBank = true,
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockWest"), 
                    new Connection("MIST_VarrockBankWestVault", alt: "(Go Down Stairs)")
                },
                ProcessingStations = new() { "GE Bank" }
            });

            locsToAdd.Add(new Location("MIST_VarrockBankWestVault", "Varrock West Bank - Vault", "Misthalin") {
                Description = "The basement area is lit by standing candelabras and lanterns hanging from the wall. There are some desks and chairs down here for attending to various bank business away from the bustle of the teller stands. There are several shelves holding various objects, along with a bunch of crates and barrels. There's a gate separating the main basement, where you are, from the vault proper where everything important is stored. The gate is locked but you can see some items on the ground inside that could be picked up with magic. The vault area is otherwise filled with many chests and sacks containing the valuables of customers.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockBankWest", alt: "(Go Up Stairs)")
                },
                ItemSpawns = new() { new("coins", 0, null, 300, true), new("barGold", 5, null, 1, true), new("oreGold", 5, null, 1, true), new("necklaceGold", 5, null, 1, true), new("ringRuby", 5, null, 1, true) },
                GatheringSpots = new() { "clueCrates", "clueBoxes" }
            });
                     
            locsToAdd.Add(new Location("MIST_VarrockNorth", "North Varrock", "Misthalin") {
                Description = "Really more of the north-east of Varrock, this is a small section of the city just east of the palace. The salon and Saradominist chapel are found here, along with a real estate agent for those looking to purchase a home. There's a manhole leading down into the sewer system, which is infested with monsters, for the adventurous. Also Charos lives here.",
                ConnectedLocations = new List<Connection>() { 
                    new Connection("MIST_VarrockCrossroadsNorth"),
                    new Connection("MIST_VarrockHouseCharos"),
                    new Connection("MIST_VarrockChapel") { CutscenesOnTraverse = [ "MI_rjChapel" ] },
                    new Connection("MIST_VarrockEstateAgent"),
                    new Connection("MIST_VarrockSalon"),
                    new Connection("MIST_VarrockPalaceBailey"),
                    new Connection("MIST_VarrockSewerEntrance"),
                    new Connection("MIST_VarrockEast")
                }, 
                NPCsHere = new() { "guard", "man", "man", "woman", "woman" },
                AreaMonsters = new() { "guard", "man", "man", "woman", "woman", "imp" },
                GatheringSpots = new() { "treePine", "treePine", "treePine", "treePine", "treePine", "treeYew" }
            }); 

            locsToAdd.Add(new Location("MIST_VarrockHouseCharos", "Charos' House", "Misthalin") {
                Description = "A fairly large luxurious manor in the upperclass part of Varrock. The downstairs is well lit by candlebras on the walls, with a rug in the middle and a large table to receive guests. There are a handful of bookcases and cabinets placed around the edge of the room containing expensive knick-knacks. Upstairs is the bedroom, containing a four-posted bed and some storage for clothing and personal belongings. A smaller table is placed there with some chairs.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockNorth")
                },
                NPCsHere = new() { "mistVarCharos" },
                MahoganyTown = "Varrock",
                MahoganyHomesHotspots = new() { "tableLarge", "chair", "chair", "bookcase", "bookcase", "shelf",  "table", "bedLarge", "chair", "chair", "wardrobe", "wardrobe", "dresser", "lighting", "lighting"  }
            });

            locsToAdd.Add(new Location("MIST_VarrockChapel", "Varrock Chapel", "Misthalin") {
                Description = "This is a chapel dedicated to Saradomin, though perhaps somewhat blasphemously in Varrockian yellow and black rather than Saradomin blue and white. There are several rows of pews leading up to the lectern Father Lawrence preaches at, and behind that an altar. There are a great deal of candles spread around the room in candelabras, standing and wall-mounted, more on votaries, and some on the altar too. There are even a couple cabinets holding more candles.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockNorth")
                },
                NPCsHere = new() { "mistVarLawrence" },
                GatheringSpots = new() { "altar", "clueChest", "cabinetCandles" }
            });

            locsToAdd.Add(new Location("MIST_VarrockEstateAgent", "Misthalin Mortgages", "Misthalin") {
                Description = "A small office just across the road from the Varrock chapel. The entry room has a row of bookshelves lining one wall, with a small desk acting as the counter for business. The room is decorate with a nice Varrockian rug and some paintings of potential houses you could one day own. Through a door near the back is the office, where there's another desk stacked with papers and ledgers. Some shelves back here hold various construction tools, and there's a grandfather clock ticking away in one corner.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockNorth")
                },
                NPCsHere = new() { "estateAgent" },
                ItemSpawns = new() { new("saw", 1), new("bookConstructionGuide", 1) }
            });

            // Varrock Sewers
            {
                locsToAdd.Add(new Location("MIST_VarrockSewerEntrance", "Varrock Sewer - Entrance", "Misthalin") {
                    DungeoneeringLevel = 10,
                    Description = "This sewer smells about as bad as you were expecting, though perhaps not as bad as it could be if the sewer was actually connected to more buildings above. /n /n There's a path northwest where you can hear some hissing, a path to the northeast where you hear moaning, and a path east to some loud squeaking. There's also a ladder back up to the north part of Varrock.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_VarrockNorth", alt: "(Ladder)"),
                        new Connection("MIST_VarrockSewerSnakes", alt: "(Hissing)"),
                        new Connection("MIST_VarrockSewerZombies", alt: "(Moaning)"),
                        new Connection("MIST_VarrockSewerScurrius", alt: "(Squeaking)")
                    },
                    AreaMonsters = [ "ratGiant", "ratGiant", "ratGiant", "ratGiant", "ratGiant", "rat", "rat" ],
                    NPCsHere = [ "mistVarHistorianAldo" ]
                });

                locsToAdd.Add(new Location("MIST_VarrockSewerSnakes", "Varrock Sewer - Snakes", "Misthalin") {
                    DungeoneeringLevel = 10,
                    Description = "Little more than a pit to hiss in. There's a small 'water'way separating this bit from another path through the sewers, but you don't want to risk crossing and falling in. /n /n There's a path back to the south you feel an occasional breeze of fresh air coming from.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_VarrockSewerEntrance", alt: "(Breeze)")
                    },
                    AreaMonsters = [ "snake", "snake", "snake" ],
                    ItemSpawns = [ new("keyPrysin", 1, new("QuestAt", 20, "MIST_DemonSlayer"))] // TODO: actual demon slayer stage
                });

                locsToAdd.Add(new Location("MIST_VarrockSewerScurrius", "Varrock Sewer - Scurrius' Lair", "Misthalin") {
                    BossHere = "bossScurrius",
                    Description = "A large chamber full of half rotten piles of food, the home of an enormous mutated ratking. /n /n There's a path back to the west you feel an occasional breeze of fresh air coming from.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_VarrockSewerEntrance", alt: "(Breeze)")
                    }
                });

                locsToAdd.Add(new Location("MIST_VarrockSewerZombies", "Varrock Sewer - Zombies", "Misthalin") {
                    DungeoneeringLevel = 10,
                    Description = "A small cramped bit of the sewers with a few zombies stuffed into it. /n /n There's a path back to the south you feel an occasional breeze of fresh air coming from. There's also an east path with rattling coming from it, and some skittering behind a door in the north wall.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_VarrockSewerEntrance", alt: "(Breeze)"),
                        new Connection("MIST_VarrockSewerScorpions", alt: "(Skittering)"),
                        new Connection("MIST_VarrockSewerSkeletons", alt: "(Rattling)")
                    },
                    AreaMonsters = [ "zombie24", "zombie24", "zombie24" ]
                });

                locsToAdd.Add(new Location("MIST_VarrockSewerScorpions", "Varrock Sewer - Scorpions", "Misthalin") {
                    DungeoneeringLevel = 10,
                    Description = "A small area with a couple scorpions in it. There's a 'water'way along the north edge of the ground, across which is a path farther along in the sewers. You wouldn't want to try crossing it though. /n /n There's only a door back to the south with some moaning on the other side.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_VarrockSewerZombies", alt: "(Moaning)")
                    },
                    AreaMonsters = [ "scorpion", "scorpion", "scorpion" ]
                });

                locsToAdd.Add(new Location("MIST_VarrockSewerSkeletons", "Varrock Sewer - Skeletons", "Misthalin") {
                    DungeoneeringLevel = 10,
                    Description = "A narrow path along a 'water'way, you'll have to be careful not to fall in. /n /n There's a path back to the south you hear some moaning coming from, and the path to the west with ghostly wailing.",
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_VarrockSewerZombies", alt: "(Moaning)"),
                        new Connection("MIST_VarrockSewerGhosts", alt: "(Wailing)")
                    },
                    AreaMonsters = [ "skeleton25", "skeleton25", "skeleton25" ]
                });

                locsToAdd.Add(new Location("MIST_VarrockSewerGhosts", "Varrock Sewer - Ghosts", "Misthalin") {
                    DungeoneeringLevel = 10,
                    Description = "A small mostly dry room, with a few ghosts floating around in it making a racket. /n /n There's a path back to the east you hear some rattling coming from, and the path to the southwest with the skittering of many legs. There's also a ladder here up to the surface.",
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_VarrockSewerSkeletons", alt: "(Rattling)"),
                        new Connection("MIST_VarrockSewerSpiders", alt: "(Skittering)"),
                        new Connection("MIST_VarrockBearCage", alt: "(Ladder)")
                    },
                    AreaMonsters = [ "ghost19", "ghost19", "ghost19" ]
                });

                locsToAdd.Add(new Location("MIST_VarrockSewerSpiders", "Varrock Sewer - Spiders", "Misthalin") {
                    DungeoneeringLevel = 10,
                    Description = "A small spider nest crawling with giant spiders. /n /n There's a path to the northeast you hear some ghostly wailing coming from, and the path to the southwest with the skittering of even more legs.",
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_VarrockSewerGhosts", alt: "(Wailing)"),
                        new Connection("MIST_VarrockSewerRedSpiders", alt: "(More Skittering)")
                    },
                    AreaMonsters = [ "spiderGiant27", "spiderGiant27", "spiderGiant27", "spiderGiant27" ]
                });

                locsToAdd.Add(new Location("MIST_VarrockSewerRedSpiders", "Varrock Sewer - Red Spiders", "Misthalin") {
                    DungeoneeringLevel = 10,
                    Description = "A small spider nest crawling with deadly red spiders. /n /n There's a gate to the north overgrown with leaves. Past the gate you can hear the rustling of leaves. You hear the skittering of many, though fewer than here, legs to the east. To the west you hear the resounding of heavy footfalls but you'll need to cut through thick webs to go that way.",
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_VarrockSewerMossGiants", [ new("CanSlash") ], alt: "(Stomping)"),
                        new Connection("MIST_VarrockSewerBryophyta", new() { new("Item", 1, "keyMossy"), new("Data", 1, "BryophytaEverEntered", false, "equals", "Already unlocked gate with a key.") }, true, alt: "(Rustling Leaves)") { WorldStateChange = "set", WorldStateID = "BryophytaEverEntered", WorldStateNum = 1 },
                        new Connection("MIST_VarrockSewerSpiders", alt: "(Less Skittering)")
                    },
                    AreaMonsters = [ "spiderDeadlyRed", "spiderDeadlyRed", "spiderDeadlyRed", "spiderDeadlyRed", "spiderDeadlyRed", "spiderDeadlyRed" ],
                    ItemSpawns = [ new("spiderEggsRed", 2, count: 2) ]
                });

                locsToAdd.Add(new Location("MIST_VarrockSewerMossGiants", "Varrock Sewer - Moss Giants", "Misthalin") {
                    DungeoneeringLevel = 10,
                    Description = "A section of the sewers far from any 'water'ways yet still covered in moss, inhabited by moss giants. There are a few mossy boulders scattered around, and a cauldron bubbling with some kind of green liquid. /n /n There is a pipe you might be able to squeeze through, and a path to the east where you can hear the skittering of many legs, though you'll have to cut through thick webs to go that way.",
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_EdgevilleDungeonTunnels", [ new("Skill", 51, "Agility") ], alt: "(Squeeze Pipe)"),
                        new Connection("MIST_VarrockSewerRedSpiders", [ new("CanSlash") ], alt: "(Much Skittering)")
                    },
                    AreaMonsters = [ "giantMoss", "giantMoss", "giantMoss", "giantMoss", "giantMoss", "giantMoss" ],
                    ItemSpawns = [ new("hatchetIron", 2, count: 1) ]
                });
                /*
                locsToAdd.Add(new Location("MIST_VarrockSewerBryophyta", "Varrock Sewer - Moss Giants", "Misthalin") {
                    BossHere = "bossBryophyta",
                    Description = "A section of the sewers far from any 'water'ways yet still covered in moss, inhabited by moss giants. There are a few mossy boulders scattered around, and a cauldron bubbling with some kind of green liquid. /n /n There is a pipe you might be able to squeeze through, and a path to the east where you can hear the skittering of many legs, though you'll have to cut through thick webs to go that way.",
                    ConnectedLocations = new List<Connection>() { 
                        new Connection("MIST_VarrockSewerRedSpiders", alt: "(Much Skittering)")
                    }
                });*/
            }

            locsToAdd.Add(new Location("MIST_VarrockEast", "East Varrock", "Misthalin") {
                Description = "The bustling eastern district of Varrock, home to the bank, Horvik's Armor Shop, Lowe's Archery Emporium, and Jeff's house. Perhaps most notably though is the presence of the Varrock Museum, housing a number of fascinating exhibits. There's even a small fountain here, and a mostly useless training hall. So many sights to see!",
                ConnectedLocations = new List<Connection>() { 
                    new Connection("MIST_VarrockNorth"),
                    new Connection("MIST_VarrockPlaza"),
                    new Connection("MIST_VarrockHouseJeff"),
                    new Connection("MIST_VarrockHorvik"),
                    new Connection("MIST_VarrockLowe"),
                    new Connection("MIST_VarrockMuseum"),
                    new Connection("MIST_VarrockMuseumWorkers"),
                    new Connection("MIST_VarrockTrainingHall"),
                    new Connection("MIST_VarrockBankEast"),
                    new Connection("MIST_VarrockSouthEast"),
                    new Connection("MIST_VarrockCrossroadsEast")
                }, 
                NPCsHere = new() { "guard", "man", "man", "woman", "woman", "woman", "woman", "streetCleaner", "townCrier" },
                AreaMonsters = new() { "guard", "man", "man", "woman", "woman", "woman", "woman" },
                GatheringSpots = new() { "treePine", "treePine", "treePine" },
                ProcessingStations = new() { "Fountain" }
            });

            locsToAdd.Add(new Location("MIST_VarrockHouseJeff", "Jeff's House", "Misthalin") {
                Description = "A cozy but upscale house in east Varrock. The entry hall contains some shelves and a bookcase, plus a table with some chairs. There are some paintings and lanterns on the wall. Through to the storage room, there are a bunch of shelves holding various items of no particular interest. Upstairs is a spacious bedroom with a large Varrockian rug, some bookcases, the large bed, and some portraits for decoration.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockEast")
                },
                NPCsHere = new() { "mistVarJeff" },
                GatheringSpots = new() { "clueDrawers", "clueBookcase", "clueBookcase" },
                MahoganyTown = "Varrock",
                MahoganyHomesHotspots = new() { "chair", "chair", "bookcase", "table", "chair", "portrait", "shelf", "shelf", "shelf", "shelf", "shelf", "chest", "drawers", "bookcase", "bookcase", "portrait", "portrait", "portrait" }
            });

            locsToAdd.Add(new Location("MIST_VarrockTrainingHall", "Training Hall", "Misthalin") {
                Description = "A modest, largely unused, training hall. There are some scrolls hanging from the walls depicting proper stances for attacking and defending, plus some tables to rest at. Someone has stored some hay bales here in one corner due to the lack of use. The main attraction of the hall is the rows of training dummies, calibrated in two ways. The first set are a little trickier to hit, allowing you to train Attack, and the second are a little tougher, allowing you to train Strength. They'll only be any benefit up to level 8 in either skill though.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockEast")
                },
                GatheringSpots = new() { "dummyAttack", "dummyAttack", "dummyAttack", "dummyStrength", "dummyStrength", "dummyStrength" },
            });

            locsToAdd.Add(new Location("MIST_VarrockBankEast", "Varrock East Bank", "Misthalin") {
                Description = "Nobody is quite sure why Varrock needs two banks so close to eachother, but they do both exist. This one is a bit smaller than the West bank, though perhaps a little more upscale. There's a line of bank booths you can use, plus a staircase up to the second floor, which contains an area enclosed by iron bars holding a bunch of gold and chests.",
                IsBank = true,
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockEast")
                }, 
                GatheringSpots = new() { "clueDrawers" },
                ProcessingStations = new() { "GE Bank" }
            });

            locsToAdd.Add(new Location("MIST_VarrockHorvik", "Horvik's Armor Shop", "Misthalin") {
                Description = "This open-air building, more of a stone canopy than an enclosed room, is really more of a forge than a storefront. A variety of smithing tools hang on the walls and there's a drill in the back corner of the shop, along with a workbench and some anvils towards the front. A few stacked crates hold the store stock, which consists of all of the main pieces of metal armor up to adamant. Horvik does charge a premium over what other shops would charge, but the convenience of having all the pieces in one place may be worth the price increase.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockEast")
                }, 
                NPCsHere = new() { "mistVarHorvik" },
                ShopPriceMultiplier = 1.2,
                ShopItemsHere = new() { "helmBronze", "platebodyBronze", "platelegsBronze", "bootsBronze", "gauntletsBronze", "sqShieldBronze", "kiteshieldBronze",
                                        "helmIron", "platebodyIron", "platelegsIron", "bootsIron", "gauntletsIron", "sqShieldIron", "kiteshieldIron",
                                        "helmSteel", "platebodySteel", "platelegsSteel", "bootsSteel", "gauntletsSteel", "sqShieldSteel", "kiteshieldSteel",
                                        "helmBlack", "platebodyBlack", "platelegsBlack", "bootsBlack", "gauntletsBlack", "sqShieldBlack", "kiteshieldBlack",
                                        "helmMithril", "platebodyMithril", "platelegsMithril", "bootsMithril", "gauntletsMithril", "sqShieldMithril", "kiteshieldMithril",
                                        "helmAdamant", "platebodyAdamant", "platelegsAdamant", "bootsAdamant", "gauntletsAdamant", "sqShieldAdamant", "kiteshieldAdamant" },
                ProcessingStations = new() { "Anvil" }
            });

            locsToAdd.Add(new Location("MIST_VarrockLowe", "Lowe's Archery Emporium", "Misthalin") {
                Description = "The inside of this shop is filled with displays of various bows and arrows. Some posters hang on the wall depicting the best spots to shoot rabbits and deer, and there's a mannequin wearing some nice looking ranger clothing. The building has some lovely green stained glass windows looking out to the street. There's a small side room containing a workshop and a ladder up to the second floor, which is largely filled with crates of inventory.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockEast")
                }, 
                NPCsHere = new() { "mistVarLowe" },
                ShopItemsHere = new() { "arrowsBronze", "arrowsIron", "arrowsSteel", "arrowsMithril", "arrowsAdamant",
                                        "boltsBronze",
                                        "shortbowPine", "shortbowOak", "shortbowWillow", "shortbowTeak", "shortbowMaple", "shortbowAcadia",
                                        "longbowPine", "longbowOak", "longbowWillow", "longbowTeak", "longbowMaple", "longbowAcadia",
                                        "coifLeather", "bodyLeather", "chapsLeather", "bootsLeather", "vambracesLeather", "shieldLeather",
                                        "coifHardleather", "bodyHardleather", "chapsHardleather", "bootsHardleather", "vambracesHardleather", "shieldHardleather",
                                        "coifStudded", "bodyStudded", "chapsStudded", "bootsStudded", "vambracesStudded", "shieldStudded",
                                        "coifCarapace", "bodyCarapace", "chapsCarapace", "bootsCarapace", "vambracesCarapace", "shieldCarapace",
                                        "coifGreenDragonhide", "bodyGreenDragonhide", "chapsGreenDragonhide", "bootsGreenDragonhide", "vambracesGreenDragonhide", "shieldGreenDragonhide" }
            });

            locsToAdd.Add(new Location("MIST_VarrockSouthEast", "Southeast Varrock", "Misthalin") {
                Description = "In a larger city this part of town might be called the slums, but that feels like overkill when it's really just a cluster of buildings you're likely to get stabbed as you walk past. Interestingly there's also a sort of gated community in the corner of the city. You know, next to the Zamorakian temple and the run-down houses and the crumbling ruins of former run-down houses. Prime real estate.",
                ConnectedLocations = new List<Connection>() { 
                    new Connection("MIST_VarrockEast"),
                    new Connection("MIST_VarrockPOH", [ new("POHActive", 0, "MIST_VarrockSouthEast")]),
                    new Connection("MIST_VarrockFencedArea"),
                    new Connection("MIST_VarrockAubury"),
                    new Connection("MIST_VarrockHouseAbandoned"),
                    new Connection("MIST_VarrockHouseRundown"), // the one with two men and a range
                    new Connection("MIST_VarrockHouseRundown2"), // adjoining above but with no resident
                    new Connection("MIST_VarrockHouseRundown3"), // next to temple, one man
                    new Connection("MIST_VarrockHouseYarlo"),
                    new Connection("MIST_VarrockZamorakTemple"),
                    new Connection("MIST_VarrockPhoenixGang", alt: "Suspiciously Maintained House"),
                    new Connection("MIST_VarrockPhoenixGangSide", alt: "Suspiciously Maintained Shack"), // that little room to the east of the main phoenix building between it and the temple
                    new Connection("MIST_VarrockSouth")
                }, 
                NPCsHere = new() { "dogStray", "man" },
                AreaMonsters = new() { "man", "mugger", "thiefNA" },
                GatheringSpots = new() { "treeDead", "treeDead" }
            });
                    
            locsToAdd.Add(new Location("MIST_VarrockPOH", "Varrock Townhouse", "Misthalin") {
                Description = "Home sweet home.".Align(HorizontalAlignment.Center, 92),
                PlayerHomeHere = "MIST_VarrockSouthEast",
                ShopPriceMultiplier = 0,
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockSouthEast")
                }
            });

            locsToAdd.Add(new Location("MIST_VarrockAubury", "Aubury's Rune Shop", "Misthalin") {
                Description = "A tiny cramped room with barely any room to walk around. There are some shelves on the walls with containers, though the labels are illegible, and other knick-knacks. There are also a couple chairs and a small table, a crate and chest, some kind of gray fur rug covering the floor. The store is, in a word, the very essence of 'disreputable'. That said it's essentially the only place in Misthalin that sells runes, so you don't have much choice.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockSouthEast")
                }, 
                NPCsHere = new() { "mistVarAubury" },
                ShopItemsHere = new() { "runeAir", "runeFire", "runeWater", "runeEarth", "runeMind", "runeBody", "runeChaos", "runeDeath" }
            });

            locsToAdd.Add(new Location("MIST_VarrockPhoenixGang", "Phoenix Gang Entrance", "Misthalin") {
                Description = "A small but surprisingly tidy room. There's a door leading to a ladder down into the hideout proper, but only members of the Phoenix Gang are allowed through. The only other thing of note in the room is Straven, sitting on a stool at a small table.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockSouthEast"), 
                    new Connection("MIST_VarrockPhoenixGangHideout", [ new("Data", 2, "ArravGangStatus", false, "equals", "Must be a member of the Phoenix Gang to go down there.") ], alt: "(Climb Down Ladder)")
                }, 
                NPCsHere = new() { "mistVarStraven" }
            });

            locsToAdd.Add(new Location("MIST_VarrockPhoenixGangHideout", "Phoenix Gang Hideout", "Misthalin") {
                Description = "A surpisingly large subterranean compound housing the offduty members of the Phoenix Gang. There's some sleeping quarters with beds, a combination dining hall and meeting area, and the store room that contains their illicit goods that haven't been sold yet. The whole space is lit by many lanterns, which are probably a pain to maintain.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockSouthEast"), 
                    new Connection("MIST_VarrockPhoenixGang", alt: "(Climb Up Ladder)")
                }, 
                AreaMonsters = new() { "thiefNA", "thiefNA", "thiefNA" },
                GatheringSpots = new() { "clueCrates", "arravChest", "clueBoxes", "clueCrates" }
            });

            locsToAdd.Add(new Location("MIST_VarrockPhoenixGangSide", "Phoenix Gang Stash Entry", "Misthalin") {
                Description = "A small but surprisingly tidy room. There's a ladder going up to the actual storage area, guarded by the Phoenix Gang weaponmaster. The only other things in this entry room are a set of drawers against a wall, and a window covered by a wooden shutter looking out towards Gower farm to the south of the city.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockSouthEast"), 
                    new Connection("MIST_VarrockPhoenixGangStash", [ new("Data", 2, "ArravGangStatus", false, "equals", "Must be a member of the Phoenix Gang to go up there.") ], alt: "(Climb Up Ladder)")
                }, 
                NPCsHere = new() { "mistVarWeaponmaster" },
                GatheringSpots = new() { "clueDrawers" }
            });

                locsToAdd.Add(new Location("MIST_VarrockPhoenixGangStash", "Phoenix Gang Stash", "Misthalin") {
                Description = "A much larger room than the entry hall below, as this part of the building extends out to the west and is partially above another nearby house. There are a bunch of shelves up here stacked with stolen or otherwise illicit goods, along with some tables and crates of the same. A nice rug is spread on the ground. There are also some steel helmets hanging on the wall and a cannon up here. You aren't quite sure how they get the cannon up the ladder.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockPhoenixGangSide", alt: "(Climb Down Ladder)")
                }, 
                ItemSpawns = new() { new("vambracesLeather", 5), new("bootsLeather", 5), new("daggerIron", 5), new("crossbowPhoenix", 2, count: 2)},
                GatheringSpots = new() { "clueCrates" }
            });

            locsToAdd.Add(new Location("MIST_VarrockSouth", "South Varrock", "Misthalin") {
                Description = "The southern entrance to Varrock, home to the sword shop and the Blue Moon Inn, the best inn in all of Misthalin! Or so they say. Probably not really anyone verifying claims like that around here. There's also a lovely little house with a cabbage patch, and a back alley west of the gate being frequented by some suspicious looking people.",
                ConnectedLocations = new List<Connection>() { 
                    new Connection("MIST_VarrockPlaza"),
                    new Connection("MIST_VarrockBlueMoon") { CutscenesOnTraverse = [ "MI_rjBlueMoon" ]},
                    new Connection("MIST_VarrockSwordShop"),
                    new Connection("MIST_VarrockHouseLovely"), // one right next to the south gate, on the right with the cabbage patch
                    new Connection("MIST_VarrockHouseClue"), // across the street from above, has a clue step here and not much else
                    new Connection("MIST_VarrockSketchyAlley"),
                    new Connection("MIST_VarrockSouthEast"),
                    new Connection("MIST_VarrockOutskirtsSouth")
                }, 
                NPCsHere = new() { "guard", "man", "man", "woman", "woman", "woman", "woman", "streetCleaner", "mistVarCharlie" },
                AreaMonsters = new() { "guard", "man", "man", "woman", "woman", "woman", "woman" },
                GatheringSpots = new() { "plantCabbage", "plantCabbage", "plantCabbage" }
            });

            locsToAdd.Add(new Location("MIST_VarrockSketchyAlley", "Sketchy Alley", "Misthalin") {
                Description = "Another of Varrocks fine locales, once again feeling a bit less safe than sticking to the main thoroughfares. All there is back here is a single door into an even sketchier looking building.",
                ConnectedLocations = new List<Connection>() { 
                    new Connection("MIST_VarrockSouth"),
                    new Connection("MIST_VarrockBlackArmEntrance", alt: "Even Sketchier Building")
                },
                AreaMonsters = new() { "thiefNA" }
            });

            locsToAdd.Add(new Location("MIST_VarrockBlackArmEntrance", "Black Arm Gang Hideout - Entrance", "Misthalin") {
                Description = "A small room crammed with crates and shelves and chests stacked with nondescript bags and unmarked boxes. There's a table sitting on a nice fur rug in the center of the room, and doors deeper into the hideout and back out to the safety of the streets.",
                ConnectedLocations = new List<Connection>() { 
                    new Connection("MIST_VarrockSketchyAlley"),
                    new Connection("MIST_VarrockBlackArmHideout", [ new("Data", 1, "ArravGangStatus", false, "equals") ])
                }, 
                NPCsHere = new() { "mistVarKatrine" },
                AreaMonsters = new() { "thiefNA" }
            });

            locsToAdd.Add(new Location("MIST_VarrockBlackArmHideout", "Black Arm Gang Hideout", "Misthalin") {
                UPCsong = "Welcome to the Black Parade",
                Description = "The actual hideout of the Black Arm Gang, with a few cabinets displaying various expensive-looking plates, candlesticks, weapons, and more. Much of the middle of the room is dominated by a table meetings are held at, surrounded by a mishmash of different styles of chairs. All paid for handsomely by someone besides the Black Arm Gang, no doubt. There's a staircase up to where the rest of the loot is held before it can be sold.",
                ConnectedLocations = new List<Connection>() { 
                    new Connection("MIST_VarrockBlackArmEntrance"),
                    new Connection("MIST_VarrockBlackArmUpstairs")
                },
                AreaMonsters = new() { "thiefNA", "thiefNA", "thiefNA" }
            });

            locsToAdd.Add(new Location("MIST_VarrockBlackArmUpstairs", "Black Arm Gang Hideout - Upstairs", "Misthalin") {
                Description = "This room is absolutely stuffed with different forms of storage to hold massive amounts of loots. Crates, shelves, and chests are everywhere overflowing with items of no value to you but presumably high value to collectors somewhere. A few sets of tables and chairs to examine loot are placed around the room. There's a cupboard off in one corner of the room, and a staircase back down to the hideout.",
                ConnectedLocations = new List<Connection>() { 
                    new Connection("MIST_VarrockBlackArmHideout")
                },
                AreaMonsters = new() { "thiefNA", "thiefNA", "thiefNA" },
                GatheringSpots = new() { "clueCrates", "clueCrates", "clueBoxes", "clueCrates", "arravCupboard", "clueChest", "clueCrates", "clueCrates" }
            });

            locsToAdd.Add(new Location("MIST_VarrockBlueMoon", "The Blue Moon Inn", "Misthalin") {
                UPCsong = "Piano Man",
                Description = "A spacious, clean, and nicely decorated bar. There are six tables with two stools apiece placed around the room, and three more stools at the bar. A large ornate blue rug covers most of the floor in the common area. There is a fire roaring in the stone fireplace, which has a painting of a water mill hanging above it. Behind the bar is a cabinet containing a wide variety of colorful bottles of alcohol. There's a staircase up to the second floor, where the rooms are, and a door back to the kitchen.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockSouth"),
                    new Connection("MIST_VarrockBlueMoonUpstairs"),
                    new Connection("MIST_VarrockBlueMoonKitchen")
                }, 
                NPCsHere = new() { "mistVarBlueBartender", "mistVarHarlow", "man", "man", "woman" },
                AreaMonsters = new() { "barbarian8", "man", "man", "woman", "arravJonny" },
                ShopItemsHere = new() { "beer" },
                GatheringSpots = new() { "clueCrates" },
                ProcessingStations = new() { "Range" }
            });

            locsToAdd.Add(new Location("MIST_VarrockBlueMoonUpstairs", "The Blue Moon Inn - Upstairs", "Misthalin") {
                Description = "A set of well furnished rooms for rent take up most of the space of this floor. There are some customers lingering in their rooms up here. On the landing there are a couple bookshelves, plus a door out to the balcony overlooking the pillory area below.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockSouth"),
                    new Connection("MIST_VarrockBlueMoonKitchen")
                }, 
                NPCsHere = new() { "man", "man" },
                AreaMonsters = new() { "man", "man" },
                GatheringSpots = new() { "clueDrawers", "clueBookcase", "clueBookcase" },
                MahoganyTown = "Varrock",
                MahoganyHomesHotspots = new() { "bedLarge", "portrait", "chest", "portrait", "drawers", "bookcase", "bed", "chest", "drawers", "portrait", "bed", "drawers", "bookcase", "bookcase"  }
            });

            locsToAdd.Add(new Location("MIST_VarrockBlueMoonKitchen", "The Blue Moon Inn - Kitchen", "Misthalin") {
                Description = "A spacious and clean kitchen. There are a few shelves to hold tankards, pots, and pans. A table holds an empty pot you could snatch. There are some racks of wine bottles without labels, plus a range that the cook cooks at.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockBlueMoon")
                }, 
                NPCsHere = new() { "mistVarBlueCook" },
                ItemSpawns = new() { new("potEmpty", 1) },
                ProcessingStations = new() { "Range" }
            });

            locsToAdd.Add(new Location("MIST_VarrockSwordShop", "Varrock - Sword Shop", "Misthalin") {
                Description = "A fine establishment offering a variety of swords and daggers. The walls and tables are absolutely covered in display cases and weapon racks showing off a multitude of weaponry, a vast majority of which cannot be purchased at this store. In a small backroom there are a bunch of crates stacked up to store excess inventory. The front of the building, facing the main thoroughfare, has pleasant green stained glass windows letting in plenty of light.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_VarrockSouth")
                }, 
                NPCsHere = new() { "mistVarShopkeeperSword" },
                ShopItemsHere = new() { "daggerBronze", "daggerIron", "daggerSteel", "daggerBlack", "daggerMithril", "daggerAdamant", "swordBronze", "swordIron", "swordSteel", "swordBlack", "swordMithril", "swordAdamant" },
                GatheringSpots = new() { "clueCrates" }
            }); 

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
