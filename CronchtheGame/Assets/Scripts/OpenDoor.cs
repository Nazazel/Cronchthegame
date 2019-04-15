using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject[] Buttons;
    private bool[] allPressed;
    public AudioClip doorSound;
    private AudioSource audio;
    // Start is called before the first frame  
    void Awake()
    {
        audio = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        bool check = true;

        for (int i = 0; i < Buttons.Length; i++)
        {
            Debug.Log(Buttons[i].GetComponent<Button>().triggered);

            if(!Buttons[i].GetComponent<Button>().triggered)
            {
                check = false;

                break;
            }
        }
        if(check == true)
        {
            audio.PlayOneShot(doorSound);
            gameObject.SetActive(false);
        }
    }
}
