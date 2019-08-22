using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{

    public GameObject prefab;//  тип обьекта внутри скрола (button)
    public GameObject parent; // координаты кнопки внутри скрола
    // Use this for initialization
    void Start()
    {
        /*foreach (AudioClip volum in Directory.selflink.music)//Переберается массив мьюзик
        {
          GameObject go =  Instantiate(prefab,parent.transform);// Обьекту go присвается одно значения prefab и parent
            Debug.Log(volum.name);
        }
        */
        for (int i = 0; i < Direct.selflink.music.Length; i++)
        {
            GameObject go = Instantiate(prefab, parent.transform);// Обьекту go присвается одно значения prefab и parent
            go.GetComponent<Click>().index = i;
            go.GetComponent<Click>()._name.text = Direct.selflink.music[i].name;// text это поле срипта Text в инспекторе | Передаем в этот текст поле имени из массива music
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
