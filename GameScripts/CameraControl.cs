using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [SerializeField]
    private CinemachineVirtualCamera playerCamera;

    [SerializeField]
    private CinemachineVirtualCamera mapCamera;

    private bool isMapViewActive = false;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        mapCamera = GameObject.Find("MapView").GetComponent<CinemachineVirtualCamera>();

        mapCamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleCameraView();
        }
    }

    private void ToggleCameraView()
    {
        if (isMapViewActive)
        {
            playerCamera.gameObject.SetActive(true);
            mapCamera.gameObject.SetActive(false);
        }
        else
        {
            playerCamera.gameObject.SetActive(false);
            mapCamera.gameObject.SetActive(true);
        }

        isMapViewActive = !isMapViewActive;
    }
}
