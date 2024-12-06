using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStat", menuName = "ScriptableObjects/WeaponStat", order = 1)]
public class WeaponStat : ScriptableObject
{
    public string weaponName;
    public float attackSpeed;
    public int attackDamage;

    public int initialDamage;

    public void Attack()
    {

    }
}
