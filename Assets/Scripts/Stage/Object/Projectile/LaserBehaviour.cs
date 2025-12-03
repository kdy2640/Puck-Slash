using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LaserBehaviour : ProjectileBase
{ 
    public float Power;
    public Vector2 Direciton;
    [SerializeField]
    private float Speed = 3.0f;
    // Update is called once per frame
    GameManager manager;
    ProjectilePool pool;

    private void Start()
    {
        manager = GameManager.Instance;
        pool = manager.Stage.ObjectPooler.GetComponent<ProjectilePool>();
        Init();
    }
    public void SetValue(Vector2 dir, float pow)
    {
        Direciton = dir;
        Power = pow;
    }

    protected override void OnMove(float dt)
    {
        Vector3 movingDirection = new Vector3(Direciton.x, 0, Direciton.y).normalized;

        if (movingDirection != Vector3.zero)
        {
            Quaternion look = Quaternion.LookRotation(movingDirection);

            Quaternion tilt = Quaternion.Euler(-90f, 0f, 0f);

            transform.rotation = look * tilt;
        }

        transform.position += movingDirection * Speed * dt;
    }

    protected override void OnProjectileHit(Collider hit)
    {
        // override optional
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if(other.tag == "Wall")
        {
            pool.ReturnToPool(this);
        }
        else if(other.tag == "Enemy")
        {
            EnemyBase enemy = other.GetComponent<EnemyBase>();
            enemy.TakeDamage(Power, false);
            pool.ReturnToPool(this);
        }
    }
}
