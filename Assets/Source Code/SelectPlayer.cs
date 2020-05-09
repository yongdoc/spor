using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayer : MonoBehaviour
{
    GameObject selectedObject;
    bool isSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSelected)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Select();
            }
        }
    }

    void Select()
    {
        RaycastHit hit;
        transform.rotation = Quaternion.identity;
        Vector3 bawah = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, bawah, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, bawah * hit.distance, Color.red);
            // print("Found Object: " + hit.collider + hit.transform.position);
            if (hit.collider.tag == "Player")
            {
                //if (!isSelected)
                //{
                GameObject temporaryObject = hit.collider.gameObject;
                //print("Selected Object : " + selectedObject);
                PlayerMovement pm = temporaryObject.GetComponent<PlayerMovement>();
                PlayerController playerState = temporaryObject.GetComponent<PlayerController>();
                pm.setObjectSelect(true);
                selectedObject = temporaryObject;
                isSelected = true;
                print("Unit Sekarang " + temporaryObject);
                //} else
                //{
                //    PlayerMovement pm1 = selectedObject.GetComponent<PlayerMovement>();
                //    pm1.setObjectSelect(false);
                //    GameObject temporaryObject = hit.collider.gameObject;
                //    //print("Selected Object : " + selectedObject);
                //    PlayerMovement pm2 = temporaryObject.GetComponent<PlayerMovement>();
                //    pm2.setObjectSelect(true);
                //    selectedObject = temporaryObject;
                //}

            }
            
        }
    }

    public void setIsSelected(bool select)
    {
        isSelected = select;
    }

    public bool getIsSelected()
    {
        return isSelected;
    }

    public GameObject GetSelectedObject()
    {
        return selectedObject;
    }
}
