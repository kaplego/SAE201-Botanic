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
        private Magasin leMagasin;
        private Produit leProduit;
        private double prixAchat;

        public Fournisseur LeFournisseur { get => leFournisseur; set => leFournisseur = value; }
        public Magasin LeMagasin { get => leMagasin; set => leMagasin = value; }
        public Produit LeProduit { get => leProduit; set => leProduit = value; }
        public double PrixAchat { get => this.prixAchat; set => this.prixAchat = value; }

        public Fournit(Fournisseur leFournisseur, Magasin leMagasin, Produit leProduit, double prixAchat)
        {
            this.LeFournisseur = leFournisseur;
            this.LeMagasin = leMagasin;
            this.LeProduit = leProduit;
            this.PrixAchat = prixAchat;
        }

        public static ObservableCollection<Fournit> Read(
            ObservableCollection<Magasin> magasins,
            ObservableCollection<Fournisseur> fournisseurs,
            ObservableCollection<Produit> produits
        )
        {
            ObservableCollection<Fournit> produitsFournis = new ObservableCollection<Fournit>();
            string sql = "SELECT num_fournisseur, num_produit, num_magasin, prix_achat FROM fournit";
            DataTable dt = DataAccess.Instance?.GetData(sql) ?? new DataTable();
            foreach (DataRow res in dt.Rows)
            {
                Fournisseur? fourn = ApplicationData.Find(fournisseurs, (i) => i.Id == int.Parse(res["num_fournisseur"].ToString()!));
                if (fourn == null)
                    continue;

                Magasin? mag = ApplicationData.Find(magasins, (i) => i.Id == int.Parse(res["num_magasin"].ToString()!));
                if (mag == null)
                    continue;

                Produit? prod = ApplicationData.Find(produits, (i) => i.Id == int.Parse(res["num_produit"].ToString()!));
                if (prod == null)
                    continue;

                Fournit nouveau = new Fournit(
                    fourn,
                    mag,
                    prod,
                    double.Parse(res["prix_achat"].ToString()!)
                );
                produitsFournis.Add(nouveau);
            }
            return produitsFournis;
        }

        public static ObservableCollection<Fournit> Read(
            ObservableCollection<Magasin> magasins,
            ObservableCollection<Fournisseur> fournisseurs,
            ObservableCollection<ProduitAchat> produitsAchat
        )
        {
            ObservableCollection<Produit> produits = new ObservableCollection<Produit>();

            foreach (ProduitAchat prod in produitsAchat)
                produits.Add(prod.LeProduit);

            return Read(magasins, fournisseurs, produits);
        }

        public int Create()
        {
            string sql = $"INSERT INTO fournit (num_fournisseur, num_produit, num_magasin, prix_achat)"
                         + $" values ('{LeFournisseur.Id}','{LeProduit.Id}','{LeMagasin.Id}'"
                         + $",'{PrixAchat}'); ";
            return DataAccess.Instance.SetData(sql);
        }

        public int Delete()
        {
            string sql = $"DELETE FROM fournit" +
                         $" WHERE num_fournisseur = {LeFournisseur.Id} AND num_produit = {LeProduit.Id} AND num_magasin = {LeMagasin.Id}";
            return DataAccess.Instance.SetData(sql);
        }

        public int Update()
        {
            string sql = $"UPDATE fournit" +
                         $" SET prix_achat={PrixAchat}" +
                         $" WHERE num_fournisseur = {LeFournisseur.Id} AND num_produit = {LeProduit.Id} AND num_magasin = {LeMagasin.Id}";
            return DataAccess.Instance.SetData(sql);
        }
    }
}
