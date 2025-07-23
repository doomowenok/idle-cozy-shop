using System.IO;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.Logger
{
    public static class ExternalLoggerEditor
    {
#if UNITY_EDITOR
        [MenuItem("Logs/Open Logs Directory")]
        public static void OpenLogsDirectory()
        {
            string logsPath = $"{Application.persistentDataPath}/Logs";
            
            if (Directory.Exists(logsPath))
            {
                EditorUtility.RevealInFinder(logsPath);   
            }
            else
            {
                Debug.LogWarning("Logs directory not found, maybe no logs at all for now.");
            }
        }  
#endif
    }
}