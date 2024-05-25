using System.Collections.Generic;

namespace MaquetteBotanic
{
    public class SousCategorie
    {
        private string nom;
        private List<Produit> produits;

        public string Nom { get => nom; set => nom = value; }
        internal List<Produit> Produits { get => this.produits; set => this.produits = value; }

        public SousCategorie(string nom, List<Produit> produits)
        {
            this.Nom = nom;
            this.Produits = produits;
        }
        public SousCategorie(string nom)
            : this(nom, new List<Produit>())
        { }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return $"Sous-catégorie {this.Nom} ({this.Produits.Count} produits)";
        }
    }
}
