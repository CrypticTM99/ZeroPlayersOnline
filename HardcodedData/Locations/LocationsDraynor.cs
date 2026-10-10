using ZeroPlayersOnline.DataTypes; 

namespace ZeroPlayersOnline.Hardcodes {
    public class LocationsDraynor {
        public static void InitLocs(Dictionary<string, Location> Atlas, Dictionary<string, GatheringTile> Gathers, Dictionary<string, AreaMonster> Monsters) {
            List<Location> locsToAdd = new();
             
            locsToAdd.Add(new Location("MIST_DraynorOutskirtsSouth", "Between Draynor and Lumbridge", "Misthalin") {
                Description = "Just outside Draynor Village on the path leading from there to Lumbridge. The path winds back and forth between the small hills of the countryside. A smaller path branches off and leads towards the Wizard's Tower. Closer to Draynor, just off the path, is the old jail building. There are a few willow trees here and some fish swarming near the coast.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_DraynorVillage"),
                    new Connection("MIST_DraynorJail"),
                    new Connection("MIST_WizardTowerBridge"),
                    new Connection("MIST_LumbridgeTowardsDraynor"),
                    new Connection("MIST_LumbridgeSwamp0")
                },
                NPCsHere = new() { "mistDrayTwiggyOKorn" }, // TODO: Achievement diary cape, also gives a chocolate bar 1/50 when talked to
                ItemSpawns = new() { new("hatchetBronze", 30) },
                GatheringSpots = new() { "treeWillow", "treeWillow", "treeWillow", "treeOak", "treeOak", "treePine", "treePine", "treePine", "fishNetSmall", "fishNetSmall", "fishBaitLow", "fishBaitLow" },
                AreaMonsters = new() { "knightBlackNA", "wizardDark7NA"  }
            });

            locsToAdd.Add(new Location("MIST_DraynorVillage", "Draynor Village", "Misthalin") {
                Description = "A small, somewhat creepy, village on the border between Misthalin and Asgarnia. It has a small market, a bank, and a few houses for the residents. To the north there's the crossroads, Draynor Farm, and Draynor Manor, while to the east is the jail.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_DraynorCrossroads"), 
                    new Connection("MIST_DraynorFarm"),
                    new Connection("MIST_DraynorPOH", [ new("POHActive", 0, "MIST_DraynorVillage")]),
                    new Connection("MIST_DraynorAgility1"), 
                    new Connection("MIST_DraynorNorthEast"), 
                    new Connection("MIST_DraynorMarket"),
                    new Connection("MIST_DraynorBank"),
                    new Connection("MIST_DraynorAggie"),
                    new Connection("MIST_DraynorNed"), 
                    new Connection("MIST_DraynorMorgan"),
                    new Connection("MIST_DraynorSpria"),
                    new Connection("MIST_DraynorWiseOldMan"),
                    new Connection("MIST_DraynorJail"), 
                    new Connection("MIST_DraynorOutskirtsSouth")
                },
                GatheringSpots = new() { "treeOak", "treeOak", "treePine", "treePine", "treePine" },
                AreaMonsters = new() { "man", "man", "man", "woman", "woman" }
            });

            locsToAdd.Add(new Location("MIST_DraynorPOH", "Draynor Gothic", "Misthalin") {
                Description = "Home sweet home.".Align(HorizontalAlignment.Center, 92),
                PlayerHomeHere = "MIST_DraynorVillage",
                ShopPriceMultiplier = 0,
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_DraynorVillage")
                }
            });

            locsToAdd.Add(new Location("MIST_DraynorMarket", "Draynor Market", "Misthalin") {
                Description = "A surprisingly busy market, given how small the village is. There are a couple seed stalls and a wine stall here, plus a toy stall with nothing worth stealing - I mean, buying. There's a small pig pen adjacent to the market with some very cute piglets waddling around in it.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_DraynorNorthEast"), 
                    new Connection("MIST_DraynorVillage"), 
                    new Connection("MIST_DraynorOutskirtsSouth"), 
                    new Connection("ASG_PortSarimNorth", skill: "Agility", lv: 42)
                },
                NPCsHere = new() { "mistDrayOlivia", "mistDrayDiango", "mistDrayFortunato", "mistDrayMartin", "farmerMaster", "townCrier", "mistDrayTree", "mistDrayBankGuard" },
                ShopItemsHere = new() { "seedPotato", "seedOnion", "seedCabbage", "seedTomato", "seedSweetcorn", "seedStrawberry", "seedSnapegrass", "seedWatermelon", "seedBarley", "seedJute", "seedRosemary", "seedMarigold", "seedHammerstone", "seedAsgarnian", "seedYanillian", "seedKrandorian", "seedWildblood", "jugWine", "jugEmpty", "bottleWine", "jugVinegar", "chronicle", "cardTeleport"},
                GatheringSpots = new() { "stallSeed", "stallSeed", "stallWine", "treeOak", "treePine", "treePine" },
                AreaMonsters = new() { "guardMarket", "guardMarket", "man", "man", "man", "woman", "imp" }
            });

            locsToAdd.Add(new Location("MIST_DraynorBank", "Draynor Bank", "Misthalin") {
                Description = "A cozy little bank building right next to the main road through Draynor. There's a row of bank booths cutting off a third of the room. In the accessible part of the building is a poll booth, a small bearskin rug on the floor, and a bank deposit box plus some noticeboards that haven't been updated in a long time. In the sectioned off area there's a part of the wall that looks different than the rest, like it had to be repaired recently.",
                IsBank = true,
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_DraynorVillage")
                },
                ProcessingStations = new() { "GE Bank" }
            });

            locsToAdd.Add(new Location("MIST_DraynorAggie", "Aggie's House", "Misthalin") {
                Description = "This house practically screams 'inhabited by a witch'. After a particularly nasty spell it might do so literally. In the middle of the room is a large cauldron with a bubbling blue liquid in it. There are some arcane sigils hanging on the walls, and the shelves are full of occult paraphernalia. There's even what looks like a human skull on one shelf. A string of garlic bulbs hangs by the door - probably wise, given the rumors of vampires running about. There are a few rolls of fabric lying against the wall... *spooky* fabric, probably. There's also a few chairs and two small tables, one with a slice of cheese and the other a tomato resting on it. A staircase leads up to Aggie's bedroom, equally witchy, with some ornate wardrobes and a comfortable looking bed.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_DraynorVillage")
                },
                NPCsHere = new() { "mistDrayAggie" },
                GatheringSpots = new() { "clueWardrobe", "clueWardrobe" },
                ItemSpawns = new() { new("cheese", 3), new("tomato", 3) },
                MahoganyTown = "Draynor",
                MahoganyHomesHotspots = new() { "wardrobe", "lighting", "lighting", "wardrobe", "lighting", "bench", "wardrobe", "lighting", "chair", "lighting", "lighting", "bedLarge" }
            });

            locsToAdd.Add(new Location("MIST_DraynorNed", "Ned's House", "Misthalin") {
                Description = "The inside of the house is stuffed fill of things, to the point that you consider Ned may have a hoarding problem. There are a bunch of chairs and nautical themed decorations all over the place, and a couple tables with ropes in the progress of being woven on them. A nice painting of White Wolf Mountain hangs on the wall. There are some crates full of even more junk, and a fireplace with a roaring fire in it.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_DraynorVillage")
                },
                NPCsHere = new() { "mistDrayNed" },
                ShopItemsHere = new() { "rope" },
                MahoganyTown = "Draynor",
                MahoganyHomesHotspots = new() { "chair", "stool", "shelf", "shelf", "lighting", "table", "table", "lighting", "bench", "lighting", "lighting", "wheel", "bench", "drawers", "bedLarge", "chair", "lighting", "lighting", "shelf" }
            });

            locsToAdd.Add(new Location("MIST_DraynorSpria", "Spria's House", "Misthalin") {
                Description = "A small house on the edge of Draynor. There's not much inside it besides a chest, a fireplace, a bed, and some drawers. There's a stuff bull head mounted on the wall.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_DraynorVillage")
                },
                NPCsHere = new() { "mistDraySpria" },
                GatheringSpots = new() { "clueDrawers" },
                ItemSpawns = new() { new("logPine", 2, count: 2) }
            });


            locsToAdd.Add(new Location("MIST_DraynorMorgan", "Morgan's House", "Misthalin") {
                Description = "A small house on the edge of Draynor. There's not much inside it. On the ground floor there are a couple small rugs and chests, a barrel, and a small table with some stools. The upper floor has another table, a bed, and a cupboard seemingly full of garlic.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_DraynorVillage")
                },
                NPCsHere = new() { "mistDrayMorgan", "woman" },
                AreaMonsters = new() { "woman" },
                GatheringSpots = new() { "clueChest", "clueChest", "cupboardMorgan" }
            });

            locsToAdd.Add(new Location("MIST_DraynorCrossroads", "Draynor Crossroads", "Misthalin") {
                Description = "The atmosphere feels particularly ominous here, as you near Draynor Manor. The crossroads go north to the manor grounds, west towards Falador, east to Draynor Farm and the Lumbridge windmill, and south to Draynor Village. There's a highwayman here looking for easy prey, and a very lost goblin who got separated from his squad at Draynor Farm.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_DraynorManorGate"),
                    new Connection("MIST_DraynorManorWest"),
                    new Connection("MIST_DraynorManorEast"),
                    new Connection("ASG_FaladorFarm"),  
                    new Connection("MIST_DraynorFarm"),
                    new Connection("MIST_LumbridgeFredsFarm"), 
                    new Connection("MIST_DraynorNorthEast"), 
                    new Connection("MIST_DraynorVillage")
                },
                GatheringSpots = new() { "treeOak", "treeOak", "treePine", "treePine", "treePine" },
                AreaMonsters = new() { "highwayman", "goblin" }
            });

            locsToAdd.Add(new Location("MIST_DraynorNorthEast", "Northeast of Draynor", "Misthalin") {
                Description = "A small area crammed between the market and the road to Falador. There's nothing really here except some trees and a trapdoor down to the Draynor sewers.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("ASG_FaladorFarm"),
                    new Connection("MIST_DraynorCrossroads"),
                    new Connection("MIST_DraynorSewersEnd", alt: "(Climb Down Trapdoor)"),
                    new Connection("MIST_DraynorMarket"), 
                    new Connection("MIST_DraynorVillage")
                },
                GatheringSpots = new() { "treePine", "treePine", "treePine", "treePine", "treePine" }
            });

            locsToAdd.Add(new Location("MIST_DraynorFarm", "Draynor Farm", "Misthalin") {
                Description = "A nice large field of wheat and potatoes near a run-down barn. It still provides most of the food for Draynor, though it is significantly less frequented since a band of goblins moved in. Now the goblins handle most of the farmwork and charge for the food that the Draynor citizens own anyways.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_DraynorCrossroads"),
                    new Connection("MIST_LumbridgeFredsFarm"),
                    new Connection("MIST_DraynorVillage")
                },
                GatheringSpots = new() { "treeOak", "treeOak", "treePine", "treePine", "treePine", "plantPotato", "plantPotato", "plantPotato", "plantGrain", "plantGrain", "plantGrain" },
                AreaMonsters = new() { "goblin", "goblin", "goblin", "goblin", "goblin", "goblin" },
                NPCsHere = new() { "mistDrayLeela", "mistLumCassius" }
            });

            locsToAdd.Add(new Location("MIST_DraynorJail", "Draynor Jail", "Misthalin") {
                Description = "An old jail building, hardly used anymore. There are some very hostile guards patrolling around outside the building. A few nettle bushes are around the back, but you'll need gloves of some kind to harvest them.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_DraynorVillage"),
                    new Connection("MIST_DraynorOutskirtsSouth"), 
                    new Connection("MIST_DraynorJailInterior"), 
                    new Connection("MIST_DraynorSewersStart", alt: "(Climb Down Trapdoor)")
                },
                GatheringSpots = new() { "treeOak", "treeOak", "treePine", "treePine", "treePine", "plantNettles", "plantNettles", "plantNettles" },
                AreaMonsters = new() { "guardJail", "guardJail", "guardJail", "guardJail" }
            });

            locsToAdd.Add(new Location("MIST_DraynorIsland", "Draynor Island", "Misthalin") {
                Description = "This is a tiny island just off the coast Misthalin, near Draynor Village and the Wizards' Tower. There is literally nothing here but a patch of land and the fairy ring you used to arrive.",
                ConnectedLocations = new List<Connection>() {
                    // TODO: Add a fairy ring here when they get added, code CLP
                }
            });

            locsToAdd.Add(new Location("MIST_WizardTowerBridge", "Wizards' Tower Bridge", "Misthalin") {
                Description = "On the large wide path from the Wizard Tower island to mainland Misthalin. There are a few banners hanging from poles, and some crates left partway down the bridge. The marble Wizard Tower spears into the sky above you, visible from far and wide.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_WizardTowerIsland"),
                    new Connection("MIST_DraynorOutskirtsSouth")
                },
                GatheringSpots = new() { "clueCrates" }
            });

            locsToAdd.Add(new Location("MIST_WizardTowerIsland", "Wizards' Tower Island", "Misthalin") {
                Description = "This island holds the Wizard's Tower, towering high above you. Two statues of famous wizards of old stand guard at the bridge entrance, while a fountain is just off the path into the tower. There is some sparse tree and shrub coverage on the island but otherwise not a lot of interest.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_WizardTower"),
                    new Connection("MIST_WizardTowerBridge")
                },
                GatheringSpots = new() { "treePine", "treePine" }
            });

            locsToAdd.Add(new Location("MIST_WizardTower", "Wizards' Tower", "Misthalin") {
                Description = "Wizards bustle about in here in the midst of completing various magical research projects. There are a few small rooms here on the first floor, plus a staircase up to the next floor and a ladder down to the basement. One of the rooms contains a library with some bookshelves containing various books that are probably very interesting if you're a wizard doing research.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_WizardTower2F"),
                    new Connection("MIST_WizardTowerBasement", alt: "(Climb Ladder)"),
                    new Connection("MIST_WizardTowerIsland")
                },
                GatheringSpots = new() { "bookshelfWizard", "bookshelfWizard", "bookshelfWizard" },
                NPCsHere = new() { "mistWizOnglewip" },
                ItemSpawns = new() { new("bootsLeather", 1), new("logPine", 1) },
                AreaMonsters = new() { "wizard", "wizard", "wizard", "wizard" }
            });

            locsToAdd.Add(new Location("MIST_WizardTower2F", "Wizards' Tower - Second Floor", "Misthalin") {
                Description = "This floor of the tower seems to be more of a sleeping area than the research areas below or above, with some cots placed in the rooms for weary wizards. There's not much else in here except a nice view through the windows looking out onto the surrounding landscape.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_WizardTower3F"),
                    new Connection("MIST_WizardTower")
                },
                NPCsHere = new() { "mistWizTraiborn", "mistWizJalarast" },
                AreaMonsters = new() { "wizard", "wizard" }
            });

            locsToAdd.Add(new Location("MIST_WizardTower3F", "Wizards' Tower - Third Floor", "Misthalin") {
                Description = "A decent portion of this floor is taken up by a large cage holding a lesser demon captive inside it. Though you can't reach it with melee attacks, you could probably hit it with ranged and magic attacks without it being able to retaliate. Slightly cruel, but it's a demon, so it's probably okay? There are also some high-ranking wizards up here busying themselves in their offices with various works. Illegible research notes cover a few of the tables up here.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_WizardTower2F")
                },
                NPCsHere = new() { "mistWizMizgog", "mistWizGrayzag" },
                AreaMonsters = new() { "wizDemonLesser", "wizard", "wizard" }
            });

            locsToAdd.Add(new Location("MIST_WizardTowerBasement", "Wizards' Tower - Basement", "Misthalin") {
                Description = "Supposedly this basement is the ruins of the old Wizards' Tower, from back when it was inhabited by Zamorakian wizards. Now though it's just a dirty and messy basement with a few crumbling walls. Archmage Sedridor has set up office down here. There's a set of locked drawers down here, and a suspicious looking altar. Also a chicken... for some reason.",
                ConnectedLocations = new List<Connection>() {
                    new Connection("MIST_WizardTower")
                },
                NPCsHere = new() { "mistWizSedridor" },
                AreaMonsters = new() { "chicken", "mistWizSkeleton" },
                GatheringSpots = new() { "mistWizAltar", "clueDrawersLocked" }
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
