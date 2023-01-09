using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundry
{
    [System.Serializable]
    public enum WeaponType
    {
        semiAuto,
        automatic
    }

    [CreateAssetMenu(fileName = "Weapon", menuName = "FoundryWeapon")]
    public class Weapon : ScriptableObject
    {
        public WeaponType WeaponType;
        public float rateOfFire;
        public GameObject bullet;
    }
}
