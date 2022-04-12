using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public float fadeDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImage;
    
    float m_Timer;

    void Update()
    {
        if(player.GetComponent<PlayerHealth>().isDead == true){
            EndLevel();
        }
    }

    void EndLevel()
    {
        m_Timer += Time.deltaTime;

        exitBackgroundImage.alpha = m_Timer / fadeDuration;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("BaseScene");
    }
}
