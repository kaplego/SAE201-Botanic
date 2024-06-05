using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace MaquetteBotanic
{
    public class Commande: ICrud
    {
        private int id;
        private DateTime dateCommande;
        private DateTime dateLivraison;
        private int numModeLivraison;
        private List<ProduitAchat> lesProduits;
        private int numMagasin;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public DateTime DateCommande
        {
            get
            {
                return dateCommande;
            }

            set
            {
                dateCommande = value;
            }
        }

        public DateTime DateLivraison
        {
            get
            {
                return dateLivraison;
            }

            set
            {
                dateLivraison = value;
            }
        }

        public List<ProduitAchat> LesProduits
        {
            get
            {
                return this.lesProduits;
            }

            set
            {
                this.lesProduits = value;
            }
        }

        public int NumModeLivraison
        {
            get
            {
                return this.NumModeLivraison;
            }

            set
            {
                this.NumModeLivraison = value;
            }
        }

        public int NumMagasin
        {
            get
            {
                return this.numMagasin;
            }

            set
            {
                this.numMagasin = value;
            }
        }

        public Commande(int id, DateTime dateCommande, DateTime dateLivraison)
        {
            this.Id = id;
            this.DateCommande = dateCommande;
            this.DateLivraison = dateLivraison;
        }

        public Commande(int id, DateTime dateCommande, DateTime dateLivraison, List<ProduitAchat> lesProduits, int NumModeLivraison)
        {
            this.Id = id;
            this.DateCommande = dateCommande;
            this.DateLivraison = dateLivraison;
            this.LesProduits = lesProduits;
            this.NumModeLivraison = NumModeLivraison;
        }

        public override bool Equals(object? obj)
        {
            return obj is Commande commande &&
                   this.Id == commande.Id &&
                   this.DateCommande == commande.DateCommande &&
                   this.DateLivraison == commande.DateLivraison &&
                   EqualityComparer<List<ProduitAchat>>.Default.Equals(this.LesProduits, commande.LesProduits) &&
                   this.NumModeLivraison == commande.NumModeLivraison;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.DateCommande, this.DateLivraison, this.LesProduits, this.NumModeLivraison);
        }

        public override string? ToString()
        {
            return $"ID : {Id} \nDate Commande : {DateCommande} \nDate de Livraison : {DateLivraison}";
        }

        public static ObservableCollection<Commande> Read()
        {
            ObservableCollection<Commande> lesCommandes = new ObservableCollection<Commande>();
            string sql = "SELECT num_commande, date_commande, date_livraison FROM Commande";
            DataTable dt = DataAccess.Instance?.GetData(sql) ?? new DataTable();
            foreach (DataRow res in dt.Rows)
            {
                Commande nouveau = new Commande(
                    int.Parse(res["num_commande"].ToString()!),
                    DateTime.Parse(res["date_commande"].ToString()!),
                    DateTime.Parse(res["date_livraison"].ToString()!)
                );
                lesCommandes.Add(nouveau);
            }
            return lesCommandes;
        }

        public int Create()
        {
            string sql = $"INSERT INTO produit (num_magasin, date_commande, date_livraison)"
                         + $" values ('{NumMagasin}','{DateCommande}','{DateLivraison}'";
            return DataAccess.Instance.SetData(sql);
        }

        public int Delete()
        {
            string sql = $"DELETE FROM commande" +
                         $" WHERE num_commande = {Id}";
            return DataAccess.Instance.SetData(sql);
        }

        public int Update()
        {
            string sql = $"UPDATE commande" +
                         $" SET date_commande={DateCommande}," +
                         $" date_livraison='{DateLivraison}'" +
                         $" WHERE num_commande={Id}";
            return DataAccess.Instance.SetData(sql);
        }
    }
}
