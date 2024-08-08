using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabHandPose : MonoBehaviour
{
    public HandData rightHandPose;

    private Vector3 startingHandPosition; // vị trí ban đầu bàn tay
    private Vector3 finalHandPosition; // vị trí bàn tay khi đang cầm 
    private Quaternion startingHandRotation; // góc quay ban đầu
    private Quaternion finalHandRotation; // góc quay bàn tay khi đang cầm

    private Quaternion[] startingFingerRotations; // góc quay ban đầu của các ngón tay
    private Quaternion[] finalFingerRotations; // góc quay của các ngón tay khi đang cầm

    private void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(SetupPose);// khi bàn tay cầm
        grabInteractable.selectExited.AddListener(UnSetPose); // khi bàn tay thả

        rightHandPose.gameObject.SetActive(false);
    }

    public void SetupPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = false;

            SetHandDataValues(handData, rightHandPose); // lưu trữ giá trị ban đầu và cuối cùng của bàn tay.
            SetHandData(handData, finalHandPosition, finalHandRotation, finalFingerRotations);// áp dụng nắm giữ cho bàn tay.
        }
    }

    public void UnSetPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = true;

            SetHandData(handData, startingHandPosition, startingHandRotation, startingFingerRotations);// trà bàn tay về tư thế ban đầu
        }
    }

    public void SetHandDataValues(HandData h1, HandData h2) // lưu trữ giá trị ban đầu và cuối cùng của bàn tay.
    {
        startingHandPosition = new Vector3( // chuẩn hóa vị trí bằng cách chia cho local scale để nếu cần scale thì vị trí vẫn k thay đổi.
            h1.root.localPosition.x / h1.root.localScale.x,
            h1.root.localPosition.y / h1.root.localScale.y, 
            h1.root.localPosition.z / h1.root.localScale.z);
        finalHandPosition = new Vector3(
            h2.root.localPosition.x / h2.root.localScale.x,
            h2.root.localPosition.y / h2.root.localScale.y,
            h2.root.localPosition.z / h2.root.localScale.z);

        startingHandRotation = h1.root.localRotation;
        finalHandRotation = h2.root.localRotation;

        startingFingerRotations = new Quaternion[h1.fingerBones.Length];
        finalFingerRotations = new Quaternion[h1.fingerBones.Length];

        for (int i = 0; i < h1.fingerBones.Length; i++)
        {
            startingFingerRotations[i] = h1.fingerBones[i].localRotation;
            finalFingerRotations[i] = h2.fingerBones[i].localRotation;
        }
    }

    public void SetHandData(HandData h, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation)
    {
        h.root.localPosition = newPosition;
        h.root.localRotation = newRotation;

        for (int i = 0; i < newBonesRotation.Length; i++)
        {
            h.fingerBones[i].localRotation = newBonesRotation[i];
        }
    }

}
