using Unity.Netcode;
using UnityEngine;

public class PlayerAnimator : NetworkBehaviour
{
    private Animator _animator;

    [SerializeField] private Player _player;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!IsOwner) return;
        _animator.SetBool(Constants.IS_WALKING, _player.IsWalking());
    }
}