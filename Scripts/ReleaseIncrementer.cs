// This file contains the code for the ReleaseIncrementer functions

#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System;

namespace Hibzz
{
    public static partial class EditorToys
    {
        internal class ReleaseIncrementer : IPreprocessBuildWithReport
        {
            public int callbackOrder => 0;

            public void OnPreprocessBuild(BuildReport report)
            {
                // TODO: add a boolean toggle to enable/disable this tool

                // Ask the user if they want to increment the release version
                // A response of "1" means the user decided to not increment the release version
                int dialog_response = EditorUtility.DisplayDialogComplex("Editor Toys", "Increment the build version?", "Minor", "None", "Patch");
                if(dialog_response == 1) { return; }

                // the user wants to increment the minor/patch version
                string version_string = PlayerSettings.bundleVersion;
                Version version = Version.Parse(version_string);

                // the user has requested a minor increment
                if(dialog_response == 0)
                {
                    version = new Version(version.Major, version.Minor + 1, 0);
                }

                // the user has requested a patch increment
                else if(dialog_response == 2)
                {
                    version = new Version(version.Major, version.Minor, version.Build + 1);
                }

                // convert the version back to string and update the bundle version on the player settings
                PlayerSettings.bundleVersion = version.ToString();
            }
        }
    }
}

#endif
