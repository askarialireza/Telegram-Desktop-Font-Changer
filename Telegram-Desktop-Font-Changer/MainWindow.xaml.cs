using System.Linq;
using static System.Windows.Media.Fonts;

namespace Telegram_Desktop_Font_Changer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private System.Collections.ObjectModel.ObservableCollection<string> Fonts { get; }

        private string SelectedFont { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Fonts = new System.Collections.ObjectModel.ObservableCollection<string>(SystemFontFamilies.OrderBy(x => x.Source).Select(x => x.Source).ToList());
            DataContext = Fonts;
            SelectedFont = string.Empty;
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        private void FontsComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelectedFont = FontsComboBox.SelectedValue.ToString();
        }

        private void ChangeFontButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SelectedFont) != true)
            {
                try
                {
                    var RegisteryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\FontSubstitutes", true);

                    if (RegisteryKey != null)
                    {
                        RegisteryKey.SetValue("MS Shell Dlg 2", SelectedFont, Microsoft.Win32.RegistryValueKind.String);
                        RegisteryKey.Close();

                        System.Windows.MessageBox.Show(
                            messageBoxText: "Your Telegram Desktop Font Successfully Changed!\nPlease Restart Your Telegram Application",
                            caption: "Done",
                            button: System.Windows.MessageBoxButton.OK,
                            icon: System.Windows.MessageBoxImage.Information,
                            defaultResult: System.Windows.MessageBoxResult.OK
                            );
                    }
                }
                catch (System.Exception exception)
                {
                    System.Windows.MessageBox.Show(
                        messageBoxText: "Your are not in Administrator Mode!\nPlease Run This app as an Administrator",
                        caption: "Error",
                        button: System.Windows.MessageBoxButton.OK,
                        icon: System.Windows.MessageBoxImage.Error,
                        defaultResult: System.Windows.MessageBoxResult.OK
                        );
                }
            }
        }
    }
}
