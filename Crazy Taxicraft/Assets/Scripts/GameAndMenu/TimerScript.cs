using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public float timer = 60;
    public float Money;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI moneyText;

    public GameObject gameover;
    public GameObject explanation;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(stopexplan());
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = timer.ToString("F1");
        moneyText.text = Money.ToString("00") + "$";

        if (timer <= 0)
        {
            Time.timeScale = 0;
            gameover.SetActive(true);
            scoreText.text = "You made" + "\n" + Money.ToString("00") + "$";
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            timer += 30;
        }
    }

    IEnumerator stopexplan()
    {
        yield return new WaitForSeconds(6);
        explanation.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void restartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    
}
