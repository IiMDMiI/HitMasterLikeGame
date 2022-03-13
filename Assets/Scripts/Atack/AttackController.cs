using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private Transform _bulletCreatePoint;
    [SerializeField] private MovementController _movementController;
    private Camera _camera;
    private void Start()
    {
        InputController.Instance.OnTouch += OnTouchHandler;
        _camera = Camera.main;
    }

    private void OnTouchHandler(Vector2 position)
    {
        if(_movementController.IsMoving == true)
            return;
            
        Ray cameraRay = _camera.ScreenPointToRay(position);
        if (Physics.Raycast(cameraRay, out RaycastHit raycastHit))
        {
            Vector3 point = raycastHit.point;
            Vector3 direction = (point - _bulletCreatePoint.position).normalized;
            _bulletPool.TryCreateBullet(direction, _bulletCreatePoint.position);
        }
    }

}
