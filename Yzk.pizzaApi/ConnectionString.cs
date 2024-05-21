using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yzk.pizzaApi;

internal static class ConnectionString
{
    public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = ".",
        InitialCatalog = "DotNetTrainingBatch4",
        UserID = "sa",
        Password = "sa@123",
        TrustServerCertificate = true,


    };

    public static object ConnectionStringBuilder { get; internal set; }
    public static SqlConnectionStringBuilder SqlConnectionStringBuilder { get => sqlConnectionStringBuilder; set => sqlConnectionStringBuilder = value; }
}
