// This file contains the code for the PrintToScreen Functions

using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Hibzz
{
    public static partial class EditorToys
    {
        #if UNITY_EDITOR

        /// <summary>
        /// A list of text content that needs to be displayed on screen along with the duration for which they must be displayed
        /// </summary>
        private static List<PrintData> printQueue = new List<PrintData>();

        /// <summary>
        /// The cumulative text to print
        /// </summary>
        private static string textToPrint = "";

        /// <summary>
        /// The default gui style for the print to screen tool
        /// </summary>
        private static GUIStyle printGuiStyle = new GUIStyle() { 
            richText = true,
            normal = new GUIStyleState()
            {
                textColor = Color.white
            }
        };

        /// <summary>
        /// Called once every editor update
        /// </summary>
        private static void PrintQueueUpdate()
        {
            // on update we reset what text needs to be printed
            textToPrint = "";

            // early exit if the print queue is empty
            if (printQueue.Count <= 0) { return; }

            // loop through everything in the print queue and gathering content
            // additionally, decrement duration
            foreach(var data in printQueue)
            {
                textToPrint += $"{data.Text}\n";
                data.Duration -= EditorDeltaTime;
            }

            // remove all data whose whose duration has elapsed
            printQueue.RemoveAll((data) => data.Duration < 0);

            // when in editor mode, according to the documentation, the OnGui
            // function doesn't get called every frame, instead, only when
            // things are changed on screen, hence, it's possible that things
            // don't get printed accurately on screen for the specified duration.
            // So, we are going to manually invoke the OnGui call when in editor
            // mode whenever we tick down the timer on the print queue. It can
            // probably be a bit more efficient and conservative with how
            // frequently this call is made, but that's something I want to
            // think about later.
            if(!Application.isPlaying)
            {
                EditorApplication.QueuePlayerLoopUpdate();
            }
        }

        private static void PrintQueueOnGui()
        {
            if(string.IsNullOrWhiteSpace(textToPrint)) { return; }
            GUI.Label(new Rect(10,10,Screen.width, Screen.height), textToPrint, printGuiStyle);
        } 

        #endif

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
            printQueue.Add(new PrintData { Text = text, Duration = duration });

            #endif
        }

        #if UNITY_EDITOR

        /// <summary>
        /// A class representing the data that needs to be printed along with any other required information
        /// </summary>
        private class PrintData
        {
            public string Text;
            public float Duration;
        }

        #endif
    }
}
