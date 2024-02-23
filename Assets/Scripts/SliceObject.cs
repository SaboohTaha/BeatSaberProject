using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
public class SliceObject : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public Material crossSectionMaterial;
    public LayerMask sliceableLayer;
    public float cutForce = 2000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
        if(hasHit)
        {
            GameObject Target = hit.transform.gameObject;
            Slice(Target);
        }
    }

    public void Slice(GameObject Target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 PlaneNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        PlaneNormal.Normalize();
        SlicedHull hull = Target.Slice(endSlicePoint.position, PlaneNormal);
        if (hull != null)
        {
            GameObject upperhull = hull.CreateUpperHull(Target, crossSectionMaterial);
            SetupSlicedComponent(upperhull);
            GameObject lowerhull = hull.CreateLowerHull(Target, crossSectionMaterial);
            SetupSlicedComponent(lowerhull);
            Destroy(Target);
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}
