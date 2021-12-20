using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneSelectWindow : EditorWindow
{
    [MenuItem("Window/Scene Select")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SceneSelectWindow));
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Menu"))
        {
            EditorSceneManager.SaveOpenScenes();
            EditorSceneManager.OpenScene("Assets/Scenes/Main_Menu.unity");
        }

        if (GUILayout.Button("Level"))
        {
            EditorSceneManager.SaveOpenScenes();
            EditorSceneManager.OpenScene("Assets/Scenes/Tile_level.unity");
        }

        if (GUILayout.Button("Select"))
        {
            EditorSceneManager.SaveOpenScenes();
            EditorSceneManager.OpenScene("Assets/Scenes/Select_ship.unity");
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Settings"))
        {
            EditorSceneManager.SaveOpenScenes();
            EditorSceneManager.OpenScene("Assets/Scenes/Settings.unity");
        }

        if (GUILayout.Button("Credits"))
        {
            EditorSceneManager.SaveOpenScenes();
            EditorSceneManager.OpenScene("Assets/Scenes/Credits.unity");
        }

        if (GUILayout.Button("Test"))
        {
            EditorSceneManager.SaveOpenScenes();
            EditorSceneManager.OpenScene("Assets/Scenes/Test_field.unity");
        }

        GUILayout.EndHorizontal();
    }
}
