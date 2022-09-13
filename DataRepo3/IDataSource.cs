namespace DataRepo3;

public interface IDataSource
{
    public bool Connect();

    public void Close();
    
}