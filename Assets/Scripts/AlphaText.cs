
/// <summary>
/// This script use to fade GUI
/// </summary>

using UnityEngine.UI; 
using UnityEngine;
using System.Collections;

public class AlphaText : MonoBehaviour {
 
 public float speedFade;
 private float count;
 public Image image;
 public GameObject GameObject;
 
 // Use this for initialization
 void Start () {
 
 }
 
 // Update is called once per frame
 void Update () {
  
  
  //Fade in-out press start
  count += speedFade * Time.deltaTime;
  
    image = GameObject.GetComponent<Image>();

  image.color = new Color(1.5f,1.5f,1.5f,Mathf.Sin(count)*1*.5f);

 }
}
