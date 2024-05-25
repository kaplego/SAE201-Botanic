using System.Collections.Generic;

namespace MaquetteBotanic
{
    public class Categorie
    {
        private string nom;
        private List<SousCategorie> sousCategories;

        public string Nom { get => this.nom; set => this.nom = value; }
        internal List<SousCategorie> SousCategories { get => this.sousCategories; set => this.sousCategories = value; }

        public Categorie(string nom, List<SousCategorie> sousCategories)
        {
            this.Nom = nom;
            this.SousCategories = sousCategories;
        }
        public Categorie(string nom)
            : this(nom, new List<SousCategorie>())
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
            return $"Catégorie {this.Nom} ({this.SousCategories.Count} sous-catégories)";
        }
    }
}
