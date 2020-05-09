using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    Vector3 up = Vector3.zero,
        right = new Vector3(0, 90f, 0),
        back = new Vector3(0, 180f, 0),
        left = new Vector3(0, 270f, 0),
        currentDirection = Vector3.zero;

    Vector3 nextPos, destination, direction;

    GameObject Pointer;

    float speed = 15f;
    float rayLength = 1f;

    bool canMove = false;
    bool overPlayer = false;
    bool pauseCursor = false;

    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        currentDirection = up;
        nextPos = Vector3.forward;
        destination = transform.position;
        Pointer = GameObject.Find("Pointer2");
        IsOverPlayer();
    }

    private void Update()
    {
        if(!pauseCursor)
        {
            Move();
        }

    }

    private void FixedUpdate()
    {
        IsOverPlayer();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (transform.position.z < 4.5)
            {
                nextPos = Vector3.forward;
                currentDirection = up;
                canMove = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if(transform.position.x < 4.5)
            {
                nextPos = Vector3.right;
                currentDirection = right;
                canMove = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if(transform.position.z > -4.5)
            {
                nextPos = Vector3.back;
                currentDirection = back;
                canMove = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(transform.position.x > -4.5)
            {
                nextPos = Vector3.left;
                currentDirection = left;
                canMove = true;
            }
        }
        if(Vector3.Distance(destination,transform.position) <= 0.00001f)
        {
            transform.localEulerAngles = currentDirection;
            if (canMove)
            {
                if(Valid())
                {
                    destination = transform.position + nextPos;
                    canMove = false;
                }
            }
        }
    }

    bool Valid()
    {
        Ray myRay = new Ray(transform.position + new Vector3(0, 0.25f, 0), transform.forward);
        RaycastHit hit;

        Debug.DrawRay(myRay.origin, myRay.direction, Color.red);

        if(Physics.Raycast(myRay, out hit, rayLength))
        {
            if(hit.collider.tag == "Wall")
            {
                return false;
            }
        }
        return true;
    }

    void IsOverPlayer()
    {

        Vector3 bawah = Pointer.transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(Pointer.transform.position, bawah, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Player")
            {
                overPlayer = true;
                //print("Yes");
                
            }
            else
            {
                overPlayer = false;
                //print("No");
            }
        }
        else
        {
            overPlayer = false;
            //print("Nothing Bellow");
        }
        // Debug.DrawRay(Pointer.transform.position, bawah * hit.distance, Color.red);
    }

    public bool getIsOverPlayer()
    {
        return overPlayer;
    }

    public void setPauseCursor(bool state)
    {
        pauseCursor = state;
    }
}
