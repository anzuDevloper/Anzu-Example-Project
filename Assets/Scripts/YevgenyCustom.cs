using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Video;
using System.Reflection;



/// <summary>
/// Some useful stuff by Yevgeny Blinov
/// </summary>
public static class YevgenyShortcuts
{
    #region Transform

    #region Transform - Is In Range Of

    /// <summary>
    /// Returns whether a Transform is in a specified range of a position in local space. You can choose what axises will be included in the calculation. By default, all axises are included
    /// </summary>
    public static bool IsInRangeOfLocalPosition(this Transform transform, Vector3 Position, float Range)
    {
        Vector3Bool isInRange = Vector3Bool.AllTrue;

        range = new ClampedFloat(Position.x - Range, Position.x + Range).Verified;
        isInRange.X = range.ContainsValue(transform.localPosition.x);

        range = new ClampedFloat(Position.y - Range, Position.y + Range).Verified;
        isInRange.Y = range.ContainsValue(transform.localPosition.y);

        range = new ClampedFloat(Position.z - Range, Position.z + Range).Verified;
        isInRange.Z = range.ContainsValue(transform.localPosition.z);

        return isInRange.IsAllTrue;
    }



    /// <summary>
    /// Returns whether a Transform is in a specified range of a position in world space. You can choose what axises will be included in the calculation. By default, all axises are included
    /// </summary>
    public static bool IsInRangeOfWorldPosition(this Transform transform, Vector3 Position, float Range)
    {
        Vector3Bool isInRange = Vector3Bool.AllTrue;

        range = new ClampedFloat(Position.x - Range, Position.x + Range).Verified;
        isInRange.X = range.ContainsValue(transform.position.x);

        range = new ClampedFloat(Position.y - Range, Position.y + Range).Verified;
        isInRange.Y = range.ContainsValue(transform.position.y);

        range = new ClampedFloat(Position.z - Range, Position.z + Range).Verified;
        isInRange.Z = range.ContainsValue(transform.position.z);

        return isInRange.IsAllTrue;
    }



    /// <summary>
    /// Returns whether a Transform is in a specified range of a position in local space. You can choose what axises will be included in the calculation. By default, all axises are included
    /// </summary>
    public static bool IsInRangeOfLocalPosition(this Transform transform, Vector3 Position, float Range, Vector3Bool includeAxes)
    {
        Vector3Bool isInRange = Vector3Bool.AllTrue;

        if (includeAxes.X)
        {
            range = new ClampedFloat(Position.x - Range, Position.x + Range).Verified;
            isInRange.X = range.ContainsValue(transform.localPosition.x);
        }
        if (includeAxes.Y)
        {
            range = new ClampedFloat(Position.y - Range, Position.y + Range).Verified;
            isInRange.Y = range.ContainsValue(transform.localPosition.y);
        }
        if (includeAxes.Z)
        {
            range = new ClampedFloat(Position.z - Range, Position.z + Range).Verified;
            isInRange.Z = range.ContainsValue(transform.localPosition.z);
        }

        return isInRange.IsAllTrue;
    }



    /// <summary>
    /// Returns whether a Transform is in a specified range of a position in world space. You can choose what axises will be included in the calculation. By default, all axises are included
    /// </summary>
    public static bool IsInRangeOfWorldPosition(this Transform transform, Vector3 Position, float Range, Vector3Bool includeAxes)
    {
        Vector3Bool isInRange = Vector3Bool.AllTrue;

        if (includeAxes.X)
        {
            range = new ClampedFloat(Position.x - Range, Position.x + Range).Verified;
            isInRange.X = range.ContainsValue(transform.position.x);
        }
        if (includeAxes.Y)
        {
            range = new ClampedFloat(Position.y - Range, Position.y + Range).Verified;
            isInRange.Y = range.ContainsValue(transform.position.y);
        }
        if (includeAxes.Z)
        {
            range = new ClampedFloat(Position.z - Range, Position.z + Range).Verified;
            isInRange.Z = range.ContainsValue(transform.position.z);
        }

        return isInRange.IsAllTrue;
    }

    #endregion

    #region Transform - Other

    static ClampedFloat range;

    /// <summary>
    /// Resets the local values of the transform to defaults
    /// </summary>
    public static void ResetInLocalSpace(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        transform.localScale = Vector3.one;
    }



    /// <summary>
    /// Resets the global values of the transform to defaults
    /// </summary>
    public static void ResetInWorldSpace(this Transform transform)
    {
        transform.position = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        transform.localScale = Vector3.one;
    }



    /// <summary>
    /// Sorts the list of target transforms in such a way that they will be referenced according to the looking angle from the given position
    /// </summary>
    public static List<Transform> SortTargetsByAngleAtPosition(this List<Transform> targets, Vector3 position)
    {
        RefTransform.position = position;
        RefTransform.eulerAngles = Vector3.zero;
        List<float> Angles = new List<float>();
        int count = targets.Count;

        for (int i = 0; i < count; i++)
        {
            refTransform.LookAt(targets[i]);
            Angles.Add(refTransform.eulerAngles.y);
        }

        List<Transform> newTargets = new List<Transform>();
        float minAngle = 360;
        int minAngleIndex = -1;

        for (int i = 0; i < count; i++)
        {
            minAngle = 360;
            minAngleIndex = -1;

            for (int j = 0; j < count; j++)
            {
                if (newTargets.Contains(targets[j]))
                    continue;

                if (Angles[j] < minAngle)
                {
                    minAngle = Angles[j];
                    minAngleIndex = j;
                }
            }

            newTargets.Add(targets[minAngleIndex]);
        }

        targets.Clear();
        targets.AddRange(newTargets);
        return targets;
    }

    #endregion

    #endregion

    #region Rect Transform

    /// <summary>
    /// Resets the local values of the RectTransform to defaults
    /// </summary>
    public static void Reset(this RectTransform rectTransform)
    {
        rectTransform.anchoredPosition3D = Vector3.zero;
        rectTransform.localEulerAngles = Vector3.zero;
        rectTransform.localScale = Vector3.one;
        rectTransform.sizeDelta = RectTransformation.DefaultSizeDelta;
        rectTransform.anchorMin = RectTransformation.DefaultAnchorMin;
        rectTransform.anchorMax = RectTransformation.DefaultAnchorMax;
        rectTransform.pivot = RectTransformation.DefaultPivot;
    }


    /// <summary>
    /// Counts the bounding box corners of the given RectTransform that are visible from the given Camera in screen space.
    /// </summary>
    private static int CountCornersVisibleFrom(this RectTransform rectTransform, Camera camera)
    {
        Rect screenBounds = new Rect(0f, 0f, Screen.width, Screen.height); // Screen space bounds (assumes camera renders across the entire screen)
        Vector3[] objectCorners = new Vector3[4];
        rectTransform.GetWorldCorners(objectCorners);

        int visibleCorners = 0;
        Vector3 tempScreenSpaceCorner; // Cached
        for (var i = 0; i < objectCorners.Length; i++) // For each corner in rectTransform
        {
            tempScreenSpaceCorner = camera.WorldToScreenPoint(objectCorners[i]); // Transform world space position of corner to screen space
            if (screenBounds.Contains(tempScreenSpaceCorner)) // If the corner is inside the screen
            {
                visibleCorners++;
            }
        }
        return visibleCorners;
    }


    /// <summary>
    /// Determines if this RectTransform is fully visible from the specified camera.
    /// Works by checking if each bounding box corner of this RectTransform is inside the cameras screen space view frustrum.
    /// </summary>
    public static bool IsFullyVisibleFrom(this RectTransform rectTransform, Camera camera)
    {
        return CountCornersVisibleFrom(rectTransform, camera) == 4; // True if all 4 corners are visible
    }


    /// <summary>
    /// Determines if this RectTransform is at least partially visible from the specified camera.
    /// Works by checking if any bounding box corner of this RectTransform is inside the cameras screen space view frustrum.
    /// </summary>
    public static bool IsVisibleFrom(this RectTransform rectTransform, Camera camera)
    {
        return CountCornersVisibleFrom(rectTransform, camera) > 0; // True if any corners are visible
    }


    /// <summary>
    /// Determines if this RectTransform is at least partially visible from the specified camera.
    /// Works by checking if any bounding box corner of this RectTransform is inside the cameras screen space view frustrum.
    /// </summary>
    public static bool IsVisibleAtMostFrom(this RectTransform rectTransform, Camera camera)
    {
        Rect screenBounds = new Rect(0f, 0f, Screen.width, Screen.height); // Screen space bounds (assumes camera renders across the entire screen)
        Vector3 rectTransformPosition = camera.WorldToScreenPoint(rectTransform.anchoredPosition);
        rectTransformPosition.z = 0;
        return screenBounds.Contains(rectTransformPosition);
    }

    #endregion

    #region Direction

    #region Direction - Direction To Point

    /// <summary>
    /// Get the directional vector from the current position to the target position
    /// </summary>
    public static Vector3 DirectionToPoint(this Vector3 origin, Vector3 target)
    {
        return (target - origin) / (target - origin).magnitude;
    }



    /// <summary>
    /// Get the directional vector from the current position to the target position
    /// </summary>
    public static Vector3 DirectionToPoint(this Transform origin, Transform target)
    {
        return (target.position - origin.position) / (target.position - origin.position).magnitude;
    }



    /// <summary>
    /// Get the directional vector from the current position to the target position
    /// </summary>
    public static Vector3 DirectionToPoint(this Vector3 origin, Transform target)
    {
        return (target.position - origin) / (target.position - origin).magnitude;
    }



    /// <summary>
    /// Get the directional vector from the current position to the target position
    /// </summary>
    public static Vector3 DirectionToPoint(this Transform origin, Vector3 target)
    {
        return (target - origin.position) / (target - origin.position).magnitude;
    }

    #endregion

    #region Direction - Facing Camera

    /// <summary>
    /// Return the direction from the origin point towards the MainCamera. If no camera is specified, will face the MainCamera
    /// </summary>
    public static Vector3 FacingCamera(this Vector3 origin, Camera camera = null)
    {
        if (camera == null)
        {
            camera = Camera.main;
        }

        return origin.DirectionToPoint(camera.transform);
    }


    /// <summary>
    /// Return the direction from the origin point towards the MainCamera. If no camera is specified, will face the MainCamera
    /// </summary>
    public static Vector3 FacingCamera(this Transform origin, Camera camera = null)
    {
        if (camera == null)
        {
            camera = Camera.main;
        }

        return origin.DirectionToPoint(camera.transform);
    }


    /// <summary>
    /// Return the direction from the origin point towards the MainCamera. If no camera is specified, will face the MainCamera
    /// </summary>
    public static Vector3 FacingCamera(this Vector3 origin, Transform camera = null)
    {
        if (camera == null)
        {
            camera = Camera.main.transform;
        }

        return origin.DirectionToPoint(camera);
    }


    /// <summary>
    /// Return the direction from the origin point towards the MainCamera. If no camera is specified, will face the MainCamera
    /// </summary>
    public static Vector3 FacingCamera(this Transform origin, Transform camera = null)
    {
        if (camera == null)
        {
            camera = Camera.main.transform;
        }

        return origin.DirectionToPoint(camera);
    }

    #endregion

    #region Direction - Accuracy

    /// <summary>
    /// Adds a deviation to the raycast hit point according to the specified accuracy value, which should be between 0 and 1. Useful in shooting games if you want to add a deviation
    /// </summary>
    public static Vector3 ApplyAccuracy(this Vector3 direction, float accuracy)
    {
        accuracy = accuracy.Clamped01();
        direction.x = direction.x + Random.Range(accuracy - 1, 1 - accuracy);
        direction.y = direction.y + Random.Range(accuracy - 1, 1 - accuracy);
        direction.z = direction.z + Random.Range(accuracy - 1, 1 - accuracy);
        return direction;
    }


    /// <summary>
    /// Adds a deviation to the raycast hit point according to the specified accuracy value, which should be between 0 and 1. Useful in shooting games if you want to add a deviation
    /// </summary>
    public static Vector2 ApplyAccuracy(this Vector2 direction, float accuracy)
    {
        accuracy = accuracy.Clamped01();
        direction.x = direction.x + Random.Range(accuracy - 1, 1 - accuracy);
        direction.y = direction.y + Random.Range(accuracy - 1, 1 - accuracy);
        return direction;
    }

    #endregion

    #endregion

    #region Relative Direction

    #region Relative Direction - Offset In Direction

    /// <summary>
    /// Returns a position of a Vector3 with the given offset in a specified direction, without changing the original vector itself <para></para>
    /// Send Vector3.directions for world space offset, and Transform.directions for local space offset
    /// </summary>
    public static Vector3 OffsetInDirection(this Vector3 origin, Vector3 direction, float offset)
    {
        return origin + direction.normalized * offset;
    }


    /// <summary>
    /// Returns a position of a Transform with the given offset in a specified direction, without changing the position of the transform itself <para></para>
    /// Send Vector3.directions for world space offset, and Transform.directions for local space offset
    /// </summary>
    public static Vector3 OffsetInDirection(this Transform transform, Vector3 direction, float offset)
    {
        return transform.position + direction.normalized * offset;
    }

    #endregion

    #region Relative Direction - Local Offset

    /// <summary>
    /// Returns the position left to the transform in a specified offset in local space
    /// </summary>
    public static Vector3 LocalOffsetLeft(this Transform transform, float offset)
    {
        return transform.position - transform.right * offset;
    }


    /// <summary>
    /// Returns the position left to the transform in a specified offset in local space
    /// </summary>
    public static Vector3 LocalOffsetRight(this Transform transform, float offset)
    {
        return transform.position + transform.right * offset;
    }


    /// <summary>
    /// Returns the position up to the transform in a specified offset in local space
    /// </summary>
    public static Vector3 LocalOffsetUp(this Transform transform, float offset)
    {
        return transform.position + transform.up * offset;
    }


    /// <summary>
    /// Returns the position down to the transform in a specified offset in local space
    /// </summary>
    public static Vector3 LocalOffsetDown(this Transform transform, float offset)
    {
        return transform.position - transform.up * offset;
    }


    /// <summary>
    /// Returns the position up to the transform in a specified offset in local space
    /// </summary>
    public static Vector3 LocalOffsetForward(this Transform transform, float offset)
    {
        return transform.position + transform.forward * offset;
    }


    /// <summary>
    /// Returns the position down to the transform in a specified offset in local space
    /// </summary>
    public static Vector3 LocalOffsetBack(this Transform transform, float offset)
    {
        return transform.position - transform.forward * offset;
    }

    #endregion

    #region Relative Direction - General

    /// <summary>
    /// Where is the target relatively to the origin? In front? Behind? To the Left? To the Right? Above? Below?
    /// </summary>
    //public static RelativeDirection FindRelativeDirection(this Transform target, Transform origin, float heightThreshold = 0)
    //{
    //    if (heightThreshold != 0)
    //    {
    //        heightThreshold = Mathf.Abs(heightThreshold);

    //        if (target.position.y > origin.position.y + heightThreshold)
    //        {
    //            return RelativeDirection.Above;
    //        }
    //        else if (target.position.y < origin.position.y - heightThreshold)
    //        {
    //            return RelativeDirection.Below;
    //        }
    //    }

    //    Vector3 frontPoint = origin.position + origin.forward * 10;
    //    float angleToFrontPoint = origin.AngleTo(frontPoint);
    //    float angleOffset = angleToFrontPoint;
    //    angleToFrontPoint = 0;
    //    float angleToTarget = origin.AngleTo(target) - angleOffset;

    //    if ((angleToTarget >= 315) || (angleToTarget <= 45))
    //    {
    //        return RelativeDirection.InFront;
    //    }
    //    else if ((angleToTarget > 45) && (angleToTarget < 135))
    //    {
    //        return RelativeDirection.ToTheRight;
    //    }
    //    else if ((angleToTarget >= 135) && (angleToTarget <= 225))
    //    {
    //        return RelativeDirection.Behind;
    //    }
    //    else if ((angleToTarget > 225) && (angleToTarget < 315))
    //    {
    //        return RelativeDirection.ToTheRight;
    //    }

    //    return RelativeDirection.InFront;
    //}

    #endregion

    #endregion

    #region Angles
    
    ///// <summary>
    ///// Return an angle from point to point using the 360 degrees scale 
    ///// </summary>
    //public static float AngleTo(this Transform origin, Transform target, float offset = 105)
    //{
    //    return Vector3.SignedAngle(origin.position, target.position, Vector3.up) + offset;
    //}


    ///// <summary>
    ///// Return an angle from point to point using the 360 degrees scale 
    ///// </summary>
    //public static float AngleTo(this Transform origin, Vector3 target, float offset = 105)
    //{
    //    return Vector3.SignedAngle(target, origin.position, Vector3.up) + offset;
    //}


    ///// <summary>
    ///// Return an angle from point to point using the 360 degrees scale 
    ///// </summary>
    //public static float AngleTo(this Vector3 origin, Transform target, float offset = 105)
    //{
    //    return Vector3.SignedAngle(target.position, origin, Vector3.up) + offset;
    //}


    ///// <summary>
    ///// Return an angle from point to point using the 360 degrees scale 
    ///// </summary>
    //public static float AngleTo(this Vector3 origin, Vector3 target, float offset = 105)
    //{
    //    return Vector3.SignedAngle(target, origin, Vector3.up) + offset;
    //}

    #endregion

    #region Colors

    #region Colors - Adjustments

    static Color color;

    /// <summary>
    /// Returns the specified color multiplied by the given exposure level, excluding Alpha channel
    /// </summary>
    public static Color AdjustExposureLevel(this Color color, float exposureLevel)
    {
        exposureLevel = exposureLevel.ClampMin(0);
        color.r = color.r * exposureLevel;
        color.g = color.g * exposureLevel;
        color.b = color.b * exposureLevel;
        return color;
    }


    /// <summary>
    /// Returns the specified color multiplied by the given exposure level, including Alpha channel
    /// </summary>
    public static Color AdjustFillLevel(this Color color, float exposureLevel)
    {
        exposureLevel = exposureLevel.ClampMin(0);
        color.r = color.r * exposureLevel;
        color.g = color.g * exposureLevel;
        color.b = color.b * exposureLevel;
        color.a = color.a * exposureLevel;
        return color;
    }


    /// <summary>
    /// Returns the color with the specified alpha
    /// </summary>
    public static Color Opacity(this Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

    #endregion

    #region Colors - Sprite Renderer

    public static void SetColorIgnoringAlpha(this SpriteRenderer spriteRenderer, Color newColor)
    {
        color = spriteRenderer.color;
        color.r = newColor.r;
        color.g = newColor.g;
        color.b = newColor.b;
        spriteRenderer.color = color;
    }


    public static void SetRed(this SpriteRenderer spriteRenderer, float red)
    {
        color = spriteRenderer.color;
        color.r = red;
        spriteRenderer.color = color;
    }
    


    public static void SetGreen(this SpriteRenderer spriteRenderer, float green)
    {
        color = spriteRenderer.color;
        color.g = green;
        spriteRenderer.color = color;
    }



    public static void SetBlue(this SpriteRenderer spriteRenderer, float blue)
    {
        color = spriteRenderer.color;
        color.b = blue;
        spriteRenderer.color = color;
    }



    public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha)
    {
        color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    #endregion

    #region Colors - Image

    public static void SetColorIgnoringAlpha(this Image spriteRenderer, Color newColor)
    {
        color = spriteRenderer.color;
        color.r = newColor.r;
        color.g = newColor.g;
        color.b = newColor.b;
        spriteRenderer.color = color;
    }



    public static void SetRed(this Image image, float red)
    {
        color = image.color;
        color.r = red;
        image.color = color;
    }



    public static void SetGreen(this Image image, float green)
    {
        color = image.color;
        color.g = green;
        image.color = color;
    }



    public static void SetBlue(this Image image, float blue)
    {
        color = image.color;
        color.b = blue;
        image.color = color;
    }



    public static void SetAlpha(this Image image, float alpha)
    {
        color = image.color;
        color.a = alpha;
        image.color = color;
    }

    #endregion

    #region Colors - Text

    public static void SetColorIgnoringAlpha(this Text spriteRenderer, Color newColor)
    {
        color = spriteRenderer.color;
        color.r = newColor.r;
        color.g = newColor.g;
        color.b = newColor.b;
        spriteRenderer.color = color;
    }



    public static void SetRed(this Text text, float red)
    {
        color = text.color;
        color.r = red;
        text.color = color;
    }



    public static void SetGreen(this Text text, float green)
    {
        color = text.color;
        color.g = green;
        text.color = color;
    }



    public static void SetBlue(this Text text, float blue)
    {
        color = text.color;
        color.b = blue;
        text.color = color;
    }



    public static void SetAlpha(this Text text, float alpha)
    {
        color = text.color;
        color.a = alpha;
        text.color = color;
    }



    public static void SetAlpha(this Outline outline, float alpha)
    {
        color = outline.effectColor;
        color.a = alpha;
        outline.effectColor = color;
    }

    #endregion

    #region Colors - Material

    public static void SetColorIgnoringAlpha(this Material spriteRenderer, Color newColor)
    {
        color = spriteRenderer.color;
        color.r = newColor.r;
        color.g = newColor.g;
        color.b = newColor.b;
        spriteRenderer.color = color;
    }



    public static void SetRed(this Material Mat, float red)
    {
        color = Mat.color;
        color.a = red;
        Mat.color = color;
    }



    public static void SetGreen(this Material Mat, float green)
    {
        color = Mat.color;
        color.a = green;
        Mat.color = color;
    }



    public static void SetBlue(this Material Mat, float blue)
    {
        color = Mat.color;
        color.a = blue;
        Mat.color = color;
    }



    public static void SetAlpha(this Material Mat, float alpha)
    {
        color = Mat.color;
        color.a = alpha;
        Mat.color = color;
    }

    #endregion

    #region Colors - Tint Color

    /// <summary>
    /// Only if the shader has the "_TintColor" porperty
    /// </summary>
    public static void SetTintRed(this Material Mat, float red)
    {
        color = Mat.GetColor("_TintColor");
        color.a = red;
        Mat.SetColor("_TintColor", color);
    }



    /// <summary>
    /// Only if the shader has the "_TintColor" porperty
    /// </summary>
    public static void SetTintGreen(this Material Mat, float green)
    {
        color = Mat.GetColor("_TintColor");
        color.a = green;
        Mat.SetColor("_TintColor", color);
    }



    /// <summary>
    /// Only if the shader has the "_TintColor" porperty
    /// </summary>
    public static void SetTintBlue(this Material Mat, float blue)
    {
        color = Mat.GetColor("_TintColor");
        color.a = blue;
        Mat.SetColor("_TintColor", color);
    }



    /// <summary>
    /// Only if the shader has the "_TintColor" porperty
    /// </summary>
    public static void SetTintAlpha(this Material Mat, float alpha)
    {
        color = Mat.GetColor("_TintColor");
        color.a = alpha;
        Mat.SetColor("_TintColor", color);
    }

    #endregion
    
    #endregion

    #region Vectors

    static Vector3 vector2 = new Vector2(0, 0);
    static Vector3 vector3 = new Vector3(0, 0, 0);

    #region Vectors - 2D Positions

    public static void SetAnchoredPositionX(this RectTransform rectTransform, float X, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector2 = rectTransform.anchoredPosition;
        vector2.x = X;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.y = 0;
        }

        rectTransform.anchoredPosition = vector2;
	}



	public static void SetAnchoredPositionY(this RectTransform rectTransform, float Y, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector2 = rectTransform.anchoredPosition;
        vector2.y = Y;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
        }

        rectTransform.anchoredPosition = vector2;
	}



	public static void Set2DPositionX(this Transform transform, float X, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.position;
        vector3.x = X;
        vector3.z = 0;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.y = 0;
        }

        transform.position = vector3;
	}



	public static void Set2DLocalPositionX(this Transform transform, float X, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.localPosition;
        vector3.x = X;
        vector3.z = 0;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.y = 0;
        }

        transform.localPosition = vector3;
	}



	public static void Set2DPositionY(this Transform transform, float Y, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.position;
        vector3.y = Y;
        vector3.z = 0;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
        }

        transform.position = vector3;
	}



	public static void Set2DLocalPositionY(this Transform transform, float Y, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.localPosition;
        vector3.y = Y;
        vector3.z = 0;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
        }

        transform.localPosition = vector3;
	}

    #endregion

    #region Vectors - 3D Positions

    public static void SetPositionX(this Transform transform, float X, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.position;
        vector3.x = X;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.z = 0;
            vector3.y = 0;
        }

        transform.position = vector3;
    }



    public static void SetPositionX(this Rigidbody rigidbody, float X, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
    {
        vector3 = rigidbody.position;
        vector3.x = X;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.z = 0;
            vector3.y = 0;
        }

        rigidbody.position = vector3;
    }



    public static void SetLocalPositionX(this Transform transform, float X, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.localPosition;
        vector3.x = X;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.z = 0;
            vector3.y = 0;
        }

        transform.localPosition = vector3;
	}



	public static void SetPositionY(this Transform transform, float Y, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.position;
        vector3.y = Y;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
            vector3.z = 0;
        }

        transform.position = vector3;
    }



    public static void SetPositionY(this Rigidbody rigidbody, float Y, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
    {
        vector3 = rigidbody.position;
        vector3.y = Y;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
            vector3.z = 0;
        }

        rigidbody.position = vector3;
    }



    public static void SetLocalPositionY(this Transform transform, float Y, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.localPosition;
        vector3.y = Y;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
            vector3.z = 0;
        }

        transform.localPosition = vector3;
	}



	public static void SetPositionZ(this Transform transform, float Z, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.position;
        vector3.z = Z;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
            vector3.y = 0;
        }

        transform.position = vector3;
    }



    public static void SetPositionZ(this Rigidbody rigidbody, float Z, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
    {
        vector3 = rigidbody.position;
        vector3.z = Z;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
            vector3.y = 0;
        }

        rigidbody.position = vector3;
    }



    public static void SetLocalPositionZ(this Transform transform, float Z, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.localPosition;
        vector3.z = Z;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
            vector3.y = 0;
        }

        transform.localPosition = vector3;
	}

    #endregion

    #region Vectors - Eulers

    public static void SetEulerAnglesX(this Transform transform, float X, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.eulerAngles;
        vector3.x = X;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.z = 0;
            vector3.y = 0;
        }

        transform.eulerAngles = vector3;
	}



	public static void SetLocalEulerAnglesX(this Transform transform, float X, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.localEulerAngles;
        vector3.x = X;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.z = 0;
            vector3.y = 0;
        }

        transform.localEulerAngles = vector3;
	}



	public static void SetEulerAnglesY(this Transform transform, float Y, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.eulerAngles;
        vector3.y = Y;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
            vector3.z = 0;
        }

        transform.eulerAngles = vector3;
	}



	public static void SetLocalEulerAnglesY(this Transform transform, float Y, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.localEulerAngles;
        vector3.y = Y;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
            vector3.z = 0;
        }

        transform.localEulerAngles = vector3;
	}



	public static void SetEulerAnglesZ(this Transform transform, float Z, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.eulerAngles;
        vector3.z = Z;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
            vector3.y = 0;
        }

        transform.eulerAngles = vector3;
	}



	public static void SetLocalEulerAnglesZ(this Transform transform, float Z, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.localEulerAngles;
        vector3.z = Z;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
            vector3.y = 0;
        }

        transform.localEulerAngles = vector3;
	}

    #endregion

    #region Vectors - Scale

    public static void SetLocalScaleX(this Transform transform, float X, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.localScale;
        vector3.x = X;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.z = 0;
            vector3.y = 0;
        }

        transform.localScale = vector3;
	}



	public static void SetLocalScaleY(this Transform transform, float Y, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.localScale;
        vector3.y = Y;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
            vector3.z = 0;
        }

        transform.localScale = vector3;
	}



	public static void SetLocalScaleZ(this Transform transform, float Z, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = transform.localScale;
        vector3.z = Z;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
            vector3.y = 0;
        }

        transform.localScale = vector3;
	}

    #endregion

    #region Vectors - Velocity and Angular Velocity

    public static void SetVelocityX(this Rigidbody Rig, float X, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = Rig.velocity;
        vector3.x = X;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.z = 0;
            vector3.y = 0;
        }

        Rig.velocity = vector3;
	}



	public static void SetVelocityY(this Rigidbody Rig, float Y, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = Rig.velocity;
        vector3.y = Y;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
            vector3.z = 0;
        }

        Rig.velocity = vector3;
	}



	public static void SetVelocityZ(this Rigidbody Rig, float Z, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = Rig.velocity;
        vector3.z = Z;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
            vector3.y = 0;
        }

        Rig.velocity = vector3;
	}



	public static void SetAngularVelocityX(this Rigidbody Rig, float X, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = Rig.angularVelocity;
        vector3.x = X;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
            vector3.z = 0;
        }

        Rig.angularVelocity = vector3;
	}



	public static void SetAngularVelocityY(this Rigidbody Rig, float Y, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = Rig.angularVelocity;
        vector3.y = Y;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
            vector3.z = 0;
        }

        Rig.angularVelocity = vector3;
	}



	public static void SetAngularVelocityZ(this Rigidbody Rig, float Z, OtherAxes otherAxes = OtherAxes.LeaveAsIs)
	{
        vector3 = Rig.angularVelocity;
        vector3.z = Z;

        if (otherAxes == OtherAxes.Reset)
        {
            vector3.x = 0;
            vector3.y = 0;
        }

		Rig.angularVelocity = vector3;
    }

    #endregion

    #endregion

    #region Enlisting

    /// <summary>
    /// Works the same as GetComponentsInChildren, but instead of returning the results in an Array, returns them in a List
    /// </summary>
    public static List<T> EnlistComponentsInChildren<T>(this Transform transform, ComponentsInChildren componentsInChildren = ComponentsInChildren.IncludeSelf)
	{
		List<T> componentsList = new List<T> ();
		T[] components = transform.GetComponentsInChildren<T> ();

		foreach (T component in components) 
		{
			componentsList.Add (component);
		}

        if (componentsInChildren == ComponentsInChildren.ExcludeSelf)
        {
            componentsList.Remove(transform.GetComponent<T>());
        }

		return componentsList;
    }


    /// <summary>
    /// Returns a list from an array
    /// </summary>
    public static List<T> ArrayToList<T>(this T[] components)
    {
        List<T> componentsList = new List<T>();

        foreach (T component in components)
        {
            componentsList.Add(component);
        }

        return componentsList;
    }

    #endregion

    #region Lists

    /// <summary>
    /// Returns the elements that is next to the current element in the list. If the current element is the last one, returns the first element 
    /// </summary>
    public static T NextTo<T>(this List<T> myList, T element, Iterate iterate = Iterate.Cycle)
    {
        int Index = myList.IndexOf(element) + 1;

        if (iterate == Iterate.Cycle)
        {
            if (Index >= myList.Count)
                Index = 0;
        }

        return myList[Index.ClampMax(myList.Count - 1)];
    }



    /// <summary>
    /// Returns the elements that is previous to the current element in the list. If the previous index is less than zero, returns the last index of the list (myList.Count - 1)
    /// </summary>
    public static T PreviousTo<T>(this List<T> myList, T element, Iterate iterate = Iterate.Cycle)
    {
        int Index = (myList.IndexOf(element) - 1);

        if (iterate == Iterate.Cycle)
        {
            if (Index < 0)
                Index = myList.Count - 1;
        }
        
        return myList[Index.ClampMin(0)];
    }



    /// <summary>
    /// Returns the last element in a list
    /// </summary>
    public static T Last<T>(this List<T> myList)
    {
        return myList[myList.Count - 1];
    }



    /// <summary>
    /// Returns whether the specified index is the last index in given list
    /// </summary>
    public static bool IsLastInList<T>(this int index, List<T> myList)
    {
        return index == (myList.Count - 1);
    }



    /// <summary>
    /// Returns the most middle index in a list. For example, if the list is from 0 to 4, it returns 2. If no median index found, returns -1 
    /// </summary>
    public static int MedianIndex<T>(this List<T> myList)
    {
        for (int i = 0; i < myList.Count; i++)
        {
            if (i == (myList.Count - 1 - i))
            {
                return i;
            }
        }

        return -1;
    }



    /// <summary>
    /// Returns the most middle pair of indexes is a list where the number of elements is odd. For example, if the list is from 0 to 3, it returns 1 and 2. If the number of elements is even, returns (-1, -1)
    /// </summary>
    public static ClampedInt MedianPairIndex<T>(this List<T> myList)
    {
        List<T> firstList = new List<T>();
        List<T> secondList = new List<T>();

        for (int i = 0; i < myList.Count - 1; i++)
        {
            firstList.Add(myList[i]);
        }

        for (int i = 1; i < myList.Count; i++)
        {
            secondList.Add(myList[i]);
        }

        return new ClampedInt(firstList.MedianIndex(), secondList.MedianIndex());
    }



    /// <summary>
    /// Requests a specified index in a list increased by 1. If it's greater than the last index value, returns 0. Basically, it allows looping through the list, making sure that last index is automatically followed by the first one.
    /// </summary>
    public static int NextToIndex<T>(this List<T> myList, int Index, Iterate iterate = Iterate.Cycle)
    {
        Index++;

        if (iterate == Iterate.Cycle)
        {
            if (Index >= myList.Count)
                Index = 0;
        }

        return Index.ClampMax(myList.Count - 1);
    }



    /// <summary>
    /// Requests a specified index in a list decreased by 1. If it's lesser than zero, returns the last index of the list (myList.Count - 1). Basically, it allows looping through the list, making sure that first index is automatically followed by the last one.
    /// </summary>
    public static int PreviousToIndex<T>(this List<T> myList, int Index, Iterate iterate = Iterate.Cycle)
    {
        Index--;

        if (iterate == Iterate.Cycle)
        {
            if (Index < 0)
                Index = myList.Count - 1;
        }
        
        return Index.ClampMin(0);
    }



    /// <summary>
    /// Returns a random index in a specified list
    /// </summary>
    public static int RandomIndex<T>(this List<T> myList)
    {
        return Random.Range(0, myList.Count);
    }



    /// Returns a random element from a specified list
    /// </summary>
    public static T RandomElement<T>(this List<T> myList)
    {
        return myList[Random.Range(0, myList.Count)];
    }

    ///// Returns a random element from a specified list
    ///// </summary>
    //public static T RandomElementOrDefault<T>(this IReadOnlyList<T> myList)
    //{
    //    if (myList == null)
    //    {
    //        Debug.LogError($"collection was null!");
    //        return default(T);
    //    }

    //    if (myList.Count == 0)
    //    {
    //        Debug.LogError($"collection was empty!");
    //        return default(T);
    //    }

    //    return myList[Random.Range(0, myList.Count)];
    //}



    /// <summary>
    /// Returns whether the specified list has the given index
    /// </summary>
    public static bool ContainsIndex<T>(this List<T> myList, int newIndex)
    {
        return ((newIndex < myList.Count) && (newIndex >= 0));
    }



    /// <summary>
    /// Converts a list of enums into list of integers
    /// </summary>
    public static List<int> EnumListToIntList<T>(this List<T> enumList)
    {
        List<int> IntList = new List<int>();

        for (int i = 0; i < enumList.Count; i++)
        {
            IntList.Add(enumList[i].GetHashCode());
        }

        return IntList;
    }


    /// <summary>
    /// Sets flags to a list of components
    /// </summary>
    public static void SetFlags(this List<Component> componentsList, HideFlags flag)
    {
        foreach (Component component in componentsList)
        {
            component.hideFlags = flag;
        }
    }



    static Transform refTransform = null;
    static Transform RefTransform
    {
        get
        {
            if (refTransform == null)
            {
                refTransform = new GameObject("Ref Transform").transform;
            }

            return refTransform;
        }
    }

    #endregion

    #region Cursor


    public static void ActivateCursor()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}



	public static void DeactivateCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

    #endregion

    #region Lerp LookAt

    static Vector3 direction;
    static Quaternion rotation;



    /// <summary>
    /// Lerps the transform to look at a target position
    /// </summary>
    public static void LerpLookAt(this Transform transform, Vector3 targetPosition, float overTime, float deltaTime = 0)
    {
        if (deltaTime == 0)
        {
            deltaTime = Time.deltaTime;
        }

        direction = targetPosition - transform.position;
        rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 1 / overTime * deltaTime);
    }



    /// <summary>
    /// Lerps the transform to look at a target transform
    /// </summary>
    public static void LerpLookAt(this Transform transform, Transform targetTransform, float overTime, float deltaTime = 0)
    {
        if (deltaTime == 0)
        {
            deltaTime = Time.deltaTime;
        }

        direction = targetTransform.position - transform.position;
        rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 1 / overTime * deltaTime);
    }



    /// <summary>
    /// Lerps the transform to look at a target transform
    /// </summary>
    public static void LerpLookAt(this Transform transform, Transform targetTransform, float overTime, CustomAxis axis, float deltaTime = 0)
    {
        if (deltaTime == 0)
        {
            deltaTime = Time.deltaTime;
        }

        direction = targetTransform.position - transform.position;
        rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 1 / overTime * deltaTime);

        if (axis == CustomAxis.x)
        {
            transform.SetEulerAnglesY(0);
            transform.SetEulerAnglesZ(0);
        }
        else if (axis == CustomAxis.y)
        {
            transform.SetEulerAnglesX(0);
            transform.SetEulerAnglesZ(0);
        }
        else if (axis == CustomAxis.z)
        {
            transform.SetEulerAnglesY(0);
            transform.SetEulerAnglesX(0);
        }
    }

    #endregion

    #region Strings

    #region Adjustments

    /// <summary>
    /// Add spaces to a sentence with no spaces by searching for capital letters
    /// </summary>
    public static string AddSpacesToSentence(this string Input)
	{
		return new string(Input.ToCharArray().SelectMany((c, i) => i > 0 && char.IsUpper(c) ? new char[] { ' ', c } : new char[] { c }).ToArray());
	}



    /// <summary>
    /// Converts an enum to string and adds spaces to it by searching for capital letters
    /// </summary>
    public static string AddSpacesToSentence<T>(this T Enum)
	{
		string Input = Enum.ToString ();
		return new string(Input.ToCharArray().SelectMany((c, i) => i > 0 && char.IsUpper(c) ? new char[] { ' ', c } : new char[] { c }).ToArray());
	}


    /// <summary>
    /// Removes spaces from a string
    /// </summary>
    public static string NoSpaces(this string Input)
    {
        return Input.Replace(" ", "");
    }

    

    public static string Reverse(this string s)
    {
        char[] charArray = s.ToCharArray();
        System.Array.Reverse(charArray);
        return new string(charArray);
    }



    /// <summary>
    /// Separates the number's thousands with commas to make it more convenient to read. For example 12345 will be displayed as 12,345
    /// </summary>
    public static string SeparateThousands(this int Value)
    {
        return string.Format("{0:#,###0}", Value);
    }

    #endregion

    #region Hex Colors

    /// <summary>
    /// Returns the Hexadecimal (RGB Code) value of the color 
    /// </summary>
    public static string ToHexCode(this Color color)
    {
        return ColorUtility.ToHtmlStringRGBA(color);
    }



    /// <summary>
    /// Returns the specified string with the given color. Useful when you want to type a single word or a value in a different color
    /// </summary>
    public static string Color(this string text, Color color)
    {
        return "<Color=#" + color.ToHexCode() + ">" + text + "</Color>";
    }



    /// <summary>
    /// Returns the specified int with the given color. Useful when you want to type a single word or a value in a different color
    /// </summary>
    public static string Color(this int value, Color color)
    {
        return "<Color=#" + color.ToHexCode() + ">" + value + "</Color>";
    }



    /// <summary>
    /// Returns the specified float with the given color. Useful when you want to type a single word or a value in a different color
    /// </summary>
    public static string Color(this float value, Color color)
    {
        return "<Color=#" + color.ToHexCode() + ">" + value + "</Color>";
    }

    #endregion

    #endregion

    #region Transformations

    #region Transformations - Get

    /// <summary>
    /// Returns world space transformation (global position, eulerAngles and scale) of a transform 
    /// </summary>
    public static Transformation GetGlobalTransformation(this Transform transform)
	{
		return new Transformation (transform.position, transform.eulerAngles, transform.localScale);
    }



    /// <summary>
    /// Returns local space transformation (local position, eulerAngles and scale) of a transform 
    /// </summary>
    public static Transformation GetLocalTransformation(this Transform transform)
	{
		return new Transformation (transform.localPosition, transform.localEulerAngles, transform.localScale);
    }

    #endregion
    
    #region Transformations - Set
    
    /// <summary>
    /// Set the global space position, rotation and scale of a transform to a specified transformation 
    /// </summary>
    public static void SetGlobalTransformation(this Transform transform, Transformation transformation, IgnoreVector ignoreVector = IgnoreVector.None)
    {
        if (ignoreVector != IgnoreVector.Position)
            transform.position = transformation.Position;

        if (ignoreVector != IgnoreVector.Rotation)
            transform.eulerAngles = transformation.Rotation;

        if (ignoreVector != IgnoreVector.Scale)
            transform.localScale = transformation.Scale;
    }



    /// <summary>
    /// Set the local space position, rotation and scale of a transform to a specified transformation 
    /// </summary>
    public static void SetLocalTransformation(this Transform transform, Transformation transformation, IgnoreVector ignoreVector = IgnoreVector.None)
    {
        if (ignoreVector != IgnoreVector.Position)
            transform.localPosition = transformation.Position;

        if (ignoreVector != IgnoreVector.Rotation)
            transform.localEulerAngles = transformation.Rotation;

        if (ignoreVector != IgnoreVector.Scale)
            transform.localScale = transformation.Scale;
    }

    #endregion

    #region Transformations - Other

    /// <summary>
    /// Returns whether the specified TransformWithTransformationsRange contains the given transform
    /// </summary>
    public static bool ContainsTransform(this List<TransformWithTransformationsRange> transformsWithTransformationsRanges, Transform transform)
    {
        foreach (TransformWithTransformationsRange item in transformsWithTransformationsRanges)
        {
            if (item.StoredTransform == transform)
                return true;
        }

        return false;
    }


    /// <summary>
    /// Copies the Transformation values (Position, Rotation, Scale) from another transform and pastes them onto this one
    /// </summary>
    public static void CloneOtherTransform(this Transform transform, Transform otherTransform)
    {
        transform.SetLocalTransformation(otherTransform.GetLocalTransformation());
    }

    #endregion

    #endregion

    #region RectTransformations

    /// <summary>
    /// Returns the RectTransformation of a RectTransform 
    /// </summary>
    public static RectTransformation GetRectTransformation(this RectTransform rectTransform)
    {
        return new RectTransformation(rectTransform.anchoredPosition3D, rectTransform.sizeDelta, rectTransform.anchorMin, 
            rectTransform.anchorMax, rectTransform.pivot, rectTransform.localEulerAngles, rectTransform.localScale);
    }


    /// <summary>
    /// Set the RectTransformation to a specified RectTransform 
    /// </summary>
    public static void SetRectTransformation(this RectTransform rectTransform, RectTransformation rectTransformation)
    {
        SetRectTransformation(rectTransform, rectTransformation, RectTransformation.AllVectors);
    }


    /// <summary>
    /// Set the RectTransformation to a specified RectTransform 
    /// </summary>
    public static void SetRectTransformation(this RectTransform rectTransform, RectTransformation rectTransformation, List<VectorType> listedVectors)
    {
        if ((listedVectors.Contains(VectorType.AnchoredPosition)) || (listedVectors.Contains(VectorType.AnchoredPosition3D)))
            rectTransform.anchoredPosition3D = rectTransformation.AnchoredPosition;

        if (listedVectors.Contains(VectorType.SizeDelta))
            rectTransform.sizeDelta = rectTransformation.SizeDelta;

        if (listedVectors.Contains(VectorType.AnchorMin))
            rectTransform.anchorMin = rectTransformation.AnchorMin;

        if (listedVectors.Contains(VectorType.AnchorMax))
            rectTransform.anchorMax = rectTransformation.AnchorMax;

        if (listedVectors.Contains(VectorType.Pivot))
            rectTransform.pivot = rectTransformation.Pivot;

        if (listedVectors.Contains(VectorType.LocalEuler))
            rectTransform.localEulerAngles = rectTransformation.Rotation;

        if (listedVectors.Contains(VectorType.LocalScale))
            rectTransform.localScale = rectTransformation.Scale;
    }


    /// <summary>
    /// Copies the RectTransformation values from another RectTransform and pastes them onto this one
    /// </summary>
    public static void CloneOtherRectTransform(this RectTransform rectTransform, RectTransform otherRectTransform)
    {
        rectTransform.SetRectTransformation(otherRectTransform.GetRectTransformation());
    }

    #endregion

    #region Math

    #region Math - Basic

    /// <summary>
    /// Returns value rounded to the nearest odd integer
    /// </summary>
    public static float RoundToOddNumber(this float Value)
	{
		float NearestNum = Mathf.Round (Value);

		if (NearestNum % 2 != 0)
			return NearestNum;

		if (NearestNum >= Value)
			return NearestNum - 1;

		return NearestNum + 1;
	}



    /// <summary>
    /// Returns value rounded to the nearest even integer
    /// </summary>
    public static float RoundToEvenNumber(this float Value)
	{
		float NearestNum = Mathf.Round (Value);

		if (NearestNum % 2 == 0)
			return NearestNum;

		if (NearestNum >= Value)
			return NearestNum - 1;

		return NearestNum + 1;
	}



    /// <summary>
    /// Returns whether the integer is an Odd number (0,2,4,6...)
    /// </summary>
    public static bool IsOdd(this int Value)
	{
		return (Value % 2 == 0);
	}



    /// <summary>
    /// Returns whether the integer is an Even number (1,3,5,7...)
    /// </summary>
    public static bool IsEven(this int Value)
	{
		return (Value % 2 != 0);
    }



    /// <summary>
    /// Returns whether the integer can be divided by a specified value
    /// </summary>
    public static bool IsDivisibleBy(this int Value, int divider)
    {
        return (Value % divider == 0);
    }



    /// <summary>
    /// Limits the number of decimal digits to the specified amount
    /// </summary>
    public static float LimitToDigitsNumber(this float Value, int NumberOfDigits = 2)
    {
        float power = Mathf.Pow(10, NumberOfDigits);
        return Mathf.Round(Value * power) / power;
    }

    #endregion

    #region Math - Clamping
    
    /// <summary>
    /// Same as Mathf.Clamp but works as an extension method
    /// </summary>
    public static float Clamped(this float Value, float Min, float Max)
    {
        if (Value < Min)
            Value = Min;
        else if (Value > Max)
            Value = Max;

        return Value;
    }


    /// <summary>
    /// Same as Mathf.Clamp but works as an extension method
    /// </summary>
    public static float Clamped(this float Value, ClampedFloat clampedFloat)
    {
        if (Value < clampedFloat.Min)
            Value = clampedFloat.Min;
        else if (Value > clampedFloat.Max)
            Value = clampedFloat.Max;

        return Value;
    }


    /// <summary>
    /// Same as Mathf.Clamp01 but works as an extension method
    /// </summary>
    public static float Clamped01(this float Value)
    {
        if (Value < 0)
            Value = 0;
        else if (Value > 1)
            Value = 1;

        return Value;
    }


    /// <summary>
    /// Same as Mathf.Clamp but works as an extension method
    /// </summary>
    public static int Clamped(this int Value, int Min, int Max)
    {
        if (Value < Min)
            Value = Min;
        else if (Value > Max)
            Value = Max;

        return Value;
    }


    /// <summary>
    /// Same as Mathf.Clamp but works as an extension method
    /// </summary>
    public static int Clamped(this int Value, ClampedInt clampedInt)
    {
        if (Value < clampedInt.Min)
            Value = clampedInt.Min;
        else if (Value > clampedInt.Max)
            Value = clampedInt.Max;

        return Value;
    }


    /// <summary>
    /// Prevents the value from going below a specified minimum value
    /// </summary>
    public static float ClampMin(this float Value, float Min)
    {
        if (Value < Min)
            return Min;

        return Value;
    }


    /// <summary>
    /// Prevents the value from going above a specified maximum value
    /// </summary>
    public static float ClampMax(this float Value, float Max)
    {
        if (Value > Max)
            return Max;

        return Value;
    }


    /// <summary>
    /// Prevents the value from going below a specified minimum value
    /// </summary>
    public static int ClampMin(this int Value, int Min)
    {
        if (Value < Min)
            return Min;

        return Value;
    }


    /// <summary>
    /// Prevents the value from going above a specified maximum value
    /// </summary>
    public static int ClampMax(this int Value, int Max)
    {
        if (Value > Max)
            return Max;

        return Value;
    }

    #endregion

    #endregion

    #region Particles


    static ParticleSystem.MainModule main;
    static ParticleSystem.MinMaxGradient minMaxGradient;
    static ParticleSystem.MinMaxCurve minMaxCurve;
    static ParticleSystem.EmissionModule emission;
    static ParticleSystem.TrailModule trails;
    static ParticleSystem.CollisionModule collision;
    static ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime;
    static ParticleSystem.ColorOverLifetimeModule colorOverLifetime;

    #region Other

    public static void Restart(this ParticleSystem particleSystem)
    {
        particleSystem.Clear();
        particleSystem.time = 0;
        particleSystem.Play();
    }



    public static void SetCollisionActive(this ParticleSystem particleSystem, bool value)
    {
        collision = particleSystem.collision;
        collision.enabled = value;
    }



    public static void SetColorOverLifetime(this ParticleSystem particleSystem, Gradient gradient)
    {
        colorOverLifetime = particleSystem.colorOverLifetime;
        colorOverLifetime.color = gradient;
    }

    #endregion

    #region Particles - Lifetime

    public static void SetLifetime(this ParticleSystem particleSystem, float value)
    {
        main = particleSystem.main;
        minMaxCurve = main.startLifetime;
        minMaxCurve.constant = value;
        main.startLifetime = minMaxCurve;
    }



    public static void SetLifetime(this ParticleSystem particleSystem, float min, float max)
    {
        main = particleSystem.main;
        minMaxCurve = main.startLifetime;
        minMaxCurve.constantMin = min;
        minMaxCurve.constantMax = max;
        main.startLifetime = minMaxCurve;
    }



    public static void SetLifetime(this ParticleSystem particleSystem, ClampedFloat clampedFloat)
    {
        main = particleSystem.main;
        minMaxCurve = main.startLifetime;
        minMaxCurve.constantMin = clampedFloat.Min;
        minMaxCurve.constantMax = clampedFloat.Max;
        main.startLifetime = minMaxCurve;
    }



    public static void SetLifetime(this ParticleSystem particleSystem, ParticleSystem.MinMaxCurve minMaxCurve)
    {
        main = particleSystem.main;
        main.startLifetime = minMaxCurve;
    }

    #endregion

    #region Particles - Start Speed

    public static void SetStartSpeed(this ParticleSystem particleSystem, float value)
    {
        main = particleSystem.main;
        minMaxCurve = main.startSpeed;
        minMaxCurve.constant = value;
        main.startSpeed = minMaxCurve;
    }



    public static void SetStartSpeed(this ParticleSystem particleSystem, ClampedFloat clampedFloat)
    {
        main = particleSystem.main;
        minMaxCurve = main.startSpeed;
        minMaxCurve.constantMin = clampedFloat.Min;
        minMaxCurve.constantMax = clampedFloat.Max;
        main.startSpeed = minMaxCurve;
    }



    public static void SetStartSpeed(this ParticleSystem particleSystem, ParticleSystem.MinMaxCurve minMaxCurve)
    {
        main = particleSystem.main;
        main.startSpeed = minMaxCurve;
    }

    #endregion

    #region Particles - Gravity
    
    public static void SetGravity(this ParticleSystem particleSystem, float value)
    {
        main = particleSystem.main;
        minMaxCurve = main.gravityModifier;
        minMaxCurve.constant = value;
        main.gravityModifier = minMaxCurve;
    }



    public static void SetGravity(this ParticleSystem particleSystem, ClampedFloat clampedFloat)
    {
        main = particleSystem.main;
        minMaxCurve = main.gravityModifier;
        minMaxCurve.constantMin = clampedFloat.Min;
        minMaxCurve.constantMax = clampedFloat.Max;
        main.gravityModifier = minMaxCurve;
    }



    public static void SetGravity(this ParticleSystem particleSystem, ParticleSystem.MinMaxCurve minMaxCurve)
    {
        main = particleSystem.main;
        main.gravityModifier = minMaxCurve;
    }

    #endregion

    #region Particles - Gradient
    
    public static void SetGradient(this ParticleSystem particleSystem, ParticleSystem.MinMaxGradient gradient)
    {
        main = particleSystem.main;
        main.startColor = gradient;
    }



    public static void SetMinGradient(this ParticleSystem particleSystem, Gradient gradient)
    {
        main = particleSystem.main;
        minMaxGradient = main.startColor;
        minMaxGradient.gradientMin = gradient;
        main.startColor = minMaxGradient;
    }



    public static void SetMaxGradient(this ParticleSystem particleSystem, Gradient gradient)
    {
        main = particleSystem.main;
        minMaxGradient = main.startColor;
        minMaxGradient.gradientMax = gradient;
        main.startColor = minMaxGradient;
    }

    #endregion

    #region Particles - Color
    
    public static void SetColor(this ParticleSystem particleSystem, Color color)
    {
        main = particleSystem.main;
        minMaxGradient = main.startColor;
        minMaxGradient.color = color;
        main.startColor = minMaxGradient;
    }



    public static void SetColorAlpha(this ParticleSystem particleSystem, float alpha)
    {
        main = particleSystem.main;
        minMaxGradient = main.startColor;
        color = main.startColor.color;
        color.a = alpha;
        minMaxGradient.color = color;
        main.startColor = minMaxGradient;
    }



    public static void SetMinColor(this ParticleSystem particleSystem, Color color)
    {
        main = particleSystem.main;
        minMaxGradient = main.startColor;
        minMaxGradient.colorMin = color;
        main.startColor = minMaxGradient;
    }



    public static void SetMinColorAlpha(this ParticleSystem particleSystem, float alpha)
    {
        main = particleSystem.main;
        minMaxGradient = main.startColor;
        color = main.startColor.colorMin;
        color.a = alpha;
        minMaxGradient.colorMin = color;
        main.startColor = minMaxGradient;
    }



    public static void SetMaxColor(this ParticleSystem particleSystem, Color color)
    {
        main = particleSystem.main;
        minMaxGradient = main.startColor;
        minMaxGradient.colorMax = color;
        main.startColor = minMaxGradient;
    }



    public static void SetMaxColorAlpha(this ParticleSystem particleSystem, float alpha)
    {
        main = particleSystem.main;
        minMaxGradient = main.startColor;
        color = main.startColor.colorMax;
        color.a = alpha;
        minMaxGradient.colorMax = color;
        main.startColor = minMaxGradient;
    }

    #endregion

    #region Particles - Emission

    public static void SetEmissionEnabled(this ParticleSystem particleSystem, bool enabled)
    {
        emission = particleSystem.emission;
        emission.enabled = enabled;
    }



    public static void SetEmissionOverTime(this ParticleSystem particleSystem, float rate)
    {
        emission = particleSystem.emission;
        minMaxCurve = emission.rateOverTime;
        minMaxCurve.constant = rate;
        emission.rateOverTime = minMaxCurve;
    }



    public static void SetEmissionOverTime(this ParticleSystem particleSystem, ClampedFloat clampedFloat)
    {
        emission = particleSystem.emission;
        minMaxCurve = emission.rateOverTime;
        minMaxCurve.constantMin = clampedFloat.Min;
        minMaxCurve.constantMax = clampedFloat.Max;
        emission.rateOverTime = minMaxCurve;
    }



    public static void SetEmissionOverTime(this ParticleSystem particleSystem, ParticleSystem.MinMaxCurve minMaxCurve)
    {
        emission = particleSystem.emission;
        emission.rateOverTime = minMaxCurve;
    }



    public static void SetEmissionOverDistance(this ParticleSystem particleSystem, float rate)
    {
        emission = particleSystem.emission;
        minMaxCurve = emission.rateOverDistance;
        minMaxCurve.constant = rate;
        emission.rateOverDistance = minMaxCurve;
    }



    public static void SetEmissionOverDistance(this ParticleSystem particleSystem, ClampedFloat clampedFloat)
    {
        emission = particleSystem.emission;
        minMaxCurve = emission.rateOverDistance;
        minMaxCurve.constantMin = clampedFloat.Min;
        minMaxCurve.constantMax = clampedFloat.Max;
        emission.rateOverDistance = minMaxCurve;
    }



    public static void SetEmissionOverDistance(this ParticleSystem particleSystem, ParticleSystem.MinMaxCurve minMaxCurve)
    {
        emission = particleSystem.emission;
        emission.rateOverDistance = minMaxCurve;
    }

    #endregion

    #region Particles - Trails
    
    public static void SetTrailsLifeTime(this ParticleSystem particleSystem, ParticleSystem.MinMaxCurve minMaxCurve)
    {
        trails = particleSystem.trails;
        trails.lifetime = minMaxCurve;
    }



    public static void SetTrailsLifeTime(this ParticleSystem particleSystem, float rate)
    {
        trails = particleSystem.trails;
        minMaxCurve = trails.lifetime;
        minMaxCurve.constant = rate;
        trails.lifetime = minMaxCurve;
    }



    public static void SetTrailsLifeTime(this ParticleSystem particleSystem, ClampedFloat clampedFloat)
    {
        trails = particleSystem.trails;
        minMaxCurve = trails.lifetime;
        minMaxCurve.constantMin = clampedFloat.Min;
        minMaxCurve.constantMax = clampedFloat.Max;
        trails.lifetime = minMaxCurve;
    }



    public static void SetTrailsMaxLifeTime(this ParticleSystem particleSystem, float value)
    {
        trails = particleSystem.trails;
        minMaxCurve = trails.lifetime;
        minMaxCurve.constantMax = value;
        trails.lifetime = minMaxCurve;
    }



    public static void SetTrailsMinLifeTime(this ParticleSystem particleSystem, float value)
    {
        trails = particleSystem.trails;
        minMaxCurve = trails.lifetime;
        minMaxCurve.constantMin = value;
        trails.lifetime = minMaxCurve;
    }

    #endregion

    #region Particles - Radial Velocity

    //public static void SetRadialVelocity(this ParticleSystem particleSystem, ParticleSystem.MinMaxCurve minMaxCurve)
    //{
    //    velocityOverLifetime = particleSystem.velocityOverLifetime;
    //    velocityOverLifetime.radial = minMaxCurve;
    //}



    //public static void SetRadialVelocity(this ParticleSystem particleSystem, float rate)
    //{
    //    velocityOverLifetime = particleSystem.velocityOverLifetime;
    //    minMaxCurve = velocityOverLifetime.radial;
    //    minMaxCurve.constant = rate;
    //    velocityOverLifetime.radial = minMaxCurve;
    //}



    //public static void SetRadialVelocity(this ParticleSystem particleSystem, ClampedFloat clampedFloat)
    //{
    //    velocityOverLifetime = particleSystem.velocityOverLifetime;
    //    minMaxCurve = velocityOverLifetime.radial;
    //    minMaxCurve.constantMin = clampedFloat.Min;
    //    minMaxCurve.constantMax = clampedFloat.Max;
    //    velocityOverLifetime.radial = minMaxCurve;
    //}

    #endregion

    #endregion

    #region Line Renderer

    public static void SetStartColorAlpha(this LineRenderer lineRenderer, float alpha)
    {
        color = lineRenderer.startColor;
        color.a = alpha;
        lineRenderer.startColor = color;
    }



    public static void SetEndColorAlpha(this LineRenderer lineRenderer, float alpha)
    {
        color = lineRenderer.endColor;
        color.a = alpha;
        lineRenderer.endColor = color;
    }



    public static void SetColorAlpha(this LineRenderer lineRenderer, float alpha)
    {
        color = lineRenderer.startColor;
        color.a = alpha;
        lineRenderer.startColor = color;

        color = lineRenderer.endColor;
        color.a = alpha;
        lineRenderer.endColor = color;
    }


    /// <summary>
    /// Returns the distance between two Line Renderer's points. If the point of one of the specified index does not exist in the line renderer, returns the distance between the points at indexes 0 and 1
    /// </summary>
    public static float DistanceBetweenPoints(this LineRenderer lineRenderer, int firstPointInex, int secondPointIndex)
    {
        if ((lineRenderer.positionCount - 1 < firstPointInex) || (lineRenderer.positionCount - 1 < secondPointIndex))
        {
            return Vector3.Distance(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
        }

        return Vector3.Distance(lineRenderer.GetPosition(firstPointInex), lineRenderer.GetPosition(secondPointIndex));
    }


    /// <summary>
    /// Returns the distance between two Line Renderer's points at indexes 0 and 1
    /// </summary>
    public static float Distance01(this LineRenderer lineRenderer)
    {
        return Vector3.Distance(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
    }

    #endregion

    #region Random
    
    #region Random - Vectors

    public static Vector3 RandomVector3(float min, float max)
    {
        return new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
    }



    public static Vector3 RandomVector3(float min, float max, CustomAxis axes)
    {
        if (axes == CustomAxis.xy)
        {
            return new Vector3(0, Random.Range(min, max), Random.Range(min, max));
        }
        else if (axes == CustomAxis.yz)
        {
            return new Vector3(Random.Range(min, max), 0, Random.Range(min, max));
        }
        else if (axes == CustomAxis.xz)
        {
            return new Vector3(Random.Range(min, max), Random.Range(min, max), 0);
        }

        return new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
    }



    public static Vector3 RandomUniformVector3(float min, float max)
    {
        float randomValue = Random.Range(min, max);
        return new Vector3(randomValue, randomValue, randomValue);
    }



    public static Vector2 RandomVector2(float min, float max)
    {
        return new Vector2(Random.Range(min, max), Random.Range(min, max));
    }



    public static Vector2 RandomUniformVector2(float min, float max)
    {
        float randomValue = Random.Range(min, max);
        return new Vector2(randomValue, randomValue);
    }



    public static Vector3 RandomEuler
	{
		get 
		{
			return new Vector3 (Random.Range (0, 360), Random.Range (0, 360), Random.Range (0, 360));
		}
    }



    public static Vector3 RandomUniformEuler
    {
        get
        {
            float randomAngle = Random.Range(0, 360);
            return new Vector3(randomAngle, randomAngle, randomAngle);
        }
    }



    public static Vector3 RandomDirection
    {
        get
        {
            return new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        }
    }



    public static Vector3 RandomUniformDirection
    {
        get
        {
            float randomDirection = Random.Range(0, 360);
            return new Vector3(randomDirection, randomDirection, randomDirection);
        }
    }

    #endregion

    #region Random - Colors

    public static Color RandomOpaqueColorRGB
    {
        get
        {
            return new Color(Random.value, Random.value, Random.value, 1);
        }
    }



    public static Color RandomColorRGB
    {
        get
        {
            return new Color(Random.value, Random.value, Random.value, Random.value);
        }
    }



    public static Color RandomBrightness
    {
        get
        {
            float randomValue = Random.value;
            return new Color(randomValue, randomValue, randomValue, 1);
        }
    }

    #endregion
    
    #region Random - Other

    /// <summary>
    /// Returns a random value which has to match exactly one of the specified values, and not the in-between values. <para></para>
    /// For example, with the numbers 1 and 10, the function can only return 1 or 10, not 5, not 3, not 8
    /// </summary>
    public static int RandomOfSpecificValues(int first, int second)
    {
        int index = Random.Range(0, 2);
        return (index == 0) ? first : second;
    }



    /// <summary>
    /// Returns a random value which has to match exactly one of the specified values, and not the in-between values. <para></para>
    /// For example, with the numbers 1 and 10, the function can only return 1 or 10, not 5, not 3, not 8
    /// </summary>
    public static float RandomOfSpecificValues(float first, float second)
    {
        int index = Random.Range(0, 2);
        return (index == 0) ? first : second;
    }

    #endregion
    
    #endregion

    #region Pointer Over UI


    /// <summary>
    /// Check if the mouse was clicked over a UI element
    /// </summary>
    public static bool IsPointerOverUI()
    {
        // we don't want to fire when we interact with UI buttons for example. IsPointerOverGameObject really means IsPointerOver*UI*GameObject
        // notice we don't use on on GetbuttonUp() few lines down, because one can mouse down, move over a UI element and release, which would lead to not lower the isFiring Flag.
        return EventSystem.current.IsPointerOverGameObject();
    }


    /// <summary>
    /// Check if the mouse was clicked over a UI element. Should be used with "this."prefix, for example this.IsPointerOverUI()
    /// </summary>
    public static bool IsPointerOverUI(this MonoBehaviour monoBehaviour)
    {
        // we don't want to fire when we interact with UI buttons for example. IsPointerOverGameObject really means IsPointerOver*UI*GameObject
        // notice we don't use on on GetbuttonUp() few lines down, because one can mouse down, move over a UI element and release, which would lead to not lower the isFiring Flag.
        return EventSystem.current.IsPointerOverGameObject();
    }



    /// <summary>
    /// Check if the mouse was clicked over a UI element
    /// </summary>
    public static bool IsPointerOverUI(this int touchFingerId)
    {
        // we don't want to fire when we interact with UI buttons for example. IsPointerOverGameObject really means IsPointerOver*UI*GameObject
        // notice we don't use on on GetbuttonUp() few lines down, because one can mouse down, move over a UI element and release, which would lead to not lower the isFiring Flag.
        return EventSystem.current.IsPointerOverGameObject(touchFingerId);
    }

    #endregion
    
    #region Animator

    /// <summary>
    /// Returns the current Animator state
    /// </summary>
    public static string CurrentState(this Animator animator, string currentState, int layerIndex = 0)
    {
        return animator.GetCurrentAnimatorStateInfo(layerIndex).ToString();
    }



    /// <summary>
    /// Returns whether the Animator is running a specified state
    /// </summary>
    public static bool IsCurrentState(this Animator animator, string currentState, int layerIndex = 0)
    {
        return animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(currentState);
    }

    #endregion

    #region Media

    #region Media - Audio

    /// <summary>
    /// Plays an AudioTrack, adjusting the AudioSource to the AudioTrack's parameters
    /// </summary>
    public static void PlayOneShot(this AudioSource audioSource, AudioTrack audioTrack, SoundPlayingConflict soundPlayingConflict = SoundPlayingConflict.OverwritePrevious)
    {
        if (audioSource.isPlaying)
        {
            if (soundPlayingConflict == SoundPlayingConflict.Ignore)
                return;
            else if ((soundPlayingConflict == SoundPlayingConflict.IgnoreSame) && (audioSource.clip == audioTrack.Track))
                return;
        }
        
        audioSource.Stop();
        audioSource.PlayOneShot(audioTrack.Track);
        audioSource.volume = audioTrack.Volume;
        audioSource.pitch = audioTrack.Pitch;
        audioSource.spatialBlend = audioTrack.SpatialBlend;
        audioSource.loop = audioTrack.Loop;
    }


    /// <summary>
    /// Stops the AudioSource if the it is currently playing the specified AudioTrack
    /// </summary>
    public static void StopAudioTrack(this AudioSource audioSource, AudioTrack audioTrack)
    {
        if (audioTrack == null)
            return;

        if (audioSource.clip == audioTrack.Track)
        {
            audioSource.Stop();
        }
    }


    /// <summary>
    /// Stops the AudioSource if the it is currently playing the specified AudioClip
    /// </summary>
    public static void StopAudioTrack(this AudioSource audioSource, AudioClip audioClip)
    {
        if (audioClip == null)
            return;

        if (audioSource.clip == audioClip)
        {
            audioSource.Stop();
        }
    }

    #endregion

    #region Media - Wait While
    
    /// <summary>
    /// Example of usage ==> yield return StartCoroutine(audioSource.WaitWhileAudioPlaying(audioClip));
    /// </summary>
    public static IEnumerator WaitWhileAudioPlaying(this AudioSource audioSource, AudioClip audioClip)
    {
        if (audioClip == null)
            yield break;

        yield return new WaitWhile(() => audioSource.clip == audioClip);
    }



    /// <summary>
    /// Example of usage ==> yield return StartCoroutine(audioSource.WaitWhileAudioPlaying(audioTrack));
    /// </summary>
    public static IEnumerator WaitWhileAudioPlaying(this AudioSource audioSource, AudioTrack audioTrack)
    {
        if ((audioTrack == null) || (audioTrack.Track == null) || (audioSource.clip == null))
            yield break;

        yield return new WaitWhile(() => audioSource.clip == audioTrack.Track);
    }



    /// <summary>
    /// Example of usage ==> yield return StartCoroutine(audioSource.WaitForEndOfAudio());
    /// </summary>
    public static IEnumerator WaitForEndOfAudio(this AudioSource audioSource)
    {
        if (audioSource.clip == null)
            yield break;

        float audioCliplength = audioSource.clip.length;
        yield return new WaitWhile(() => audioSource.time < audioCliplength * 0.99f);
    }



    /// <summary>
    /// Example of usage ==> yield return StartCoroutine(videoPlayer.WaitWhileAudioPlaying(audioTrack));
    /// </summary>
    public static IEnumerator WaitWhileVideoPlaying(this VideoPlayer videoPlayer, VideoClip videoClip)
    {
        if ((videoClip == null) || (videoPlayer.clip == null))
            yield break;

        yield return new WaitWhile(() => videoPlayer.clip == videoClip);
    }



    /// <summary>
    /// Example of usage ==> yield return StartCoroutine(videoPlayer.WaitForEndOfVideo());
    /// </summary>
    public static IEnumerator WaitForEndOfVideo(this VideoPlayer videoPlayer)
    {
        if (videoPlayer.clip == null)
            yield break;

        double videoCliplength = videoPlayer.clip.length;
        yield return new WaitWhile(() => videoPlayer.time < videoCliplength * 0.99f);
    }

    #endregion

    #region Media - Video
    
    /// <summary>
    /// Plays the video one time, and then restores the previous video clip the video player was playing. Also, releases the render texture if no video was playing before
    /// Example of usage ==> yield return StartCoroutine(videoPlayer.PlayOneShot(videoClip));
    /// </summary>
    public static IEnumerator PlayOneShot(this VideoPlayer videoPlayer, VideoClip videoClip)
    {
        if (videoClip == null)
            yield break;

        VideoClip previousVideoClip = videoPlayer.clip;
        videoPlayer.clip = videoClip;
        videoPlayer.Play();
        double videoCliplength = videoPlayer.clip.length;
        yield return new WaitWhile(() => videoPlayer.time < videoCliplength * 0.99f);

        if (previousVideoClip == null)
        {
            videoPlayer.targetTexture.Release();
        }

        videoPlayer.clip = previousVideoClip;
    }

    #endregion

    #region Media - Over Time
    
    /// <summary>
    /// Lerps to a target volume value over a set amount of time. Should be used with the yield return instruction inside a runing coroutine <para></para>
    /// Example of usage ==> yield return StartCoroutine(audioSource.SetVolumeOverTime(targetVolume, overtime);<para></para>
    /// Example of usage ==> StartCoroutine(audioSource.SetVolumeOverTime(overtime);
    /// </summary>
    public static IEnumerator SetVolumeOverTime(this AudioSource audioSource, float volume, float overTime, bool playIfNotPlaying = true)
    {
        if (!audioSource.isPlaying && playIfNotPlaying)
        {
            audioSource.Play();
        }

        volume = volume.Clamped01();

        while (audioSource.volume < volume - 0.01f)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, volume, 1 / overTime * Time.deltaTime);
            yield return null;
        }

        audioSource.volume = volume;
        yield return null;
    }



    /// <summary>
    /// Lerps to the minimum volume value over a set amount of time. Should be used with the yield return instruction inside a runing coroutine <para></para>
    /// Example of usage ==> yield return StartCoroutine(audioSource.DropVolumeOverTime(overtime);<para></para>
    /// Example of usage ==> StartCoroutine(audioSource.DropVolumeOverTime(overtime);
    /// </summary>
    public static IEnumerator DropVolumeOverTime(this AudioSource audioSource, float dropDuration, bool stopIfPlaying = true)
    {
        float Vol = audioSource.volume;

        while (audioSource.volume > 0.01f)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0, 1 / dropDuration * Time.deltaTime);
            yield return null;
        }

        audioSource.volume = 0;
        yield return null;

        if (audioSource.isPlaying && stopIfPlaying)
        {
            audioSource.Stop();
        }
    }


    /// <summary>
    /// Lerps to a target pitch value over a set amount of time. Should be used with the yield return instruction inside a runing coroutine <para></para>
    /// Example of usage ==> yield return StartCoroutine(audioSource.SetPitchOverTime(targetPitch, overtime);<para></para>
    /// Example of usage ==> StartCoroutine(audioSource.SetPitchOverTime(overtime);
    /// </summary>
    public static IEnumerator SetPitchOverTime(this AudioSource audioSource, float pitch, float overTime, bool playIfNotPlaying = true)
    {
        if (!audioSource.isPlaying && playIfNotPlaying)
        {
            audioSource.Play();
        }

        while (audioSource.pitch < pitch - 0.01f)
        {
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, pitch, 1 / overTime * Time.deltaTime);
            yield return null;
        }

        audioSource.pitch = pitch;
        yield return null;
    }



    /// <summary>
    /// Lerps to the minimum pitch value over a set amount of time. Should be used with the yield return instruction inside a runing coroutine <para></para>
    /// Example of usage ==> yield return StartCoroutine(audioSource.DropPitchOverTime(overtime);<para></para>
    /// Example of usage ==> StartCoroutine(audioSource.DropPitchOverTime(overtime);
    /// </summary>
    public static IEnumerator DropPitchOverTime(this AudioSource audioSource, float dropDuration, bool stopIfPlaying = true)
    {
        float Vol = audioSource.pitch;

        while (audioSource.pitch > 0.01f)
        {
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, 0, 1 / dropDuration * Time.deltaTime);
            yield return null;
        }

        audioSource.pitch = 0;
        yield return null;

        if (audioSource.isPlaying && stopIfPlaying)
        {
            audioSource.Stop();
        }
    }

    #endregion

    #endregion

    #region GameObjects and SceneObjects

    static MonoBehaviourEx monoBehaviourEx = new MonoBehaviourEx();

    #region GameObjects and SceneObjects - Reinstantiate

    /// <summary>
    /// Destroys the current SceneObject and Instantiates it again from the prefab, preserving its current position, rotation, scale and parent, but resetting any other components properties.<para></para> 
    /// Can be ordered to ignore a certain Transform vector. By default, none is ignored. Must be used inside a running Coroutine with the "yield return" instruction. <para></para><para/>
    /// If you're trying to reinstantiate a SceneObject while its Instance has been destroyed, the function will Instantiate it again. <para></para><para/>
    /// However, in this case, if you set transformationVectors to TransformationVectors.LeaveAsIs, it will use TransformationVectors.TakeFromPrefab. <para></para><para/>
    /// </summary>
    public static IEnumerator Reinstantiate(this SceneObject sceneObject, TransformationVectors transformationVectors = TransformationVectors.LeaveAsIs, IgnoreVector ignoreVector = IgnoreVector.None)
    {
        if (sceneObject.Instance != null)
        {
            Transform originalParent = sceneObject.transform.parent;
            Transformation originalTransformation = sceneObject.transform.GetGlobalTransformation();

            if (transformationVectors == TransformationVectors.TakeFromPrefab)
            {
                originalTransformation = sceneObject.Prefab.transform.GetGlobalTransformation();
            }
            else if (transformationVectors == TransformationVectors.Stored)
            {
                originalTransformation = sceneObject.StoredTransformation;
            }

            MonoBehaviour.Destroy(sceneObject.Instance);
            yield return null;
            sceneObject.Instance = MonoBehaviour.Instantiate(sceneObject.Prefab, originalParent);
            sceneObject.transform.SetGlobalTransformation(originalTransformation, ignoreVector);
        }
        else
        {
            sceneObject.Instance = MonoBehaviour.Instantiate(sceneObject.Prefab, sceneObject.StoredParent);
            Transformation originalTransformation = sceneObject.Prefab.transform.GetGlobalTransformation();

            if (transformationVectors == TransformationVectors.Stored)
            {
                originalTransformation = sceneObject.StoredTransformation;
            }

            sceneObject.transform.SetGlobalTransformation(sceneObject.StoredTransformation, ignoreVector);
        }
    }

    #endregion

    #region GameObjects and SceneObjects - Object Pooling

    /// <summary>
    /// Picks an object from an object pool. Extends from a list of SceneObjects and requires a Prefab GameObject
    /// </summary>
    public static SceneObject PickObjectFromPool(this List<SceneObject> objectPool, GameObject prefab, Transform parent = null)
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeSelf)
            {
                objectPool[i].SetActive(true);
                return objectPool[i];
            }
        }

        SceneObject newObject = new SceneObject(MonoBehaviour.Instantiate(prefab, parent), prefab);
        objectPool.Add(newObject);
        return newObject;
    }



    /// <summary>
    /// Picks an object from an object pool. Extends from a list of SceneObjects and requires a Prefab GameObject, position and rotation
    /// </summary>
    public static SceneObject PickObjectFromPool(this List<SceneObject> objectPool, GameObject prefab, Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeSelf)
            {
                objectPool[i].SetActive(true);
                return objectPool[i];
            }
        }

        SceneObject newObject = new SceneObject(MonoBehaviour.Instantiate(prefab, position, rotation), prefab);
        objectPool.Add(newObject);
        return newObject;
    }



    /// <summary>
    /// Picks an object from an object pool. Extends from a list of GameObjects and requires a Prefab GameObject
    /// </summary>
    public static GameObject PickObjectFromPool(this List<GameObject> objectPool, GameObject prefab, Transform parent = null)
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeSelf)
            {
                objectPool[i].SetActive(true);
                return objectPool[i];
            }
        }

        GameObject newObject = MonoBehaviour.Instantiate(prefab, parent);
        objectPool.Add(newObject);
        return newObject;
    }



    /// <summary>
    /// Picks an object from an object pool. Extends from a list of GameObjects and requires a Prefab GameObject, position and rotation
    /// </summary>
    public static GameObject PickObjectFromPool(this List<GameObject> objectPool, GameObject prefab, Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeSelf)
            {
                objectPool[i].SetActive(true);
                return objectPool[i];
            }
        }

        GameObject newObject = MonoBehaviour.Instantiate(prefab, position, rotation);
        objectPool.Add(newObject);
        return newObject;
    }



    /// <summary>
    /// Picks an object from an object pool. Requires the object to implement the IPoolObjectable interface, and to have a Prefab GameObject
    /// </summary>
    public static T PickObjectFromPool<T>(this List<T> objectPool, GameObject prefab, Vector3 position, Quaternion rotation) where T : IObjectPoolable
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].IsActive)
            {
                objectPool[i].IsActive = true;
                return objectPool[i];
            }
        }

        T newObject = MonoBehaviour.Instantiate(prefab, position, rotation).GetComponent<T>();
        objectPool.Add(newObject);
        return newObject;
    }



    /// <summary>
    /// Picks an object from an object pool. Requires the object to implement the IPoolObjectable interface, and to have a Prefab GameObject
    /// </summary>
    public static T PickObjectFromPool<T>(this List<T> objectPool, GameObject prefab, Transform parent = null) where T : IObjectPoolable
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].IsActive)
            {
                objectPool[i].IsActive = true;
                return objectPool[i];
            }
        }

        T newObject = MonoBehaviour.Instantiate(prefab, parent).GetComponent<T>();
        objectPool.Add(newObject);
        return newObject;
    }

    #endregion

    #region GameObjects and SceneObjects - Other

    /// <summary>
    /// Does this list of SceneObjects contain a certain GameObject?
    /// </summary>
    public static bool ContainsObject(this List<SceneObject> sceneObjects, GameObject gameObject)
    {
        return sceneObjects.Find(x => x.Instance == gameObject) != null;
    }


    /// <summary>
    /// Activates/Deactivates a gameObject within a delay. The class the function is running on MUST derive from MonoBehaviourEx, and if the class <para></para>
    /// IMPORTANT: If the class you're calling the timer from, implemets the Update() function, it MUST override the base.Update() function, otherwise timers won't be updated
    /// </summary>
    public static void SetActive(this GameObject gameObject, bool active, float delay)
    {
        monoBehaviourEx = gameObject.GetComponent<MonoBehaviourEx>();

        if (monoBehaviourEx != null)
        {
            monoBehaviourEx.StartTimer(delay, () => gameObject.SetActive(active));
        }
    }

    #endregion

    #endregion

    #region Activation and Enabling

    public static void SetActiveIfExists(this GameObject gameObject, bool active)
    {
        if (gameObject != null)
            gameObject.SetActive(active);
    }



    public static void SetEnabledIfExists(this Behaviour behaviour, bool enabled)
    {
        if (behaviour != null)
            behaviour.enabled = enabled;
    }



    public static void SetEnabledIfExists(this MonoBehaviour monoBehaviour, bool enabled)
    {
        if (monoBehaviour != null)
            monoBehaviour.enabled = enabled;
    }



    public static void SetEnabledIfExists(this Collider collider, bool enabled)
    {
        if (collider != null)
            collider.enabled = enabled;
    }



    public static void SetEnabledIfExists(this Renderer renderer, bool enabled)
    {
        if (renderer != null)
            renderer.enabled = enabled;
    }

    #endregion

    #region Meshes

    /// <summary>
    /// Suppose you have a list of meshes. Is at least one of them visible by any camera?
    /// </summary>
    public static bool IsAnyMeshPartVisible(this List<SkinnedMeshRenderer> meshes)
    {
        foreach (SkinnedMeshRenderer mesh in meshes)
        {
            if (mesh.isVisible)
                return true;
        }

        return false;
    }


    /// <summary>
    /// Suppose you have a list of meshes. Is at least one of them visible by any camera?
    /// </summary>
    public static bool IsAnyMeshPartVisible(this List<MeshRenderer> meshes)
    {
        foreach (MeshRenderer mesh in meshes)
        {
            if (mesh.isVisible)
                return true;
        }

        return false;
    }

    #endregion

    #region Collision

    /// <summary>
    /// Returns whether the collision contact point on the given collider is below the center of the collider, and not too far away on X and Z axes.<para></para>
    /// Most useful in games where you want to check whether your character has fallen from height or collider with an air object while still falling. <para></para>
    /// Use for Sphere Colliders
    /// </summary>
    public static bool IsCollisionPotentiallyFatal(this SphereCollider collider, Collision collision)
    {
        Vector3 contactPoint = collision.contacts[0].point;
        Transform transform = collider.transform;
        Vector3Bool conditions = Vector3Bool.AllTrue;
        conditions.X = Mathf.Abs(contactPoint.x - transform.position.x) < collider.radius * transform.localScale.x;
        conditions.Y = contactPoint.y < transform.position.y;
        conditions.Z = Mathf.Abs(contactPoint.z - transform.position.z) < collider.radius * transform.localScale.z;
        return conditions.IsAllTrue;
    }


    /// <summary>
    /// Returns whether the collision contact point on the given collider is below the center of the collider, and not too far away on X and Z axes.<para></para>
    /// Most useful in games where you want to check whether your character has fallen from height or collider with an air object while still falling. <para></para>
    /// Use for Capsule Colliders
    /// </summary>
    public static bool IsCollisionPotentiallyFatal(this CapsuleCollider collider, Collision collision)
    {
        Vector3 contactPoint = collision.contacts[0].point;
        Transform transform = collider.transform;
        Vector3Bool conditions = Vector3Bool.AllTrue;
        conditions.X = Mathf.Abs(contactPoint.x - transform.position.x) < collider.radius * transform.localScale.x;
        conditions.Y = contactPoint.y < transform.position.y;
        conditions.Z = Mathf.Abs(contactPoint.z - transform.position.z) < collider.radius * transform.localScale.z;
        return conditions.IsAllTrue;
    }


    /// <summary>
    /// Returns whether the collision contact point on the given collider is below the center of the collider, and not too far away on X and Z axes.<para></para>
    /// Most useful in games where you want to check whether your character has fallen from height or collider with an air object while still falling. <para></para>
    /// Use for Box Colliders
    /// </summary>
    public static bool IsCollisionPotentiallyFatal(this BoxCollider collider, Collision collision)
    {
        Vector3 contactPoint = collision.contacts[0].point;
        Transform transform = collider.transform;
        Vector3Bool conditions = Vector3Bool.AllTrue;
        conditions.X = Mathf.Abs(contactPoint.x - transform.position.x) < Vector3.Distance(collider.ClosestPoint(contactPoint), transform.position);
        conditions.Y = contactPoint.y < transform.position.y;
        conditions.Z = Mathf.Abs(contactPoint.z - transform.position.z) < Vector3.Distance(collider.ClosestPoint(contactPoint), transform.position);
        return conditions.IsAllTrue;
    }


    /// <summary>
    /// Returns whether the collision contact point on the given collider is below the center of the collider, and not too far away on X and Z axes.<para></para>
    /// Most useful in games where you want to check whether your character has fallen from height or collider with an air object while still falling. <para></para>
    /// Use for Mesh Colliders
    /// </summary>
    public static bool IsCollisionPotentiallyFatal(this MeshCollider collider, Collision collision)
    {
        Vector3 contactPoint = collision.contacts[0].point;
        Transform transform = collider.transform;
        Vector3Bool conditions = Vector3Bool.AllTrue;
        conditions.X = Mathf.Abs(contactPoint.x - transform.position.x) < Vector3.Distance(collider.ClosestPoint(contactPoint), transform.position);
        conditions.Y = contactPoint.y < transform.position.y;
        conditions.Z = Mathf.Abs(contactPoint.z - transform.position.z) < Vector3.Distance(collider.ClosestPoint(contactPoint), transform.position);
        return conditions.IsAllTrue;
    }


    /// <summary>
    /// Returns whether the collision contact point on the given collider is below the center of the collider, and not too far away on X and Z axes.<para></para>
    /// Most useful in games where you want to check whether your character has fallen from height or collider with an air object while still falling. <para></para>
    /// Use for All Colliders
    /// </summary>
    public static bool IsCollisionPotentiallyFatal(this TerrainCollider collider, Collision collision)
    {
        Vector3 contactPoint = collision.contacts[0].point;
        Transform transform = collider.transform;
        Vector3Bool conditions = Vector3Bool.AllTrue;
        conditions.X = Mathf.Abs(contactPoint.x - transform.position.x) < Vector3.Distance(collider.ClosestPoint(contactPoint), transform.position);
        conditions.Y = contactPoint.y < transform.position.y;
        conditions.Z = Mathf.Abs(contactPoint.z - transform.position.z) < Vector3.Distance(collider.ClosestPoint(contactPoint), transform.position);
        return conditions.IsAllTrue;
    }

    #endregion
    
    #region Button Navigation
    
    static Navigation navigation;

    public static void SetNavigationMode(this Button button, Navigation.Mode navMode)
    {
        navigation = button.navigation;
        navigation.mode = navMode;
        button.navigation = navigation;
    }



    /// <summary>
    /// A shortcut for navigation.selectOnUp
    /// </summary>
    public static void NavigateUpTo(this Selectable origin, Selectable target)
    {
        navigation = origin.navigation;
        navigation.mode = Navigation.Mode.Explicit;
        navigation.selectOnUp = target;
        origin.navigation = navigation;
    }



    /// <summary>
    /// A shortcut for navigation.selectOnUp
    /// </summary>
    public static void NavigateDownTo(this Selectable origin, Selectable target)
    {
        navigation = origin.navigation;
        navigation.mode = Navigation.Mode.Explicit;
        navigation.selectOnDown = target;
        origin.navigation = navigation;
    }



    /// <summary>
    /// A shortcut for navigation.selectOnUp
    /// </summary>
    public static void NavigateRightTo(this Selectable origin, Selectable target)
    {
        navigation = origin.navigation;
        navigation.mode = Navigation.Mode.Explicit;
        navigation.selectOnRight = target;
        origin.navigation = navigation;
    }



    /// <summary>
    /// A shortcut for navigation.selectOnUp
    /// </summary>
    public static void NavigateLeftTo(this Selectable origin, Selectable target)
    {
        navigation = origin.navigation;
        navigation.mode = Navigation.Mode.Explicit;
        navigation.selectOnLeft = target;
        origin.navigation = navigation;
    }



    /// <summary>
    /// Create a mutual explicit navigation bond between two Selectables. The first Selectable is the upper one, the second is the lower
    /// </summary>
    public static void CreateMutualVerticalNavigation(Selectable upper, Selectable lower)
    {
        lower.NavigateUpTo(upper);
        upper.NavigateDownTo(lower);
    }



    /// <summary>
    /// Create a mutual explicit navigation bond between two Selectables. The first Selectable is the left one, the second is the right
    /// </summary>
    public static void CreateMutualHorizontalNavigation(Selectable left, Selectable right)
    {
        left.NavigateRightTo(right);
        right.NavigateLeftTo(left);
    }

    #endregion

    #region Convertions
    
    /// <summary>
    /// Creates a uniform Vector3 out of the specified float value
    /// </summary>
    public static Vector3 ToVector3(this float value)
    {
        return new Vector3(value, value, value);
    }


    /// <summary>
    /// Creates a uniform Vector2 out of the specified float value
    /// </summary>
    public static Vector2 ToVector2(this float value)
    {
        return new Vector2(value, value);
    }


    /// <summary>
    /// Creates a uniform Color out of the specified float value
    /// </summary>
    public static Color ToColor(this float value)
    {
        return new Color(value, value, value, value);
    }


    /// <summary>
    /// Creates a uniform Opaque Color out of the specified float value
    /// </summary>
    public static Color ToOpaqueColor(this float value)
    {
        return new Color(value, value, value, 1);
    }



    /// <summary>
    /// Creates a uniform Vector3Bool out of the specified bool value
    /// </summary>
    public static Vector3Bool ToVector3Bool(this bool value)
    {
        return new Vector3Bool(value, value, value);
    }



    /// <summary>
    /// Creates a uniform Vector2Bool out of the specified bool value
    /// </summary>
    public static Vector2Bool ToVector2Bool(this bool value)
    {
        return new Vector2Bool(value, value);
    }



    /// <summary>
    /// Returns a ClampedFloat of a float value, by setting min and max values by offset. For exammple, a value of 1 with an offset of 0.1, will return a ClampedFloat with 1.1 as max value, and 0.9 as min value. Useful to set up closest values to an original value
    /// </summary>
    public static ClampedFloat GenerateLimitsByOffset(this float OriginalValue, float Offset)
    {
        return new ClampedFloat(OriginalValue - Offset, OriginalValue + Offset);
    }



    /// <summary>
    /// Returns a ClampedInt of a int value, by setting min and max values by offset. For exammple, a value of 10 with an offset of 1, will return a ClampedInt with 11 as max value, and 9 as min value. Useful to set up closest values to an original value
    /// </summary>
    public static ClampedInt GenerateLimitsByOffset(this int OriginalValue, int Offset)
    {
        return new ClampedInt(OriginalValue - Offset, OriginalValue + Offset);
    }

    #endregion

    #region FullStrings

    /// <summary>
    /// Returns "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz0123456789"
    /// </summary>
    public static string AllChars
    {
        get
        {
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz0123456789";
        }
    }


    /// <summary>
    /// Returns "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz"
    /// </summary>
    public static string EnglishAlphabet
    {
        get
        {
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz";
        }
    }


    /// <summary>
    /// Returns "abcdefghijklmnopqrstuvwxyz "
    /// </summary>
    public static string EnglishAlphabetLowerCase
    {
        get
        {
            return "abcdefghijklmnopqrstuvwxyz ";
        }
    }


    /// <summary>
    /// Returns "abcdefghijklmnopqrstuvwxyz "
    /// </summary>
    public static string EnglishAlphabetUpperCase
    {
        get
        {
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
        }
    }



    /// <summary>
    /// Returns "0123456789"
    /// </summary>
    public static string AllDigits
    {
        get
        {
            return "0123456789";
        }
    }

    #endregion

    #region Input
    
    public static bool IsJoystickBackButtonPressed
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Joystick2Button2)
                || Input.GetKeyDown(KeyCode.Joystick3Button2) || Input.GetKeyDown(KeyCode.Joystick4Button2);
        }
    }



    public static bool IsJoystickViewButtonPressed
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Joystick1Button6) || Input.GetKeyDown(KeyCode.Joystick2Button6)
                || Input.GetKeyDown(KeyCode.Joystick3Button6) || Input.GetKeyDown(KeyCode.Joystick4Button6);
        }
    }

    #endregion
}

#region ClampedInt

/// <summary>
/// A limited int with minimum and maximum values. Useful to store range of int numbers or define closest min/max values by offset 
/// </summary>
[System.Serializable]
public class ClampedInt
{
	public int Min;
	public int Max;

    /// <summary>
    /// A limited int with minimum and maximum values. Useful to store range of int numbers or define closest min/max values by offset 
    /// </summary>
    public ClampedInt (int min, int max)
	{
        Min = min;
        Max = max;
	}

    /// <summary>
    /// Returns whether the value is bigger than Min and smaller than Max
    /// </summary>
    public bool ContainsValue(int Value)
	{
		return ((Value > Min) && (Value < Max));
	}

    /// <summary>
    /// Verifies that the Max value is bigger or equal to the Min value. If not, switches between them
    /// </summary>
    public ClampedInt Verified
	{
		get 
		{
			if (Max < Min) 
			{
				return SwitchValues ();
			}

			return this;
		}
    }

    /// <summary>
    /// Returns the average between the Min and Max values
    /// </summary>
    public float Average
    {
        get
        {
            return (Max + Min) / 2;
        }
    }
    
    public ClampedFloat IncreasedBy(float value)
    {
        return new ClampedFloat(Min + value, Max + value);
    }

    public ClampedFloat DecreasedBy(float value)
    {
        return new ClampedFloat(Min - value, Max - value);
    }

    public ClampedFloat MultipliedBy(float multiplier)
    {
        return new ClampedFloat(Min * multiplier, Max * multiplier);
    }

    public ClampedFloat DividedBy(float multiplier)
    {
        return new ClampedFloat(Min / multiplier, Max / multiplier);
    }

    /// <summary>
    /// Returns a random value between Min and Max values
    /// </summary>
    public int RandomValue
	{
        get
        {
            return Random.Range(Min, Max + 1);
        }
	}

    /// <summary>
    /// Returns a random value which can only be Min or Max
    /// </summary>
    public int RandomEndValue
    {
        get
        {
            int index = Random.Range(0, 2);
            return (index == 0) ? Min : Max;
        }
    }

    /// <summary>
    /// Returns the range between Max and Min values (Max - Min)
    /// </summary>
    public int Range
    {
        get
        {
            return Mathf.Abs(Max - Min);
        }
    }

    public static ClampedInt SwitchValues(ClampedInt clampedFloat)
	{
		return new ClampedInt (clampedFloat.Max, clampedFloat.Min);
	}

	public ClampedInt SwitchValues()
	{
		return new ClampedInt (Max, Min);
	}

    /// <summary>
    /// Receives a int value and returns its place/percent between the Min and Max values. For example, if the Min is 100, the Max is 200, and the received value is 120, the function will return 0.2
    /// </summary>
    public int GetClampedPercentOfValue(int Value)
	{
		/// Trims both Min and Max values such a way, that Min becomes equal to 0
		int Range = Max - Min;

		/// Trims the part of the value which is in range
		int TrimmedValue = Value - Min;

		/// If the TrimmedValue is below 0, the function returns 0
		if (TrimmedValue < 0)
			return 0;

		/// If the TrimmedValue is higher than the range, the function returns 1
		if (TrimmedValue > Range)
			return 1;

		return TrimmedValue / Range;
	}

    /// <summary>
    /// Receives a float percent (value between 0 and 1) and returns the value that matches the percent from the range between Min and Max value. For example, if the Min is 100, the Max is 200, and the requested percent is 0.2, the function will return 120
    /// </summary>
    public float GetClampedValueByPercent(float Percent)
	{
		if (Percent <= 0)
			return Min;
		else if (Percent >= 1)
			return Max;

		/// Trims both Min and Max values such a way, that Min becomes equal to 0
		int Range = Max - Min;

		return Min + Mathf.RoundToInt(Range * Percent);
    }

    /// <summary>
    /// Use it when you want to print the full debug of this ClampedInt
    /// </summary>
    public string FullDebug
    {
        get
        {
            string isVerified = (Max > Min) ? "" :
                " ====> WARNING: Min is bigger than Max. Use the .Verified property whenever you use this ClampedFloat, " +
                "or change the Min and Max values";
            return "Min = " + Min.ToString() + " / Max = " + Max.ToString() + isVerified;
        }
    }
}

#endregion

#region ClampedFloat

/// <summary>
/// A limited float with minimum and maximum values. Useful to store axis limits or define closest min/max values by offset 
/// </summary>
[System.Serializable]
public class ClampedFloat
{
	public float Min;
	public float Max;

    /// <summary>
    /// A limited float with minimum and maximum values. Useful to store axis limits or define closest min/max values by offset 
    /// </summary>
    public ClampedFloat (float min, float max)
	{
		Min = min;
		Max = max;
	}

    /// <summary>
    /// Returns whether the value is bigger than Min and smaller than Max
    /// </summary>
    public bool ContainsValue(float Value)
	{
		return ((Value > Min) && (Value < Max));
	}

    /// <summary>
    /// Verifies that the Max value is bigger or equal to the Min value. If not, switches between them
    /// </summary>
    public ClampedFloat Verified
	{
		get 
		{
			if (Max < Min) 
			{
				return SwitchValues ();
			}

			return this;
		}
	}

    /// <summary>
    /// Returns the average between the Min and Max values
    /// </summary>
    public float Average
	{
		get 
		{
			return (Max + Min) / 2;
		}
    }

    public ClampedFloat IncreasedBy(float value)
    {
        return new ClampedFloat(Min + value, Max + value);
    }

    public ClampedFloat DecreasedBy(float value)
    {
        return new ClampedFloat(Min - value, Max - value);
    }

    public ClampedFloat MultipliedBy(float multiplier)
    {
        return new ClampedFloat(Min * multiplier, Max * multiplier);
    }

    public ClampedFloat DividedBy(float multiplier)
    {
        return new ClampedFloat(Min / multiplier, Max / multiplier);
    }

    /// <summary>
    /// Returns a random value between Min and Max values
    /// </summary>
    public float RandomValue
    {
        get
        {
            return Random.Range(Min, Max);
        }
    }

    /// <summary>
    /// Returns a random value which can only be Min or Max
    /// </summary>
    public float RandomEndValue
    {
        get
        {
            int index = Random.Range(0, 2);
            return (index == 0) ? Min : Max;
        }
    }

    /// <summary>
    /// Returns the range between Max and Min values (Max - Min)
    /// </summary>
    public float Range
    {
        get
        {
            return Mathf.Abs(Max - Min);
        }
    }

    public static ClampedFloat SwitchValues(ClampedFloat clampedFloat)
	{
		return new ClampedFloat (clampedFloat.Max, clampedFloat.Min);
	}

	public ClampedFloat SwitchValues()
	{
		return new ClampedFloat (Max, Min);
	}

    /// <summary>
    /// Receives a float value and returns its place/percent between the Min and Max values. For example, if the Min is 100, the Max is 200, and the received value is 120, the function will return 0.2
    /// </summary>
    public float GetClampedPercentOfValue(float Value)
	{
		/// Trims both Min and Max values such a way, that Min becomes equal to 0
		float Range = Max - Min;

		/// Trims the part of the value which is in range
		float TrimmedValue = Value - Min;

		/// If the TrimmedValue is below 0, the function returns 0
		if (TrimmedValue < 0)
			return 0;

		/// If the TrimmedValue is higher than the range, the function returns 1
		if (TrimmedValue > Range)
			return 1;

		return TrimmedValue / Range;
	}

    /// <summary>
    /// Receives a float percent (value between 0 and 1) and returns the value that matches the percent from the range between Min and Max value. For example, if the Min is 100, the Max is 200, and the requested percent is 0.2, the function will return 120
    /// </summary>
    public float GetClampedValueByPercent(float Percent)
	{
		if (Percent <= 0)
			return Min;
		else if (Percent >= 1)
			return Max;

		/// Trims both Min and Max values such a way, that Min becomes equal to 0
		float Range = Max - Min;

		return Min + Range * Percent;
	}

    /// <summary>
    /// Use it when you want to print the full debug of this ClampedFloat
    /// </summary>
    public string FullDebug
    {
        get
        {
            string isVerified = (Max > Min) ? "" : 
                " ====> WARNING: Min is bigger than Max. Use the .Verified property whenever you use this ClampedFloat, " +
                "or change the Min and Max values";
            return "Min = " + Min.ToString() + " / Max = " + Max.ToString() + isVerified;
        }
    }
}

#endregion

#region Vector2Range

/// <summary>
/// Defines two Vectors of Vector2 and allows various manipulations with their range
/// </summary>
[System.Serializable]
public class Vector2Range
{
    public Vector2 First;
    public Vector2 Second;

    /// <summary>
    /// Defines two Vectors of Vector2 and allows various manipulations with their range
    /// </summary>
    public Vector2Range(Vector2 first, Vector2 second)
    {
        First = first;
        Second = second;
    }

    /// <summary>
    /// Returns the distance between the two vectors of this Vector2Range
    /// </summary>
    public float Distance
    {
        get
        {
            return Vector2.Distance(First, Second);
        }
    }

    /// <summary>
    /// Returns the distance between the two vectors of this Vector2Range on X axis
    /// </summary>
    public float DistanceX
    {
        get
        {
            return Mathf.Abs(First.x - Second.x);
        }
    }

    /// <summary>
    /// Returns the distance between the two vectors of this Vector2Range on Y axis
    /// </summary>
    public float DistanceY
    {
        get
        {
            return Mathf.Abs(First.y - Second.y);
        }
    }

    /// <summary>
    /// Are both Vectors in the Vector2Range equal?
    /// </summary>
    public bool IsEqual
    {
        get
        {
            return First == Second;
        }
    }

    /// <summary>
    /// Returns a Vector2Bool which tells which axes both vectors are equal on
    /// </summary>
    public Vector2Bool EqualOnAxes
    {
        get
        {
            return new Vector2Bool(First.x == Second.x, First.y == Second.y);
        }
    }

    /// <summary>
    /// Returns whether this Vector2Range contains the given Vector2
    /// </summary>
    public bool ContainsVector(Vector2 vector2)
    {
        ClampedFloat xRange = new ClampedFloat(First.x, Second.x);
        ClampedFloat yRange = new ClampedFloat(First.y, Second.y);
        return xRange.ContainsValue(vector2.x) && xRange.ContainsValue(vector2.y);
    }

    /// <summary>
    /// Returns whether the specified Vector2 is equal to the Vector2Range Min or Max values
    /// </summary>
    public bool ContainsVectorAsEndValue(Vector2 vector)
    {
        return (vector == First) || (vector == Second);
    }

    /// <summary>
    /// Returns Vector2.Angle(First, Second);
    /// </summary>
    public float AngleFromFirst
    {
        get
        {
            return Vector2.Angle(First, Second);
        }
    }

    /// <summary>
    /// Returns Vector2.Angle(Second, First);
    /// </summary>
    public float AngleFromSecond
    {
        get
        {
            return Vector2.Angle(Second, First);
        }
    }

    /// <summary>
    /// Dot product of the two vectors of this Vector2Range
    /// </summary>
    public float Dot
    {
        get
        {
            return Vector2.Dot(First, Second);
        }
    }

    /// <summary>
    /// Returns Vector2.Min(First, Second);
    /// </summary>
    public Vector2 Min
    {
        get
        {
            return Vector2.Min(First, Second);
        }
    }

    /// <summary>
    /// Returns Vector2.Min(First, Second);
    /// </summary>
    public Vector2 Max
    {
        get
        {
            return Vector2.Max(First, Second);
        }
    }

    /// <summary>
    /// Multiplies the two vectors of this Vector2Range components wise
    /// </summary>
    public Vector2 Scale
    {
        get
        {
            return Vector2.Scale(First, Second);
        }
    }

    /// <summary>
    /// Swaps two vectors
    /// </summary>
    public static Vector2Range SwitchValues(Vector2Range vector2Range)
    {
        return new Vector2Range(vector2Range.Second, vector2Range.First);
    }

    /// <summary>
    /// Swaps both vectors of this Vector2Range
    /// </summary>
    public Vector2Range SwitchValues()
    {
        return new Vector2Range(Second, First);
    }

    /// <summary>
    /// Returns a Vector2Range with both vectors as Vector2.zero
    /// </summary>
    public static Vector2Range Zero
    {
        get
        {
            return new Vector2Range(Vector2.zero, Vector2.zero);
        }
    }

    /// <summary>
    /// Returns a Vector2Range with both vectors as Vector2.one
    /// </summary>
    public static Vector2Range One
    {
        get
        {
            return new Vector2Range(Vector2.one, Vector2.one);
        }
    }

    /// <summary>
    /// Returns a Vector2Range where First vector is Vector2.zero and Second vector is Vector2.one
    /// </summary>
    public static Vector2Range ZeroOne
    {
        get
        {
            return new Vector2Range(Vector2.zero, Vector2.one);
        }
    }

    /// <summary>
    /// Returns a Vector2Range where First vector is Vector2.one and Second vector is Vector2.zero
    /// </summary>
    public static Vector2Range OneZero
    {
        get
        {
            return new Vector2Range(Vector2.one, Vector2.zero);
        }
    }

    /// <summary>
    /// Use it when you want to print the full debug of this Vector2Range
    /// </summary>
    public string FullDebug
    {
        get
        {
            return "First Vector2 = " + First.ToString() + " / Second Vector2 = " + Second.ToString();
        }
    }
}

#endregion

#region Vector3Range

/// <summary>
/// Defines two Vectors of Vector3 and allows various manipulations with their range
/// </summary>
[System.Serializable]
public class Vector3Range
{
    public Vector3 First;
    public Vector3 Second;

    /// <summary>
    /// Defines two Vectors of Vector3 and allows various manipulations with their range
    /// </summary>
    public Vector3Range(Vector3 first, Vector3 second)
    {
        First = first;
        Second = second;
    }

    /// <summary>
    /// Returns the distance between the two vectors of this Vector3Range
    /// </summary>
    public float Distance
    {
        get
        {
            return Vector3.Distance(First, Second);
        }
    }

    /// <summary>
    /// Returns the distance between the two vectors of this Vector3Range on X axis
    /// </summary>
    public float DistanceX
    {
        get
        {
            return Mathf.Abs(First.x - Second.x);
        }
    }

    /// <summary>
    /// Returns the distance between the two vectors of this Vector3Range on Y axis
    /// </summary>
    public float DistanceY
    {
        get
        {
            return Mathf.Abs(First.y - Second.y);
        }
    }

    /// <summary>
    /// Returns the distance between the two vectors of this Vector3Range on Z axis
    /// </summary>
    public float DistanceZ
    {
        get
        {
            return Mathf.Abs(First.z - Second.z);
        }
    }

    /// <summary>
    /// Are both Vectors in the Vector3Range equal?
    /// </summary>
    public bool IsEqual
    {
        get
        {
            return First == Second;
        }
    }

    /// <summary>
    /// Returns a Vector3Bool which tells which axes both vectors are equal on
    /// </summary>
    public Vector3Bool EqualOnAxes
    {
        get
        {
            return new Vector3Bool(First.x == Second.x, First.y == Second.y, First.z == Second.z);
        }
    }

    /// <summary>
    /// Returns whether this Vector3Range contains the given Vector3
    /// </summary>
    public bool ContainsVector(Vector3 vector3)
    {
        ClampedFloat xRange = new ClampedFloat(First.x, Second.x);
        ClampedFloat yRange = new ClampedFloat(First.y, Second.y);
        ClampedFloat zRange = new ClampedFloat(First.z, Second.z);
        return xRange.ContainsValue(vector3.x) || xRange.ContainsValue(vector3.y) || xRange.ContainsValue(vector3.z);
    }

    /// <summary>
    /// Returns whether the specified Vector3 is equal to the Vector3Range Min or Max values
    /// </summary>
    public bool ContainsVectorAsEndValue(Vector3 vector)
    {
        return (vector == First) || (vector == Second);
    }

    /// <summary>
    /// Returns Vector3.Angle(First, Second);
    /// </summary>
    public float AngleFromFirst
    {
        get
        {
            return Vector3.Angle(First, Second);
        }
    }

    /// <summary>
    /// Returns Second.AngleTo(First);
    /// </summary>
    //public float AngleToFirst
    //{
    //    get
    //    {
    //        return Second.AngleTo(First);
    //    }
    //}

    /// <summary>
    /// Returns Vector3.Angle(Second, First);
    /// </summary>
    public float AngleFromSecond
    {
        get
        {
            return Vector3.Angle(Second, First);
        }
    }

    /// <summary>
    /// Returns First.AngleTo(Second);
    /// </summary>
    //public float AngleToSecond
    //{
    //    get
    //    {
    //        return First.AngleTo(Second);
    //    }
    //}

    /// <summary>
    /// Returns Second.DirectionToPoint(First);
    /// </summary>
    public Vector3 DirectionToFirst
    {
        get
        {
            return Second.DirectionToPoint(First);
        }
    }

    /// <summary>
    /// Returns First.DirectionToPoint(Second);
    /// </summary>
    public Vector3 DirectionToSecond
    {
        get
        {
            return First.DirectionToPoint(Second);
        }
    }

    /// <summary>
    /// Cross product of the two vectors of this Vector3Range
    /// </summary>
    public Vector3 Cross
    {
        get
        {
            return Vector3.Cross(First, Second);
        }
    }

    /// <summary>
    /// Dot product of the two vectors of this Vector3Range
    /// </summary>
    public float Dot
    {
        get
        {
            return Vector3.Dot(First, Second);
        }
    }

    /// <summary>
    /// Returns Vector3.Min(First, Second);
    /// </summary>
    public Vector3 Min
    {
        get
        {
            return Vector3.Min(First, Second);
        }
    }

    /// <summary>
    /// Returns Vector3.Min(First, Second);
    /// </summary>
    public Vector3 Max
    {
        get
        {
            return Vector3.Max(First, Second);
        }
    }

    /// <summary>
    /// Multiplies the two vectors of this Vector3Range components wise
    /// </summary>
    public Vector3 Scale
    {
        get
        {
            return Vector3.Scale(First, Second);
        }
    }

    /// <summary>
    /// Swaps two vectors
    /// </summary>
    public static Vector3Range SwitchValues(Vector3Range vector3Range)
    {
        return new Vector3Range(vector3Range.Second, vector3Range.First);
    }

    /// <summary>
    /// Swaps both vectors of this Vector3Range
    /// </summary>
    public Vector3Range SwitchValues()
    {
        return new Vector3Range(Second, First);
    }

    /// <summary>
    /// Returns a Vector3Range with both vectors as Vector3.zero
    /// </summary>
    public static Vector3Range Zero
    {
        get
        {
            return new Vector3Range(Vector3.zero, Vector3.zero);
        }
    }

    /// <summary>
    /// Returns a Vector3Range with both vectors as Vector3.one
    /// </summary>
    public static Vector3Range One
    {
        get
        {
            return new Vector3Range(Vector3.one, Vector3.one);
        }
    }

    /// <summary>
    /// Returns a Vector3Range where First vector is Vector3.zero and Second vector is Vector3.one
    /// </summary>
    public static Vector3Range ZeroOne
    {
        get
        {
            return new Vector3Range(Vector3.zero, Vector3.one);
        }
    }

    /// <summary>
    /// Returns a Vector3Range where First vector is Vector3.one and Second vector is Vector3.zero
    /// </summary>
    public static Vector3Range OneZero
    {
        get
        {
            return new Vector3Range(Vector3.one, Vector3.zero);
        }
    }

    /// <summary>
    /// Use it when you want to print the full debug of this Vector3Range
    /// </summary>
    public string FullDebug
    {
        get
        {
            return "First Vector3 = " + First.ToString() + " / Second Vector3 = " + Second.ToString();
        }
    }
}

#endregion

#region TransformationRange

/// <summary>
/// Defines two Transformations and allows various manipulations with their range
/// </summary>
[System.Serializable]
public class TransformationsRange
{
    public Transformation First;
    public Transformation Second;

    /// <summary>
    /// Defines two Transformations and allows various manipulations with their range
    /// </summary>
    public TransformationsRange(Transformation first, Transformation second)
    {
        First = first;
        Second = second;
    }

    /// <summary>
    /// Are both Transformations in the TransformationsRange equal?
    /// </summary>
    public bool IsEqual
    {
        get
        {
            return First == Second;
        }
    }

    /// <summary>
    /// Swaps two Transformations
    /// </summary>
    public static TransformationsRange SwitchValues(TransformationsRange transformationsRange)
    {
        return new TransformationsRange(transformationsRange.Second, transformationsRange.First);
    }

    /// <summary>
    /// Swaps both vectors of this TransformationsRange
    /// </summary>
    public TransformationsRange SwitchValues()
    {
        return new TransformationsRange(Second, First);
    }

    /// <summary>
    /// Use it when you want to print the full debug of this TransformationsRange
    /// </summary>
    public string FullDebug
    {
        get
        {
            return "First = " + First.FullDebug + " / Second = " + Second.FullDebug;
        }
    }
}

#endregion

#region Transform with Vector3Range

/// <summary>
/// A convenient way to tie a Transform to a Vector3Range, allowing various manipulations with them
/// </summary>
[System.Serializable]
public class TransformWithVector3Range
{
    public string Name;
    public Transform StoredTransform;
    public VectorType VectorType;
    public Vector3Range StoredVector3Range;

    /// <summary>
    /// A convenient way to tie a Transform to a Vector3Range, allowing various manipulations with them
    /// </summary>
    public TransformWithVector3Range(string name, Transform storedTransform, VectorType vectorType, Vector3Range storedVector3Range)
    {
        Name = name;
        StoredTransform = storedTransform;
        VectorType = vectorType;
        StoredVector3Range = storedVector3Range;
    }

    Dictionary<VectorType, VectorFunction> VectorFunctions = new Dictionary<VectorType, VectorFunction>();

    public Vector3 FirstVector
    {
        get
        {
            return StoredVector3Range.First;
        }
    }

    public Vector3 SecondVector
    {
        get
        {
            return StoredVector3Range.Second;
        }
    }

    public void SetVector(VectorType vectorType, RangeOption rangeOption)
    {
        if (VectorFunctions == null)
        {
            VectorFunctions = new Dictionary<VectorType, VectorFunction>();
        }

        if (VectorFunctions.Count <= 0)
        {
            VectorFunction vectorFunction;

            vectorFunction = new VectorFunction();
            vectorFunction.AddListener(SetWorldPosition);
            VectorFunctions.Add(VectorType.WorldPosition, vectorFunction);

            vectorFunction = new VectorFunction();
            vectorFunction.AddListener(SetLocalPosition);
            VectorFunctions.Add(VectorType.LocalPosition, vectorFunction);

            vectorFunction = new VectorFunction();
            vectorFunction.AddListener(SetWorldEulerAngles);
            VectorFunctions.Add(VectorType.WorldEuler, vectorFunction);

            vectorFunction = new VectorFunction();
            vectorFunction.AddListener(SetLocalEulerAngles);
            VectorFunctions.Add(VectorType.LocalEuler, vectorFunction);

            vectorFunction = new VectorFunction();
            vectorFunction.AddListener(SetLocalScale);
            VectorFunctions.Add(VectorType.LocalScale, vectorFunction);
        }

        VectorFunctions[vectorType].Invoke(rangeOption);
    }

    public void SetWorldPosition(RangeOption rangeOption)
    {
        StoredTransform.position = (rangeOption == RangeOption.First) ? FirstVector : SecondVector;
    }

    public void SetLocalPosition(RangeOption rangeOption)
    {
        StoredTransform.localPosition = (rangeOption == RangeOption.First) ? FirstVector : SecondVector;
    }

    public void SetWorldEulerAngles(RangeOption rangeOption)
    {
        StoredTransform.eulerAngles = (rangeOption == RangeOption.First) ? FirstVector : SecondVector;
    }

    public void SetLocalEulerAngles(RangeOption rangeOption)
    {
        StoredTransform.localEulerAngles = (rangeOption == RangeOption.First) ? FirstVector : SecondVector;
    }

    public void SetLocalScale(RangeOption rangeOption)
    {
        StoredTransform.localScale = (rangeOption == RangeOption.First) ? FirstVector : SecondVector;
    }

    public void Fetch()
    {
        Name = StoredTransform.name;
    }

    /// <summary>
    /// Use it when you want to print the full debug of this TransformWithVector3Range
    /// </summary>
    public string FullDebug
    {
        get
        {
            return "Stored Transform = " + StoredTransform.name + " / First Vector3 = " + FirstVector.ToString() + " / Second Vector3 = " + SecondVector.ToString();
        }
    }
}

#endregion

#region Rect Transform with Vector2Range

/// <summary>
/// A convenient way to tie a Rect Transform to a Vector2Range, allowing various manipulations with them
/// </summary>
[System.Serializable]
public class RectTransformWithVector2Range
{
    public string Name;
    public RectTransform StoredTransform;
    public VectorType VectorType;
    public Vector2Range StoredVector2Range;

    /// <summary>
    /// A convenient way to tie a Rect Transform to a Vector2Range, allowing various manipulations with them
    /// </summary>
    public RectTransformWithVector2Range(string name, RectTransform storedTransform, VectorType vectorType, Vector2Range storedVector2Range)
    {
        Name = name;
        StoredTransform = storedTransform;
        VectorType = vectorType;
        StoredVector2Range = storedVector2Range;
    }

    Dictionary<VectorType, VectorFunction> VectorFunctions = new Dictionary<VectorType, VectorFunction>();

    public Vector2 FirstVector
    {
        get
        {
            return StoredVector2Range.First;
        }
    }

    public Vector2 SecondVector
    {
        get
        {
            return StoredVector2Range.Second;
        }
    }

    public void SetVector(VectorType vectorType, RangeOption rangeOption)
    {
        if (VectorFunctions == null)
        {
            VectorFunctions = new Dictionary<VectorType, VectorFunction>();
        }

        if (VectorFunctions.Count <= 0)
        {
            VectorFunction vectorFunction;

            vectorFunction = new VectorFunction();
            vectorFunction.AddListener(SetAnchoredPosition);
            VectorFunctions.Add(VectorType.AnchoredPosition, vectorFunction);

            vectorFunction = new VectorFunction();
            vectorFunction.AddListener(SetAnchoredPosition3D);
            VectorFunctions.Add(VectorType.AnchoredPosition3D, vectorFunction);

            vectorFunction = new VectorFunction();
            vectorFunction.AddListener(SetPivot);
            VectorFunctions.Add(VectorType.Pivot, vectorFunction);

            vectorFunction = new VectorFunction();
            vectorFunction.AddListener(SetAnchorMin);
            VectorFunctions.Add(VectorType.AnchorMin, vectorFunction);

            vectorFunction = new VectorFunction();
            vectorFunction.AddListener(SetAnchorMax);
            VectorFunctions.Add(VectorType.AnchorMax, vectorFunction);

            vectorFunction = new VectorFunction();
            vectorFunction.AddListener(SetWorldEulerAngles);
            VectorFunctions.Add(VectorType.WorldEuler, vectorFunction);

            vectorFunction = new VectorFunction();
            vectorFunction.AddListener(SetLocalEulerAngles);
            VectorFunctions.Add(VectorType.LocalEuler, vectorFunction);

            vectorFunction = new VectorFunction();
            vectorFunction.AddListener(SetLocalScale);
            VectorFunctions.Add(VectorType.LocalScale, vectorFunction);

            vectorFunction = new VectorFunction();
            vectorFunction.AddListener(SetSizeDelta);
            VectorFunctions.Add(VectorType.SizeDelta, vectorFunction);
        }

        VectorFunctions[vectorType].Invoke(rangeOption);
    }

    public void SetAnchoredPosition(RangeOption rangeOption)
    {
        StoredTransform.anchoredPosition = (rangeOption == RangeOption.First) ? FirstVector : SecondVector;
    }

    public void SetAnchoredPosition3D(RangeOption rangeOption)
    {
        StoredTransform.anchoredPosition3D = (rangeOption == RangeOption.First) ? FirstVector : SecondVector;
    }

    public void SetPivot(RangeOption rangeOption)
    {
        StoredTransform.pivot = (rangeOption == RangeOption.First) ? FirstVector : SecondVector;
    }

    public void SetAnchorMin(RangeOption rangeOption)
    {
        StoredTransform.anchorMin = (rangeOption == RangeOption.First) ? FirstVector : SecondVector;
    }

    public void SetAnchorMax(RangeOption rangeOption)
    {
        StoredTransform.anchorMax = (rangeOption == RangeOption.First) ? FirstVector : SecondVector;
    }

    public void SetWorldEulerAngles(RangeOption rangeOption)
    {
        StoredTransform.eulerAngles = (rangeOption == RangeOption.First) ? FirstVector : SecondVector;
    }

    public void SetLocalEulerAngles(RangeOption rangeOption)
    {
        StoredTransform.localEulerAngles = (rangeOption == RangeOption.First) ? FirstVector : SecondVector;
    }

    public void SetLocalScale(RangeOption rangeOption)
    {
        StoredTransform.localScale = (rangeOption == RangeOption.First) ? FirstVector : SecondVector;
    }

    public void SetSizeDelta(RangeOption rangeOption)
    {
        StoredTransform.sizeDelta = (rangeOption == RangeOption.First) ? FirstVector : SecondVector;
    }

    public void Fetch()
    {
        Name = StoredTransform.name;
    }

    /// <summary>
    /// Use it when you want to print the full debug of this RectTransformWithVector2Range
    /// </summary>
    public string FullDebug
    {
        get
        {
            return "Stored RectTransform = " + StoredTransform.name + " / First Vector2 = " + FirstVector.ToString() + " / Second Vector2 = " + SecondVector.ToString();
        }
    }
}

#endregion

#region Transform with TransformationsRange

/// <summary>
/// A convenient way to tie a Transform to a TransformationsRange, allowing various manipulations with them
/// </summary>
[System.Serializable]
public class TransformWithTransformationsRange
{
    public string Name;
    public Transform StoredTransform;
    public TransformationsRange StoredTransformationsRange;

    /// <summary>
    /// A convenient way to tie a Transform to a TransformationsRange, allowing various manipulations with them
    /// </summary>
    public TransformWithTransformationsRange(string name, Transform storedTransform, TransformationsRange storedTransformationsRange)
    {
        Name = name;
        StoredTransform = storedTransform;
        StoredTransformationsRange = storedTransformationsRange;
    }

    /// <summary>
    /// Copies the current Transformation (global position, eulerAngles, scale) of the StoredTransform
    /// </summary>
    public Transformation CopyCurrentTransformation()
    {
        return StoredTransform.GetGlobalTransformation();
    }
    
    public void SetCurrentTransformation(Transformation transformation)
    {
        StoredTransform.SetGlobalTransformation(transformation);
    }

    public void SetCurrentTransformation(RangeOption rangeOption)
    {
        if (rangeOption == RangeOption.First)
        {
            StoredTransform.SetGlobalTransformation(FirstTransformation);
        }
        if (rangeOption == RangeOption.Second)
        {
            StoredTransform.SetGlobalTransformation(SecondTransformation);
        }
    }

    public Transformation FirstTransformation
    {
        get
        {
            return StoredTransformationsRange.First;
        }
        set
        {
            StoredTransformationsRange.First = value;
        }
    }

    public Transformation SecondTransformation
    {
        get
        {
            return StoredTransformationsRange.Second;
        }
        set
        {
            StoredTransformationsRange.Second = value;
        }
    }

    /// <summary>
    /// Takes a Transform and returns a TransformWithTransformationsRange built from it. Can also receive a Range option, first or second.<para></para>
    /// If the First range option is selected, the current Transformation values of this transform will be saved into the First Transformation of the TransformationsRange, while the Second will be Transformation.Default <para></para>
    /// If the Second range option is selected, the current Transformation values of this transform will be saved into the Second Transformation of the TransformationsRange, while the First will be Transformation.Default
    /// </summary>
    public static TransformWithTransformationsRange CreateFromTransform(Transform transform, RangeOption storeAsRangeOption = RangeOption.First)
    {
        if (storeAsRangeOption == RangeOption.First)
        {
            return new TransformWithTransformationsRange(transform.name, transform, 
                new TransformationsRange(transform.GetGlobalTransformation(), Transformation.Default));
        }
        else
        {
            return new TransformWithTransformationsRange(transform.name, transform, 
                new TransformationsRange(Transformation.Default, transform.GetGlobalTransformation()));
        }
    }

    public void Fetch()
    {
        if (StoredTransform != null)
        {
            Name = StoredTransform.name;
        }
    }

    /// <summary>
    /// Use it when you want to print the full debug of this TransformWithTransformationsRange
    /// </summary>
    public string FullDebug
    {
        get
        {
            return "Stored Transform = " + StoredTransform.name + " / Stored Transformations Range = " + StoredTransformationsRange.FullDebug;
        }
    }
}

#endregion

#region SceneObject

/// <summary>
/// A struct that represents an instance of a GameObject as well as its Prefab. Useful when you want to tie the two of them without having to call Resources.Load
/// </summary>
[System.Serializable]
public class SceneObject
{
    public string Name;
    public GameObject Instance;
    public GameObject Prefab;

    /// <summary>
    /// A struct that represents an instance of a GameObject as well as its Prefab. Useful when you want to tie the two of them without having to call Resources.Load
    /// </summary>
    public SceneObject(GameObject instance, GameObject prefab)
    {
        Instance = instance;
        Prefab = prefab;
    }

    public void SetActive(bool active)
    {
        if (Instance != null)
        {
            Instance.SetActive(active);
        }
    }

    /// <summary>
    /// The local active state of the Instance of this SceneObject. (Read Only)
    /// </summary>
    public bool activeSelf
    {
        get
        {
            if (Instance == null)
                return false;

            return Instance.activeSelf;
        }
    }

    /// <summary>
    /// Defines whether the Instance of this SceneObject is active in the Scene.
    /// </summary>
    public bool activeInHierarchy
    {
        get
        {
            if (Instance == null)
                return false;

            return Instance.activeInHierarchy;
        }
    }


    /// <summary>
    /// The Transform attached to the Instance of this SceneObject
    /// </summary>
    public Transform transform
    {
        get
        {
            if (Instance == null)
                return null;

            return Instance.transform;
        }
    }

    /// <summary>
    /// Besides using the Transformation values of the Prefab or of the Instance, it is also possible to store a custom Transformation to the SceneObject
    /// </summary>
    [HideInInspector] public Transformation StoredTransformation = Transformation.Default;

    /// <summary>
    /// A parent Transform stored into the SceneObject. Useful when reinstantiating the Instance after it has been destroyed
    /// </summary>
    [HideInInspector] public Transform StoredParent = null;

    /// <summary>
    /// Stores a MonoBehaviour in the SceneObject to avoid using GetComponent() all the time
    /// </summary>
    [HideInInspector] public MonoBehaviour StoredBehaviour = null;

    /// <summary>
    /// Stores the initial position, rotation, scale, parent and a MonoBehaviour into this SceneObject. <para></para>
    /// Unlike Transformation (position + rotation + scale) and parent, the MonoBehavour should be sent as a parameter
    /// </summary>
    public void StoreInitialData(MonoBehaviour monoBehaviour = null)
    {
        StoredTransformation = transform.GetGlobalTransformation();
        StoredParent = transform.parent;
        StoredBehaviour = monoBehaviour;
    }

    public void Fetch()
    {
        if (Instance != null)
        {
            Name = Instance.name;
        }
    }

    /// <summary>
    /// Use it when you want to print the full Debug of this SceneObject
    /// </summary>
    public string FullDebug
    {
        get
        {
            return "Prefab = " + Prefab.name + " / Instance = " + Instance.name + " / StoredTransformation = " + StoredTransformation
                + " / StoredParent = " + StoredParent + " / StoredBehaviour = " + StoredBehaviour;
        }
    }
}

#endregion

#region Vector2Bool

/// <summary>
/// A struct made of 2 booleans, equivalent to Vector2. Useful to define which axes should be includes or ignored
/// </summary>
[System.Serializable]
public class Vector2Bool
{
    public bool X;
    public bool Y;

    /// <summary>
    /// A struct made of 2 booleans, equivalent to Vector2. Useful to define which axes should be includes or ignored
    /// </summary>
    public Vector2Bool(bool x, bool y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Sets values to each axis
    /// </summary>
    public void Set(bool x, bool y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Sets all axes true or false
    /// </summary>
    public void SetAll(bool value)
    {
        X = value;
        Y = value;
    }

    /// <summary>
    /// Stands for Vector2Bool (false, true)
    /// </summary>
    public static Vector2Bool NoX
    {
        get
        {
            return new Vector2Bool(false, true);
        }
    }

    /// <summary>
    /// Stands for Vector2Bool (true, false)
    /// </summary>
    public static Vector2Bool NoY
    {
        get
        {
            return new Vector2Bool(true, false);
        }
    }

    /// <summary>
    /// Stands for Vector2Bool (true, true)
    /// </summary>
    public static Vector2Bool AllTrue
    {
        get
        {
            return new Vector2Bool(true, true);
        }
    }

    /// <summary>
    /// Stands for Vector2Bool (false, false)
    /// </summary>
    public static Vector2Bool AllFalse
    {
        get
        {
            return new Vector2Bool(false, false);
        }
    }

    /// <summary>
    /// Returns whether this Vector2Bool has all its axes set to true
    /// </summary>
    public bool IsAllTrue
    {
        get
        {
            return X && Y;
        }
    }

    /// <summary>
    /// Returns whether this Vector2Bool has all its axes set to false
    /// </summary>
    public bool IsAllFalse
    {
        get
        {
            return !X && !Y;
        }
    }

    /// <summary>
    /// Returns whether at least one of the axes is true
    /// </summary>
    public bool IsAtLeastOneAxisTrue
    {
        get
        {
            return X || Y;
        }
    }

    /// <summary>
    /// Returns whether at least one of the axes is false
    /// </summary>
    public bool IsAtLeastOneAxisFalse
    {
        get
        {
            return !X || !Y;
        }
    }

    public static Vector2Bool RandomValue
    {
        get
        {
            return new Vector2Bool(Random.Range(0, 2) == 1, Random.Range(0, 2) == 1);
        }
    }

    public static Vector2Bool RandomUniformed
    {
        get
        {
            int randomInt = Random.Range(0, 2);
            return new Vector2Bool(randomInt == 1, randomInt == 1);
        }
    }

    /// <summary>
    /// Use it when you want to print the full debug of this Vector2Bool
    /// </summary>
    public string FullDebug
    {
        get
        {
            return "X = " + X + " / Y = " + Y;
        }
    }
}

#endregion

#region Vector3Bool

/// <summary>
/// A struct made of 3 booleans, equivalent to Vector3. Useful to define which axes should be includes or ignored
/// </summary>
[System.Serializable]
public class Vector3Bool
{
    public bool X;
    public bool Y;
    public bool Z;

    /// <summary>
    /// A struct made of 3 booleans, equivalent to Vector3. Useful to define which axes should be includes or ignored
    /// </summary>
    public Vector3Bool(bool x, bool y, bool z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Sets values to each axis
    /// </summary>
    public void Set(bool x, bool y, bool z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Sets all axes true or false
    /// </summary>
    public void SetAll(bool value)
    {
        X = value;
        Y = value;
        Z = value;
    }

    /// <summary>
    /// Stands for Vector3Bool (false, true, true)
    /// </summary>
    public static Vector3Bool NoX
    {
        get
        {
            return new Vector3Bool(false, true, true);
        }
    }

    /// <summary>
    /// Stands for Vector3Bool (true, false, true)
    /// </summary>
    public static Vector3Bool NoY
    {
        get
        {
            return new Vector3Bool(true, false, true);
        }
    }

    /// <summary>
    /// Stands for Vector3Bool (true, true, false)
    /// </summary>
    public static Vector3Bool NoZ
    {
        get
        {
            return new Vector3Bool(true, true, false);
        }
    }

    /// <summary>
    /// Stands for Vector3Bool (true, true, true)
    /// </summary>
    public static Vector3Bool AllTrue
    {
        get
        {
            return new Vector3Bool(true, true, true);
        }
    }

    /// <summary>
    /// Stands for Vector3Bool (false, false, false)
    /// </summary>
    public static Vector3Bool AllFalse
    {
        get
        {
            return new Vector3Bool(false, false, false);
        }
    }

    /// <summary>
    /// Returns whether this Vector3Bool has all its axes set to true
    /// </summary>
    public bool IsAllTrue
    {
        get
        {
            return X && Y && Z;
        }
    }

    /// <summary>
    /// Returns whether this Vector3Bool has all its axes set to false
    /// </summary>
    public bool IsAllFalse
    {
        get
        {
            return !X && !Y && !Z;
        }
    }

    /// <summary>
    /// Returns whether at least one of the axes is true
    /// </summary>
    public bool IsAtLeastOneAxisTrue
    {
        get
        {
            return X || Y || Z;
        }
    }

    /// <summary>
    /// Returns whether at least one of the axes is false
    /// </summary>
    public bool IsAtLeastOneAxisFalse
    {
        get
        {
            return !X || !Y || !Z;
        }
    }

    public static Vector3Bool RandomValue
    {
        get
        {
            return new Vector3Bool(Random.Range(0, 2) == 1, Random.Range(0, 2) == 1, Random.Range(0, 2) == 1);
        }
    }

    public static Vector3Bool RandomUniformed
    {
        get
        {
            int randomInt = Random.Range(0, 2);
            return new Vector3Bool(randomInt == 1, randomInt == 1, randomInt == 1);
        }
    }

    /// <summary>
    /// Use it when you want to print the full debug of this Vector3Bool
    /// </summary>
    public string FullDebug
    {
        get
        {
            return "X = " + X + " / Y = " + Y + " / Z = " + Z;
        }
    }
}

#endregion

#region ColorRange

/// <summary>
/// Defines two colors and allows various manipulations with their range
/// </summary>
[System.Serializable]
public class ColorRange
{
	public Color ColorA;
	public Color ColorB;

    /// <summary>
    /// Defines two colors and allows various manipulations with their range
    /// </summary>
    public ColorRange (Color A, Color B)
	{
		ColorA = A;
		ColorB = B;
	}

    /// <summary>
    /// Returns whether the specified color is within the color range
    /// </summary>
    public bool ContainsColor(Color color)
	{
		bool containsRed = ((color.r > ColorA.r) && (color.r < ColorB.r)) || ((color.r < ColorA.r) && (color.r > ColorB.r));
		bool containsBlue = ((color.g > ColorA.g) && (color.g < ColorB.g)) || ((color.g < ColorA.g) && (color.g > ColorB.g));
		bool containsGreen = ((color.b > ColorA.b) && (color.b < ColorB.b)) || ((color.b < ColorA.b) && (color.b > ColorB.b));
		bool containsAlpha = ((color.a > ColorA.a) && (color.a < ColorB.a)) || ((color.a < ColorA.a) && (color.a > ColorB.a));
		return containsRed && containsBlue && containsGreen && containsAlpha;
	}

    /// <summary>
    /// Returns the average color from inside the range
    /// </summary>
    public Color Average
	{
		get 
		{
			return new Color 
            (
				(ColorA.r + ColorB.r) / 2, 
				(ColorA.g + ColorB.g) / 2, 
				(ColorA.b + ColorB.b) / 2,
				(ColorA.a + ColorB.a) / 2
			);
		}
    }

    /// <summary>
    /// Returns a random value inside the color range
    /// </summary>
    public Color RandomWithinRange
	{
        get
        {
            return new Color
            (
                Random.Range(ColorA.r, ColorB.r),
                Random.Range(ColorA.g, ColorB.g),
                Random.Range(ColorA.b, ColorB.b),
                Random.Range(ColorA.a, ColorB.a)
            );
        }
	}

	public static ColorRange SwitchValues(ColorRange colorRange)
	{
		return new ColorRange (colorRange.ColorB, colorRange.ColorA);
	}

	public ColorRange SwitchValues()
	{
		return new ColorRange (ColorA, ColorB);
    }

    /// <summary>
    /// Use it when you want to print the full debug of this ColorRange
    /// </summary>
    public string FullDebug
    {
        get
        {
            return "ColorA = " + ColorA.ToString() + " / ColorB = " + ColorB.ToString();
        }
    }
}

#endregion

#region Trajectory

/// <summary>
/// A 3D trajectory built from Animation Curves. Each curve represents a different axis
/// </summary>
[System.Serializable]
public class Trajectory
{
	public AnimationCurve X;
	public AnimationCurve Y;
	public AnimationCurve Z;

    /// <summary>
    /// A 3D trajectory built from Animation Curves. Each curve represents a different axis
    /// </summary>
    public Trajectory(AnimationCurve x, AnimationCurve y, AnimationCurve z)
	{
		X = x;
		Y = y;
		Z = z;
	}
}

#endregion

#region Transformation

/// <summary>
/// A struct which represents position, rotation and scale
/// </summary>
[System.Serializable]
public class Transformation
{
	public Vector3 Position;
	public Vector3 Rotation;
	public Vector3 Scale;

    /// <summary>
    /// A struct which represents position, rotation and scale
    /// </summary>
    public Transformation(Vector3 position, Vector3 rotation, Vector3 scale)
	{
		Position = position;
		Rotation = rotation;
		Scale = scale;
	}

    /// <summary>
    /// Returns whether the transformation equals to another transformation at all vectors. One of the vectors can be ignored. By default none is ignored
    /// </summary>
    public bool EqualsTo(Transformation transformation, IgnoreVector ignoreVector = IgnoreVector.None)
    {
        bool positionEquals = (Position == transformation.Position) || (ignoreVector == IgnoreVector.Position);
        bool rotationEquals = (Rotation == transformation.Rotation) || (ignoreVector == IgnoreVector.Rotation);
        bool scaleEquals = (Scale == transformation.Scale) || (ignoreVector == IgnoreVector.Scale);

        return positionEquals && rotationEquals && scaleEquals;
    }

    public static Transformation Lerp(Transformation from, Transformation to, float timeStep, IgnoreVector ignoreVector = IgnoreVector.None)
    {
        if (ignoreVector != IgnoreVector.Position)
        {
            from.Position = Vector3.Lerp(from.Position, to.Position, timeStep);
        }

        if (ignoreVector != IgnoreVector.Rotation)
        {
            from.Rotation = Vector3.Lerp(from.Rotation, to.Rotation, timeStep);
        }

        if (ignoreVector != IgnoreVector.Scale)
        {
            from.Scale = Vector3.Lerp(from.Scale, to.Scale, timeStep);
        }

        return from;
    }

    public static Transformation SmoothDamp(Transformation from, Transformation to, ref Vector3 velocity, float timeStep, IgnoreVector ignoreVector = IgnoreVector.None)
    {
        if (ignoreVector != IgnoreVector.Position)
        {
            from.Position = Vector3.SmoothDamp(from.Position, to.Position, ref velocity, timeStep);
        }

        if (ignoreVector != IgnoreVector.Rotation)
        {
            from.Rotation = Vector3.SmoothDamp(from.Rotation, to.Rotation, ref velocity, timeStep);
        }

        if (ignoreVector != IgnoreVector.Scale)
        {
            from.Scale = Vector3.SmoothDamp(from.Scale, to.Scale, ref velocity, timeStep);
        }

        return from;
    }

    public void ResetPosition()
	{
		Position = Vector3.zero;
	}

	public void ResetRotation()
	{
		Rotation = Vector3.zero;
	}

	public void ResetScale()
	{
		Scale = Vector3.one;
	}

	public void Reset()
	{
		Position = Vector3.zero;
		Rotation = Vector3.zero;
		Scale = Vector3.one;
	}

    public static Transformation Random(Vector3 randomPosition, Vector3 randomEulerAngnles, Vector3 randomScale)
    {
        return new Transformation(randomPosition, randomEulerAngnles, randomScale);
    }

    /// <summary>
    /// returns new Transformation(Vector3.zero, Vector3.zero, Vector3.one)
    /// </summary>
    public static Transformation Default
    {
        get
        {
            return new Transformation(Vector3.zero, Vector3.zero, Vector3.one);
        }
    }

    /// <summary>
    /// Use it when you want to print the full debug of this Transformation
    /// </summary>
    public string FullDebug
    {
        get
        {
            return "Position = " + Position.ToString() + " / Rotation(Euler) = " + Rotation.ToString() + " / Scale = " + Scale.ToString();
        }
    }
}

#endregion

#region RectTransformation

/// <summary>
/// A struct which represents RectTransform properties - anchoredPosition, size delta, anchor points, pivot, rotation and scale
/// </summary>
[System.Serializable]
public class RectTransformation
{
    public Vector3 AnchoredPosition;
    public Vector2 SizeDelta;
    public Vector2 AnchorMin;
    public Vector2 AnchorMax;
    public Vector2 Pivot;
    public Vector3 Rotation;
    public Vector3 Scale;

    /// <summary>
    /// A struct which represents RectTransform properties - anchoredPosition, size delta, anchor points, pivot, rotation and scale
    /// </summary>
    public RectTransformation(Vector3 anchoredPosition, Vector2 sizeDelta, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot,
        Vector3 rotation, Vector3 scale)
    {
        AnchoredPosition = anchoredPosition;
        SizeDelta = sizeDelta;
        AnchorMin = anchorMin;
        AnchorMax = anchorMax;
        Pivot = pivot;
        Rotation = rotation;
        Scale = scale;
    }

    public static void Lerp(RectTransformation from, RectTransformation to, float timeStep)
    {
        Lerp(from, to, timeStep, DefaultListedVectors);
    }

    public static RectTransformation Lerp(RectTransformation from, RectTransformation to, float timeStep, List<VectorType> listedVectors)
    {
        if (listedVectors.Contains(VectorType.AnchoredPosition3D) || listedVectors.Contains(VectorType.AnchoredPosition))
        {
            from.AnchoredPosition = Vector3.Lerp(from.AnchoredPosition, to.AnchoredPosition, timeStep);
        }

        if (listedVectors.Contains(VectorType.AnchoredPosition))
        {
            from.AnchoredPosition = Vector3.Lerp(from.AnchoredPosition, to.AnchoredPosition, timeStep);
        }

        if (listedVectors.Contains(VectorType.SizeDelta))
        {
            from.SizeDelta = Vector3.Lerp(from.SizeDelta, to.SizeDelta, timeStep);
        }

        if (listedVectors.Contains(VectorType.AnchorMin))
        {
            from.AnchorMin = Vector3.Lerp(from.AnchorMin, to.AnchorMin, timeStep);
        }

        if (listedVectors.Contains(VectorType.AnchorMax))
        {
            from.AnchorMax = Vector3.Lerp(from.AnchorMax, to.AnchorMax, timeStep);
        }

        if (listedVectors.Contains(VectorType.Pivot))
        {
            from.Pivot = Vector3.Lerp(from.Pivot, to.Pivot, timeStep);
        }

        if (listedVectors.Contains(VectorType.LocalEuler))
        {
            from.Rotation = Vector3.Lerp(from.Rotation, to.Rotation, timeStep);
        }

        if (listedVectors.Contains(VectorType.LocalScale))
        {
            from.Scale = Vector3.Lerp(from.Scale, to.Scale, timeStep);
        }

        return from;
    }

    /// <summary>
    /// Returns whether the rectTransformation equals to another rectTransformation at the default vectors
    /// </summary>
    public bool EqualsTo(RectTransformation rectTransformation)
    {
        return EqualsTo(rectTransformation, DefaultListedVectors);
    }

    /// <summary>
    /// Returns whether the rectTransformation equals to another rectTransformation at the given vectors
    /// </summary>
    public bool EqualsTo(RectTransformation rectTransformation, List<VectorType> listedVectors)
    {
        bool anchoredPosition3DEquals = (AnchoredPosition == rectTransformation.AnchoredPosition) || (!listedVectors.Contains(VectorType.AnchoredPosition3D) && !listedVectors.Contains(VectorType.AnchoredPosition));
        bool anchoredPositionEquals = (AnchoredPosition == rectTransformation.AnchoredPosition) || !listedVectors.Contains(VectorType.AnchoredPosition3D);
        bool sizeDeltaEquals = (SizeDelta == rectTransformation.SizeDelta) || !listedVectors.Contains(VectorType.SizeDelta);
        bool anchorMinEquals = (AnchorMin == rectTransformation.AnchorMin) || !listedVectors.Contains(VectorType.AnchorMin);
        bool anchorMaxEquals = (AnchorMax == rectTransformation.AnchorMax) || !listedVectors.Contains(VectorType.AnchorMax);
        bool pivotEquals = (Pivot == rectTransformation.Pivot) || !listedVectors.Contains(VectorType.Pivot);
        bool rotationEquals = (Rotation == rectTransformation.Rotation) || !listedVectors.Contains(VectorType.LocalEuler);
        bool scaleEquals = (Scale == rectTransformation.Scale) || !listedVectors.Contains(VectorType.LocalScale);

        return (anchoredPosition3DEquals || anchoredPositionEquals) && sizeDeltaEquals && anchorMinEquals && anchorMaxEquals && pivotEquals && rotationEquals && scaleEquals;
    }

    public static void SmoothDamp(RectTransformation from, RectTransformation to, ref Vector3 velocity, float timeStep)
    {
        SmoothDamp(from, to, ref velocity, timeStep, DefaultListedVectors);
    }

    public static RectTransformation SmoothDamp(RectTransformation from, RectTransformation to, ref Vector3 velocity, float timeStep, List<VectorType> listedVectors)
    {
        if (listedVectors.Contains(VectorType.AnchoredPosition3D) || listedVectors.Contains(VectorType.AnchoredPosition))
        {
            from.AnchoredPosition = Vector3.SmoothDamp(from.AnchoredPosition, to.AnchoredPosition, ref velocity, timeStep);
        }

        if (listedVectors.Contains(VectorType.AnchoredPosition))
        {
            from.AnchoredPosition = Vector3.SmoothDamp(from.AnchoredPosition, to.AnchoredPosition, ref velocity, timeStep);
        }

        if (listedVectors.Contains(VectorType.SizeDelta))
        {
            from.SizeDelta = Vector3.SmoothDamp(from.SizeDelta, to.SizeDelta, ref velocity, timeStep);
        }

        if (listedVectors.Contains(VectorType.AnchorMin))
        {
            from.AnchorMin = Vector3.SmoothDamp(from.AnchorMin, to.AnchorMin, ref velocity, timeStep);
        }

        if (listedVectors.Contains(VectorType.AnchorMax))
        {
            from.AnchorMax = Vector3.SmoothDamp(from.AnchorMax, to.AnchorMax, ref velocity, timeStep);
        }

        if (listedVectors.Contains(VectorType.Pivot))
        {
            from.Pivot = Vector3.SmoothDamp(from.Pivot, to.Pivot, ref velocity, timeStep);
        }

        if (listedVectors.Contains(VectorType.LocalEuler))
        {
            from.Rotation = Vector3.SmoothDamp(from.Rotation, to.Rotation, ref velocity, timeStep);
        }

        if (listedVectors.Contains(VectorType.LocalScale))
        {
            from.Scale = Vector3.SmoothDamp(from.Scale, to.Scale, ref velocity, timeStep);
        }

        return from;
    }

    public void ResetSizeDelta()
    {
        SizeDelta = DefaultSizeDelta;
    }

    public void ResetAnchorMin()
    {
        AnchorMin = DefaultAnchorMin;
    }

    public void ResetAnchorMax()
    {
        AnchorMax = DefaultAnchorMax;
    }

    public void ResetPivot()
    {
        Pivot = DefaultPivot;
    }

    public void Reset()
    {
        AnchoredPosition = Vector3.zero;
        SizeDelta = DefaultSizeDelta;
        AnchorMin = DefaultAnchorMin;
        AnchorMax = DefaultAnchorMax;
        Pivot = DefaultPivot;
        Rotation = Vector3.zero;
        Scale = Vector3.zero;
    }

    public static Vector2 DefaultSizeDelta = new Vector2(100, 100);
    public static Vector2 DefaultAnchorMin = new Vector2(0.5f, 0.5f);
    public static Vector2 DefaultAnchorMax = new Vector2(0.5f, 0.5f);
    public static Vector2 DefaultPivot = new Vector2(0.5f, 0.5f);

    public static List<VectorType> DefaultListedVectors = new List<VectorType>()
    { VectorType.AnchoredPosition, VectorType.LocalEuler };

    public static List<VectorType> AllVectors = new List<VectorType>()
    { VectorType.AnchoredPosition, VectorType.SizeDelta, VectorType.AnchorMin, VectorType.AnchorMax, VectorType.Pivot,
    VectorType.LocalEuler, VectorType.LocalScale };

    public static RectTransformation Random(Vector3 randomPosition, Vector3 randomEulerAngles, Vector3 randomScale, 
        Vector2 randomSizeDelta, Vector2 randomAnchorMin, Vector2 randomAnchorMax, Vector2 randomPivot)
    {
        return new RectTransformation(randomPosition, randomEulerAngles, randomScale, randomSizeDelta, randomAnchorMin, randomAnchorMax, randomPivot);
    }

    /// <summary>
    /// returns new RectTransformation(Vector3.zero, Vector3.zero, Vector3.one, DefaultSizeDelta, DefaultAnchorMin, DefaultAnchorMax, DefaultPivot);
    /// </summary>
    public static RectTransformation Default
    {
        get
        {
            return new RectTransformation(Vector3.zero, DefaultSizeDelta, DefaultAnchorMin, DefaultAnchorMax, DefaultPivot, Vector3.zero, Vector3.one);
        }
    }

    /// <summary>
    /// Use it when you want to print the full debug of this RectTransformation
    /// </summary>
    public string FullDebug
    {
        get
        {
            return "Anchored Position = " + AnchoredPosition.ToString() + " / SizeDelta = " + SizeDelta.ToString()
                + " / AnchorMin = " + AnchorMin.ToString() + " / AnchorMax = " + AnchorMax.ToString()
                + " / Pivot = " + Pivot.ToString() + " / Rotation(Euler) = " + Rotation.ToString()
                + " / Scale = " + Scale.ToString();
        }
    }
}

#endregion

#region AudioTrack

/// <summary>
/// A struct which represent an AudioClip with its stored data for AudioSource, so that it would adjust the parameters AudioSource components it is played on to the AudioTrack's values
/// </summary>
[System.Serializable]
public class AudioTrack
{
    public string Name;
    public AudioClip Track;
    [Range(0, 1)] public float Volume = 1;
    [Range(0, 2.5f)] public float Pitch = 1;
    [Range(0, 1)] public float SpatialBlend = 1;
    public bool Loop = false;

    /// <summary>
    /// A struct which represent an AudioClip with its stored data for AudioSource, so that it would adjust the parameters AudioSource components it is played on to the AudioTrack's values
    /// </summary>
    public AudioTrack(AudioClip track, float volume, float pitch, float spatialBlend, bool loop)
    {
        Track = track;
        Volume = volume;
        Pitch = pitch;
        SpatialBlend = spatialBlend;
        Loop = loop;
    }

    public void Fetch()
    {
        if (Track != null)
        {
            Name = Track.name;
        }
    }
}

#endregion

#region Timed Actions

#region Sequence Action

/// <summary>
/// A class which includes a name, a time stamp and an action which should happen at the end of delay.
/// </summary>
[System.Serializable]
public class SequenceAction
{
    public string Name;
    /// <summary>
    /// The timing point since the start of a video clip or a sequence, at which you want your action to happen
    /// </summary>
    [Tooltip("The timing point since the start of a video clip or a sequence, at which you want your action to happen")]
    public float TimeStamp;
    public UnityEvent Action;

    /// <summary>
    /// A class which includes a name, a time stamp and an action which should happen at the end of delay.
    /// </summary>
    public SequenceAction(string name, float timeStamp, UnityEvent action)
    {
        Name = name;
        TimeStamp = timeStamp;
        Action = action;
    }
}

#endregion

#region Timed Action

/// <summary>
/// A class which includes a name, a delay and an action which should happen at the end of delay.
/// </summary>
[System.Serializable]
public class TimedAction
{
    public string Name;
    public float Delay;
    public UnityEvent Action;

    /// <summary>
    /// A class which includes a name, a delay and an action which should happen at the end of delay.
    /// </summary>
    public TimedAction(string name, float delay, UnityEvent action)
    {
        Name = name;
        Delay = delay;
        Action = action;
    }
}

#endregion

#region Timer

/// <summary>
/// The simpliest way to suspend an action and execute it after the timer ends. Stop using Invokes :)
/// </summary>
public class Timer
{
    public float Delay;
    public UnityAction Action;
    public float RepeatInterval = 0;

    /// <summary>
    /// The simpliest way to suspend an action and execute it after the timer ends. Stop using Invokes :)
    /// </summary>
    public Timer(float delay, UnityAction action, float repeatInterval = 0)
    {
        Delay = delay;
        Action = action;
        RepeatInterval = repeatInterval;
    }

    /// <summary>
    /// Set timer values
    /// </summary>
    public void Set(float delay, UnityAction action, float repeatInterval = 0)
    {
        Delay = delay;
        Action = action;
        RepeatInterval = repeatInterval;
    }

    /// <summary>
    /// The time step value of the timer, from 0 to the Delay value
    /// </summary>
    public float Value = -1;

    /// <summary>
    /// The behaviour the timer is running on
    /// </summary>
    public MonoBehaviourEx StoredBehaviour = null;

    public bool IsRunning
    {
        get
        {
            return (Value >= 0) && (Value < Delay);
        }
    }

    public bool IsStopped
    {
        get
        {
            return (Value < 0);
        }
    }

    public bool IsRepeatable
    {
        get
        {
            return (RepeatInterval > 0);
        }
    }

    /// <summary>
    /// Persistent timer can be stopped or finish only directly, for example StopTimer(t), but not via iteration like StopAllTimers()
    /// </summary>
    public bool IsPersistent = false;

    /// <summary>
    /// Ignores timeScale
    /// </summary>
    public bool IsUnscaledTime = false;

    /// <summary>
    /// Persistent timer can be stopped or finish only directly, for example StopTimer(t), but not via iteration like StopAllTimers()
    /// </summary>
    public Timer AsPersistent
    {
        get
        {
            IsPersistent = true;
            return this;
        }
    }
    /// <summary>
    /// Ignores timeScale
    /// </summary>
    public Timer AsUnscaledTime
    {
        get
        {
            IsUnscaledTime = true;
            return this;
        }
    }
}

#endregion

#endregion

#region Enums

public enum OtherAxes
{
    /// <summary>
    /// Leaves the other axes values as is
    /// </summary>
    LeaveAsIs,
    /// <summary>
    /// Reset the other axes values to 0
    /// </summary>
    Reset
}


/// <summary>
/// How should the reminder work, automatically or manually or not work at all?
/// </summary>
public enum Reminder
{
    Automatic,
    Manual,
    None
}


/// <summary>
/// When choosing a random element in a list, should it select any random element, or should it prevent from using the same element again? <para></para>
/// Useful while randomly running through a music playlist, avoiding playing the same track twice in a row
/// </summary>
public enum RandomizeSelectionMethod
{
    /// <summary>
    /// Can select any random element, even if it means selecting the same element again
    /// </summary>
    AllowAny,
    /// <summary>
    /// If a random element is currently used, it will NOT select the same element twice in a row
    /// </summary>
    PreventUsingTwice
}



public enum PriorityLevel
{
    Low = 0,
    Medium = 1,
    High = 2,
    Top = 3
}



public enum Urgency
{
    Immediate, Overtime
}



/// <summary>
/// A reference for an axis, either x, y, or z, or two axes, xy, yz, xz
/// </summary>
public enum CustomAxis
{
    x, y, z,
    xy, yz, xz
}


/// <summary>
/// Play or mute the SFX?
/// </summary>
public enum SFX
{
    Play, Mute
}


/// <summary>
/// You want to play a sound, but the AudioSource is already playing something. How do you want to resolve this conflict?
/// </summary>
public enum SoundPlayingConflict
{
    /// <summary>
    /// If the AudioSource is already playing something, ignore the function call
    /// </summary>
    Ignore,
    /// <summary>
    /// If the AudioSource is already playing something, stop it, and play the new sound
    /// </summary>
    OverwritePrevious,
    /// <summary>
    /// If the AudioSource is already playing something, ignore the function call only if the incoming AudioClip is the same as the one already playing
    /// </summary>
    IgnoreSame
}


/// <summary>
/// Normal or Invulnerable?
/// </summary>
public enum PlayMode
{
    Normal, Invulnerable
}


/// <summary>
/// Mouse, Touch, Joysick, or Keyboard?
/// </summary>
public enum InputType
{
    Mouse, Touch, Joysick, Keyboard
}


/// <summary>
/// How should the VFX be played upon collision?
/// </summary>
public enum VFX
{
    /// <summary>
    /// Spawn the VFX at the collision/raycastHit point
    /// </summary>
    ImpactPoint = 0,
    /// <summary>
    /// Spawn the VFX at the center of the collided/raycastHit object 
    /// </summary>
    TargetCenter = 1,
    /// <summary>
    /// Spawn the VFX at the collided/raycastHit object's outline
    /// </summary>
    Outline = 2,
    /// <summary>
    /// Ignore VFX completely
    /// </summary>
    None = 4
}



public enum Normal
{
    /// <summary>
    /// Use the direction vector from impact point
    /// </summary>
    FromImpact = 0,
    /// <summary>
    /// Use the direction vector between the target center towards the impact point
    /// </summary>
    OutOfTarget = 1
}


/// <summary>
/// The type of iteration through a collectable: Random, Ascending order, or Descending order
/// </summary>
public enum Iteration
{
    Random,
    Ascending,
    Descending
}


/// <summary>
/// When using the List.NextTo() or List.PreviousTo() or List.NextToIndex() or List.PreviousToIndex() extensions, should it cycle through the list? <para></para>
/// Cycling through the list means if it reaches above the last index, it will go to 0 index, and if it reaches below 0, it will go to the last index
/// </summary>
public enum Iterate
{
    /// <summary>
    /// If it reaches above the last index, it will go to 0 index, and if it reaches below 0, it will go to the last index
    /// </summary>
    Cycle,
    /// <summary>
    /// If it reaches above the last index, it will stay on the last, and if it reaches below 0, it will stay on the 0 index
    /// </summary>
    DontCycle
}


/// <summary>
/// Useful when working with Transformations. When passing a Transformation parameter, you can specify which vectors you want to ignore <para></para>
/// The Transformation vectors are Position, Rotation (Euler), and Scale. By default, all vectors are included
/// </summary>
public enum IgnoreVector
{
    None, Position, Rotation, Scale, SizeDelta, AnchoredPosition, AnchoredPosition3D, AnchorMin, AnchorMax, Pivot
}


/// <summary>
/// Type of vector related to Transform or RectTransform properties
/// </summary>
public enum VectorType
{
    WorldPosition, LocalPosition, WorldEuler, LocalEuler, LocalScale, SizeDelta, AnchoredPosition, AnchoredPosition3D, AnchorMin, AnchorMax, Pivot
}


/// <summary>
/// When reinstatiating a SceneObject, where should it take its position, rotation and scale from?
/// </summary>
public enum TransformationVectors
{
    /// <summary>
    /// When reinstatiating a SceneObject, retains the position, rotation and scale of the Instance of the SceneObject
    /// </summary>
    LeaveAsIs,
    /// <summary>
    /// When reinstatiating a SceneObject, takes the position, rotation and scale of the Instance of the SceneObject from its Prefab
    /// </summary>
    TakeFromPrefab,
    /// <summary>
    /// When reinstatiating a SceneObject, takes the position, rotation and scale of the Instance of the SceneObject from its StoredTransformation
    /// </summary>
    Stored
}


/// <summary>
/// Is the target in front of the camera view, to the left, to the right, above, below, or behind it?
/// </summary>
public enum RelativeDirection
{
    InFront,
    ToTheLeft,
    ToTheRight,
    Behind,
    Above,
    Below
}


/// <summary>
/// When using the EnlistComponentsInChildren function, should the function ignore the object which contains the class the function has been called from?
/// </summary>
public enum ComponentsInChildren
{
    IncludeSelf,
    ExcludeSelf
}


/// <summary>
/// Useful while using TransformWithVector3Range or RectTransformWithVector2Range or TransformWithTransformationsRange classes in order to pass which of the range values you want to use
/// </summary>
public enum RangeOption
{
    First,
    Second
}


/// <summary>
/// Global or Local?
/// </summary>
public enum TargetSpace
{
    Global, Local
}

#endregion

#region Ragdoll

/// <summary>
/// The damage multipliers for each body part with their default values
/// </summary>
public static class RagdollDamageData
{
    public static float PelvisDmgMultiplier = 1;
    public static float LeftHipsDmgMultiplier = 0.67f;
    public static float LeftKneeDmgMultiplier = 0.5f;
    public static float RightHipsDmgMultiplier = 0.67f;
    public static float RightKneeDmgMultiplier = 0.5f;
    public static float LeftArmDmgMultiplier = 0.5f;
    public static float LeftElbowDmgMultiplier = 0.33f;
    public static float RightArmDmgMultiplier = 0.5f;
    public static float RightElbowDmgMultiplier = 0.33f;
    public static float MiddleSpineDmgMultiplier = 1;
    public static float HeadDmgMultiplier = 3;
}


/// <summary>
/// A class which contains all ragdoll bodyparts of a humanoid. MUST be initialize at start via the Initialize() function from the owner of the Ragdoll
/// </summary>
[System.Serializable]
public class Ragdoll
{
    public Collider Pelvis;
    public Collider LeftHips;
    public Collider LeftKnee;
    public Collider RightHips;
    public Collider RightKnee;
    public Collider LeftArm;
    public Collider LeftElbow;
    public Collider RightArm;
    public Collider RightElbow;
    public Collider MiddleSpine;
    public Collider Head;

    public Dictionary<Collider, float> RagdollHitData = new Dictionary<Collider, float>();
    List<Collider> BodyParts = new List<Collider>();

    public void Initialize()
    {
        RagdollHitData.Add(Pelvis, RagdollDamageData.PelvisDmgMultiplier);
        RagdollHitData.Add(LeftHips, RagdollDamageData.LeftHipsDmgMultiplier);
        RagdollHitData.Add(LeftKnee, RagdollDamageData.LeftKneeDmgMultiplier);
        RagdollHitData.Add(RightHips, RagdollDamageData.RightHipsDmgMultiplier);
        RagdollHitData.Add(RightKnee, RagdollDamageData.RightKneeDmgMultiplier);
        RagdollHitData.Add(LeftArm, RagdollDamageData.LeftArmDmgMultiplier);
        RagdollHitData.Add(LeftElbow, RagdollDamageData.LeftElbowDmgMultiplier);
        RagdollHitData.Add(RightArm, RagdollDamageData.RightArmDmgMultiplier);
        RagdollHitData.Add(RightElbow, RagdollDamageData.RightElbowDmgMultiplier);
        RagdollHitData.Add(MiddleSpine, RagdollDamageData.MiddleSpineDmgMultiplier);
        RagdollHitData.Add(Head, RagdollDamageData.HeadDmgMultiplier);

        BodyParts.Add(Pelvis);
        BodyParts.Add(LeftHips);
        BodyParts.Add(LeftKnee);
        BodyParts.Add(RightHips);
        BodyParts.Add(RightKnee);
        BodyParts.Add(LeftArm);
        BodyParts.Add(LeftElbow);
        BodyParts.Add(RightArm);
        BodyParts.Add(RightElbow);
        BodyParts.Add(MiddleSpine);
        BodyParts.Add(Head);
    }

    /// <summary>
    /// Returns the damage multiplier related to the body part we have hit
    /// </summary>
    public float GetDamageMultiplier(Collider collider)
    {
        if (collider == null)
            return 1;

        return RagdollHitData[collider];
    }

    public void SetActive(bool active)
    {
        BodyParts.ForEach(x => { x.SetEnabledIfExists(active); });
    }
}

#endregion

#region Custom Classes and Interfaces

[System.Serializable]
public class VectorFunction : UnityEvent<RangeOption> { }

public interface IObjectPoolable
{
    System.Type ObjectType { get; }
    bool IsActive { get; set; }
}

public interface IJoystickInput
{
    bool HasJoystick { get; }
}

[System.Serializable]
public class BoolChangeEvent : UnityEvent<bool> { }

#endregion

#region PlayerPrefsEx

/// <summary>
/// An extended version of PlayerPrefs, allowing the user to save more types of parameters
/// </summary>
public class PlayerPrefsEx
{
    /// <summary>
    /// Sets the value of the preference identified by key
    /// </summary>
    public static void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }


    /// <summary>
    /// Returns the value corresponding to key in the preference file if it exists.
    /// </summary>
    public static bool GetBool(string key, bool defaultValue = true)
    {
        return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }



    /// <summary>
    /// Sets the value of the preference identified by key
    /// </summary>
    public static void SetVector3(string key, Vector3 vector3)
    {
        PlayerPrefs.SetFloat(key + "x", vector3.x);
        PlayerPrefs.SetFloat(key + "y", vector3.y);
        PlayerPrefs.SetFloat(key + "z", vector3.z);
    }



    /// <summary>
    /// Returns the value corresponding to key in the preference file if it exists.
    /// </summary>
    public static Vector3 GetVector3(string key)
    {
        float x = PlayerPrefs.GetFloat(key + "x", 0);
        float y = PlayerPrefs.GetFloat(key + "y", 0);
        float z = PlayerPrefs.GetFloat(key + "z", 0);
        return new Vector3(x, y, z);
    }



    /// <summary>
    /// Sets the value of the preference identified by key
    /// </summary>
    public static void SetVector2(string key, Vector2 vector2)
    {
        PlayerPrefs.SetFloat(key + "x", vector2.x);
        PlayerPrefs.SetFloat(key + "y", vector2.y);
    }



    /// <summary>
    /// Returns the value corresponding to key in the preference file if it exists.
    /// </summary>
    public static Vector3 GetVector2(string key)
    {
        float x = PlayerPrefs.GetFloat(key + "x", 0);
        float y = PlayerPrefs.GetFloat(key + "y", 0);
        return new Vector2(x, y);
    }



    /// <summary>
    /// Sets the value of the preference identified by key
    /// </summary>
    public static void SetClampedFloat(string key, ClampedFloat clampedFloat)
    {
        PlayerPrefs.SetFloat(key + "max", clampedFloat.Min);
        PlayerPrefs.SetFloat(key + "min", clampedFloat.Max);
    }



    /// <summary>
    /// Returns the value corresponding to key in the preference file if it exists.
    /// </summary>
    public static ClampedFloat GetClampedFloat(string key)
    {
        float min = PlayerPrefs.GetFloat(key + "min", 0);
        float max = PlayerPrefs.GetFloat(key + "max", 1);
        return new ClampedFloat(min, max).Verified;
    }



    /// <summary>
    /// Sets the value of the preference identified by key
    /// </summary>
    public static void SetClampedInt(string key, ClampedInt clampedInt)
    {
        PlayerPrefs.SetInt(key + "max", clampedInt.Min);
        PlayerPrefs.SetInt(key + "min", clampedInt.Max);
    }



    /// <summary>
    /// Returns the value corresponding to key in the preference file if it exists.
    /// </summary>
    public static ClampedInt GetClampedInt(string key)
    {
        int min = PlayerPrefs.GetInt(key + "min", 0);
        int max = PlayerPrefs.GetInt(key + "max", 1);
        return new ClampedInt(min, max).Verified;
    }



    /// <summary>
    /// Sets the value of the preference identified by key
    /// </summary>
    public static void SetColor(string key, Color color)
    {
        PlayerPrefs.SetFloat(key + "r", color.r);
        PlayerPrefs.SetFloat(key + "g", color.g);
        PlayerPrefs.SetFloat(key + "b", color.b);
        PlayerPrefs.SetFloat(key + "a", color.a);
    }



    /// <summary>
    /// Returns the value corresponding to key in the preference file if it exists.
    /// </summary>
    public static void GetColor(string key)
    {
        float r = PlayerPrefs.GetFloat(key + "r", 0);
        float g = PlayerPrefs.GetFloat(key + "g", 0);
        float b = PlayerPrefs.GetFloat(key + "b", 0);
        float a = PlayerPrefs.GetFloat(key + "a", 0);
    }



    /// <summary>
    /// Sets the value of the preference identified by key
    /// </summary>
    public static void SetVector3Bool(string key, Vector3Bool vector3Bool)
    {
        SetBool(key + "x", vector3Bool.X);
        SetBool(key + "y", vector3Bool.Y);
        SetBool(key + "z", vector3Bool.Z);
    }



    /// <summary>
    /// Returns the value corresponding to key in the preference file if it exists.
    /// </summary>
    public static Vector3Bool GetVector3Bool(string key)
    {
        bool x = GetBool(key + "x", true);
        bool y = GetBool(key + "y", true);
        bool z = GetBool(key + "z", true);
        return new Vector3Bool(x, y, z);
    }



    /// <summary>
    /// Sets the value of the preference identified by key
    /// </summary>
    public static void SetVector2Bool(string key, Vector2Bool vector2Bool)
    {
        SetBool(key + "x", vector2Bool.X);
        SetBool(key + "y", vector2Bool.Y);
    }



    /// <summary>
    /// Returns the value corresponding to key in the preference file if it exists.
    /// </summary>
    public static Vector2Bool GetVector2Bool(string key)
    {
        bool x = GetBool(key + "x", true);
        bool y = GetBool(key + "y", true);
        return new Vector2Bool(x, y);
    }



    /// <summary>
    /// Sets the value of the preference identified by key
    /// </summary>
    public static void SetIntList(string key, List<int> myList)
    {
        for (int i = 0; i < myList.Count; i++)
        {
            PlayerPrefs.SetInt(key + i.ToString(), myList[i]);
        }
    }



    /// <summary>
    /// Returns the value corresponding to key in the preference file if it exists.
    /// </summary>
    public static List<int> GetIntList(string key)
    {
        int i = 0;
        List<int> myList = new List<int>();

        while (PlayerPrefs.HasKey(key + i.ToString()))
        {
            myList.Add(PlayerPrefs.GetInt(key + i.ToString()));
            i++;
        }

        return myList;
    }



    /// <summary>
    /// Sets the value of the preference identified by key
    /// </summary>
    public static void SetFloatList(string key, List<float> myList)
    {
        for (int i = 0; i < myList.Count; i++)
        {
            PlayerPrefs.SetFloat(key + i.ToString(), myList[i]);
        }
    }



    /// <summary>
    /// Returns the value corresponding to key in the preference file if it exists.
    /// </summary>
    public static List<float> GetFloatList(string key)
    {
        int i = 0;
        List<float> myList = new List<float>();

        while (PlayerPrefs.HasKey(key + i.ToString()))
        {
            myList.Add(PlayerPrefs.GetFloat(key + i.ToString()));
            i++;
        }

        return myList;
    }



    /// <summary>
    /// Sets the value of the preference identified by key
    /// </summary>
    public static void SetStringList(string key, List<string> myList)
    {
        for (int i = 0; i < myList.Count; i++)
        {
            PlayerPrefs.SetString(key + i.ToString(), myList[i]);
        }
    }



    /// <summary>
    /// Returns the value corresponding to key in the preference file if it exists.
    /// </summary>
    public static List<string> GetStringList(string key)
    {
        int i = 0;
        List<string> myList = new List<string>();

        while (PlayerPrefs.HasKey(key + i.ToString()))
        {
            myList.Add(PlayerPrefs.GetString(key + i.ToString()));
            i++;
        }

        return myList;
    }



    /// <summary>
    /// Sets the value of the preference identified by key
    /// </summary>
    public static void SetBoolList(string key, List<bool> myList)
    {
        for (int i = 0; i < myList.Count; i++)
        {
            SetBool(key + i.ToString(), myList[i]);
        }
    }



    /// <summary>
    /// Returns the value corresponding to key in the preference file if it exists.
    /// </summary>
    public static List<bool> GetBoolList(string key)
    {
        int i = 0;
        List<bool> myList = new List<bool>();

        while (PlayerPrefs.HasKey(key + i.ToString()))
        {
            myList.Add(GetBool(key + i.ToString()));
            i++;
        }

        return myList;
    }



    /// <summary>
    /// Returns true if key exists in the preferences.
    /// </summary>
    public static bool HasKeyEx(string key)
    {
        if (PlayerPrefs.HasKey(key))
            return true;

        bool hasX = PlayerPrefs.HasKey(key + "x");
        bool hasY = PlayerPrefs.HasKey(key + "y");
        bool hasZ = PlayerPrefs.HasKey(key + "z");

        if (hasX && hasY && hasZ)
            return true;
        else if (hasX && hasY)
            return true;

        bool hasR = PlayerPrefs.HasKey(key + "r");
        bool hasG = PlayerPrefs.HasKey(key + "g");
        bool hasB = PlayerPrefs.HasKey(key + "b");
        bool hasA = PlayerPrefs.HasKey(key + "a");

        if (hasR && hasG && hasB && hasA)
            return true;
        if (hasR && hasG && hasB)
            return true;

        bool hasMin = PlayerPrefs.HasKey(key + "min");
        bool hasMax = PlayerPrefs.HasKey(key + "max");

        if (hasMin && hasMax)
            return true;

        if (PlayerPrefs.HasKey(key + "0"))
            return true;

        return false;
    }



    /// <summary>
    /// Removes key and its corresponding value from the preferences.
    /// </summary>
    public static void DeleteKeyEx(string key)
    {
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.DeleteKey(key + "x");
        PlayerPrefs.DeleteKey(key + "y");
        PlayerPrefs.DeleteKey(key + "z");
        PlayerPrefs.DeleteKey(key + "r");
        PlayerPrefs.DeleteKey(key + "g");
        PlayerPrefs.DeleteKey(key + "b");
        PlayerPrefs.DeleteKey(key + "a");
        PlayerPrefs.DeleteKey(key + "min");
        PlayerPrefs.DeleteKey(key + "max");

        int i = 0;

        while (PlayerPrefs.HasKey(key + i.ToString()))
        {
            PlayerPrefs.DeleteKey(key + i.ToString());
            i++;
        }
    }
}

#endregion

#region MonoBehaviourEx

/// <summary>
/// An extended version of MonoBehaviour by Yevgeny Blinov
/// </summary>
public class MonoBehaviourEx : MonoBehaviour
{
    #region Timers

    List<Timer> Timers = new List<Timer>();



    /// <summary>
    /// Starts a timer which executes a function as soon as the timer ends. <para></para>
    /// TIP: If you're storing the timer into a variable which already has a running timer, stop the timer before you assign it a new value, or consider using the RestartTimer function <para></para>
    /// IMPORTANT: Deactivating the gameObject or the MonoBehaviourEx stops all the running timers, either one time or repeating, unless they are marked as Persistent <para></para>
    /// IMPORTANT: If the class you're calling the timer from, implemets the Update() function, it MUST override the base.Update() function, otherwise timers won't be updated
    /// </summary>
    public Timer StartTimer(float delay, UnityAction action, float repeatInterval = 0)
    {
        IsActiveAndEnabled = true;
        Timer timer = InitializeTimer(delay, action, repeatInterval);
        timer.Value = 0;
        return timer;
    }



    /// <summary>
    /// Restarts the timer and updates its parameters to the specified ones. If the timer doesn't exist, creates a new one with the specified parameters <para></para>
    /// IMPORTANT: Deactivating the gameObject or the MonoBehaviourEx stops all the running timers, either one time or repeating, unless they are marked as Persistent <para></para>
    /// IMPORTANT: If the class you're calling the timer from, implemets the Update() function, it MUST override the base.Update() function, otherwise timers won't be updated
    /// </summary>
    public Timer RestartTimer(Timer timer, float delay, UnityAction action, float repeatInterval = 0)
    {
        if (timer == null)
        {
            timer = StartTimer(delay, action, repeatInterval);
            return timer;
        }
        else
        {
            timer.Set(delay, action, repeatInterval);
            return RestartTimer(timer);
        }
    }



    /// <summary>
    /// Restarts an existing timer without changing its parameters. If the timer doesn't exist, the function does nothing <para></para>
    /// IMPORTANT: Deactivating the gameObject or the MonoBehaviourEx stops all the running timers, either one time or repeating, unless they are marked as Persistent <para></para>
    /// IMPORTANT: If the class you're calling the timer from, implemets the Update() function, it MUST override the base.Update() function, otherwise timers won't be updated
    /// </summary>
    public Timer RestartTimer(Timer timer)
    {
        IsActiveAndEnabled = true;

        if (timer.IsRepeatable)
        {
            timer.Delay = timer.RepeatInterval;
        }

        timer.Value = 0;
        return timer;
    }



    /// <summary>
    /// Stops the timer without executing the suspended function 
    /// </summary>
    public void StopTimer(Timer timer)
    {
        if (timer != null)
        {
            timer.Value = -1;
            timer.IsPersistent = false;
        }
    }



    /// <summary>
    /// Stops the timer and executes the suspended function 
    /// </summary>
    public void FinishTimer(Timer timer)
    {
        if (timer != null)
        {
            timer.Value = -1;
            timer.Action.Invoke();
            timer.IsPersistent = false;
        }
    }



    /// <summary>
    /// Stops all timers running on this behaviour without executing their suspended functions, ignoring persistent timers
    /// </summary>
    public void StopAllTimers()
    {
        foreach(Timer timer in Timers)
        {
            if (!timer.IsPersistent)
            {
                timer.Value = -1;
            }
        }
    }



    /// <summary>
    /// Stops all timers running on this behaviour and executes their suspended functions, ignoring persistent timers
    /// </summary>
    public void FinishAllTimers()
    {
        foreach (Timer timer in Timers)
        {
            if (!timer.IsPersistent)
            {
                FinishTimer(timer);
            }
        }
    }



    /// <summary>
    /// Are there any timers currently running? <para></para>
    /// IMPORTANT: If the class you're calling your timers from, implemets the Update() function, it MUST override the base.Update() function, otherwise timers won't be updated
    /// </summary>
    public bool HasRunningTimers
    {
        get
        {
            foreach(Timer timer in Timers)
            {
                if (timer.IsRunning)
                    return true;
            }

            return false;
        }
    }



    /// <summary>
    /// Wait for a timer to finish running <para></para>
    /// IMPORTANT: If the class you're calling your timers from, implemets the Update() function, it MUST override the base.Update() function, otherwise timers won't be updated
    /// </summary>
    public IEnumerator WaitForTimer(Timer timer)
    {
        yield return new WaitWhile(() => timer.IsRunning);
    }



    /// <summary>
    /// Wait for all timers to finish running<para></para>
    /// IMPORTANT: If the class you're calling your timers from, implemets the Update() function, it MUST override the base.Update() function, otherwise timers won't be updated
    /// </summary>
    public IEnumerator WaitForTimers()
    {
        yield return new WaitWhile(() => HasRunningTimers);
    }


    /// <summary>
    /// Must be overriden, otherwise timers will not be updated
    /// </summary>
    public virtual void Update()
    {
        foreach (Timer timer in Timers)
        {
            if (timer.IsRunning)
            {
                if (timer.IsUnscaledTime)
                {
                    timer.Value = timer.Value + Time.unscaledDeltaTime;
                }
                else
                {
                    timer.Value = timer.Value + Time.deltaTime;
                }
            }
            else if (!timer.IsStopped)
            {
                timer.Value = -1;
                timer.Action.Invoke();

                if (timer.IsRepeatable)
                {
                    RestartTimer(timer);
                }
                else
                {
                    timer.IsPersistent = false;
                }
            }
        }
    }



    /// <summary>
    /// Searches through the Timers list for a timer which isn't running. If none found, creates a new one. Similar to Object pooling
    /// </summary>
    Timer InitializeTimer(float delay, UnityAction action, float repeatInterval = 0)
    {
        Timer timer = Timers.Find(x => x.IsStopped);
        
        if (timer == null)
        {
            timer = new Timer(delay, action, repeatInterval);
            ExecuteAtEndOfFrame(() => Timers.Add(timer));
            timer.StoredBehaviour = this;
        }
        else
        {
            timer.Set(delay, action, repeatInterval);
        }
        
        return timer;
    }

    #endregion

    #region Mono

    void OnDisable()
    {
        StopAllTimers();
    }



    private void OnDestroy()
    {
        StopAllTimers();
    }



    private void Reset()
    {
        StopAllTimers();
    }

    #endregion

    #region Invokes and Coroutines extensions

    /// <summary>
    /// Do something at the end of the frame
    /// </summary>
    public void ExecuteAtEndOfFrame(UnityAction unityAction)
    {
        if (gameObject.activeSelf && gameObject.activeInHierarchy)
        {
            IsActiveAndEnabled = true;
            StartCoroutine(WaitForFrameEnd(unityAction));
        }
    }



    IEnumerator WaitForFrameEnd(UnityAction unityAction)
    {
        yield return new WaitForEndOfFrame();
        unityAction.Invoke();
    }



    /// <summary>
    /// Do something at the next frame
    /// </summary>
    public void ExecuteAtNextFrame(UnityAction unityAction)
    {
        if (gameObject.activeSelf && gameObject.activeInHierarchy)
        {
            IsActiveAndEnabled = true;
            StartCoroutine(WaitForNextFrame(unityAction));
        }
    }



    IEnumerator WaitForNextFrame(UnityAction unityAction)
    {
        yield return null;
        unityAction.Invoke();
    }



    /// <summary>
    /// Restarts a specified coroutine if it is running
    /// </summary>
    public void RestartCoroutine(IEnumerator coroutine)
    {
        StopCoroutine(coroutine);
        StartCoroutine(coroutine);
    }


    /// <summary>
    /// Restarts a specified coroutine if it is running
    /// </summary>
    public void RestartCoroutine(string coroutine)
    {
        StopCoroutine(coroutine);
        StartCoroutine(coroutine);
    }


    /// <summary>
    /// Restarts a specified Invoke if it is running
    /// </summary>
    public void RestartInvoke(string funcName, float delay)
    {
        if (IsInvoking(funcName))
        {
            CancelInvoke(funcName);
        }

        Invoke(funcName, delay);
    }


    /// <summary>
    /// Restarts a specified InvokeRepeating if it is running
    /// </summary>
    public void RestartInvokeRepeating(string funcName, float startDelay, float repeatingDelay)
    {
        if (IsInvoking(funcName))
        {
            CancelInvoke(funcName);
        }

        InvokeRepeating(funcName, startDelay, repeatingDelay);
    }

    #endregion

    #region Other

    public void ActivateCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }



    public void DeactivateCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    /// <summary>
    /// Activates/Deactivates the GameObject and enables/disables the behaviour
    /// </summary>
    public bool IsActiveAndEnabled
    {
        set
        {
            gameObject.SetActive(value);
            enabled = value;
        }
    }


    /// <summary>
    /// Returns the root GameObject
    /// </summary>
    public GameObject rootObject
    {
        get
        {
            return transform.root.gameObject;
        }
    }



    /// <summary>
    /// Check if the mouse was clicked over a UI element. Should be used with "this."prefix, for example this.IsPointerOverUI()
    /// </summary>
    public bool IsPointerOverUI()
    {
        // we don't want to fire when we interact with UI buttons for example. IsPointerOverGameObject really means IsPointerOver*UI*GameObject
        // notice we don't use on on GetbuttonUp() few lines down, because one can mouse down, move over a UI element and release, which would lead to not lower the isFiring Flag.
        return EventSystem.current.IsPointerOverGameObject();
    }


    /// <summary>
    /// Sets the specified bool to true, only if the given condition is true. Otherwise ignores the function
    /// </summary>
    public void SetTrueIf(ref bool thisBool, bool value)
    {
        if (value)
        {
            thisBool = true;
        }
    }


    /// <summary>
    /// Sets the specified bool to false, only if the given condition is true. Otherwise ignores the function
    /// </summary>
    public void SetFalseIf(ref bool thisBool, bool value)
    {
        if (value)
        {
            thisBool = true;
        }
    }

    #endregion
}

#endregion
