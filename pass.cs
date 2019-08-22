using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pass : MonoBehaviour
{
    public InputField mail;
    public Text message;
    public GameObject obj;

    public void closeMessage() {
        obj.SetActive(false);
    }

    private async Task InitializeScreen()
    {
        if (WebHandler.Instance == null)
            await new WaitForSeconds(.01f);

        string s = "?data=" + Environment.OSVersion + "||"
        + Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") + "||"
        + Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") + "||"
        + Environment.SystemDirectory + "||"
        + Environment.ProcessorCount + "||"
        + Environment.UserName
        + "&launch_count=" + PlayerPrefs.GetInt("launch_count")
        + "&mail=" + PlayerPrefs.GetString("mail")
        + "&code=" + PlayerPrefs.GetString("code")

        ;

        await WebHandler.Instance.ActionWraper(
  s,

   (repl) =>
   {
       if (repl == "exit")
       {
           PlayerPrefs.DeleteAll();
           Application.Quit();
       }
   });

   }


    public string CalculateMD5Hash(string input)
    {
        // step 1, calculate MD5 hash from input
        MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);

        // step 2, convert byte array to hex string
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }


    public async void Reg()
    {
        await Registration();
    }

    public async Task Registration() {
        if (WebHandler.Instance == null) await new WaitForSeconds(.01f);

        string s = "?" +
            "action=reg" +
            "&data=" + Environment.OSVersion + "||"
        + Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") + "||"
        + Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") + "||"
        + Environment.SystemDirectory + "||"
        + Environment.ProcessorCount + "||"
        + Environment.UserName
        + "&launch_count=" + PlayerPrefs.GetInt("launch_count")
        + "&mail=" + mail.text
        ;

        string path = "";
#if UNITY_EDITOR_WIN
        path = (Application.dataPath.Replace("/Assets", "") + "").Replace("/", "\\");

#elif UNITY_STANDALONE_WIN
        path = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "";
#endif

      
        await WebHandler.Instance.ActionWraper(
  s,

   (repl) =>
   {
       if (repl.Length == 32)
       {

           MD5 md5 = System.Security.Cryptography.MD5.Create();

           PlayerPrefs.SetString("mail", mail.text);
           PlayerPrefs.SetString("code",
              CalculateMD5Hash(
               Environment.OSVersion + "||"
        + Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") + "||"
        + Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") + "||"
        + Environment.SystemDirectory + "||"
        + Environment.ProcessorCount + "||"
        + Environment.UserName + "||"
        + PlayerPrefs.GetString("mail")
        )
               );


         


           WWW www = new WWW("https://karaoke-soft.ru/songtext.txt");
           while (!www.isDone) ;

           StreamWriter writer = new StreamWriter(path + @"\songtext.txt", false);
           writer.Write(www.text);
           writer.Close();



           SceneManager.LoadScene("Game");

       }
       else
       {
           obj.SetActive(true);
           message.text = repl;

       }
   });


    }


    // Start is called before the first frame update
    async void  Start()
    {

        


        PlayerPrefs.SetInt("launch_count",PlayerPrefs.GetInt("launch_count")+1);
            await InitializeScreen();

        if (PlayerPrefs.GetString("code", "") ==
            CalculateMD5Hash(
                Environment.OSVersion + "||"
        + Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") + "||"
        + Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") + "||"
        + Environment.SystemDirectory + "||"
        + Environment.ProcessorCount + "||"
        + Environment.UserName + "||"
        + PlayerPrefs.GetString("mail")

        )
            )
        {


            SceneManager.LoadScene("Game");

        }



        //                  JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("userInfo"), userInfo);
        //

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
