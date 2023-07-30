#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Hibzz
{
    public static partial class EditorToys
    {
        /// <summary>
        /// The last time since startup of the application... Useful to calculate editor delta time
        /// </summary>
        private static double lastTimeSinceStartup;

        /// <summary>
        /// Get the delta time on the editor
        /// </summary>
        public static float EditorDeltaTime { get; private set; }

        /// <summary>
        /// Initialize the print queue system
        /// </summary>
        [InitializeOnLoadMethod]
        private static void InitializeEditorToys()
        {
            lastTimeSinceStartup = EditorApplication.timeSinceStartup;
            EditorApplication.update += EditorToysUpdate;

            // create the instance of the object at runtime and add hooks to it
            var hooks = EditorToysHooks.Instance; // cheap way to create the object
            EditorToysHooks.OnGuiHandler += PrintQueueOnGui;
        }

        /// <summary>
        /// Called every frame on the editor
        /// </summary>
        private static void EditorToysUpdate()
        {
            // calculate the editor delta time
            EditorDeltaTime = (float) (EditorApplication.timeSinceStartup - lastTimeSinceStartup);
            lastTimeSinceStartup = EditorApplication.timeSinceStartup;

            // call update for other editor tools
            PrintQueueUpdate();
        }
    }
}

#endif
