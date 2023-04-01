using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float speed;
    public float topBound;
    public float lowerBound;
    public float leftBound;
    public float rightBound;
    public bool isMoving;
    public bool vertical;
    public bool horizontal;
    protected Vector3 startingPosition;
    // Start is called before the first frame update
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

    protected void Move()
    {
        //The princible of abstraction has been utilized to obfuscate the inner workings of how platforms are moved in the cardinal directions.
        if (isMoving && vertical && transform.position.y < (startingPosition.y + topBound))
        {
            GoUp();
        }
        if (!isMoving && vertical && transform.position.y > (startingPosition.y + lowerBound))
        {
            GoDown();
        }

        if (isMoving && horizontal && transform.position.x < (startingPosition.x + rightBound))
        {
            GoRight();
        }
        if (!isMoving && horizontal && transform.position.x > (startingPosition.x + leftBound))
        {
            GoLeft();
        }
    }

    private void GoLeft()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        if (transform.position.x < startingPosition.x + leftBound)
        {
            isMoving = true;
        }
    }

    protected virtual void GoRight()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        if (transform.position.x > startingPosition.x + rightBound)
        {
            isMoving = false;
        }
    }

    private void GoDown()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
        if (transform.position.y < startingPosition.y + lowerBound)
        {
            isMoving = true;
        }
    }

    private void GoUp()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
        if (transform.position.y > startingPosition.y + topBound)
        {
            isMoving = false;
        }
    }
}
