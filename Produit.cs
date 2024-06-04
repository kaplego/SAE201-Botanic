using System.Collections.ObjectModel;
using System.Data;

namespace MaquetteBotanic
{
    public class Produit : ICrud
    {
        private int id;
        private string nom;
        private double prix;
        private string description;
        private string couleur;
        private string taille;
        private int numCategorie;

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public double Prix { get => prix; set => prix = value; }
        public string Description { get => this.description; set => this.description = value; }
        public string Couleur { get => this.couleur; set => this.couleur = value; }
        public string Taille { get => this.taille; set => this.taille = value; }
        public int NumCategorie
        {
            get => this.numCategorie;
            set => this.numCategorie = value;
        }

        public Produit(int id, string nom, double prix, string description, string couleur, string taille)
        {
            this.Id = id;
            this.Nom = nom;
            this.Prix = prix;
            this.Description = description;
            this.Couleur = couleur;
            this.Taille = taille;
        }

        public override string? ToString()
        {
            return this.Nom;
        }

        public static ObservableCollection<Produit> Read()
        {
            ObservableCollection<Produit> lesProduits = new ObservableCollection<Produit>();
            string sql = "SELECT num_produit, num_categorie, nom_produit, couleur_produit, taille_produit, description_produit, prix_vente FROM produit";
            DataTable dt = DataAccess.Instance?.GetData(sql) ?? new DataTable();
            foreach (DataRow res in dt.Rows)
            {
                Produit nouveau = new Produit(
                    int.Parse(res["num_produit"].ToString()!), 
                    res["nom_produit"].ToString()!,
                    double.Parse(res["prix_vente"].ToString()!), 
                    res["description_produit"].ToString()!,
                    res["couleur_produit"].ToString()!,
                    res["taille_produit"].ToString()!
                );
                lesProduits.Add(nouveau);
            }
            return lesProduits;
        }

        public int Create()
        {
            string sql = $"INSERT INTO produit (nom_produit, num_categorie, couleur_produit, taille_produit, description_produit, prix_vente)"
                         + $" values ('{Nom}','{numCategorie}','{Couleur}'"
                         + $",'{Taille}','{Description}', "
                         + $"{Prix}); ";
            return DataAccess.Instance.SetData(sql);
        }

        public int Delete()
        {
            string sql = $"DELETE FROM produit" +
                         $" WHERE num_produit = {Id}";
            return DataAccess.Instance.SetData(sql);
        }

        public int Update()
        {
            string sql = $"UPDATE produit" +
                         $" SET nom_produit='{Nom}', num_categorie={NumCategorie}," +
                         $" couleur_produit='{Couleur}', taille_produit='{Taille}', description_produit='{Description}', prix_vente={Prix}" +
                         $" WHERE num_produit={Id}";
            return DataAccess.Instance.SetData(sql);
        }
    }
}
