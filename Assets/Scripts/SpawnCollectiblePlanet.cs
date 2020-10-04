using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class SpawnCollectiblePlanet : MonoBehaviour, ISpawnCollectibles
{
    public GameObject Planet;
    public GameObject CollectibleContainer;

    public GameObject Collectible;

    public int MAX_COUNT = 1;

    private int count = 0;
    private Vector3 outside;
    private Vector3 collectibleSize;


    // Start is called before the first frame update
    void Start()
    {
        outside = Planet.transform.Find("UranusSprite").GetComponent<SpriteRenderer>().bounds.size;
        collectibleSize = Collectible.transform.GetComponent<Renderer>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        while (count < MAX_COUNT)
        {
            // random position
            Vector3 newPos = new Vector3(0, Random.Range(collectibleSize.x/2, outside.y/2 - collectibleSize.x/2), 0);
            // spawn new collectible
            GameObject newInstance = Instantiate(Collectible, newPos, Quaternion.identity, CollectibleContainer.transform);
            count++;
            
            newInstance.transform.RotateAround(Planet.transform.position, Vector3.forward, Random.Range(0, 360));
        }
    }

    public void decreaseCount()
    {
        if (count > 0)
        {
            count--;
        }
    }
}
