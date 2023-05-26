using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{
    [SerializeField] string Scene_Name;
    [SerializeField] GameObject PanelRetry;
    public static int i { get;  set; }

    private Enemy[] No_Of_Enemies;

    public void Home_Button(string Home)
    {
        SceneManager.LoadScene(Home);
    }
    private void Awake()
    {
        i = 0;
      
     
    }
        private void OnEnable()
    {
        No_Of_Enemies = FindObjectsOfType<Enemy>();

    }
    private void Update()
    {
        //Debug.Log(i);
        foreach (Enemy Enemies in No_Of_Enemies)
        {
            if (i == 4)
            {
                OpenPanelRetry();
            }
            if (Enemies != null )
            {
                return;
            }
         
        }
        
        SceneManager.LoadScene(Scene_Name);    
    }
    void OpenPanelRetry()
    {
        PanelRetry.SetActive(true);
    }
    public void Load_Scene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
