using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingGame : MonoBehaviour
{
    public void start()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }
}
