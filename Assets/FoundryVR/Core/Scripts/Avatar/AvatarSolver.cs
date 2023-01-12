using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

//THIS SCRIPT IS PRE CONFIGURED IF YOU CHANGE ANYTHING EXPECT THINGS TO CHANGE WITH HOW AVATAR MOVES AND IS SYNCED

public class AvatarSolver : MonoBehaviour
{
    private Transform headTracked, leftHandTracked, rightHandTracked, avatarHead, avatarLeftHand, avatarRightHand;

    public SkinnedMeshRenderer face;

    private AvatarRoot AR;

    private Transform boneRoot;
    public Transform[] bones;

    private void Start()
    {
        this.gameObject.AddComponent<PhotonTransformView>();

        face = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        face.updateWhenOffscreen = true;
    }

    //Run start methods in update to make sure they apply after we spawn the avatar
    private void Update()
    {
        if (AR == null) 
        {
            AR = transform.root.GetComponent<AvatarRoot>();
            AR.gameObject.GetComponent<ExpressionControll>().face = face;

            if (!face.GetComponent<ExpressionSync>())
            {
                face.gameObject.AddComponent<ExpressionSync>();
            }
        }

        if (boneRoot == null) 
        {
            boneRoot = transform.Find("Armature");
            bones = boneRoot.GetComponentsInChildren<Transform>();
        }

        if (headTracked == null || leftHandTracked == null || rightHandTracked == null) 
        {
            headTracked = AR.headTracked;
            leftHandTracked = AR.leftHandTracked;
            rightHandTracked = AR.rightHandTracked;
        }
        
        MapNeck(bones[2], headTracked);
        MapHead(bones[4], headTracked);
        MapLeftHand(bones[23], leftHandTracked);
        MapRightHand(bones[7], rightHandTracked);
        MapRightFingers(new List<Transform> { bones[11], bones[12], bones[13], bones[14], bones[15], bones[16], bones[17], bones[18], bones[19] }, new List<Transform> { bones[8], bones[9], bones[10] }, new List<Transform> { bones[21], bones[22] });
        MapLeftFingers(new List<Transform> { bones[27], bones[28], bones[29], bones[30], bones[31], bones[32], bones[33], bones[34], bones[35] }, new List<Transform> { bones[24], bones[25], bones[26] }, new List<Transform> { bones[37], bones[38] });
    }

    void MapNeck(Transform neck, Transform HMD)
    {
        neck.transform.position = HMD.transform.TransformPoint(AR.neckoffset);
        neck.transform.position = neck.transform.position + new Vector3(0, AR.neckoffset.y, 0);

        neck.transform.forward = Vector3.Lerp(neck.transform.forward, Vector3.ProjectOnPlane(HMD.forward, Vector3.up), 10 * Time.deltaTime);
    }

    void MapHead(Transform head, Transform HMD)
    {
        head.transform.rotation = HMD.rotation *= Quaternion.Euler(AR.headOffsetRotation);
    }

    void MapLeftHand(Transform leftHand, Transform leftController)
    {
        leftHand.transform.parent = leftHandTracked.transform;
        leftHand.localPosition = Vector3.zero;
        leftHand.localRotation = Quaternion.Euler(AR.leftHandOffsetRotation);
    }
    
    void MapRightHand(Transform rightHand, Transform rightController)
    {
        rightHand.transform.parent = rightHandTracked.transform;
        rightHand.localPosition = Vector3.zero;
        rightHand.localRotation = Quaternion.Euler(AR.rightHandOffsetRotation);
    }

    void MapRightFingers(List<Transform> gripFingers, List<Transform> indexFinger, List<Transform> thumb) 
    {
        foreach (Transform finger in gripFingers)
        {
            if (finger.GetComponent<PhotonTransformView>() == null)
            {
                PhotonTransformView view = finger.gameObject.AddComponent<PhotonTransformView>();

                view.m_SynchronizePosition = false;
                view.m_UseLocal = true;

                transform.root.GetComponent<PhotonView>().ObservedComponents.Add(view);
            }

            finger.localRotation = Quaternion.Lerp(finger.localRotation, Quaternion.Euler(new Vector3(AR.totalFingerCurve * AR.gripR.ReadValue<float>() + AR.minFingerCurve, 0, 0)), 10 * Time.deltaTime);
        }

        foreach (Transform finger in indexFinger)
        {
            if (finger.GetComponent<PhotonTransformView>() == null)
            {
                PhotonTransformView view = finger.gameObject.AddComponent<PhotonTransformView>();
                
                view.m_SynchronizePosition = false;
                view.m_UseLocal = true;

                transform.root.GetComponent<PhotonView>().ObservedComponents.Add(view);
            }

            finger.localRotation = Quaternion.Lerp(finger.localRotation, Quaternion.Euler(new Vector3(AR.totalFingerCurve * AR.triggerR.ReadValue<float>()  + AR.minFingerCurve, 0, 0)), 10 * Time.deltaTime);
        }

        foreach (Transform finger in thumb)
        {
            if (finger.GetComponent<PhotonTransformView>() == null)
            {
                PhotonTransformView view = finger.gameObject.AddComponent<PhotonTransformView>();

                view.m_SynchronizePosition = false;
                view.m_UseLocal = true;

                transform.root.GetComponent<PhotonView>().ObservedComponents.Add(view);
            }

            finger.localRotation = Quaternion.Lerp(finger.localRotation, Quaternion.Euler(new Vector3(AR.totalThumbCurve * AR.thumbR.ReadValue<float>()  + AR.minThumbCurve, 0, 0)), 10 * Time.deltaTime);
        }
    }

    void MapLeftFingers(List<Transform> gripFingers, List<Transform> indexFinger, List<Transform> thumb)
    {
        foreach (Transform finger in gripFingers)
        {
            if (finger.GetComponent<PhotonTransformView>() == null)
            {
                PhotonTransformView view = finger.gameObject.AddComponent<PhotonTransformView>();

                view.m_SynchronizePosition = false;
                view.m_UseLocal = true;

                transform.root.GetComponent<PhotonView>().ObservedComponents.Add(view);
            }

            finger.localRotation = Quaternion.Lerp(finger.localRotation, Quaternion.Euler(new Vector3(AR.totalFingerCurve * AR.gripL.ReadValue<float>()  + AR.minFingerCurve, 0, 0)), 10 * Time.deltaTime);
        }

        foreach (Transform finger in indexFinger)
        {
            if (finger.GetComponent<PhotonTransformView>() == null)
            {
                PhotonTransformView view = finger.gameObject.AddComponent<PhotonTransformView>();

                view.m_SynchronizePosition = false; 
                view.m_UseLocal = true;

                transform.root.GetComponent<PhotonView>().ObservedComponents.Add(view);
            }

            finger.localRotation = Quaternion.Lerp(finger.localRotation, Quaternion.Euler(new Vector3(AR.totalFingerCurve * AR.triggerL.ReadValue<float>()  + AR.minFingerCurve, 0, 0)), 10 * Time.deltaTime);
        }

        foreach (Transform finger in thumb)
        {
            if (finger.GetComponent<PhotonTransformView>() == null)
            {
                PhotonTransformView view = finger.gameObject.AddComponent<PhotonTransformView>();

                view.m_SynchronizePosition = false;
                view.m_UseLocal = true;

                transform.root.GetComponent<PhotonView>().ObservedComponents.Add(view);
            }

            finger.localRotation = Quaternion.Lerp(finger.localRotation, Quaternion.Euler(new Vector3(AR.totalThumbCurve * AR.thumbL.ReadValue<float>()  + AR.minThumbCurve, 0, 0)), 10 * Time.deltaTime);
        }
    }
}
