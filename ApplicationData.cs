using System;
using System.Collections.ObjectModel;

namespace MaquetteBotanic
{
    public class ApplicationData
    {
        private static ApplicationData instance;
        private Utilisateur utilisateurActuel;
        private Magasin magasinActuel;

        private ObservableCollection<TypeProduit> typesProduit;
        private ObservableCollection<Categorie> categories;
        private ObservableCollection<Produit> produits;
        private ObservableCollection<Commande> commandes;
        private ObservableCollection<Fournisseur> fournisseurs;
        private ObservableCollection<ModeLivraison> modeLivraisons;
        private ObservableCollection<Fournit> produitsFournits;

        public static ApplicationData Instance {
            get {
                instance ??= new ApplicationData();
                return instance;
            }
        }

        public ObservableCollection<TypeProduit> TypesProduit { get => this.typesProduit; set => this.typesProduit = value; }
        public Utilisateur UtilisateurActuel { get => this.utilisateurActuel; set => this.utilisateurActuel = value; }
        public Magasin MagasinActuel { get => this.magasinActuel; set => this.magasinActuel = value; }

        public ObservableCollection<Categorie> Categories { get => categories; set => categories = value; }
        public ObservableCollection<Produit> Produits { get => this.produits; set => this.produits = value; }
        public ObservableCollection<Commande> Commandes { get => this.commandes; set => this.commandes = value; }
        public ObservableCollection<Fournisseur> Fournisseurs { get => this.fournisseurs; set => this.fournisseurs = value; }
        public ObservableCollection<ModeLivraison> ModeLivraisons { get => this.modeLivraisons; set => this.modeLivraisons = value; }
        public ObservableCollection<Fournit> ProduitsFournits { get => this.produitsFournits; set => this.produitsFournits = value; }

        private ApplicationData()
        {
            UtilisateurActuel = Utilisateur.Current()!;

            MagasinActuel = Magasin.Current(UtilisateurActuel)!;

            Fournisseurs = Fournisseur.Read();
            ModeLivraisons = ModeLivraison.Read();
            TypesProduit = TypeProduit.Read();
            Categories = Categorie.Read(TypesProduit);
            Produits = Produit.Read(Categories);
            ProduitsFournits = Fournit.Read(
                Fournisseurs,
                Produits
            );
            Commandes = Commande.Read(MagasinActuel);
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
