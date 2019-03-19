using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnzuTextureStatsManager : MonoBehaviour
{
    List<MonoBehaviour> PlacementsStats = new List<MonoBehaviour>();

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
        AnzuTextureStatsListener[] statsListeners = FindObjectsOfType<AnzuTextureStatsListener>();
        MonoBehaviour stats = null;

        foreach (AnzuTextureStatsListener statsListener in statsListeners)
        {
            stats = statsListener.GetComponent("AnimatedTextureStats") as MonoBehaviour;

            if (stats != null)
            {
                PlacementsStats.Add(stats);
            }
        }
    }


    private void Start()
    {
        ShowTextureStats = false;
    }


    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Toggle Stats")) && !OverlayController.IsActive)
        {
            ShowTextureStats = !ShowTextureStats;
        }
    }
}
