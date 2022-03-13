using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;

    public event Action OnStop;
    public bool IsMoving => _isMoving;

    private Vector3 _groundNormal;
    private float _groundCheckDistance = 0.1f;
    private float _turnAmount;
    private float _forwardAmount;
    private bool _isMoving;
    private float _stoppingTime = 0.25f;

    private void Update()
    {
        if (_isMoving == false)
            return;

        Move(_agent.desiredVelocity);
    }


    public void StartMoving(Transform target)
    {
        _agent.SetDestination(target.position);
        _isMoving = true;
        _stoppingTime = 0.5f;
    }
    private void Move(Vector3 move)
    {
        move.Normalize();
        move = transform.InverseTransformDirection(move);

        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, _groundNormal);

        _turnAmount = Mathf.Atan2(move.x, move.z);
        _forwardAmount = move.z;

        UpdateAnimator(move);
        if (move == Vector3.zero)
            Stopping();


    }
    private void Stopping()
    {
        _stoppingTime -= Time.deltaTime;
        if (_stoppingTime < 0)
        {
            _isMoving = false;
            OnStop?.Invoke();
        }

    }

    private void CheckGroundStatus()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, _groundCheckDistance))
        {
            _groundNormal = hitInfo.normal;
            _animator.applyRootMotion = true;
        }
        else
        {
            _groundNormal = Vector3.up;
            _animator.applyRootMotion = false;
        }
    }

    private void UpdateAnimator(Vector3 move)
    {
        _animator.SetFloat("Forward", _forwardAmount, 0.1f, Time.deltaTime);
        _animator.SetFloat("Turn", _turnAmount, 0.1f, Time.deltaTime);
    }
}
