#if ENABLE_DEFINE_MANAGER

using Hibzz.DefineManager;

namespace Hibzz.EditorToys
{
    internal static class DefineRegistrant
    {
        const string Category = "Hibzz.EditorToys";

        [RegisterDefine]
        static DefineRegistrationData RegisterPrintToScreen()
        {
            DefineRegistrationData data = new DefineRegistrationData();

            data.Define = "DISABLE_PRINT_TO_SCREEN";
            data.DisplayName = "Disable Print To Screen";
            data.Category = Category;
            data.EnableByDefault = false;
            data.Description = "Print to Screen is an editor toy that let's " +
                "users print debug statements to the screen with the given " +
                "color for the requested duration.\n\n" +
                "Installing this define will disable this tool.";

            return data;
        }

        [RegisterDefine]
        static DefineRegistrationData RegisterReleaseIncrementor()
        {
            DefineRegistrationData data = new DefineRegistrationData();

            data.Define = "DISABLE_RELEASE_INCREMENTOR";
            data.DisplayName = "Disable Release Incrementor";
            data.Category = Category;
            data.EnableByDefault = false;
            data.Description = "The Release Incrementor, when enabled, prompts " +
                "the user to increase the version number of the application " +
                "(the patch or the minor number) when the user creates a new " +
                "build.\n\n" +
                "Installing this define disables the Release Incrementor.";

            return data;
        }

        [RegisterDefine]
        static DefineRegistrationData RegisterScriptableObjectCreator()
        {
            DefineRegistrationData data = new DefineRegistrationData();

            data.Define = "DISABLE_SCRIPTABLE_OBJECT_CREATOR";
            data.DisplayName = "Disable Scriptable Object Creator";
            data.Category = Category;
            data.EnableByDefault = false;
            data.Description = "The Scriptable Object Creator is a utility that " +
                "lets users right click on a script file that contains a class " +
                "that inherits ScriptableObject and create new scriptable " +
                "object instance without any other additional menu items.\n\n" +
                "Installing this define disables the Scriptable Object Creator";

            return data;
        }
    }
}

#endif
