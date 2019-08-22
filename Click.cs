using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Click : MonoBehaviour
{

    // Use this for initialization
    public int index = 0;
    public Text _name;


    public void SetIndex(int xz)
    {
        index = xz;
        Direct.selflink.mem = xz;
        Controller.self.RunTimer ("restart", push);
        //Restart.RestartGame();
    }



    public void push()
    {
        Direct.selflink.Some(index);// передается индекс кнопки по selflink в метод Some
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
