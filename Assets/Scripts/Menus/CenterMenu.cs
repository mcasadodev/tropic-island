using System.Collections;
using UnityEngine;

public class CenterMenu : MonoBehaviour
{
    public RectTransform _canvasRectTransform; // holds the rectangle transform of our menu
    public Camera _mainCamera;
    public float angleThreshold = 0.5f; // The amount of precision we want to center with (if 0, centering is perfect the camera needs to be too steady for too much time)
    public bool yCentering; //, zCentering;

    enum State
    {
        InScreen,
        NotInScreen,
        Moving
    };

    private State _currentState;

    void Start()
    {
        //_canvasRectTransform = GetComponentInChildren<RectTransform>();
        //_mainCamera = Camera.main;
        _currentState = State.InScreen;
    }

    void Update()
    {
        //transform.forward = Vector3.Lerp(transform.forward, GetCameraFoward(), 3 * Time.unscaledDeltaTime);

        switch (_currentState)
        {
            case State.InScreen:
                if (!IsFullyVisibleFrom(_canvasRectTransform, _mainCamera))
                {
                    // If the menu isn't fully visible anymore switch to NotInScreen state.
                    _currentState = State.NotInScreen;
                }
                break;
            case State.NotInScreen:
                // If the menu isn't in the screen anymore, start centering.
                _currentState = State.Moving;
                StartCoroutine(RotateToFrontOfPlayer());
                //StartCoroutine(MoveToFrontOfPlayer());                
                break;
        }
    }

    // Get the Vector location of the foward of our camera with some 
    // distance adjustments.
    private Vector3 GetCameraFoward()
    {
        Vector3 forward = _mainCamera.transform.forward;

        if (!yCentering)
            forward.y = 0;

        //if (zCentering)
        //    transform.rotation = _mainCamera.transform.rotation;

        return forward;
    }


    // Coroutine that will center our menu in front of our player every frame by rotating the center of the parent of the camera (which should have the same position as the camera)
    private IEnumerator RotateToFrontOfPlayer()
    {
        // While we're not directly in front of the player, slowly move the menu to the front of our player
        while (Vector3.Angle(transform.forward, GetCameraFoward()) > angleThreshold)
        {
            transform.forward = Vector3.Lerp(transform.forward, GetCameraFoward(), 3 * Time.unscaledDeltaTime);
            yield return null;
        }
        _currentState = State.InScreen; // change back to our normal state after the menu goes back to the front of the camera
    }


    // Coroutine that will center our menu in front of our player every frame by moving the canvas
    private IEnumerator MoveToFrontOfPlayer()
    {
        // While we're not directly in front of the player, slowly move the menu to the front of our player
        while (transform.position != GetCameraFoward())
        {
            float speed = 8 * Time.deltaTime; // the speed we're going to move the position of our camera
            //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, _mainCamera.transform.eulerAngles, speed); // set our angle to be the same as the one the camera is facing
            transform.eulerAngles = _mainCamera.transform.eulerAngles; // set our angle to be the same as the one the camera is facing
            transform.position = Vector3.MoveTowards(transform.position, GetCameraFoward(), speed); // move the position of our menu to our camera's foward.
            yield return null;
        }
        _currentState = State.InScreen; // change back to our normal state after the menu goes back to the front of the camera
    }

    /// <summary>
    /// Counts the bounding box corners of the given RectTransform that are visible from the given Camera in screen space.
    /// </summary>
    /// <returns> The amount of bounding box corners that are visible from the Camera or -1 if a corner isn't in the screen
    /// </returns>
    /// <param name="rectTransform">Rect transform.</param>
    /// <param name="camera">Camera.</param>
    private int CountCornersVisibleFrom(RectTransform rectTransform, Camera camera)
    {
        Rect screenBounds = new Rect(0f, 0f, Screen.width, Screen.height); // Screen space bounds (assumes camera renders across the entire screen)
        Vector3[] objectCorners = new Vector3[4];
        rectTransform.GetWorldCorners(objectCorners);

        int visibleCorners = 0;
        for (var i = 0; i < objectCorners.Length; i++) // For each corner in rectTransform
        {
            Vector3 tempScreenSpaceCorner = camera.WorldToScreenPoint(objectCorners[i]); // Transform world space position of corner to screen space
            if (screenBounds.Contains(tempScreenSpaceCorner)) // If the corner is inside the screen
            {
                visibleCorners++;
            }
            else
            {
                return -1;
            }
        }
        return visibleCorners;
    }

    /// <summary>
    /// Determines if this RectTransform is fully visible from the specified camera.
    /// Works by checking if each bounding box corner of this RectTransform is inside the cameras screen space view frustrum.
    /// </summary>
    /// <returns><c>true</c> If is fully visible from the specified camera; otherwise, <c>false</c>.</returns>
    /// <param name="rectTransform">Rect transform.</param>
    /// <param name="camera">Camera.</param>
    private bool IsFullyVisibleFrom(RectTransform rectTransform, Camera camera)
    {
        return CountCornersVisibleFrom(rectTransform, camera) >= 4; // True if all 4 corners are visible
    }
}
