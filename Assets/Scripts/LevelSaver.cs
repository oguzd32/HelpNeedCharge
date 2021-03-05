using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelSaver : MonoBehaviour
{
    public LevelGenerator level;

    private void Start()
    {
        SaveAsPrefab();
    }

    public void SaveAsPrefab()
    {
        for (int i = 1; i <= 10; i++)
        {
            LevelGenerator.currentLevel = i ;

            string localPath = "Assets/Prefabs/Levels/Level" + LevelGenerator.currentLevel + ".prefab";

            PrefabUtility.SaveAsPrefabAsset(level.gameObject, localPath);
        }
    }
}
