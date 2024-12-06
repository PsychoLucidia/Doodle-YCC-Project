using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteParticleHandler : MonoBehaviour
{   
    private Coroutine playCoroutine;
    private Animator animator;
    private 
    
    void PlayAnimation()
    {
        if (playCoroutine == null)
        {
            playCoroutine = StartCoroutine(PlaySequence());
        }
    }

    IEnumerator PlaySequence()
    {
        animator.Play("Poof");
        yield return new WaitForSeconds(animator.GetNextAnimatorStateInfo(0).length);
    }
}
