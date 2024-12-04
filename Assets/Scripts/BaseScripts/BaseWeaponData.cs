using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BaseWeaponData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[CreateAssetMenu(fileName = "WeaponStat", menuName = "ScriptableObjects/WeaponStat", order = 1)]
public class WeaponStat : ScriptableObject, IWeapon
{
    public string weaponName;
    public float attackSpeed;
    public int attackDamage;

    public int initialDamage;

    public void Attack()
    {

    }
}

public interface IWeapon
{
    void Attack();
}