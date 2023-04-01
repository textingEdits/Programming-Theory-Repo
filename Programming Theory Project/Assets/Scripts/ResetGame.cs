using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class ResetGame : MonoBehaviour
{
    public static ResetGame Instance { get; private set; }
    public PlayerController playerController;

    // Update is called once per frame
    void Update()
    {
        // If the player ball is no longer detected this means that the game can restart.
        if (GameObject.Find("Player"))
            {
            return;
        }
        else if (!GameObject.Find("Player") && !playerController.isSuccessful)
        {
            StartCoroutine(Restart());
        }
        else if (!GameObject.Find("Player") && playerController.isSuccessful)
        {
            StartCoroutine(MoveOn());
        }
    }
    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private IEnumerator MoveOn()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
}
