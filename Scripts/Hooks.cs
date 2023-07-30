using Hibzz.Singletons;
using UnityEngine;

namespace Hibzz
{
    // This is mostly a function for internal use only, however, if you wish to
    // use it for any purpose, please thread carefully on how you incorporate
    // it into your code. This system isn't battle tested.

    // How does this system work?
    // Since its not possible to directly subscribe to the unity events, I
    // created this system to subscribe to those events. The hooks are made
    // possible because when the editor loads, we create a new gameobject with
    // a singleton monobehavior in it that'll recieve the event calls from
    // Unity using the functions we know. The gameobject is hidden from the
    // end user, doesn't get saved to the scene and doesn't get destroyed by
    // the editor, so it's lifetime is temporary.

    // It's kinda icky to do this, but I can't think of another way to
    // subscribe to those events. I had to make this entire package "non-editor"
    // to accomodate for this system, which sucks. I genuinly hate adding
    // #if UNITY_EDITOR everywhere. Without that this component can't be
    // attached to a gameobject to make this system work.

    // You know what would be cool? EditorBehavior! A monobehavior that exist
    // at editor time as well. If unity supports that, that would make my life
    // a lot more easier.

    // So, although possible, please don't use this system at runtime. And
    // please, don't call EditorToyHooks.Instance at runtime. I have no idea
    // how it'll behave. Use it at your own risk.

    [ExecuteAlways]
    [AddComponentMenu("")]
    public class EditorToysHooks : Singleton<EditorToysHooks>
    {
        // a delegate representing callback from the hooks
        public delegate void CallbackFunction();

        public static CallbackFunction OnGuiHandler;

        protected static new EditorToysHooks CreateNewInstance()
        {
            // add a new object to the scene representing an editor hook
            var editorHookObject = new GameObject("Editor Hook Object");
            editorHookObject.hideFlags = HideFlags.HideAndDontSave;

            // add the hook as a component to the scene
            return editorHookObject.AddComponent<EditorToysHooks>();
        }

        // invoke the static event when on gui is called
        void OnGUI()
        {
            OnGuiHandler?.Invoke();
        }
    }
}
