using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveDirection;
    private float lastWTime = -1f;
    private float lastATime = -1f;
    private float lastSTime = -1f;
    private float lastDTime = -1f;

    private Door currentDoor;
    [SerializeField] private GameObject npcDetecter;
    [SerializeField] private GameObject itemDetecter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateInputTimes();
        UpdateMoveDirection();

        UpdateInteraction();

        if (Input.GetKeyDown(KeyCode.F))
        {
            npcDetecter.SetActive(true);
            itemDetecter.SetActive(true);
            Invoke("DetectersSetActiveFalse", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (GM.Instance.bag.gameObject.activeSelf)
            {
                GM.Instance.bag.BagClose();
            }
            else
            {
                GM.Instance.bag.BagOpen();
            }
        }
    }

    public void DetectersSetActiveFalse()
    {
        npcDetecter.SetActive(false);
        itemDetecter.SetActive(false);
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void UpdateInputTimes()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastWTime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            lastATime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            lastSTime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            lastDTime = Time.time;
        }

        if (!Input.GetKey(KeyCode.W))
        {
            lastWTime = -1f;
        }
        if (!Input.GetKey(KeyCode.A))
        {
            lastATime = -1f;
        }
        if (!Input.GetKey(KeyCode.S))
        {
            lastSTime = -1f;
        }
        if (!Input.GetKey(KeyCode.D))
        {
            lastDTime = -1f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 10f;
        }
        else
        {
            moveSpeed = 5f;
        }
    }

    void UpdateMoveDirection()
    {
        moveDirection = Vector2.zero;
        float newestTime = -1f;

        if (Input.GetKey(KeyCode.W) && lastWTime > newestTime)
        {
            newestTime = lastWTime;
            moveDirection = Vector2.up;
            if (anim.GetInteger("dir") != 1)
            {
                anim.SetInteger("dir", 1);
                anim.SetTrigger("isChange");
            }
        }
        else if (Input.GetKey(KeyCode.S) && lastSTime > newestTime)
        {
            newestTime = lastSTime;
            moveDirection = Vector2.down;
            if (anim.GetInteger("dir") != 3)
            {
                anim.SetInteger("dir", 3);
                anim.SetTrigger("isChange");
            }
        }
        else if (Input.GetKey(KeyCode.A) && lastATime > newestTime)
        {
            newestTime = lastATime;
            moveDirection = Vector2.left;
            if (anim.GetInteger("dir") != 2)
            {
                anim.SetInteger("dir", 2);
                anim.SetTrigger("isChange");
            }
        }
        else if (Input.GetKey(KeyCode.D) && lastDTime > newestTime)
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

    // 상호작용
    void UpdateInteraction()
    {
        if (currentDoor != null && Input.GetKeyDown(KeyCode.F))
        {
            if (!GM.Instance.doorLock)
            {
                Interact(currentDoor.doorID);
            }
        }
    }

    private void Interact(int id)
    {
        if (id == 1)//본인방 나갈때
        {
            //밤인 경우 열쇠 있을 때만 혹은 낮의 경우에 나갈 수 있다.
            if ((GM.Instance.clock.GetDayOrNigjtStatus() == 1
            && GM.Instance.bag.items[5].GetComponent<Items>().GetIsItemAcquired())
            || GM.Instance.clock.GetDayOrNigjtStatus() == 0)
            {
                GM.Instance.SetMoveMonsterStartDefault();
                transform.position = new Vector3(0f, 10.5f, transform.position.z);
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                }
            }
        }
        else if (id == 2)
        {
            transform.position = new Vector3(0f, 3f, transform.position.z);
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
        else if (id == 3)
        {
            GM.Instance.SetMoveMonsterStartCenter();
            transform.position = new Vector3(-24f, 10.5f, transform.position.z);
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
        else if (id == 4)//특수방
        {
            //밤에만 열쇠 있을 때 들어갈 수 있다.
            if (GM.Instance.clock.GetDayOrNigjtStatus() == 1
            && GM.Instance.bag.items[5].GetComponent<Items>().GetIsItemAcquired())
            {
                transform.position = new Vector3(-24f, 3f, transform.position.z);
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                }
            }
        }
        else if (id == 5)
        {
            GM.Instance.SetMoveMonsterStartCenter();
            transform.position = new Vector3(-24f, 16f, transform.position.z);
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
        else if (id == 6)//특수방
        {
            //밤에만 열쇠 있을 때 들어갈 수 있다.
            if (GM.Instance.clock.GetDayOrNigjtStatus() == 1
            && GM.Instance.bag.items[5].GetComponent<Items>().GetIsItemAcquired())
            {
                transform.position = new Vector3(-24f, 23.5f, transform.position.z);
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                }
            }
        }
        else if (id == 7)
        {
            GM.Instance.SetMoveMonsterStartCenter();
            transform.position = new Vector3(-12f, 16f, transform.position.z);
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
        else if (id == 8)//특수방
        {
            if (GM.Instance.clock.GetDayOrNigjtStatus() == 1
            && GM.Instance.bag.items[5].GetComponent<Items>().GetIsItemAcquired())
            {
                transform.position = new Vector3(-12f, 36.5f, transform.position.z);
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                }
            }
        }
        else if (id == 9)
        {
            GM.Instance.SetMoveMonsterStartCenter();
            transform.position = new Vector3(24f, 10.5f, transform.position.z);
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
        else if (id == 10)
        {
            transform.position = new Vector3(24f, -10f, transform.position.z);
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
        else if (id == 11)
        {
            GM.Instance.SetMoveMonsterStartCenter();
            transform.position = new Vector3(29.5f, 13f, transform.position.z);
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
        else if (id == 12)
        {
            transform.position = new Vector3(34.5f, 13f, transform.position.z);
            if (rb != null)
            {
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
