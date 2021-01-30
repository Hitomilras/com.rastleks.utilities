using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RCanvasUtilities  
{

    /// <summary>
    /// Gets the position of the target mapped to screen cordinates.
    /// </summary>
    /// <param name="mainCamera">Refrence to the main camera</param>
    /// <param name="targetPosition">Target position</param>
    /// <returns></returns>
    public static Vector3 GetScreenPosition(Camera mainCamera, Vector3 targetPosition)
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(targetPosition);
        return screenPosition;
    }

    public static Vector2 WorldToPointInOverlayRectangle(Vector3 worldPoint, RectTransform rect)
    {
        Vector2 result;

        var screenPoint = GetScreenPosition(Camera.main, worldPoint);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPoint, null, out result);

        return result;
    }

    /// <summary>
    /// Gets if the screen position is within the view frustrum.
    /// </summary>
    /// <param name="screenPosition">Position of the target mapped to screen cordinates</param>
    /// <returns></returns>
    public static bool IsOnScreen(Vector3 screenPosition)
    {
        bool isTargetVisible = screenPosition.z > 0 && screenPosition.x > 0 && screenPosition.x < Screen.width && screenPosition.y > 0 && screenPosition.y < Screen.height;
        return isTargetVisible;
    }

    public static Vector3 ScreenCenter()
    {
        return new Vector3(Screen.width, Screen.height, 0) / 2;
    }

    public static Vector3 ScreenBounds(float boundsFactor)
    {
        return ScreenCenter() * boundsFactor;
    }

    public static void ScreenPositionToBounds(ref Vector3 screenPosition, ref float angle, Vector3 screenCentre, Vector3 screenBounds)
    {
        // Our screenPosition's origin is screen's bottom-left corner.
        // But we have to get the arrow's screenPosition and rotation with respect to screenCentre.
        screenPosition -= screenCentre;

        // When the targets are behind the camera their projections on the screen (WorldToScreenPoint) are inverted,
        // so just invert them.
        if (screenPosition.z < 0)
        {
            screenPosition *= -1;
        }

        // Angle between the x-axis (bottom of screen) and a vector starting at zero(bottom-left corner of screen) and terminating at screenPosition.
        angle = Mathf.Atan2(screenPosition.y, screenPosition.x);
        // Slope of the line starting from zero and terminating at screenPosition.
        float slope = Mathf.Tan(angle);

        // Two point's line's form is (y2 - y1) = m (x2 - x1) + c, 
        // starting point (x1, y1) is screen botton-left (0, 0),
        // ending point (x2, y2) is one of the screenBounds,
        // m is the slope
        // c is y intercept which will be 0, as line is passing through origin.
        // Final equation will be y = mx.
        if (screenPosition.x > 0)
        {
            // Keep the x screen position to the maximum x bounds and
            // find the y screen position using y = mx.
            screenPosition = new Vector3(screenBounds.x, screenBounds.x * slope, 0);
        }
        else
        {
            screenPosition = new Vector3(-screenBounds.x, -screenBounds.x * slope, 0);
        }
        // Incase the y ScreenPosition exceeds the y screenBounds 
        if (screenPosition.y > screenBounds.y)
        {
            // Keep the y screen position to the maximum y bounds and
            // find the x screen position using x = y/m.
            screenPosition = new Vector3(screenBounds.y / slope, screenBounds.y, 0);
        }
        else if (screenPosition.y < -screenBounds.y)
        {
            screenPosition = new Vector3(-screenBounds.y / slope, -screenBounds.y, 0);
        }
        // Bring the ScreenPosition back to its original reference.
        screenPosition += screenCentre;
    }
}
