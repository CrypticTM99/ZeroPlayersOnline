using ZeroPlayersOnline.DataTypes;
using ZeroPlayersOnline.HardcodedData;

namespace ZeroPlayersOnline.Hardcodes {
    public static class HardcodedItemsPets {
        public static void InitItems(Dictionary<string, Item> ItemLibrary) {
            List<Item> itemsToAdd = new();
            
            // Boss pets
            itemsToAdd.Add(new Item("Baby zombie plush", "Despite being a zombie, kinda cute? It even has a little chicken that it's riding on.", "petBabyZombie", 34, 140, 34, 1000, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "The baby zombie gurgles a bit.", "The zombie's chicken clucks loudly.", "The baby zombie runs in a small circle quickly.", "CHICKEN JOCKEY!"}
            });  

            itemsToAdd.Add(new Item("Beef", "Commonly referred to as Sir Loin.", "petBeef", Color.DimGray, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Beef lets out a little moo.", "Beef lets out a loud moo.", "Beef struts his stuff.", "Beef eats a bit of grass."},
                UseString = "BeefEmote", ConsumedOnUse = false
            }); 

            itemsToAdd.Add(new Item("Scurry", "Three conjoined rats, each with its own personality.", "petScurry", Color.SandyBrown, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Scurry says, 'We need-desire food-morsels.'", "Scurry says, 'Yessss, kill-slay them all.'", "Chewy says, 'Toss-throw the fur, make them regret-rue our rage-wrath!'", "Nibbles says, 'Sorry about my brothers. We don't all speak like that.'"},
                UseString = "BeefEmote", ConsumedOnUse = false
            });

            // Skill pets
            itemsToAdd.Add(new Item("Dojo Mojo", "A highly-trained ninja monkey.", "petSkillAgility", Color.DimGray, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Dojo Mojo does a sick flip.", "Dojo Mojo scratches his side idly.", "Dojo Mojo desires a banana but as a ninja must abstain.", "Dojo Mojo keeps watch for assassins."}
            });

            itemsToAdd.Add(new Item("Sifu", "A dragon-like lizard with a fu manchu and antlers. Perfectly composed.", "petSkillAttack", Color.Firebrick, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Sifu says, 'those who attack first often make mistakes'.", "Sifu says, 'to know when to strike, all senses are important, not just sight'.", "Sifu says, 'do not let orders blind you from what is right'.", "Sifu says, 'aim twice, strike once'."}
            });

            itemsToAdd.Add(new Item("Morty", "A large pink axolotl. What a heart beat.", "petSkillConstitution", Color.HotPink, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Something bumps against Morty. A few seconds later he becomes startled.", "Morty smiles, looking quite gormless.", "Morty stays still for so long you grow concerned he may have died, then chirps suddenly.", "Morty dreams of swimming in an aquarium."}
            });

            itemsToAdd.Add(new Item("Baby Yaga's House", "A tiny house on legs.", "petSkillConstruction", Color.SaddleBrown, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Some hammering comes from inside Baby Yaga's House.", "Baby Yaga's House hops around.", "Baby Yaga's House clucks and pecks at the ground with its... front door?", "Baby Yaga's House retracts its legs and pretends to be a normal tiny house."}
            });

            itemsToAdd.Add(new Item("Ramsay", "A small goblin wearing an apron and a chefs hat. I'd think twice before eating anything he cooks.", "petSkillCooking", Color.ForestGreen, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Ramsay says, 'You, idiot sandwich. What you?'.", "Ramsay says, 'Human taste simple. Get shark, cook shark, eat shark. Boring!'.", "Ramsay pretends to cook something while explaining his process. When asked, he claims to be practicing for his cooking show.", "Ramsay says, 'Maybe Ramsay make leaf stew with no water? Ramsay call it salad. But that silly, no one eat leaf'."}
            });

            itemsToAdd.Add(new Item("Wallace", "A grenwall wearing a spiked helm and holding a shield. Innocent and scared.", "petSkillDefense", Color.DimGray, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Wallace quivers in fear at enemies, imagined and real.", "Wallace's eyes bug out and he looks around frantically. When asked what's wrong, he says 'the germs! they're EVERYWHERE! can't you see them?!'", "Wallace curls into a ball with his shield on top of him for total protection.", "Wallace wishes for a nice home on the coast, far from any monsters."}
            });

            itemsToAdd.Add(new Item("Gordie", "A tiny gorajo dressed almost like a jester. Forever a naabe.", "petSkillDungeoneering", Color.Crimson, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Gordie lounges in the air while he floats along behind you.", "Gordie says, 'let's play a game!'.", "Gordie fans some cards - you aren't sure where he got them - and says, 'pick a card, any card!'.", "Gordie does a traditional Gorajo dance."}
            });

            itemsToAdd.Add(new Item("Brains", "A small scarecrow hopping around on a pole. You have to admire his balance.", "petSkillFarming", Color.Khaki, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Brains says, 'if I only had a me'.", "Brains says, 'could you give my back a scratch? my hay isn't sitting right'.", "Brains stays on the lookout for crows to scare. Any kind of bird, really, he's not picky.", "Brains wishes for a companion named Pinky."}
            });

            itemsToAdd.Add(new Item("Bernie", "A small golem made of wood, currently on fire. His name is a complete coincidence.", "petSkillFiremaking", Color.Orange, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Bernie strides forward with confidence and grace and more than a little smoke coming off him.", "Bernie remembers he is on fire and tries to pat himself out, but his efforts are in vain.", "Bernie says, 'greed can never be satisfied. knowing one's self will bring you greater happiness'.", "Bernie says, 'continue onwards until you're at the peak of your performance, then look back and realize how far you've actually come'."}
            });

            itemsToAdd.Add(new Item("Bubbles", "A rockfish contained in an enchanted ball of water that is ready to explore the world of land-walkers.", "petSkillFishing", Color.Aquamarine, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Bubbles says, 'blub blub blub'.", "Bubbles swims a lap of his bubble.", "Bubbles has an amazing idea for how to make tons of money! ...Then forgets it before he can relay the details.", "Bubbles flicks his esca on and off rapidly in morse code, but you don't catch what he was trying to say with it."}
            });

            itemsToAdd.Add(new Item("Flo", "A magical feather that floats around the world looking for adventure.", "petSkillFletching", Color.SkyBlue, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Flo says... nothing, she's a feather. She has no mouth.", "Flo sketches a beautiful drawing in the air. At least you assume it's beautiful, you can't actually see it.", "Flo writes a word in the air that you should probably not repeat in polite company.", "Flo longs for a cap to be a feather in."}
            });

            itemsToAdd.Add(new Item("Herbert", "A tortoise covered in herbs.", "petSkillHerblore", Color.ForestGreen, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Despite being a tortoise, somehow Herbert never fails to keep up with you.", "Herbert pulls a leaf off his back and eats it. Is that cannibalism?", "Herbert plods along.", "Herbert wonders if he is a tortoise that is part herb, or an herb that is part tortoise."}
            });

            itemsToAdd.Add(new Item("Ace", "A majestic hawk, and famous squadron leader who is battle-ready at a moment's notice.", "petSkillHunter", Color.Tan, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Ace startles you by screeching loudly.", "Ace makes a racket by flapping his wings rapidly.", "Ace preens his feathers with his beak to smooth them out.", "Ace rests on your shoulder for a bit." }
            });

            itemsToAdd.Add(new Item("Newton", "A newt with a lil wizard hat floating above his head, carrying a crystal ball. Defies gravity one rune at a time.", "petSkillMagic", Color.Gold, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "You notice that one of Newton's eyes has been replaced by a mind rune.", "The rune shown in Newton's crystal ball flips rapidly between Chaos, Death, and Blood. He desires combat.", "The rune shown in Newton's crystal ball is Nature. Time for some alchemy?", "Newton blabbers about how great magic is for a while, but nobody is listening."}
            });

            itemsToAdd.Add(new Item("Rocky", "A small rock golem. Don't worry, he's used to people looking at him in ore.", "petSkillMining", Color.Gray, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Rocky says, 'it's smashing time!'.", "Rocky says, 'the world... it ain't all sunshine and rainbows'.", "Rocky bobs and weaves... very slowly, because he's made out of stone.", "Rocky considers the benefits of fracking. 'It's just mining but big, so it must be good?' You quickly correct him before he gets any ideas."}
            });

            itemsToAdd.Add(new Item("Ghostly", "A small ghost carrying a censer. Poor little guy doesn't remember who he is.", "petSkillPrayer", Color.AntiqueWhite, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Ghostly says, 'ooOOoo- sorry, I was just yawning'.", "Ghostly replaces the incense in his censer and a new smell starts drifting out.", "Ghostly hovers ominously.", "Ghostly wishes he could've been a more interesting type of specter. A banshee, perhaps."}
            });

            itemsToAdd.Add(new Item("Sparky", "A squirrel with a green bycocket and a crossbow. Unfortunately his nut thievery days are sadly over due to his leg injury.", "petSkillRanged", Color.RosyBrown, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Sparky longs for a nice store of nuts for the winter.", "Sparky aims his crossbow at you and mimics firing. The threat is clear.", "Sparky says, 'take from the rich and give to the poor'. You suspect he may have gotten his hands on Marxist reading material.", "Sparky scratches his head, which is quite difficult while juggling his crutch and his crossbow."}
            });

            itemsToAdd.Add(new Item("Rue", "A dragon imbued with runic energy.", "petSkillRunecrafting", Color.Indigo, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Rue wants to one day earn a trimmed completionist cape. She says 'gotta dream big!'.", "Rue wishes she could be crafting runes right now.", "Rue says, 'did you know you can't craft cabbage runes? I wonder why?'.", "Rue says you will rue the day. She refuses to elaborate."}
            });

            itemsToAdd.Add(new Item("Crabbe", "A small stone gargoyle. A rock solid travelling companion.", "petSkillSlayer", Color.DarkGray, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "You wonder how Crabbe is managing to fly despite being made of stone.", "Crabbe says, 'could we get some rock cakes?'", "Crabbe says, 'can I have my own pet? I'd like a pet rock.'.", "Crabbe says, 'could we listen to some music? I like classical.'. Not the genre you were expecting him to be into."}
            });

            itemsToAdd.Add(new Item("Smithy", "A small metal golem with sharp protrusions sticking out of him everywhere.", "petSkillSmithing", ColorLib.Steel, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Smithy is looking forward to Christmas with his siblings Gemi and Rocky.", "Smithy works quietly on some schematics for new armor. For golems, not humans.", "Smithy says, 'you should wear more armor. you humans are too squishy to have that much skin exposed.'.", "Smithy expresses how impressed he is by the quality of armor and weapons you have made."}
            });

            itemsToAdd.Add(new Item("Kangali", "A tiny kangaroo with boxing gloves on. Ready to pounce just like a lion.", "petSkillStrength", Color.SandyBrown, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Kangali says, 'float like a butterfly, sting like a bee'.", "Kangali shadowboxes for a bit.", "Kangali questions your strength and suggests a training regimen.", "Kangali hops around like a boxer. That they're a kangaroo and can only hop is irrelevant."}
            });

            itemsToAdd.Add(new Item("Ralph", "A bipedal raccon with a swag bag thrown over one shoulder.", "petSkillThieving", Color.DimGray, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Ralph wishes to go to the Grand Exchange to sell some items he... 'found'.", "Ralph rolls around in a small trash pile.", "You catch Ralph trying to reach into your pockets.", "Ralph catalogues his loot and seems quite pleased."}
            });

            itemsToAdd.Add(new Item("Woody", "A small wooden golem with an unoriginal name.", "petSkillWoodcutting", Color.BurlyWood, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Woody tells a lie and his nose grows slightly longer.", "Woody tells a truth and his nose grows slightly shorter. You don't think it's supposed to work like that, but don't question it.", "Woody says, 'got wood?'.", "Woody says, 'what type of tree fits in your hand? A PALM tree!'."}
            });


            // Regular cats
            itemsToAdd.Add(new Item("Kitten (black)", "A friendly little pet.", "kittenBlack", Color.DimGray, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your kitten purrs.", "Your kitten meows.", "Your kitten rolls around on the ground.", "Your kitten stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            }); 

            itemsToAdd.Add(new Item("Kitten (gray)", "A friendly little pet.", "kittenGray", Color.DarkGray, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your kitten purrs.", "Your kitten meows.", "Your kitten rolls around on the ground.", "Your kitten stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            }); 

            itemsToAdd.Add(new Item("Kitten (white)", "A friendly little pet.", "kittenWhite", Color.White, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your kitten purrs.", "Your kitten meows.", "Your kitten rolls around on the ground.", "Your kitten stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            }); 

            itemsToAdd.Add(new Item("Kitten (orange)", "A friendly little pet.", "kittenOrange", Color.Orange, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your kitten purrs.", "Your kitten meows.", "Your kitten rolls around on the ground.", "Your kitten stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            }); 

            itemsToAdd.Add(new Item("Kitten (brown)", "A friendly little pet.", "kittenBrown", Color.SaddleBrown, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your kitten purrs.", "Your kitten meows.", "Your kitten rolls around on the ground.", "Your kitten stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            }); 

            itemsToAdd.Add(new Item("Kitten", "A friendly little pet.", "kittenCalico", Color.White, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your kitten purrs.", "Your kitten meows.", "Your kitten rolls around on the ground.", "Your kitten stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            }); 

            itemsToAdd.Add(new Item("Cat (black)", "A fully grown feline.", "catBlack", Color.DimGray, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your cat purrs.", "Your cat meows.", "Your cat rolls around on the ground.", "Your cat stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            }); 

            itemsToAdd.Add(new Item("Cat (gray)", "A fully grown feline.", "catGray", Color.DarkGray, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your cat purrs.", "Your cat meows.", "Your cat rolls around on the ground.", "Your cat stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            }); 

            itemsToAdd.Add(new Item("Cat (white)", "A fully grown feline.", "catWhite", Color.White, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your cat purrs.", "Your cat meows.", "Your cat rolls around on the ground.", "Your cat stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            }); 

            itemsToAdd.Add(new Item("Cat (orange)", "A fully grown feline.", "catOrange", Color.Orange, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your cat purrs.", "Your cat meows.", "Your cat rolls around on the ground.", "Your cat stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            }); 

            itemsToAdd.Add(new Item("Cat (brown)", "A fully grown feline.", "catBrown", Color.SaddleBrown, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your cat purrs.", "Your cat meows.", "Your cat rolls around on the ground.", "Your cat stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            }); 

            itemsToAdd.Add(new Item("Cat", "A fully grown feline.", "catCalico", Color.White, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your cat purrs.", "Your cat meows.", "Your cat rolls around on the ground.", "Your cat stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            });

            itemsToAdd.Add(new Item("Overgrown cat (black)", "A friendly not-so little pet.", "catBlackOvergrown", Color.DimGray, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your overgrown cat purrs.", "Your overgrown cat meows.", "Your overgrown cat rolls around on the ground.", "Your overgrown cat stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            }); 

            itemsToAdd.Add(new Item("Overgrown cat (gray)", "A friendly not-so little pet.", "catGrayOvergrown", Color.DarkGray, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your overgrown cat purrs.", "Your overgrown cat meows.", "Your overgrown cat rolls around on the ground.", "Your overgrown cat stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            }); 

            itemsToAdd.Add(new Item("Overgrown cat (white)", "A friendly not-so little pet.", "catWhiteOvergrown", Color.White, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your overgrown cat purrs.", "Your overgrown cat meows.", "Your overgrown cat rolls around on the ground.", "Your overgrown cat stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            }); 

            itemsToAdd.Add(new Item("Overgrown cat (orange)", "A friendly not-so little pet.", "catOrangeOvergrown", Color.Orange, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your overgrown cat purrs.", "Your overgrown cat meows.", "Your overgrown cat rolls around on the ground.", "Your overgrown cat stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            }); 

            itemsToAdd.Add(new Item("Overgrown cat (brown)", "A friendly not-so little pet.", "catBrownOvergrown", Color.SaddleBrown, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your overgrown cat purrs.", "Your overgrown cat meows.", "Your overgrown cat rolls around on the ground.", "Your overgrown cat stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            }); 

            itemsToAdd.Add(new Item("Overgrown cat", "A friendly not-so little pet.", "catCalicoOvergrown", Color.White, 0, trade: false) {
                EquipSlot = "Pet",  Cosmetic = true, PetBlurbs = new() { "Your overgrown cat purrs.", "Your overgrown cat meows.", "Your overgrown cat rolls around on the ground.", "Your overgrown cat stares at a point in space intently."},
                UseString = "CatMenu", ConsumedOnUse = false
            });

            for (int i = 0; i < itemsToAdd.Count; i++) {
                ItemLibrary.Add(itemsToAdd[i].ID, itemsToAdd[i]);
            }
        }
    }
}
