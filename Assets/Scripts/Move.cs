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

    private Door currentDoor;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
    }

    void Update() {
        UpdateInputTimes();
        UpdateMoveDirection();

        UpdateInteraction();
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

        if(Input.GetKey(KeyCode.LeftShift)){
            moveSpeed = 10f;
        }
        else{
            moveSpeed = 5f;
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

    // 상호작용
    void UpdateInteraction() {
        if (currentDoor != null && Input.GetKeyDown(KeyCode.F)) {
            Interact(currentDoor.doorID);
        }
    }

    private void Interact(int id) 
    {
        if (id == 1) {
            transform.position = new Vector3(0f, 10.5f, transform.position.z);
            if (rb != null) {
                rb.linearVelocity = Vector2.zero; 
            }
        }
        else if (id == 2) {
            transform.position = new Vector3(0f, 3f, transform.position.z);
            if (rb != null) {
                rb.linearVelocity = Vector2.zero; 
            }
        }
        else if (id == 3) {
            transform.position = new Vector3(-24f, 10.5f, transform.position.z);
            if (rb != null) {
                rb.linearVelocity = Vector2.zero; 
            }
        }
        else if (id == 4) {
            transform.position = new Vector3(-24f, 3f, transform.position.z);
            if (rb != null) {
                rb.linearVelocity = Vector2.zero; 
            }
        }
        else if (id == 5) {
            transform.position = new Vector3(-24f, 16f, transform.position.z);
            if (rb != null) {
                rb.linearVelocity = Vector2.zero; 
            }
        }
        else if (id == 6) {
            transform.position = new Vector3(-24f, 23.5f, transform.position.z);
            if (rb != null) {
                rb.linearVelocity = Vector2.zero; 
            }
        }
        else if (id == 7) {
            transform.position = new Vector3(-12f, 16f, transform.position.z);
            if (rb != null) {
                rb.linearVelocity = Vector2.zero; 
            }
        }
        else if (id == 8) {
            transform.position = new Vector3(-12f, 36.5f, transform.position.z);
            if (rb != null) {
                rb.linearVelocity = Vector2.zero; 
            }
        }
        else if (id == 9) {
            transform.position = new Vector3(24f, 10.5f, transform.position.z);
            if (rb != null) {
                rb.linearVelocity = Vector2.zero; 
            }
        }
        else if (id == 10) {
            transform.position = new Vector3(24f, -10f, transform.position.z);
            if (rb != null) {
                rb.linearVelocity = Vector2.zero; 
            }
        }
        else if (id == 11) {
            transform.position = new Vector3(29.5f, 13f, transform.position.z);
            if (rb != null) {
                rb.linearVelocity = Vector2.zero; 
            }
        }
        else if (id == 12) {
            transform.position = new Vector3(34.5f, 13f, transform.position.z);
            if (rb != null) {
                rb.linearVelocity = Vector2.zero; 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Door door = other.GetComponent<Door>();
        
        if (door != null) 
        {
            currentDoor = door;
            Debug.Log(currentDoor.doorID + "번 문 앞에 도착");
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        Door door = other.GetComponent<Door>();
        
        if (door != null && door == currentDoor) 
        {
            Debug.Log(currentDoor.doorID + "번 문에서 멀어짐");
            currentDoor = null; 
        }
    }
}
