using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform bulletProjectilePrefab;
    [SerializeField] private Transform shootPointTransform;


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
}
