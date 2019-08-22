using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopQuadScroller1 : MonoBehaviour
{
   

    Material myMaterial;
    Vector2 offSet;

    private void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(5f,0f);
    }

    void FixedUpdate()
    {
       myMaterial.mainTextureOffset += (offSet * Time.fixedDeltaTime)/24;
       
    }

}
