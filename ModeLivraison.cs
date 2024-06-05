using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquetteBotanic
{
    public class ModeLivraison: ICrud
    {
        private int id;
        private string libelle;

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

        public string Libelle
        {
            get
            {
                return this.libelle;
            }

            set
            {
                this.libelle = value;
            }
        }

        public ModeLivraison(int id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }

        public override bool Equals(object? obj)
        {
            return obj is ModeLivraison livraison &&
                   this.Id == livraison.Id &&
                   this.Libelle == livraison.Libelle;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.Libelle);
        }

        public override string? ToString()
        {
            return $"Mode de livraison : {Libelle}";
        }

        public static ObservableCollection<ModeLivraison> Read()
        {
            ObservableCollection<ModeLivraison> lesModesLivraisons = new ObservableCollection<ModeLivraison>();
            string sql = "SELECT NUM_MODE_LIVRAISON ,LIBELLE_MODE_LIVR FROM Mode_Livraison";
            DataTable dt = DataAccess.Instance?.GetData(sql) ?? new DataTable();
            foreach (DataRow res in dt.Rows)
            {
                ModeLivraison nouveau = new ModeLivraison(
                    int.Parse(res["NUM_MODE_LIVRAISON"].ToString()!),
                    res["LIBELLE_MODE_LIVR"].ToString()!
                );
                lesModesLivraisons.Add(nouveau);
            }
            return lesModesLivraisons;
        }

        public int Create()
        {
            string sql = $"INSERT INTO Mode_Livraison (NUM_MODE_LIVRAISON, LIBELLE_MODE_LIVR)"
                         + $" values ('{Id}','{Libelle}'";
            return DataAccess.Instance.SetData(sql);
        }

        public int Delete()
        {
            string sql = $"DELETE FROM Mode_Livraison" +
                         $" WHERE NUM_MODE_LIVRAISON = {Id}";
            return DataAccess.Instance.SetData(sql);
        }

        public int Update()
        {
            string sql = $"UPDATE Mode_Livraison" +
                         $" SET LIBELLE_MODE_LIVR={Libelle}," +
                         $" WHERE NUM_MODE_LIVRAISON={Id}";
            return DataAccess.Instance.SetData(sql);
        }
    }
}
