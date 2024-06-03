using System.Windows;

namespace MaquetteBotanic
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Connexion connexion = new Connexion();
            bool ok = connexion.ShowDialog() ?? false;

            if (!ok)
            {
                this.Close();
                return;
            }
            
            InitializeComponent();
        }
    }
}
