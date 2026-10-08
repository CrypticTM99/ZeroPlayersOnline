using ZeroPlayersOnline.DataTypes;
using ZeroPlayersOnline.HardcodedData;

namespace ZeroPlayersOnline.Hardcodes {
    public static class HardcodedPOHObjects {
        public static List<POH_Room> RoomTypes = new() {
            new("Parlor", ["Bookcase", "Chair", "Curtains", "Fireplace", "Rug", "Teleport Focus"], 1_000, 1),
            new("Garden", ["Big Plant", "Small Plant", "Small Plant", "Tree", "Plot"], 1_000, 1),
            new("Kitchen", ["Keg", "Cat Basket", "Larder", "Shelf", "Sink", "Spice Rack", "Stove", "Counter"], 5_000, 5),
            new("Dining Room", ["Curtains", "Decoration", "Fireplace", "Seating", "Table"], 5_000, 10),
            new("Workshop", ["Primary Station", "Secondary Station", "Tool Rack", "Repair Bench"], 10_000, 15), // TODO: Re-add clockwork bench when it matters for quests
            new("Bedroom", ["Bed", "Corner", "Curtains", "Dresser", "Fireplace", "Rug", "Wardrobe"], 10_000, 20),
            new("Study", ["Bookcase", "Crystal Ball", "Globe", "Lectern", "Telescope"], 50_000, 40),
            new("Chapel", ["Altar", "Icon", "Lamp", "Musical", "Rug", "Statue", "Window"], 50_000, 45),
            new("Portal Chamber", ["Portal", "Portal", "Portal", "Teleport Focus"], 100_000, 50)
        }; 
        // Missing Skill Hall, Games Room, Combat Room, Quest Hall, Menagerie, Costume Room, Formal Garden, Throne Room, Superior Garden, and Achievement Gallery
        // Some have to be workshopped a bit to give them an actual valuable use

        public static void InitPOHObjects(Dictionary<string, POH_Object> ObjLib, Dictionary<string, Spell> SpellLib, Dictionary<string, Location> Atlas) {
            List<POH_Object> toAdd = new();

            List<MaterialDef> Woods = new() {
                new("Pine", ColorLib.Pine, 1, 1, 30, "Bronze"),
                new("Oak", ColorLib.Oak, 2, 10, 60, "Iron"),
                new("Willow", ColorLib.Willow, 3, 20, 90, "Steel"),
                new("Teak", ColorLib.Teak, 4, 30, 120, "Mithril"),
                new("Maple", ColorLib.Maple, 5, 40, 150, "Adamant"),
                new("Acadia", ColorLib.Acadia, 6, 50, 180, "Rune"),
                new("Mahogany", ColorLib.Mahogany, 7, 60, 210, "Orichalcum"),
                new("Yew", ColorLib.Yew, 8, 70, 240, "Necronium"),
                new("Magic", ColorLib.Magic, 9, 80, 270, "Bane"),
                new("Elder", ColorLib.Elder, 10, 90, 300, "ElderRune")
            };

            for (int i = 0; i < 10; i++) { // Tiered items
                toAdd.Add(new("Bookcase", Woods[i].Name + " bookcase", "A" + (Helper.VowelStart(Woods[i].Name.ToLower()) ? "n " : " ") + Woods[i].Name.ToLower() + " bookcase. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",3", "nails" + Woods[i].Descriptor + ",6" ], ["hammer,1", "saw,1"], Woods[i].CostMultiplier * 3, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level, "Construction")], (int) Woods[i].Tier, "clueBookcase"));
                toAdd.Add(new("Chair", Woods[i].Name + " chair", "A" + (Helper.VowelStart(Woods[i].Name.ToLower()) ? "n " : " ") + Woods[i].Name.ToLower() + " chair. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",2", "nails" + Woods[i].Descriptor + ",4" ], ["hammer,1", "saw,1"], Woods[i].CostMultiplier * 2, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level, "Construction")], (int) Woods[i].Tier));
                toAdd.Add(new("Chair", Woods[i].Name + " armchair", "A" + (Helper.VowelStart(Woods[i].Name.ToLower()) ? "n " : " ") + Woods[i].Name.ToLower() + " armchair. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",3", "nails" + Woods[i].Descriptor + ",6" ], ["hammer,1", "saw,1"], Woods[i].CostMultiplier * 3, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level + 5, "Construction")], (int) Woods[i].Tier));
                
                toAdd.Add(new("Counter", Woods[i].Name + " counter", "A" + (Helper.VowelStart(Woods[i].Name.ToLower()) ? "n " : " ") + Woods[i].Name.ToLower() + " counter to prepare food. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",3", "nails" + Woods[i].Descriptor + ",6" ], ["hammer,1", "saw,1"], Woods[i].CostMultiplier * 3, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level + 5, "Construction")], (int) Woods[i].Tier));
                
                toAdd.Add(new("Decoration", Woods[i].Name + " wall decoration", "A" + (Helper.VowelStart(Woods[i].Name.ToLower()) ? "n " : " ") + Woods[i].Name.ToLower() + " carving of your family crest. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",2", "nails" + Woods[i].Descriptor + ",4" ], ["hammer,1", "saw,1"], Woods[i].CostMultiplier * 2, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level, "Construction"), new("Data", 1, "HasCrest", summ: "Must have spoken to Sir Renitee to know your family crest.")], (int) Woods[i].Tier));
                toAdd.Add(new("Seating", Woods[i].Name + " bench", "A" + (Helper.VowelStart(Woods[i].Name.ToLower()) ? "n " : " ") + Woods[i].Name.ToLower() + " bench for guests to sit. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",4", "nails" + Woods[i].Descriptor + ",8" ], ["hammer,1", "saw,1"], Woods[i].CostMultiplier * 4, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level, "Construction")], (int) Woods[i].Tier));
                toAdd.Add(new("Table", Woods[i].Name + " dining table", "A" + (Helper.VowelStart(Woods[i].Name.ToLower()) ? "n " : " ") + Woods[i].Name.ToLower() + " dining table. Holds plenty of food. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",6", "nails" + Woods[i].Descriptor + ",12" ], ["hammer,1", "saw,1"], Woods[i].CostMultiplier * 6, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level, "Construction")], (int) Woods[i].Tier));
                
                if (i < 9) {
                    toAdd.Add(new("Bed", Woods[i].Name + " bed", "Great for sleeping in. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",5", "nails" + Woods[i].Descriptor + ",10" ], ["hammer,1", "saw,1"], Woods[i].CostMultiplier * 5, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level, "Construction")], (int) Woods[i].Tier, acts: [ new("POH", "Rest", "", 0)] ));
                    toAdd.Add(new("Corner", Woods[i].Name + " clock", "Time is a tool you can put on the wall or wear it on your rizt. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",2", "nails" + Woods[i].Descriptor + ",4" ], ["hammer,1", "saw,1"], Woods[i].CostMultiplier * 2, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level + 5, "Construction")], (int) Woods[i].Tier));
                    toAdd.Add(new("Dresser", Woods[i].Name + " dresser", "To help you do your hair. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",2", "nails" + Woods[i].Descriptor + ",4", "glassMolten,1" ], ["hammer,1", "saw,1"], Woods[i].CostMultiplier * 2, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level, "Construction")], (int) Woods[i].Tier));
                    toAdd.Add(new("Wardrobe", Woods[i].Name + " wardrobe", "A place to keep all your clothes. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",3", "nails" + Woods[i].Descriptor + ",6" ], ["hammer,1", "saw,1"], Woods[i].CostMultiplier * 3, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level, "Construction")], (int) Woods[i].Tier));
                    toAdd.Add(new("Globe", Woods[i].Name + " globe", "A wooden planet of your very own. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",3", "nails" + Woods[i].Descriptor + ",6" ], ["hammer,1", "saw,1"], Woods[i].CostMultiplier * 3, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level, "Construction")], (int) Woods[i].Tier));
                    toAdd.Add(new("Telescope", Woods[i].Name + " telescope", "Used for observing heavenly bodies. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",2", "nails" + Woods[i].Descriptor + ",4", "glassMolten,1" ], ["hammer,1", "saw,1"], Woods[i].CostMultiplier * 2, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level, "Construction")], (int) Woods[i].Tier, activateText: "You peer through the telescope at the night sky.", acts: [ new("POH", "Telescope", "", 0) ]));
                
                } else {
                    toAdd.Add(new("Bed", "Gilded elder bed", "Great for sleeping in. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",5", "nails" + Woods[i].Descriptor + ",10", "goldLeaf,2" ], ["hammer,1", "saw,1"], (Woods[i].CostMultiplier * 5) + 600, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level, "Construction")], (int) Woods[i].Tier, acts: [ new("POH", "Rest", "", 0)] ));
                    toAdd.Add(new("Corner", "Gilded elder clock", "Time is a tool you can put on the wall or wear it on your rizt. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",2", "nails" + Woods[i].Descriptor + ",4", "goldLeaf,1" ], ["hammer,1", "saw,1"], (Woods[i].CostMultiplier * 2) + 300, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level + 5, "Construction")], (int) Woods[i].Tier));
                    toAdd.Add(new("Dresser", "Gilded elder dresser", "To help you do your hair. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",2", "nails" + Woods[i].Descriptor + ",4", "glassMolten,2", "goldLeaf,1" ], ["hammer,1", "saw,1"], (Woods[i].CostMultiplier * 2) + 300, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level, "Construction")], (int) Woods[i].Tier));
                    toAdd.Add(new("Wardrobe", "Gilded elder wardrobe", "A place to keep all your clothes. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",3", "nails" + Woods[i].Descriptor + ",6", "goldLeaf,1" ], ["hammer,1", "saw,1"], (Woods[i].CostMultiplier * 3) + 300, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level, "Construction")], (int) Woods[i].Tier));
                    toAdd.Add(new("Globe", "Gilded elder globe", "A wooden planet of your very own. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",3", "nails" + Woods[i].Descriptor + ",6", "goldLeaf,1" ], ["hammer,1", "saw,1"], (Woods[i].CostMultiplier * 3) + 300, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level, "Construction")], (int) Woods[i].Tier));
                    toAdd.Add(new("Telescope", "Gilded elder telescope", "Used for observing heavenly bodies. Provides " + (int) Woods[i].Tier + " comfort.", [ "plank" + Woods[i].Name + ",2", "nails" + Woods[i].Descriptor + ",4", "glassMolten,1", "goldLeaf,1" ], ["hammer,1", "saw,1"], (Woods[i].CostMultiplier * 2) + 300, Woods[i].R, Woods[i].G, Woods[i].B, [ new("Skill", Woods[i].Level, "Construction")], (int) Woods[i].Tier, activateText: "You peer through the telescope at the night sky.", acts: [ new("POH", "Telescope", "", 0) ]));
                }
            }

            // Parlor
            {
                toAdd.Add(new("Teleport Focus", "Teleportation focus", "Allows you to teleport to this house, provided the house is active. Only one needed per house.", [ "plankMaple,8", "nailsAdamant,16", "stoneMagic" ], ["hammer,1", "saw,1"], 2200, Color.MediumPurple, [ new("Skill", 49, "Construction")]));
                
                toAdd.Add(new("Curtains", "Torn curtains", "Some ragged curtains. Provides 1 comfort.", [ "plankPine,3", "nailsBronze,6", "clothBolt,3" ], ["hammer,1", "saw,1"], 135, Color.Crimson.GetDarker(), [ new("Skill", 2, "Construction")], 1));
                toAdd.Add(new("Curtains", "Curtains", "Some nice curtains. Provides 2 comfort.", [ "plankOak,3", "nailsIron,6", "clothBolt,3" ], ["hammer,1", "saw,1"], 225, Color.Crimson.GetDark(), [ new("Skill", 18, "Construction")], 2));
                toAdd.Add(new("Curtains", "Opulent curtains", "Some gaudy curtains. Provides 5 comfort.", [ "plankMaple,3", "nailsAdamant,6", "clothBolt,3" ], ["hammer,1", "saw,1"], 495, Color.Crimson, [ new("Skill", 40, "Construction")], 5));
            
                toAdd.Add(new("Fireplace", "Clay fireplace", "A clay fireplace. May be used for Cooking. Provides 1 comfort.", [ "claySoft,3"  ], ["hammer,1", "saw,1"], 30, Color.SaddleBrown, [ new("Skill", 3, "Construction")], 1, station: "Fire"));
                toAdd.Add(new("Fireplace", "Stone fireplace", "A limestone fireplace. May be used for Cooking. Provides 4 comfort.", [ "blockLimestone,3"  ], ["hammer,1", "saw,1"], 40, Color.DarkGray, [ new("Skill", 33, "Construction")], 4, station: "Fire"));
                toAdd.Add(new("Fireplace", "Marble fireplace", "A marble fireplace. May be used for Cooking. Provides 7 comfort.", [ "blockMarble,3"  ], ["hammer,1", "saw,1"], 1500, Color.White, [ new("Skill", 63, "Construction")], 7, station: "Fire"));
            
                toAdd.Add(new("Rug", "Brown rug", "An ugly rug, but better than a bare floor. Provides 1 comfort.", [ "clothBolt,2"  ], ["hammer,1", "saw,1"], 30, Color.SaddleBrown, [ new("Skill", 2, "Construction")], 1));
                toAdd.Add(new("Rug", "Rug", "A nice rug. Provides 2 comfort.", [ "clothBolt,4"  ], ["hammer,1", "saw,1"], 60, Color.Maroon, [ new("Skill", 13, "Construction")], 2));
                toAdd.Add(new("Rug", "Opulent rug", "An opulent rug woven with gold leaf. Provides 7 comfort.", [ "clothBolt,4", "goldLeaf,1"  ], ["hammer,1", "saw,1"], 360, Color.Crimson, [ new("Skill", 65, "Construction")], 7));
            }

            // Garden
            {
                toAdd.Add(new("Tree", "Pine tree", "One of the most common trees in Gielinor. Regrows after chopping.", [ "plantPotPine,1"  ], ["shovel,1"], 30, ColorLib.Pine, [ new("Skill", 1, "Construction"), new("Skill", 1, "Farming")], 0, "treePine"));
                toAdd.Add(new("Tree", "Oak tree", "A beautiful old oak. Regrows after chopping.", [ "plantPotOak,1"  ], ["shovel,1"], 60, ColorLib.Oak, [ new("Skill", 10, "Construction"), new("Skill", 10, "Farming")], 0, "treeOak"));
                toAdd.Add(new("Tree", "Willow tree", "Usually grows near water. Regrows after chopping.", [ "plantPotWillow,1"  ], ["shovel,1"], 90, ColorLib.Willow, [ new("Skill", 20, "Construction"), new("Skill", 20, "Farming")], 0, "treeWillow"));
                toAdd.Add(new("Tree", "Teak tree", "A beautiful old teak tree. Regrows after chopping.", [ "plantPotTeak,1"  ], ["shovel,1"], 120, ColorLib.Teak, [ new("Skill", 30, "Construction"), new("Skill", 30, "Farming")], 0, "treeTeak"));
                toAdd.Add(new("Tree", "Maple tree", "I bet this make good syrup! Regrows after chopping.", [ "plantPotMaple,1"  ], ["shovel,1"], 150, ColorLib.Maple, [ new("Skill", 40, "Construction"), new("Skill", 40, "Farming")], 0, "treeMaple"));
                toAdd.Add(new("Tree", "Acadia tree", "Provides some welcome shade. Regrows after chopping.", [ "plantPotAcadia,1"  ], ["shovel,1"], 180, ColorLib.Acadia, [ new("Skill", 50, "Construction"), new("Skill", 50, "Farming")], 0, "treeAcadia"));
                toAdd.Add(new("Tree", "Mahogany tree", "A beautiful old mahogany tree. Regrows after chopping.", [ "plantPotMahogany,1"  ], ["shovel,1"], 210, ColorLib.Mahogany, [ new("Skill", 60, "Construction"), new("Skill", 60, "Farming")], 0, "treeMahogany"));
                toAdd.Add(new("Tree", "Yew tree", "A splendid tree. Regrows after chopping.", [ "plantPotYew,1"  ], ["shovel,1"], 240, ColorLib.Yew, [ new("Skill", 70, "Construction"), new("Skill", 70, "Farming")], 0, "treeYew"));
                toAdd.Add(new("Tree", "Magic tree", "The tree shimmers with a magical force. Regrows after chopping.", [ "plantPotMagic,1"  ], ["shovel,1"], 270, ColorLib.Magic, [ new("Skill", 80, "Construction"), new("Skill", 80, "Farming")], 0, "treeMagic"));
                toAdd.Add(new("Tree", "Elder tree", "The aged tree dominates with its huge presence. Regrows after chopping.", [ "plantPotElder,1"  ], ["shovel,1"], 300, ColorLib.Elder, [ new("Skill", 90, "Construction"), new("Skill", 90, "Farming")], 0, "treeElder"));
             
                toAdd.Add(new("Big Plant", "Redberry bush", "Thurgo would love this. Pies on demand!", [ "seedRedberry,10"  ], ["shovel,1"], 30, Color.ForestGreen, [ new("Skill", 5, "Construction"), new("Skill", 10, "Farming")], 0, "plantRedberry"));
                toAdd.Add(new("Big Plant", "Cadava bush", "Why do you need a regular source of cadava berries?", [ "seedCadava,5"  ], ["shovel,1"], 30, Color.ForestGreen, [ new("Skill", 10, "Construction"), new("Skill", 22, "Farming")], 0, "plantCadava"));
                toAdd.Add(new("Big Plant", "Doogle bush", "Don't tell Fluffs.", [ "doogle,5"  ], ["shovel,1"], 30, Color.ForestGreen, [ new("Skill", 5, "Construction"), new("Skill", 5, "Farming")], 0, "plantDoogle"));
                toAdd.Add(new("Big Plant", "Nettle bush", "All the nettle tea you want, but at what cost?", [ "nettles,5"  ], ["shovel,1"], 30, Color.ForestGreen, [ new("Skill", 5, "Construction"), new("Skill", 5, "Farming")], 0, "plantNettles"));
                // TODO: Requires stuff from Forestry events, revisit this when that gets figured out
                //toAdd.Add(new("Big Plant", "Beehive", "The bees buzz happily. Provides honeycomb and wax.", [ "nettles,5"  ], ["shovel,1"], 30, Color.ForestGreen, [ new("Skill", 5, "Construction"), new("Skill", 5, "Farming")], 0, "plantNettles"));
            
                toAdd.Add(new("Small Plant", "Marigolds", "A small patch of marigolds. Lovely!", [ "seedMarigold,5"  ], ["shovel,1"], 15, 255, 165, 0, [ new("Skill", 1, "Construction"), new("Skill", 2, "Farming")], 1));
                toAdd.Add(new("Small Plant", "Rosemary", "A small patch of rosemary. Lovely!", [ "seedRosemary,5"  ], ["shovel,1"], 15, 255, 165, 0, [ new("Skill", 1, "Construction"), new("Skill", 11, "Farming")], 2));
                toAdd.Add(new("Small Plant", "Nasturtiums", "A small patch of nasturtiums. Lovely!", [ "seedNasturtium,5"  ], ["shovel,1"], 15, 255, 0, 0, [ new("Skill", 1, "Construction"), new("Skill", 24, "Farming")], 3));
                toAdd.Add(new("Small Plant", "Trollweiss", "Very rare flowers with a pungent odor.", [ "trollweiss,1"  ], ["shovel,1"], 15, Color.SkyBlue, [ new("Skill", 1, "Construction"), new("Skill", 32, "Farming")], 4));
                toAdd.Add(new("Small Plant", "White Lilies", "A small patch of white lilies. Lovely!", [ "seedWhiteLily,5"  ], ["shovel,1"], 15, Color.White, [ new("Skill", 1, "Construction"), new("Skill", 52, "Farming")], 6));
            
                toAdd.Add(new("Plot", "Sand pit", "A box of sand you can fill buckets with.", [ "bucketSand,4", "plankPine,8", "nailsBronze,16" ], ["hammer,1", "saw,1", "shovel,1"], 300, Color.SandyBrown, [ new("Skill", 1, "Construction")], station: "Sand")); 
                toAdd.Add(new("Plot", "Clay deposit", "Some unearthed clay you could mine.", [ "clayDust,8", "plankPine,8", "nailsBronze,16" ], ["hammer,1", "saw,1", "shovel,1"], 300, Color.SandyBrown, [ new("Skill", 1, "Construction")], gather: "rockClay"));

                toAdd.Add(new("Plot", "Allotment patch", "An allotment patch, allowing you to grow produce.", [ "bucketCompost,4", "plankPine,8", "nailsBronze,16" ], ["hammer,1", "saw,1", "shovel,1"], 300, Color.SaddleBrown, [ new("Skill", 1, "Construction")], patch: "Allotment"));
                toAdd.Add(new("Plot", "Flower patch", "A flower patch, allowing you to grow flowers.", [ "bucketCompost,4", "plankOak,8", "nailsIron,16" ], ["hammer,1", "saw,1", "shovel,1"], 540, Color.SaddleBrown, [ new("Skill", 10, "Construction")], patch: "Flower"));
                toAdd.Add(new("Plot", "Herb patch", "An herb patch, allowing you to grow herbs.", [ "bucketCompost,4", "plankWillow,8", "nailsSteel,16" ], ["hammer,1", "saw,1", "shovel,1"], 780, Color.SaddleBrown, [ new("Skill", 20, "Construction")], patch: "Herb"));
                toAdd.Add(new("Plot", "Hops patch", "A hops patch, allowing you to grow hops.", [ "bucketCompost,4", "plankTeak,8", "nailsMithril,16" ], ["hammer,1", "saw,1", "shovel,1"], 1020, Color.SaddleBrown, [ new("Skill", 30, "Construction")], patch: "Hops"));
                toAdd.Add(new("Plot", "Bush patch", "A bush patch, allowing you to grow a bush.", [ "bucketCompost,4", "plankMaple,8", "nailsAdamant,16" ], ["hammer,1", "saw,1", "shovel,1"], 1260, Color.SaddleBrown, [ new("Skill", 40, "Construction")], patch: "Bush"));
                toAdd.Add(new("Plot", "Fruit tree patch", "A fruit tree patch, allowing you to grow a fruit tree.", [ "bucketCompost,4", "plankAcadia,8", "nailsRune,16" ], ["hammer,1", "saw,1", "shovel,1"], 1500, Color.SaddleBrown, [ new("Skill", 50, "Construction")], patch: "Fruit Tree"));
                toAdd.Add(new("Plot", "Tree patch", "A tree patch, allowing you to grow a tree.", [ "bucketCompost,4", "plankMahogany,8", "nailsOrichalcum,16" ], ["hammer,1", "saw,1", "shovel,1"], 1740, Color.SaddleBrown, [ new("Skill", 60, "Construction")], patch: "Tree"));
                toAdd.Add(new("Plot", "Cactus patch", "A cactus patch, allowing you to grow cacti.", [ "bucketCompost,4", "plankYew,8", "nailsNecronium,16" ], ["hammer,1", "saw,1", "shovel,1"], 1980, Color.SaddleBrown, [ new("Skill", 70, "Construction")], patch: "Cactus"));
            }

            // Kitchen
            {
                toAdd.Add(new("Keg", "Beer keg", "A keg full of beer.", [ "plankPine,3", "nailsBronze,6", "beer,8"  ], ["hammer,1", "saw,1"], 90, Color.SaddleBrown, [ new("Skill", 7, "Construction"), new("Skill", 5, "Cooking")], station: "Beer Keg"));
                toAdd.Add(new("Keg", "Cider keg", "A keg full of cider.", [ "plankOak,3", "nailsIron,6", "cider,8"  ], ["hammer,1", "saw,1"], 180, Color.SaddleBrown, [ new("Skill", 12, "Construction"), new("Skill", 14, "Cooking")], station: "Cider Keg"));
                toAdd.Add(new("Keg", "Asgarnian ale keg", "A keg full of asgarnian ale.", [ "plankWillow,3", "nailsSteel,6", "aleAsgarnian,8"  ], ["hammer,1", "saw,1"], 270, Color.SaddleBrown, [ new("Skill", 18, "Construction"), new("Skill", 24, "Cooking")], station: "Asgarnian Ale Keg"));
                toAdd.Add(new("Keg", "Greenman's ale keg", "A keg full of greenman's ale.", [ "plankWillow,3", "nailsSteel,6", "aleGreenman,8"  ], ["hammer,1", "saw,1"], 270, Color.SaddleBrown, [ new("Skill", 26, "Construction"), new("Skill", 29, "Cooking")], station: "Greenman's Ale Keg"));
                toAdd.Add(new("Keg", "Dragon bitter keg", "A keg full of dragon bitter.", [ "plankTeak,3", "nailsMithril,6", "dragonBitter,8"  ], ["hammer,1", "saw,1"], 360, Color.SaddleBrown, [ new("Skill", 36, "Construction"), new("Skill", 39, "Cooking")], station: "Dragon Bitter Keg"));
                toAdd.Add(new("Keg", "Chef's delight keg", "A keg full of chef's delight.", [ "plankMaple,3", "nailsAdamant,6", "chefsDelight,8"  ], ["hammer,1", "saw,1"], 450, Color.SaddleBrown, [ new("Skill", 48, "Construction"), new("Skill", 44, "Cooking")], station: "Chef's Delight Keg"));
            
                toAdd.Add(new("Cat Basket", "Cat blanket", "A place for your pet to sleep. Provides 1 comfort.", [ "clothBolt,1"  ], ["hammer,1", "saw,1"], 15, Color.SaddleBrown, [ new("Skill", 5, "Construction")], 1));
                toAdd.Add(new("Cat Basket", "Cat basket", "Your pet would love to sleep here. Provides 2 comfort.", [ "clothBolt,1", "plankOak,2", "nailsIron,4"  ], ["hammer,1", "saw,1"], 135, Color.SaddleBrown, [ new("Skill", 19, "Construction")], 2));
                toAdd.Add(new("Cat Basket", "Cushioned basket", "A luxurious sleeping place for your pet. Provides 4 comfort.", [ "woolRaw,2", "plankTeak,2", "nailsMithril,4" ], ["hammer,1", "saw,1"], 250, Color.Crimson, [ new("Skill", 33, "Construction")], 4));
            
                toAdd.Add(new("Larder", "Pine larder", "A pine larder to keep food cool.", [ "plankPine,8", "nailsBronze,16"  ], ["hammer,1", "saw,1"], 240, ColorLib.Pine, [ new("Skill", 1, "Construction")]) { ShopItems = [ "bucketMilk", "teaLeaves" ]});
                toAdd.Add(new("Larder", "Oak larder", "An oak larder to keep food cool.", [ "plankOak,8", "nailsIron,16"  ], ["hammer,1", "saw,1"], 480, ColorLib.Oak, [ new("Skill", 10, "Construction")]) { ShopItems = [ "bucketMilk", "teaLeaves", "potFlour" ]});
                toAdd.Add(new("Larder", "Willow larder", "A willow larder to keep food cool.", [ "plankWillow,8", "nailsSteel,16"  ], ["hammer,1", "saw,1"], 720, ColorLib.Willow, [ new("Skill", 20, "Construction")]) { ShopItems = [ "bucketMilk", "teaLeaves", "potFlour", "eggChicken" ]});
                toAdd.Add(new("Larder", "Teak larder", "A teak larder to keep food cool.", [ "plankTeak,8", "nailsMithril,16"  ], ["hammer,1", "saw,1"], 960, ColorLib.Teak, [ new("Skill", 30, "Construction")]) { ShopItems = [ "bucketMilk", "teaLeaves", "potFlour", "eggChicken", "cheese" ]});
                toAdd.Add(new("Larder", "Maple larder", "A maple larder to keep food cool.", [ "plankMaple,8", "nailsAdamant,16"  ], ["hammer,1", "saw,1"], 1200, ColorLib.Maple, [ new("Skill", 40, "Construction")]) { ShopItems = [ "bucketMilk", "teaLeaves", "potFlour", "eggChicken", "cheese", "potato" ]});
                toAdd.Add(new("Larder", "Acadia larder", "An acadia larder to keep food cool.", [ "plankAcadia,8", "nailsRune,16"  ], ["hammer,1", "saw,1"], 1440, ColorLib.Acadia, [ new("Skill", 50, "Construction")]) { ShopItems = [ "bucketMilk", "teaLeaves", "potFlour", "eggChicken", "cheese", "potato", "garlic" ]});
                toAdd.Add(new("Larder", "Mahogany larder", "A mahogany larder to keep food cool.", [ "plankMahogany,8", "nailsOrichalcum,16"  ], ["hammer,1", "saw,1"], 1680, ColorLib.Mahogany, [ new("Skill", 60, "Construction")]) { ShopItems = [ "bucketMilk", "teaLeaves", "potFlour", "eggChicken", "cheese", "potato", "garlic", "onion" ]});
                toAdd.Add(new("Larder", "Yew larder", "A yew larder to keep food cool.", [ "plankYew,8", "nailsNecronium,16"  ], ["hammer,1", "saw,1"], 1920, ColorLib.Yew, [ new("Skill", 70, "Construction")]) { ShopItems = [ "bucketMilk", "teaLeaves", "potFlour", "eggChicken", "cheese", "potato", "garlic", "onion", "tomato" ]});
            
                toAdd.Add(new("Shelf", "Pine shelf", "Shelves full of kitchen utensils.", [ "plankPine,3", "nailsBronze,6"  ], ["hammer,1", "saw,1"], 90, ColorLib.Pine, [ new("Skill", 5, "Construction")]) { ShopItems = [ "kettleEmpty", "teapotClayEmpty", "cupClayEmpty" ]});
                toAdd.Add(new("Shelf", "Oak shelf", "Shelves full of kitchen utensils.", [ "plankOak,3", "nailsIron,6", "claySoft,6" ], ["hammer,1", "saw,1"], 240, ColorLib.Oak, [ new("Skill", 15, "Construction")]) { ShopItems = [ "kettleEmpty", "teapotClayEmpty", "cupClayEmpty", "beerGlass" ]});
                toAdd.Add(new("Shelf", "Willow shelf", "Shelves full of kitchen utensils.", [ "plankWillow,3", "nailsSteel,6", "claySoft,6" ], ["hammer,1", "saw,1"], 330, ColorLib.Willow, [ new("Skill", 25, "Construction")]) { ShopItems = [ "kettleEmpty", "teapotPorcelainEmpty", "cupPorcelainEmpty", "beerGlass", "tinCakeEmpty" ]});
                toAdd.Add(new("Shelf", "Teak shelf", "Shelves full of kitchen utensils.", [ "plankTeak,3", "nailsMithril,6", "claySoft,6" ], ["hammer,1", "saw,1"], 420, ColorLib.Teak, [ new("Skill", 35, "Construction")]) { ShopItems = [ "kettleEmpty", "teapotPorcelainEmpty", "cupPorcelainEmpty", "beerGlass", "tinCakeEmpty", "bowlEmpty" ]});
                toAdd.Add(new("Shelf", "Maple shelf", "Shelves full of kitchen utensils.", [ "plankMaple,3", "nailsAdamant,6", "claySoft,6" ], ["hammer,1", "saw,1"], 510, ColorLib.Maple, [ new("Skill", 45, "Construction")]) { ShopItems = [ "kettleEmpty", "teapotPorcelainEmpty", "cupPorcelainEmpty", "beerGlass", "tinCakeEmpty", "bowlEmpty", "pieShell" ]});
                toAdd.Add(new("Shelf", "Acadia shelf", "Shelves full of kitchen utensils.", [ "plankAcadia,3", "nailsRune,6", "claySoft,6" ], ["hammer,1", "saw,1"], 600, ColorLib.Acadia, [ new("Skill", 55, "Construction")]) { ShopItems = [ "kettleEmpty", "teapotPorcelainEmpty", "cupPorcelainEmpty", "beerGlass", "tinCakeEmpty", "bowlEmpty", "pieShell", "potEmpty" ]});
                toAdd.Add(new("Shelf", "Mahogany shelf", "Shelves full of kitchen utensils.", [ "plankMahogany,3", "nailsOrichalcum,6", "claySoft,6", "goldLeaf,2" ], ["hammer,1", "saw,1"], 1290, ColorLib.Mahogany, [ new("Skill", 65, "Construction")]) { ShopItems = [ "kettleEmpty", "teapotAntiqueEmpty", "cupAntiqueEmpty", "beerGlass", "tinCakeEmpty", "bowlEmpty", "pieShell", "potEmpty", "chefHat" ]});
            
                toAdd.Add(new("Sink", "Pump and drain", "Shelves full of kitchen utensils.", [ "barBronze,5"  ], ["hammer,1", "saw,1"], 25, ColorLib.Bronze, [ new("Skill", 7, "Construction")], station: "Sink"));
                toAdd.Add(new("Sink", "Pump and tub", "Shelves full of kitchen utensils.", [ "barSteel,10"  ], ["hammer,1", "saw,1"], 150, ColorLib.Steel, [ new("Skill", 27, "Construction")], station: "Sink"));
                toAdd.Add(new("Sink", "Sink", "Shelves full of kitchen utensils.", [ "barAdamant,15"  ], ["hammer,1", "saw,1"], 375, ColorLib.Adamant, [ new("Skill", 47, "Construction")], station: "Sink"));
                toAdd.Add(new("Sink", "Gold sink", "Shelves full of kitchen utensils.", [ "goldCondensed,10", "plankAcadia,5", "nailsRune,10", "goldLeaf,5"  ], ["hammer,1", "saw,1"], 11144, Color.Gold, [ new("Skill", 47, "Construction")], station: "Sink"));
            
                toAdd.Add(new("Spice Rack", "Spice rack", "Rack used to store spices.", [ "plankMahogany,3", "nailsOrichalcum,6", "claySoft,6", "spiceBrown", "spiceOrange", "spiceRed", "spiceYellow"  ], ["hammer,1", "saw,1"], 630, ColorLib.Mahogany, [ new("Skill", 60, "Construction")]) { ShopItems = [ "spiceBrown", "spiceOrange", "spiceRed", "spiceYellow", "spiceGnome" ]});
            
                toAdd.Add(new("Stove", "Firepit", "A basic cooking fire.", [ "barBronze,1", "claySoft,2" ], ["hammer,1", "saw,1"], 25, Color.SandyBrown, [ new("Skill", 5, "Construction")], station: "Fire"));
                toAdd.Add(new("Stove", "Firepit with hook", "A cooking fire.", [ "barIron,2", "claySoft,2" ], ["hammer,1", "saw,1"], 40, Color.SandyBrown, [ new("Skill", 11, "Construction")], station: "Fire"));
                toAdd.Add(new("Stove", "Small oven", "Good for baking bread.", [ "barSteel,4" ], ["hammer,1", "saw,1"], 60, Color.SandyBrown, [ new("Skill", 24, "Construction")], station: "Fire"));
                toAdd.Add(new("Stove", "Large oven", "Good for baking pizza.", [ "barSteel,5" ], ["hammer,1", "saw,1"], 75, Color.SandyBrown, [ new("Skill", 29, "Construction")], station: "Fire"));
                toAdd.Add(new("Stove", "Mithril range", "Finally a proper range.", [ "barMithril,6" ], ["hammer,1", "saw,1"], 120, ColorLib.Mithril, [ new("Skill", 34, "Construction")], station: "Range"));
                toAdd.Add(new("Stove", "Adamant range", "A fairly fancy frange.", [ "barAdamant,8" ], ["hammer,1", "saw,1"], 200, ColorLib.Adamant, [ new("Skill", 42, "Construction")], station: "Range"));
            }

            // Workshop
            {
                toAdd.Add(new("Repair Bench", "Repair bench", "Allows you to repair broken arrows and broken staves.", [ "plankOak,2", "nailsIron,4"  ], ["hammer,1", "saw,1"], 120, ColorLib.Oak, [ new("Skill", 15, "Construction")], station: "Repair Bench"));
                toAdd.Add(new("Repair Bench", "Whetstone", "Allows you to repair broken arrows, broken staves, and rusty swords.", [ "plankTeak,2", "nailsMithril,4", "blockLimestone,1" ], ["hammer,1", "saw,1"], 240, ColorLib.Teak, [ new("Skill", 35, "Construction")], station: "Whetstone"));
            
                toAdd.Add(new("Tool Rack", "Basic tool rack", "Provides saws, hammers, chisels, and shears.", [ "plankOak,2", "nailsIron,4"  ], ["hammer,1", "saw,1"], 120, ColorLib.Oak, [ new("Skill", 15, "Construction")], shops: [ "saw", "hammer", "chisel", "shears" ]));
                toAdd.Add(new("Tool Rack", "Advanced tool rack", "Basic Tool Rack, but also buckets, knives, spades, and tinderboxes.", [ "plankWillow,2", "nailsSteel,4"  ], ["hammer,1", "saw,1"], 180, ColorLib.Willow, [ new("Skill", 25, "Construction")], shops: [ "saw", "hammer", "chisel", "shears", "bucketEmpty", "knife", "shovel", "tinderbox" ]));
                toAdd.Add(new("Tool Rack", "Expert tool rack", "Advanced Tool Rack, but also brown aprons, glassblowing pipes, and needles.", [ "plankTeak,2", "nailsMithril,4"  ], ["hammer,1", "saw,1"], 240, ColorLib.Teak, [ new("Skill", 35, "Construction")], shops: [ "saw", "hammer", "chisel", "shears", "bucketEmpty", "knife", "shovel", "tinderbox", "brownApron", "glassblowingPipe", "needle" ]));
                toAdd.Add(new("Tool Rack", "Master tool rack", "Expert Tool Rack, but also amulet, necklace, ring, holy symbol, bracelet, and tiara moulds.", [ "plankMaple,2", "nailsAdamant,4"  ], ["hammer,1", "saw,1"], 300, ColorLib.Maple, [ new("Skill", 44, "Construction")], shops: [ "saw", "hammer", "chisel", "shears", "bucketEmpty", "knife", "shovel", "tinderbox", "brownApron", "glassblowingPipe", "needle" ]));
            
                toAdd.Add(new("Primary Station", "Pottery wheel", "Allows you to work clay.", [ "blockLimestone,1", "plankOak,2", "nailsIron,4" ], ["hammer,1", "saw,1"], 75, Color.DimGray, [ new("Skill", 10, "Construction")], station: "Pottery Wheel"));
                toAdd.Add(new("Primary Station", "Herblore workbench", "Makes it easier to turn herbs into potions.", [ "plankWillow,4", "nailsSteel,8" ], ["hammer,1", "saw,1"], 360, ColorLib.Willow, [ new("Skill", 20, "Construction")], station: "Herblore Workbench"));
            
                toAdd.Add(new("Primary Station", "Bronze anvil", "Allows you to work metals up to Bronze.", [ "barBronze,5", "plankPine,2", "nailsBronze,4" ], ["hammer,1", "saw,1"], 85, ColorLib.Bronze, [ new("Skill", 1, "Construction")], station: "Bronze Anvil"));
                toAdd.Add(new("Primary Station", "Iron anvil", "Allows you to work metals up to Iron.", [ "barIron,5", "plankOak,2", "nailsIron,4" ], ["hammer,1", "saw,1"], 170, ColorLib.Iron, [ new("Skill", 10, "Construction")], station: "Iron Anvil"));
                toAdd.Add(new("Primary Station", "Steel anvil", "Allows you to work metals up to Steel.", [ "barSteel,5", "plankWillow,2", "nailsSteel,4" ], ["hammer,1", "saw,1"], 255, ColorLib.Steel, [ new("Skill", 20, "Construction")], station: "Steel Anvil"));
                toAdd.Add(new("Primary Station", "Mithril anvil", "Allows you to work metals up to Mithril.", [ "barMithril,5", "plankTeak,2", "nailsMithril,4" ], ["hammer,1", "saw,1"], 340, ColorLib.Mithril, [ new("Skill", 30, "Construction")], station: "Mithril Anvil"));
                toAdd.Add(new("Primary Station", "Adamant anvil", "Allows you to work metals up to Adamant.", [ "barAdamant,5", "plankMaple,2", "nailsAdamant,4" ], ["hammer,1", "saw,1"], 425, ColorLib.Adamant, [ new("Skill", 40, "Construction")], station: "Adamant Anvil"));
                toAdd.Add(new("Primary Station", "Rune anvil", "Allows you to work metals up to Rune.", [ "barRune,5", "plankAcadia,2", "nailsRune,4" ], ["hammer,1", "saw,1"], 510, ColorLib.Rune, [ new("Skill", 50, "Construction")], station: "Rune Anvil"));
                toAdd.Add(new("Primary Station", "Orichalcum anvil", "Allows you to work metals up to Orichalcum.", [ "barOrichalcum,5", "plankMahogany,2", "nailsOrichalcum,4" ], ["hammer,1", "saw,1"], 595, Color.Crimson, [ new("Skill", 60, "Construction")], station: "Orichalcum Anvil"));
                toAdd.Add(new("Primary Station", "Necronium anvil", "Allows you to work metals up to Necronium.", [ "barNecronium,5", "plankYew,2", "nailsNecronium,4" ], ["hammer,1", "saw,1"], 680, Color.ForestGreen, [ new("Skill", 70, "Construction")], station: "Necronium Anvil"));
                toAdd.Add(new("Primary Station", "Bane anvil", "Allows you to work metals up to Bane.", [ "barBane,5", "plankMagic,2", "nailsBane,4" ], ["hammer,1", "saw,1"], 765, Color.MediumSlateBlue, [ new("Skill", 80, "Construction")], station: "Bane Anvil"));
                toAdd.Add(new("Primary Station", "Elder rune anvil", "Allows you to work metals up to Elder Rune.", [ "barElderRune,5", "plankElder,2", "nailsElderRune,4" ], ["hammer,1", "saw,1"], 850, ColorLib.Rune.GetBrighter(), [ new("Skill", 90, "Construction")], station: "Elder Rune Anvil"));
            
                toAdd.Add(new("Secondary Station", "Bronze furnace", "Allows you to smelt metals up to Bronze.", [ "barBronze,15", "plankPine,2", "nailsBronze,4" ], ["hammer,1", "saw,1"], 135, ColorLib.Bronze, [ new("Skill", 1, "Construction")], station: "Bronze Furnace"));
                toAdd.Add(new("Secondary Station", "Iron furnace", "Allows you to smelt metals up to Iron.", [ "barIron,15", "plankOak,2", "nailsIron,4" ], ["hammer,1", "saw,1"], 270, ColorLib.Iron, [ new("Skill", 10, "Construction")], station: "Iron Furnace"));
                toAdd.Add(new("Secondary Station", "Steel furnace", "Allows you to smelt metals up to Steel.", [ "barSteel,15", "plankWillow,2", "nailsSteel,4" ], ["hammer,1", "saw,1"], 405, ColorLib.Steel, [ new("Skill", 20, "Construction")], station: "Steel Furnace"));
                toAdd.Add(new("Secondary Station", "Mithril furnace", "Allows you to smelt metals up to Mithril.", [ "barMithril,15", "plankTeak,2", "nailsMithril,4" ], ["hammer,1", "saw,1"], 540, ColorLib.Mithril, [ new("Skill", 30, "Construction")], station: "Mithril Furnace"));
                toAdd.Add(new("Secondary Station", "Adamant furnace", "Allows you to smelt metals up to Adamant.", [ "barAdamant,15", "plankMaple,2", "nailsAdamant,4" ], ["hammer,1", "saw,1"], 675, ColorLib.Adamant, [ new("Skill", 40, "Construction")], station: "Adamant Furnace"));
                toAdd.Add(new("Secondary Station", "Rune furnace", "Allows you to smelt metals up to Rune.", [ "barRune,15", "plankAcadia,2", "nailsRune,4" ], ["hammer,1", "saw,1"], 810, ColorLib.Rune, [ new("Skill", 50, "Construction")], station: "Rune Furnace"));
                toAdd.Add(new("Secondary Station", "Orichalcum furnace", "Allows you to smelt metals up to Orichalcum.", [ "barOrichalcum,15", "plankMahogany,2", "nailsOrichalcum,4" ], ["hammer,1", "saw,1"], 945, Color.Crimson, [ new("Skill", 60, "Construction")], station: "Orichalcum Furnace"));
                toAdd.Add(new("Secondary Station", "Necronium furnace", "Allows you to smelt metals up to Necronium.", [ "barNecronium,15", "plankYew,2", "nailsNecronium,4" ], ["hammer,1", "saw,1"], 1080, Color.ForestGreen, [ new("Skill", 70, "Construction")], station: "Necronium Furnace"));
                toAdd.Add(new("Secondary Station", "Bane furnace", "Allows you to smelt metals up to Bane.", [ "barBane,15", "plankMagic,2", "nailsBane,4" ], ["hammer,1", "saw,1"], 1215, Color.MediumSlateBlue, [ new("Skill", 80, "Construction")], station: "Bane Furnace"));
                toAdd.Add(new("Secondary Station", "Elder rune furnace", "Allows you to smelt metals up to Elder Rune.", [ "barElderRune,15", "plankElder,2", "nailsElderRune,4" ], ["hammer,1", "saw,1"], 1350, ColorLib.Rune.GetBrighter(), [ new("Skill", 90, "Construction")], station: "Elder Rune Furnace"));
            
                toAdd.Add(new("Secondary Station", "Pottery kiln", "Fires pottery into ceramics.", [ "claySoft,12" ], ["hammer,1", "saw,1"], 120, Color.SandyBrown, [ new("Skill", 10, "Construction")], station: "Pottery Kiln"));
                toAdd.Add(new("Secondary Station", "Crucible", "Allows you to cast molten metal into shape with moulds.", [ "barSteel,15" ], ["hammer,1", "saw,1"], 225, Color.DarkGray, [ new("Skill", 20, "Construction")], station: "Casting"));
                toAdd.Add(new("Secondary Station", "Spinning wheel", "Spins fibers into fabrics.", [ "plankWillow,4", "nailsSteel,8" ], ["hammer,1", "saw,1"], 360, ColorLib.Willow, [ new("Skill", 20, "Construction")], station: "Spinning Wheel"));
                toAdd.Add(new("Secondary Station", "Tanning rack", "Turns hides into leather.", [ "plankWillow,8", "nailsSteel,16" ], ["hammer,1", "saw,1"], 720, ColorLib.Willow, [ new("Skill", 20, "Construction")], station: "Tannery"));
                    /*
                    toAdd.Add(new("Clockwork Bench", "Toy bench", "Create toy horseys and wooden cats.", [ "plankOak,4", "nailsIron,8" ], ["hammer,1", "saw,1"], 240, Color.SandyBrown, [ new("Skill", 16, "Construction")], station: "Toy Bench"));
                    toAdd.Add(new("Clockwork Bench", "Basic clockwork bench", "Toy bench, but also clockwork mechanisms.", [ "plankWillow,4", "nailsSteel,8", "glassMolten,1" ], ["hammer,1", "saw,1"], 370, Color.SandyBrown, [ new("Skill", 25, "Construction")], station: "Basic Clockwork"));
                    toAdd.Add(new("Clockwork Bench", "Advanced clockwork bench", "Basic clockwork bench, but also toy soldiers, toy dolls, and clockwork suits.", [ "plankTeak,4", "nailsMithril,8", "glassMolten,3" ], ["hammer,1", "saw,1"], 510, Color.SandyBrown, [ new("Skill", 34, "Construction")], station: "Advanced Clockwork"));
                    toAdd.Add(new("Clockwork Bench", "Expert clockwork bench", "Create toy horseys and wooden cats.", [ "plankMaple,6", "nailsAdamant,8", "glassMolten,3" ], ["hammer,1", "saw,1"], 830, Color.SandyBrown, [ new("Skill", 42, "Construction")], station: "Expert Clockwork"));
                    */
            }

            // Study
            {
                toAdd.Add(new("Crystal Ball", "Crystal ball", "Allows you to freely change the element of elemental staves.", [ "plankMaple,3", "nailsAdamant,6", "orbUnpowered,1" ], ["hammer,1", "saw,1"], 450, ColorLib.Maple, [ new("Skill", 42, "Construction")], station: "Crystal Ball"));
                toAdd.Add(new("Crystal Ball", "Elemental sphere", "Allows you to also change battlestaff element, for 100 runes of the old type.", [ "plankAcadia,3", "nailsRune,6", "orbUnpowered,1", "goldLeaf,1" ], ["hammer,1", "saw,1"], 840, ColorLib.Acadia, [ new("Skill", 54, "Construction")], station: "Elemental Sphere"));
                toAdd.Add(new("Crystal Ball", "Crystal of power", "Allows you to also change mystic staff element, for 1000 runes of the old type.", [ "plankMahogany,3", "nailsOrichalcum,6", "orbUnpowered,1", "goldLeaf,2" ], ["hammer,1", "saw,1"], 1230, ColorLib.Mahogany, [ new("Skill", 66, "Construction")], station: "Crystal of Power"));
                
                toAdd.Add(new("Lectern", "Pine lectern", "Allows you to make spell tablets up to level 10.", [ "plankPine,2", "nailsBronze,4" ], ["hammer,1", "saw,1"], 60, ColorLib.Pine, [ new("Skill", 1, "Construction")], station: "Pine Lectern"));
                toAdd.Add(new("Lectern", "Oak lectern", "Allows you to make spell tablets up to level 20.", [ "plankOak,2", "nailsIron,4" ], ["hammer,1", "saw,1"], 120, ColorLib.Oak, [ new("Skill", 10, "Construction")], station: "Oak Lectern"));
                toAdd.Add(new("Lectern", "Willow lectern", "Allows you to make spell tablets up to level 30.", [ "plankWillow,2", "nailsSteel,4" ], ["hammer,1", "saw,1"], 180, ColorLib.Willow, [ new("Skill", 20, "Construction")], station: "Willow Lectern"));
                toAdd.Add(new("Lectern", "Teak lectern", "Allows you to make spell tablets up to level 40.", [ "plankTeak,2", "nailsMithril,4" ], ["hammer,1", "saw,1"], 240, ColorLib.Teak, [ new("Skill", 30, "Construction")], station: "Teak Lectern"));
                toAdd.Add(new("Lectern", "Maple lectern", "Allows you to make spell tablets up to level 50.", [ "plankMaple,2", "nailsAdamant,4" ], ["hammer,1", "saw,1"], 300, ColorLib.Maple, [ new("Skill", 40, "Construction")], station: "Maple Lectern"));
                toAdd.Add(new("Lectern", "Acadia lectern", "Allows you to make spell tablets up to level 60.", [ "plankAcadia,2", "nailsRune,4" ], ["hammer,1", "saw,1"], 360, ColorLib.Acadia, [ new("Skill", 50, "Construction")], station: "Acadia Lectern"));
                toAdd.Add(new("Lectern", "Mahogany lectern", "Allows you to make spell tablets up to level 70.", [ "plankMahogany,2", "nailsOrichalcum,4", "goldLeaf,1" ], ["hammer,1", "saw,1"], 720, ColorLib.Mahogany, [ new("Skill", 60, "Construction")], station: "Mahogany Lectern"));
                toAdd.Add(new("Lectern", "Yew lectern", "Allows you to make spell tablets up to level 80.", [ "plankYew,2", "nailsNecronium,4", "goldLeaf,1" ], ["hammer,1", "saw,1"], 780, ColorLib.Yew, [ new("Skill", 70, "Construction")], station: "Yew Lectern"));
                toAdd.Add(new("Lectern", "Magic lectern", "Allows you to make spell tablets up to level 90.", [ "plankMagic,2", "nailsBane,4", "goldLeaf,2" ], ["hammer,1", "saw,1"], 1140, ColorLib.Magic, [ new("Skill", 80, "Construction")], station: "Magic Lectern"));
                toAdd.Add(new("Lectern", "Elder lectern", "Allows you to make spell tablets up to level 99.", [ "plankElder,2", "nailsElderRune,4", "goldLeaf,2", "stoneMagic,1" ], ["hammer,1", "saw,1"], 2200, ColorLib.Elder, [ new("Skill", 90, "Construction")], station: "Elder Lectern"));
            }

            // Chapel
            {
                toAdd.Add(new("Altar", "Maple altar", "Allows you to offer bones for 100-200% Prayer experience.", [ "plankMaple,4", "nailsAdamant,8"], ["hammer,1", "saw,1"], 600, ColorLib.Maple, [ new("Skill", 45, "Construction")], gather: "altar", acts: [ new("POH", "OfferBones", "", 100)]));
                toAdd.Add(new("Altar", "Acadia altar", "Allows you to offer bones for 110-210% Prayer experience.", [ "plankAcadia,4", "nailsRune,8"], ["hammer,1", "saw,1"], 720, ColorLib.Acadia, [ new("Skill", 50, "Construction")], gather: "altar", acts: [ new("POH", "OfferBones", "", 110)]));
                toAdd.Add(new("Altar", "Cloth altar", "Allows you to offer bones for 125-225% Prayer experience.", [ "plankAcadia,4", "nailsRune,8", "clothBolt,2"], ["hammer,1", "saw,1"], 750, ColorLib.Acadia, [ new("Skill", 56, "Construction")], gather: "altar", acts: [ new("POH", "OfferBones", "", 125)]));
                toAdd.Add(new("Altar", "Mahogany altar", "Allows you to offer bones for 150-250% Prayer experience.", [ "plankMahogany,4", "nailsOrichalcum,8", "clothBolt,2"], ["hammer,1", "saw,1"], 870, ColorLib.Mahogany, [ new("Skill", 60, "Construction")], gather: "altar", acts: [ new("POH", "OfferBones", "", 150)]));
                toAdd.Add(new("Altar", "Limestone altar", "Allows you to offer bones for 175%-275% Prayer experience.", [ "plankMahogany,6", "nailsOrichalcum,12", "blockLimestone,2"], ["hammer,1", "saw,1"], 1280, Color.DarkGray, [ new("Skill", 64, "Construction")], gather: "altar", acts: [ new("POH", "OfferBones", "", 175)]));
                toAdd.Add(new("Altar", "Marble altar", "Allows you to offer bones for 200-300% Prayer experience.", [ "blockMarble,2", "clothBolt,2"], ["hammer,1", "saw,1"], 1030, Color.White, [ new("Skill", 70, "Construction")], gather: "altar", acts: [ new("POH", "OfferBones", "", 200)]));
                toAdd.Add(new("Altar", "Gilded altar", "Allows you to offer bones for 250-350% Prayer experience.", [ "blockMarble,2", "clothBolt,2", "goldLeaf,4"], ["hammer,1", "saw,1"], 2230, Color.AntiqueWhite, [ new("Skill", 75, "Construction")], gather: "altar", acts: [ new("POH", "OfferBones", "", 250)]));
                
                toAdd.Add(new("Lamp", "Adamant torches", "Lights the room up nicely.", [ "barAdamant,2" ], ["hammer,1", "saw,1"], 50, ColorLib.Adamant, [ new("Skill", 45, "Construction")]));
                toAdd.Add(new("Lamp", "Maple torches", "Lights the room up nicely.", [ "plankMaple,2", "nailsAdamant,4" ], ["hammer,1", "saw,1"], 300, ColorLib.Maple, [ new("Skill", 49, "Construction")]));
                toAdd.Add(new("Lamp", "Rune candlesticks", "Lights the room up nicely.", [ "barRune,6", "candle,6" ], ["hammer,1", "saw,1"], 180, ColorLib.Rune, [ new("Skill", 53, "Construction")]));
                toAdd.Add(new("Lamp", "Gilded rune candlesticks", "Lights the room up nicely.", [ "barRune,6", "candle,6", "goldLeaf,1" ], ["hammer,1", "saw,1"], 480, Color.Gold, [ new("Skill", 57, "Construction")]));
                toAdd.Add(new("Lamp", "Acadia incense burners", "Lights the room up nicely. Increases experience for offered bones by 50%.", [ "plankAcadia,4", "nailsRune,8", "barRune,2" ], ["hammer,1", "saw,1"], 780, ColorLib.Acadia, [ new("Skill", 59, "Construction")], acts: [ new("POH", "LightIncense", "", 0)]));
                toAdd.Add(new("Lamp", "Mahogany incense burners", "Lights the room up nicely. Increases experience for offered bones by 50%.", [ "plankMahogany,4", "nailsOrichalcum,8", "barOrichalcum,2" ], ["hammer,1", "saw,1"], 910, ColorLib.Mahogany, [ new("Skill", 65, "Construction")], acts: [ new("POH", "LightIncense", "", 0)]));
                toAdd.Add(new("Lamp", "Marble incense burners", "Lights the room up nicely. Increases experience for offered bones by 50%.", [ "blockMarble,2", "barOrichalcum,2" ], ["hammer,1", "saw,1"], 1070, Color.White, [ new("Skill", 69, "Construction")], acts: [ new("POH", "LightIncense", "", 0)]));
                
                toAdd.Add(new("Icon", "Guthix symbol", "Dedicate your chapel to Guthix, God of Balance. Provides 5 comfort.", [ "plankMaple,2", "nailsAdamant,4"], ["hammer,1", "saw,1"], 300, Color.Green, [ new("Skill", 48, "Construction")], 5));
                toAdd.Add(new("Icon", "Saradomin symbol", "Dedicate your chapel to Saradomin, God of Order. Provides 5 comfort.", [ "plankMaple,2", "nailsAdamant,4"], ["hammer,1", "saw,1"], 300, Color.Turquoise, [ new("Skill", 48, "Construction")], 5));
                toAdd.Add(new("Icon", "Zamorak symbol", "Dedicate your chapel to Zamorak, God of Chaos. Provides 5 comfort.", [ "plankMaple,2", "nailsAdamant,4"], ["hammer,1", "saw,1"], 300, Color.Crimson, [ new("Skill", 48, "Construction")], 5));
                toAdd.Add(new("Icon", "Armadyl symbol", "Dedicate your chapel to Armadyl, God of Justice. Provides 5 comfort.", [ "plankMaple,2", "nailsAdamant,4"], ["hammer,1", "saw,1"], 300, Color.White, [ new("Skill", 48, "Construction")], 5));
                toAdd.Add(new("Icon", "Bandos symbol", "Dedicate your chapel to Bandos, God of War. Provides 5 comfort.", [ "plankMaple,2", "nailsAdamant,4"], ["hammer,1", "saw,1"], 300, Color.DarkGoldenrod, [ new("Skill", 48, "Construction")], 5));
                toAdd.Add(new("Icon", "Guthix icon", "Dedicate your chapel to Guthix, God of Balance. Provides 6 comfort.", [ "plankAcadia,2", "nailsRune,4"], ["hammer,1", "saw,1"], 420, Color.Green, [ new("Skill", 59, "Construction")], 6));
                toAdd.Add(new("Icon", "Saradomin icon", "Dedicate your chapel to Saradomin, God of Order. Provides 6 comfort.", [ "plankAcadia,2", "nailsRune,4"], ["hammer,1", "saw,1"], 420, Color.Turquoise, [ new("Skill", 59, "Construction")], 6));
                toAdd.Add(new("Icon", "Zamorak icon", "Dedicate your chapel to Zamorak, God of Chaos. Provides 6 comfort.", [ "plankAcadia,2", "nailsRune,4"], ["hammer,1", "saw,1"], 420, Color.Crimson, [ new("Skill", 59, "Construction")], 6));
                toAdd.Add(new("Icon", "Armadyl icon", "Dedicate your chapel to Armadyl, God of Justice. Provides 6 comfort.", [ "plankAcadia,2", "nailsRune,4"], ["hammer,1", "saw,1"], 420, Color.White, [ new("Skill", 59, "Construction")], 6));
                toAdd.Add(new("Icon", "Bandos icon", "Dedicate your chapel to Bandos, God of War. Provides 6 comfort.", [ "plankAcadia,2", "nailsRune,4"], ["hammer,1", "saw,1"], 360, Color.DarkGoldenrod, [ new("Skill", 59, "Construction")], 6));
                toAdd.Add(new("Icon", "Bob icon", "Dedicate your chapel to Bob the Cat. Provides 8 comfort.", [ "plankYew,2", "nailsNecronium,4"], ["hammer,1", "saw,1"], 480, Color.DimGray, [ new("Skill", 71, "Construction")], 8));
                
                toAdd.Add(new("Musical", "Windchimes", "They tinkle most pleasantly. Provides 5 comfort.", [ "plankMaple,4", "nailsAdamant,8", "barSteel,4"], ["hammer,1", "saw,1"], 660, ColorLib.Steel, [ new("Skill", 49, "Construction")], 5, acts: [ new("UnlockSong", "Rush E", "", 0) ]));
                toAdd.Add(new("Musical", "Bells", "Their tintinabulations are truly titillating. Provides 6 comfort.", [ "plankAcadia,4", "nailsRune,8", "barSteel,6"], ["hammer,1", "saw,1"], 780, ColorLib.Steel, [ new("Skill", 58, "Construction")], 6, acts: [ new("UnlockSong", "Hallelujah", "", 0) ]));
                toAdd.Add(new("Musical", "Organ", "A delightful sound. Provides 7 comfort.", [ "plankMahogany,4", "nailsOrichalcum,8", "barSteel,6"], ["hammer,1", "saw,1"], 900, ColorLib.Mahogany, [ new("Skill", 69, "Construction")], 7, acts: [ new("UnlockSong", "Free Bird", "", 0) ]));
                
                toAdd.Add(new("Statue", "Small statue", "A small statue of the god your chapel is dedicated to. Provides 5 comfort.", [ "blockLimestone,2"], ["hammer,1", "saw,1"], 20, Color.DarkGray, [ new("Skill", 49, "Construction")], 5));
                toAdd.Add(new("Statue", "Medium statue", "A medmium statue of the god your chapel is dedicated to. Provides 7 comfort.", [ "blockMarble,1"], ["hammer,1", "saw,1"], 500, Color.White, [ new("Skill", 69, "Construction")], 7));
                toAdd.Add(new("Statue", "Large statue", "A largestatue of the god your chapel is dedicated to. Provides 9 comfort.", [ "blockMarble,3"], ["hammer,1", "saw,1"], 1500, Color.White, [ new("Skill", 89, "Construction")], 9));
                
                toAdd.Add(new("Window", "Shuttered window", "A basic window. Provides 5 comfort.", [ "plankMaple,8", "nailsAdamant,16" ], ["hammer,1", "saw,1"], 1200, ColorLib.Maple, [ new("Skill", 49, "Construction")], 5));
                toAdd.Add(new("Window", "Decorative window", "What nice shapes you can make out of glass! Provides 7 comfort.", [ "glassMolten,8" ], ["hammer,1", "saw,1"], 80, Color.DarkGray, [ new("Skill", 69, "Construction")], 7));
                toAdd.Add(new("Window", "Stained glass window", "It fills the room with colored light. Provides 9 comfort.", [ "glassMolten,16" ], ["hammer,1", "saw,1"], 160, Color.MediumPurple, [ new("Skill", 89, "Construction")], 9));
                
            }

            // Portal Room
            {
                foreach (var spell in SpellLib) {
                    if (spell.Value.Category == "Tele" && spell.Value.Runes.Count > 0) {
                        string loc = spell.Value.MiscString2; 

                        List<string> runes = new(spell.Value.Runes);
                        for (int i = 0; i < runes.Count; i++) {
                            runes[i] = runes[i] + "00";
                        }

                        toAdd.Add(new("Portal", "Acadia portal (" + loc + ")", "A portal that you can use to teleport to " + loc + ".", new List<string>(runes) { "plankAcadia,3", "nailsRune,6" }, ["hammer,1", "saw,1"], 540, ColorLib.Maple, [ new("Skill", 50, "Construction")], acts: [ new("Teleport", spell.Value.MiscString, "", 0) ]));
                        toAdd.Add(new("Portal", "Mahogany portal (" + loc + ")", "A portal that you can use to teleport to " + loc + ".", new List<string>(runes) { "plankMahogany,3", "nailsOrichalcum,6" }, ["hammer,1", "saw,1"], 630, ColorLib.Maple, [ new("Skill", 65, "Construction")], acts: [ new("Teleport", spell.Value.MiscString, "", 0) ]));
                        toAdd.Add(new("Portal", "Marble portal (" + loc + ")", "A portal that you can use to teleport to " + loc + ".", new List<string>(runes) { "blockMarble,3" }, ["hammer,1", "saw,1"], 1500, ColorLib.Maple, [ new("Skill", 80, "Construction")], acts: [ new("Teleport", spell.Value.MiscString, "", 0) ]));
                    }
                }

            }

            for (int i = 0; i < toAdd.Count; i++) { 
                ObjLib.Add(toAdd[i].Name, toAdd[i]);
            }
        }
    }
}
