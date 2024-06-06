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
        private string nom;
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

        public string Nom { get => this.nom; set => this.nom = value; }

        public Utilisateur(int id, string login, string nom, string password)
        {
            this.Id = id;
            this.Login = login;
            this.Nom = nom;
            this.Password = password;
        }

        public Utilisateur(int id, string login, string nom, string password, int numMagasin)
        {
            this.Id = id;
            this.Login = login;
            this.Nom = nom;
            this.Password = password;
            this.NumMagasin = numMagasin;
        }

        public override bool Equals(object? obj)
        {
            return obj is Utilisateur utilisateur &&
                   this.Id == utilisateur.Id &&
                   this.Login == utilisateur.Login &&
                   this.Nom == utilisateur.Nom &&
                   this.Password == utilisateur.Password &&
                   this.NumMagasin == utilisateur.NumMagasin;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.Login, this.Nom, this.Password, this.NumMagasin);
        }

        public override string? ToString()
        {
            return $"Login : {Login}\nPassword : ****";
        }

        public static ObservableCollection<Utilisateur> Read()
        {
            ObservableCollection<Utilisateur> unUtilisateur = new ObservableCollection<Utilisateur>();
            string sql = "SELECT * FROM utilisateur";
            DataTable dt = DataAccess.Instance?.GetData(sql) ?? new DataTable();
            foreach (DataRow res in dt.Rows)
            {
                Utilisateur nouveau = new Utilisateur(
                    int.Parse(res["num_utilisateur"].ToString()!),
                    res["login_user"].ToString()!,
                    res["nom_user"].ToString()!,
                    res["pass_user"].ToString()!,
                    int.Parse(res["num_magasin"].ToString()!)
                );
                unUtilisateur.Add(nouveau);
            }
            return unUtilisateur;
        }

        public static Utilisateur? Current()
        {
            string sql = "SELECT * FROM utilisateur WHERE login_user=user";
            DataTable dt = DataAccess.Instance?.GetData(sql) ?? new DataTable();

            if (dt.Rows.Count == 0)
                return null;

            DataRow res = dt.Rows[0];

            return new Utilisateur(
                int.Parse(res["num_utilisateur"].ToString()!),
                res["login_user"].ToString()!,
                res["nom_user"].ToString()!,
                res["pass_user"].ToString()!,
                int.Parse(res["num_magasin"].ToString()!)
            );
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
