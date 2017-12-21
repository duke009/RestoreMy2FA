using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace GoogleAuthCruncher
{
    // No nedd to use Entity Framework here, lol
    class GoogleAuthDatabaseReader
    {
        private readonly SQLiteConnection sqlite;

        public GoogleAuthDatabaseReader(string path)
        {
            sqlite = new SQLiteConnection($@"Data Source={path};Version=3;FailIfMissing=True");
        }

        public IEnumerable<Account> GetAccounts()
        {
            var dt = new DataTable();
            try
            {
                sqlite.Open();
                using (var cmd = sqlite.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM accounts;";
                    var ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(dt);
                }
            }
            catch (SQLiteException ex)
            {

            }
            finally
            {
                sqlite.Close();
            }

            return GetAccounts(dt); 
        }

        private IEnumerable<Account> GetAccounts(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                yield return new Account() {Email = (string)row["email"], Issuer = (string)row["issuer"], Secret = (string)row["secret"], OriginalName = (string)row["original_name"] };
            }
        }
    }
}