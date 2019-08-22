using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class Voice : MonoBehaviour
{
    public float testSound;
    public static float MicLoudness;
    private string _device;
    private AudioClip _clipRecord = null;// = new AudioClip(); 
    private int _sampleWindow = 128;
    public float coor;
    public GameObject circus;
    public Vector3 speed;
    public float a = 1f; // Регулятор громкости
    [SerializeField]
    public float maxVoice = 1;
    public AudioSource audioSource;
    public float sensa;
    public static Voice self;

    void Awake()
    {
        self = this;
    }

    void Start()
    {
        Debug.Log("Запуск////////////////////////////////////////////////////////////////");
        MelodyPlayer.self.IncMus();
        InitMic();
    }

    public void InitMic()
    {
        Debug.Log("Микрофон///////////////////////////////////////////////////////////////");
        if (_device == null)
        {
            Debug.Log("Ещё что-то с микрофоном///////////////////////////////////////////////////////////////");
            _device = Microphone.devices[0];
            _clipRecord = Microphone.Start(_device, true, 999, 44100);
        }
        MelodyPlayer.self.PlaySong(_clipRecord);
        Debug.Log(_clipRecord);
    }

    void StopMicrophone()
    {
        Microphone.End(_device);
    }

    public float LevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1);
        if (micPosition < 0)
        {
            return 0;
        }
        _clipRecord.GetData(waveData, micPosition);
        for (int i = 0; i < _sampleWindow; ++i)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }

    public void slider(float x)
    {
        a = (x - 0.5f) * 2f;
    }


    public void Update()
    {
        transform.position += new Vector3(0, speed.y * Time.deltaTime * 10, 0);
        speed += new Vector3(0, LevelMax() * maxVoice - coor, 0);// Подобрать резкость и плавность
        speed = new Vector3(Mathf.Clamp(speed.x, -a, a), Mathf.Clamp(speed.y, -a, a), Mathf.Clamp(speed.z, -a, a));
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 4f), transform.position.z);
    }
}