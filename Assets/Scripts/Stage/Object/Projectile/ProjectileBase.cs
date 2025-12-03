using System;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    public Action<Collider> OnHit;   // 충돌 시 외부에 알리는 이벤트

    protected bool isActive = false;

    public virtual void Init()
    {
        isActive = true;
    }

    private void Update()
    {
        if (!isActive) return;

        float dt = Time.deltaTime;
        OnMove(dt);   
    }

    /// <summary>
    /// 하위 클래스에서 이동 로직을 구현
    /// 직선, 포물선, 호밍 등 자유롭게 구현 가능
    /// </summary>
    protected abstract void OnMove(float dt);

    /// <summary>
    /// 충돌 시 이벤트 발생
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        OnHit?.Invoke(other);
        OnProjectileHit(other);
    }

    /// <summary>
    /// 하위에서 필요 시 익스텐션용
    /// (폭발 이펙트, 데미지 처리 등)
    /// </summary>
    protected virtual void OnProjectileHit(Collider hit)
    {
        // override optional
    }
}
