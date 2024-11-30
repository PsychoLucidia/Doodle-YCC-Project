using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBarLogic : MonoBehaviour
{
    [Header("Image Components (Base)")]
    public Image backBarImage;
    public Image frontBarImage;

    public abstract void UpdateBar();
}
