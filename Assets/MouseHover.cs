using TMPro;
using UnityEngine;  
using System.Collections;  
using UnityEngine.EventSystems;  
using UnityEngine.UI;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public TextMeshProUGUI tmpText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        tmpText.color = Color.black; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tmpText.color = Color.white; 
    }
}