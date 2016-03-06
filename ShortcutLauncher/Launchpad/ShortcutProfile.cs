using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace ShortcutLauncher
{
    /// <summary>
    /// The class that represents a profile of shortcuts.
    /// </summary>
    [XmlInclude(typeof(ApplicationShortcut))]
    [XmlInclude(typeof(DocumentShortcut))]
    [XmlInclude(typeof(FolderShortcut))]
    [XmlInclude(typeof(WebsiteShortcut))]
    public class ShortcutProfile
    {
        /// <summary>
        /// The profile's list of shortcuts.
        /// </summary>
        private List<Shortcut> shortcuts = new List<Shortcut>();
        //{
        //    new FolderShortcut { Description = "Xampp htdocs", Path = @"C:\xampp\htdocs", Type = ShortcutType.Folder },
        //    new ApplicationShortcut { Description = "Skype", Path = @"C:\Skype\Phone\Skype.exe", Type = ShortcutType.Application },
        //    new FolderShortcut { Description = "Dropbox", Path = @"C:\Users\owner\Dropbox", Type = ShortcutType.Folder },
        //    new WebsiteShortcut { Description = "Blackboard", Path = @"http://elearn.ntc.edu", Type = ShortcutType.Website },
        //    new WebsiteShortcut { Description = "Google", Path = @"http://www.google.com", Type = ShortcutType.Website },
        //    new ApplicationShortcut { Description = "Notepad", Path = "notepad.exe", Type = ShortcutType.Application },
        //    new DocumentShortcut { Description = "GradeInfo.xlsx", Path = @"C:\Desktop\GradeInfo.xlsx", Type = ShortcutType.Document }
        //};

        /// <summary>
        /// Gets the profile's list of shortcuts.
        /// </summary>
        public List<Shortcut> Shortcuts
        {
            get
            {
                return this.shortcuts;
            }
        }

        /// <summary>
        /// Gets the file path where the shortcuts are stored.
        /// </summary>
        /// <returns>The file path.</returns>
        public static string GetFilePath()
        {
            var path = Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["filePath"]);
            var fileName = @"ShortcutLauncher.xml";

            return Path.Combine(path, fileName);
        }
    }
}