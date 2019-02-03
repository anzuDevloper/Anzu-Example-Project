using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnzuTextureStatsManager : MonoBehaviour
{
    [HideInInspector] public MonoBehaviour[] PlacementsStats;

    bool showTextureStats = false;
    public bool ShowTextureStats
    {
        get
        {
            return showTextureStats;
        }
        set
        {
            showTextureStats = value;

            foreach (MonoBehaviour stats in PlacementsStats)
            {
                stats.enabled = showTextureStats;
            }
        }
    }


    private void Awake()
    {
        System.Type animatedTextureStats = System.Type.GetType("AnimatedTextureStats");

        if (animatedTextureStats != null)
        {
            PlacementsStats = FindObjectsOfType(animatedTextureStats) as MonoBehaviour[];

            foreach (MonoBehaviour stats in PlacementsStats)
            {
                if (stats.GetComponent<AnzuTextureStatsListener>() == null)
                {
                    stats.gameObject.AddComponent<AnzuTextureStatsListener>();
                }
            }
        }
    }


    private void Start()
    {
        ShowTextureStats = false;
    }


    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && !OverlayController.IsActive)
        {
            ShowTextureStats = !ShowTextureStats;
        }
    }
}
