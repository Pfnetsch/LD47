using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    public Transform Planet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 planetPos = Planet.position;
        Vector3 playerPos = gameObject.transform.position;

        Vector3 direction = (planetPos - playerPos).normalized;

        Vector2 newGravityDirection = new Vector2(direction.x, direction.y);

        //gameObject.transform.LookAt(Planet, Quaternion.AngleAxis(-90, Vector3.up) * direction);

        //gameObject.transform.RotateAround(planetPos, Vector3.forward, 0.01f);

        Physics2D.gravity = newGravityDirection * 9.8F;

        //Physics2D.gravity = new Vector2()
        UnityEngine.Debug.Log("New gravity direction: " + newGravityDirection * 9.8F);
    }
}
