using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using System.Diagnostics;

[Serializable]
public class ClipIm
{
       public AudioClip clip;
    public ClipIm( AudioClip _clip)
    {
        clip = _clip;
    }
}

public class Direct : MonoBehaviour
{
    public AudioSource audioSource;
    public static Direct selflink; // Не зависит от экземпляра будет у всех
    public AudioClip[] music;
    public List<ClipIm> clipIms = new List<ClipIm>();
    public int mem = 0;
    //  string path = "file://";

    public void Some(int val)
    {// Делает музыку по наатию на кнопку
        print(val);
        audioSource.clip = clipIms[val].clip;//То что будет воспроизводиться присваеваем из масиива какойто клип по номеру val который находится в скрипте click и  обращается по  some
        //audioSource.Play();
        mem = val;
    }

    public void IncMusic(bool pol = true)
    {
        if (pol)
        {
            mem++;
            if (mem > music.Length - 1)
            {
                mem = 0;

            }
        }
        MelodyPlayer.self.PlayMelody(mem);
    }

    private void Awake()// Событие до старта
    {
        DontDestroyOnLoad(gameObject);// Будет переноситься  со сцены на сцену
        if (!selflink) selflink = this; // Если selflink пустой  то присваевается значение
        else Destroy(gameObject);// Возвращает обьект на корторм висит
        Loader();
    }

    public void Loader()
    {
        List<AudioClip> audioClips = LoaderClips().ToList<AudioClip>(); //new List<AudioClip>();
        List<Texture2D> texture = LoaderTex().ToList<Texture2D>();
        for (int i = 0; i < audioClips.ToArray().Length; i++)
        {
            UnityEngine.Debug.Log(i);
            clipIms.Add(new ClipIm(audioClips[i]));
        }
    }

    public AudioClip[] LoaderClips()
    {
        List<AudioClip> audioClips = new List<AudioClip>();
        var path = "";


#if UNITY_EDITOR_WIN
        path = (Application.dataPath.Replace("/Assets", "") + "/Music").Replace("/", "\\");
        UnityEngine.Debug.Log("UNITY_EDITOR_WIN");

#elif UNITY_STANDALONE_WIN
        path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\Music";
        //path = Application.persistentDataPath + "\\Music";
        print(path);
        UnityEngine.Debug.Log("UNITY_STANDALONE_WIN");

#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
        path = Application.dataPath.Replace("/Assets", "") + "/Music";
        UnityEngine.Debug.Log("UNITY_STANDALONE_OSX");
#else
        path = Application.dataPath.Replace("/Assets", "").Replace("/voice.app/Contents","") + "/Music";
        UnityEngine.Debug.Log("!UNITY_!!!!");
#endif

        UnityEngine.Debug.Log("path use:" + path);

        if (System.IO.Directory.Exists(path))
        {
            var files = System.IO.Directory.GetFiles(path);
            foreach (var file in files)
                if (file.Contains(".wav") && !file.Contains(".meta"))
                {

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
                    var www = new WWW("file://" + file);
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
                var www = new WWW( "file://"+ file);      
#else
                var www = new WWW( "file://"+ file);
#endif


                    while (!www.isDone) ;
                    var audioClip = www.GetAudioClip();

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
                    audioClip.name = file.Replace(path + "\\", "").Replace(".wav", "");
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
                audioClip.name = file.Replace(path + "/", "").Replace(".wav", "");     
#else
               audioClip.name = file.Replace(path + "\\", "").Replace(".wav", "");
#endif

                    // UnityEngine.Debug.Log(file);
                    audioClips.Add(audioClip);
                }
        }
        else UnityEngine.Debug.Log("Not found");
        return audioClips.ToArray();
    }

    public Texture2D[] LoaderTex()
    {
        List<Texture2D> texture = new List<Texture2D>();

        var path = "";


#if UNITY_EDITOR_WIN
        path = (Application.dataPath.Replace("/Assets", "") + "/Image").Replace("/", "\\");
        UnityEngine.Debug.Log("UNITY_EDITOR_WIN");

#elif UNITY_STANDALONE_WIN
        path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\Image";
        UnityEngine.Debug.Log("UNITY_STANDALONE_WIN");

#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
        path = Application.dataPath.Replace("/Assets", "") + "/Image";
        UnityEngine.Debug.Log("UNITY_STANDALONE_OSX");
#else
        path = Application.dataPath.Replace("/Assets", "").Replace("/voice.app/Contents","") + "/Image";
        
        UnityEngine.Debug.Log("!$$$$$$!!!!");
#endif


        UnityEngine.Debug.Log("!!!!!!!!!!!!!!");

        UnityEngine.Debug.Log(path);
        if (System.IO.Directory.Exists(path))
        {
            var files = System.IO.Directory.GetFiles(path);
            foreach (var file in files)
                if (file.Contains(".png") && !file.Contains(".mega"))
                {

#if UNITY_STANDALONE_WIN
                    var www = new WWW("file://" + file);
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
                var www = new WWW( "file://"+ file);     
#else
                var www = new WWW( "file://"+ file);
#endif


                    UnityEngine.Debug.Log(file);

                    while (!www.isDone) ;
                    var textures = www.texture;
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
                    textures.name = file.Replace(path + "\\", "").Replace(".png", "");
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
                textures.name = file.Replace(path + "/", "").Replace(".png", "");     
#else
               textures.name = file.Replace(path + "\\", "").Replace(".png", "");
#endif

                    texture.Add(textures);
                }
        }
        else UnityEngine.Debug.Log("Not found");

        //        foreach (var t in texture) Debug.Log(t.);

        return texture.ToArray();

        // Update is called once per frame
    }
}
