using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGeneric : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PrintValue(42);
        PrintValue("Hello, Generivs!");
        PrintValue(3.14f);
    }

    // Update is called once per frame
    void PrintValue<T>(T value)
    {
        Debug.Log($"Value : {value}, Type : {typeof(T)}");
    }
}
