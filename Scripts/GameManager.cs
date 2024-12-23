using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static int _enemiesLeft;
    
    public struct LevelOptions
    {
        public int currentLevel;
    }
    
    public static LevelOptions levelOptions;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            LoadLevel1();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            LoadLevel2();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }

    public void EnemyDied()
    {
        _enemiesLeft--;
        Debug.Log("Enemies left: " + _enemiesLeft);
        if (_enemiesLeft == 0)
        {
            GameObject.Find("Elevator").GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("level1");
        levelOptions.currentLevel = 1;
    }
    
    public void LoadLevel2()
    {
        SceneManager.LoadScene("level2");
        levelOptions.currentLevel = 2;
    }

    public void LoadEnd()
    {
        SceneManager.LoadScene("End");
    }

    public void ReloadScene()
    {
        if (levelOptions.currentLevel == 1)
        {
            LoadLevel1();
        }
        else
        {
            LoadLevel2();
        }
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "level1" || scene.name == "level2")
        {
            var enemyParent = GameObject.Find("Enemies");
            if (enemyParent != null)
            {
                _enemiesLeft = enemyParent.transform.childCount;
                Debug.Log($"Enemies in {scene.name}: {_enemiesLeft}");
            }
            else
            {
                
            }
        }
    }
}