using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;

public class InputManager
{
    public XRNode hand;

    public InputManager(XRNode _hand)
    {
        this.hand = _hand;
    }

    InputFeatureUsage<bool> selectedButton;
    InputFeatureUsage<float> selectedAxis;
    InputFeatureUsage<Vector2> selectedVector2;

    //public bool isPressed, hasBeenPressed, hasBeenReleased;

    public enum ButtonOptions
    {
        triggerButton,
        gripButton,
        menuButton,
        primaryButton,
        primaryTouch,
        secondaryButton,
        secondaryTouch,
        primary2DAxisClick,
        primary2DAxisTouch,
        secondary2DAxisClick,
        secondary2DAxisTouch
    };

    public enum Axis1DOptions
    {
        trigger,
        grip
    };

    public enum Axis2DOptions
    {
        primary2DAxis,
        secondary2DAxis
    };

    // DICCIONARIOS PARA QUE NO SE SOLAPEN LOS BOOLS DE LOS BOTONES
    // -- BUTTON
    public Dictionary<ButtonOptions, bool> buttonIsPressed = new Dictionary<ButtonOptions, bool>
        {
            { ButtonOptions.triggerButton, false },
            { ButtonOptions.gripButton, false },
            { ButtonOptions.menuButton, false },
            { ButtonOptions.primaryButton, false },
            { ButtonOptions.primaryTouch, false },
            { ButtonOptions.secondaryButton, false },
            { ButtonOptions.secondaryTouch, false },
            { ButtonOptions.primary2DAxisClick, false },
            { ButtonOptions.primary2DAxisTouch, false },
            { ButtonOptions.secondary2DAxisClick, false },
            { ButtonOptions.secondary2DAxisTouch, false }
        };
    // -- BUTTON DOWN
    public Dictionary<ButtonOptions, bool> buttonHasBeenPressed = new Dictionary<ButtonOptions, bool>
        {
            { ButtonOptions.triggerButton, false },
            { ButtonOptions.gripButton, false },
            { ButtonOptions.menuButton, false },
            { ButtonOptions.primaryButton, false },
            { ButtonOptions.primaryTouch, false },
            { ButtonOptions.secondaryButton, false },
            { ButtonOptions.secondaryTouch, false },
            { ButtonOptions.primary2DAxisClick, false },
            { ButtonOptions.primary2DAxisTouch, false },
            { ButtonOptions.secondary2DAxisClick, false },
            { ButtonOptions.secondary2DAxisTouch, false }
        };
    // -- BUTTON UP
    public Dictionary<ButtonOptions, bool> buttonHasBeenReleased = new Dictionary<ButtonOptions, bool>
        {
            { ButtonOptions.triggerButton, false },
            { ButtonOptions.gripButton, false },
            { ButtonOptions.menuButton, false },
            { ButtonOptions.primaryButton, false },
            { ButtonOptions.primaryTouch, false },
            { ButtonOptions.secondaryButton, false },
            { ButtonOptions.secondaryTouch, false },
            { ButtonOptions.primary2DAxisClick, false },
            { ButtonOptions.primary2DAxisTouch, false },
            { ButtonOptions.secondary2DAxisClick, false },
            { ButtonOptions.secondary2DAxisTouch, false }
        };



    bool returnValue;

    // AUX METHOD To GET BUTTON
    public InputFeatureUsage<bool> GetButtonValue(ButtonOptions buttonName)
    {
        string buttonNameString = buttonName.ToString();
        Dictionary<string, InputFeatureUsage<bool>> availableButtons = new Dictionary<string, InputFeatureUsage<bool>>
        {
            { "triggerButton", CommonUsages.triggerButton },
            { "gripButton", CommonUsages.gripButton },
            { "menuButton", CommonUsages.menuButton },
            { "primaryButton", CommonUsages.primaryButton },
            { "primaryTouch", CommonUsages.primaryTouch },
            { "secondaryButton", CommonUsages.secondaryButton },
            { "secondaryTouch", CommonUsages.secondaryTouch },
            { "primary2DAxisClick", CommonUsages.primary2DAxisClick },
            { "primary2DAxisTouch", CommonUsages.primary2DAxisTouch },
            { "secondary2DAxisClick", CommonUsages.secondary2DAxisClick },
            { "secondary2DAxisTouch", CommonUsages.secondary2DAxisTouch }
        };

        // find dictionary entry
        availableButtons.TryGetValue(buttonNameString, out selectedButton);

        return selectedButton;
    }

    // GETBUTTON
    public bool GetButton(ButtonOptions button, XRNode node)
    {
        bool pressing = false;
        if (InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(GetButtonValue(button), out pressing) && pressing)
        {
            //Debug.Log(node + " - GetButton - " + button.ToString());

            buttonIsPressed[button] = true;
            //isPressed = true;
        }
        return pressing;
    }

    // GETBUTTONDOWN
    public bool GetButtonDown(ButtonOptions button, XRNode node)
    {
        bool pressing = false;
        if (InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(GetButtonValue(button), out pressing) && pressing)
        {
            //if (!hasBeenPressed)
            if (!buttonHasBeenPressed[button])
            {
                //Debug.Log(node + " - GetButtonDown - " + button.ToString());

                buttonIsPressed[button] = true;
                //isPressed = true;

                buttonHasBeenPressed[button] = true;
                //hasBeenPressed = true;

                return true;
            }
            else
                return false;
        }

        buttonHasBeenPressed[button] = false;
        //hasBeenPressed = false;

        return false;
    }

    // GETBUTTONUP
    public bool GetButtonUp(ButtonOptions button, XRNode node)
    {
        bool pressing = false;
        if (InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(GetButtonValue(button), out pressing) && !pressing && buttonIsPressed[button]) //isPressed)
        {
            //if (!hasBeenReleased)
            if (!buttonHasBeenReleased[button])
            {
                //Debug.Log(node + " - GetButtonUp - " + button.ToString());
                returnValue = true;
                buttonHasBeenReleased[button] = true;
                //hasBeenReleased = true;

                return returnValue;
            }
            else
            {
                returnValue = false;
                buttonIsPressed[button] = false;
                //isPressed = false;

                return returnValue;
            }
        }

        buttonHasBeenReleased[button] = false;
        //hasBeenReleased = false;

        return returnValue;
    }

    // AUX METHOD To GET AXIS 1D
    public InputFeatureUsage<float> GetAxis1DValue(Axis1DOptions axisName)
    {
        string axisNameString = axisName.ToString();
        Dictionary<string, InputFeatureUsage<float>> availableAxis = new Dictionary<string, InputFeatureUsage<float>>
            {
                { "trigger", CommonUsages.trigger },
                { "grip", CommonUsages.grip }
            };

        // find dictionary entry
        availableAxis.TryGetValue(axisNameString, out selectedAxis);

        return selectedAxis;
    }

    // GETAXIS1D
    public float GetAxis1D(Axis1DOptions axis1D, XRNode node)
    {
        float value = 0;
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(GetAxis1DValue(axis1D), out value);

        //if (value > 0.01f || value < -0.01f)
        //Debug.Log(node + " is pressing the " + axis1D + " with a value of: " + value);

        return value;
    }

    // AUX METHOD To GET AXIS 2D
    public InputFeatureUsage<Vector2> GetAxis2DValue(Axis2DOptions Vector2Name)
    {
        string Vector2String = Vector2Name.ToString();
        Dictionary<string, InputFeatureUsage<Vector2>> availableVector2 = new Dictionary<string, InputFeatureUsage<Vector2>>
            {
                { "primary2DAxis", CommonUsages.primary2DAxis },
                { "secondary2DAxis", CommonUsages.secondary2DAxis }
            };

        // find dictionary entry
        availableVector2.TryGetValue(Vector2String, out selectedVector2);

        return selectedVector2;
    }

    //GETAXIS2D
    public Vector2 GetAxis2D(Axis2DOptions axis2D, XRNode node)
    {
        Vector2 value = Vector2.zero;
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(GetAxis2DValue(axis2D), out value);

        //if (value != Vector2.zero)
        //Debug.Log(node + " is moving the " + axis2D + " with a value of: " + value);

        return value;
    }
}
