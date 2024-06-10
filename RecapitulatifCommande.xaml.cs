using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaquetteBotanic
{
    /// <summary>
    /// Logique d'interaction pour RecapitulatifCommande.xaml
    /// </summary>
    public partial class RecapitulatifCommande : Window
    {
        public List<ProduitAchat> lesProduits = new List<ProduitAchat>();

        public RecapitulatifCommande(ObservableCollection<ProduitAchat> produits, bool locale)
        {
            InitializeComponent();
            
            dgProduits.ItemsSource = produits;
            
            foreach (ProduitAchat pa in produits)
            {
                lesProduits.Add(pa);
            }

            int nbProduits = produits.Aggregate(0, (acc, p) => acc + p.Quantite);
            double total = produits.Aggregate(0.0, (acc, p) =>
                acc + (p.Quantite * ApplicationData.Find(ApplicationData.Instance.ProduitsFournits, (pf) =>
                    pf.LeProduit.Id == p.LeProduit.Id)!.PrixAchat)
            );

            tbkTotal.Text = $"Total : {nbProduits} produits pour {total:n2} €";

            foreach (ModeLivraison ml in ApplicationData.Instance.ModeLivraisons)
            {
                if ((locale == false && ml.Id == 3) ||
                    (locale == true && ml.Id != 3))
                    continue;

                ComboBoxItem cbitem = new ComboBoxItem();
                cbitem.Content = ml.Libelle;
                cbitem.Tag = ml;
                cbModesLivraison.Items.Add(cbitem);
            }
        }

        private void btnAcheter_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem? cbitem = cbModesLivraison.SelectedItem as ComboBoxItem;

            if (cbitem == null)
            {
                MessageBox.Show(
                    "Veuillez sélectionner un mode de livraison.",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }

            ModeLivraison ml = cbitem.Tag! as ModeLivraison;

            Commande commande = new Commande(ApplicationData.Instance.MagasinActuel, DateTime.Now, null);
            commande.NumModeLivraison = ml.Id;
            commande.LesProduits = lesProduits;
            commande.Create();
            this.DialogResult = true;
        }
    }
}
