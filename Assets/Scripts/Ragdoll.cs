using UnityEngine;
public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Enemy _enemy;

    private void Start() => _enemy.OnEnemyDied += enemy => _animator.enabled = false;
}
