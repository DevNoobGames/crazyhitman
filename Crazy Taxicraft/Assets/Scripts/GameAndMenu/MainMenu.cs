using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }
    public void StarTTheGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quot()
    {
        Application.Quit();
    }
}
