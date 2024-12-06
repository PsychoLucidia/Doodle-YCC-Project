using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffPanelHandler : MonoBehaviour
{
    public PlayerStat playerStat;
    public BuffData buffData;
    public Image buffImage;
    public TextMeshProUGUI buffName;
    public TextMeshProUGUI buffDescription;
    public Button applyBuffButton;

    public void ApplyBuff()
    {
        switch (buffData.buffType)
        {
            case BuffType.Health: playerStat.maxHealth += buffData.health; break;
            case BuffType.Speed: playerStat.additionalSpeed += buffData.speed; break;
            case BuffType.Attack: playerStat.additionalDamage += buffData.attack; break;
            case BuffType.RestoreHealth: playerStat.health += buffData.restoreHealthAmount; break;
            case BuffType.Combined: CombinedBuff(); break;
            default:
                break;
        }
    }

    void CombinedBuff()
    {
        if (buffData.health > 0) { playerStat.maxHealth += buffData.health; }
        if (buffData.speed > 0) { playerStat.additionalSpeed += buffData.speed; }
        if (buffData.attack > 0) { playerStat.additionalDamage += buffData.attack; }
        if (buffData.restoreHealthAmount > 0) { playerStat.health += buffData.restoreHealthAmount; }
    }

    public void RemoveButtonListener()
    {
        applyBuffButton.onClick.RemoveAllListeners();
    }
}
