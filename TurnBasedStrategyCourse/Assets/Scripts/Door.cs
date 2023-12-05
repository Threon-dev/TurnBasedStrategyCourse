using System;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isOpen;
    private GridPosition _gridPosition;
    private Animator _animator;
    private Action onInteractComplete;
    private float timer;
    private bool isActive;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetInteractableAtGridPosition(_gridPosition,this);

        if (isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void Update()
    {
        if (!isActive) return;
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            isActive = false;
            onInteractComplete();
        }
    }

    public void Interact(Action onInteractComplete)
    {
        this.onInteractComplete = onInteractComplete;
        isActive = true;
        timer = .5f;
        
        if (isOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        PathFinding.Instance.SetWalkableGridPosition(_gridPosition,isOpen);
        _animator.SetBool("isOpen",isOpen);
    }

    private void CloseDoor()
    {
        isOpen = false;
        PathFinding.Instance.SetWalkableGridPosition(_gridPosition,isOpen);
        _animator.SetBool("isOpen",isOpen);
    }
}
