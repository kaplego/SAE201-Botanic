using System;
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
        public static readonly List<Categorie> categories = new List<Categorie>()
        {
            new Categorie("Mobilier", new List<SousCategorie>()
            {
                new SousCategorie("Mobilier", new List<Produit>()
                {
                    new Produit("Prod Mob1", 1.99, "Produit de mobilier 1", Produit.CouleurProduit.Rouge, Produit.TailleProduit.TresPetit),
                    new Produit("Prod Mob2", 2.99, "Produit de mobilier 2", Produit.CouleurProduit.Bleu, Produit.TailleProduit.Petit),
                })
            }),
            new Categorie("Potager", new List<SousCategorie>()
            {
                new SousCategorie("Potager", new List<Produit>()
                {
                    new Produit("Prod Pot1", 10.99, "Produit de potager 1", Produit.CouleurProduit.Violet, Produit.TailleProduit.Moyen),
                    new Produit("Prod Pot2", 24.99, "Produit de potager 2", Produit.CouleurProduit.Blanc, Produit.TailleProduit.Grand),
                })
            }),
            new Categorie("Jardin", new List<SousCategorie>()
            {
                new SousCategorie("Plantes", new List<Produit>()
                {
                    new Produit("Prod JarP1", 1.99, "Produit de jardin, plante 1", Produit.CouleurProduit.Rouge, Produit.TailleProduit.TresPetit),
                    new Produit("Prod JarP2", 2.99, "Produit de jardin, plante 2", Produit.CouleurProduit.Rouge, Produit.TailleProduit.TresPetit),
                }),
                new SousCategorie("Semences", new List<Produit>()
                {
                    new Produit("Prod JarS1", 1.99, "Produit de jardin, semence 1", Produit.CouleurProduit.Rouge, Produit.TailleProduit.TresPetit),
                    new Produit("Prod JarS2", 28.99, "Produit de jardin, semence 2", Produit.CouleurProduit.Rouge, Produit.TailleProduit.Moyen),
                }),
                new SousCategorie("Entretien", new List<Produit>()
                {
                    new Produit("Prod JarE1", 49.99, "Produit de jardin, entretien 1", Produit.CouleurProduit.Rouge, Produit.TailleProduit.Grand),
                    new Produit("Prod JarE2", 159.99, "Produit de jardin, entretien 2", Produit.CouleurProduit.Autre, Produit.TailleProduit.TresGrand),
                }),
                new SousCategorie("Arrosage", new List<Produit>()
                {
                    new Produit("Prod JarA1", 89.99, "Produit de jardin, arrosage 1", Produit.CouleurProduit.Rouge, Produit.TailleProduit.Moyen),
                    new Produit("Prod JarA2", 209.99, "Produit de jardin, arrosage 2", Produit.CouleurProduit.Noir, Produit.TailleProduit.TresGrand),
                }),
                new SousCategorie("Décoration", new List<Produit>()
                {
                    new Produit("Prod JarD1", 5.99, "Produit de jardin, décoration 1", Produit.CouleurProduit.Orange, Produit.TailleProduit.TresPetit),
                    new Produit("Prod JarD2", 9.99, "Produit de jardin, décoration 2", Produit.CouleurProduit.Rouge, Produit.TailleProduit.Petit),
                }),
                new SousCategorie("Biodiversité", new List<Produit>()
                {
                    new Produit("Prod JarB1", 1.99, "Produit de jardin, biodiversité 1", Produit.CouleurProduit.Gris, Produit.TailleProduit.TresPetit),
                    new Produit("Prod JarB2", 2.99, "Produit de jardin, biodiversité 2", Produit.CouleurProduit.Blanc, Produit.TailleProduit.TresPetit),
                })
            }),
            new Categorie("Balcon & Terrasse", new List<SousCategorie>()
            {
                new SousCategorie("Balcon & Terrasse", new List<Produit>()
                {
                    new Produit("Prod Bal1", 1.99, "Produit de balcon & terrasse 1", Produit.CouleurProduit.Rouge, Produit.TailleProduit.TresPetit),
                    new Produit("Prod Bal2", 2.99, "Produit de balcon & terrasse 2", Produit.CouleurProduit.Bleu, Produit.TailleProduit.Petit),
                }),
            }),
            new Categorie("Maison", new List<SousCategorie>()
            {
                new SousCategorie("Maison", new List<Produit>()
                {
                    new Produit("Prod Mai1", 29.99, "Produit de maison 1", Produit.CouleurProduit.Rose, Produit.TailleProduit.Moyen),
                    new Produit("Prod Mai2", 39.99, "Produit de maison 2", Produit.CouleurProduit.Vert, Produit.TailleProduit.Grand),
                }),
            })
        };

        public static readonly Brush couleurBotanic = new SolidColorBrush(Color.FromRgb(0, 99, 76));

        public Categorie? categorie = null;
        public SousCategorie? sousCategorie = null;

        public Produit.CouleurProduit? filtreCouleur = null;
        public Produit.TailleProduit? filtreTaille = null;

        public MainWindow()
        {
            InitializeComponent();

            int i = 0;
            foreach (Categorie categorie in categories)
            {
                Button btn = new Button();
                if (i == 0)
                    btn.Margin = new Thickness(0, 0, 4, 0);
                else if (i == categories.Count - 1)
                    btn.Margin = new Thickness(4, 0, 0, 0);

                btn.Content = categorie.Nom;
                btn.Click += Categorie_Click;
                btn.Style = (Style)this.FindResource("ButtonsCategories");

                btnsCategories.Children.Add(btn);
                i++;
            }

            foreach (Produit.CouleurProduit couleur in Enum.GetValues(typeof(Produit.CouleurProduit)))
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = couleur.ToString();
                cbCouleur.Items.Add(item);
            }
            foreach (Produit.TailleProduit taille in Enum.GetValues(typeof(Produit.TailleProduit)))
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = taille.ToString();
                cbTaille.Items.Add(item);
            }
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

            string catName = btnCat.Content.ToString() ?? "";
            categorie = categories.Find((c) => c.Nom == catName);

            if (categorie == null)
                throw new Exception("Catégorie non trouvée");

            int i = 0;
            foreach (SousCategorie sousCat in categorie.SousCategories)
            {
                Button btn = new Button();
                if (i == 0)
                    btn.Margin = new Thickness(0, 0, 4, 0);
                else if (i == categorie.SousCategories.Count)
                    btn.Margin = new Thickness(4, 0, 0, 0);

                btn.Content = sousCat.Nom;
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

            string sousCatName = btnSubCat.Content.ToString() ?? "";
            sousCategorie = categorie?.SousCategories.Find((sc) => sc.Nom == sousCatName);

            if (sousCategorie == null)
                throw new Exception("Sous-catégorie non trouvée");

            gridFiltres.Visibility = Visibility.Visible;

            dgProduits.ItemsSource = sousCategorie.Produits;
            dgProduits.Visibility = Visibility.Visible;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            int index = cb.SelectedIndex;

            ComboBoxItem item = (ComboBoxItem)cb.Items[index];
            string? content = item.Content.ToString()!;

            if (cb == cbCouleur)
            {
                if (item == cb.Items[0])
                    filtreCouleur = null;
                else
                    filtreCouleur = (Produit.CouleurProduit)Enum.Parse(typeof(Produit.CouleurProduit), content);
            }
            else if (cb == cbTaille)
            {
                if (item == cb.Items[0])
                    filtreTaille = null;
                else
                    filtreTaille = (Produit.TailleProduit)Enum.Parse(typeof(Produit.TailleProduit), content);
            }

            if (categorie == null || sousCategorie == null)
                return;

            dgProduits.ItemsSource = sousCategorie.Produits.FindAll((p) =>
            {
                if (filtreCouleur != null && p.Couleur != filtreCouleur)
                    return false;
                if (filtreTaille != null && p.Taille != filtreTaille)
                    return false;
                return true;
            }) ?? new List<Produit>();
        }
    }
}
