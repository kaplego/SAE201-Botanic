using System;
using System.Data;
using System.IO;
using System.Windows;
using Npgsql;

namespace MaquetteBotanic;

public class DataAccess
{
    private static DataAccess? instance;
    public static string username;
    public static string password;

    public DataAccess()
    {
        ConnexionBD();
    }

    private static void ErrorMsgBox()
    {
        MessageBox.Show("Une erreur s'est produite avec la base de données.", "Erreur", MessageBoxButton.OK,
            MessageBoxImage.Error);
    }
    
    public static DataAccess? Instance
    {
        get
        {
            instance ??= new DataAccess();
            return instance;
        }
        set
        {
            if (value == null)
                instance = value;
        }
    }
    
    public NpgsqlConnection? Connexion
    {
        get;
        set;
    }
    
    public void ConnexionBD()
    {
        try
        {
            Connexion = new NpgsqlConnection();
            Connexion.ConnectionString = "Server=srv-peda-new;port=5433;"
                                         +$"Database=magnenal_botanic;Search Path=magnenal_botanic;uid={username};password={password};";
            Connexion.Open();
        }
        catch (Exception e)
        {
            Console.WriteLine("pb à la connexion : " + e);
            ErrorMsgBox();
        }
    }
    public void DeconnexionBD()
    {
        try
        {
            Connexion.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("pb à la déconnexion : " + e);
            ErrorMsgBox();
        }
    }

    public bool Test()
    {
        try
        {
            new NpgsqlDataAdapter("SELECT 1;", Connexion);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("pb de test : " + e.ToString());
            ErrorMsgBox();
            return false;
        }
    }
    
    public DataTable GetData(string selectSQL)
    {
        try
        {
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(selectSQL, Connexion);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }
        catch (Exception e)
        {
            Console.WriteLine("pb avec : " + selectSQL + e.ToString());
            ErrorMsgBox();
            return null;
        }
    }
    
    public int SetData(string setSQL)
    {
        try
        {
            NpgsqlCommand sqlCommand = new NpgsqlCommand(setSQL, Connexion);
            int nbLines = sqlCommand.ExecuteNonQuery();
            return nbLines;
        }
        catch (Exception e)
        {
            Console.WriteLine("pb avec : " + setSQL + e.ToString());
            ErrorMsgBox();
            return 0;
        }
    }
}