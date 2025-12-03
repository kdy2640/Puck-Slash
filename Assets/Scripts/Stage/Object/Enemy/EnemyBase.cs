using System;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("---- Enemy Stats ----")]
    public float MaxHP = 10f;
    public float CurrentHP = 10f;

    public float AttackDamage = 1f;

    [Tooltip("원거리 데미지 비례 감소 (예: 0.2면 20% 감소)")]
    public float RangedResistance = 0f;
    public float MeleeResistance = 0f;

    [Header("---- Behaviour ----")]
    public float BehaviorInterval = 1.0f; // 1초마다 행동
    private float behaviorTimer = 0f;

    // 죽을 때 호출되는 콜백
    public Action<EnemyBase> OnDeath;

    protected virtual void Start()
    {
        CurrentHP = MaxHP;
        BehaviorInterval += UnityEngine.Random.Range(-0.2f, 0.2f);
        BehaviorInterval = Mathf.Max(0.1f, BehaviorInterval);
    }

    protected virtual void Update()
    {
        behaviorTimer += Time.deltaTime;

        if (behaviorTimer >= BehaviorInterval)
        {
            behaviorTimer = 0f;
            OnTick(); // 행동 루프 호출
        }
    }

    /// <summary>
    /// 일정 주기로 실행되는 적 행동 (하위 클래스에서 구현)
    /// </summary>
    protected abstract void OnTick();

    /// <summary>
    /// 데미지를 받음
    /// </summary>
    public virtual void TakeDamage(float amount, bool isMelee)
    {
        float resist = isMelee ? MeleeResistance : RangedResistance;
        float finalDmg = amount * (1f - resist);
        CurrentHP -= finalDmg;

        if (CurrentHP <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 체력 0일 때 처리
    /// </summary>
    protected virtual void Die()
    {
        OnDeath?.Invoke(this);
        gameObject.SetActive(false);
    }
}
