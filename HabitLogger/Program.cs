// See https://aka.ms/new-console-template for more information

using System.Data.SQLite;

public class Start
{
    static void Main(string[] args)
    {
        SQLiteConnection m_dbConnection;

        string filePath = @"D:\study\HabitLogger\HabitLogger\bin\Debug\net8.0\mydb.sqlite";

        if (File.Exists(filePath) && Path.GetExtension(filePath).Equals(".db", StringComparison.OrdinalIgnoreCase))
        {
            m_dbConnection = new SQLiteConnection($"Data Source={filePath}");
            m_dbConnection.Open();
        }

        else
        {
            Console.WriteLine("파일이 없습니다. ");
            Console.WriteLine("데이터베이스를 생성합니다.");

            SQLiteConnection.CreateFile(filePath);

            m_dbConnection = new SQLiteConnection($"Data Source={filePath}");
            m_dbConnection.Open();

            string sql = "CREATE TABLE habit (" +
                         "habit_name TEXT," +
                         "start_date TEXT," +
                         "end_date TEXT," +
                         "amount REAL," +
                         "unit TEXT)";


            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

            command.ExecuteNonQuery();
        }

        string sqlCheck = "SELECT COUNT(*) FROM habit";
        SQLiteCommand countCommand = new SQLiteCommand(sqlCheck, m_dbConnection);
        int count = Convert.ToInt32(countCommand.ExecuteScalar());

        if (count == 0)
        {
            string connectionString = $"Data Source={filePath}";
            Random random = new Random();
            string[] units = { "hours", "amount", "개수", "days" };
            string[] letters = { "A", "B", "C", "D" };

            for (int i = 0; i < 100; i++)
            {
                string startDate = RandomDate(random, 2024).ToString("yyyy-MM-dd");
                string endDate = RandomDate(random, 2024).ToString("yyyy-MM-dd");
                int amount = random.Next(0, 11);
                string unit = units[random.Next(units.Length)];

                // Use modulo to cycle through the letters
                string currentLetter = letters[i % letters.Length];

                InsertHabit(connectionString, currentLetter, startDate, endDate, amount, unit);
            }
        }
        else
        {
            Console.WriteLine("테이블에 데이터가 있어 더미데이터를 집어넣지 않습니다.");
        }
        m_dbConnection.Close();

        Console.WriteLine("숫자를 입력하세요.");
    }

    static DateTime RandomDate(Random random, int year)
    {
        int month = random.Next(1, 13);
        int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
        return new DateTime(year, month, day);
    }

    static void InsertHabit(string connectionString, string habitName, string startDate, string endDate, int amount, string unit)
    {
        string sql = "INSERT INTO habit (habit_name, start_date, end_date, amount, unit) " +
                     "VALUES (@habit_name, @start_date, @end_date, @amount, @unit)";

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@habit_name", habitName);
            command.Parameters.AddWithValue("@start_date", startDate);
            command.Parameters.AddWithValue("@end_date", endDate);
            command.Parameters.AddWithValue("@amount", amount);
            command.Parameters.AddWithValue("@unit", unit);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
