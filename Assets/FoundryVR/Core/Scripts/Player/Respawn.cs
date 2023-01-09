using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foundry
{
    public class Respawn : MonoBehaviour
    {
        public float floorPlaneHeight;
        public float maxDepth;

        private void Update()
        {
            float depth = floorPlaneHeight - transform.position.y;

            if (depth > maxDepth)
            {
                transform.position = Vector3.zero;
                depth = floorPlaneHeight;
            }
        }
    }
}
