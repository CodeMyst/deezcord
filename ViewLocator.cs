using System;

using Avalonia.Controls;
using Avalonia.Controls.Templates;

using DeezCord.ViewModels;

namespace DeezCord
{
    public class ViewLocator : IDataTemplate
    {
        public bool SupportsRecycling => false;

        public IControl Build (object data)
        {
            string name = data.GetType ().FullName.Replace ("ViewModel", "View");
            Type type = Type.GetType (name);

            if (type != null)
                return (Control) Activator.CreateInstance (type);
            else
                return new TextBlock { Text = $"Not found: {name}" };
        }

        public bool Match (object data)
        {
            return data is ViewModelBase;
        }
    }
}