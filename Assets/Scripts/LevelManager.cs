using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public UIManager uiManager;
    public GuessCrate guessCrate;

    public GameObject LetterSetCHAIR, LetterSetSHIRT, LetterSetLIGHT;

    public GameObject mapChair, mapShirt, mapLight;
    
    public void Start()
    {
        CHAIR(); // have this be called when entering level instead
    }

    public void CHAIR()
    {
        mapChair.SetActive(true); // load map
        Debug.Log("Chair called in levelmanager");
        
        LetterSetCHAIR.SetActive(true); // spawn letter set
        uiManager.UpdateRiddle("chair"); // setactive riddle UI

        if (guessCrate.guessCount == 2)
        {
            uiManager.ShowHint("chair"); // setactive hint UI
                                         // (NEW- change this to only show hint if a certain amount
                                         // of guesses has been made, and make showhint a coroutine to
                                         // show for a bit, then toggle with T key)
            Debug.Log("LevelManager: showing chair hint UI");
        }

    }
    private void SHIRT()
    {
        mapShirt.SetActive(true);
        LetterSetSHIRT.SetActive(true);
        uiManager.UpdateRiddle("shirt");
        uiManager.ShowHint("shirt");
    }
    private void LIGHT()
    {
        mapLight.SetActive(true);
        LetterSetLIGHT.SetActive(true);
        uiManager.UpdateRiddle("light");
        uiManager.ShowHint("light");
    }
}
// handle scene transitions to load the next level when the player reaches the end of the current level
// when the player opens the door load next scene (have public list to drop scene levels in inspector?)

// Level set up
// can either do so through inspector or reading text files through code
// may be easier since working with letter [refabs to do inspector