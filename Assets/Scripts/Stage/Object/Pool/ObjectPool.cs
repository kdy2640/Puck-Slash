using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : Component
{
    [Header("Pooling Settings")]
    public T prefab;
    public int initialCount = 10;
    public bool expandable = true;

    protected Queue<T> pool = new Queue<T>();

    protected virtual void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// 초기 객체 생성
    /// </summary>
    protected virtual void Initialize()
    {
        for (int i = 0; i < initialCount; i++)
        {
            CreateNewObject();
        }
    }

    /// <summary>
    /// 새로운 오브젝트 생성 후 pool에 넣기
    /// </summary>
    protected virtual T CreateNewObject()
    {
        T obj = Instantiate(prefab, transform);
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
        return obj;
    }

    /// <summary>
    /// 오브젝트 하나 가져오기
    /// </summary>
    public virtual T Get()
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else if (expandable)
        {
            return CreateNewObjectWithActivate();
        }

        return null;
    }

    private T CreateNewObjectWithActivate()
    {
        for (int i = 0; i < initialCount / 2; i++)
        {
            CreateNewObject();
        }
        T obj = CreateNewObject();
        obj.gameObject.SetActive(true);
        return obj;
    }

    /// <summary>
    /// 오브젝트 반환 (비활성 + pool에 되돌림)
    /// </summary>
    public virtual void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
