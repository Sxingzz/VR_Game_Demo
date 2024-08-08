using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Haptic
{
    [Range(0f, 1f)] // tạo slider trong inspector
    public float intensity;

    public float duration;

    // BaseInteractionEventArgs chứa thông tin sự kiện
    public void TriggerHaptic(BaseInteractionEventArgs eventArgs)
    {
        if (eventArgs.interactorObject is XRBaseControllerInteractor controllerInteractor)
        {
            TriggerHaptic(controllerInteractor.xrController);
        }
    }

    public void TriggerHaptic(XRBaseController controller)
    {
        if (intensity > 0f)
        {
            // SendHapticImpulse dùng để kích hoạt phản hồi haptic trên bộ điều khiển VR
            controller.SendHapticImpulse(intensity, duration);
        }
    }
}
public class HapticInteractable : MonoBehaviour
{
    public Haptic hapticOnActivated;
    public Haptic hapticHoverEntered;
    public Haptic hapticHoverExited;
    public Haptic hapticSelectEntered;
    public Haptic hapticSelectExited;

    private void Start()
    {
        // XRBaseInteractable là một lớp cơ sở cho các đối tượng có thể tương tác
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        interactable.activated.AddListener(hapticOnActivated.TriggerHaptic);

        interactable.hoverEntered.AddListener(hapticHoverEntered.TriggerHaptic);
        interactable.hoverExited.AddListener(hapticHoverExited.TriggerHaptic);
        interactable.selectEntered.AddListener(hapticSelectEntered.TriggerHaptic);
        interactable.selectExited.AddListener(hapticSelectExited.TriggerHaptic);
    }

    

}
 