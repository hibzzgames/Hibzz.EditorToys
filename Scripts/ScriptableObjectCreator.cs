#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.IO;

namespace Hibzz.EditorToys
{
    public static class ScriptableObjectCreator
    {
        const string MENU_KEY = "Assets/Create/Scriptable Object Instance";

        [MenuItem(MENU_KEY, priority = 120)]
        private static void CreateScriptableObject()
        {
            // not going to do any validation at the moment because of the
            // validation function below... This button will be inactive if the
            // validation fails, so assume that every operation performed here
            // is valid
            var monoscript = Selection.activeObject as MonoScript;
            var class_type = monoscript.GetClass();

            // figure out the new file name of this asset
            var directory_name = Path.GetDirectoryName(AssetDatabase.GetAssetPath(monoscript));
            var filepath = $"{directory_name}/{class_type.Name}.asset";
            var unique_path = AssetDatabase.GenerateUniqueAssetPath(filepath);

            // create the scriptable object and store it in the asset database
            var instance = ScriptableObject.CreateInstance(class_type);
            AssetDatabase.CreateAsset(instance, unique_path);
        }

        [MenuItem(MENU_KEY, validate = true)]
        private static bool CreateScriptableObjectValidate()
        {
            // selected object must be a monobehavior script
            var monoscript = Selection.activeObject as MonoScript;
            if (monoscript is null) { return false; }

            // selected monobehavior script must be derived from ScriptableObject
            var class_type = monoscript.GetClass();
            if (!class_type.IsSubclassOf(typeof(ScriptableObject))) { return false; }

            // all checks passed, the selection is valid
            return true;
        }
    }
}

#endif
