using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;

    private int _comboStep = 0;
    private float _comboTimer = 0f;
    private float _maxComboDelay = 1.0f;

    private Vector3 _moveInput = Vector3.zero;
    private float _moveSpeed = 3.0f;
    private float _jumpForce = 10.0f;
    private bool _isGrounded = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _animator.speed = 0.8f;
    }

    private void Update()
    {
        UpdateAttackCombo();
        ComboTimerUpdate();
        Move();
    }

    private void FixedUpdate()
    {

    }

    private void Move()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W))
            moveZ = 1f;
        if (Input.GetKey(KeyCode.S))
            moveZ = -1f;
        if (Input.GetKey(KeyCode.A))
            moveX = -1f;
        if (Input.GetKey(KeyCode.D))
            moveX = 1f;

        Vector3 moveDir = new Vector3(moveX, 0f, moveZ).normalized;

        if (moveDir != Vector3.zero)
        {
            // 이동 방향으로 회전
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);

            // 이동
            transform.position += moveDir * _moveSpeed * Time.deltaTime;
        }
    }

    private void UpdateAttackCombo()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_comboStep == 0)
            {
                _comboStep = 1;
                _animator.SetBool("Attack1", true);
                _comboTimer = _maxComboDelay;
            }
            else if (_comboStep == 1)
            {
                _comboStep = 2;
                _animator.SetBool("Attack2", true);
                _comboTimer = _maxComboDelay;
            }
            else if (_comboStep == 2)
            {
                _comboStep = 3;
                _animator.SetBool("Attack3", true);
                _comboTimer = _maxComboDelay;
            }
        }
    }

    private void ComboTimerUpdate()
    {
        if (_comboStep != 0)
        {
            _comboTimer -= Time.deltaTime;
            if (_comboTimer <= 0f)
            {
                ResetCombo();
            }
        }
    }

    private void ResetCombo()
    {
        _comboStep = 0;
        _animator.SetBool("Attack1", false);
        _animator.SetBool("Attack2", false);
        _animator.SetBool("Attack3", false);
    }
}

