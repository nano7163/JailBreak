using System;
using UnityEngine;

public class MoveMonster : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveDirection;
    private float lastATime = -1f;
    private float lastDTime = -1f;
    private bool aMode = false;
    private bool dMode = true;
    [SerializeField] GameObject leftLight;
    [SerializeField] GameObject rightLight;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateInputTimes();
        UpdateMoveDirection();
    }
    public void SetLeft()
    {
        aMode = true;
        dMode = false;
    }
    public void SetRight()
    {
        dMode = true;
        aMode = false;
    }
    public void TurnMonster()
    {
        aMode = !aMode;
        dMode = !dMode;
    }
    void FixedUpdate()
    {
        MovePlayer();
        if (aMode)
        {
            leftLight.SetActive(true);
            rightLight.SetActive(false);
        }
        else if (dMode)
        {
            leftLight.SetActive(false);
            rightLight.SetActive(true);
        }
    }

    void UpdateInputTimes()
    {
        if (aMode)
        {
            lastATime = Time.time;
        }
        if (dMode)
        {
            lastDTime = Time.time;
        }
        if (!aMode)
        {
            lastATime = -1f;
        }
        if (!dMode)
        {
            lastDTime = -1f;
        }
    }

    void UpdateMoveDirection()
    {
        moveDirection = Vector2.zero;
        float newestTime = -1f;
        if (aMode && lastATime > newestTime)
        {
            newestTime = lastATime;
            moveDirection = Vector2.left;
            if (anim.GetInteger("dir") != 2)
            {
                anim.SetInteger("dir", 2);
                anim.SetTrigger("isChange");
            }
        }
        else if (dMode && lastDTime > newestTime)
        {
            newestTime = lastDTime;
            moveDirection = Vector2.right;
            if (anim.GetInteger("dir") != 4)
            {
                anim.SetInteger("dir", 4);
                anim.SetTrigger("isChange");
            }
        }
        else
        {
            anim.SetInteger("dir", 0);
        }
    }
    void MovePlayer()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}
