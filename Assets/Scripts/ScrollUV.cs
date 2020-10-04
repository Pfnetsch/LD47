using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUV : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Material mat = GetComponent<MeshRenderer>().material;
        
        mat.mainTextureOffset += new Vector2(Time.deltaTime / 100F, Time.deltaTime / 100F);
    }
}
