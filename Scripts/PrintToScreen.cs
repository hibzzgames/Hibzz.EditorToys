// This file contains the code for the PrintToScreen Functions

using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Hibzz.EditorToys
{
    #if UNITY_EDITOR

    /// <summary>
    /// Contains code that manages the print queue required for 
    /// EditorToys.PrintToScreen functions to work
    /// </summary>
    internal static class PrintQueue
    {
        /// <summary>
        /// A list of text content that needs to be displayed on screen along with the duration for which they must be displayed
        /// </summary>
        internal static List<PrintData> Queue = new List<PrintData>();

        /// <summary>
        /// The cumulative text to print
        /// </summary>
        private static string textToPrint = "";

        /// <summary>
        /// let's the system know that print to screen requires a refresh
        /// </summary>
        private static bool requiresRefresh = false;

        /// <summary>
        /// The default gui style for the print to screen tool
        /// </summary>
        private static GUIStyle guiStyle = new GUIStyle()
        {
            richText = true,
            normal = new GUIStyleState()
            {
                textColor = Color.white
            }
        };

        /// <summary>
        /// Called once every editor update
        /// </summary>
        internal static void Update()
        {
            // on update we reset what text needs to be printed
            textToPrint = "";

            // early exit if the print queue is empty
            if (Queue.Count <= 0)
            {
                // There's a special case where the text has been set to be
                // empty because there's nothing on the queue. However, because
                // of how this early exit code is done to optimize unnecessary
                // calls, it would not invoke the ongui event manually. So, the
                // last text that was printed to the screen would stay there
                // until something on the scene changes. To account for that, a
                // boolean "requiresRefresh" is introduced as a flag which lets
                // the system know to queue the player update loop on the
                // editor so the on gui event is called refreshing the text
                if (requiresRefresh)
                {
                    // look down a couple of blocks on why this is required
                    if (!Application.isPlaying)
                    {
                        EditorApplication.QueuePlayerLoopUpdate();
                    }

                    // since the refresh was done, no longer need to do it
                    // multiple times
                    requiresRefresh = false;
                }

                return;
            }

            // loop through everything in the print queue and gathering content
            // additionally, decrement duration
            foreach (var data in Queue)
            {
                textToPrint += $"{data.Text}\n";
                data.Duration -= EditorToys.EditorDeltaTime;
            }

            // remove all data whose whose duration has elapsed
            Queue.RemoveAll((data) => data.Duration < 0);

            // when in editor mode, according to the documentation, the OnGui
            // function doesn't get called every frame, instead, only when
            // things are changed on screen, hence, it's possible that things
            // don't get printed accurately on screen for the specified duration.
            // So, we are going to manually invoke the OnGui call when in editor
            // mode whenever we tick down the timer on the print queue. It can
            // probably be a bit more efficient and conservative with how
            // frequently this call is made, but that's something I want to
            // think about later.
            if (!Application.isPlaying)
            {
                EditorApplication.QueuePlayerLoopUpdate();
            }

            // ask the system to refresh gui to account for one special case,
            // where the queue is empty but still requires one last refresh
            // when the application is in editor mode
            requiresRefresh = true;
        }

        /// <summary>
        /// Perform the actual print to screen using the GUI.Label 
        /// </summary>
        internal static void OnGui()
        {
            if (string.IsNullOrWhiteSpace(textToPrint)) { return; }
            GUI.Label(new Rect(10, 10, Screen.width, Screen.height), textToPrint, guiStyle);
        }

        /// <summary>
        /// A class representing the data that needs to be printed along with any other required information
        /// </summary>
        internal class PrintData
        {
            public string Text;
            public float Duration;
        }
    }

    #endif

    public static partial class EditorToys
    {
        /// <summary>
        /// Prints the given text to the screen for a single frame
        /// </summary>
        /// <param name="text">The text to display on the screen</param>
        public static void PrintToScreen(string text)
        {
            // default duration of 0 and a color of white
            PrintToScreen(text, 0, Color.white);
        }

        /// <summary>
        /// Print the given text to the screen with the given color for a single frame
        /// </summary>
        /// <param name="text">The text to display on the screen</param>
        /// <param name="color">The color of the text as it gets printed</param>
        public static void PrintToScreen(string text, Color color)
        {
            PrintToScreen(text, 0, color);
        }

        /// <summary>
        /// Print the given text to the screen
        /// </summary>
        /// <param name="text">The text to display on the screen</param>
        /// <param name="duration">
        /// How long should the text appear on screen
        /// <list type="bullet">
        /// If a duration of 0 is given, then it displays for 1 frame
        /// </list>
        /// </param>
        public static void PrintToScreen(string text, float duration)
        {
            // The default color is white
            PrintToScreen(text, duration, Color.white);
        }

        /// <summary>
        /// Print the given text to the screen with the given color
        /// </summary>
        /// <param name="text">The text to display on the screen</param>
        /// <param name="duration">
        /// How long should the text appear on screen
        /// <list type="bullet">
        /// If a duration of 0 is given, then it displays for 1 frame
        /// </list>
        /// </param>
        /// <param name="color">The color of the text as it gets printed</param>
        public static void PrintToScreen(string text, float duration, Color color)
        {
            #if UNITY_EDITOR

            // interpolate string based on the color
            text = $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{text}</color>";

            // add the given content to the queue
            PrintQueue.Queue.Add(new PrintQueue.PrintData { Text = text, Duration = duration });

            #endif
        }
    }
}
