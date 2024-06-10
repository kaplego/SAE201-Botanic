using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public static ObservableCollection<ProduitAchat>? dgItemsCentrale = null;
        public static ObservableCollection<ProduitAchat> produitsCentrale = new ObservableCollection<ProduitAchat>();
        public static string? filtreCouleur = null;
        public static string? filtreTaille = null;

        public static ObservableCollection<ProduitAchat>? dgItemsLocaux = null;
        public static ObservableCollection<ProduitAchat> produitsLocaux = new ObservableCollection<ProduitAchat>();
        public static string? filtreNom = null;
        public static string? filtreFournisseur = null;

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

            dgItemsLocaux = ProduitAchat.FromListProduit(ApplicationData.Filter(ApplicationData.Instance.Produits, (p) =>
            {
                Fournit? fournit = ApplicationData.Find(ApplicationData.Instance.ProduitsFournits, (pf) => pf.LeProduit.Id == p.Id);
                if (fournit == null)
                    return false;
                return fournit.LeFournisseur.Local == true;
            }));

            dgCommandes.ItemsSource = ApplicationData.Instance.Commandes;
        }
        private void Recapitulatif(ObservableCollection<ProduitAchat> produits, bool local)
        {
            if (produits == null || produits.Count == 0)
            {
                MessageBox.Show(
                    "Vous devez sélectionner des produits.",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }

            RecapitulatifCommande recap = new RecapitulatifCommande(produits, false);
            if (recap.ShowDialog() == true)
            {
                MessageBox.Show(
                    "La commande a été créée avec succès.",
                    "Succès",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
                ApplicationData.Instance.Commandes = Commande.Read(ApplicationData.Instance.MagasinActuel, ApplicationData.Instance.DetailsCommandes);
            }
        }

        /* ========================================== */
        /* ===============  Centrale  =============== */
        /* ========================================== */
        #region Centrale

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
                        dgItemsCentrale = ProduitAchat.FromListProduit(ApplicationData.Filter(ApplicationData.Instance.Produits, (p) =>
                        {
                            return
                                p.LaCategorie.Id == idCategorie &&
                                ApplicationData.Find(ApplicationData.Instance.ProduitsFournits, (pf) =>
                                    pf.LeProduit.Id == p.Id)?.LeFournisseur.Local == false;
                        }));
                        dgProduits.ItemsSource = dgItemsCentrale;

                        cbCouleur.Items.Clear();
                        cbTaille.Items.Clear();

                        cbCouleur.Items.Add("Aucun filtre de couleur");
                        cbTaille.Items.Add("Aucun filtre de taille");

                        cbCouleur.SelectedIndex = 0;
                        cbTaille.SelectedIndex = 0;

                        List<string> couleurs = new List<string>();
                        List<string> tailles = new List<string>();
                        foreach (ProduitAchat produit in dgProduits.ItemsSource)
                        {
                            // Initialiser les filtres
                            if (!couleurs.Contains(produit.LeProduit.Couleur))
                            {
                                ComboBoxItem item = new ComboBoxItem();
                                item.Content = produit.LeProduit.Couleur;
                                cbCouleur.Items.Add(item);
                            }
                            if (!tailles.Contains(produit.LeProduit.Taille))
                            {
                                ComboBoxItem item = new ComboBoxItem();
                                item.Content = produit.LeProduit.Taille;
                                cbTaille.Items.Add(item);
                            }

                            // Initialiser la quantité
                            if (produitsCentrale.Contains(produit))
                            {
                                produit.Quantite = ApplicationData.Find(produitsCentrale , (p) => p.Equals(produit))!.Quantite;
                            }
                        }

                        gridFiltres.Visibility = Visibility.Visible;
                        dgProduits.Visibility = Visibility.Visible;
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

        private void dgProduits_CurrentCellChanged(object sender, System.EventArgs e)
        {
            foreach (ProduitAchat produit in dgProduits.Items)
            {
                if (produit.Quantite > 0 && !produitsCentrale.Contains(produit))
                {
                    produitsCentrale.Add(produit);
                }
                else if (produit.Quantite <= 0 && produitsCentrale.Contains(produit))
                    produitsCentrale.Remove(produit);
            }

            btnAcheter.IsEnabled = produitsCentrale.Count > 0;
        }

        private void cbCouleur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox cb)
            {
                if (cb.SelectedIndex == 0 || cb.SelectedItem == null)
                    filtreCouleur = null;
                else
                    filtreCouleur = ((ComboBoxItem)cb.SelectedItem).Content.ToString();
            }
            FiltrerCentrale();
        }

        private void cbTaille_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox cb)
            {
                if (cb.SelectedIndex == 0 || cb.SelectedItem == null)
                    filtreTaille = null;
                else
                    filtreTaille = ((ComboBoxItem)cb.SelectedItem).Content.ToString();
            }
            FiltrerCentrale();
        }

        private void FiltrerCentrale()
        {
            if (dgItemsCentrale == null)
                return;

            if (filtreCouleur == null && filtreTaille == null)
                dgProduits.ItemsSource = dgItemsCentrale;

            dgProduits.ItemsSource = ApplicationData.Filter(dgItemsCentrale, (i) =>
                (filtreCouleur == null || i.LeProduit.Couleur == filtreCouleur) &&
                (filtreTaille == null || i.LeProduit.Taille == filtreTaille));
        }

        private void btnAcheter_Click(object sender, RoutedEventArgs e)
        {
            Recapitulatif(produitsCentrale, false);
        }

        #endregion

        /* ========================================== */
        /* ===============    Local   =============== */
        /* ========================================== */
        #region Local

        private void nomProduit_TextChanged(object sender, TextChangedEventArgs e)
        {
            filtreNom = nomProduit.Text;
            FiltrerLocal();
        }

        private void nomFournisseur_TextChanged(object sender, TextChangedEventArgs e)
        {
            filtreFournisseur = nomFournisseur.Text;
            FiltrerLocal();
        }

        private void FiltrerLocal()
        {
            if (dgItemsLocaux == null)
                return;

            if (string.IsNullOrEmpty(filtreNom) && string.IsNullOrEmpty(filtreFournisseur))
            {
                dgProduitsLocaux.ItemsSource = new ObservableCollection<ProduitAchat>();
                return;
            }

            dgProduitsLocaux.ItemsSource = ApplicationData.Filter(dgItemsLocaux, (produit) => {
                Fournit? fournit = ApplicationData.Find(ApplicationData.Instance.ProduitsFournits, (pf) => pf.LeProduit.Id == produit.LeProduit.Id);

                return (string.IsNullOrEmpty(filtreNom) || produit.LeProduit.Nom.ToLower().Contains(filtreNom.ToLower())) &&
                       (string.IsNullOrEmpty(filtreFournisseur) || (fournit?.LeFournisseur.Nom.ToLower().Contains(filtreFournisseur.ToLower()) ?? false));
            });
        }

        private void dgProduitsLocaux_CurrentCellChanged(object sender, EventArgs e)
        {
            foreach (ProduitAchat produit in dgProduitsLocaux.Items)
            {
                if (produit.Quantite > 0 && !produitsLocaux.Contains(produit))
                {
                    produitsLocaux.Add(produit);
                }
                else if (produit.Quantite <= 0 && produitsLocaux.Contains(produit))
                    produitsLocaux.Remove(produit);
            }

            btnAcheterLocal.IsEnabled = produitsLocaux.Count > 0;
        }

        private void btnAcheterLocal_Click(object sender, RoutedEventArgs e)
        {
            Recapitulatif(produitsLocaux, true);
        }

        #endregion

        /* ========================================== */
        /* =========  Visualiser commandes  ========= */
        /* ========================================== */
        #region Visualiser commmandes

        private void btnModifierCommande_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show(
                "Voulez-vous enregister les modifications ?",
                "Enregistrement",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (res == MessageBoxResult.Yes)
            {
                ((Commande)dgCommandes.SelectedItem).DateLivraison = dpDateLivraison.SelectedDate;
                ((Commande)dgCommandes.SelectedItem).Update();
            }
        }

        private void dgCommandes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Commande cmd = ((Commande)dgCommandes.SelectedItem);

            dgProduitsCommande.ItemsSource = cmd.LesProduits;
            tbkTotalCommande.Text = $"{cmd.LesProduits.Count} produit(s)  |  {cmd.PrixTotal()} €";
        }

        #endregion
    }
}
