using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;
using System.Xml.Linq;

public class MyEditorWindow : EditorWindow
{
    [MenuItem("Window/My Editor Window")]
    public static void ShowExample()
    {
        MyEditorWindow wnd = GetWindow<MyEditorWindow>();
        wnd.titleContent = new GUIContent("MyEditorWindow");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy
        Label label = new Label("Hello World!");
        root.Add(label);

        // Create button
        Button button = new Button();
        button.name = "button";
        button.text = "Button";
        root.Add(button);

        var button1 = new Button { text = "Click me" };
        button1.clicked += OnClick;
        root.Add(button1);

        // Create toggle
        Toggle toggle = new Toggle();
        toggle.name = "toggle";
        toggle.label = "Toggle";
        root.Add(toggle);
    }

    public void OnClick()
    {
        Spawner spawner = FindObjectOfType<Spawner>();
        spawner.SpawnItems();
    }

    
}
