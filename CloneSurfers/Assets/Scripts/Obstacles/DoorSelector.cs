using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSelector : MonoBehaviour,IInteractable
{
    [SerializeField] GameObject[] _door;
    Animator[] _anim;
    bool _actionTriggered = false;
    public void OnAction()
    {
        if (_actionTriggered)
            return;
        int rand = Random.Range(0,_anim.Length);
        _anim[rand].SetTrigger("OpenDoor");
        _actionTriggered = true;
    }

    public void OnReset()
    {
        for (int i = 0; i < _anim.Length; i++)
            if (_anim[i].GetCurrentAnimatorStateInfo(0).IsName("OpenedDoor"))
                _anim[i].SetTrigger("CloseDoor");
        _actionTriggered = false;
    }

    void Awake()
    {
        _anim = new Animator[_door.Length];
        for (int i = 0; i < _anim.Length; i++)
        {
            _anim[i] = _door[i].GetComponent<Animator>();
        }
    }
    private void OnDisable()
    {
        OnReset();
    }
}
