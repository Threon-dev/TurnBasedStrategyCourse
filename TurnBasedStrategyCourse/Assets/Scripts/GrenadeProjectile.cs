using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    public static event Action OnAnyGrenadeExploded;

    [SerializeField] private Transform grenadeExplodeVfx;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private AnimationCurve arcYAnimationCurve;
    
    
    private Vector3 targetPosition;
    private Action OnGrenadeBehaviourComplete;
    private float totalDistance;
    private Vector3 positionXZ;
    private void Update()
    {
        Vector3 moveDir = (targetPosition - positionXZ).normalized;
        
        float moveSpeed = 15f;
        positionXZ += moveDir * moveSpeed * Time.deltaTime;

        float distance = Vector3.Distance(positionXZ, targetPosition);
        float distanceNormalized = 1 - distance / totalDistance;

        float maxHeight = totalDistance / 4f;
        float positionY = arcYAnimationCurve.Evaluate(distanceNormalized) * maxHeight;
        transform.position = new Vector3(positionXZ.x, positionY, positionXZ.z);
        float reachedTargetDistance = .2f;
        if (Vector3.Distance(positionXZ, targetPosition) < reachedTargetDistance)
        {
            float damageRadius = 4f;
            Collider[] colliderArray = Physics.OverlapSphere(targetPosition, damageRadius);
            
            foreach (var VARIABLE in colliderArray)
            {
                if (VARIABLE.TryGetComponent<Unit>(out Unit unit))
                {
                    unit.Damage(30);
                }
                if (VARIABLE.TryGetComponent<DestructibleCrate>(out DestructibleCrate crate))
                {
                    crate.Damage();
                }
            }
            
            OnAnyGrenadeExploded?.Invoke();
            trailRenderer.transform.parent = null;
            Instantiate(grenadeExplodeVfx, targetPosition + Vector3.up * 1f, Quaternion.identity);
            Destroy(gameObject);

            OnGrenadeBehaviourComplete();
        }
    }

    public void Setup(GridPosition targetGridPosition,Action OnGrenadeBehaviourComplete)
    {
        targetPosition = LevelGrid.Instance.GetWorldPosition(targetGridPosition);
        this.OnGrenadeBehaviourComplete = OnGrenadeBehaviourComplete;

        positionXZ = transform.position;
        positionXZ.y = 0;
        totalDistance = Vector3.Distance(positionXZ, targetPosition);
    }
}
