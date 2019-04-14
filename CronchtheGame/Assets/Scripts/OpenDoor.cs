using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject[] Buttons;
    private bool[] allPressed;
    // Start is called before the first frame  
    void Start()
    {
        allPressed = new bool[Buttons.Length];
       
    }

    // Update is called once per frame
    void Update()
    {
        bool check = true;

        for (int i = 0; i < Buttons.Length; i++)
        {
            if(!Buttons[i].GetComponent<Button>().triggered)
            {
                check = false;
                break;
            }
        }
        if(check == true)
        {
            gameObject.SetActive(false);
        }
    }
}
