using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Stats")]
public class EnemyStats : ScriptableObject
{
    public float MaxHP;
    public float Damage;
    public float RangeRes;
    public float MeleeRes;
    public float AttackInterval;
    public PhysicMaterial PhysicMaterial;
    public Material VisualMatarial;
    public Mesh mesh;
}