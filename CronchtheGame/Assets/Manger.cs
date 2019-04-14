using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Manger : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Credits;

    // Start is called before the first frame update
    public void start()
    {   
        SceneManager.LoadScene("level_1");
    }
    public void quit()
    {
        Application.Quit();
    }
    public void credits()
    {
        MainMenu.SetActive(false);
        Credits.SetActive(true);
    }
}
