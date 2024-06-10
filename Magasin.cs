using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquetteBotanic
{
    public class Magasin
    {
        private int id;
        private string nom;
        private string adresseRue;
        private string adresseCP;
        private string adresseVille;
        private string horaires;

        private List<ProduitAchat> stock;
        private List<Fournit> produitsFournits;

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string AdresseRue { get => adresseRue; set => adresseRue = value; }
        public string AdresseCP { get => adresseCP; set => adresseCP = value; }
        public string AdresseVille { get => adresseVille; set => adresseVille = value; }
        public string Horaires { get => this.horaires; set => this.horaires = value; }
        public List<ProduitAchat> Stock { get => stock; set => stock = value; }
        public List<Fournit> ProduitsFournits { get => this.produitsFournits; set => this.produitsFournits = value; }

        public Magasin(int id, string nom, string adresse_rue, string adresse_cp, string adresse_ville, string horaires)
        {
            this.Id = id;
            this.Nom = nom;
            this.AdresseRue = adresse_rue;
            this.AdresseCP = adresse_cp;
            this.AdresseVille = adresse_ville;
            this.Horaires = horaires;
        }

        public static ObservableCollection<Magasin> Read()
        {
            ObservableCollection<Magasin> lesMagasins = new ObservableCollection<Magasin>();
            string sql = "SELECT num_magasin, nom_magasin, adresse_rue_magasin, adresse_cp_magasin, adresse_ville_magasin, horaire FROM magasin";
            DataTable dt = DataAccess.Instance?.GetData(sql) ?? new DataTable();
            foreach (DataRow res in dt.Rows)
            {
                Magasin nouveau = new Magasin(
                    int.Parse(res["num_magasin"].ToString()!),
                    res["nom_magasin"].ToString()!,
                    res["adresse_rue_magasin"].ToString()!,
                    res["adresse_cp_magasin"].ToString()!,
                    res["adresse_ville_magasin"].ToString()!,
                    res["horaire"].ToString()!
                );
                lesMagasins.Add(nouveau);
            }
            return lesMagasins;
        }

        public static Magasin? Current(Utilisateur utilisateur)
        {
            string sql = "SELECT num_magasin, nom_magasin, adresse_rue_magasin, adresse_cp_magasin, adresse_ville_magasin, horaire " +
                "FROM magasin " +
                $"WHERE num_magasin = {utilisateur.NumMagasin}";
            DataTable dt = DataAccess.Instance?.GetData(sql) ?? new DataTable();

            if (dt.Rows.Count == 0)
                return null;

            DataRow res = dt.Rows[0];

            return new Magasin(
                int.Parse(res["num_magasin"].ToString()!),
                res["nom_magasin"].ToString()!,
                res["adresse_rue_magasin"].ToString()!,
                res["adresse_cp_magasin"].ToString()!,
                res["adresse_ville_magasin"].ToString()!,
                res["horaire"].ToString()!
            );
        }

        public int Create()
        {
            string sql = $"INSERT INTO magasin (nom_magasin, adresse_rue_magasin, adresse_cp_magasin, adresse_ville_magasin, horaire)"
                         + $" values ('{Nom}','{AdresseRue}','{AdresseCP}'"
                         + $",'{AdresseVille}','{Horaires}'); ";
            return DataAccess.Instance.SetData(sql);
        }

        public int Delete()
        {
            string sql = $"DELETE FROM magasin" +
                         $" WHERE num_magasin = {Id}";
            return DataAccess.Instance.SetData(sql);
        }

        public int Update()
        {
            string sql = $"UPDATE magasin" +
                         $" SET nom_magasin='{Nom}', adresse_rue_magasin='{AdresseRue}', adresse_cp_magasin='{AdresseCP}'," +
                         $" adresse_ville_magasin='{AdresseVille}', horaire='{Horaires}'" +
                         $" WHERE num_magasin = {Id}";
            return DataAccess.Instance.SetData(sql);
        }
    }
}
