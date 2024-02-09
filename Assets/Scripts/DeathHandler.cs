using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;

    void Start()
    {
        gameOverCanvas.enabled = false;
    }

    public void HandleDeath()
    {
        gameOverCanvas.enabled = true;
        //stop time in game
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        // cursor not locked
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
