using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Manager.Dialogs
{
    public class DialogStorage
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        private Dictionary<string, Dialog> _dialogs;
        private DialogInterpreter _interpretator;
        public DialogStorage()
        {
            _interpretator= new DialogInterpreter();
            _dialogs = new Dictionary<string, Dialog>();
        }

        public void LoadDialogs()
        {
            // Получаем список всех ресурсов в сборке
            string[] resourceNames = assembly.GetManifestResourceNames();

            // Фильтруем только ресурсы, которые имеют расширение .txt
            var dialogResourceNames = resourceNames.Where(name => name.EndsWith(".txt"));

            foreach (var resourceName in dialogResourceNames)
            {
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        var dialog = _interpretator.Interpret(reader.ReadToEnd());
                        _dialogs[dialog.Id] = dialog;
                    }
                }
            }
        }

        public Dialog GetDialog(string dialogId)
        {
            if (_dialogs.TryGetValue(dialogId, out var dialog))
            {
                return dialog;
            }

            throw new KeyNotFoundException($"Dialog with ID {dialogId} not found.");
        }

        
    }

}
