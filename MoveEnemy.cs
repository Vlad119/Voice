using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{

    // Use this for initialization
    public static MoveEnemy self;


    
  
    void Start()
    {
      
    }

       // Update is called once per frame
    void Update()
    {
        transform.Translate(5f * Time.deltaTime, 0f, 0f);
    }

    private void Recycle()
    {
        
    }
}
