using System.Collections.Generic;
using static MaquetteBotanic.Produit;

namespace MaquetteBotanic
{
    public class ApplicationData
    {
        private List<ProduitAchat> templateProduits;

        public List<ProduitAchat> TemplateProduits { get => this.templateProduits; set => this.templateProduits = value; }

        public ApplicationData()
        {
            this.templateProduits = ProduitAchat.FromListProduit(new List<Produit>()
            {
                new Produit("Produit 1", 19.99, "Description du produit 1", CouleurProduit.Blanc, TailleProduit.Petit),
                new Produit("Produit 2", 9.99, "Description du produit 2", CouleurProduit.Rouge, TailleProduit.TresPetit),
                new Produit("Produit 3", 149.99, "Description du produit 3", CouleurProduit.Noir, TailleProduit.Grand),
            });
        }
    }
}
