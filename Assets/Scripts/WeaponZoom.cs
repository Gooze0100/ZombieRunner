using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using StarterAssets;

public class WeaponZoom : MonoBehaviour
{
    // [SerializeField] Camera fpsCamera;
    [Header("General")]
    [SerializeField] CinemachineVirtualCamera cameraCinemascope;
    [SerializeField] FirstPersonController firstPersonController;
    [Header("Zoom Settings")]
    [SerializeField] float zoomOut = 44.56f;
    [SerializeField] float zoomIn = 20f;
    [SerializeField] float zoomOutSensitivity = 1f;
    [SerializeField] float zoomInSensitivity = .5f;
    [Tooltip("Zoom out game object(image of reticule)")]
    [SerializeField] GameObject reticleZoomOut;
    [Tooltip("Zoom in game object(image of reticule)")]
    [SerializeField] GameObject reticleZoomIn;
    bool zoomedInToggle = false;

    public bool ZoomedInToggle { get { return zoomedInToggle; } }

    void OnDisable()
    {
        ZoomOut();
    }

    void Update()
    {
        ChangeWeaponZoom();
    }


    void ChangeWeaponZoom()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (zoomedInToggle == false)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }
    }

    void ZoomIn()
    {
        reticleZoomOut.SetActive(false);
        reticleZoomIn.SetActive(true);
        zoomedInToggle = true;
        // fpsCamera.fieldOfView = zoomIn;
        firstPersonController.RotationSpeed = zoomInSensitivity;
        cameraCinemascope.m_Lens.FieldOfView = zoomIn;
    }

    void ZoomOut()
    {
        zoomedInToggle = false;
        // fpsCamera.fieldOfView = zoomOut;
        firstPersonController.RotationSpeed = zoomOutSensitivity;
        cameraCinemascope.m_Lens.FieldOfView = zoomOut;
        reticleZoomOut.SetActive(true);
        reticleZoomIn.SetActive(false);
    }
}
