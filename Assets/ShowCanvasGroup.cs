using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowCanvasGroup : MonoBehaviour
{
    public CanvasGroup image;
    private bool isVisible = false;
    public Image controls;

    void Awake()
    {
        image.alpha = image.alpha = 0;
    }

    public void ToggleVisiblity()
    {
        controls.enabled = isVisible;

        if (isVisible) {
            HideImage();
        }

        if (image != null)
        {
            image.alpha = image.alpha > 0 ? 0 : 1; 
            image.blocksRaycasts = image.alpha > 0;
        }
    }

    public void HideImage() {
        isVisible = false;
        controls.enabled = false;
    }
}
