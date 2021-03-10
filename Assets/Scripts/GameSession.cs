using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    public static bool isGameStart;
    public static bool isGameFinish;
    public static bool youLose;

    static int levelCount = 0;

    [SerializeField] Text levelText;

    [SerializeField] GameObject startingPanel;
    [SerializeField] GameObject youLosePanel;
    [SerializeField] GameObject finishPanel;
    [SerializeField] GameObject[] levels;

    private void Start()
    {
        isGameStart = false;
        isGameFinish = false;
        youLose = false;

        levelText.text = "Level " + levelCount.ToString();

        GetLevel();
    }

    private void FixedUpdate()
    {
        if (youLose)
        {
            youLosePanel.SetActive(true);
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
        levelCount++;
        if (levelCount + 1 > levels.Length)
        {
            levelCount = 0;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GetLevel()
    {
        Instantiate(levels[levelCount]);
    }
}
