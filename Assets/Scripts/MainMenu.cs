using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start_Button(string Start)
    { 
        SceneManager.LoadScene(Start);
    }
    public void Quit_Game()
    {
        Application.Quit();
    }
    public void Home_Button(string Home)
    {
        SceneManager.LoadScene(Home);
    }
}
