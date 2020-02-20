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
    [CustomEditor(typeof(Enemy))]
    class EnemyEditor : Editor
    {
        private Enemy _enemy;
        private void OnEnable()
        {
            _enemy = (Enemy) target;
        }
        public override void OnInspectorGUI()
        {
            if (_enemy.EnemyImage != null)
            {
                var texture = AssetPreview.GetAssetPreview(_enemy.EnemyImage);
                GUILayout.Label(texture);
            }
            DrawDefaultInspector();
        }
    }
}
