using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static int currentLevel =1;

    [SerializeField] GameObject player;
    [SerializeField] GameObject lanes;
    [SerializeField] GameObject obstacle;
    [SerializeField] GameObject doubleObstacle;
    [SerializeField] GameObject battery;
    [SerializeField] GameObject finishBoard;

    [SerializeField] GameObject level;

    void Start()
    {
        SaveAsPrefab();    
    }

    void SaveAsPrefab()
    {
        for (int i = 1; i <= 10; i++)
        {
            int childs = level.transform.childCount;

            for (int j = childs - 1; j>=0 ; j--)
            {
                GameObject.DestroyImmediate(level.transform.GetChild(j).gameObject);
            }

            GenerateLanes();
            Instantiate(player, new Vector3(0, 1, 2), Quaternion.identity).transform.parent = level.transform;
            GenerateObstacle();
            GenerateDoubleObstacle();
            GenerateBattery();
            GenerateFinishBoard();

            string localPath = "Assets/Prefabs/Levels/Level" + currentLevel + ".prefab";

            //PrefabUtility.SaveAsPrefabAsset(level.gameObject, localPath);

            currentLevel++;
        }
    }

    void GenerateLanes()
    {
        GameObject _lanes = Instantiate(lanes);
        _lanes.transform.localScale = new Vector3(1, 1, 1 * 1 + 0.5f * currentLevel - 0.5f);
        _lanes.transform.position = new Vector3(0, 0, 25 * (currentLevel + 1));
        _lanes.transform.parent = level.transform;
    }

    void GenerateObstacle()
    {
        float[] xPoses = { -2.5f, 0, 2.5f };

        int obstacleAmount = 10 + 1 * currentLevel - 1;
        for (int i = 0; i < obstacleAmount; i++)
        {
            float x = xPoses[Random.Range(0, 3)];
            float z = currentLevel * 20 + i * 5 * Random.Range(1, 3);

            Instantiate(obstacle, new Vector3(x, 0.5f, z), Quaternion.identity).transform.parent = level.transform;
        }    
    }

    void GenerateDoubleObstacle()
    {
        float[] xPoses = { -2.5f, 0, 2.5f };

        int obstacleAmount = 5 + 1 * currentLevel - 1;
        for (int i = 0; i < obstacleAmount; i++)
        {
            float x = xPoses[Random.Range(0, 3)];
            float z = currentLevel * 30 + i * Random.Range(2, 3);

            Instantiate(doubleObstacle, new Vector3(x, 0.5f, 30 + z * 1.5f), Quaternion.identity).transform.parent = level.transform;
        }
    }

    void GenerateBattery()
    {
        float[] xPoses = { -2.5f, 0, 2.5f };
        int batteryAmount = 6 * currentLevel;

        for (int i = 0; i < batteryAmount; i++)
        {
            float x = xPoses[Random.Range(0, 3)];
            float z = 15 * i + Random.Range(3,7);

            Instantiate(battery, new Vector3(x, 1f, 23 + z), Quaternion.identity).transform.parent = level.transform;
        }
    }

    void GenerateFinishBoard()
    {
        Instantiate(finishBoard, new Vector3(0, 2, 50 * (currentLevel + 1)), Quaternion.identity).transform.parent = level.transform;
    }
}
