using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static Pause P;

    public bool paused, blockControl, startGame;
    public GameObject cameraUI;

    void Awake()
    {
        P = this;
    }

    private void Start()
    {
        startGame = true;
        blockControl = true;
        paused = true;
        Time.timeScale = 0;
        cameraUI.SetActive(true);
    }
}
