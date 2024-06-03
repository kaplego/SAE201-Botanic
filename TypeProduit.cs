using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace MaquetteBotanic
{
    public class TypeProduit
    {
        private int id;
        private string nom;

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }

        public TypeProduit(string nom)
        {
            this.Nom = nom;
        }

        private TypeProduit(int id, string nom)
            : this(nom)
        {
            this.Id = id;
        }
        
        public static ObservableCollection<TypeProduit> Read()
        {
            ObservableCollection<TypeProduit> lesTypes = new ObservableCollection<TypeProduit>();
            string sql = "SELECT num_type, designation_type FROM categorie";
            DataTable dt = DataAccess.Instance.GetData(sql);
            foreach (DataRow res in dt.Rows)
            {
                TypeProduit nouveau = new TypeProduit(
                    int.Parse(res["num_type"].ToString()!),
                    res["designation_type"].ToString()!
                );
                lesTypes.Add(nouveau);
            }
            return lesTypes;
        }

        public override string? ToString()
        {
            return $"Sous-catégorie {this.Nom}";
        }
        
        public int Create()
        {
            string sql = $"INSERT INTO type_produit (designation_type)"
                         + $" values ('{Nom}');";
            return DataAccess.Instance.SetData(sql);
        }

        public int Delete()
        {
            string sql = $"DELETE FROM type_produit" +
                         $" WHERE num_type = {Id}";
            return DataAccess.Instance.SetData(sql);
        }

        public int Update()
        {
            string sql = $"UPDATE type_produit " +
                         $"SET designation_type='{Nom}' " +
                         $"WHERE num_type={Id}";
            return DataAccess.Instance.SetData(sql);
        }
    }
}
