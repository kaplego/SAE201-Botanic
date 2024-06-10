using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MaquetteBotanic
{
    public class ProduitAchat
    {
        private Produit leProduit;
        private int quantite = 0;

        public Produit LeProduit
        {
            get => this.leProduit;
            set => this.leProduit = value;
        }
        public int Quantite { get => this.quantite; set => this.quantite = value; }

        private ProduitAchat(Produit produit)
        {
            this.leProduit = produit;
        }

        public ProduitAchat(Produit produit, int quantite)
            : this(produit)
        {
            this.Quantite = quantite;
        }

        public static ObservableCollection<ProduitAchat> FromListProduit(ObservableCollection<Produit> liste)
        {
            ObservableCollection<ProduitAchat> listeAchat = new ObservableCollection<ProduitAchat>();
            foreach (Produit produit in liste)
            {
                listeAchat.Add(new ProduitAchat(produit));
            }
            return listeAchat;
        }

        public double PrixTotal()
        {
            return this.Quantite * this.LeProduit.Prix;
        }

        public override bool Equals(object? obj)
        {
            return obj is ProduitAchat achat &&
                   EqualityComparer<Produit>.Default.Equals(this.LeProduit, achat.LeProduit);
        }
    }
}
