#define ANZU_SDK_USED

using System;
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
    static int counter = 0;

    static DefaultSceneLoader()
    {
        EditorApplication.update += Update;
        DownloadSDKDialogueWindow.Init();
    }

    static void LoadSampleScene()
    {
        if (!EditorApplication.isPlaying)
        {
            scene = EditorSceneManager.OpenScene("Assets/Scenes/SampleScene.unity");
            Debug.Log(scene.path + " has been loaded");
        }
    }

    static void Update()
    {
        counter++;

        if (counter == 1)
        {
            LoadSampleScene();
            EditorApplication.update -= Update;
        }
    }
}
