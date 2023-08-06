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
    }
}

#endif
