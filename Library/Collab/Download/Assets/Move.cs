using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject Pointer;
    public Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        Pointer.transform.Rotate(new Vector3(0f, 3f, 0f), Space.World);
        if (Input.GetKeyDown(KeyCode.W))
        {
            if( Pointer.transform.position.z < 4.5f ) rigidBody.MovePosition(Pointer.transform.position + new Vector3(0f, 0f, 1f));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Pointer.transform.position.x > -4.5f) rigidBody.MovePosition(Pointer.transform.position + new Vector3(-1f, 0f, 0f));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (Pointer.transform.position.z > -4.5f) rigidBody.MovePosition(Pointer.transform.position + new Vector3(0f, 0f, -1f));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Pointer.transform.position.x < 4.5f) rigidBody.MovePosition(Pointer.transform.position + new Vector3(1f, 0f, 0f));
        }
        
    }

}
