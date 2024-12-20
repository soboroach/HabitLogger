// See https://aka.ms/new-console-template for more information

using System.Data.SQLite;

public class Start
{
    static void Main(string[] args)
    {
        SQLiteConnection sqlite_conn;
        SQLiteCommand sqlite_cmd;

        if (File.Exists(@"D:\study\HabitLogger"))
        {
            Console.WriteLine("파일이 있습니다.");
        }
        else if
    }
}
