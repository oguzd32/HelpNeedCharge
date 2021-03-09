using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public static bool isGameStart;
    public static bool isGameFinish;
    public static bool youLose;

    static int levelCount = 0;

    [SerializeField] GameObject startingPanel;
    [SerializeField] GameObject youLosePanel;
    [SerializeField] GameObject finishPanel;
    [SerializeField] GameObject[] levels;

    private void Start()
    {
        isGameStart = false;
        isGameFinish = false;
        youLose = false;
        GetLevel();
    }

    private void FixedUpdate()
    {
        if (youLose)
        {
            youLosePanel.SetActive(true);
            //Time.timeScale = 0;
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
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GetLevel()
    {
        Instantiate(levels[levelCount]);
    }
}
