using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTeleportation;
    public GameObject rightTeleportation;

    public InputActionProperty leftActivate; // chuột trái
    public InputActionProperty rightActivate; // chuột trái

    public InputActionProperty leftCancle; // phím G 
    public InputActionProperty rightCancle; // phím G

    private void Update()
    {
        leftTeleportation.SetActive(leftCancle.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1f);
        rightTeleportation.SetActive(rightCancle.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f);
    }
}
