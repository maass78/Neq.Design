using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Xml;

namespace Neq.Design.WPF.Themes.ThemeSaveHandlers
{
    public class SerializeThemesSave
    {
        public ResourceDictionary[] Themes { get; set; }

        public int CurrentThemeIndex { get; set; }
    }

    public class FileThemeSaveHandler : IThemeSaveHandler
    {
        private static readonly object _locker = new object();

        private List<ThemesSave> _saves = new List<ThemesSave>();
        public string FileName { get; set; }

        public event EventHandler Saving;

        public FileThemeSaveHandler(string fileName)
        {
            FileName = fileName;
        }

        private void SaveNow(ThemesSave save)
        {
            var serializeSB = new StringBuilder();

            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };

            var dsm = new XamlDesignerSerializationManager(XmlWriter.Create(serializeSB, settings));

            var serialize = new SerializeThemesSave()
            {
                Themes = save.Themes.Select(x => x.CompiledResources).ToArray(),
                CurrentThemeIndex = save.Themes.IndexOf(save.CurrentTheme)
            };

            XamlWriter.Save(serialize, dsm);

            lock (_locker)
            {
                Saving?.Invoke(this, new EventArgs());
                File.WriteAllText(FileName, serializeSB.ToString());
            }
        }

        public async void Save(ThemesSave save)
        {
            if (_saves.Count > 0)
                _saves[_saves.Count - 1].HasNewSave = true;

            _saves.Add(save);

            await Task.Delay(500);

            if (save.HasNewSave)
                return;

            SaveNow(save);
            _saves.Clear();
        }

        public ThemesSave Load()
        {
            if (!File.Exists(FileName))
                return null;

            using (var reader = new FileStream(FileName, FileMode.Open, FileAccess.Read))
            {
                var deserialize = (SerializeThemesSave)XamlReader.Load(XmlReader.Create(reader, new XmlReaderSettings()));

                var themes = new Helpers.FullyObservableCollection<Theme>(deserialize.Themes.Select(x => new Theme(x)));

                return new ThemesSave(themes, themes[deserialize.CurrentThemeIndex]);
            }
        }
    }
}
