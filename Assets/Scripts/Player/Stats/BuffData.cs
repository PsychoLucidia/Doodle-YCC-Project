using UnityEngine;

[CreateAssetMenu(fileName = "BuffData", menuName = "ScriptableObjects/BuffData")]
public class BuffData : ScriptableObject
{
    public BuffType buffType;
    public int health;
    public int attack;
    public int restoreHealthAmount;
    public float speed;
    public Color32 buffColor;

    [Header("Description")]
    public string buffName;
    public string description;
}