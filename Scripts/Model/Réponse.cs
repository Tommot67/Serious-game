using System.Data;
using Godot;
using Mono.Data.Sqlite;

namespace T3Projet.Scripts.Models;

public class Réponse
{
    // Liste des attributs de la réponse.
    private int ID;
    private string réponse;
    public string RéponseText 
    {
        get => réponse;
    }
    private int stress;
    public int Stress 
    {
        get => stress;
    }
    
    public Réponse(int ID)
    {
        this.ID = ID;
        ChargerRéponse();
    }
    
    /// <summary>
    /// Méthode qui charge les informations de la réponse dans la base de donnée.
    /// </summary>
    /// <returns></returns>
    private void ChargerRéponse()
    {
        try
        {
            SqliteCommand command = new SqliteCommand()
            {
                Connection = Database.GetConnection(),
                CommandType = CommandType.Text,
                CommandText = "SELECT * FROM Reponses WHERE id = @ID;",
            };
            command.Parameters.Add("@ID", DbType.Int32);
            command.Parameters[0].Value = ID;

            SqliteDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                this.réponse = data.GetString(1);
                this.stress = data.GetInt32(2);
            }
        }
        catch (SqliteException err)
        {
            GD.Print("ERREUR DB Questions : " + err.Message);
        }
    }
}