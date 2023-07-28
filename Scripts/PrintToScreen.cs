// This file contains the code for the PrintToScreen Functions

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Hibzz
{
    public static partial class EditorToys
    {
        /// <summary>
        /// A list of text content that needs to be displayed on screen along with the duration for which they must be displayed
        /// </summary>
        private static List<PrintData> printQueue = new List<PrintData>();

        /// <summary>
        /// Called once every editor update
        /// </summary>
        private static void PrintQueueUpdate()
        {
            // early exit if the print queue is empty
            if(printQueue.Count <= 0) { return; }

            // loop through everything in the print queue and gathering content
            // additionally, decrement duration
            string finalout = "";
            foreach(var data in printQueue)
            {
                finalout += $"{data.Text}\n";
                data.Duration -= EditorDeltaTime;
            }

            // remove all data whose whose duration has elapsed
            printQueue.RemoveAll((data) => data.Duration < 0);

            // TODO: figure out how to print this
        }

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
            // interpolate string based on the color
            text = $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{text}</color>";

            // add the given content to the queue
            printQueue.Add(new PrintData { Text = text, Duration = duration });
        }

        /// <summary>
        /// A class representing the data that needs to be printed along with any other required information
        /// </summary>
        private class PrintData
        {
            public string Text;
            public float Duration;
        }
    }
}
