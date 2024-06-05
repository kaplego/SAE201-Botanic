using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquetteBotanic
{
    public class Fournisseur : ICrud
    {
        private int id;
        private string nom;
        private bool local;

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

        public string Nom
        {
            get
            {
                return nom;
            }

            set
            {
                nom = value;
            }
        }

        public bool Local
        {
            get
            {
                return this.local;
            }

            set
            {
                this.local = value;
            }
        }

        public Fournisseur(int id, string nom, bool local)
        {
            this.Id = id;
            this.Nom = nom;
            this.Local = local;
        }

        public override bool Equals(object? obj)
        {
            return obj is Fournisseur fournisseur &&
                   this.Id == fournisseur.Id &&
                   this.Nom == fournisseur.Nom &&
                   this.Local == fournisseur.Local;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.Nom, this.Local);
        }

        public override string? ToString()
        {
            return $"Nom Fournisseur : {Nom} \nLocal : {(Local == true ? "Oui" : "Non") }";
        }

        public static ObservableCollection<Fournisseur> Read()
        {
            ObservableCollection<Fournisseur> lesFournisseurs = new ObservableCollection<Fournisseur>();
            string sql = "SELECT NUM_FOURNISSEUR, NOM_FOURNISSEUR, CODE_LOCAL FROM Fournisseur";
            DataTable dt = DataAccess.Instance?.GetData(sql) ?? new DataTable();
            foreach (DataRow res in dt.Rows)
            {
                Fournisseur nouveau = new Fournisseur(
                    int.Parse(res["NUM_FOURNISSEUR"].ToString()!),
                    res["NOM_FOURNISSEUR"].ToString()!,
                    bool.Parse(res["CODE_LOCAL"].ToString()!)
                );
                lesFournisseurs.Add(nouveau);
            }
            return lesFournisseurs;
        }

        public int Create()
        {
            string sql = $"INSERT INTO Fournisseur (NUM_FOURNISSEUR, NOM_FOURNISSEUR, CODE_LOCAL)"
                         + $" values ('{Id}','{Nom}','{Local}'";
            return DataAccess.Instance.SetData(sql);
        }

        public int Delete()
        {
            string sql = $"DELETE FROM Fournisseur" +
                         $" WHERE NUM_FOURNISSEUR = {Id}";
            return DataAccess.Instance.SetData(sql);
        }

        public int Update()
        {
            string sql = $"UPDATE Fournisseur" +
                         $" SET NOM_FOURNISSEUR={Nom}," +
                         $" CODE_LOCAL='{Local}'" +
                         $" WHERE NUM_FOURNISSEUR={Id}";
            return DataAccess.Instance.SetData(sql);
        }


    }
}
