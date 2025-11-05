using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemiesMovement))]
public class PosSelectorEditor : Editor
{
    private bool selecting = false;

    void OnSceneGUI()
    {
        if (!selecting) return;

        // Detecta clic del ratón
        Event e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 0)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                EnemiesMovement script = (EnemiesMovement)target;
                script.positions.Add(hit.point);
                selecting = false;

                // Marca el objeto como modificado para guardar el cambio
                EditorUtility.SetDirty(script);
                e.Use();
            }
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button(selecting ? "Haz clic en el escenario..." : "Seleccionar posición"))
        {
            selecting = !selecting;
        }
    }
}
