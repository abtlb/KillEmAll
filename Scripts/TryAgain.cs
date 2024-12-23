using UnityEngine;

public class TryAgain : MonoBehaviour
{
    public void Reload()
    {
        Debug.Log("reloading");
        GameObject.Find("Game Manager").GetComponent<GameManager>().ReloadScene();
    }
}
