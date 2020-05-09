using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    bool isSelected = false;
    bool isMoving = false;
    bool isAttacking = false;
    bool isGuarding = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isGuarding", isGuarding);
    }

    public void setIsSelected(bool select)
    {
        isSelected = select;
    }

    public bool getIsSelected()
    {
        return isSelected;
    }

    public void setIsMoving(bool state)
    {
        isMoving = state;
    }

    public bool getIsMoving()
    {
        return isMoving;
    }

    public void setIsGuarding(bool state)
    {
        isGuarding = state;

    }

    public void setIsAttacking(bool state)
    {
        isAttacking = state;
    }

}
