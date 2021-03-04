using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public static bool isGameStart;
    public static bool isGameFinish;
    public static bool youLose;

    [SerializeField] GameObject startingPanel;
    [SerializeField] GameObject youLosePanel;
    [SerializeField] GameObject finishPanel;

    private void Start()
    {
        isGameStart = false;
        isGameFinish = false;
        youLose = false;
    }

    private void FixedUpdate()
    {
        if (youLose)
        {
            youLosePanel.SetActive(true);
            Time.timeScale = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isGameStart = true;
            startingPanel.SetActive(false);
        }

        if (isGameFinish)
        {
            finishPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResetLevel()
    {
        Debug.Log("Reset level");
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Debug.Log("Next level");
        Time.timeScale = 1;
    }
}
