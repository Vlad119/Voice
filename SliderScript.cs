using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {

    public static SliderScript self;
    AudioSource audios;
    Slider slider;
    public AudioClip song;
    public bool show = true;
    // Use this for initialization
    void Start()
    {
        audios = MelodyPlayer.self.source;
        slider = GetComponent<Slider>();
        audios.clip = song;
        slider.minValue = 0;
        slider.maxValue = song.length;       
    }

    // Update is called once per frame
    void Update()
    {
        if (show)
        slider.value = audios.time;

    }
    public void meytod(float x) { }

    public void MovePosition()
    {
        if ((audios.time < audios.clip.length)&&(slider.value < audios.clip.length))
        
             audios.time = slider.value;
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        show = false;
        //throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        audios.time = slider.value;
        MoveEnemy.self.transform.position = new Vector3(((-slider.value * 3.15f)), MoveEnemy.self.transform.position.y, MoveEnemy.self.transform.position.z);
        show = true;
       

    }
}
