using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CollectibleDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        gameObject.transform.parent.gameObject.GetComponent<ISpawnCollectibles>().decreaseCount();
    }
}
