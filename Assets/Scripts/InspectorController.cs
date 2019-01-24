using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;



[ExecuteInEditMode]
public class InspectorController : MonoBehaviourEx
{
    public static InspectorController CopiedInspector = null;
    List<Component> Components = new List<Component>();



    void OnValidate()
    {
        Components = GetComponents<Component>().ArrayToList();
    }



    [ContextMenu("Copy Inspector")]
    public void CopyInspector()
    {
        CopiedInspector = this;
    }



    [ContextMenu("Paste Inspector")]
    public void PasteInspector()
    {
        if (CopiedInspector == null)
            return;

        System.Type type;

        foreach (Component component in Components)
        {
            type = component.GetType();

            if ((type != typeof(InspectorController)) && (type != typeof(Transform)) && (type != typeof(RectTransform)))
            {
                DestroyImmediate(component);
            }
        }

        gameObject.name = CopiedInspector.gameObject.name;
        gameObject.tag = CopiedInspector.gameObject.tag;
        gameObject.layer = CopiedInspector.gameObject.layer;

        Components.Clear();
        Component tempComponent;

        foreach (Component component in CopiedInspector.Components)
        {
            type = component.GetType();

            if (type == typeof(InspectorController))
            {
                continue;
            }
            else
            {
                if (type == typeof(RectTransform))
                {
                    tempComponent = GetComponent<RectTransform>();
                    ((RectTransform)tempComponent).CloneOtherRectTransform(((RectTransform)component));
                    continue;
                }
                else if (type == typeof(Transform))
                {
                    tempComponent = transform;
                    ((Transform)tempComponent).CloneOtherTransform(((Transform)component));
                    continue;
                }
                else
                {
                    tempComponent = gameObject.AddComponent(type);
                }

                Components.Add(tempComponent);

                if (component is Behaviour)
                {
                    ((Behaviour)tempComponent).enabled = ((Behaviour)component).enabled;

                    FieldInfo[] fields = type.GetFields();

                    foreach (FieldInfo field in fields)
                    {
                        if (field.IsStatic)
                            continue;

                        field.SetValue(tempComponent, field.GetValue(component));
                    }

                    continue;
                }
                else if (component is Renderer)
                {
                    ((Renderer)tempComponent).enabled = ((Renderer)component).enabled;
                }
                else if (component is Collider)
                {
                    ((Collider)tempComponent).enabled = ((Collider)component).enabled;
                }

                PropertyInfo[] props = type.GetProperties();

                foreach (PropertyInfo prop in props)
                {
                    if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name")
                    {
                        continue;
                    }

                    prop.SetValue(tempComponent, prop.GetValue(component, null), null);
                }
            }
        }
    }
}
