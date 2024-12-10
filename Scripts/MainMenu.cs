using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string DefaultButtonScene = "TestSphere";
    public string MainMenuScene = "TitleScreen";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Console.Write("Hey");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(DefaultButtonScene);
        
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(MainMenuScene);
    }

    public void OptionsMenu()
    {

    }
    public void QuitGame()
    {
        Console.WriteLine("Application closed.");   
        Application.Quit();  
    }
}
