using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;

    // only accessible to Ammo class
    // inside private class public methods can be accessed to Ammo class
    [System.Serializable] // can be seen in inspector
    private class AmmoSlot
    {
        // this is enum so we can select 
        public AmmoType ammoType;
        public int ammoAmount;
    }

    // public int AmmoAmount
    // {
    //     get
    //     {
    //         return ammoAmount;
    //     }
    // }

    public int GetCurrentAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoAmount;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount--;
    }

    public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoAmount)
    {
        GetAmmoSlot(ammoType).ammoAmount += ammoAmount;
    }

    AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot ammoSlot in ammoSlots)
        {
            if (ammoSlot.ammoType == ammoType)
            {
                return ammoSlot;
            }
        }

        return null;
    }


}
