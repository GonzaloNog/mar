using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Turret : MonoBehaviour
{
    [Header("Lanza Misiles")]
    [SerializeField] InputAction shot;
    //[SerializeField] Lanzamisiles lanzamisiles;

    [Header("Lanza Llamas")]
    [SerializeField] InputAction shotSecondary;
    [SerializeField] ParticleSystem particleSystem;


    ParticleSystem.EmissionModule emissionModule;

    private void Awake()
    {
        emissionModule = particleSystem.emission;
    }


    private void OnEnable()
    {
        shot.Enable();
        shotSecondary.Enable();
    }

    private void OnDisable()
    {
        shot.Disable();
        shotSecondary.Disable();

    }
    // Update is called once per frame
    void Update()
    {
        if (shot.WasPressedThisFrame())
        {
           // lanzamisiles.PerformShot();
        }

        //if (shotSecondary.IsPressed())
        emissionModule.enabled = shotSecondary.IsPressed();
    }
}