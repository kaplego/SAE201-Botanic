using System.Collections.Generic;
using System.Collections.ObjectModel;
using static MaquetteBotanic.Produit;

namespace MaquetteBotanic
{
    public class ApplicationData
    {
        private ObservableCollection<ProduitAchat> produits;
        private ObservableCollection<Categorie> categories;
        private ObservableCollection<TypeProduit> typesProduits;
        private ObservableCollection<Commande> commandes;

        public ObservableCollection<ProduitAchat> Produits { get => this.produits; set => this.produits = value; }
        public ObservableCollection<Categorie> Categories { get => categories; set => categories = value; }
        public ObservableCollection<TypeProduit> TypesProduits { get => typesProduits; set => typesProduits = value; }

        public ApplicationData()
        {
            this.Produits = ProduitAchat.FromListProduit(Produit.Read());
            this.Categories = Categorie.Read();
            this.TypesProduits = TypeProduit.Read();
            this.commandes = Commande.Read();
        }
    }
}
