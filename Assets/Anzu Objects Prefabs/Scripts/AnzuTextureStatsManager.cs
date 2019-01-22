using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anzu;


public class AnzuTextureStatsManager : MonoBehaviour
{
    [HideInInspector] public AnimatedTextureStats[] PlacementsStats;

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

            foreach (AnimatedTextureStats stats in PlacementsStats)
            {
                stats.enabled = showTextureStats;
            }
        }
    }

    private void Awake()
    {
        PlacementsStats = FindObjectsOfType<AnimatedTextureStats>();

        foreach (AnimatedTextureStats stats in PlacementsStats)
        {
            if (stats.GetComponent<AnzuTextureStatsListener>() == null)
            {
                stats.gameObject.AddComponent<AnzuTextureStatsListener>();
            }
        }
    }

    private void Start()
    {
        ShowTextureStats = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowTextureStats = !ShowTextureStats;
        }
    }
}
