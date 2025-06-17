public class Database
{
    private readonly string connectionString;

    public Database(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public SqlConnection GetConnection() => new SqlConnection(connectionString);
}

