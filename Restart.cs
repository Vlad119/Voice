using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
//using Debug = UnityEngine.Debug;

public class Restart : MonoBehaviour
{
    public static Restart self;
    public struct LevlData
    {
        public Texture2D Teh;
        public AudioClip Aud;
    };

    public static LevlData levlData;

    public static void RestartGame()
    {
        MelodyPlayer.self.PlayMelody(Direct.selflink.mem);
        OverFon.instance.sphere.transform.localPosition = new Vector3(0f, 0, 7f);
        Camera.main.transform.position = new Vector3(0, 0, 0);
        OverFon.instance.sphere.SetActive(true);
        try { Runner.self.CreateText(); }
        catch { }
    }

    public void Start()
    {
        self = this;
    }
}
