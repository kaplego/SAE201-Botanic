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
            bool connecte = connexion.ShowDialog() ?? false;

            if (!connecte)
            {
                this.Close();
                return;
            }

            InitializeComponent();

            nomMagasin.Text = "Magasin : " + ApplicationData.Instance.MagasinActuel.Nom;
            nomUtilisateur.Text = ApplicationData.Instance.UtilisateurActuel.Nom;

            // Charger les types de produits
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
            // Parcourir tous les boutons de type
            foreach (object obj in btnsTypes.Children)
            {
                if (obj is Button btn)
                {
                    if (btn == sender)
                    {
                        // Si le bouton est le bouton cliqué
                        int idType = int.Parse(btn.Name.Substring(4));

                        // Effacer les anciennes catégories
                        btnsCategories.Children.Clear();

                        // Ajouter les catégories pour le type sélectionné
                        foreach (Categorie cat in ApplicationData.Filter(ApplicationData.Instance.Categories, (i) => i.Type.Id == idType))
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

                        // Mettre en surbrillance le bouton
                        btn.Background = couleurBotanic;
                        btn.Foreground = Brushes.White;
                    }
                    else
                    {
                        // Supprimer la surbrillance des autres boutons
                        btn.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFD9D9D9")!;
                        btn.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF393939")!;
                    }
                }
            }
        }

        private void ButtonCategorie_Click(object sender, RoutedEventArgs e)
        {
            // Parcourir tous les boutons de catégorie
            foreach (object obj in btnsCategories.Children)
            {
                if (obj is Button btn)
                {
                    if (btn == sender)
                    {
                        // Si le bouton est le bouton cliqué
                        int idCategorie = int.Parse(btn.Name.Substring(6));

                        // Mettre à jour les produits filtrés par la catégorie
                        dgProduits.ItemsSource = ApplicationData.Filter(ApplicationData.Instance.Produits, (p) =>
                        {
                            return
                                p.LeProduit.LaCategorie.Id == idCategorie &&
                                ApplicationData.Find(ApplicationData.Instance.ProduitsFournits, (pf) =>
                                    pf.LeProduit.Id == p.LeProduit.Id)?.LeFournisseur.Local == false;
                        });

                        gridFiltres.Visibility = Visibility.Visible;
                        dgProduits.Visibility = Visibility.Visible;
                        tbkTotal.Visibility = Visibility.Visible;
                        btnAcheter.Visibility = Visibility.Visible;

                        // Mettre en surbrillance le bouton
                        btn.Background = couleurBotanic;
                        btn.Foreground = Brushes.White;
                    }
                    else
                    {
                        // Supprimer la surbrillance des autres boutons
                        btn.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFD9D9D9")!;
                        btn.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF393939")!;
                    }
                }
            }
        }


    }
}
