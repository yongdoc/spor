using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 up = Vector3.zero,
        right = new Vector3(0, 90f, 0),
        back = new Vector3(0, 180f, 0),
        left = new Vector3(0, 270f, 0);
    GameObject Pointer, PointerMove;
    Vector3 destination;
    [SerializeField] private CanvasGroup UnitMovementWarning;
    public List<TileMap.Node> currentPath = null;
    
    //float speed = 5f;

    bool objectSelected = false;

    private PlayerController playerControl;
    //bool canMove = false;
    // Start is called before the first frame update
    void Start()
    {
        Pointer = GameObject.Find("Pointer2");
        PointerMove = GameObject.Find("Pointer");
        playerControl = gameObject.GetComponent<PlayerController>();

        destination = transform.position;

        UnitWarning();
    }

    // Update is called once per frame
    void Update()
    {
        if (objectSelected)
        { 
            if(playerControl.getIsMoving())
            {
                CursorMovement cursorMovement = PointerMove.GetComponent<CursorMovement>();
                if (!cursorMovement.getIsOverPlayer())
                { 
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        destination = Pointer.transform.position - new Vector3(0, 2.95001f, 0);
                        float m = Mathf.Abs(destination.x - transform.position.x), n = Mathf.Abs(destination.z - transform.position.z);
                        switch ((int)(m+n)<=3)
                        {
                            case true:
                                transform.position = destination;
                                objectSelected = false;
                                SelectPlayer sp = Pointer.GetComponent<SelectPlayer>();
                                sp.setIsSelected(false);
                                playerControl.setIsMoving(false);
                                break;
                            case false:
                                showUnitWarning();
                                break;
                        }
                    }
                }
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnitWarning();
        }

    }

    void showUnitWarning()
    {
        UnitMovementWarning.alpha = 1;
        UnitMovementWarning.blocksRaycasts = true;
        UnitMovementWarning.interactable = true;
    }

    void UnitWarning()
    {
        UnitMovementWarning.alpha = 0;
        UnitMovementWarning.blocksRaycasts = false;
        UnitMovementWarning.interactable = false;
    }

    public bool getObjectSelect()
    {
        return objectSelected;
    }

    public void setObjectSelect(bool Selected)
    {
        objectSelected = Selected;
    }
}
