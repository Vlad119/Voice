using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ReedText : MonoBehaviour {
string path = @"C:\Users\ADMIN\Documents\Project_Unity\Voice\Assets\Resources\Text\1.txt";
Text text;
string line;

    // Use this for initialization
    void Start()
    {


        using (StreamReader sr = new StreamReader(path))
        {
            line = sr.ReadToEnd();
        Debug.Log("1"+line);

        }
        text = GetComponent<Text>();
        text.text = line;
        Debug.Log("2" + text.text);

    }

   
}
