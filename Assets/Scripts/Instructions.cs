using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    public Text IndividualInstruction;
    public Text GeneralInstruction;

    public string IndividualInstruction_PC = "";
    public string IndividualInstruction_XBOX = "";
    public string IndividualInstruction_PS4 = "";
    public string IndividualInstruction_Android = "";
    public string IndividualInstruction_iOS = "";

    public string GeneralInstruction_PC = "";
    public string GeneralInstruction_XBOX = "";
    public string GeneralInstruction_PS4 = "";
    public string GeneralInstruction_Android = "";
    public string GeneralInstruction_iOS = "";

    string CurrentIndividualInstruction = "";
    string CurrentGeneralInstruction = "";


    public bool IsPC
    {
        get
        {
            return Application.platform == RuntimePlatform.WindowsEditor || Application.isEditor
                || Application.platform == RuntimePlatform.WindowsPlayer;
        }
    }


    public bool IsXBOX
    {
        get
        {
            return Application.platform == RuntimePlatform.XboxOne;
        }
    }


    public bool IsPS4
    {
        get
        {
            return Application.platform == RuntimePlatform.PS4;
        }
    }


    public bool IsAndroid
    {
        get
        {
            return Application.platform == RuntimePlatform.Android;
        }
    }


    public bool IsiOS
    {
        get
        {
            return Application.platform == RuntimePlatform.IPhonePlayer;
        }
    }


    private void Awake()
    {
        if (IsPC)
        {
            CurrentIndividualInstruction = IndividualInstruction_PC;
            CurrentGeneralInstruction = GeneralInstruction_PC;
        }
        else if (IsAndroid)
        {
            CurrentIndividualInstruction = IndividualInstruction_Android;
            CurrentGeneralInstruction = GeneralInstruction_Android;
        }
        else if (IsiOS)
        {
            CurrentIndividualInstruction = IndividualInstruction_iOS;
            CurrentGeneralInstruction = GeneralInstruction_iOS;
        }
        else if (IsXBOX)
        {
            CurrentIndividualInstruction = IndividualInstruction_XBOX;
            CurrentGeneralInstruction = GeneralInstruction_XBOX;
        }
        else if (IsPS4)
        {
            CurrentIndividualInstruction = IndividualInstruction_PS4;
            CurrentGeneralInstruction = GeneralInstruction_PS4;
        }
    }


    private void Start()
    {
        IndividualInstruction.enabled = false;
        GeneralInstruction.enabled = false;

        if (CurrentIndividualInstruction != "")
        {
            IndividualInstruction.enabled = true;
            IndividualInstruction.text = CurrentIndividualInstruction + " on an ad space to toggle its Viewability Stats";
        }

        if (CurrentGeneralInstruction != "")
        {
            GeneralInstruction.enabled = true;
            GeneralInstruction.text = CurrentGeneralInstruction + " to toggle Viewability Stats on ALL ad spaces";
        }
    }
}
