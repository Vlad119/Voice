using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyLineManager : MonoBehaviour
{
    public List_songs list_songs;
    public Transform prefab;
    public int numberOfObjects;
    public Vector3 startPosition;
    private Vector3 nextPosition;
    public float recycleOffset;
    private Queue<Transform> objectQueue;
    public Vector3 minSize, maxSize;
    public int count = 1;

    void Start()
    {

        objectQueue = new Queue<Transform>(numberOfObjects);
        for (int i = 0; i < numberOfObjects; i++)
        {
            objectQueue.Enqueue((Transform)Instantiate(prefab));
        }
        nextPosition = startPosition;
        for (int i = 0; i < numberOfObjects; i++)
        {
           
         Down();  Up();
           
        }
    }

    void Update()
    {
        if (objectQueue.Peek().localPosition.x + recycleOffset + 5 < Runner.distanceTraveled)
        {                      
          Down(); Up();           
           
        }
    }




    public void Up()
    {
       Vector3 scale = new Vector3(Random.Range(minSize.x, maxSize.x), Random.Range(minSize.y, maxSize.y),
        Random.Range(minSize.z, maxSize.z));
        Vector3 position = nextPosition;
        if (scale.y < 2.5)
        scale.y = 0;         
        Transform o = objectQueue.Dequeue();
        position.x += scale.x * 0.5f; 
        //o.rotation = new Quaternion(180f, 0f, 0, 0f);
        position.y += scale.y * 0.5f;
        o.localScale = scale;
        o.localPosition = position;
        nextPosition.x += scale.x;
        objectQueue.Enqueue(o);
        count++;
        
    }

    public void Down()
    {
        Vector3 scale = new Vector3(Random.Range(minSize.x, maxSize.x), Random.Range(minSize.y, maxSize.y),
       Random.Range(minSize.z, maxSize.z));
        Vector3 position = nextPosition;
        if (scale.y < 2)
        scale.y = 0;
        if (count % 4 == 0)
            scale.y = 0;
        Transform o = objectQueue.Dequeue();
        position.x += scale.x * 0.5f;
        o.rotation = new Quaternion(180f, 0f, 0, 0f);
        position.y -= scale.y * 0.5f;
        position.y = 3.2f;
        o.localScale = scale;
        o.localPosition = position;
        nextPosition.x += scale.x;
        objectQueue.Enqueue(o);
        count++;
    }

   


}
