using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.IO;
using System.Diagnostics;

public class Controller : MonoBehaviour
{
    public static Controller self;
    public GameObject fon_timer;
    public float chustitelnost;
    public GameObject soundList;
    public UnityAction postAct = null;

    public List<GameObject> list_buttons;

    public void SetIndex(int xz)
    {
        Direct.selflink.mem = xz;
        RunTimer("restart");
        //    Restart.RestartGame();
    }

    public void RunTimer(string act, UnityAction Pact = null)
    {
        StartCoroutine(Timer(act));
        postAct = Pact;
    }
    public IEnumerator Timer(string act)
    {
        fon_timer.gameObject.SetActive(true);
        GetComponent<RayCast>().Some[3].SetActive(false);


        int i = 3;
        while (i > 0)
        {
            fon_timer.GetComponentInChildren<Text>().text = i + "";
            yield return new WaitForSeconds(1f);
            i--;
        }
        fon_timer.gameObject.SetActive(false);
        if (act == "restart") Restart.RestartGame();
        if (act == "next_level") MelodyPlayer.self.IncMus();
        if (postAct != null) postAct();
    }


    void Awake()
    {
        self = this;
        chustitelnost = 0.5f;
    }
    // Use this for initialization
    void Start()
    {

    }

    public void ValListButtion()
    {
        for (int i = 0; i < list_buttons.ToArray().Length; i++)
        {
            try
            {
                if (i < Direct.selflink.clipIms.ToArray().Length) list_buttons[i].SetActive(true);
                list_buttons[i].GetComponentInChildren<Text>().text = Direct.selflink.clipIms[i].clip.name;
            }
            catch { list_buttons[i].SetActive(false); }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //            Restart.RestartGame();
            StartCoroutine(Timer("restart"));

        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            StartCoroutine(Timer("next_level"));

        }




        if (Input.GetKeyDown(KeyCode.Z))
        {
            chustitelnost -= 0.1f;
            Voice.self.slider(chustitelnost);
            //   Debug.Log(chustitelnost);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            chustitelnost += 0.1f;
            Voice.self.slider(chustitelnost);
            //  Debug.Log(chustitelnost);
        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            ValListButtion();
        }




    }
}
