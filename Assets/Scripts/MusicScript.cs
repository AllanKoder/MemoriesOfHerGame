using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioClip firstClip;
    public AudioClip SecondClip;

    private AudioSource AudioPlayer;
    private bool StartBool = false;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        AudioPlayer = GetComponent<AudioSource>();
        print(SceneManager.GetActiveScene().buildIndex);

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex < 10 && StartBool == false)
        {
            AudioPlayer.clip = firstClip;
            AudioPlayer.Play();
            StartBool = true;
        }
        else if (StartBool && SceneManager.GetActiveScene().buildIndex >= 10)
        {
            AudioPlayer.clip = SecondClip;
            AudioPlayer.Play();
            StartBool = false;
        }
    }
}
