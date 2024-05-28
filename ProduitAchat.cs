using System.Collections.Generic;

namespace MaquetteBotanic
{
    public class ProduitAchat : Produit
    {
        private int quantite;

        public int Quantite { get => this.quantite; set => this.quantite = value; }

        public ProduitAchat(string nom, double prix, string description, CouleurProduit couleur, TailleProduit taille)
            : base(nom, prix, description, couleur, taille)
        {
            this.Quantite = 0;
        }

        public ProduitAchat(Produit produit)
            : this(produit.Nom, produit.Prix, produit.Description, produit.Couleur, produit.Taille)
        { }

        public static List<ProduitAchat> FromListProduit(List<Produit> liste)
        {
            List<ProduitAchat> listeAchat = new List<ProduitAchat>();
            foreach (Produit produit in liste)
            {
                listeAchat.Add(new ProduitAchat(produit));
            }
            return listeAchat;
        }

        public double PrixTotal()
        {
            return this.Quantite * this.Prix;
        }
    }
}
