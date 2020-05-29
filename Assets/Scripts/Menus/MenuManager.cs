using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum MenuState
{
    START = 0,
    MAIN = 1,
    LOCOMOTION = 2,
    SOUND = 3,
    SEE_CONTROLS = 4
}

public class MenuManager : MonoBehaviour
{
    MenuState menuState;
    public Animator animMenu, animMenuBg;
    public GameObject cameraUI;

    private void Update()
    {
        //if (PauseNEW.P.blockControl)
        //    return;

        if (Input.GetButtonDown("menuButton_L"))
        {
            if (!Pause.P.paused)
            {
                // PAUSE
                cameraUI.SetActive(true);
                Time.timeScale = 0;
                menuState = 0;
                animMenu.SetInteger("MenuState", 1);
                animMenu.SetTrigger("Open");
                animMenuBg.SetTrigger("Open");
                Pause.P.paused = true;
            }
            else
            {
                // UNPAUSE
                cameraUI.SetActive(false);
                Time.timeScale = 1;
                animMenu.SetInteger("MenuState", -1); // PARA QUE LA ANIMACION DE CERRAR SE REPRODUZCA
                animMenu.SetTrigger("Close");
                animMenuBg.SetTrigger("Close");
                Pause.P.paused = false;
                Pause.P.startGame = false;
            }
        }
    }

    public void StartGame()
    {
        cameraUI.SetActive(false);
        animMenu.SetInteger("MenuState", -1); // PARA QUE LA ANIMACION DE CERRAR SE REPRODUZCA
        animMenu.SetTrigger("Close");
        animMenuBg.SetTrigger("Close");
        Time.timeScale = 1;
        Pause.P.blockControl = false;
        Pause.P.paused = false;
        Pause.P.startGame = false;
    }

    public void ChangeMenu(int index)
    {
        if (Pause.P.startGame)
        {
            if (index == 1)
                index -= 1;
        }
        animMenu.SetInteger("MenuState", index);
    }


}
