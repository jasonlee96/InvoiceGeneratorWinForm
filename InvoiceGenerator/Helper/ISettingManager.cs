using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceGenerator.Helper
{
    public interface ISettingManager
    {
        T Load<T>() where T : new();

        void Write<T>(T setting);
    }
}
