using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSnapTurn : MonoBehaviour
{
    public Teleport handTeleport;
    public bool active = true;
    public int degrees = 30;
    bool canTurn;

    void Update()
    {
        if (!active || Pause.P.blockControl)
            return;

        if (handTeleport.isPosibleTeleport)
        {
            if (Input.GetAxis("primary2DAxis_X_L") > 0.7f && Input.GetAxis("primary2DAxis_X_L") <= 1f ||
            Input.GetAxis("primary2DAxis_X_R") > 0.7f && Input.GetAxis("primary2DAxis_X_R") <= 1f)
            {
                if (canTurn)
                {
                    transform.Rotate(0, degrees, 0);
                    canTurn = false;
                }
            }
            else if (Input.GetAxis("primary2DAxis_X_L") < -0.7f && Input.GetAxis("primary2DAxis_X_L") >= -1f ||
            Input.GetAxis("primary2DAxis_X_R") < -0.7f && Input.GetAxis("primary2DAxis_X_R") >= -1f)
            {
                if (canTurn)
                {
                    transform.Rotate(0, -degrees, 0);
                    canTurn = false;
                }
            }
            else if (Input.GetAxis("primary2DAxis_X_L") == 0 && Input.GetAxis("primary2DAxis_X_R") == 0 &&
            Input.GetAxis("primary2DAxis_Y_L") == 0 && Input.GetAxis("primary2DAxis_Y_R") == 0)
            {
                if (!canTurn)
                {
                    canTurn = true;
                }
            }
        }
        else
        {

            if (Input.GetAxis("primary2DAxis_X_R") > 0.3f && Input.GetAxis("primary2DAxis_X_R") < 0.5f)
            {
                if (canTurn)
                {
                    transform.Rotate(0, degrees, 0);
                    canTurn = false;
                }
            }
            else if (Input.GetAxis("primary2DAxis_X_R") < -0.3f && Input.GetAxis("primary2DAxis_X_R") > -0.5f)
            {
                if (canTurn)
                {
                    transform.Rotate(0, -degrees, 0);
                    canTurn = false;
                }
            }
            else if (Input.GetAxis("primary2DAxis_X_R") < 0.025f && Input.GetAxis("primary2DAxis_X_R") >= 0 ||
            Input.GetAxis("primary2DAxis_X_R") > -0.025f && Input.GetAxis("primary2DAxis_X_R") <= 0)
            {
                if (!canTurn)
                {
                    canTurn = true;
                }
            }
        }

    }
}
