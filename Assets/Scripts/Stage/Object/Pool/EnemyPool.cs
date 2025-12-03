using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool<EnemyBase>
{
    // Start is called before the first frame update
    protected override EnemyBase CreateNewObject()
    {
        EnemyBase obj = Instantiate(prefab, transform);
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
        return obj;
    }


    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }

}
