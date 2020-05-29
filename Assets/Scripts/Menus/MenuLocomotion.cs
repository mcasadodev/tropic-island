using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuLocomotion : MonoBehaviour
{
    public Teleport handLTeleport, handRTeleport;

    public GameObject player, collisionWall;
    public GameObject checkTeleport, checkWalk, degreesContainer;
    public TMP_Text degreesText;

    public void ToggleTeleport()
    {
        checkTeleport.SetActive(true);
        checkWalk.SetActive(false);

        collisionWall.SetActive(false);

        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;
        handLTeleport.isPosibleTeleport = true;
        handRTeleport.isPosibleTeleport = true;
    }

    public void ToggleWalk()
    {
        checkTeleport.SetActive(false);
        checkWalk.SetActive(true);

        collisionWall.SetActive(true);

        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        handLTeleport.isPosibleTeleport = false;
        handRTeleport.isPosibleTeleport = false;
    }

    public void ToggleSnapTurn(bool active)
    {
        player.GetComponent<PlayerSnapTurn>().enabled = active;
        degreesContainer.SetActive(active);
    }

    public void DecreaseDegrees() // NOTA: EL ORDEN DE LOS if IMPORTA (NO SÉ POR QUÉ)
    {
        if (player.GetComponent<PlayerSnapTurn>().degrees == 45)
        {
            degreesText.text = "30º";
            player.GetComponent<PlayerSnapTurn>().degrees = 30;
        }

        if (player.GetComponent<PlayerSnapTurn>().degrees == 90)
        {
            degreesText.text = "45º";
            player.GetComponent<PlayerSnapTurn>().degrees = 45;
        }
    }

    public void IncreaseDegrees() // NOTA: EL ORDEN DE LOS if IMPORTA (NO SÉ POR QUÉ)
    {
        if (player.GetComponent<PlayerSnapTurn>().degrees == 45)
        {
            degreesText.text = "90º";
            player.GetComponent<PlayerSnapTurn>().degrees = 90;
        }

        if (player.GetComponent<PlayerSnapTurn>().degrees == 30)
        {
            degreesText.text = "45º";
            player.GetComponent<PlayerSnapTurn>().degrees = 45;
        }
    }
}
