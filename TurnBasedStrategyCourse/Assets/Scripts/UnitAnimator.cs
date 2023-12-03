using System;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform bulletProjectilePrefab;
    [SerializeField] private Transform shootPointTransform;

    [SerializeField] private Transform riffleTransform;
    [SerializeField] private Transform swordTransform;
    


    private void Awake()
    {
        if(TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.OnStartMoving += MoveActionOnOnStartMoving;
            moveAction.OnStopMoving += MoveActionOnOnStopMoving;
        }
        
        if(TryGetComponent<ShootAction>(out ShootAction shootAction))
        {
            shootAction.OnShoot += ShootActionOnOnShoot;
        }
        if(TryGetComponent<SwordAction>(out SwordAction swordAction))
        {
            swordAction.OnSwordActionStarted += SwordActionOnOnSwordActionStarted;
            swordAction.OnSwordActionCompleted += SwordActionOnOnSwordActionCompleted;
        }
    }

    private void Start()
    {
        EquipRifle();
    }

    private void SwordActionOnOnSwordActionStarted()
    {
        animator.SetTrigger("SwordSlash");
        EquipSword();
    }
    private void SwordActionOnOnSwordActionCompleted()
    {
        EquipRifle();
    }
    

    private void MoveActionOnOnStartMoving()
    {
        animator.SetBool("IsWalking",true);
    }

    private void MoveActionOnOnStopMoving()
    {
        animator.SetBool("IsWalking",false);
    }

    private void ShootActionOnOnShoot(object sender, ShootAction.OnShootEventArgs e)
    {
        animator.SetTrigger("Shoot");
        
        Transform bulletProjectileTransform = 
            Instantiate(bulletProjectilePrefab,shootPointTransform.position,Quaternion.identity);
        BulletProjectile bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();
        bulletProjectile.Setup(e.targetUnit.GetWorldPosition());
    }

    private void EquipSword()
    {
        riffleTransform.gameObject.SetActive(false);
        swordTransform.gameObject.SetActive(true);
    }

    private void EquipRifle()
    {
        riffleTransform.gameObject.SetActive(true);
        swordTransform.gameObject.SetActive(false);
    }
}
