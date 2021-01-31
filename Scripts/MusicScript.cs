using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioClip firstClip;
    public AudioClip SecondClip;

    private AudioSource AudioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        AudioPlayer = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().buildIndex < 10)
        {
            AudioPlayer.clip = firstClip;
        }
        else
        {
            AudioPlayer.clip = SecondClip;

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
