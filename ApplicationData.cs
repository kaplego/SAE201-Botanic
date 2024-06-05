using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Automation.Peers;
using static MaquetteBotanic.Produit;

namespace MaquetteBotanic
{
    public class ApplicationData
    {
        private static ApplicationData instance;
        private ObservableCollection<TypeProduit> typesProduit;
        private ObservableCollection<Categorie> categories;
        private ObservableCollection<Commande> commandes;
        private ObservableCollection<ProduitAchat> produits;


        public static ApplicationData Instance {
            get {
                instance ??= new ApplicationData();
                return instance;
            }
        }

        public ObservableCollection<TypeProduit> TypesProduit { get => this.typesProduit; set => this.typesProduit = value; }
        public ObservableCollection<Categorie> Categories { get => categories; set => categories = value; }
        public ObservableCollection<ProduitAchat> Produits { get => this.produits; set => this.produits = value; }

        private ApplicationData()
        {
            TypesProduit = TypeProduit.Read();
            Categories = Categorie.Read();
            Produits = ProduitAchat.FromListProduit(Produit.Read());
        }

        public delegate bool CallBack<T>(T item);


        public ObservableCollection<Commande> Commandes
        {
            get
            {
                return this.commandes;
            }

            set
            {
                this.commandes = value;
            }
        }

        public ApplicationData()
        {
            this.Produits = ProduitAchat.FromListProduit(Produit.Read());
            this.Categories = Categorie.Read();
            this.TypesProduits = TypeProduit.Read();
            this.Commandes = Commande.Read();

        public static T? Find<T>(ObservableCollection<T> from, CallBack<T> callback)
        {
            T? result = default;
            foreach (T item in from)
            {
                if (callback.Invoke(item))
                {
                    result = item;
                }
            }
            return result;

        }
    }
}
