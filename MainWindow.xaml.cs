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
        }

        private void AjouterProduit(int id, Produit produit)
        {
            RowDefinition rowDef = new RowDefinition();

            Separator sep = new Separator();
            Grid.SetColumnSpan(sep, 4);
            Grid.SetRow(sep, id - 1);
            sep.Style = (Style)this.FindResource("SepProduits");

            TextBox tbxQte = new TextBox();
            tbxQte.Name = $"qteProduit_{id}";
            Grid.SetRow(tbxQte, id);
            tbxQte.Style = (Style)this.FindResource("TBxQteProduit");
            tbxQte.Text = "0";

            TextBlock tbkNom = new TextBlock();
            tbkNom.Text = produit.Nom;
            Grid.SetRow(tbkNom, id);
            Grid.SetColumn(tbkNom, 1);
            tbkNom.Style = (Style)this.FindResource("TBkProduit");

            TextBlock tbkPrix = new TextBlock();
            tbkPrix.Text = $"{produit.Prix:n2} €";
            Grid.SetRow(tbkPrix, id);
            Grid.SetColumn(tbkPrix, 2);
            tbkPrix.Style = (Style)this.FindResource("TBkPrixProduit");

            TextBlock tbkDesc = new TextBlock();
            tbkDesc.Text = produit.Description;
            Grid.SetRow(tbkDesc, id);
            Grid.SetColumn(tbkDesc, 3);
            tbkDesc.Style = (Style)this.FindResource("TBkProduit");

            gridProduits.Children.Add(sep);
            gridProduits.RowDefinitions.Add(rowDef);
            gridProduits.Children.Add(tbxQte);
            gridProduits.Children.Add(tbkNom);
            gridProduits.Children.Add(tbkPrix);
            gridProduits.Children.Add(tbkDesc);

            gridProduits.Height += 30;
        }

        private void AjouterProduit(Produit produit)
            => AjouterProduit(gridProduits.RowDefinitions.Count, produit);

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

            gridProduits.Children.RemoveRange(4, gridProduits.Children.Count - 4);
            gridProduits.RowDefinitions.RemoveRange(1, gridProduits.RowDefinitions.Count - 1);
            gridProduits.Height = 30;

            foreach (Produit produit in sousCategorie.Produits)
            {
                AjouterProduit(produit);
            }

            gridProduits.Visibility = Visibility.Visible;
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
    }
}
