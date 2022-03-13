using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{   
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage = 100;
    private float _remainingTime;
   
    
    public void Activate(Vector3 forceDirection, Vector3 position)
    {           
        _remainingTime = _lifeTime;
        transform.position = position;
        gameObject.SetActive(true);
        _rb.AddForce(forceDirection * _speed, ForceMode.Impulse);
    }
    public void DecreaseLifeTime() 
    {   
        _remainingTime -= Time.deltaTime;
        if(_remainingTime < 0)
            Deactivate();
    }

    private void OnTriggerEnter (Collider other)
    {
        var enemy = other.gameObject.GetComponent<Enemy>();
        if(enemy == null)
            return;
        
        enemy.GetDamage(_damage);
        Deactivate();       
    }

    private void Deactivate() 
    {
        _rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    } 
    
      
    
   
}
