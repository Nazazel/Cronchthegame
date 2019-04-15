using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public AudioClip buttonSound;
    private AudioSource audio;
    public bool triggered{get;set;}

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"|| other.tag == "DeadBody")
        {
            audio.PlayOneShot(buttonSound);
            triggered = true;
            Debug.Log("SALFHL");

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(gameObject.tag != "electric"){
            if (other.tag == "Player" || other.tag == "DeadBody")
            {
                audio.PlayOneShot(buttonSound);
                triggered = false;
            }
        }
    }
}
