using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        PlacementStats.enabled = !PlacementStats.enabled;
    }
}
