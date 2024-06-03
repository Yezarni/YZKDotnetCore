using System.Data.SqlClient;

namespace Yzk.Nlayer.DataAccess;

internal static class ConnectionString
{
    public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = ".",
        //   InitialCatalog = "DotNetTrainingBatch4",
        UserID = "sa",
        Password = "sa@123",
        TrustServerCertificate = true,


    };


}
