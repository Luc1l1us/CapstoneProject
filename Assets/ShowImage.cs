using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowImage : MonoBehaviour
{
    public Image control;
    public CanvasGroup Settings;
    private bool isVisible = false;

    void Awake()
    {
        control.enabled = false;
    }

    
    public void ToggleVisibility()
    {

        isVisible = !isVisible;

        if (isVisible) {
            Settings.alpha = 0f;
            Settings.blocksRaycasts = false;
        } else {
            HideCanvasGroup();
        }

        if (control != null)
        {
            control.enabled = !control.enabled;
        }
    }

     public void HideCanvasGroup() {
        isVisible = false;
        Settings.alpha = 0f;
        Settings.blocksRaycasts = false;
    } 
}
