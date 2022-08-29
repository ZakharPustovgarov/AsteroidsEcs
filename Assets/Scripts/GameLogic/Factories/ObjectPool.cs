using UnityEngine;
using System.Collections.Generic;

public class ObjectPool<T> where T : MonoBehaviour
{ 
    private List<T> _objects = new List<T>();

    private Transform _parent;
    private string _parentName;

    public ObjectPool(string parentName)//Bullet type, int initialSize)
    {
        _parentName = parentName;
        CreateParent(_parentName);
    }

    private void CreateParent(string parentName)
    {
        CheckForNulls();
        _parent = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        _parent.position = new Vector3(-200, -200, -200);
        _parent.name = parentName;
    }

    private void CheckForNulls()
    {
        for(int i = 0; i < _objects.Count;)
        {
            if (_objects[i] == null) _objects.RemoveAt(i);
            else i++;
        }
    }

    public T Spawn()
    {
        if (_parent == null) CreateParent(_parentName);

        foreach (var obj in _objects)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        return null;
    }

    public void Despawn(T obj)
    {
        if (_parent == null) CreateParent(_parentName);

        if (!_objects.Contains(obj))
        {
            _objects.Add(obj);
            obj.transform.parent = _parent;
        }

        obj.gameObject.SetActive(false);
    }

    public void AddObject(T obj)
    {
        if (_parent == null) CreateParent(_parentName);

        _objects.Add(obj);
        obj.gameObject.SetActive(false);
        obj.transform.parent = _parent;
    }
}

