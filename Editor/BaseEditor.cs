using System;
using System.Text;
using System.Linq.Expressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
///
/// Part of PostProcessingStack.
/// 
/// Original:
/// https://github.com/Hitomilras/PostProcessing/blob/v2/PostProcessing/Editor/BaseEditor.cs
/// 
/// Small wrapper on top of <see cref="Editor"/> to ease the access of the underlying component
/// and its serialized fields.
/// </summary>
/// <typeparam name="T">The type of the target component to make an editor for</typeparam>
/// <example>
/// <code>
/// public class MyMonoBehaviour : MonoBehaviour
/// {
///     public float myProperty = 1.0f;
/// }
/// 
/// [CustomEditor(typeof(MyMonoBehaviour))]
/// public sealed class MyMonoBehaviourEditor : BaseEditor&lt;MyMonoBehaviour&gt;
/// {
///     SerializedProperty m_MyProperty;
/// 
///     void OnEnable()
///     {
///         m_MyProperty = FindProperty(x => x.myProperty);
///     }
/// 
///     public override void OnInspectorGUI()
///     {
///         EditorGUILayout.PropertyField(m_MyProperty);
///     }
/// }
/// </code>
/// </example>
///

public class BaseEditor<T> : Editor
    where T : MonoBehaviour
{
    /// <summary>
    /// The target component.
    /// </summary>
    protected T m_Target
    {
        get { return (T)target; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="expr"></param>
    /// <returns></returns>
    protected SerializedProperty FindProperty<TValue>(Expression<Func<T, TValue>> expr)
    {
        return serializedObject.FindProperty(GetFieldPath(expr));
    }

    /// <summary>
    /// Returns a string path from an expression. This is mostly used to retrieve serialized
    /// properties without hardcoding the field path as a string and thus allowing proper
    /// refactoring features.
    ///
    /// /// Part of PostProcessingStack.
    /// 
    /// Original:
    /// https://github.com/Hitomilras/PostProcessing/blob/v2/PostProcessing/Runtime/Utils/RuntimeUtilities.cs
	/// 
    /// </summary>
    /// <typeparam name="TType">The class type where the member is defined</typeparam>
    /// <typeparam name="TValue">The member type</typeparam>
    /// <param name="expr">An expression path fo the member</param>
    /// <returns>A string representation of the expression path</returns>
    protected static string GetFieldPath<TType, TValue>(Expression<Func<TType, TValue>> expr)
    {
        MemberExpression me;
        switch (expr.Body.NodeType)
        {
            case ExpressionType.MemberAccess:
                me = expr.Body as MemberExpression;
                break;
            default:
                throw new InvalidOperationException();
        }

        var members = new List<string>();
        while (me != null)
        {
            members.Add(me.Member.Name);
            me = me.Expression as MemberExpression;
        }

        var sb = new StringBuilder();
        for (int i = members.Count - 1; i >= 0; i--)
        {
            sb.Append(members[i]);
            if (i > 0) sb.Append('.');
        }

        return sb.ToString();
    }

}
