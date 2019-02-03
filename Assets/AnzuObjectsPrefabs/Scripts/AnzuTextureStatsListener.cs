using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnzuTextureStatsListener : MonoBehaviour
{
    MonoBehaviour PlacementStats;
    
    private void Awake()
    {
        PlacementStats = GetComponent("AnimatedTextureStats") as MonoBehaviour;
    }
    
    private void OnMouseDown()
    {
        if (!OverlayController.IsActive && (PlacementStats != null))
        {
            PlacementStats.enabled = !PlacementStats.enabled;
        }
    }
}
