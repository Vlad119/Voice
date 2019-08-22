using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Phrases
{
    public float time;
    public string text;

}

[Serializable]
public class Songs
{
    public List<Phrases> phrases = new List<Phrases>();
    public string songName;
}

[Serializable]
public class List_songs
{
    public List<Songs> songs = new List<Songs>();
}

public class OverFon : MonoBehaviour
{
    public static OverFon instance;
    public GameObject cube;
    public GameObject sphere;

    void Start()
    {
        instance = this;
    }
}
