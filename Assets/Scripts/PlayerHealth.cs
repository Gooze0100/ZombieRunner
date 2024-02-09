using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    DeathHandler deathHandler;
    FirstPersonController firstPersonController;

    void Start()
    {
        deathHandler = GetComponent<DeathHandler>();
        firstPersonController = GetComponent<FirstPersonController>();
        firstPersonController.enabled = true;
    }

    public void TakeDamage(float decreaseHitPoints)
    {
        hitPoints -= decreaseHitPoints;

        if (hitPoints <= 0)
        {
            deathHandler.HandleDeath();
            firstPersonController.enabled = false;
        }
    }
}
