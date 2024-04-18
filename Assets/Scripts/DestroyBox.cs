using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DestroyBox : MonoBehaviour
{
    [SerializeField] bool isLeftWeapon = true;
    [SerializeField] LevelController levelController;
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
            levelController.IncreaseScore();
        }
        else if (!isLeftWeapon && other.gameObject.tag == "RightWeaponBox")
        {
            controller.SendHapticImpulse(intensity, duration);
            Destroy(other.gameObject);
            levelController.IncreaseScore();
        }
    }
    void Start()
    {
        controller = GetComponentInParent<ActionBasedController>();
    }
}
