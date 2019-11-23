using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] private GameObject levelOneButton;
    [SerializeField] private GameObject levelTwoButton;
    [SerializeField] private GameObject levelThreeButton;
    [SerializeField] private GameObject levelFourButton;
    [SerializeField] private GameObject levelFiveButton;
    [SerializeField] private GameObject levelSixButton;
    [SerializeField] private GameObject levelSevenButton;

    private SaveMechanic saveManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        saveManagerScript = GameObject.Find("SaveManager").GetComponent<SaveMechanic>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the 1st level has not been completed yet
        if(saveManagerScript.levelComplete[0] == false)
        {
            // Disable button 2
            levelTwoButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            // Enable button 2
            levelTwoButton.GetComponent<Button>().interactable = true;
        }

        // If the 2nd level has not been completed yet
        if (saveManagerScript.levelComplete[1] == false)
        {
            // Disable button 3
            levelThreeButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            // Enable button 3
            levelThreeButton.GetComponent<Button>().interactable = true;
        }

        // If the 3rd level has not been completed yet
        if (saveManagerScript.levelComplete[2] == false)
        {
            // Disable button 4
            levelFourButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            // Enable button 4
            levelFourButton.GetComponent<Button>().interactable = true;
        }

        // If the 4th level has not been completed yet
        if (saveManagerScript.levelComplete[3] == false)
        {
            // Disable button 5
            levelFiveButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            // Enable button 5
            levelFiveButton.GetComponent<Button>().interactable = true;
        }

        // If the 5th level has not been completed yet
        if (saveManagerScript.levelComplete[4] == false)
        {
            // Disable button 6
            levelSixButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            // Enable button 6
            levelSixButton.GetComponent<Button>().interactable = true;
        }

        // If the 5th level has not been completed yet
        if (saveManagerScript.levelComplete[5] == false)
        {
            // Disable button 7
            levelSevenButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            // Enable button 7
            levelSevenButton.GetComponent<Button>().interactable = true;
        }
    }
}
