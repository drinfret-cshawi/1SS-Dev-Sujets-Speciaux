
using System.Data.Common;

namespace DataRepo4;

public abstract class SqlDataRepo<TData, TKey> : IDataRepo<TData, TKey>
{
    public static string? GetStringOrNull(DbDataReader reader, int col)
    {
        return Convert.IsDBNull(reader[col]) ? null : reader.GetString(col);
    }
    
    public SqlDataSource SqlDataSource { get; set; }

    protected SqlDataRepo(SqlDataSource sqlDataSource)
    {
        SqlDataSource = sqlDataSource;
    }

    public abstract List<TData> Select();

    public abstract TData? Select(int id);

    public abstract TKey? Insert(TData data);

    public abstract bool Update(TData data);

    public abstract bool Delete(TData data);

    public abstract bool Delete(TKey key);
}