using UnityEngine;
using System.Collections.Generic;
public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet _prefab;
    private ObjectPool<Bullet> _pool;
    private int count = 30;
    private void Start()
    {
        _pool = new ObjectPool<Bullet>(_prefab, count, transform);
    }
    private void Update()
    {
        List<Bullet> bullets = _pool.GetActiveElements();
        if (bullets == null || bullets.Count == 0)
            return;

        foreach (var bullet in bullets)
            bullet.DecreaseLifeTime();
    }

    public void TryCreateBullet(Vector3 forceDirection, Vector3 position)
    {

        Bullet bullet = _pool.GetFreeElement(); 
              
        if (bullet == null)
            return;
        
        
        bullet.Activate(forceDirection, position);
    }



}
