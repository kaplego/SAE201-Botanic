using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquetteBotanic
{
    public class DetailCommande
    {
        private Produit leProduit;
        private int qteProduit;

        public Produit LeProduit
        {
            get
            {
                return leProduit;
            }

            set
            {
                leProduit = value;
            }
        }

        public int QteProduit
        {
            get
            {
                return this.qteProduit;
            }

            set
            {
                if(value < 1 || value > 100) throw new ArgumentOutOfRangeException("la quantité doit être comprise entre 1 et 100");
                this.qteProduit = value;
            }
        }

        public DetailCommande()
        {

        }

        public DetailCommande(Produit leProduit, int qteProduit)
        {
            this.LeProduit = leProduit;
            this.QteProduit = qteProduit;
        }

        public override string? ToString()
        {
            return $"{LeProduit} \nQuantité : {QteProduit}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.LeProduit, this.QteProduit);
        }

        public override bool Equals(object? obj)
        {
            return obj is DetailCommande commande &&
                   EqualityComparer<Produit>.Default.Equals(this.LeProduit, commande.LeProduit) &&
                   this.QteProduit == commande.QteProduit;
        }
    }
}
