using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectibles : MonoBehaviour
{
    public GameObject Planet;
    public GameObject Ring;

    public GameObject Collectible;

    public int MAX_COUNT = 10;

    private int count = 0;
    private Vector3 inside;
    private Vector3 outside;
    private Vector3 collectibleSize;


    // Start is called before the first frame update
    void Start()
    {
        inside = Planet.transform.GetComponent<Renderer>().bounds.size;
        outside = Ring.transform.GetComponent<Renderer>().bounds.size;
        collectibleSize = Collectible.transform.GetComponent<Renderer>().bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        while (count < MAX_COUNT)
        {
            // random position
            Vector3 newPos = new Vector3(0, Random.Range(inside.y/2 + collectibleSize.x/2, outside.y/2 - collectibleSize.x/2), 0);
            // spawn new collectible
            GameObject newInstance = Instantiate(Collectible, newPos, Quaternion.identity, Ring.transform);
            count++;
            
            newInstance.transform.RotateAround(Planet.transform.position, Vector3.forward, Random.Range(0, 360));
            
            // if other objects are close to the chosen position => remove new object and retry
            foreach (Transform child in Ring.transform)
            {
                if (Vector3.Distance(newInstance.transform.position, child.transform.position) != 0 &&
                    Vector3.Distance(newInstance.transform.position, child.transform.position) < 10F)
                {
                    Destroy(newInstance);
                    break;
                }
            }
        }
    }
}
