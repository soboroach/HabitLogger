using System.Data;
using System.Data.SQLite;

namespace HabitLogger
{
    public class HabitLoggerDB
    {
        private SQLiteConnection sqlite;

        public HabitLoggerDB()
        {
            sqlite = new SQLiteConnection(@"D:\study\HabitLogger\dbFile");
        }

        public DataTable SelectQuery(string query)
        {
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();

            try
            {
                SQLiteCommand cmd;
                sqlite.Open();
                cmd = sqlite.CreateCommand();
                cmd.CommandText = query;
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt);
            }
            catch (SQLiteException ex)
            {

            }

            sqlite.Close();
            return dt;
        }
    }
}
