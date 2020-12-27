using OverviewRkiData.Components.UserSettings;

namespace OverviewRkiData.Components
{
    public class ComponentFactory
    {

        public static IUserSettingsComponent GetUserSettingsComponent()
        {
            return UserSettingsLoader.GetInstance();
        }

    }
}
