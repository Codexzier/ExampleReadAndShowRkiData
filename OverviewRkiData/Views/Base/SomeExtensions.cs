using OverviewRkiData.Components.UserSettings;
using System;
using System.Collections.Generic;
using System.Text;

namespace OverviewRkiData.Views.Base
{
    public static class SomeStaticMethods
    {
        internal static string GetServiceUrl()
        {
            var setting = UserSettingsLoader.GetInstance().Load();

            // only for development
            if (string.IsNullOrEmpty(setting.ServiceAddress))
            {
                return "http://localhost:5010";
            }

            return $"{setting.ServiceAddress}:{setting.Port}";
        }
    }
}
