using System.Windows;
using System.Windows.Media;

namespace MaquetteBotanic
{
    /// <summary>
    /// Interaction logic for Commander.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly Brush couleurBotanic = new SolidColorBrush(Color.FromRgb(0, 99, 76));

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

        private void btnJardin_Click(object sender, RoutedEventArgs e)
        {
            panelSousCategories.Visibility = Visibility.Visible;

            btnJardin.Background = couleurBotanic;
            btnJardin.Foreground = Brushes.White;
        }

        private void btnPlantes_Click(object sender, RoutedEventArgs e)
        {
            gridFiltres.Visibility = Visibility.Visible;
            dgProduits.Visibility = Visibility.Visible;
            tbkTotal.Visibility = Visibility.Visible;
            btnAcheter.Visibility = Visibility.Visible;

            btnPlantes.Background = couleurBotanic;
            btnPlantes.Foreground = Brushes.White;
        }


    }
}
