using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.Animations;
 
class PrettifyAnimatorControllerWindow : EditorWindow
{
    [MenuItem("Tools/PrettifyAnimator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PrettifyAnimatorControllerWindow));
    }
 
    Object myAsset;
    float xPos;
    float yFactor;
 
    void OnGUI()
    {
        GUILayout.Label("PrettifyAnimatorController", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        myAsset = EditorGUILayout.ObjectField(myAsset, typeof(AnimatorController), false);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("xPosition");
        xPos = EditorGUILayout.FloatField(xPos);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("RowSize");
        yFactor = EditorGUILayout.FloatField(yFactor);
        EditorGUILayout.EndHorizontal();
 
 
        if (GUILayout.Button("Prettify States"))
        {
            if (myAsset != null && myAsset is AnimatorController)
            {
                AnimatorController controller = (AnimatorController)myAsset;
                Debug.Log("Prettifying:" + controller);
                for (int i = 0; i < controller.layers.Length; ++i)
                {
                    Debug.Log("Going through layers" + controller.layers[i]);
                    if (controller.layers[i] != null && controller.layers[i].stateMachine != null)
                    {
                        LoadStateMachine(controller.layers[i].stateMachine);
                    }
                }
 
                EditorUtility.SetDirty(controller);
                AssetDatabase.Refresh();
                AssetDatabase.SaveAssets();
            }
 
        }
    }
 
    public void LoadStateMachine(AnimatorStateMachine stateMachine)
    {
        ChildAnimatorState[] childStates = new ChildAnimatorState[stateMachine.states.Length];
 
        for(int i = 0; i < stateMachine.states.Length; ++i)
        {
 
            childStates[i] = stateMachine.states[i];
            Vector2 pos = new Vector2(xPos, i * yFactor);
            childStates[i].position = pos;
            Debug.Log("Child:" + childStates[i].state.name + " " + childStates[i].position);
        }
        stateMachine.states = childStates;
    }
}
