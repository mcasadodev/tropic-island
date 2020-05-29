using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatorUICamera : MonoBehaviour
{
    public GameObject UICamera;

    public void ActivateUICamera()
    {
        UICamera.SetActive(true);
    }

    public void DeactivateUICamera()
    {
        UICamera.SetActive(false);
    }
}
