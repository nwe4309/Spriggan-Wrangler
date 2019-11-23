using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    [SerializeField] private GameObject mainMainMenu; //starting main menu with the options to select level, go to instruction, go to options, or exit game
    [SerializeField] private GameObject instructionsMenu; //instructions menu
    [SerializeField] private GameObject creditsMenu; //credits menu

    //assuming this method is called from a button in the main main menu, this method disables the main main menu (basically hides it) and enables the instruction menu (shows it)
    public void ToInstructions()
    {
        mainMainMenu.SetActive(false);
        instructionsMenu.SetActive(true);
    }

    //same as ToInstructions(), but takes you to credits menu
    public void ToCredits()
    {
        mainMainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    //assuming this method is called from any button in any menu that is NOT the main main menu, disable every possible menu that the player COULD have been in and enable the main main menu
    public void ToMainMenu()
    {
        //set every menu except for mainMainMenu to inactive. this is a little dumb, but it works.
        instructionsMenu.SetActive(false);
        creditsMenu.SetActive(false);

        mainMainMenu.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadLevelOne()
    {
        StaticGameInformation.LevelNameToLoad = "Level1_Ice_Tutorial";
        SceneManager.LoadScene("Game");
    }

    public void LoadLevelTwo()
    {
        StaticGameInformation.LevelNameToLoad = "Level2_Ice";
        SceneManager.LoadScene("Game");
    }

    public void LoadLevelThree()
    {
        StaticGameInformation.LevelNameToLoad = "Level3_Fire_Tutorial";
        SceneManager.LoadScene("Game");
    }

    public void LoadLevelFour()
    {
        StaticGameInformation.LevelNameToLoad = "Level4_Fire_Ice";
        SceneManager.LoadScene("Game");
    }

    public void LoadLevelFive()
    {
        StaticGameInformation.LevelNameToLoad = "Level5_Wind_Tutorial";
        SceneManager.LoadScene("Game");
    }

    public void LoadLevelSix()
    {
        StaticGameInformation.LevelNameToLoad = "Level6_Wind";
        SceneManager.LoadScene("Game");
    }

    public void LoadLevelSeven()
    {
        StaticGameInformation.LevelNameToLoad = "Level7_Final_Level";
        SceneManager.LoadScene("Game");
    }

    
}
