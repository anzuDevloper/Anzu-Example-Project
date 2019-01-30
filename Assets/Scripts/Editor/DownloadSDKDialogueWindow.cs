//using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class RandomizeInSelection : EditorWindow
{
    public const string Description = "Please follow the link below to download the latest version of Anzu SDK, \nor click the Download SDK button.";
    public const string LinkSDK = "https://s3.amazonaws.com/anzu-sdk/anzu-latest.unitypackage";

    GUIStyle descriptionGuiStyle = new GUIStyle();
    GUIStyle linkGuiStyle = new GUIStyle();
    GUIStyle buttonFirstGuiStyle = new GUIStyle();
    GUIStyle buttonSecondGuiStyle = new GUIStyle();

    //[MenuItem("Example/No SDK found")]
    public static void Init()
    {
        RandomizeInSelection window = ScriptableObject.CreateInstance(typeof(RandomizeInSelection)) as RandomizeInSelection;
        window.titleContent.text = "No SDK found";
        window.minSize = new Vector2(500, 200);
        window.maxSize = new Vector2(500, 200);
        window.ShowUtility();
    }
    
    void OnGUI()
    {
        buttonFirstGuiStyle = new GUIStyle(GUI.skin.button);
        buttonSecondGuiStyle = new GUIStyle(GUI.skin.button);

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        descriptionGuiStyle.alignment = TextAnchor.MiddleLeft;
        descriptionGuiStyle.padding = new RectOffset(25, 25, 0, 0);
        EditorGUILayout.LabelField(Description, descriptionGuiStyle);

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        linkGuiStyle = EditorStyles.textArea;
        linkGuiStyle.normal.textColor = Color.blue;
        linkGuiStyle.active.textColor = Color.blue;
        linkGuiStyle.focused.textColor = Color.blue;
        linkGuiStyle.fontStyle = FontStyle.Bold;
        linkGuiStyle.alignment = TextAnchor.MiddleCenter;
        linkGuiStyle.margin = new RectOffset(25, 25, 10, 10);
        linkGuiStyle.fixedWidth = 450;
        linkGuiStyle.fixedHeight = 30;
        EditorGUILayout.TextArea(LinkSDK, linkGuiStyle);

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        buttonFirstGuiStyle.alignment = TextAnchor.MiddleCenter;
        buttonFirstGuiStyle.margin = new RectOffset(200, 200, 0, 0);

        if (GUILayout.Button("Download SDK", buttonFirstGuiStyle, GUILayout.Width(100), GUILayout.Height(30)))
        {
            Application.OpenURL("https://s3.amazonaws.com/anzu-sdk/anzu-latest.unitypackage");
            Close();
        }

        buttonSecondGuiStyle.alignment = TextAnchor.MiddleCenter;
        buttonSecondGuiStyle.margin = new RectOffset(200, 200, 10, 0);

        if (GUILayout.Button("Later", buttonSecondGuiStyle, GUILayout.Width(100), GUILayout.Height(30)))
        {
            Close();
        }
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }
}



//public class ShowPopupExample : EditorWindow
//{
//    [MenuItem("Example/ShowPopup Example")]
//    static void Init()
//    {
//        ShowPopupExample window = ScriptableObject.CreateInstance<ShowPopupExample>();
//        window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
//        window.ShowPopup();
//    }

//    void OnGUI()
//    {
//        EditorGUILayout.LabelField("This is an example of EditorWindow.ShowPopup", EditorStyles.wordWrappedLabel);
//        EditorGUILayout.LabelField("This is an example of EditorWindow.ShowPopup", EditorStyles.radioButton);
//        EditorGUILayout.LabelField("This is an example of EditorWindow.ShowPopup", EditorStyles.objectField);
//        EditorGUILayout.LabelField("This is an example of EditorWindow.ShowPopup", EditorStyles.colorField);
//        GUILayout.Space(70);
//        if (GUILayout.Button("Agree!")) this.Close();
//    }
//}



public class DownloadSDKDialogueWindow : EditorWindow
{
    public static void Init()
    {
        //if (!NamespaceExists("anzu"))
        //{
        //    OfferToDownloadSDK();
        //}
    }

    //public static bool NamespaceExists(string desiredNamespace)
    //{
    //    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
    //    {
    //        foreach (Type type in assembly.GetTypes())
    //        {
    //            if (type.Namespace == desiredNamespace)
    //                return true;
    //        }
    //    }
    //    return false;
    //}

    //static void OfferToDownloadSDK()
    //{
    //    bool dialogueWindow = EditorUtility.DisplayDialog("No Anzu SDK found",
    //    "Do you want to download the latest version of the Anzu SDK? https://s3.amazonaws.com/anzu-sdk/anzu-latest.unitypackage",
    //    "Download SDK",
    //    "Later");

    //    if (dialogueWindow)
    //    {
    //        Application.OpenURL("https://s3.amazonaws.com/anzu-sdk/anzu-latest.unitypackage");
    //    }
    //    else
    //    {
    //        Debug.Log("Follow this link <Color=blue>https://s3.amazonaws.com/anzu-sdk/anzu-latest.unitypackage</Color> to download the latest version of the Anzu SDK");
    //    }
    //}
}
