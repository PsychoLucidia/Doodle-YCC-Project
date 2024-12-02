using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextHandler : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public Vector3 worldSpawnPos;
    public Transform positionTransform;
    public CanvasGroup canvasGroup;
    public Color32 color;
    public DamageTextPool damageTextPool;

    RectTransform _rectTransform;

    void Awake()
    {
        _rectTransform = this.GetComponent<RectTransform>();
    }

    void Update()
    {
        _rectTransform.position = Camera.main.WorldToScreenPoint(positionTransform.position);
    }

    public void PlayAnimation()
    {
        LeanTween.cancel(this.gameObject);
        transform.position = worldSpawnPos;
        damageText.transform.localPosition = new Vector3(0, 0, 0);
        canvasGroup.alpha = 1;

        LeanTween.moveLocalY(damageText.gameObject, 50, 0.25f).setEaseOutCubic().setOnComplete(() => {
            LeanTween.alphaCanvas(canvasGroup, 0, 0.25f).setEaseInCubic();
            LeanTween.moveLocalY(damageText.gameObject, 0, 0.25f).setEaseInCubic().setOnComplete(() => { DisableThis(); });
        });
    }

    void DisableThis()
    {
        damageTextPool.DeactivateObject(this.gameObject, positionTransform.gameObject);
    }
}
