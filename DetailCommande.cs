using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace MaquetteBotanic
{
    public class DetailCommande
    {
        private int numCommande;
        private int numFournisseur;
        private ProduitAchat leProduitAchat;

        public int NumCommande { get => numCommande; set => numCommande = value; }
        public int NumFournisseur { get => numFournisseur; set => numFournisseur = value; }
        public ProduitAchat LeProduitAchat { get => this.leProduitAchat; set => this.leProduitAchat = value; }
        public double PrixUnitaire
        {
            get
            {
                Fournit fournit = ApplicationData.Find(ApplicationData.Instance.ProduitsFournits, (pf) => pf.LeProduit.Id == this.LeProduitAchat.LeProduit.Id);
                return fournit.PrixAchat;
            }
        }
        public double PrixTotal
        {
            get
            {
                return this.PrixUnitaire * this.LeProduitAchat.Quantite;
            }
        }

        public DetailCommande(int num_commande, int num_fournisseur, ProduitAchat produit)
        {
            this.NumCommande = num_commande;
            this.NumFournisseur = num_fournisseur;
            this.LeProduitAchat = produit;
        }

        public override bool Equals(object? obj)
        {
            return obj is DetailCommande commande &&
                   this.NumCommande == commande.NumCommande &&
                   this.NumFournisseur == commande.NumFournisseur &&
                   EqualityComparer<ProduitAchat>.Default.Equals(this.LeProduitAchat, commande.LeProduitAchat);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.NumCommande, this.NumFournisseur, this.LeProduitAchat);
        }

        public static ObservableCollection<DetailCommande> Read(ObservableCollection<Produit> produits)
        {
            ObservableCollection<DetailCommande> lesDetails = new ObservableCollection<DetailCommande>();
            string sql = $"SELECT num_commande, num_fournisseur, num_produit, quantite_commandee " +
                $"FROM detail_commande";
            DataTable dt = DataAccess.Instance?.GetData(sql) ?? new DataTable();

            foreach (DataRow res in dt.Rows)
            {
                DetailCommande nouveau = new DetailCommande(
                    int.Parse(res["num_commande"].ToString()!),
                    int.Parse(res["num_fournisseur"].ToString()!),
                    new ProduitAchat(ApplicationData.Find(produits, (p) => p.Id == int.Parse(res["num_produit"].ToString()!)), int.Parse(res["quantite_commandee"].ToString()!))
                );
                lesDetails.Add(nouveau);
            }
            return lesDetails;
        }
    }
}
