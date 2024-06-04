using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquetteBotanic
{
    public class Commande
    {
        public enum ModesLivraison
        {
            UPS,
            Chronopost,
            Botanic,
            Relais,
            Autre
        }

        private int id;
        private DateTime dateCommande;
        private DateTime dateLivraison;
        private ModesLivraison modeLivraison;
        private List<DetailCommande> lesProduits;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public DateTime DateCommande
        {
            get
            {
                return dateCommande;
            }

            set
            {
                dateCommande = value;
            }
        }

        public DateTime DateLivraison
        {
            get
            {
                return dateLivraison;
            }

            set
            {
                dateLivraison = value;
            }
        }

        public List<DetailCommande> LesProduits
        {
            get
            {
                return this.lesProduits;
            }

            set
            {
                this.lesProduits = value;
            }
        }

        public ModesLivraison ModeLivraison
        {
            get
            {
                return this.modeLivraison;
            }

            set
            {
                this.modeLivraison = value;
            }
        }

        public Commande()
        {

        }

        public Commande(int id, DateTime dateCommande, DateTime dateLivraison, List<DetailCommande> lesProduits, ModesLivraison modeLivraison)
        {
            this.Id = id;
            this.DateCommande = dateCommande;
            this.DateLivraison = dateLivraison;
            this.LesProduits = lesProduits;
            this.ModeLivraison = modeLivraison;
        }

        public override bool Equals(object? obj)
        {
            return obj is Commande commande &&
                   this.Id == commande.Id &&
                   this.DateCommande == commande.DateCommande &&
                   this.DateLivraison == commande.DateLivraison &&
                   EqualityComparer<List<DetailCommande>>.Default.Equals(this.LesProduits, commande.LesProduits) &&
                   this.ModeLivraison == commande.ModeLivraison;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.DateCommande, this.DateLivraison, this.LesProduits, this.ModeLivraison);
        }

        public override string? ToString()
        {
            return $"ID : {Id} \nDate Commande : {DateCommande} \nMode de Livraison : {ModeLivraison} \nDate de Livraison : {DateLivraison}";
        }
    }
}
