using Assets.Scripts.Towers;
using UnityEditor;
using UnityEngine;

namespace Assets.Editors
{
    [CustomEditor(typeof(Tower))]
    public class TowerEditor : Editor
    {
        private Tower _tower;
        private GUIStyle _centeredText;
        private GUIStyle _brightBackground;
        private void OnEnable()
        {
            _tower = (Tower)target;
            _centeredText = new GUIStyle {fontStyle = FontStyle.Bold, alignment = TextAnchor.LowerCenter};
            _brightBackground = new GUIStyle {normal = {background = MakeTex(600, 1, new Color(1, 1, 1, 0.4f))}};
        }

        public override void OnInspectorGUI()
        {
            ShowTowersPreview();
            SetSpritesAndLevels();
            GUILayout.BeginVertical("box");
            _tower.BuyingCost = EditorGUILayout.IntField("Bying Cost", _tower.BuyingCost);
            _tower.ShootAnimation = (Animator)EditorGUILayout.ObjectField("Shoot Animation",_tower.ShootAnimation, typeof(Animator), false);
            _tower.TowerPrefab = (GameObject)EditorGUILayout.ObjectField("Tower Prefab",_tower.TowerPrefab, typeof(GameObject), false);
            GUILayout.EndVertical();
            SetUpgrades();
            serializedObject.ApplyModifiedProperties();
        }

        private void ShowTowersPreview()
        {
            GUILayout.BeginHorizontal("box");

            if (_tower.FirstTowerUpgrade != null)
            {
                GUILayout.BeginVertical();
                var texture = AssetPreview.GetAssetPreview(_tower.FirstTowerUpgrade);
                GUILayout.Label(texture);
                GUILayout.FlexibleSpace();
                GUILayout.Label("Tier #1", _centeredText);
                GUILayout.EndVertical();
            }
            if (_tower.SecondTowerUpgrade != null)
            {
                GUILayout.BeginVertical();
                var texture = AssetPreview.GetAssetPreview(_tower.SecondTowerUpgrade);
                GUILayout.Label(texture);
                GUILayout.FlexibleSpace();
                GUILayout.Label("Tier #2", _centeredText);
                GUILayout.EndVertical();
            }
            if (_tower.ThirdTowerUpgrade != null)
            {
                GUILayout.BeginVertical();
                var texture = AssetPreview.GetAssetPreview(_tower.ThirdTowerUpgrade);
                GUILayout.Label(texture);
                GUILayout.FlexibleSpace();
                GUILayout.Label("Tier #3", _centeredText);
                GUILayout.EndVertical();
            }

            GUILayout.EndHorizontal();
        }

        private void SetSpritesAndLevels()
        {
            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("Tower Tier #1");
            _tower.FirstTowerUpgrade = (Sprite)EditorGUILayout.ObjectField(_tower.FirstTowerUpgrade, typeof(Sprite), false);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("Tower Tier #2");
            _tower.SecondTowerUpgrade = (Sprite)EditorGUILayout.ObjectField(_tower.SecondTowerUpgrade, typeof(Sprite), false);
            GUILayout.FlexibleSpace();
            _tower.LevelToFirstUpgrade = EditorGUILayout.IntField("Level to Upgrade", _tower.LevelToFirstUpgrade);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("Tower Tier #3");
            _tower.ThirdTowerUpgrade = (Sprite)EditorGUILayout.ObjectField(_tower.ThirdTowerUpgrade, typeof(Sprite), false);
            GUILayout.FlexibleSpace();
            _tower.LevelToSecondUpgrade = EditorGUILayout.IntField("Level to Upgrade", _tower.LevelToSecondUpgrade);
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        private void SetUpgrades()
        {
            GUILayout.BeginVertical("box");

            for (int i = 0; i < _tower.Upgrades.Count; i++)
            {
                GUILayout.BeginVertical(_brightBackground);
                GUILayout.Label($"Upgrade Level #{i+1}{(i==0 ? " (Basic)" :"")}",_centeredText);
                _tower.Upgrades[i].Cost = EditorGUILayout.IntField("Cost", _tower.Upgrades[i].Cost);
                _tower.Upgrades[i].SellCost = EditorGUILayout.IntField("Sell Cost", _tower.Upgrades[i].SellCost);
                _tower.Upgrades[i].Range = EditorGUILayout.FloatField("Attack Range", _tower.Upgrades[i].Range);
                _tower.Upgrades[i].ShootInterval = EditorGUILayout.FloatField("Shoot Interval", _tower.Upgrades[i].ShootInterval);
                _tower.Upgrades[i].Damage = EditorGUILayout.FloatField("Damage", _tower.Upgrades[i].Damage);
                _tower.Upgrades[i].DamageRadius = EditorGUILayout.FloatField("Damage Radius", _tower.Upgrades[i].DamageRadius);

                GUILayout.BeginHorizontal(_brightBackground);
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Delete Level", GUILayout.Width(300)))
                {
                    _tower.Upgrades.RemoveAt(i);
                    break;
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
                GUILayout.Space(5);
            }
            GUILayout.BeginHorizontal("box");
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Add Level", GUILayout.Width(300)))
            {
                _tower.Upgrades.Add(new TowerUpgrades());
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];

            for (int i = 0; i < pix.Length; i++)
                pix[i] = col;

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }
    }
}