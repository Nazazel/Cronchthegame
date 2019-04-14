using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private bool triggered{get;set;}
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"|| other.tag == "DeadBody")
        {
            triggered = true; 
        }
    }
}
