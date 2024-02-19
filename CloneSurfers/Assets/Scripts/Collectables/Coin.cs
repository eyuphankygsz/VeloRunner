using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IInteractable
{
    Animator _anim;
    public void OnAction()
    {
        if (_anim == null)
            _anim = GetComponent<Animator>();
        _anim.StopPlayback();
        GameManager.Instance.AddScore(10);
        GameObject particle = ObjectPool.Instance.GetNewParticle();
        particle.SetActive(false);
        particle.transform.position = transform.position;
        particle.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnReset()
    {
        _anim.StartPlayback();
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
