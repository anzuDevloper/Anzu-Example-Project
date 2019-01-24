using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayController : MonoBehaviour
{
    public static OverlayController Instance;
    Animator animator;
    bool isAlreadyDeactivated = false;

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    public void OnLinkClick()
    {
        Application.OpenURL("https://wiki.anzu.io/unity-integration-guide");
    }

    public void OnOverlayClick()
    {
        if (isAlreadyDeactivated)
            return;
        
        animator.SetTrigger("Deactivate");
        isAlreadyDeactivated = true;
    }

    /// <summary>
    /// Called via DeactivateOverlay animation event
    /// </summary>
    void DeactivateObject()
    {
        gameObject.SetActive(false);
    }

    public static bool IsActive
    {
        get
        {
            return Instance.gameObject.activeSelf;
        }
    }
}
