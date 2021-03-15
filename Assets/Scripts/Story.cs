using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Story : MonoBehaviour
{
    public float Duration = 10f;
    public float TextTypingSpeed = 0.05f;

    public Text StoryText;

    public string Sentence;
    public bool Final;
    public bool StartScene;

    private bool SpawnOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TypeSentence(Sentence));
        if (StartScene)
        {
            GameObject[] DataList = GameObject.FindGameObjectsWithTag("ItemData");
            for (int i = 0; i < DataList.Length; i++)
            {
                Destroy(DataList[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Duration -= Time.deltaTime;
        if (Duration < 0 && !SpawnOnce)
        {
            LoadScene();
            SpawnOnce = true;
        }
    }

    void LoadScene()
    {
        if (!Final)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        StoryText.text = "";
        foreach (char letter in Sentence)
        {
            StoryText.text += letter;
            yield return new WaitForSeconds(TextTypingSpeed);
        }
    }

    public void Continue()
    {
        SceneManager.LoadScene("Intro");
        PlayerController.DamageToEnemy *= 3.5f;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Intro");


        FindObjectOfType<ItemData>().ClearData();

        PlayerController.HealthMultipier = 1f;
        PlayerController.MovementSpeedMultipier = 1f;
        PlayerController.AmmoMultipler = 1f;
        PlayerController.ChanceMultipler = 2f;
        PlayerController.HealthRegenRate = 1f;
        PlayerController.CoinMultipler = 1f;
        PlayerController.DamageToEnemy = 1f;
        PlayerController.EnemyPower = 0.8f;
        PlayerController.BallMultipler = 0f;
        PlayerController.Music = 0f;
    }
    public void End()
    {
        Destroy(GameObject.FindGameObjectWithTag("Music"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerController.HealthMultipier = 1f;
        PlayerController.MovementSpeedMultipier = 1f;
        PlayerController.AmmoMultipler = 1f;
        PlayerController.ChanceMultipler = 2f;
        PlayerController.HealthRegenRate = 1f;
        PlayerController.CoinMultipler = 1f;
        PlayerController.DamageToEnemy = 1f;
        PlayerController.EnemyPower = 0.8f;
        PlayerController.BallMultipler = 0f;
        PlayerController.Music = 0f;

        //does not exist, fix this 
    }
}
