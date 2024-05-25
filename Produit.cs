namespace MaquetteBotanic
{
    public class Produit
    {
        public enum CouleurProduit
        {
            Rouge,
            Vert,
            Bleu,
            Jaune,
            Blanc,
            Noir,
            Marron,
            Gris,
            Rose,
            Violet,
            Orange,
            Autre
        }
        public enum TailleProduit
        {
            TresPetit,
            Petit,
            Moyen,
            Grand,
            TresGrand
        }

        private string nom;
        private double prix;
        private string description;
        private CouleurProduit couleur;
        private TailleProduit taille;

        public string Nom { get => nom; set => nom = value; }
        public double Prix { get => prix; set => prix = value; }
        public string Description { get => this.description; set => this.description = value; }
        public CouleurProduit Couleur { get => couleur; set => couleur = value; }
        public TailleProduit Taille { get => this.taille; set => this.taille = value; }

        public Produit(string nom, double prix, string description, CouleurProduit couleur, TailleProduit taille)
        {
            this.Nom = nom;
            this.Prix = prix;
            this.Description = description;
            this.Couleur = couleur;
            this.Taille = taille;
        }

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
            return this.Nom;
        }
    }
}
