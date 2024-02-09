using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("General")]
    [SerializeField] Camera FPCamera;

    [Header("VFX")]
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;

    [Header("Gun settings")]
    [SerializeField] float range = 100f;
    [SerializeField] float damageLevel = 25f;
    [SerializeField] float timeBetweenShots = .2f;

    [Header("Ammo")]
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] TextMeshProUGUI ammoText;
    [Space(5)]
    Animator animationOnChildren;

    bool canShoot = true;

    void OnEnable()
    {
        canShoot = true;
    }

    void Start()
    {
        animationOnChildren = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        DisplayAmmo();
        // if (Input.GetButtonDown("Fire1") && ammoSlot.AmmoAmount > 0 && canShoot == true)
        if (Input.GetMouseButtonDown(0) && ammoSlot.GetCurrentAmmo(ammoType) > 0 && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString("000");
    }

    // TODO for ak47 make burst mode
    IEnumerator Shoot()
    {
        canShoot = false;

        if (animationOnChildren != null)
        {
            animationOnChildren.SetTrigger("Fire");
        }

        PlayMuzzleFlash();
        ProcessRaycast();
        ammoSlot.ReduceCurrentAmmo(ammoType);

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    void ProcessRaycast()
    {
        RaycastHit hitSth;

        // return a bool, we can ask what we hit
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hitSth, range))
        {
            // Debug.Log("hit sth: " + hitSth.transform.name);
            CreateHitImpact(hitSth);
            EnemyHealth target = hitSth.transform.GetComponent<EnemyHealth>();

            if (target == null)
            {
                return;
            }

            target.TakeDamage(damageLevel);
        }
        else
        {
            return;
        }
    }

    void CreateHitImpact(RaycastHit hitSth)
    {
        // Debug.Log("hitting" + hitSth.transform.name);
        // hitSth.normal it is like mirror it mirrors effect from building
        GameObject impact = Instantiate(hitEffect, hitSth.point, Quaternion.LookRotation(hitSth.normal));

        if (impact != null)
        {
            Destroy(impact, 0.5f);
        }
    }
}
