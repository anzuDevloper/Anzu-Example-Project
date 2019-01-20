using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayController : MonoBehaviour
{
    Animator animator;
    bool isAlreadyDeactivated = false;

    private void Awake()
    {
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

    void DeactivateObject()
    {
        gameObject.SetActive(false);
    }
}
