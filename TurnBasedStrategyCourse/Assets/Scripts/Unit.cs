using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 _targetPosition;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _targetPosition = transform.position;
    }

    private void Update()
    {
        float stoppingDistance = .1f;
        if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
        {
            Vector3 moveDir = (_targetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += moveDir * moveSpeed * Time.deltaTime;

            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward,moveDir,Time.deltaTime * rotateSpeed);
            _animator.SetBool("IsWalking",true);
        }
        else
        {
            _animator.SetBool("IsWalking",false);
        }
    }

    public void Move(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}
