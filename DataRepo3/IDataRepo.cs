namespace DataRepo3;

public interface IDataRepo<TData, TKey> : IDataSource
{
    public List<TData> Select();
    public TData? Select(TKey id);
    public bool Insert(TData data);
    public bool Update(TData data);
    public bool Delete(TData data);
    public bool Delete(TKey key);
}