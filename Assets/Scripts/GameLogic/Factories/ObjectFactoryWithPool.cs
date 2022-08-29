using UnityEngine;

public class ObjectFactoryWithPool<T> where T : MonoBehaviour
{
    private ObjectPool<T> _pool;

    private int _initialSize = 6;
    private T _prefab;

    public  ObjectFactoryWithPool(T prefab)
    {
        _prefab = prefab;

        _pool = new ObjectPool<T>(typeof(T).Name);
    }

    public T Create()
    {
        T prefab = null;

        prefab = GameObject.Instantiate<T>(prefab);

        return prefab;
    }

    public T Spawn()
    {
        T obj = null;

        obj = _pool.Spawn();

        if (obj == null)
        {
            obj = Create();
        }

        //Debug.Log("Bullet created in factory: " + bullet.name);
        return obj;
    }

    public void Despawn(T obj)
    {
        _pool.Despawn(obj);
    }
}
