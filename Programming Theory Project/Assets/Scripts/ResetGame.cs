using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the player ball is no longer detected this means that the game can restart.
        if (GameObject.Find("Player"))
            {
            return;
        }
        else
        {
            StartCoroutine(Restart());
        }
    }
    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
