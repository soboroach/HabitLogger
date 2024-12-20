// See https://aka.ms/new-console-template for more information

using System.Data.SQLite;

public class Start
{
    static void Main(string[] args)
    {
        SQLiteConnection m_dbConnection;

        string filePath = @"D:\study\HabitLogger\HabitLogger\bin\Debug\net8.0\dbFile.db";

        if (File.Exists(filePath) && Path.GetExtension(filePath).Equals(".db", StringComparison.OrdinalIgnoreCase))
        {
            m_dbConnection = new SQLiteConnection("Data Source=mydb.sqlite;Version=3");
            m_dbConnection.Open();
        }

        else
        {
            Console.WriteLine("파일이 없습니다. ");
            Console.WriteLine("데이터베이스를 생성합니다.");

            SQLiteConnection.CreateFile("mydb.sqlite");

            m_dbConnection = new SQLiteConnection("Data Source=mydb.sqlite;Version=3;");
            m_dbConnection.Open();

            string sql = "CREATE TABLE Habit (" +
                         "habit_name TEXT," +
                         "start_date TEXT," +
                         "end_date TEXT," +
                         "amount REAL," +
                         "unit TEXT)";


            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

            command.ExecuteNonQuery();
        }
    }
}
