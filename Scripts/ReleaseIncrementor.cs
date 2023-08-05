// This file contains the code for the ReleaseIncrementor functions

#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System;

namespace Hibzz
{
    public static partial class EditorToys
    {
        internal class ReleaseIncrementor : IPreprocessBuildWithReport
        {
            const string MENU_KEY = "Hibzz/Editor Toys/Release Incrementor";

            public int callbackOrder => 0;

            [InitializeOnLoadMethod]
            public static void Initialize()
            {
                // ISSUE: doesn't work when it's first called when the editor is launched
                Menu.SetChecked(MENU_KEY, EditorPrefs.GetBool(MENU_KEY, false));
            }

            public void OnPreprocessBuild(BuildReport report) 
            {
                // only execute the tool's code if it was enabled
                if (!EditorPrefs.GetBool(MENU_KEY, false)) { return; }

                // Ask the user if they want to increment the release version
                // A response of "1" means the user decided to not increment the release version
                int dialog_response = EditorUtility.DisplayDialogComplex("Editor Toys", "Increment the build version?", "Minor", "None", "Patch");
                if (dialog_response == 1) { return; }

                // the user wants to increment the minor/patch version
                string version_string = PlayerSettings.bundleVersion;
                Version version = Version.Parse(version_string);

                // the user has requested a minor increment
                if (dialog_response == 0)
                {
                    version = new Version(version.Major, version.Minor + 1, 0);
                }

                // the user has requested a patch increment
                else if (dialog_response == 2)
                {
                    version = new Version(version.Major, version.Minor, version.Build + 1);
                }

                // convert the version back to string and update the bundle version on the player settings
                PlayerSettings.bundleVersion = version.ToString();
            }

            [MenuItem(MENU_KEY)]
            static void ToggleReleaseIncrementor()
            {
                Menu.SetChecked(MENU_KEY, !EditorPrefs.GetBool(MENU_KEY, false));
                EditorPrefs.SetBool(MENU_KEY, Menu.GetChecked(MENU_KEY));
            }
        }
    }
}

#endif
