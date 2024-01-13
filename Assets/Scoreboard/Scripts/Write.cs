using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

public class Write : MonoBehaviour
{
    public Text name;
    public Text Surnames;
    public Text age;
    private string connectionString;
    private MySqlConnection MS_Connection;
    private MySqlCommand MS_Command;
    string query;

    public void sendInfo() {

        connection();

        query = "insert into Douglas(Nom, Cognom, Edat) values( '" + name.text + "' , '" + Surnames.text + "','" + age.text + "');";

        MS_Command = new MySqlCommand(query, MS_Connection);

        MS_Command.ExecuteNonQuery();

        MS_Connection.Close();
    }

    public void connection() {

        connectionString = "Server = ellaboratori.cat ; Database = marcadors ; User = uMarcadorExt; Password = QTxdhGkPGLO5eRUW; Charset = utf8;";
        MS_Connection = new MySqlConnection(connectionString);

        MS_Connection.Open();

    }

}
