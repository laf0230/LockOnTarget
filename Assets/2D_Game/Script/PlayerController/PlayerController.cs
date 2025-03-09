using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IInterectionable, IDamageable
{
    Vector3 direction;
    public float health;
    public float damage;
    public float speed;
    public float speedComplexity;
    public float jumpPower;
    public float ropeCoolTime;
    [SerializeField] private Transform body;

    private bool isKnockBack = false;
    Rigidbody2D rb;
    public bool isTalking { get; set; } = false;
    public bool isJumping = false;
    private Rope rope;
    private bool isAttackable = false;

    public List<ItemData> inventory = new List<ItemData>();

    public static PlayerController instance {  get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        // hm,,
        rb = GetComponent<Rigidbody2D>();
        transform.LookAt(transform.position, Vector3.up);
        rope = GetComponentInChildren<Rope>();
        rope.SetRopeData(ropeCoolTime);
    }

    private void Update()
    {
        rope.RopeTracking();
        if (isTalking)
            return;

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= speedComplexity;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= speedComplexity;
        }

        if(Input.GetAxis("Jump")> 0 && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpPower);
        }

        // 이동
        if(!isKnockBack)
        {
            Move();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Interection(this);
        }

        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if(Input.GetMouseButtonDown(1))
        {
            rope.WarpToMousePosition(this);
        }

        // 방향 설정
        if (!(body.localScale.x < 0) && rope.GetDirection().x < 0)
            body.localScale = new Vector3(-1, 1, 1);
        else if(!(body.localScale.x > 0) && rope.GetDirection().x > 0)
        {
            body.localScale = new Vector3(1, 1, 1);
        }
    }

    public void SetGravityScale(float gravityScale = 1f)
    {
        rb.gravityScale = gravityScale;
    }

    public void SetRGVelocity(Vector2? velocity = null)
    {
        if(velocity == null)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = velocity.Value;
        }
    }

    private void Move()
    {
        transform.position += new Vector3( Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0);
        if(Input.GetAxis("Horizontal") < 0)
        {
            direction = Vector3.left;
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            direction = Vector3.right;
        }
    }

    #region Interection Function

    public void HighlightInterectionables()
    {
        RaycastHit2D physics2D = Physics2D.CircleCast(transform.position + direction * 1.5f, 0.5f, direction);

        if (physics2D.collider != null && physics2D.collider.TryGetComponent<IInterectionable>(out IInterectionable entity))
        {
            entity.Highlight(true);
        } else
        {
            Debug.Log("Nothing to Interection");
        }
    }

    public void Interection(MonoBehaviour target)
    {
        var interectionArea = GetComponentInChildren<PlayerInterectionArea>();

        if (interectionArea.interectableObject != null)
        {
            interectionArea.interectableObject.Interection(target);
        } 
        else
        {
            Debug.Log("Nothing to Interection");
        }
    }

#endregion

    public void Attack()
    {
        var target = GetComponentInChildren<AttackArea>().damageableObject;
        if(target != null )
            target.Damaged(this, damage);
    }

    public void Damaged(MonoBehaviour attacker, float damageAmount)
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
