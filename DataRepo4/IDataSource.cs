namespace DataRepo4;

public interface IDataSource
{
    public bool Connect();

    public void Close();
    
}