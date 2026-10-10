using ZeroPlayersOnline.DataTypes; 

namespace ZeroPlayersOnline.Hardcodes {
    public class HardcodedLocations {
        public static void InitLocs(Dictionary<string, Location> Atlas, Dictionary<string, GatheringTile> Gathers, Dictionary<string, AreaMonster> Monsters) {
            List<Location> locsToAdd = new();

            // Misthalin Locations
            {
                
                // Barbarian Village and Stronghold of Security
                {
                    locsToAdd.Add(new Location("MIST_BarbarianVillage", "Barbarian Village", "Misthalin") {
                        Description = "Sometimes also called Grunnarsgrunn, or Gunnar's Ground, this is a village inhabited by barbarians just south of Edgeville and west of Varrock on the River Lum. Outside the crude palisade is a lookout tower, and a perpetual fire next to the fishing spots on the river. Inside the walls are a few buildings, including a helmet shop, a pottery studio, a storage hut, and a longhall. Surrounded by ore rocks in the center of the village is a hole leading down into the Stronghold of Security.",
                        ConnectedLocations = new List<Connection>() {
                            new Connection("MIST_BetweenEdgevilleBarbarian"),
                            new Connection("MIST_BarbarianLonghall"),
                            new Connection("MIST_BarbarianPeksa"),
                            new Connection("MIST_BarbarianLookout"),
                            new Connection("MIST_StrongholdSecurity1Entrance"),
                            new Connection("MIST_VarrockCrossroadsWest"),
                            new Connection("MIST_BetweenBarbarianDraynor"),
                            new Connection("MIST_LumbridgeAcrossLum", null, false, 60, "Woodcutting", "(Canoe to Lumbridge)", false, 27),
                            new Connection("MIST_VarrockMineWest", null, false, 30, "Woodcutting", "(Canoe to the Champion's Guild)", false, 12),
                            new Connection("MIST_Edgeville", null, false, 30, "Woodcutting", "(Canoe to Edgeville)", false, 12),
                            new Connection("WILD_FeroxEnclave", null, false, 150, "Woodcutting", "(Canoe to Ferox Enclave)", false, 57),
                            new Connection("WILD_WildernessPond", null, false, 150, "Woodcutting", "(Canoe to Wilderness Pond)", false, 57)
                        }, 
                        ItemSpawns = new() { new("pickaxeBronze", 1) },
                        AreaMonsters = new() { "barbarian9", "barbarian9", "barbarian9", "barbarian10", "barbarian10", "unicorn" },
                        ProcessingStations = new() { "Spinning Wheel", "Pottery Wheel", "Pottery Kiln" },
                        GatheringSpots = new() { "clueChest", "treePine", "treePine", "treeOak", "oreTin", "oreTin", "oreCoal", "oreCoal", "fishBaitLow", "fishBaitLow", "fishLure", "fishLure" }
                    }); 

                    locsToAdd.Add(new Location("MIST_BarbarianPeksa", "Peksa's Helmet Shop", "Misthalin") {
                        Description = "A small storefront with a variety of helmets on sale, some of them hanging on the wall for display. There's a fire burning in the middle of the room, plus some shelves and tables stacked with goods against the walls. There are a couple crates stacked in one corner.",
                        ConnectedLocations = new List<Connection>() {
                            new Connection("MIST_BarbarianVillage")
                        }, 
                        ShopItemsHere = new() { "helmBronze", "helmIron", "helmSteel", "helmMithril", "helmAdamant" },
                        ItemSpawns = new() { new("potEmpty", 1) },
                        ProcessingStations = new() { "Fire" },
                        NPCsHere = new() { "mistBarbPeksa" },
                        GatheringSpots = new() { "clueCrates" }
                    });

                    locsToAdd.Add(new Location("MIST_BarbarianLonghall", "The Long Hall", "Misthalin") {
                        Description = "A longhall - or maybe a tavern called 'The Long Hall'? - filled with barbarians. There are two long rows of tables with some stools placed haphazardly next to them. At the very back of the hall are two fires you could cook with, and some crates in one corner. Some unattended bits of meat and beer are on the tables. A stuffed bull's head hangs above the fires, and there's a keg full of beer in another corner of the hall.",
                        ConnectedLocations = new List<Connection>() {
                            new Connection("MIST_BarbarianVillage")
                        },  
                        AreaMonsters = new() { "barbarian15", "barbarian15", "barbarian15", "barbarian17", "barbarian17", "barbarianGunthor" },
                        ItemSpawns = new() { new("beer", 3, count: 3), new("meatCookedBeef", 3, count: 2) },
                        ProcessingStations = new() { "Fire", "Beer Keg" }
                    });

                    locsToAdd.Add(new Location("MIST_BarbarianLookout", "Barbarian Village - Lookout Tower", "Misthalin") {
                        Description = "The view from up here is excellent, allowing you to see Edgeville in the north, Varrock in the east, Draynor manor and village in the south, and Falador distantly in the west. Inside the tower are a bunch of crates and a few ominous jail cells, plus some chests behind a locked grate.",
                        ConnectedLocations = new List<Connection>() {
                            new Connection("MIST_BarbarianVillage")
                        },
                        NPCsHere = new() { "mistBarbHunding" }
                    });
                }
            }


            // Runecraft Altars
            {
                locsToAdd.Add(new Location("RunecraftEssenceMine", "Rune Essence Mine", "Elsewhere") {
                    Description = "This appears to be some kind of strange pocket dimension contained outside of the normal plane of existence. A huge pillar of rune essence resets in the center of the space, surrounded by rocky terrain with a light dusting of snow. It is snowing gently.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_WizardTowerBasement", [ new("Data", 1, "LastTeleportToEssenceMine", false, "equals") ], hideIfNoReqs: true),
                        new Connection("MIST_VarrockAubury", [ new("Data", 2, "LastTeleportToEssenceMine", false, "equals") ], hideIfNoReqs: true)
                    },
                    GatheringSpots = new() { "rockEssencePillar" }
                });

                locsToAdd.Add(new Location("RunecraftAltarMind", "Altar of Mind", "Elsewhere") { 
                    UPCsong = "Where is My Mind",
                    Description = "This appears to be some kind of strange pocket dimension contained outside of the normal plane of existence. A series of small inaccessible bits of land fill the water around the main island you're standing on, though the space appears to end not too far out from this main island. Four stone arches and four pillars with an orb emitting soft light circle a stone altar engraved with the symbol for water.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwamp14")
                    },
                    ProcessingStations = new List<string>() { "Water Altar" }
                });

                locsToAdd.Add(new Location("RunecraftAltarWater", "Altar of Water", "Elsewhere") { 
                    UPCsong = "Aquatic Ambience",
                    Description = "This appears to be some kind of strange pocket dimension contained outside of the normal plane of existence. A series of small inaccessible bits of land fill the water around the main island you're standing on, though the space appears to end not too far out from this main island. Four stone arches and four pillars with an orb emitting soft light circle a stone altar engraved with the symbol for water.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_LumbridgeSwamp14")
                    },
                    ProcessingStations = new List<string>() { "Water Altar" }
                });

                locsToAdd.Add(new Location("RunecraftAltarFire", "Altar of Fire", "Elsewhere") { 
                    Description = "This appears to be some kind of strange pocket dimension contained outside of the normal plane of existence. A series of small inaccessible bits of blasted rock fill the lava around the main island you're standing on, though the space appears to end not too far out from this main island. Four stone arches and four pillars with an orb emitting soft light circle a stone altar engraved with the symbol for fire.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("DES_AlKharidMineOutside")
                    },
                    ProcessingStations = new List<string>() { "Fire Altar" }
                });

                locsToAdd.Add(new Location("RunecraftAltarEarth", "Altar of Earth", "Elsewhere") { 
                    Description = "This appears to be some kind of strange pocket dimension contained outside of the normal plane of existence. It takes the form of a moderately sized cavern full of rocks of various shapes and sizes. Four stone arches and four pillars with an orb emitting soft light circle a stone altar engraved with the symbol for earth.",
                    ConnectedLocations = new List<Connection>() {
                        new Connection("MIST_VarrockCrossroadsEast")
                    },
                    ProcessingStations = new List<string>() { "Earth Altar" }
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
