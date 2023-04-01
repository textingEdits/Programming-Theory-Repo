using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    SpringJoint sj;
    Rigidbody slingRb;
    public GameObject player { get; private set; }
    bool isPressed;
    public bool isSuccessful;
    [SerializeField] float releaseDelay { get; }
    [SerializeField] float maxDragDistance = 10f;
    [SerializeField] float minVelocity = 5f;
    [SerializeField] int bounceCount = 0;
    [SerializeField] int bounceLimit = 10;
    public Vector3 lastFrameVelocity;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        sj = GetComponent<SpringJoint>();
        slingRb = sj.connectedBody;
        //Lock the ball in place until the player interacts with it
        rb.constraints = RigidbodyConstraints.FreezePosition;
        isSuccessful = false;
    }

    void Update()
    {
        //Continually track the ball's speed
        lastFrameVelocity = rb.velocity;

        if (isPressed)
        {
            DragBall();
        }
    }

    private void DragBall()
    {

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector3.Distance(mousePosition, slingRb.position);
        if (distance > maxDragDistance)
        {
            Vector3 direction = ((Vector3)mousePosition - (Vector3)slingRb.position).normalized;
            rb.position = slingRb.position + direction * maxDragDistance;
        }
        else
        {
            rb.position = new Vector2(mousePosition.x, mousePosition.y);
        }
    }

    private void OnMouseDown()
    {
        GrabBall();
    }

    private void GrabBall()
    {
        //Ball is locked to mouse position but still constrained by the invisible sling.
        isPressed = true;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    private void OnMouseUp()
    {
        ReleaseBall();
    }

    private void ReleaseBall()
    {
        isPressed = false;
        rb.isKinematic = false;
        StartCoroutine("Release");
    }

    private IEnumerator Release()
    {
        //The sling's break force is snapped just after the ball is released, sending it forward on it's trajectory.
        yield return new WaitForSeconds(releaseDelay);
        sj.breakForce = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (bounceCount < bounceLimit && !collision.gameObject.CompareTag("Shot Eater") && !collision.gameObject.CompareTag("Destructible"))
        {
            Bounce(collision.contacts[0].normal);
            ++bounceCount;
        }
        else if (bounceCount < bounceLimit && collision.gameObject.CompareTag("Destructible"))
        {
            Bounce(collision.contacts[0].normal);
            Destroy(collision.gameObject);
            ++bounceCount;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isSuccessful = true;
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

    private void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);
        rb.velocity = direction * Mathf.Max(speed, minVelocity);
    }
}
