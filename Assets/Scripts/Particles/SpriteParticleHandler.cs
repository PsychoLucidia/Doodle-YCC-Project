using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteParticleHandler : MonoBehaviour
{   
    private Coroutine _playCoroutine;
    [SerializeField] private Animator _animator;
    public SpriteParticlePool spriteParticlePool;
    
    public float _time;
    
    public void PlayAnimation()
    {
        _playCoroutine = StartCoroutine(PlaySequence());
    }

    void Update()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
        }
        else
        {
            spriteParticlePool.DeactivateObject(this.gameObject);
        }
    }

    IEnumerator PlaySequence()
    {
        _animator.Play("PoofAnimation");
        
        _time = _animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        _playCoroutine = null;
    }
}
