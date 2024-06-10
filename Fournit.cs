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
    public class Fournit : ICrud
    {
        private Fournisseur leFournisseur;
        private Produit leProduit;
        private double prixAchat;

        public Fournisseur LeFournisseur { get => leFournisseur; set => leFournisseur = value; }
        public Produit LeProduit { get => leProduit; set => leProduit = value; }
        public double PrixAchat { get => this.prixAchat; set => this.prixAchat = value; }

        public Fournit(Fournisseur leFournisseur, Produit leProduit, double prixAchat)
        {
            this.LeFournisseur = leFournisseur;
            this.LeProduit = leProduit;
            this.PrixAchat = prixAchat;
        }

        public static ObservableCollection<Fournit> Read(
            ObservableCollection<Fournisseur> fournisseurs,
            ObservableCollection<Produit> produits
        )
        {
            ObservableCollection<Fournit> produitsFournis = new ObservableCollection<Fournit>();
            string sql = "SELECT num_fournisseur, num_produit, prix_achat FROM fournit";
            DataTable dt = DataAccess.Instance?.GetData(sql) ?? new DataTable();
            foreach (DataRow res in dt.Rows)
            {
                Fournisseur? fourn = ApplicationData.Find(fournisseurs, (i) => i.Id == int.Parse(res["num_fournisseur"].ToString()!));
                if (fourn == null)
                    continue;

                Produit? prod = ApplicationData.Find(produits, (i) => i.Id == int.Parse(res["num_produit"].ToString()!));
                if (prod == null)
                    continue;

                Fournit nouveau = new Fournit(
                    fourn,
                    prod,
                    double.Parse(res["prix_achat"].ToString()!)
                );
                produitsFournis.Add(nouveau);
            }
            return produitsFournis;
        }

        public static ObservableCollection<Fournit> Read(
            ObservableCollection<Fournisseur> fournisseurs,
            ObservableCollection<ProduitAchat> produitsAchat
        )
        {
            ObservableCollection<Produit> produits = new ObservableCollection<Produit>();

            foreach (ProduitAchat prod in produitsAchat)
                produits.Add(prod.LeProduit);

            return Read(fournisseurs, produits);
        }

        public int Create()
        {
            string sql = $"INSERT INTO fournit (num_fournisseur, num_produit, prix_achat)"
                         + $" values ('{LeFournisseur.Id}','{LeProduit.Id}','{PrixAchat}'"
                         + $",'{PrixAchat}'); ";
            return DataAccess.Instance.SetData(sql);
        }

        public int Delete()
        {
            string sql = $"DELETE FROM fournit" +
                         $" WHERE num_fournisseur = {LeFournisseur.Id} AND num_produit = {LeProduit.Id}";
            return DataAccess.Instance.SetData(sql);
        }

        public int Update()
        {
            string sql = $"UPDATE fournit" +
                         $" SET prix_achat={PrixAchat}" +
                         $" WHERE num_fournisseur = {LeFournisseur.Id} AND num_produit = {LeProduit.Id}";
            return DataAccess.Instance.SetData(sql);
        }
    }
}
