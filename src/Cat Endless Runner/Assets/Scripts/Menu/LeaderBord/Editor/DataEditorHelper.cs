using System.IO;
using UnityEditor;
using UnityEngine;

namespace Menu.LeaderBord.Editor
{
    public static class DataEditorHelper
    {
        [MenuItem("Helper/OpenDataFolder")]
        public static void OpenDataFolder()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }
        
        [MenuItem("Helper/ClearPlayerPrefs")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        [MenuItem("Helper/ClearData")]
        public static void ClearData()
        {
            Debug.Log("Delete Data Folder");
            if (Directory.Exists(Application.persistentDataPath))
            {
                Directory.Delete(Application.persistentDataPath, true);

                Debug.Log("Data deleted");
            }
            else
            {
                Debug.Log("Data already empty");
            }
        }
    }
}