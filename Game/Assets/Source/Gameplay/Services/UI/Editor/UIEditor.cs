using System;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Services.UI
{
    public sealed class UIEditor : EditorWindow
    {
        private const string UIViewTypePath = "Assets/Source/Gameplay/Services/UI/UIViewType.cs";
        private const string UIServicePath = "Assets/Source/Gameplay/Services/UI/UIService.cs";

        private const string InfoMessageAfterAddingNewType = "Don't forget to add prefab name to UIConfig."; 
        
        private string _windowType = "";
        private string _baseViewName = "";
        private string _viewModelName = "";
        
        [MenuItem("UI/New UI Type")]
        public static void ShowWindow()
        {
            GetWindow<UIEditor>("UI Editor");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Create new UI view type: ");
            _windowType = EditorGUILayout.TextField("Type:", _windowType);
            _baseViewName = EditorGUILayout.TextField("View Name:", _baseViewName);
            _viewModelName = EditorGUILayout.TextField("Model View Name:", _viewModelName);

            if (GUILayout.Button("Update"))
            {
                CreateUIViewType();
                UpdateUIService();
                EditorUtility.DisplayDialog(nameof(UIEditor), InfoMessageAfterAddingNewType, "ОК");
            }
        }

        private void CreateUIViewType()
        {
            int id = Enum.GetValues(typeof(UIViewType)).Length;
            int previousId = id - 1;
            string fileContent = System.IO.File.ReadAllText(UIViewTypePath);
            string worldType = $"{_windowType} = {id},";
            fileContent = fileContent.Replace($"{previousId},", $"{previousId}," + "\n\t\t" + worldType);
            System.IO.File.WriteAllText(UIViewTypePath, fileContent);
            AssetDatabase.Refresh();
        }
        
        private void UpdateUIService()
        {
            int count = 0;
            string[] lines = System.IO.File.ReadAllLines(UIServicePath);
            string placesForUpdating = $"switch (viewType)";
            
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < lines.Length; i++)
            {
                result.AppendLine(lines[i]);
                
                if (i > 1 && lines[i - 1].Contains(placesForUpdating) && lines[i].Contains("{"))
                {
                    if (count == 0)
                    {
                        result.AppendLine("\t\t\t\tcase UIViewType." + _windowType + ":");
                        result.AppendLine($"\t\t\t\t\tawait Subscribe<{_baseViewName}, {_viewModelName}>(config.UIData[viewType].Name);");
                        result.AppendLine($"\t\t\t\t\tbreak;");
                        count++;
                    }
                    else
                    {
                        result.AppendLine("\t\t\t\tcase UIViewType." + _windowType + ":");
                        result.AppendLine($"\t\t\t\t\tUnsubscribe<{_baseViewName}, {_viewModelName}>();");
                        result.AppendLine("\t\t\t\t\tbreak;");
                    }
                }
            }

            System.IO.File.WriteAllText(UIServicePath, result.ToString());
            AssetDatabase.Refresh();
        }
    }
}