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
        private DateTime? dateLivraison;
        private int numModeLivraison;
        private ObservableCollection<DetailCommande> lesProduits;
        private Magasin magasin;

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

        public DateTime? DateLivraison
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

        public ObservableCollection<DetailCommande> LesProduits
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
                return this.numModeLivraison;
            }

            set
            {
                this.numModeLivraison = value;
            }
        }

        public Magasin Magasin
        {
            get
            {
                return this.magasin;
            }

            set
            {
                this.magasin = value;
            }
        }

        public Commande(Magasin magasin, DateTime dateCommande, DateTime? dateLivraison)
        {
            this.Magasin = magasin;
            this.DateCommande = dateCommande;
            this.DateLivraison = dateLivraison;
        }

        public Commande(int id, Magasin magasin, DateTime dateCommande, DateTime? dateLivraison)
            : this(magasin, dateCommande, dateLivraison)
        {
            this.Id = id;
        }

        public Commande(int id, Magasin magasin, DateTime dateCommande, DateTime? dateLivraison, int NumModeLivraison, ObservableCollection<DetailCommande> produits)
            : this(id, magasin, dateCommande, dateLivraison)
        {
            this.NumModeLivraison = NumModeLivraison;
            this.LesProduits = produits;
        }

        public override bool Equals(object? obj)
        {
            return obj is Commande commande &&
                   this.Id == commande.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.DateCommande, this.DateLivraison, this.NumModeLivraison);
        }

        public override string? ToString()
        {
            return $"ID : {Id} \nDate Commande : {DateCommande} \nDate de Livraison : {DateLivraison}";
        }

        public static ObservableCollection<Commande> Read(Magasin magasin, ObservableCollection<DetailCommande> detailsCommandes)
        {
            ObservableCollection<Commande> lesCommandes = new ObservableCollection<Commande>();
            string sql = $"SELECT num_commande, date_commande, date_livraison, num_mode_livraison " +
                $"FROM commande_achat " +
                $"WHERE num_magasin = {magasin.Id}";
            DataTable dt = DataAccess.Instance?.GetData(sql) ?? new DataTable();

            foreach (DataRow res in dt.Rows)
            {
                Commande nouveau = new Commande(
                    int.Parse(res["num_commande"].ToString()!),
                    magasin,
                    DateTime.Parse(res["date_commande"].ToString()!),
                    string.IsNullOrEmpty(res["date_livraison"].ToString()) ? null : DateTime.Parse(res["date_livraison"].ToString()!),
                    int.Parse(res["num_mode_livraison"].ToString()!),
                    ApplicationData.Filter(detailsCommandes, (dc) => dc.NumCommande == int.Parse(res["num_commande"].ToString()!))
                );
                lesCommandes.Add(nouveau);
            }
            return lesCommandes;
        }

        public double PrixTotal()
        {
            return this.LesProduits.Aggregate(0.0, (acc, p) => acc + p.PrixTotal);
        }

        public int Create()
        {
            string sql = $"INSERT INTO commande_achat (num_magasin, date_commande, date_livraison, num_mode_livraison) " +
                         $"VALUES ('{this.Magasin}','{this.DateCommande}',{(DateLivraison != null ? $"'{DateLivraison}'" : "NULL")},{this.NumModeLivraison}) RETURNING num_commande;";
            DataTable dt = DataAccess.Instance.GetData(sql);
            DataRow? dr = dt.Rows[0] ?? null;

            if (dr == null)
                return -1;

            int commande_id = int.Parse(dr["num_commande"].ToString()!);

            foreach (DetailCommande detail in this.LesProduits)
            {
                Fournit? fournit = ApplicationData.Find(ApplicationData.Instance.ProduitsFournits, (pf) => pf.LeProduit.Id == detail.LeProduitAchat.LeProduit.Id);

                if (fournit == null)
                    continue;

                sql = "INSERT INTO detail_commande (num_commande, num_produit, num_fournisseur, quantite_commandee) " +
                    $"VALUES ({commande_id}, {detail.LeProduitAchat.LeProduit.Id}, {fournit.LeFournisseur.Id}, {detail.LeProduitAchat.Quantite});";
                DataAccess.Instance.SetData(sql);
            }
            return 0;
        }

        public int Delete()
        {
            string sql = $"DELETE FROM commande_achat" +
                         $" WHERE num_commande = {Id}";
            return DataAccess.Instance.SetData(sql);
        }

        public int Update()
        {
            string sql = $"UPDATE commande_achat" +
                         $" SET date_livraison={(DateLivraison != null ? $"'{DateLivraison?.Month}-{DateLivraison?.Day}-{DateLivraison?.Year}'" : "NULL")}" +
                         $" WHERE num_commande={this.Id}";
            return DataAccess.Instance.SetData(sql);
        }
    }
}
