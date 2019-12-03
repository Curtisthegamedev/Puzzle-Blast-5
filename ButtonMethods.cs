using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class ButtonMethods : MonoBehaviour
{
    public static bool LoadPuzzleVideo = false; 
    [SerializeField] GameObject MainMenu, BasicInfoNextButton,
        Puzzlemenu, PowerUpInfoMenu, menuDecorations, LevelTwoInstructions;

    public void CloseGame()
    {
        //closes app. 
        Application.Quit(); 
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene("JetPack"); 
    }

    public void LevelTwoInstructionsActive()
    {
        LevelTwoInstructions.SetActive(true);
        MainMenu.SetActive(false); 
    }

    public void LoadLevelOne()
    {
        //loads scene "levelOne". 
        SceneManager.LoadScene("LevelOne"); 
    }

    public void BackToMainMenu()
    {
        LoadPuzzleVideo = false; 
        MainMenu.SetActive(true);
        menuDecorations.SetActive(true);
        Slime.activateCoroutine = true; 
        Puzzlemenu.SetActive(false);
        PowerUpInfoMenu.SetActive(false); 
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("MainMenu"); 
    }

    public void PuzzleMenuStart()
    {
        LoadPuzzleVideo = true; 
        MainMenu.SetActive(false);
        Puzzlemenu.SetActive(true);
        menuDecorations.SetActive(false); 
        PowerUpInfoMenu.SetActive(false); 
    }

    public void PowerUpInfoMenuStart()
    {
        MainMenu.SetActive(false); 
        PowerUpInfoMenu.SetActive(true); 
    }
}
