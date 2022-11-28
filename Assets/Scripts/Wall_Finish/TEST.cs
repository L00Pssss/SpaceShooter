using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    private float x;

    private void Update()
    {
        lerpr();
    }
    private void lerpr()
    {
        x = Mathf.Lerp(12, 20, 0.4f);
        Debug.Log(x);
    }
}
