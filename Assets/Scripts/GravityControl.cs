using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    public Transform Planet;
    public Transform Player;

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
        Vector3 playerPos = Player.position;

        Vector3 direction = (planetPos - playerPos).normalized;
        Vector2 newGravityDirection = new Vector2(direction.x, direction.y);

        var charRotation = Quaternion.LookRotation(direction, Vector3.forward);
        charRotation.x = 0f;
        charRotation.y = 0f;
        Player.rotation = charRotation;

        Physics2D.gravity = newGravityDirection * 9.8F;
    }
}
