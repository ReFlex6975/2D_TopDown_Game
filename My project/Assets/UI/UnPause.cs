using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    public GameObject panel;

    public void play()
    {
        panel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
