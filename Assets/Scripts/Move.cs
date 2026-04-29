using UnityEngine;

public class Move : MonoBehaviour {
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float lastWTime = -1f;
    private float lastATime = -1f;
    private float lastSTime = -1f;
    private float lastDTime = -1f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
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
        }
        if (Input.GetKey(KeyCode.S) && lastSTime > newestTime) {
            newestTime = lastSTime;
            moveDirection = Vector2.down;
        }
        if (Input.GetKey(KeyCode.A) && lastATime > newestTime) {
            newestTime = lastATime;
            moveDirection = Vector2.left;
        }
        if (Input.GetKey(KeyCode.D) && lastDTime > newestTime) {
            newestTime = lastDTime;
            moveDirection = Vector2.right;
        }
    }

    void MovePlayer() {
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}
