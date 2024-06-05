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
        private ObservableCollection<ProduitAchat> produits;
        private ObservableCollection<Commande> commandes;
        private ObservableCollection<Fournisseur> fournisseurs;
        private ObservableCollection<Utilisateur> utilisateurs;
        private ObservableCollection<ModeLivraison> modeLivraisons;

        public static ApplicationData Instance {
            get {
                instance ??= new ApplicationData();
                return instance;
            }
        }

        public ObservableCollection<TypeProduit> TypesProduit { get => this.typesProduit; set => this.typesProduit = value; }
        public ObservableCollection<Categorie> Categories { get => categories; set => categories = value; }
        public ObservableCollection<ProduitAchat> Produits { get => this.produits; set => this.produits = value; }
        public ObservableCollection<Commande> Commandes { get => this.commandes; set => this.commandes = value; }
        public ObservableCollection<Fournisseur> Fournisseurs { get => this.fournisseurs; set => this.fournisseurs = value; }
        public ObservableCollection<Utilisateur> Utilisateurs { get => this.utilisateurs; set => this.utilisateurs = value; }
        public ObservableCollection<ModeLivraison> ModeLivraisons { get => this.modeLivraisons; set => this.modeLivraisons = value; }

        private ApplicationData()
        {
            TypesProduit = TypeProduit.Read();
            Categories = Categorie.Read(TypesProduit);
            Produits = ProduitAchat.FromListProduit(Produit.Read(Categories));
            Fournisseurs = Fournisseur.Read();
            Utilisateurs = Utilisateur.Read();
            ModeLivraisons = ModeLivraison.Read();
        }

        public delegate bool CallBack<T>(T item);

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

        public static ObservableCollection<T> Filter<T>(ObservableCollection<T> from, CallBack<T> callback)
        {
            ObservableCollection<T> result = new ObservableCollection<T>();
            foreach (T item in from)
            {
                if (callback.Invoke(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
