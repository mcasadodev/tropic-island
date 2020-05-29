using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuSeeControls : MonoBehaviour
{
    public TMP_Text mode;
    public GameObject teleportMode, walkMode;

    public void ChangeMode()
    {
        if (mode.text == "Teleport")
        {
            teleportMode.SetActive(false);
            walkMode.SetActive(true);

            mode.text = "Walk";
        }
        else if (mode.text == "Walk")
        {
            teleportMode.SetActive(true);
            walkMode.SetActive(false);

            mode.text = "Teleport";
        }
    }
}
