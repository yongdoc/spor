using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour
{
    [SerializeField] private CanvasGroup UnitInfoPanel;
    [SerializeField] private CanvasGroup UnitCommnandPanel;
    private GameObject CameraController;
    private GameObject Pointer;
    private GameObject PointerMove;
    private GameObject PlayerUnit;

    private CursorMovement cursorMovement;
    private CameraMovement cameraMovement;
    private SelectPlayer selectPlayer;
    private PlayerController playerControl;

    private bool isSelected, isHover, isMoving;
    [SerializeField] private string objectName;
    Text nameDisplay;
    


    // Start is called before the first frame update
    void Start()
    {
        Pointer = GameObject.Find("Pointer2");
        PointerMove = GameObject.Find("Pointer");
        CameraController = GameObject.Find("CameraController");
        UnitInfoPanel.alpha = 0;
        UnitInfoPanel.blocksRaycasts = false;
        UnitInfoPanel.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        // nameDisplay.text = objectName
        cameraMovement = CameraController.GetComponent<CameraMovement>();
        cursorMovement = PointerMove.GetComponent<CursorMovement>();
        isHover = cursorMovement.getIsOverPlayer();

        selectPlayer = Pointer.GetComponent<SelectPlayer>();
        isSelected = selectPlayer.getIsSelected();

        if (isSelected)
        {
            PlayerUnit = selectPlayer.GetSelectedObject();
            playerControl = PlayerUnit.GetComponent<PlayerController>();
            isMoving = playerControl.getIsMoving();
        }

        showUnitInfo(isSelected, isHover);
        showUnitCommand(isSelected, isMoving);
        if(isSelected && !isMoving)
        {
            unitCommand();
        }

    }

    void showUnitInfo(bool isSelected, bool isHover)
    {
        if (isSelected || isHover)
        {
            UnitInfoPanel.alpha = 1;
            UnitInfoPanel.blocksRaycasts = true;
            UnitInfoPanel.interactable = true;
        }
        else
        {
            UnitInfoPanel.alpha = 0;
            UnitInfoPanel.blocksRaycasts = false;
            UnitInfoPanel.interactable = false;
        }
    }

    void showUnitCommand(bool isSelected, bool isMoving)
    {
        if (isSelected && !isMoving)
        {
            UnitCommnandPanel.alpha = 1;
            UnitCommnandPanel.blocksRaycasts = true;
            UnitCommnandPanel.interactable = true;

            cursorMovement.setPauseCursor(true);
            cameraMovement.setMovable(false);
        }
        else
        {
            UnitCommnandPanel.alpha = 0;
            UnitCommnandPanel.blocksRaycasts = false;
            UnitCommnandPanel.interactable = false;

            cursorMovement.setPauseCursor(false);
            cameraMovement.setMovable(true);
        }

    }

    void unitCommand()
    {
        playerControl.setIsGuarding(false);
        playerControl.setIsMoving(false);
        playerControl.setIsAttacking(false);

        if(Input.GetKeyDown(KeyCode.J)) // Move
        {
            cursorMovement.setPauseCursor(false);
            playerControl.setIsMoving(true);

            // Choose destination -> moving -> set false
        }
        else if(Input.GetKeyDown(KeyCode.O)) // Attacc
        {
            playerControl.setIsAttacking(true);
            selectPlayer.setIsSelected(false);

            Animator animator = PlayerUnit.GetComponent<Animator>();
            animator.Play("attack");
            // Choose enemy -> do attack animation -> set false

        }
        else if (Input.GetKeyDown(KeyCode.L)) // Open Skill Window
        {
            // Same as attack
        }
        else if (Input.GetKeyDown(KeyCode.I)) // Guard Mode
        {
            playerControl.setIsGuarding(true);
            selectPlayer.setIsSelected(false);

            // guard for one turn -> next turn set false
        }
        else if (Input.GetKeyDown(KeyCode.Space)) // Cancel
        {
            selectPlayer.setIsSelected(false);
        }
    }


}
