using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Rendering.CameraUI;
using UnityEngine.UIElements;
using System.Diagnostics;
using System.Reflection;

public class WordPuzzles : MonoBehaviour
{
    // manage and recognize unique riddles/words per level

    public string secretWord { get; private set; }
    
    public int guessCount;
    
    private char[] letters = { // pull from this to get letter set (find a way so i dont need a new array for each word)
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 
        'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
        'Q','R','S','T','U','V','W','X','Y','Z'
    };
    // ref. variable list of letters in inventory - compare if its secretword
    // ref. variable riddleUI in UIManager - update w unique text
    // ref. alphabet prefabs to initialize letter set (make an array of chars?/string?)

    #region Easy Levels
    private void Chair()
    {
        // Riddle:
        //     �I am normally below you.
        //     If you remove my 1st letter, you'll find me above you.
        //     If you remove my 1st & 2nd letters, you can't see me.
        //     What am I?� 
        // secret word: CHAIR
        // Letter set: C,H,A,I,R,T,A,G,P
    }
    private void Shirt()
    {
        // Riddle:
        //     �I have a neck, but no head.
        //     I have two arms, but no hands.
        //     What am I?�  
        // secret word: SHIRT
        // Letter set: S,H,I,R,T,F,E,O,I 
    }
    private void Light()
    {
        // Riddle:
        //     �What can go through glass without breaking it?� 
        // secret word: LIGHT
        // Letter set: L,I,G,H,T,F,C,U,E,R
    }
    #endregion

    #region Normal Levels
    private void Wheat()
    {
        // Riddle:
        //     �If you take away first letter it is something you get from sun,
        //     if you remove second letter you will get something to eat,
        //     if you remove third letter you get a word you use in pointing at
        //     and if you remove the fourth letter you get something to drink. What is it?� 
        // secret word: WHEAT
        // Letter set: W,H,E,A,T,C,S,E,Y,A,F,S,Q,X,B,K
    }
    private void Cause()
    {
        // Riddle: 
        //    �When I'm lost, still some will try. 
        //    When common I'm shared by many. 
        //    I can be a contracted reason why.
        //    A rebel may not have any. 
        //    What am I ?� 
        // secret word: CAUSE
        // Letter set: C,A,U,S,E,R,I,O,A,S,G,H,E,N,B,P,S
    }
    private void Queue()
    {
        // Riddle:
        //    �What English word retains the same pronunciation,
        //    even after you take away four of its five letter?� 
        // secret word: QUEUE
        // Letter set: Q,U,E,U,E,T,G,D,E,A,F,B,M,L,E,E,T,S
    }
    #endregion

    #region Hard Levels
    private void Paper()
    {
        // Riddle:
        //     �My first came from a man Chinese 
        //    Made with devices thrown into seas

        //    Back in the day when we were schooled
        //    You would often have seen me ruled

        //    Technology wills my defeat
        //    By rendering me obsolete� 
        // secret word: PAPER
        // Letter set: all
        // Hint: after 2 wrong guesses, show
        // "The invention of _____ is traditionally attributed to Cai Lun,
        // a Chinese court official, dated to 105 A.D. He used various materials to make it,
        // including fishing nets, which are "devices thrown into seas"
        // after 4 wrong guesses, show: "Ruled _____ is _____ that has lines __ it.
        // Before the age of _______ and smartphones,
        // _____ _____ was about the only thing used to make _____ in school"
    }
    private void Clock()
    {
        // Riddle:
        //     "Three brothers share a family sport: 
        //    A non-stop marathon
        //    The oldest one is fat and short
        //    And trudges slowly on
        //    The middle brother's tall and slim 
        //    And keeps a steady pace
        //    The youngest runs just like the wind,
        //    Speeding through the race
        //    "He's young in years, we let him run,"
        //    The other brothers say
        //    "'Cause though he's surely number one, 
        //    He's second, in a way. 

        //    What could this be ?" 
        // secret word: CLOCK
        // Letter set: all
        // Hint: after 3 wrong guesses, show UI text "They are all missing a hand"
        // after 5 wrong guesses, show text UIL "365, 12, 7, 24, 60per"
    }
    #endregion
}
// have a list like the projectiles, but with each word riddle for UI display as well as the secret word/ answer
// access this through inventory for the right combination

// set parameters to recognize the word light as secretword when in the _____ level
// set up riddle to display in UI
// set up secret word _____
// read inventory (do this here?)
// update win condition (do this here?)