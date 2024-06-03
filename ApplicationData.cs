using System.Collections.Generic;
using System.Collections.ObjectModel;
using static MaquetteBotanic.Produit;

namespace MaquetteBotanic
{
    public class ApplicationData
    {
        private ObservableCollection<ProduitAchat> produits;

        public ObservableCollection<ProduitAchat> Produits { get => this.produits; set => this.produits = value; }

        public ApplicationData()
        {
            this.Produits = ProduitAchat.FromListProduit(Produit.Read());
        }
    }
}
