using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace ShortcutLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The MainWindow's shortcut profile.
        /// </summary>
        private ShortcutProfile shortcutProfile;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public MainWindow()
        {
            this.shortcutProfile = new ShortcutProfile();
            this.InitializeComponent();
        }

        /// <summary>
        /// Starts the shortcut that has been clicked.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event's arguments.</param>
        private void shortcutsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Shortcut shortcut = shortcutsListBox.SelectedItem as Shortcut;

            if (shortcut != null)
            {
                Process process = new Process();
                process.StartInfo.FileName = shortcut.Path;

                if (shortcut is ApplicationShortcut)
                {
                    if (shortcut is DocumentShortcut)
                    {
                        process.StartInfo.Arguments = (shortcut as DocumentShortcut).Arguments;
                    }

                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.WorkingDirectory = (shortcut as ApplicationShortcut).WorkingFolder;
                }

                try
                {
                    process.Start();
                }
                catch
                {
                    MessageBox.Show("Could not launch the process.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Loads the window.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event's arguments.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ShortcutProfile));

            string filePath = ShortcutProfile.GetFilePath();

            try
            {
                using (Stream stream = File.OpenRead(filePath))
                {
                    this.shortcutProfile = serializer.Deserialize(stream) as ShortcutProfile;
                }
            }
            catch
            {
                MessageBox.Show("Could not load the XML file.");
            }

            this.PopulateShortcutsListBox();
        }

        /// <summary>
        /// Saves the current state of the manager.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event's arguments.</param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ShortcutProfile));

            string filePath = ShortcutProfile.GetFilePath();

            using (Stream stream = File.Create(filePath))
            {
                serializer.Serialize(stream, this.shortcutProfile);
            }
        }

        /// <summary>
        /// Populates the list box with the list of shortcuts.
        /// </summary>
        private void PopulateShortcutsListBox()
        {
            this.shortcutsListBox.Items.Clear();

            this.shortcutsListBox.ItemsSource = this.shortcutProfile.Shortcuts;
        }
    }
}