using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public float fadeDuration = 1f;
    public GameObject player;
    public GameObject fadeCanvas;
    public CanvasGroup exitBackgroundImage;
    public CanvasGroup winBackgroundImage;
    public int skel_count = 1;
    
    float m_Timer;

    void Start()
    {
        GameObject.Find("Game Over").SetActive(false);
        GameObject.Find("Game Win").SetActive(false);

    }
    void Update()
    {
        if(player.GetComponent<PlayerHealth>().isDead == true){

            fadeCanvas.transform.Find("Game Over").gameObject.SetActive(true);

            EndLevel();
        }
        if (player.GetComponent<Bear>().skDeathCount >= skel_count)
        {
            fadeCanvas.transform.Find("Game Win").gameObject.SetActive(true);
            PlayerWins();
        }
    }

    void EndLevel()
    {
        m_Timer += Time.deltaTime;

        exitBackgroundImage.alpha = m_Timer / fadeDuration;
    }

    void PlayerWins()
    {
        m_Timer += Time.deltaTime;

        winBackgroundImage.alpha = m_Timer / fadeDuration;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("BaseScene");
    }
}
