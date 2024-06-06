using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaquetteBotanic
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class Connexion : Window
    {
        public const int MAX_USERNAME_LENGTH = 100;
        
        public Connexion()
        {
            InitializeComponent();
        }

        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer les entrées utilisateurs
            string usr = tbxUsername.Text;
            string pwd = tbxPassword.Password;
            
            // Vérifier que les valeurs sont correctes
            if (string.IsNullOrEmpty(usr) || usr.Length > MAX_USERNAME_LENGTH || string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show(
                    "Le nom d'utilisateur ou le mot de passe est incorrect.",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }
            
            // Mettre à jour les identifiants DataAccess
            DataAccess.username = usr;
            DataAccess.password = pwd;

            // Vérifier la connexion à la base de données
            try
            {
                DataAccess.Instance?.Test();
            }
            catch
            {
                MessageBox.Show(
                    "Le nom d'utilisateur ou le mot de passe est incorrect.",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }
            
            // Ouvrir la mainWindow si les identifiants sont corrects.
            this.DialogResult = true;
        }
    }
}
