using ZeroPlayersOnline.DataTypes;

namespace ZeroPlayersOnline.HardcodedData {
    public static class HardcodedBooks {
        public static void InitBooks(Dictionary<string, Book> BookLib) {
            List<Book> toAdd = new();

            toAdd.Add(new("Pie recipe book", "recipesPie", 1, new() {
                new("Redberry pie", "Pour a handful of Redberries into an empty Pie Shell, bake until the berries are soft and serve warm."),
                new("Meat pie", "Line a fresh Pie Shell with Cooked Meat and heat until the pastry starts to bronze, serve with a selection of sauces."),
                new("Mud pie", "Start with a Pie Shell and add a Bucket of Compost, then pour in a Bucket of Water to keep the consistency gooey. To finish, cover with Clay and bake until a good shell forms. Serve at maximum speed with a good over-arm throw!"),
                new("Apple pie", "Take a Pie Shell and layer in Apple, cook until the juices start to bubble and leave to cool before serving."),
                new("Garden pie", "Fill a Pie Shell with Tomato, then add Onion and top with Cabbage. Bake golden brown and serve with a steak."),
                new("Fish pie", "Take one Pie Shell and fill with Trout, add a Cod for flavor, and then top with Potato for texture. Cook well until the potato turns golden and serve."),
                new("Botanical pie", "Line a Pie Shell with a Golovanova fruit. Bake until the fruit has become crispy on the top. Best served with cream and some milk."),
                new("Mushroom pie", "If you are looking for an acquired taste, fill your Pie Shell with a Sulliuscep cap and bake until the Pie Shell starts to take on the Sulliuscep's color."),
                new("Admiral pie", "For a more upperclass fish pie, fill your Pie Shell with Salmon and then add Tuna for color. Top with Potato and cook until golden."),
                new("Dragonfruit pie", "If you're looking for an exotic pie, fill your Pie Shell with a single Dragonfruit cut into chunks. Cook until the crust starts to glow."),
                new("Wild pie", "Line a Pie Shell with Raw Bear Meat, then add Raw Chompy for substance, and top with fresh Rabbit Meat. Bake until the juices start to bubble and serve."),
                new("Summer pie", "Into a Pie Shell, put Strawberry, then a layer of Watermelon, and top with Apple. Cook well and leave to cool before serving.")
            }));

            toAdd.Add(new("Scrumpled paper", "paperScrumpled", 0, new() {
                new("Delicious Ugthanki Kebab", "Ingredients: Cooked Ugthanki meat, Flour, Water, Onion, Tomato. The Ugthanki meat should be nicely grilled. "),
                new("Delicious Ugthanki Kebab", "Next take the flour and water and make some Pitta Bread. You'll need a range to do this. Take an onion and chop it into a bowl. Take a tomato and chop it into the onion mixture."),
                new("Delicious Ugthanki Kebab", "Chop the meat into the Onion and Tomato mixture. Finally fill the pitta bread with the Ugthanki, Onion, and Tomato mixture to make your delicious Ugthanki Kebab.")
            }));

            toAdd.Add(new("Shield of Arrav", "bookArrav", 1, new() {
                new("", ""),
                new("", "/n /n /n /n    The Shield of Arrav /n /n       by A. R. Wright"),
                new("Shield of Arrav", "Arrav is probably the best known hero of the 4th Age. Many legends are told of his heroics. /n /n One surviving artefact from the 4th Age is a fabulous shield. This shield is believed to have once belonged to Arrav and is now indeed known as the Shield of Arrav. /n /n For over 150 years it was the prize piece in the royal museum of Varrock."),
                new("Shield of Arrav", "However, in the year 143 of the fifth age a gang of thieves called the Phoenix Gang broke into the museum and stole the shield in a daring raid.  /n /n As a result, the current ruler, King Roald, put a 1200 gold bounty (a massive sum of money in those days) on the return of the shield, hoping that one of the culprits would betray his fellows out of greed."),
                new("Shield of Arrav", "This tactic did not work however, and the thieves who stole the shield have since gone on to become the most powerful crime gang in Varrock, despite making an enemy of the Royal Family many years ago. /n /n The reward for the return of the shield still stands.", [ new("Quest", "MI_ShieldOfArrav", "", 5) ], [ new("QuestAt", 0, "MI_ShieldOfArrav") ])
            }));

            toAdd.Add(new("Intelligence Report", "arravIntel", 0, new() {
                new("Intelligence Report", "There is an archaeologist with mining equipment hanging around the statue outside of the city. Could she be on to a hidden treasure buried near the statue? /n /n A new channel has been dug and a barge has been assembled near the digsite. The Varrock museum seems to be paying for it. Could they have information about new treasures?"),
            }));

            toAdd.Add(new("The Groats Principles", "bookGroats", 1, new() {
                new("Principle One", "Happy animals make happy farmers. It is crucial to ensure your animals have everything they could need: space, food, water. Without those essentials met you'll spend your time running around trying to make them happy and healthy. Not to mention, unhappy animals produce worse products! /n /n The Groats name lives and dies on its reputation, and that reputation relies on producing high quality milk and eggs."),
                new("Principle Two", "Never make more than you need. Greed will rot not only your soul, but your land. Your animals remain healthier when not pushed to their limits."),
                new("Principle Three", "Always squirrel money away from the tax collector. It's not a good citizen that avoids all of their taxes, but a smart one keeps a little back. The more you can invest into your farm and your animals, the more you can give back later on in your life.")
            }));

            toAdd.Add(new("Construction Guidebook", "bookConstructionGuide", 1, new() {
                new("Buying a House", "To buy a house, speak to any Estate Agent. They can typically be found in a large city in each region, such as Varrock, with one Agent per region. /n /n You can speak with an agent about purchasing a home in their region. This will cost a variable amount of gold, and the house will be located in the place they specify. You may buy any number of homes, but only have an amount equal to your construction tier active."),
                new("Material Tiers", "Materials used for construction are typically tiered, and things require materials based on the level needed to build them. /n /n  1 -  9: Pine, Bronze /n 10 - 19: Oak, Iron /n 20 - 29: Willow, Steel /n 30 - 39: Teak, Mithril /n 40 - 49: Maple, Adamant /n 50 - 59: Acadia, Rune /n 60 - 69: Mahogany, Orichalcum /n 70 - 79: Yew, Necronium /n 80 - 89: Magic, Bane /n 90 - 99: Elder, Elder Rune"),
                new("How to Build", "Take a saw, hammer, and some planks and nails to your new home. /n /n In the home, view the Minigame tab (*) on the right. You can click (BUILD) to enter or leave building mode. The Room Management option allows you to add or remove rooms, though you can only have rooms equal to your Construction tier in each house. The object menu lets you add and remove objects in the room you're standing in."),
                new("Buildable Objects", "In the Objects menu, there is a list of currently built objects and empty hotspots. /n /n With materials in hand, click an empty hotspot to view a list of things you can build there, then click what you would like to build. The ? allows you to view the requirements and materials to build each object. /n /n You will receive bonus experience for filling all hotspots."),
                new("Teleporting to Houses", "If you have at least 40 Magic and the appropriate runes, you can teleport to any owned house that is currently active and has a Teleportation Focus built in the Parlour or Portal Chamber. /n /n You may choose which houses are active by speaking to any Estate Agent."),
                new("Comfort Level", "Houses have a comfort level, typically raised by constructing objects that are purely decorative. The higher tier of materials used, the more comfort you it gives. /n /n Once you can build a bedroom, you can sleep in the bed every hour of play to become Well Rested for 30 minutes. This increases the benefit you gain from potions, giving a larger bonus if the house has high comfort."),
                new("Parlor", "A sitting room, primarily useful for raising the comfort level of the house as nearly all the buildable objects are decorative. /n /n A teleportation focus can also be built here, allowing you to teleport to the home."),
                new("Garden", "A small garden you can plant things in. /n /n You can 'build' bushes of plants such as redberry to gather them in your home, trees that can be chopped for wood, and a single farm plot to plant seeds in. The plot hotspot can alternatively contain a sand pit or clay deposit to gather those resources."),
                new("Kitchen", "No home is complete without a kitchen! /n /n Allows you to cook food, having a hotspot for ranges and sinks. /n /n The larder and shelves also give you an unlimited supply of some specific items, higher tiers providing more items."),
                new("Dining Room", "A place for guests to eat. /n /n Mostly useful to boost comfort, as all items are decorative."),
                new("Workshop", "A place for artisanry. /n /n The hotspots allow you to customize the workshop to your liking to focus on the skills you prefer. This includes an anvil and furnace, a spinning wheel, a pottery wheel and kiln, and a few more options."),
                new("Bedroom", "A place for sleeping. /n /n As mentioned previously a bed allows you to become Well Rested based on the comfort of the house, otherwise the things in this room are decorative and simply increase comfort."),
                new("Study", "A sort of magical workshop / research room. /n /n A number of the facilities here are quite useful. Telescopes allow you to locate crashed stars to mine, crystal balls can change the element of elemental staves, and lecterns allow you to create spell tablets that replace the runes for their corresponding spell."),
                new("Chapel", "A room to help you feel closer to the Gods. /n /n The main use of this room is the altar and incense burners. Bones may be offered at the altar to receive increased experience based on the altar tier. /n /n If you build incense burners, you can light them to receive 50% more experience per bone each (up to two lit)."),
                new("Portal Chamber", "A chamber with portals in it. Helpful, I know. /n /n You can build up to three portals in each Chamber, allowing you to teleport to any place a spell could teleport you without the need for runes. /n /n Each portal costs some materials for the frame, plus runes equivalent to 100 casts of the spell.")
            }));

            toAdd.Add(new("Cadava Potion Ingredients", "potionCadavaList", 0, new() {
                new("!! ONLY FOR TRUE LOVE !!", "Ingredients for death sleeping draught: /n /n Cadava Berries /n Vial of Water /n Wing of Rat /n Tail of Frog /n Ear of Snake /n Horn of Dog /n /n Use the berries on the vial, then activate the vial to mix everything else in at once."),
            }));

            toAdd.Add(new("Letter for Juliet", "letterRomeo", 0, new() {
                new("From Romeo", "My sweet dearest Juliet, /n /n     I am breathless without you. /n     Our families may feud, but my heart burns only of the passion for thee. /n     Every moment you are near me my soul is set alight. /n     Please, you must tell me how you feel by the morrow. /n     I cannot bear to live another day without knowing how you feel. /n /n Forever for you, Romeo."),
            }));

            toAdd.Add(new("Letter for Romeo", "letterJuliet", 0, new() {
                new("From Juliet", "My gentle Romeo, /n /n My heart burns there too. /n /n Meet me under my veranda in the moonlight, tonight. /n /n I will wait for thee. /n /n /n /n /n /n /n /n Your, Juliet."),
            }));

            for (int i = 0; i < toAdd.Count; i++) {
                BookLib.Add(toAdd[i].ID, toAdd[i]);
            }
        }
    }
}
