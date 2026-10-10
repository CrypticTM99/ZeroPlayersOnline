using ZeroPlayersOnline.DataTypes;

namespace ZeroPlayersOnline.Hardcodes {
    public static class HardcodedCutscenes {
        public static void InitCutscenes(Dictionary<string, Cutscene> lib) {
            List<Cutscene> toAdd = new();

            toAdd.Add(new("MI_BloodPact1", "Ahead of you in the Catacombs...", new() {
                new("The inside of the catacombs are surprisingly spacious. There are two main rows of sarcophagi in the middle of the hall, and various pots of offerings and remains everywhere. Set into some of the walls are more stone coffins, and there are Saradomin banners hanging from the walls. /n /n The cultists are dragging Ilona, bound with ropes, through the catacombs while bickering amongst themselves.",
                new() {
                    "Reese: Come on, Kayle! We don't have forever!",
                    "Kayle gestures to Reese. /n /n Kayle: Look, Reese; are you sure about this? There must be some other way we can...",
                    "Reese nods firmly at Kayle. /n /n Reese: We made a blood pact, Kayle! The three of us are in this all the way.",
                    "Kayle runs his hand down his face. /n /n Kayle: Yes, but...",
                    "Caitlin clenches her fist. /n /n Caitlin: Do we have to take this idiot?",
                    "Reese nods to Caitlin. /n /n Reese: Yes! The blood pact! You read the book!",
                    "Ilona: Let me go! I didn't make any blood pact with-",
                    "Reese barely manages to restrain his anger. /n /n Reese: Shut up!",
                    "Reese: Kayle, you stay here. Guard the door.",
                    "Reese: You, come on.",
                    "The screen fades out and back in. You enter the catacombs with Xenia.",
                    "Xenia: Looks like there's a guard in the room ahead. I think we should be able to overpower him. Speak to me if you have any questions."
                })
            }, new() { new("Quest", "MI_BloodPact", "", 10) }, null, new() { new("QuestAt", 0, "MI_BloodPact") } ));

            toAdd.Add(new("MI_BloodPact2", "Approaching the first Cultist", new() {
                new("The inside of the catacombs are surprisingly spacious. There are two main rows of sarcophagi in the middle of the hall, and various pots of offerings and remains everywhere. Set into some of the walls are more stone coffins, and there are Saradomin banners hanging from the walls. /n /n The cultist Kayle, an archer, is standing guard.",
                new() {
                    "As you lead Xenia to Kayle, he fires an arrow. You swiftly duck, but the arrow injures Xenia. The two of you retreat quickly back to the entrance.",
                    "Xenia: Ah... It looks like I'm too old for this after all. You'll have to do the rest without me.",
                    "Xenia: I'll follow you, but I'll stay out of combat. Return to me if you're wounded. I have some food to share.",
                    "Xenia: The first cultist is using a ranged weapon, so you should attack him with a melee weapon. Here's my spare dagger."
                })
            }, new() { new("Quest", "MI_BloodPact", "", 20) }, new() { "daggerBronze" }, new() { new("QuestAt", 10, "MI_BloodPact") } ));

            toAdd.Add(new("MI_BloodPact3", "Choosing Kayle's Fate", new() {
                new("The inside of the catacombs are surprisingly spacious. There are two main rows of sarcophagi in the middle of the hall, and various pots of offerings and remains everywhere. Set into some of the walls are more stone coffins, and there are Saradomin banners hanging from the walls. /n /n The cultist Kayle lies dead on the floor, his chargebow dropped beside him.",
                new() {
                    "Xenia: It's a pity you had to kill that man... but I'm not questioning your judgement.",
                    "Xenia: I think the second cultist was using magic. You should use a ranged weapon to defeat magic-users. Grab that chargebow if you haven't got one. Ask me if you need any help."
                })
            }, new() { new("Quest", "MI_BloodPact", "", 40), new("Data", "KayleSpared", "set", -1) }, null, new() { new("QuestAt", 30, "MI_BloodPact") } ));

            toAdd.Add(new("MI_BloodPact3a", "Choosing Kayle's Fate", new() {
                new("The inside of the catacombs are surprisingly spacious. There are two main rows of sarcophagi in the middle of the hall, and various pots of offerings and remains everywhere. Set into some of the walls are more stone coffins, and there are Saradomin banners hanging from the walls. /n /n Needing no further prompting, Kayle stands up and runs out of the catacombs, leaving his chargebow behind.",
                new() {
                    "Xenia: I don't think that cultist will be any more trouble. I'm glad you didn't have to kill him.",
                    "Xenia: I think the second cultist was using magic. You should use a ranged weapon to defeat magic-users. Grab that chargebow if you haven't got one. Ask me if you need any help."
                })
            }, new() { new("Quest", "MI_BloodPact", "", 40), new("Data", "KayleSpared", "set", 1) }, null, new() { new("QuestAt", 30, "MI_BloodPact") } ));

            toAdd.Add(new("MI_BloodPact4", "Choosing Caitlin's Fate", new() {
                new("This side room of the catacombs is, for some reason, essentially two large balconies facing eachother with a pit across the middle of the room. There are railings on either side of the hole that leads down, and the two halves of the room are connected by a walkway with a gate. You are on the far side from the entrance. /n /n The cultist Caitlin lies dead on the floor, her staff dropped beside her.",
                new() {
                    "Xenia: I heard you killed the second cultist. It's a pity it had to come to that... but I'm not questioning your judgement.",
                    "Xenia: I think the third cultist was a swordsman. Magic is the best thing to use against melee fighters. Grab that staff and speak to me if you need any help."
                })
            }, new() { new("Quest", "MI_BloodPact", "", 60), new("Data", "CaitlinSpared", "set", -1) }, null, new() { new("QuestAt", 50, "MI_BloodPact") } ));

            toAdd.Add(new("MI_BloodPact4a", "Choosing Caitlin's Fate", new() {
                new("This side room of the catacombs is, for some reason, essentially two large balconies facing eachother with a pit across the middle of the room. There are railings on either side of the hole that leads down, and the two halves of the room are connected by a walkway with a gate. You are on the far side from the entrance. /n /n Caitlin stands and leaves the catacombs, leaving her staff on the floor.",
                new() {
                    "Xenia: The second cultist passed me on her way out. I don't think she'll be anymore trouble. I'm glad you didn't have to kill her.",
                    "Xenia: I think the third cultist was a swordsman. Magic is the best thing to use against melee fighters. Grab that staff and speak to me if you need any help."
                })
            }, new() { new("Quest", "MI_BloodPact", "", 60), new("Data", "CaitlinSpared", "set", 1) }, null, new() { new("QuestAt", 50, "MI_BloodPact") } ));

            toAdd.Add(new("MI_BloodPact5", "Confronting the Final Cultist", new() {
                new("A large room supported by pillars, with banners hanging from the walls and more urns spread around the room in piles. One large sarcophagus, the resting place of Dragith Nurn, rests at the south end of the hall. Beneath a grate in the floor, some water flows through the center of the room.",
                new() {
                    "Reese: The potion is complete. Where are they? The whole group should be present.",
                    "Ilona: Let me go, you-",
                    "Reese: Shut up!",
                    "The camera cuts to you, walking towards Reese.",
                    "Reese: Who are you? What are you doing here?",
                    "You: I'm an adventurer. Don't worry, Ilona, I'm here to rescue you.",
                    "Ilona: Thank Saradomin! He's insane! He's going to kill me!",
                    "Reese: Maybe you can take her place as the sacrifice, adventurer. Stand and fight!",
                    "You: I defeated both your lackeys. Think you can do better?",
                    "Reese: They were weak. Zamorak will turn his face from them - but he will smile on me when I offer him your blood!"
                })
            }, null, null, new() { new("QuestAt", 60, "MI_BloodPact") } ));

            toAdd.Add(new("MI_BloodPact6", "Choosing Reese's Fate", new() {
                new("A large room supported by pillars, with banners hanging from the walls and more urns spread around the room in piles. One large sarcophagus, the resting place of Dragith Nurn, rests at the south end of the hall. Beneath a grate in the floor, some water flows through the center of the room.",
                new() {
                    "With one final blow, you slay Reese where he kneels.",
                    "Seemingly triggered by some kind of magic, the catacombs shake around you.",
                    "The fake tomb of Dragith Nurn crumbles, revealing stairs to the last level of the catacombs.",
                    "Ilona: Help! Untie me so we can get out of here!"
                })
            }, new() { new("Quest", "MI_BloodPact", "", 80) }, null, new() { new("QuestAt", 70, "MI_BloodPact") } ));

             toAdd.Add(new("MI_BloodPact6a", "Choosing Reese's Fate", new() {
                new("A large room supported by pillars, with banners hanging from the walls and more urns spread around the room in piles. One large sarcophagus, the resting place of Dragith Nurn, rests at the south end of the hall. Beneath a grate in the floor, some water flows through the center of the room.",
                new() {
                    "Reese: No! There must be a death! The blood pact must be complete!",
                    "Before you can stop him, Reese pulls out a poisonous potion and drinks it, dying in moments.",
                    "Seemingly triggered by some kind of magic, the catacombs shake around you.",
                    "The fake tomb of Dragith Nurn crumbles - revealing stairs to the last level of the catacombs.",
                    "Ilona: Help! Untie me so we can get out of here!"
                })
            }, new() { new("Quest", "MI_BloodPact", "", 80) }, null, new() { new("QuestAt", 70, "MI_BloodPact") } ));

            toAdd.Add(new("MI_rjButler", "Entering the Leptoc Mansion", new() {
                new("You stand outside a large elegant mansion just outside the west gates of Varrock. The landscaping around the building is fantastic, the hedges and flowers all perfectly maintained to project an air of wealth.",
                new() {
                    "(You knock on the front door. After a moment a butler pulls the door open and looks you up and down.)",
                    "Leptoc Butler: You don't look like the cleaner. Really, showing up so late! It's important that the house be well maintained!",
                    "You: I'm... the cleaner? Yeah, here to clean.",
                    "Leptoc Butler: Preposterous. You aren't even dressed like a cleaner. If you really are, then have a tiny sliver of professionalism and go get your uniform!",
                    "Leptoc Butler: Really, showing up to clean without any uniform or supplies. What a waste of my time.",
                    "(The butler slams the door in your face. Perhaps you need a disguise to get in.)"
                })
            }, new() { new("Quest", "MI_RomeoJuliet", "", 10) }, null, null ));

            toAdd.Add(new("MI_rjNotStarted", "Entering the Leptoc Mansion", new() {
                new("You stand outside a large elegant mansion just outside the west gates of Varrock. The landscaping around the building is fantastic, the hedges and flowers all perfectly maintained to project an air of wealth.",
                new() {
                    "(You see no reason to disturb the Leptocs currently, and as such do not knock on the door.)"
                })
            }, null, null, [ new("QuestAt", -1, "MI_RomeoJuliet")] ));

            toAdd.Add(new("MI_rjSerenade", "A Moonlight Serenade", new() {
                new("You stand outside a large elegant mansion just outside the west gates of Varrock. The landscaping around the building is fantastic, the hedges and flowers all perfectly maintained to project an air of wealth. /n /n It is the middle of the night and the moon is high. Romeo is on one knee below Juliet's balcony.",
                new() {
                    "Romeo: Juliet, I call to thee!",
                    "Juliet: O Romeo, Romeo! Wherefore art thou Romeo?",
                    "Romeo: I am here, under your window my dear, which I believe has a crack in the glass.",
                    "Juliet: Oh, don't worry about that my love. I care only that you are here.",
                    "You: This may take some time..."
                })
            }, null, null, [ new("QuestAt", 50, "MI_RomeoJuliet")] ));

            toAdd.Add(new("MI_rjChapel", "A Sudden Wedding", new() {
                new("You stand outside the Varrock Chapel, dedicated to Saradomin but decorated in Varrockian colors. From inside you can hear a small ruckus from what guests Romeo has managed to gather on such short notice.",
                new() {
                    "Romeo: You came! Let us wed, my love!",
                    "(Romeo takes Juliet by the hand and begins to lead her into the Chapel.)",
                    "(As you follow, Romeo places a hand on your shoulder.)",
                    "Romeo: You need to keep lookout, my friend. Make sure nobody stops the ceremony!",
                    "(Without waiting for you to agree, Romeo leads Juliet inside and pulls the doors closed behind them.)",
                    "(You sigh.)",
                    "You: At least it's nice out.",
                    "(Time passes.)",
                    "(Romeo and Juliet, together at last, walk out of the chapel. Smiles adorn their faces and tears remain in the eyes.",
                    "Juliet: I must sneak back before my father notices anything is amiss. I shall miss you dearly my sweet betrothed.",
                    "You: Gross.",
                    "Romeo: And I you, my eternal bride.",
                    "You: Stop.",
                    "(Juliet leaves, sticking to the shadows as she goes back to her estate, never more happy than now.)",
                    "Romeo: This calls for celebration! To the ale house!",
                    "You: That's okay, I really have things to-",
                    "Romeo: I will hear nothing of it, drinks are on me!"
                })
            }, [ new("Quest", "MI_RomeoJuliet", "", 70) ], null, [ new("QuestAt", 60, "MI_RomeoJuliet")] ));

            toAdd.Add(new("MI_rjBlueMoon", "Celebrations at the Blue Moon", new() {
                new("A spacious, clean, and nicely decorated bar. There are six tables with two stools apiece placed around the room, and three more stools at the bar. A large ornate blue rug covers most of the floor in the common area. There is a fire roaring in the stone fireplace, which has a painting of a water mill hanging above it. Behind the bar is a cabinet containing a wide variety of colorful bottles of alcohol. There's a staircase up to the second floor, where the rooms are, and a door back to the kitchen. /n /n The guests who were actually allowed into the ceremony have been invited here as well and are partying.",
                new() {
                    "Romeo: Come, my friend!",
                    "Romeo: Drinks are on me like I promised!",
                    "Romeo: In fact, a round for everyone!",
                    "(The entire pub erupts in cheers at the thought of a free drink.)",
                    "(The door to the inn slams open. Tybalt Leptoc, Juliet's cousin, steps inside with a scowl on his face.)",
                    "Tybalt: Pompous as usual, eh Romeo?",
                    "Romeo: It's a time for celebration, kin!",
                    "Tybalt: I am nay your kin, and never shall be, Gontamue.",
                    "Tybalt: As such, I tire of seeing your face around Varrock. As do many of us.",
                    "Romeo: I merely wish to have a drink with a friend.",
                    "Tybalt: You should not have reason to celebrate, you- nay, your entire family has never accomplished anything worth a damn.",
                    "Romeo: You speak out of turn, sir!",
                    "Tybalt: It is you, who lives out of turn.",
                    "Romeo: Tybalt, not even you can ruin this day for me.",
                    "Romeo: Let me buy you a drink.",
                    "(Romeo rises and rakes a step toward the bar. Tybalt draws his sword and places the blade against Romeo's throat.)",
                    "Tybalt: I shall not be drinking with the likes of you, or your 'friend'.",
                    "Bartender: I'll have no fighting in here, boys!",
                    "(Tybalt lowers his sword.)",
                    "Tybalt: Romeo Gontamue, I challenge you to a duel.",
                    "Romeo: I have no quarrel with you.",
                    "Tybalt: Tomorrow.",
                    "Tybalt: Sunrise.",
                    "Tybalt: In the Square, for all to see.",
                    "Romeo: But I-",
                    "Tybalt: Come, 'kin'. I need to go get this sword sharpened.",
                    "Tybalt: On the morrow, it shall see blood.",
                    "(Tybalt leaves, and slowly the silence that had fallen over the party lifts and people return to their carousing.)",
                    "Romeo: I will not fight him.",
                    "You: Good.",
                    "Romeo: I have more important things to worry about.",
                    "You: Such as?",
                    "Romeo: Juliet.",
                    "(Romeo looks towards the door.)",
                    "Romeo: Let us enjoy tonight. Tomorrow can wait.",
                    "(You and Romaeo share a few drinks, talking of glorious battles.)",
                    "Romeo: What shall I do? I cannot allow my honor to be called in to question. At the same time, I have just married Juliet!",
                    "You: You could just run away.",
                    "Romeo: And leave my Juliet here to rot under her fathers iron-fist rule? I would never.",
                    "You: I never have a choice, do I? Leave it to me. I think I have an idea. I just need to see a man about a sword."
                })
            }, [ new("Quest", "MI_RomeoJuliet", "", 80) ], null, [ new("QuestAt", 70, "MI_RomeoJuliet")] ));

            toAdd.Add(new("MI_rjDuel", "A Duel for Romeo's Honor", new() {
                new("A small crowd has gathered in Varrock Square, their murmuring covering the normal loud sloshing of the fountain. Even the merchants have fallen quiet as the two combatants approach eachother.",
                new() {
                    "Romeo: I do not wish to do this, Tybalt.",
                    "Tybalt: Then perhaps the tales are true about house Gontamue. You are all cowards.",
                    "Romeo: ...",
                    "Romeo: Take up your sword.",
                    "(A heated battle takes place. Onlookers watch in awe at the swordsmanship between the two defenders of honor.)",
                    "(Glancing blow after glacing blow send sparks high into the sky until at last-)",
                    "(A strong swipe from Romeo shatters Tybalt's sword to pieces!)",
                    "(The strength of the Romeo's slash continues into Tybalt.)",
                    "(Tybalt, following suit with his blade, falls lifeless to the ground.)",
                    "Romeo: No...",
                    "You: It was an accident, Romeo.",
                    "Romeo: That was my brother.",
                    "You: As of recently, yes.",
                    "Romeo: My kinship.",
                    "You: We need to get you out of here, *now*.",
                    "(Palace guards begin to swarm the square as you leave through the south Varrock gate. You hear a woman scream.)",
                    "(You and Romeo run south a ways until you feel like you're no longer being followed.)",
                    "You: That could have ended better.",
                    "Romeo: What do we do now?",
                    "You: ... 'we'?",
                    "Romeo: You can't just leave me like this.",
                    "You: ...",
                    "You: I mean I *could*.",
                    "Romeo: I need a strong drink.",
                    "(Romeo begins to walk away, toward Lumbridge.)",
                    "You: Where you are going?",
                    "Romeo: Pub."
                })
            }, [ new("Quest", "MI_RomeoJuliet", "", 120), new("Teleport", "MIST_GroatsFarm", "", 1) ], null, [ new("QuestAt", 110, "MI_RomeoJuliet")] ));

            toAdd.Add(new("MI_rjFinal", "One Last Goodbye", new() {
                new("You stand outside a large elegant mansion just outside the west gates of Varrock. The landscaping around the building is fantastic, the hedges and flowers all perfectly maintained to project an air of wealth.",
                    new() {
                        "Juliet: Who goes there?",
                        "You: It's me!",
                        "Juliet: Oh, hello. What are you doing here?",
                        "You: I have a gift from Romeo.",
                        "Juliet: Oh?",
                        "You: He says you need to drink it.",
                        "Juliet: What is it?",
                        "You: A sleeping draught.",
                        "Juliet: And what exactly is the plan here?",
                        "You: You drink it, everyone thinks you're dead, then I come get you and take you to Port Sarim where you sail off with Romeo.",
                        "Juliet: ...",
                        "You: Look, it's more complicated than I would have preferred.",
                        "Juliet: And you expect me to simply trust this?",
                        "You: It was professionally made!",
                        "(Juliet raises an eyebrow.)",
                        "You: Well, I had a list and followed the directions.",
                        "Juliet: That's not reassuring.",
                        "You: Just trust me.",
                        "(You toss the potion up to Juliet.)",
                        "Juliet: Very well. Tell Romeo I shall see him soon.",
                        "You: You can tell him yourself.",
                        "(Juliet disappears inside.)",
                        "You: This had better work.",
                        "(In a short time the Leptoc Estate begins to crumble into chaos.)",
                        "Butler: JULIET!",
                        "Servant: Someone fetch the priest!",
                        "Draul: My daughter! My poor daughter, what has befallen you?",
                        "(You watch from outside as servants rush through the estate.",
                        "(You gulp nervously.)",
                        "(A few minutes later, Father Lawrence rushes to the door and enters without wait.)",
                        "Father Lawrence: Make way! Let me see the poor girl.",
                        "Father Lawrence: ...",
                        "Father Lawrence: She has passed.",
                        "Draul: My sweet Juliet...",
                        "Father Lawrence: She must be taken to the crypt beneath the chapel. There she may rest beside her ancestors.",
                        "You: That's convenient.",
                        "(You follow Father Lawrence and his cart to the chapel, where he takes Juliet down to the crypt.)",
                        "(After a moment, Father Lawrence returns upstairs to pray.)",
                        "You: Now's my chance!",
                        "(You walk down the stone staircase to the Varrock crypts.)"
                    }
                ),
                new("The crypt below the Varrock Chapel. Rows of tombs and funeral slabs for nobles line the halls, the crypt dark and dreary. The newest addition to their ranks, Juliet, lies on a white marble slab.", new() {
                    "You: Let's go, sleepyhead.",
                    "(Picking up Juliet, you sneak back upstairs and through the church doors.)",
                    "You: This doesn't look suspicious at all.",
                    "(A fairly long walk later, with your arms starting to get a little tired from carrying Juliet all this way, you arrive at the Port Sarim docks.)"
                }),
                new("The bustling docks of Port Sarim surround you. There are many ships loading and unloading cargo and passangers, bound for many other ports. Romeo appears to have been waiting here nervously for quite a while.", new() {
                    "Romeo: Juliet!",
                    "(Romeo rushes towards you, tears in his eyes once again.",
                    "Romeo: Is she-",
                    "You: Sleeping.",
                    "Romeo: Of course.",
                    "You: You owe me for this.",
                    "Romeo: I owe you my life, friend.",
                    "(Juliet begins to stir.)",
                    "Juliet: Romeo?",
                    "Romeo: My love!",
                    "(They embrace, Juliet hopping down out of your arms with nary a thanks.)",
                    "You: Right, I'll leave you two to it.",
                    "Romeo: Wait!",
                    "(Romeo turns to you.)",
                    "Romeo: Thank you. For everything.",
                    "You: Just don't come back to Varrock.",
                    "Romeo: I have no intention of doing so.", 
                    "(A ship's bell rings.)",
                    "Captain: All aboard!",
                    "Juliet: Come, Romeo.",
                    "Romeo: Farewell, my friend.",
                    "(Romeo and Juliet board the ship.)",
                    "(The ship sails away from Port Sarim.)",
                    "You: Well. At least one of us got a happy ending."
                })
            }, [ new("Quest", "MI_RomeoJuliet", "", 150), new("Teleport", "ASG_PortSarimDocks", "", 1) ], null, [ new("QuestAt", 140, "MI_RomeoJuliet"), new("Item", 1, "potionCadava", true)] ));

            for (int i = 0; i < toAdd.Count; i++) {
                lib.TryAdd(toAdd[i].ID, toAdd[i]);
            }
        }
    }
}
