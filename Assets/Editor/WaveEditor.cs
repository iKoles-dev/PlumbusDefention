#if UNITY_EDITOR
using System.Collections.Generic;
using Assets.Scripts;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Assets.Editors
{
    [CustomEditor(typeof(Wave))]
    public class WaveEditor : Editor
    {
        private Wave _wave;
        private List<Enemy> _allEnemys;
        private string[] _allEnemysNames;
        private GUIStyle _cursive = new GUIStyle();
        private void OnEnable()
        {
            _wave = (Wave) target;
            _cursive.fontStyle = FontStyle.Italic;
            SetAllEnemies();
        }
        private void SetAllEnemies()
        {
            _allEnemys = new List<Enemy>(Resources.LoadAll<Enemy>("Enemies"));
            _allEnemysNames = new string[_allEnemys.Count];
            for (int i = 0; i < _allEnemys.Count; i++)
            {
                _allEnemysNames[i] = _allEnemys[i].Name;
            }
        }
        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("Wave Editor");
            GUILayout.EndHorizontal();
            int minTimeToNextWave = 0;
            _wave.EnemiesInWave.ForEach(enemy =>
                {
                    minTimeToNextWave += (int) (enemy.EnemiesAmount * enemy.TimeBetweenSpawns + enemy.TimeToNextEnemies);
                });
            _wave.TimeToNextWave = EditorGUILayout.FloatField($"Time to next wave (min: {minTimeToNextWave})",_wave.TimeToNextWave);
            for (int i = 0; i < _wave.EnemiesInWave.Count; i++)
            {
                GUILayout.BeginVertical("box");
                ShowParametres(i);
                GUILayout.EndVertical();
            }
            GUILayout.BeginHorizontal("box");
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Add new enemy", GUILayout.Width(300)))
            {
                if (_allEnemys.Count > 0)
                {
                    _wave.EnemiesInWave.Add(new EnemiesProperties(_allEnemys[0]));
                }
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            if (UnityEngine.GUI.changed)
            {
                EditorSceneManager.MarkSceneDirty(Player.Instance.gameObject.scene);
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void ShowParametres(int index)
        {
            _wave.EnemiesInWave[index].PopUpIndex = EditorGUILayout.Popup(_wave.EnemiesInWave[index].PopUpIndex, _allEnemysNames);
            _wave.EnemiesInWave[index].CurrentEnemy = _allEnemys[_wave.EnemiesInWave[index].PopUpIndex];
            GUILayout.BeginHorizontal("box");
            GUILayout.BeginVertical("box");
            _wave.EnemiesInWave[index].EnemiesAmount = EditorGUILayout.IntField("Enemies in wave", _wave.EnemiesInWave[index].EnemiesAmount);
            _wave.EnemiesInWave[index].TimeBetweenSpawns = EditorGUILayout.FloatField("Time between spawns", _wave.EnemiesInWave[index].TimeBetweenSpawns);
            _wave.EnemiesInWave[index].TimeToNextEnemies = EditorGUILayout.FloatField("Time to next enemies", _wave.EnemiesInWave[index].TimeToNextEnemies);
            GUILayout.Label($"Speed: {_wave.EnemiesInWave[index].CurrentEnemy.MoveSpeed}",_cursive);
            GUILayout.Label($"Attack Strenght: {_wave.EnemiesInWave[index].CurrentEnemy.AttackStrength}", _cursive);
            GUILayout.Label($"Attack Speed: {_wave.EnemiesInWave[index].CurrentEnemy.AttackSpeed}", _cursive);
            GUILayout.Label($"Health: {_wave.EnemiesInWave[index].CurrentEnemy.Health}", _cursive);
            GUILayout.EndVertical();
            var texture = AssetPreview.GetAssetPreview(_wave.EnemiesInWave[index].CurrentEnemy.EnemyImage);
            GUILayout.Label(texture);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal("box");
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Delete enemy", GUILayout.Width(300)))
            {
                _wave.EnemiesInWave.RemoveAt(index);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        private void SetObjectDirty(GameObject dirtyObject)
        {
            EditorUtility.SetDirty(dirtyObject);
            EditorSceneManager.MarkSceneDirty(dirtyObject.scene);
        }
    }
}
#endif