using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteParticleHandler : MonoBehaviour
{   
    private Coroutine _playCoroutine;
    private Animator _animator;
    public SpriteParticlePool spriteParticlePool;
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (_playCoroutine == null)
        {
            _playCoroutine = StartCoroutine(PlaySequence());
        }
    }

    IEnumerator PlaySequence()
    {
        _animator.Play("Poof");
        
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        _playCoroutine = null;
        spriteParticlePool.DeactivateObject(this.gameObject);
    }
}
