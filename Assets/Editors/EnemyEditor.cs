using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.Editors
{
    [CustomEditor(typeof(EnemyСharacteristics))]
    class EnemyEditor : Editor
    {
        private EnemyСharacteristics _enemyСharacteristics;
        private void OnEnable()
        {
            _enemyСharacteristics = (EnemyСharacteristics) target;
        }
        public override void OnInspectorGUI()
        {
            if (_enemyСharacteristics.EnemyImage != null)
            {
                var texture = AssetPreview.GetAssetPreview(_enemyСharacteristics.EnemyImage);
                GUILayout.Label(texture);
            }
            DrawDefaultInspector();
        }
    }
}
