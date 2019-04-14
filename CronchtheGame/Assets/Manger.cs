using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Manger : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Credits;

 
    

    public void StartButton()
    {

        SceneManager.LoadScene("level_1");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void CreditsButton()
    {
        MainMenu.SetActive(false);
        Credits.SetActive(true);
    }
}
