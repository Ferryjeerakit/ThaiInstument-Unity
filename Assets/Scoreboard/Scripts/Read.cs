using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;


//โชว์ scoreboard
public class Read : MonoBehaviour
{
   [SerializeField] Button fetchdatabtn;
    private string connectionString;
    string query;
    private MySqlConnection MS_Connection;
    private MySqlCommand MS_Command;
    private MySqlDataReader MS_Reader;
    public Text textCanvas;

    public void viewInfo() {

        fetchdatabtn.gameObject.SetActive(false);
        query = "SELECT * FROM users";

        connectionString = "Server = localhost ; Database = unity ; User = root; Password = ; Charset = utf8;";

        MS_Connection = new MySqlConnection(connectionString);
        MS_Connection.Open();

        MS_Command = new MySqlCommand(query, MS_Connection);

        MS_Reader = MS_Command.ExecuteReader();
        while (MS_Reader.Read())
        {
            textCanvas.text += "\n                                       " + MS_Reader[1] + "                                                " + MS_Reader[3];
        }
        MS_Reader.Close();

    }
   
}