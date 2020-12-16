using OverviewRkiData.Components.Export;
using OverviewRkiData.Components.Import;
using OverviewRkiData.Components.LocalStorage;
using OverviewRkiData.Components.Service;
using OverviewRkiData.Components.UserSettings;
using System.Runtime.CompilerServices;

namespace OverviewRkiData.Components
{
    public class ComponentFactory
    {
        public static IImportComponent CreateImport()
        {
            return new ImportComponent();
        }

        public static IExportComponent CreateExportComponent()
        {
            return new ExportComponent();
        }

        public static ILocalStorageComponent CreateLocalStorageComponent()
        {
            return new LocalStorageComponent();
        }

        public static IUserSettingsComponent GetUserSettingsComponent()
        {
            return UserSettingsLoader.GetInstance();
        }

        public static IServiceConnector GetServiceConnector()
        {
            var settings = UserSettingsLoader.GetInstance().Load();
            return ServiceConnector.GetInstance(settings.ServiceAddress);
        }
    }
}
