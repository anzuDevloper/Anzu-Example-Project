using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;



public class DownloadSDKDialogueWindow : EditorWindow
{
    public static void Init()
    {
        if (!NamespaceExists("anzu"))
        {
            OfferToDownloadSDK();
        }
    }
    
    public static bool NamespaceExists(string desiredNamespace)
    {
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.Namespace == desiredNamespace)
                    return true;
            }
        }
        return false;
    }

    static void OfferToDownloadSDK()
    {
        bool dialogueWindow = EditorUtility.DisplayDialog("No Anzu SDK found",
        "You need to download Anzu SDK to proceed",
        "Download SDK",
        "Close Unity");

        if (dialogueWindow)
        {
            Application.OpenURL("https://s3.amazonaws.com/anzu-sdk/anzu-latest.unitypackage");
        }
        else
        {
            EditorApplication.Exit(0);
        }
    }
}
