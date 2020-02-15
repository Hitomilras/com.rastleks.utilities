using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AttributesContainer))]
public class AttributesContainerDrawer : PropertyDrawer
{

    ReorderableList list;

    SerializedProperty containerProperty;

    SerializedProperty attributesListProperty;

    SerializedObject target;

    private int selectedElementIndex = -1;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (list == null)
            return EditorGUIUtility.singleLineHeight;

        return list.GetHeight();
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        target = property.serializedObject;

        if (list == null)
        {
            containerProperty = property;
            attributesListProperty = property.FindPropertyRelative("attributes");

            list = new ReorderableList(property.serializedObject, attributesListProperty, false, true, true, true);

            list.onSelectCallback = OnSelect;

            list.drawHeaderCallback = DrawHeader;

            list.drawElementCallback = DrawElementCallback;

            list.elementHeightCallback = ElementHeightCallback;

            list.onAddDropdownCallback = AddDropdown;

            list.onRemoveCallback = Remove;
        }

        list.DoList(position);
    }

    public void DrawHeader(Rect rect)
    {
        EditorGUI.LabelField(rect, containerProperty.displayName);
    }

    private void OnSelect(ReorderableList l)
    {
        selectedElementIndex = l.index;
    }

    private void DrawElementCallback(Rect rect, int index, bool isactive, bool isfocused)
    {
        var element = list.serializedProperty.GetArrayElementAtIndex(index);

        if (index == selectedElementIndex)
        {
            rect.y += 2;

            var childrens = GetVisibleChildren(element);

            var elementFullTypeName = element.managedReferenceFullTypename.Split(' ');

            EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                               new GUIContent(element.FindPropertyRelative("name").stringValue + " (" + elementFullTypeName[elementFullTypeName.Length - 1] + ")"));

            rect.y += EditorGUIUtility.singleLineHeight;

            foreach (var ch in childrens)
            {
                EditorGUI.PropertyField(
                    new Rect(rect.x, rect.y, rect.width,
                    EditorGUIUtility.singleLineHeight),
                    ch, true);

                rect.y += EditorGUIUtility.singleLineHeight + 2;
            }
        }
        else
        {
            rect.y += 2;

            EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                                   new GUIContent(element.FindPropertyRelative("name").stringValue));
        }
    }

    private float ElementHeightCallback(int index)
    {
        float result = 0;

        if (index == selectedElementIndex)
        {
            result += 4;
            result += EditorGUIUtility.singleLineHeight;

            var childrens = GetVisibleChildren(attributesListProperty.GetArrayElementAtIndex(index));

            foreach (var ch in childrens)
                result += EditorGUIUtility.singleLineHeight;
        }
        else
        {
            result += EditorGUIUtility.singleLineHeight;
        }

        return result;
    }

    public IEnumerable<SerializedProperty> GetVisibleChildren(SerializedProperty serializedProperty)
    {
        SerializedProperty currentProperty = serializedProperty.Copy();
        SerializedProperty nextSiblingProperty = serializedProperty.Copy();

        {
            nextSiblingProperty.NextVisible(false);
        }

        if (currentProperty.NextVisible(true))
        {
            do
            {
                if (SerializedProperty.EqualContents(currentProperty, nextSiblingProperty))
                    break;

                yield return currentProperty;
            }
            while (currentProperty.NextVisible(false));
        }
    }

    private void Remove(ReorderableList l)
    {
        var container = (AttributesContainer)fieldInfo.GetValue(target.targetObject);
        container.RemoveAt(l.index);

        l.index--;

        EditorUtility.SetDirty(target.targetObject);
    }

    private void AddDropdown(Rect buttonRect, ReorderableList l)
    {
        var menu = new GenericMenu();
        var types = AttributesManager.Instance.AttributesTypes;

        foreach (var tp in types)
            menu.AddItem(new GUIContent(tp.Name), false, OnAddAtributeTypeSelected, System.Activator.CreateInstance(tp));

        menu.ShowAsContext();
    }

    private void OnAddAtributeTypeSelected(object attr)
    {
        var container = (AttributesContainer)fieldInfo.GetValue(target.targetObject);
        container.Add((AttributeBase)attr);

        EditorUtility.SetDirty(target.targetObject);
    }

}
