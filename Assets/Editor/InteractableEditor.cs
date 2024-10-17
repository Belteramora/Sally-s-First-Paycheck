using UnityEditor;

[CustomEditor(typeof(Interactable), editorForChildClasses: true) ]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = target as Interactable;

        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            interactable.promptMessage = EditorGUILayout.TextField("Prompt Message", interactable.promptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteractable can ONLY use UnityEvents", MessageType.Info);

            if (interactable.GetComponent<InteractionEvent>() == null)
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();

            }

            return;
        }

        base.OnInspectorGUI();


        if (interactable.useEvents)
        {
            if(interactable.GetComponent<InteractionEvent>() == null)
                interactable.gameObject.AddComponent<InteractionEvent>();
        }
        else
        {
            if (interactable.GetComponent<InteractionEvent>() != null)
                DestroyImmediate(interactable.gameObject.GetComponent<InteractionEvent>());
        }
    }


}
