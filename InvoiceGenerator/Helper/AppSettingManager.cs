using System;
using System.Configuration;
using System.Reflection;
using InvoiceGenerator.Model;

namespace InvoiceGenerator.Helper
{
    public class AppSettingManager: ISettingManager
    {
        public T Load<T>() where T : new()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var appSetting = config.AppSettings.Settings;

            T setting = new T();
            var props = setting.GetType().GetProperties();
            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttribute<SettingNameAttribute>();

                string name = attr.Name ?? prop.Name;

                var value = appSetting[name].Value;

                if (value == null) continue;

                System.TypeCode typeCode = Type.GetTypeCode(prop.PropertyType);
                switch (typeCode)
                {
                    case TypeCode.Boolean:
                        prop.SetValue(setting, bool.Parse(value.ToString()));
                        break;
                    case TypeCode.String:
                        prop.SetValue(setting, value.ToString());
                        break;
                    case TypeCode.Int32:
                        prop.SetValue(setting, int.Parse(value.ToString()));
                        break;
                    case TypeCode.Decimal:
                        prop.SetValue(setting, decimal.Parse(value.ToString()));
                        break;
                    case TypeCode.Double:
                        prop.SetValue(setting, double.Parse(value.ToString()));
                        break;
                    case TypeCode.Int64:
                        prop.SetValue(setting, long.Parse(value.ToString()));
                        break;
                    default: break;
                }

            }

            return setting;
        }

        public void Write<T>(T setting)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var props = setting.GetType().GetProperties();
            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttribute<SettingNameAttribute>();

                string name = attr.Name ?? prop.Name;

                config.AppSettings.Settings[name].Value = prop.GetValue(setting).ToString();
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
