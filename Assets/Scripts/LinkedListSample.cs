using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedListSample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LinkedList<string> list = new LinkedList<string>();

        list.AddFirst("È«±æµ¿");
        list.AddLast("±è±æµ¿");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
