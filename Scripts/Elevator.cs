using UnityEngine;

public class Elevator : MonoBehaviour
{
    GameManager gameManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        Debug.Log(GameManager.levelOptions.currentLevel);
        if(GameManager.levelOptions.currentLevel == 1)
        {
            gameManager.LoadLevel2();
        }
        else
        {
            gameManager.LoadEnd();
        }
    }
}
