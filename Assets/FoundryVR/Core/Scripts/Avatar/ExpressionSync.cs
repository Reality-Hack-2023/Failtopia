using System;
using UnityEngine;
using Photon.Pun;

public class ExpressionSync : MonoBehaviourPunCallbacks, IPunObservable
{
    private SkinnedMeshRenderer face;

    private float mouthOpenAmount;
    
    private void Start()
    {
        transform.root.GetComponent<PhotonView>().ObservedComponents.Add(this);
    }

    private void Update()
    {
        if(face == null && GetComponent<SkinnedMeshRenderer>() != null)
            face = GetComponent<SkinnedMeshRenderer>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(face.GetBlendShapeWeight(51));
        }
        else
        {
            mouthOpenAmount = (float)stream.ReceiveNext();
        }
    }

    private void FixedUpdate()
    {
        if (face != null)
        {
            float smoothMouth = Mathf.Lerp(face.GetBlendShapeWeight(51), mouthOpenAmount, 6 * Time.deltaTime);
            face.SetBlendShapeWeight(51, Mathf.Clamp(smoothMouth, 0, 100));
        }
    }
}
