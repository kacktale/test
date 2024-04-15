using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 6f;
    public float maxJumpTime = 0.4f;
    private float jumpTimeCounter;
    private bool isJumping;
    private Rigidbody2D rigid;
    public float speed;

    public bool landing;

    public GameObject restartButton;

    private void OnDestroy()
    {
        if (restartButton != null)
        {
            restartButton.SetActive(true);
        }
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        HandleJump();
    }

    void HandleJump()
    {
        if ((Input.GetButtonDown("Jump") && landing) || (Input.GetKeyDown(KeyCode.W) && landing) || (Input.GetKeyDown(KeyCode.UpArrow) && landing) || (Input.GetMouseButton(0) && landing))
        {
            isJumping = true;
            jumpTimeCounter = 0;
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        }

        if ((Input.GetButton("Jump") && isJumping) || (Input.GetKey(KeyCode.W) && isJumping) || (Input.GetKey(KeyCode.UpArrow) && isJumping || (Input.GetMouseButton(0) && isJumping)))
        {
            if (jumpTimeCounter < maxJumpTime)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                jumpTimeCounter += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");

        if ((Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)) ||
            (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
        else
        {
            rigid.velocity = new Vector2(h * speed, rigid.velocity.y);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            landing = true;
        }
        if (collision.gameObject.tag == "Spike")
        {
            if (restartButton != null)
            {
                restartButton.SetActive(true);
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            landing = false;
        }
    }
}
