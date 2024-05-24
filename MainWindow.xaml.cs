using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MaquetteBotanic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly List<List<string>> categories = new List<List<string>>() {
            new List<string>() { "Mobilier" },
            new List<string>() { "Potager" },
            new List<string>() { "Jardin", "Plantes", "Semences", "Entretien", "Arrosage", "Décoration", "Biodiversité" },
            new List<string>() { "Balcon & Terrasse" },
            new List<string>() { "Maison" },
            new List<string>() { "Animalerie" },
            new List<string>() { "Alimentation Bio" },
            new List<string>() { "Bien-être, santé & hygiène" }
        };

        public static readonly Brush couleurBotanic = new SolidColorBrush(Color.FromRgb(0, 99, 76));

        public MainWindow()
        {
            InitializeComponent();

            int i = 0;
            foreach (List<string> categorie in categories)
            {
                Button btn = new Button();
                if (i == 0)
                    btn.Margin = new Thickness(0, 0, 4, 0);
                else if (i == categories.Count - 1)
                    btn.Margin = new Thickness(4, 0, 0, 0);

                btn.Content = categorie[0];
                btn.Click += Categorie_Click;
                btn.Style = (Style)this.FindResource("ButtonsCategories");

                btnsCategories.Children.Add(btn);
                i++;
            }
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Categorie_Click(object sender, RoutedEventArgs e)
        {
            btnsSousCategories.Children.Clear();
            foreach (Button btn in btnsCategories.Children)
            {
                btn.ClearValue(BackgroundProperty);
                btn.ClearValue(ForegroundProperty);
            }

            Button btnCat = (Button)sender;
            btnCat.Background = couleurBotanic;
            btnCat.Foreground = Brushes.White;

            string categorie = btnCat.Content.ToString() ?? "";
            List<string>? subcats = categories.Find((cat) => cat[0] == categorie);

            if (subcats == null)
                return;
            int i = 0;
            foreach (string subcat in subcats)
            {
                if (i == 0)
                {
                    i++;
                    continue;
                }

                Button btn = new Button();
                if (i == 1)
                    btn.Margin = new Thickness(0, 0, 4, 0);
                else if (i == subcats.Count - 1)
                    btn.Margin = new Thickness(4, 0, 0, 0);

                btn.Content = subcat;
                btn.Click += SousCategorie_Click;
                btn.Style = (Style)this.FindResource("ButtonsCategories");

                btnsSousCategories.Children.Add(btn);
                i++;
            }

            panelSousCategories.Visibility = Visibility.Visible;
        }

        private void SousCategorie_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button btn in btnsSousCategories.Children)
            {
                btn.ClearValue(BackgroundProperty);
                btn.ClearValue(ForegroundProperty);
            }

            Button btnSubCat = (Button)sender;
            btnSubCat.Background = couleurBotanic;
            btnSubCat.Foreground = Brushes.White;

            gridFiltres.Visibility = Visibility.Visible;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            int index = cb.SelectedIndex;

            if (index == 0)
                return;

            ComboBoxItem item = (ComboBoxItem)cb.Items.GetItemAt(index);

            MessageBox.Show(item.Content.ToString());
        }

        private void AchatProduit_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            string id = cb.Name.Split("_")[1];

            if (id == null)
                return;

            ((TextBox)this.FindName($"qteProduit_{id}")).Visibility = cb.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
