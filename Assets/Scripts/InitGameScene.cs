using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGameScene : MonoBehaviour
{
    public GameObject planet1;
    public GameObject planet2;
    public GameObject planet3;
    public GameObject planet4;
    public GameObject planet5;
    public GameObject planet6;
    public GameObject planet7;
    public GameObject planet8;
    public GameObject planet9;

    public GameObject player;

    private GravityControl _gravityControl;

    // Start is called before the first frame update
    void Start()
    {
        _gravityControl = GetComponent<GravityControl>();

        // Initialize scene
        planet1.SetActive(false);
        planet2.SetActive(false);
        planet3.SetActive(false);
        planet4.SetActive(false);
        planet5.SetActive(false);
        planet6.SetActive(false);
        planet7.SetActive(false);
        planet8.SetActive(false);
        planet9.SetActive(false);

        switch (GlobalInformation.currentScene)
        {
            case 1:
                planet1.SetActive(true);
                initPlayer(planet1);
                break;
            case 2:
                planet2.SetActive(true);
                initPlayer(planet2);
                break;
            case 3:
                planet3.SetActive(true);
                initPlayer(planet3);
                break;
            case 4:
                planet4.SetActive(true);
                initPlayer(planet4);
                break;
            case 5:
                planet5.SetActive(true);
                initPlayer(planet5);
                break;
            case 6:
                planet6.SetActive(true);
                initPlayer(planet6);
                break;
            case 7:
                planet7.SetActive(true);
                initPlayer(planet7);
                break;
            case 8:
                planet8.SetActive(true);
                initPlayer(planet8);
                break;
            case 9:
                planet9.SetActive(true);
                initPlayer(planet9);
                break;
                
        }
    }

    private void initPlayer(GameObject planet)
    {
        _gravityControl.Planet = planet.transform;

        if (planet.transform.Find("Spawn"))
        {
            Transform spawn = planet.transform.Find("Spawn").transform;
            player.transform.position = spawn.position;
        }
        else
        {
            Debug.LogError("No spawnpoint for planet: " + planet.name);
        }
    }
}
