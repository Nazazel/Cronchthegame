using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSwitch : MonoBehaviour
{
    public AudioClip victorySound;
    
    private AudioSource audio;
    private float duration;
    public string level;
            

    private void Awake()
    {
        audio = GetComponent<AudioSource>();

    }

    IEnumerator WaitForSound()
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Player")
        {
            audio.clip = victorySound;
            audio.Play();
            duration = victorySound.length;
            StartCoroutine(WaitForSound());

        }
    }

}
