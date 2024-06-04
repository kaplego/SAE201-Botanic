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
        private ObservableCollection<Categorie> categories;
        private ObservableCollection<TypeProduit> typesProduits;

        public static ApplicationData Instance {
            get {
                instance ??= new ApplicationData();
                return instance;
            }
        }

        public ObservableCollection<TypeProduit> TypesProduit { get => this.typesProduit; set => this.typesProduit = value; }
        public ObservableCollection<Categorie> Categories { get => categories; set => categories = value; }
        public ObservableCollection<ProduitAchat> Produits { get => this.produits; set => this.produits = value; }
        public ObservableCollection<Categorie> Categories { get => categories; set => categories = value; }
        public ObservableCollection<TypeProduit> TypesProduits { get => typesProduits; set => typesProduits = value; }

        private ApplicationData()
        {
            TypesProduit = TypeProduit.Read();
            Categories = Categorie.Read();
            Produits = ProduitAchat.FromListProduit(Produit.Read());
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
    }
}
