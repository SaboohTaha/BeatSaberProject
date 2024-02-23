using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DestroyBox : MonoBehaviour
{
    [SerializeField] bool isLeftWeapon = true;

    [Range(0, 1)]
    public float intensity;
    public float duration;
    ActionBasedController controller;
    private void OnTriggerEnter(Collider other)
    {
        if (isLeftWeapon && other.gameObject.tag == "LeftWeaponBox")
        {
            controller.SendHapticImpulse(intensity, duration);
            Destroy(other.gameObject);
        }
        else if (!isLeftWeapon && other.gameObject.tag == "RightWeaponBox")
        {
            controller.SendHapticImpulse(intensity, duration);
            Destroy(other.gameObject);
        }
    }
    void Start()
    {
        controller = GetComponentInParent<ActionBasedController>();
    }
}
