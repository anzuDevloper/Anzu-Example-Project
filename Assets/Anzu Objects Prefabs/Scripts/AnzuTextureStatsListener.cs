using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ANZU_SDK_USED

using anzu;

[RequireComponent(typeof(AnimatedTextureStats))]
public class AnzuTextureStatsListener : MonoBehaviour
{
    AnimatedTextureStats PlacementStats;


    private void Awake()
    {
        PlacementStats = GetComponent<AnimatedTextureStats>();
    }


    private void OnMouseDown()
    {
        if (!OverlayController.IsActive)
        {
            PlacementStats.enabled = !PlacementStats.enabled;
        }
    }
}

#endif
