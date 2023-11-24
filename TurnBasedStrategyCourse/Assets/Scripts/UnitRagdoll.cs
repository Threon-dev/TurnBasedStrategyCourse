using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitRagdoll : MonoBehaviour
{
    [SerializeField] private Transform ragdollRootBone;

    public void Setup(Transform original)
    {
        MatchAllChildTransform(original,ragdollRootBone);
        
        ApplyExplosionToRagdoll(ragdollRootBone,300f,transform.position,10f);
    }

    private void MatchAllChildTransform(Transform root, Transform clone)
    {
        foreach (Transform child in root)
        {
            Transform cloneChild = clone.Find(child.name);
            if (cloneChild != null)
            {
                cloneChild.position = child.position;
                cloneChild.rotation = child.rotation;
                MatchAllChildTransform(child,cloneChild);
            }
        }
    }

    private void ApplyExplosionToRagdoll(Transform root,float explosionForce, Vector3 explosionPosition,float explosionRadius)
    {
        foreach (Transform child in root)
        {
            if(child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
            {
                childRigidbody.AddExplosionForce(explosionForce,explosionPosition,explosionRadius);
            }
            
            ApplyExplosionToRagdoll(child,explosionForce,explosionPosition,explosionRadius);
        }
    }
}
