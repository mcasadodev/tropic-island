using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Teleport : MonoBehaviour
{
    // INPUT LEGACY - public string hand; // cambiar a XRNode

    [HideInInspector]
    public Hand hand;

    public Teleport oppositeHand;

    // INPUT MANAGER CUSTOM - 
    // [HideInInspector]
    // public Hand hand;
    // public InputManager.ButtonOptions teleportButton;
    // public InputManager.Axis2DOptions teleportEnableAxis;

    private void Start()
    {
        // INPUT MANAGER CUSTOM - hand = GetComponentInParent<Hand>();
        hand = GetComponentInParent<Hand>();

        teleportEnabled = false;
        teleportMarker.SetActive(false);
        isPosibleTeleport = true;
    }

    public BezierCurve bezierCurve;
    public GameObject teleportMarker;
    public Animator fadeAnim;

    public bool isAimingTeleport;
    public bool isPosibleTeleport;

    public bool teleportEnabled;


    void Update()
    {
        if (Pause.P.paused)
        {
            return;
        }

        if (isPosibleTeleport)
            UpdateTeleportEnabled();

        if (teleportEnabled)
        {
            // HandleBezier();
            HandleTeleport();
        }
    }

    void UpdateTeleportEnabled()
    {
        //if (OVRInput.GetDown(teleportActivate)) {
        ToggleTeleportMode();
        //}
    }

    void HandleTeleport()
    {
        if (bezierCurve.endPointDetected)
        {
            if (bezierCurve.validTeleport)
            {
                // There is a point to teleport to
                // Display the teleport point.
                teleportMarker.SetActive(true);
                teleportMarker.transform.position = bezierCurve.EndPoint;
                //teleportMarker.transform.position = Vector3.Lerp(teleportMarker.transform.position, bezier.EndPoint, 5 * Time.deltaTime);

                // Teleport to the position                
                /* WITH AXIS */
                //if (Input.GetAxis("trigger_" + hand.hand) > 0.3f)
                //{
                //    TeleportToPosition(bezierCurve.EndPoint);
                //}

                /* WITH BUTTON */
                if (Input.GetButtonDown("triggerButton_" + hand.hand))
                    // INPUT MANAGER CUSTOM - if (hand.input.GetButtonDown(teleportButton, hand.hand))
                    TeleportToPosition(bezierCurve.EndPoint);
            }
            else
                teleportMarker.SetActive(false);
        }
        else
            teleportMarker.SetActive(false);
    }

    /* ESTE HAY QUE PROBARLO

            // Optional: use the touchpad to move the teleport point closer or further
            void HandleBezier()
            {
                //Vector2 touchCoords = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
                Vector2 touchCoords;
                touchCoords.y = 0.9f;
                if (hand == "L")
                {
                    if (Mathf.Abs(touchCoords.y) > 0.8f)
                    {
                        bezierL.ExtensionFactor = touchCoords.y > 0f ? 1f : -1f;
                    }
                    else
                    {
                        bezierL.ExtensionFactor = 0f;
                    }
                }
                else
                {
                    if (Mathf.Abs(touchCoords.y) > 0.8f)
                    {
                        bezierR.ExtensionFactor = touchCoords.y > 0f ? 1f : -1f;
                    }
                    else
                    {
                        bezierR.ExtensionFactor = 0f;
                    }
                }
            }

    */

    void ToggleTeleportMode()
    {
        if (oppositeHand.teleportEnabled)
            return;

        teleportEnabled = Input.GetAxis("primary2DAxis_Y_" + hand.hand) > 0.75f;


        // INPUT MANAGER CUSTOM - teleportEnabled = hand.input.GetAxis2D(teleportEnableAxis, hand.hand).y > 0.75f;
        // INPUT MANAGER CUSTOM - isAimingTeleport = hand.input.GetAxis2D(teleportEnableAxis, hand.hand).y > 0.75f;

        bezierCurve.ToggleDraw(teleportEnabled);

        if (!teleportEnabled)
            teleportMarker.SetActive(false);

    }

    void TeleportToPosition(Vector3 teleportPos)
    {
        StartCoroutine(FadeToTeleport(teleportPos, 0.25f));
    }

    IEnumerator FadeToTeleport(Vector3 teleportPos, float fadeTime)
    {
        Pause.P.blockControl = true;
        fadeAnim.Play("TeleportFade_FadeIn");
        yield return new WaitForSeconds(fadeTime);
        teleportMarker.SetActive(false);
        GameObject.FindWithTag("Player").transform.position = teleportPos; // + Vector3.up * 0.5f;
        fadeAnim.Play("TeleportFade_FadeOut");
        yield return new WaitForSeconds(fadeTime);
        fadeAnim.Play("TeleportFade_Blank");
        Pause.P.blockControl = false;
    }
}