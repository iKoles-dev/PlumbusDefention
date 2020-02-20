using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Additional;
using UnityEngine;

namespace Assets.Scripts
{
    class PathCreator : MonoBehaviour
    {
        [HideInInspector] public List<Vector3> PathPoints = new List<Vector3>();
        [HideInInspector] public bool IsManualEdit = false;
        [HideInInspector] public List<GameObject> PointObjects = new List<GameObject>();
        private void OnDrawGizmos()
        {
            if (PointObjects.Count < 2)
            {
                return;
            }
            for (int i = 0; i < PointObjects.Count-1; i++)
            {
                Gizmos.DrawLine(PointObjects[i].transform.position, PointObjects[i+1].transform.position);
            }
        }
    }
}
