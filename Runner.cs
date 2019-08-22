using System;
using System.IO;
using UnityEngine;
using System.Linq;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.UI;
using System.Diagnostics;

public class Runner : MonoBehaviour
{

    public List_songs list_songs;
    public static Runner self;
    public static float distanceTraveled; //пройденное расстояние (необходимо для генерации препятствий)
    public TMP_Text songtext; // фразы из песен
    public float speed = 5;
    public GameObject prefab;
    public Transform textParent;
    public string pathToReserve;
    public string[] rsrvFiles;
    public LoopScrollRect loopScrool;
    public GameObject finishLine;
    public GameObject cube;
    public string path;
    public int GetReservedSpace()
    {
        UpdateFilesState();
        return rsrvFiles.Length;
    }

    private void UpdateFilesState()
    {
        var sorted = Directory.GetFiles(pathToReserve);
        rsrvFiles = sorted.ToArray();
    }


    private void Awake()
    {

#if UNITY_EDITOR_WIN
        path = (Application.dataPath.Replace("/Assets", "") + "").Replace("/", "\\");
#elif UNITY_STANDALONE_WIN
        path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "";
#endif
    }

    async void Start()
    {
        self = this;
        await ReadFiles(); //чтение из файла
        CreateText(); //генерация текста (можно и с файла и с эдитора)
        InitialiseReserveDirectory();  //запись текста песен в файл
    }

    void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * speed, 0f, 0f);
        distanceTraveled = transform.position.x;
    }

    public void CreateText()
    {
        foreach (Transform txt in textParent)
        {
            Destroy(txt.gameObject);
        }

        foreach (Phrases phrases in list_songs.songs[MelodyPlayer.self.value].phrases) //перебираем все фразы из листа всех возможных песен
        {
            var inst = Instantiate(prefab, new Vector3(phrases.time * (Time.fixedDeltaTime * 50) * speed, -4.1f, 85), Quaternion.identity);
         //   var instanse = Instantiate(cube, new Vector3(phrases.time * (Time.fixedDeltaTime * 50) * speed+5f, -3.2f, 86),Quaternion.identity);
            {
                inst.GetComponent<TextMeshPro>().text = phrases.text;
                inst.transform.SetParent(textParent);
             //   instanse.GetComponent<Transform>().Rotate(0f, 0f, 180f);
           //     instanse.GetComponent<Transform>().localScale = new Vector3(UnityEngine.Random.Range(4f, 5f)+5f, UnityEngine.Random.Range(0f, 5f), 0f);
            }
        }
    }

    public void NewCube()
    {
     //   var instanse = cube;
       // instanse.GetComponent<Transform>().localScale = new Vector3(UnityEngine.Random.Range(0f,5f), UnityEngine.Random.Range(0f, 5f), 0f);
   }

    private void InitialiseReserveDirectory()
    {
        pathToReserve = path;
//        pathToReserve = Application.persistentDataPath + "/SongText";
        
        if (!Directory.Exists(pathToReserve) || GetReservedSpace() == 0)
        {
            Directory.CreateDirectory(pathToReserve);
            CreateFile();
        }
    }

    public void CreateFile()
    {
        
        var songText = JsonUtility.ToJson(list_songs, true);
        
        pathToReserve = path;
        // pathToReserve = Application.persistentDataPath + "/SongText";
        StreamWriter writer = new StreamWriter(pathToReserve + "/" + "songtext" + ".txt", false);
        writer.Write(songText);
        writer.Close();
    }

    private async Task ReadFiles()
    {
        pathToReserve = path;
        //
        //pathToReserve = Application.persistentDataPath + "/SongText";

        if (Directory.Exists(pathToReserve) || GetReservedSpace() != 0)
        {
            await FileRead();
        }
        loopScrool.totalCount = list_songs.songs.Count;
        loopScrool.RefillCells();
    }

    public async Task FileRead()
    {
        char[] buffer;
        pathToReserve = path + @"\songtext.txt";
        //pathToReserve = Application.persistentDataPath + "/SongText";
        using (var sr = new StreamReader(pathToReserve , true))
        {
            print(sr.BaseStream.Length);
            buffer = new char[sr.BaseStream.Length];
            await sr.ReadAsync(buffer, 0, (int)sr.BaseStream.Length);
        }
        string readed = new string(buffer);
        JsonUtility.FromJsonOverwrite(readed, list_songs);
    }
}

