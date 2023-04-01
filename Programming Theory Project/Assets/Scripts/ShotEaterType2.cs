using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEaterType2 : PlatformController
{
    // Inheritance and polymorphism is utilized here. Borrowing from the Platform controller to modify the level 2 shot eater with enhanced forward stabbing speed.
    void Start()
    {
        isMoving = true;
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected override void GoRight()
    {
        transform.Translate(Vector3.right * speed * 6 * Time.deltaTime, Space.World);
        if (transform.position.x > startingPosition.x + rightBound)
        {
            isMoving = false;
        }
    }
}
