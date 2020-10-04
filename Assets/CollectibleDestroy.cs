using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        gameObject.transform.parent.gameObject.GetComponent<SpawnCollectibles>().decreaseCount();
    }
}
