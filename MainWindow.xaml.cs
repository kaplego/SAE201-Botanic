using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
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
            LoadTypesCategories();
        }

        private void LoadTypesCategories()
        {
            foreach (TypeProduit type in ApplicationData.Instance.TypesProduit)
            {
                Button btn = new Button();
                btn.Margin = new Thickness(0, 0, 5, 0);
                btn.Content = type.Nom;
                btn.Name = $"type{type.Id}";
                btn.Style = (Style)this.FindResource("ButtonsCategories");
                btn.Click += ButtonType_Click;

                btnsTypes.Children.Add(btn);
            }
        }

        private void ButtonType_Click(object sender, RoutedEventArgs e)
        {
            foreach (object obj in btnsTypes.Children)
            {
                if (obj is Button btn)
                {
                    if (btn == sender)
                    {
                        int id = int.Parse(btn.Name.Substring(4));

                        btnsCategories.Children.Clear();
                        foreach (Categorie cat in ApplicationData.Filter(ApplicationData.Instance.Categories, (i) => i.Type.Id == id))
                        {
                            Button btnCat = new Button();
                            btnCat.Margin = new Thickness(0, 0, 5, 0);
                            btnCat.Content = cat.Nom;
                            btnCat.Name = $"catego{cat.Id}";
                            btnCat.Style = (Style)this.FindResource("ButtonsCategories");
                            btnCat.Click += ButtonCategorie_Click;

                            btnsCategories.Children.Add(btnCat);
                        }
                        panelSousCategories.Visibility = Visibility.Visible;
                        gridFiltres.Visibility = Visibility.Collapsed;
                        dgProduits.Visibility = Visibility.Collapsed;
                        tbkTotal.Visibility = Visibility.Collapsed;
                        btnAcheter.Visibility = Visibility.Collapsed;

                        btn.Background = couleurBotanic;
                        btn.Foreground = Brushes.White;
                    }
                    else
                    {
                        btn.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFD9D9D9")!;
                        btn.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF393939")!;
                    }
                }
            }
        }

        private void ButtonCategorie_Click(object sender, RoutedEventArgs e)
        {
            foreach (object obj in btnsCategories.Children)
            {
                if (obj is Button btn)
                {
                    if (btn == sender)
                    {
                        int id = int.Parse(btn.Name.Substring(6));

                        dgProduits.ItemsSource = ApplicationData.Filter(ApplicationData.Instance.Produits, (p) => p.LeProduit.LaCategorie.Id == id);

                        gridFiltres.Visibility = Visibility.Visible;
                        dgProduits.Visibility = Visibility.Visible;
                        tbkTotal.Visibility = Visibility.Visible;
                        btnAcheter.Visibility = Visibility.Visible;

                        btn.Background = couleurBotanic;
                        btn.Foreground = Brushes.White;
                    }
                    else
                    {
                        btn.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFD9D9D9")!;
                        btn.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF393939")!;
                    }
                }
            }
        }


    }
}
