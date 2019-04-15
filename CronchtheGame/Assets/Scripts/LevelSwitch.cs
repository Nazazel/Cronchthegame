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
    public SpriteRenderer sr;
            

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();

    }

    IEnumerator WaitForSound()
    {
        sr.enabled = false;
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
