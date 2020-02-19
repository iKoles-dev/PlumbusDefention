using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Additional;
using UnityEngine;

namespace Assets.Scripts
{
    [ExecuteInEditMode]
    class Path : MonoBehaviour
    {
        public List<Vector3> PathPoints = new List<Vector3>();
        public bool IsManualEdit = false;
        public List<GameObject> PointObjects = new List<GameObject>();
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
