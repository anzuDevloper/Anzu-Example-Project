#define ANZU_SDK_USED

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;



[InitializeOnLoad]
public static class DefaultSceneLoader
{
    static Scene scene;
    static string DefaultScenePath = "Assets/Scenes/SampleScene.unity";
    static float TimeSinceStartupDelay = 12;
    static float DefaultSceneLoadDelay = 13;
    static float SDKNotificationDelay = 15;
    static bool IsNotificationShown = false;
    

    static bool IsProjectLoaded
    {
        get
        {
            return EditorApplication.timeSinceStartup >= TimeSinceStartupDelay;
        }
    }
    

    static bool CanLoadDefaultScene
    {
        get
        {
            return EditorApplication.timeSinceStartup >= DefaultSceneLoadDelay;
        }
    }

    
    static bool IsDefaultSceneLoaded
    {
        get
        {
            return EditorSceneManager.GetActiveScene().path == DefaultScenePath;
        }
    }

    
    static bool CanShowNotification
    {
        get
        {
            return EditorApplication.timeSinceStartup >= SDKNotificationDelay;
        }
    }


    /// <summary>
    /// Although is it supposed to run only once when Unity starts, it actually runs 3 times !!!
    /// </summary>
    static DefaultSceneLoader()
    {
        if (IsProjectLoaded)
        {
            EditorApplication.update += Update;
            Debug.Log("The project has been loaded");
        }
    }
    

    static void LoadDefaultScene()
    {
        if (!EditorApplication.isPlaying)
        {
            scene = EditorSceneManager.OpenScene(DefaultScenePath);
            Debug.Log(scene.path + " has been loaded");
        }
    }

    
    static void Update()
    {
        if (CanLoadDefaultScene && !IsDefaultSceneLoaded)
        {
            LoadDefaultScene();
        }

        if (CanShowNotification && !IsNotificationShown)
        {
            EditorApplication.update -= Update;
            DownloadSDKDialogueWindow.Init();
            IsNotificationShown = true;

            Debug.Log("Follow this link <Color=blue>" + DownloadSDKDialogueWindow.LinkSDK + "</Color> to download the latest version of the Anzu SDK");
        }
    }
}
