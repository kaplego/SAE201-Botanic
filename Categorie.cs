using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace MaquetteBotanic
{
    public class Categorie : ICrud
    {
        private int id;
        private string nom;
        private TypeProduit type;

        public int Id
        {
            get => this.id;
            set => this.id = value;
        }
        public string Nom { get => this.nom; set => this.nom = value; }
        public TypeProduit Type { get => this.type; set => this.type = value; }

        public Categorie(string nom)
        {
            this.Nom = nom;
        }
        
        public Categorie(int id, string nom, TypeProduit type)
            : this(nom)
        {
            this.Id = id;
            this.Type = type;
        }
        
        public static ObservableCollection<Categorie> Read(ObservableCollection<TypeProduit> typesProduit)
        {
            ObservableCollection<Categorie> lesCategories = new ObservableCollection<Categorie>();
            string sql = "SELECT num_categorie, num_type, libelle_categorie FROM categorie";
            DataTable dt = DataAccess.Instance?.GetData(sql) ?? new DataTable();
            foreach (DataRow res in dt.Rows)
            {
                TypeProduit? type = ApplicationData.Find(typesProduit, (i) => i.Id == int.Parse(res["num_type"].ToString()));
                if (type == null)
                    throw new Exception("Le type produit n'existe pas.");

                Categorie nouveau = new Categorie(
                    int.Parse(res["num_categorie"].ToString()!),
                    res["libelle_categorie"].ToString()!,
                    type
                );
                lesCategories.Add(nouveau);
            }
            return lesCategories;
        }

        public override string? ToString()
        {
            return $"Catégorie {this.Nom}";
        }

        public int Create()
        {
            string sql = $"INSERT INTO categorie (num_type, libelle_categorie)"
                         + $" values ('{Nom}','{Type}');";
            return DataAccess.Instance.SetData(sql);
        }

        public int Delete()
        {
            string sql = $"DELETE FROM categorie" +
                         $" WHERE num_categorie = {Id}";
            return DataAccess.Instance.SetData(sql);
        }

        public int Update()
        {
            string sql = $"UPDATE categorie " +
                         $"SET num_type='{Type}', libelle_categorie='{Nom}' " +
                         $"WHERE num_categorie={Id}";
            return DataAccess.Instance.SetData(sql);
        }
    }
}
