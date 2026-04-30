using UnityEngine;

public class Move : MonoBehaviour {
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveDirection;
    private float lastWTime = -1f;
    private float lastATime = -1f;
    private float lastSTime = -1f;
    private float lastDTime = -1f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
    }

    void Update() {
        UpdateInputTimes();
        UpdateMoveDirection();
    }

    void FixedUpdate() {
        MovePlayer();
    }

    void UpdateInputTimes() {
        if (Input.GetKeyDown(KeyCode.W)) {
            lastWTime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            lastATime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            lastSTime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            lastDTime = Time.time;
        }

        if (!Input.GetKey(KeyCode.W)) {
            lastWTime = -1f;
        }
        if (!Input.GetKey(KeyCode.A)) {
            lastATime = -1f;
        }
        if (!Input.GetKey(KeyCode.S)) {
            lastSTime = -1f;
        }
        if (!Input.GetKey(KeyCode.D)) {
            lastDTime = -1f;
        }
    }

    void UpdateMoveDirection() {
        moveDirection = Vector2.zero;
        float newestTime = -1f;

        if (Input.GetKey(KeyCode.W) && lastWTime > newestTime) {
            newestTime = lastWTime;
            moveDirection = Vector2.up;
            if(anim.GetInteger("dir") != 1) {
                anim.SetInteger("dir", 1);
                anim.SetTrigger("isChange");
            }
        }
        else if (Input.GetKey(KeyCode.S) && lastSTime > newestTime) {
            newestTime = lastSTime;
            moveDirection = Vector2.down;
            if(anim.GetInteger("dir") != 3) {
                anim.SetInteger("dir", 3);
                anim.SetTrigger("isChange");
            }
        }
        else if (Input.GetKey(KeyCode.A) && lastATime > newestTime) {
            newestTime = lastATime;
            moveDirection = Vector2.left;
            if(anim.GetInteger("dir") != 2) {
                anim.SetInteger("dir", 2);
                anim.SetTrigger("isChange");
            }
        }
        else if (Input.GetKey(KeyCode.D) && lastDTime > newestTime) {
            newestTime = lastDTime;
            moveDirection = Vector2.right;
            if(anim.GetInteger("dir") != 4) {
                anim.SetInteger("dir", 4);
                anim.SetTrigger("isChange");
            }
        }
        else {
            anim.SetInteger("dir", 0);
        }
    }

    void MovePlayer() {
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}
