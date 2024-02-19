using UnityEngine;

public class Houses : MonoBehaviour, IInteractable
{
    Animator anim;
    [SerializeField] GameObject _door;
    bool _actionTriggered = false;

    public void OnAction()
    {
        if (_actionTriggered)
            return;
        anim.SetTrigger("OpenDoor");
        _actionTriggered = true;

    }

    public void OnReset()
    {
        _actionTriggered = false;
        anim.SetTrigger("CloseDoor");
    }

    // Start is called before the first frame update
    void Awake()
    {
        anim = _door.GetComponent<Animator>();
        anim.Play("DoorOpen");
    }
    private void OnDisable()
    {
        OnReset();
    }
}
