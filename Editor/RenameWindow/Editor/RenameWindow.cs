using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class RenameWindow : EditorWindow
{

    private static RenameWindow m_Instance;

    private const float defaultWidth = 250f, defaultHeight = 20;

    private Action<bool, string> resultFunction;

    private string m_OriginalString;

    private string m_CurrentString;

    public static void RenameString(string original, Action<bool, string> resultFunction)
    {
        if (m_Instance != null)
            m_Instance.Close();

        //   m_Instance = (RenameWindow)EditorWindow.GetWindowWithRect(typeof(RenameWindow), new Rect(Screen.width / 2, Screen.height / 2, defaultWidth, defaultHeight));

        Event e = Event.current;
        Vector2 mousePos = GUIUtility.GUIToScreenPoint(e.mousePosition);

        m_Instance = ScriptableObject.CreateInstance<RenameWindow>();
        m_Instance.position = new Rect(mousePos.x, mousePos.y, defaultWidth, defaultHeight);
        m_Instance.ShowPopup();
        m_Instance.Focus();

        m_Instance.m_OriginalString = original;
        m_Instance.m_CurrentString = original;
        m_Instance.resultFunction = resultFunction;
    }

    public void OnGUI()
    {
        GUILayout.Space(defaultHeight * 0.1f);

        GUI.SetNextControlName("StringRenamer");
        m_CurrentString = GUILayout.TextField(m_CurrentString, GUILayout.Height(defaultHeight * 0.8f));

        GUILayout.Space(defaultHeight * 0.1f);

        Event e = Event.current;

        if (e.keyCode == KeyCode.Escape)
        {
            resultFunction(false, m_OriginalString);
            m_Instance.Close();
        }

        if (e.keyCode == KeyCode.Return)
        {
            resultFunction(true, m_CurrentString);
            m_Instance.Close();
        }

        GUI.FocusControl("StringRenamer");
    }

    public void OnLostFocus()
    {
        m_Instance.Close();
    }


}
