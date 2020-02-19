using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Path))]
public class PathEditor : Editor
{
    private Path _path;

    private void OnEnable()
    {
        _path = (Path) target;
    }

    public override void OnInspectorGUI()
    {
        VisualEdit();
        PointSync();
        EditorGUILayout.LabelField("Path Editor");
        for (int i = 0; i < _path.PathPoints.Count; i++)
        {
            GUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"Point #{i+1}");
            _path.PathPoints[i] = EditorGUILayout.Vector3Field("Position", _path.PathPoints[i]);
            if (GUILayout.Button("Delete Point", GUILayout.Width(300)))
            {
                _path.PathPoints.RemoveAt(i);
                if (_path.IsManualEdit)
                {
                    DestroyImmediate(_path.PointObjects[i]);
                    _path.PointObjects.RemoveAt(i);
                    RenameObjectPoints();
                }
                break;
            }
            GUILayout.EndVertical();
        }
        if (GUILayout.Button("Add New Point", GUILayout.Width(300)))
        {
            _path.PathPoints.Add(Vector3.zero);
            if (_path.IsManualEdit)
            {
                GameObject newObjPoint = new GameObject();
                newObjPoint.transform.parent = _path.transform;
                newObjPoint.transform.name = $"Point #{_path.PathPoints.Count}";
                DrawIcon(newObjPoint, 0);
                _path.PointObjects.Add(newObjPoint);
            }
        }
        ObjectSync();
        if (UnityEngine.GUI.changed)
        {
            SetObjectDirty(_path.gameObject);
        }
    }
    private void VisualEdit()
    {
        if (!_path.IsManualEdit)
        {
            if (GUILayout.Button("Enable Visual Edit", GUILayout.Width(300)))
            {
                _path.IsManualEdit = true;
                CreatePathPoints();
            }
        }
        else
        {
            if (GUILayout.Button("Disable Visual Edit", GUILayout.Width(300)))
            {
                _path.IsManualEdit = false;
                DeletePathPoints();
            }
        }
    }

    private void RenameObjectPoints()
    {
        for (int i = 0; i < _path.PointObjects.Count; i++)
        {
            _path.PointObjects[i].transform.name = $"Point #{i + 1}";
        }
    }
    private void PointSync()
    {
        if (!_path.IsManualEdit)
        {
            return;
        }
        for (int i = 0; i < _path.PointObjects.Count; i++)
        {
            _path.PathPoints[i] = _path.PointObjects[i].transform.position;
        }
    }
    private void ObjectSync()
    {
        if (!_path.IsManualEdit)
        {
            return;
        }
        for (int i = 0; i < _path.PathPoints.Count; i++)
        {
            _path.PointObjects[i].transform.position = _path.PathPoints[i];
        }
    }
    private void CreatePathPoints()
    {
        int number = 1;
        _path.PathPoints.ForEach(point =>
        {
            GameObject pathPoint = new GameObject();
            pathPoint.transform.position = point;
            pathPoint.transform.parent = _path.transform;
            pathPoint.transform.name = $"Point #{number++}";
            _path.PointObjects.Add(pathPoint);
            DrawIcon(pathPoint,0);
        });
    }
    private void DeletePathPoints()
    {
        _path.PointObjects.ForEach(point =>
        {
            DestroyImmediate(point);
        });
        _path.PointObjects.Clear();
    }
    private void DrawIcon(GameObject gameObject, int idx)
    {
        var largeIcons = GetTextures("sv_label_", string.Empty, 0, 8);
        var icon = largeIcons[idx];
        var egu = typeof(EditorGUIUtility);
        var flags = BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
        var args = new object[] { gameObject, icon.image };
        var setIcon = egu.GetMethod("SetIconForObject", flags, null, new Type[] { typeof(UnityEngine.Object), typeof(Texture2D) }, null);
        setIcon.Invoke(null, args);
    }
    private GUIContent[] GetTextures(string baseName, string postFix, int startIndex, int count)
    {
        GUIContent[] array = new GUIContent[count];
        for (int i = 0; i < count; i++)
        {
            array[i] = EditorGUIUtility.IconContent(baseName + (startIndex + i) + postFix);
        }
        return array;
    }
    private void SetObjectDirty(UnityEngine.GameObject dirtyObject)
    {
        UnityEditor.EditorUtility.SetDirty(dirtyObject);
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(dirtyObject.scene);
    }
}
