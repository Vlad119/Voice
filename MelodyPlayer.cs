using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class MelodyPlayer : MonoBehaviour
{
    public static MelodyPlayer self;
    public AudioSource source;
    public AudioSource song;
    public AudioClip[] music;
    public string patch = @"";
    public Text songtext;

    public void PlaySong(AudioClip clip)
    {
        Debug.Log("PlaySong");
        song.clip = clip;
        song.Play();
    }

    private void Awake()
    {
        self = this;
    }

    public int value;
    public void PlayMelody(int val)
    {
        source.clip = Direct.selflink.clipIms[val].clip;
        value = val;
        source.Play();
    }

    public void PlaySong()
    {
        song.clip = Resources.Load<AudioClip>("Louse");
        song.Play();
    }

    public void IncMus()
    {
        Debug.Log("PlaySongICNkrhqqojkf");
        Direct.selflink.IncMusic();
        Voice.self.InitMic();
        Restart.RestartGame();
    }
}