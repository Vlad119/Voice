using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public Ray ray;
    public RaycastHit hit;
    public Camera cam;
    public GameObject[] Some;

    public void Update()
    {
        if (Some[3].activeSelf)
        {
            foreach (GameObject num in Some)
            {
                Ray ray = cam.ScreenPointToRay(cam.WorldToScreenPoint(num.transform.position));
                if (Physics.Raycast(ray, out hit, 200))
                {
                    //Debug.Log("hit");
                    //Debug.DrawRay(ray.origin, ray.direction);
                    Texture2D TextureMap = (Texture2D)hit.collider.gameObject.GetComponent<Renderer>().material.mainTexture;
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x *= TextureMap.width;
                    pixelUV.y *= TextureMap.height;
                    //if (TextureMap.GetPixel((int)pixelUV.x, (int)pixelUV.y).a != 0)
                   // {
                        if (Some[3].activeSelf)
                        StartCoroutine(Controller.self.Timer("restart"));
                        Some[3].SetActive(false);
                        MelodyPlayer.self.PlaySong();
                  // }
                }
            }
        }
    }
}