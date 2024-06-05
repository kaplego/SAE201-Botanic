using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquetteBotanic
{
    public class Utilisateur: ICrud
    {
        private int id;
        private string login;
        private string password;
        private int numMagasin;

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

        public string Login
        {
            get
            {
                return login;
            }

            set
            {
                login = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public int NumMagasin
        {
            get
            {
                return this.numMagasin;
            }

            set
            {
                this.numMagasin = value;
            }
        }

        public Utilisateur(int id, string login, string password)
        {
            this.Id = id;
            this.Login = login;
            this.Password = password;
        }

        public override bool Equals(object? obj)
        {
            return obj is Utilisateur utilisateur &&
                   this.Id == utilisateur.Id &&
                   this.Login == utilisateur.Login &&
                   this.Password == utilisateur.Password &&
                   this.NumMagasin == utilisateur.NumMagasin;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.Login, this.Password, this.NumMagasin);
        }

        public override string? ToString()
        {
            return $"Login : {Login} \nPassword : ****";
        }

        public static ObservableCollection<Utilisateur> Read()
        {
            ObservableCollection<Utilisateur> lesCommandes = new ObservableCollection<Utilisateur>();
            string sql = "SELECT NUM_UTILISATEUR, LOGIN_USER, PASS_USER FROM Utilisateur";
            DataTable dt = DataAccess.Instance?.GetData(sql) ?? new DataTable();
            foreach (DataRow res in dt.Rows)
            {
                Utilisateur nouveau = new Utilisateur(
                    int.Parse(res["num_commande"].ToString()!),
                    res["date_commande"].ToString()!,
                    res["date_livraison"].ToString()!
                );
                lesCommandes.Add(nouveau);
            }
            return lesCommandes;
        }

        public int Create()
        {
            string sql = $"INSERT INTO Utilisateur (NUM_UTILISATEUR, LOGIN_USER, PASS_USER)"
                         + $" values ('{Id}','{Login}','{Password}'";
            return DataAccess.Instance.SetData(sql);
        }

        public int Delete()
        {
            string sql = $"DELETE FROM Utilisateur" +
                         $" WHERE NUM_UTILISATEUR = {Id}";
            return DataAccess.Instance.SetData(sql);
        }

        public int Update()
        {
            string sql = $"UPDATE Utilisateur" +
                         $" SET LOGIN_USER={Login}," +
                         $" PASS_USER='{Password}'" +
                         $" WHERE NUM_UTILISATEUR={Id}";
            return DataAccess.Instance.SetData(sql);
        }
    }
}
