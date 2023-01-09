using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using Photon.Pun;

public class FireworkSpawner : MonoBehaviourPunCallbacks 
{
    public GameObject fireworkPrefab;
    public Transform rocketSpawnPoint;

    public void Spawn() 
    {
        PhotonNetwork.Instantiate(fireworkPrefab.name , rocketSpawnPoint.position, rocketSpawnPoint.rotation);
    }
}
