using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Pointer;
    public Rigidbody Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.MovePosition(Pointer.transform.position + new Vector3(0f, -2.5f, 0f));
        }
    }
}
