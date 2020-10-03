using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    public GameObject allPlanets;

    public float speedMultiplier;

    private List<Transform> _planetTransforms;

    // Start is called before the first frame update
    void Start()
    {
        _planetTransforms = allPlanets.GetComponentsInChildren<Transform>().ToList();
    }

    private void FixedUpdate()
    {
        foreach (var planet in _planetTransforms)
        {
            float rotationSpeed = 0.0F;

            switch (planet.gameObject.name)
            {
                case "Mercury":
                    rotationSpeed = 1F;
                    break;

                case "Venus":
                    rotationSpeed = 0.39F;
                    break;

                case "Earth":
                    rotationSpeed = 0.24F;
                    break;

                case "Mars":
                    rotationSpeed = 0.13F;
                    break;

                case "Jupiter":
                    rotationSpeed = 0.02F;
                    break;

                case "Saturn":
                    rotationSpeed = 0.009F;
                    break;

                case "Uranus":
                    rotationSpeed = 0.003F;
                    break;

                case "Neptune":
                    rotationSpeed = 0.0015F;
                    break;

                case "Pluto":
                    rotationSpeed = 0.0001F;
                    break;

                default:
                    break;
            }

            planet.RotateAround(Vector3.zero, Vector3.forward, rotationSpeed * speedMultiplier);
        }
    }
}
