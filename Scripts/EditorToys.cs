#if UNITY_EDITOR

using UnityEditor;

namespace Hibzz.EditorToys
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
            // initialize variable required to calculate editor delta time and
            // subscribe to the editor update event
            lastTimeSinceStartup = EditorApplication.timeSinceStartup;
            EditorApplication.update += EditorToysUpdate;

            // create the instance of the object at runtime and add hooks to it
            // accessing the instance property is a cheap way to create the object
            var hooks = EditorToysHooks.Instance;

            #if !DISABLE_PRINT_TO_SCREEN
            EditorToysHooks.OnGuiHandler += PrintQueue.OnGui;
            #endif

            // remove all hooks when the hook object is destroyed
            EditorToysHooks.HookDestroyHandler += RemoveSelfFromEditorHook;
        }

        /// <summary>
        /// Called every frame on the editor
        /// </summary>
        private static void EditorToysUpdate()
        {
            // calculate the editor delta time
            EditorDeltaTime = (float)(EditorApplication.timeSinceStartup - lastTimeSinceStartup);
            lastTimeSinceStartup = EditorApplication.timeSinceStartup;

            // call update for other editor tools
            #if !DISABLE_PRINT_TO_SCREEN
            PrintQueue.Update();
            #endif
        }

        private static void RemoveSelfFromEditorHook()
        {
            #if !DISABLE_PRINT_TO_SCREEN
            EditorToysHooks.OnGuiHandler -= PrintQueue.OnGui;
            #endif

            EditorToysHooks.HookDestroyHandler -= RemoveSelfFromEditorHook;
        }
    }
}

#endif
