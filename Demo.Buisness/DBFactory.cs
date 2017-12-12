using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace Demo.Buisness
{
    public static class DBFactory
    {
        public static IDbConnection GetConnection()
        {
            //var root = Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location );
            //var db = Path.Combine( root, "Demo.mdf" );
            //var dbEscaped = "\"" + db + "\"";
            //var source = $@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename = {dbEscaped}; Integrated Security = True";
            //return new SqlConnection( source );

            var acct = "bestuadmin";
            var pwd = "zn~iDRyz|wS8pL5]";

            return new SqlConnection( $"Server=tcp:bestudb.database.windows.net,1433;Initial Catalog=bestUDB;Persist Security Info=False;User ID={acct};Password={pwd};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" );
        }
    }
}
