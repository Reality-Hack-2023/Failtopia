using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using Photon.Pun;

namespace Foundry
{
    public class WeaponController : MonoBehaviourPunCallbacks
    {
        public Weapon weapon;
        public Transform barrelPosition;

        private bool shooting;

        private float timeSinceLastFire;

        private void Start()
        {
            timeSinceLastFire = weapon.rateOfFire;
        }

        public void ShootWeapon()
        {
            if (weapon.WeaponType == WeaponType.semiAuto)
            {
                GameObject bullet = PhotonNetwork.Instantiate(weapon.bullet.name, barrelPosition.position,
                    barrelPosition.rotation);
                Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();

                bulletRB.AddForce(transform.forward * 2500F);

                Destroy(bullet, 10F);
            }
            else
            {
                shooting = true;
            }
        }

        public void StopShooting()
        {
            shooting = false;
        }

        private void Update()
        {
            timeSinceLastFire += Time.deltaTime;
            
            if (shooting && timeSinceLastFire >= weapon.rateOfFire)
            {
                timeSinceLastFire = 0;
                
                GameObject bullet = PhotonNetwork.Instantiate(weapon.bullet.name, barrelPosition.position,
                    barrelPosition.rotation);
                Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();

                bulletRB.AddForce(transform.forward * 2500F);

                Destroy(bullet, 10F);
            }
        }
    }
}
