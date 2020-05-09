using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Pointer;
    public Rigidbody Player;
    public bool objectSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        Pointer = GameObject.Find("Pointer");
    }

    // Update is called once per frame
    void Update()
    {
        if (objectSelected)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Player.MovePosition(Pointer.transform.position + new Vector3(0f, -2.5f, 0f));
                objectSelected = false;
            }
        }
        
    }
}
