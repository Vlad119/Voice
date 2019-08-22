using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomQuadScroller : MonoBehaviour
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
