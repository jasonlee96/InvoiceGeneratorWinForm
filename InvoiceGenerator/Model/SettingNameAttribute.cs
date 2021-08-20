using System;

namespace InvoiceGenerator.Model
{
    internal class SettingNameAttribute : Attribute
    {
        public string Name;
        public SettingNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}