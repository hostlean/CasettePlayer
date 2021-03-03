using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speedMult = 200;
    [SerializeField] private float _jumpMult = 200;
    [SerializeField] private float _dashMult = 200;
    [SerializeField] private float _dashDistance = 10f;
    [SerializeField] private bool _isJumping = false;
    [SerializeField] private bool _isRunning = false;
    [SerializeField] private bool _isDashing = false;
    [SerializeField] private bool _canDash = true;
    [SerializeField] private bool _canFloat = false;
    [SerializeField] private float _horizontalValue;

    private int _seconds = 0;

    private BoxCollider2D boxCollider2D;
    private CapsuleCollider2D capsuleCollider2D;
    private PlayerTimeBody playerTimeBody;
    private bool _isDamaged = false;
    private bool _isClimable = false;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private GameObject dashEffect;

    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        playerTimeBody = GetComponent<PlayerTimeBody>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalValue = Input.GetAxis("Horizontal");
        if(GameManager.Instance.GetSeconds() >= 5 && GameManager.Instance.GetSeconds() < 8)
            rb.velocity = new Vector2(Vector3.right.x * _speedMult * Time.deltaTime, rb.velocity.y);
        if(GameManager.Instance.GetSeconds() >= 8)
        {
            if(!_isDamaged)
            {
                Run();
                Slide();
                Jump();
                Climb();
                Dash();
            }
        }
        HandleAnimation();

    }

    private void HandleAnimation()
    {
        if(IsGrounded())
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }
    }

    private bool IsGrounded()
    {
        float extraHeightTest = 0.1f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(boxCollider2D.bounds.center, 
                               Vector2.down, boxCollider2D.bounds.extents.y + extraHeightTest, _targetLayer);
        Color rayColor;
        if (raycastHit2D.collider != null)
        {
            rayColor = Color.green;
        }
        else rayColor = Color.red;

        Debug.DrawRay(boxCollider2D.bounds.center,
                               Vector2.down * (boxCollider2D.bounds.extents.y + extraHeightTest), rayColor);
        return raycastHit2D.collider != null;
    }

    private void Run()
    {
        if (Input.GetButton("Horizontal"))
            _isRunning = true;
        else _isRunning = false;
        float myHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(myHorizontal * _speedMult * Time.deltaTime, rb.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector2.up * _jumpMult, ForceMode2D.Impulse);
        }
    }

    private void Climb()
    {
        if(_isClimable)
        {
            float myVertical = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, myVertical * _speedMult * Time.deltaTime);
        }
    }

    private void Slide()
    {
        if (Input.GetKey(KeyCode.S) && IsGrounded() && !_isClimable)
            capsuleCollider2D.enabled = false;
        if (Input.GetKeyUp(KeyCode.S))
            capsuleCollider2D.enabled = true;
    }

    private void Dash()
    {
        if(_seconds + 2 <= GameManager.Instance.GetSeconds())
            _canDash = true;

        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            _canFloat = false;
            float myHorizontal = Input.GetAxis("Horizontal");
            float dashDistance = _dashDistance;
            Vector3 moveDir = Vector3.right;
            Vector3 dashEffectRotation = Vector3.zero;
            Vector3 dashEffectPosition = Vector3.zero;
            Vector3 backwardDashEffectRotation = Vector3.zero;
            Vector3 backwardDashEffectPosition = Vector3.zero;
            if (myHorizontal >= 0)
            {
                dashDistance = Mathf.Abs(dashDistance);
                dashEffectPosition = new Vector3(this.transform.position.x -
                                2.83f, this.transform.position.y);
                backwardDashEffectRotation = Vector3.back * 180;
                backwardDashEffectPosition = new Vector3(this.transform.position.x +
                                2.83f, this.transform.position.y);

            }
            else
            {
                dashDistance *= -1;
                dashEffectRotation = Vector3.back * 180;
                dashEffectPosition = new Vector3(this.transform.position.x +
                                2.83f, this.transform.position.y);
                backwardDashEffectPosition = new Vector3(this.transform.position.x -
                                2.83f, this.transform.position.y);
                moveDir *= -1;
            }

            Instantiate(dashEffect, dashEffectPosition, Quaternion.Euler(dashEffectRotation), 
                                this.gameObject.transform);
            playerTimeBody.AddDashEffectList(playerTimeBody.Timer, backwardDashEffectPosition, backwardDashEffectRotation);
            rb.MovePosition(transform.position + moveDir * _dashMult);
            _canDash = false;
            _seconds = GameManager.Instance.GetSeconds();
        }
    }

}
