using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using _levelProperties;
using _itemProperties;

[CustomEditor(typeof(levelManager))]
public class LevelManagerEditor : Editor
{
    private Vector2 scrollPos;

    public override void OnInspectorGUI()
    {
        levelManager manager = (levelManager)target;

        if (GUILayout.Button("Add New Level"))
        {
            manager.levelItemsList.Add(new levelProperties());
        }

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        for (int l = 0; l < manager.levelItemsList.Count; l++)
        {
            levelProperties level = manager.levelItemsList[l];
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("Level " + l);
            level.levelIndex = EditorGUILayout.IntField("Level Index", level.levelIndex);
            level.numberOfMovesLimit = EditorGUILayout.IntField("Number of Moves Limit", level.numberOfMovesLimit);
            level.isUnlocked = EditorGUILayout.Toggle("Is Unlocked", level.isUnlocked);
            level.levelBGColor = EditorGUILayout.ColorField("Background Color", level.levelBGColor);

            if (GUILayout.Button("Remove Level"))
            {
                manager.levelItemsList.RemoveAt(l);
                break;
            }

            if (GUILayout.Button("Initialize Level Array"))
            {
                level.InitializeArray();
            }

            if (level.levelArray != null)
            {
                for (int i = 0; i < level.levelArray.Length; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    for (int j = 0; j < level.levelArray[i].Length; j++)
                    {
                        List<_levelItem> list = level.levelArray[i][j];
                        EditorGUILayout.BeginVertical(GUI.skin.box);
                        EditorGUILayout.LabelField($"[{i},{j}]", GUILayout.Width(50));
                        if (list != null)
                        {
                            for (int k = 0; k < list.Count; k++)
                            {
                                _levelItem item = list[k];
                                item.cellObj.cellColor = (_color)EditorGUILayout.EnumPopup("Cell Color", item.cellObj.cellColor);
                                item.itemObj.itemColor = (_color)EditorGUILayout.EnumPopup("Item Color", item.itemObj.itemColor);
                                item.itemObj.itemType = (_type)EditorGUILayout.EnumPopup("Item Type", item.itemObj.itemType);
                                item.itemObj.itemRotation = (_rotation)EditorGUILayout.EnumPopup("Item Rotation", item.itemObj.itemRotation);

                                if (GUILayout.Button("Remove Item"))
                                {
                                    list.RemoveAt(k);
                                    break;
                                }
                            }

                            if (GUILayout.Button("Add Item"))
                            {
                                list.Add(new _levelItem());
                            }
                        }
                        EditorGUILayout.EndVertical();
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndScrollView();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
