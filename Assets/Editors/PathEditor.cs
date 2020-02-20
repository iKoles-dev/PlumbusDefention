using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathCreator))]
public class PathEditor : Editor
{
    private PathCreator _pathCreator;

    private void OnEnable()
    {
        _pathCreator = (PathCreator) target;
    }
    public override void OnInspectorGUI()
    {
        VisualEdit();
        PointSync();
        EditorGUILayout.LabelField("PathCreator Editor");
        for (int i = 0; i < _pathCreator.PathPoints.Count; i++)
        {
            GUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"Point #{i+1}");
            _pathCreator.PathPoints[i] = EditorGUILayout.Vector3Field("Position", _pathCreator.PathPoints[i]);
            if (GUILayout.Button("Delete Point", GUILayout.Width(300)))
            {
                _pathCreator.PathPoints.RemoveAt(i);
                if (_pathCreator.IsManualEdit)
                {
                    DestroyImmediate(_pathCreator.PointObjects[i]);
                    _pathCreator.PointObjects.RemoveAt(i);
                    RenameObjectPoints();
                }
                break;
            }
            GUILayout.EndVertical();
        }
        if (GUILayout.Button("Add New Point", GUILayout.Width(300)))
        {
            _pathCreator.PathPoints.Add(Vector3.zero);
            if (_pathCreator.IsManualEdit)
            {
                GameObject newObjPoint = new GameObject();
                newObjPoint.transform.parent = _pathCreator.transform;
                newObjPoint.transform.name = $"Point #{_pathCreator.PathPoints.Count}";
                DrawIcon(newObjPoint, 0);
                _pathCreator.PointObjects.Add(newObjPoint);
            }
        }
        ObjectSync();
        if (UnityEngine.GUI.changed)
        {
            SetObjectDirty(_pathCreator.gameObject);
        }
    }
    private void VisualEdit()
    {
        if (!_pathCreator.IsManualEdit)
        {
            if (GUILayout.Button("Enable Visual Edit", GUILayout.Width(300)))
            {
                _pathCreator.IsManualEdit = true;
                CreatePathPoints();
            }
        }
        else
        {
            if (GUILayout.Button("Disable Visual Edit", GUILayout.Width(300)))
            {
                _pathCreator.IsManualEdit = false;
                DeletePathPoints();
            }
        }
    }
    private void RenameObjectPoints()
    {
        for (int i = 0; i < _pathCreator.PointObjects.Count; i++)
        {
            _pathCreator.PointObjects[i].transform.name = $"Point #{i + 1}";
        }
    }
    private void PointSync()
    {
        if (!_pathCreator.IsManualEdit)
        {
            return;
        }
        for (int i = 0; i < _pathCreator.PointObjects.Count; i++)
        {
            _pathCreator.PathPoints[i] = _pathCreator.PointObjects[i].transform.position;
        }
    }
    private void ObjectSync()
    {
        if (!_pathCreator.IsManualEdit)
        {
            return;
        }
        for (int i = 0; i < _pathCreator.PathPoints.Count; i++)
        {
            _pathCreator.PointObjects[i].transform.position = _pathCreator.PathPoints[i];
        }
    }
    private void CreatePathPoints()
    {
        int number = 1;
        _pathCreator.PathPoints.ForEach(point =>
        {
            GameObject pathPoint = new GameObject();
            pathPoint.transform.position = point;
            pathPoint.transform.parent = _pathCreator.transform;
            pathPoint.transform.name = $"Point #{number++}";
            _pathCreator.PointObjects.Add(pathPoint);
            DrawIcon(pathPoint,0);
        });
    }
    private void DeletePathPoints()
    {
        _pathCreator.PointObjects.ForEach(point =>
        {
            DestroyImmediate(point);
        });
        _pathCreator.PointObjects.Clear();
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
