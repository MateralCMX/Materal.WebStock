using Materal.WebStockClient.Common;
using System;

namespace Materal.WebStockClient.Commands
{
    public static class CommandExtend
    {
        public static string GetHandelerName(this IWebStockClientCommand command)
        {
            string name = string.Empty;
            Type objType = command.GetType();
            object[] attrs = objType.GetCustomAttributes(typeof(HandlerAttribute), false);
            if (attrs == null || attrs.Length == 0) throw new ArgumentException("需要特性HandlerAttribute");
            foreach (HandlerAttribute attr in attrs)
            {
                name = attr.HandlerName;
            }
            return name;
        }
    }
}
