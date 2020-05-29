using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inputs_Buttons
{
    public string name;
    public bool value;
}

[System.Serializable]
public class Inputs_Axis1D
{
    public string name;
    public float value;
}

[System.Serializable]
public class Inputs_Axis2D_XY
{
    public string name;
    public float value;
}

[System.Serializable]
public class Inputs_Axis2D
{
    public string name;
    public Vector2 value;
}

public class InputChecker : MonoBehaviour
{
    public Inputs_Buttons[] inputsButtons = new Inputs_Buttons[18];
    public Inputs_Axis1D[] inputsAxis1D = new Inputs_Axis1D[4];
    public Inputs_Axis2D_XY[] inputsAxis2DXY = new Inputs_Axis2D_XY[8];
    public Inputs_Axis2D[] inputsAxis2D = new Inputs_Axis2D[4];

    void Update()
    {
        for (int i = 0; i < inputsButtons.Length; i++)
        {
            inputsButtons[i].value = Input.GetButton(inputsButtons[i].name);
        }

        for (int i = 0; i < inputsAxis1D.Length; i++)
        {
            inputsAxis1D[i].value = Input.GetAxis(inputsAxis1D[i].name);
        }

        for (int i = 0; i < inputsAxis2DXY.Length; i++)
        {
            inputsAxis2DXY[i].value = Input.GetAxis(inputsAxis2DXY[i].name);
        }

        for (int i = 0; i < inputsAxis2D.Length; i++)
        {
            inputsAxis2D[i].value.x = inputsAxis2DXY[i].value;
            inputsAxis2D[i].value.y = inputsAxis2DXY[i + 1].value;
        }
    }
}
