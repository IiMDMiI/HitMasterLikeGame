using System.Collections.Generic;
using UnityEngine;


public class ObjectPool<T> where T : MonoBehaviour
{
    public T Prefab { get; }
    public Transform Container { get; }
    private List<T> _pool;

    public ObjectPool(T prefab, int count, Transform container)
    {
        Prefab = prefab;
        Container = container;
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            var createObject = Object.Instantiate(Prefab, Container);
            createObject.gameObject.SetActive(false);
            _pool.Add(createObject);
        }
    }

    public T GetFreeElement()
    {   
        foreach (var element in _pool)
        {
            if(element.gameObject.activeInHierarchy == false)
                return element;
        }
        
        Debug.Log("All objects in pool're active");
        return null;        
    }

    public List<T> GetActiveElements()
    {           
        List<T> activeElements = new List<T>();
        foreach (var element in _pool)
        {
            if(element.gameObject.activeInHierarchy == true)
               activeElements.Add(element);
        }
        
        if(activeElements.Count > 0)
            return activeElements;

        return null;        
    }


}
