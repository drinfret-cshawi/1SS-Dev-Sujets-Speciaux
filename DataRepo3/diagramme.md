````plantuml
interface IDataSource {
    +Connect() : bool
    +Close()
}

interface IDataRepo<TData, TKey> {
    +Select() : List<TData> 
    +Select(TKey id) : TData? 
    +Insert(TData data) : bool
    +Update(TData data) : bool
    +Delete(TData data) : bool
    +Delete(TKey key) : bool
}
IDataSource <|-- IDataRepo : <TData, TKey>

abstract class SqlDataRepo<TData, TKey> {
    {static} +GetStringOrNull(reader : DbDataReader, col : int)
    +ConnectionString : string { get; set; }
    +Connection : DbConnection? { get; set; }
    +SqlDataRepo(connectionString : string)
    +SqlDataRepo(server : string, db : string, user : string, password : string, port : int)
    +{abstract} Connect() : bool
    +{abstract} Close()
    +{abstract} GetCommand(sql : string?) : DbCommand
    +{abstract} GetDbParameter(name : string, sqlDbType : SqlDbType, value : int) :  DbParameter
    +{abstract} Select() : List<TData> 
    +{abstract} Select(TKey id) : TData? 
    +{abstract} Insert(TData data) : bool
    +{abstract} Update(TData data) : bool
    +{abstract} Delete(TData data) : bool
    +{abstract} Delete(TKey key) : bool
}
IDataRepo <|.. SqlDataRepo : <TData, TKey>

abstract class PgsqlRepo<TData, TKey> {
    +PgsqlRepo(connectionString : string)
    +PgsqlRepo(server : string, db : string, user : string, password : string, port : int)
    +Connect() : bool
    +Close()
    +GetCommand(sql : string?) : DbCommand
    +GetDbParameter(name : string, sqlDbType : SqlDbType, value : int) :  DbParameter
    
}
SqlDataRepo <|-- PgsqlRepo : <TData, TKey>

class PgsqlUserRepo {
    +PgsqlUserRepo(connectionString : string)
    +PgsqlUserRepo(server : string, db : string, user : string, password : string, port : int)
    +Select() : List<User> 
    +Select(int id) : User? 
    +Insert(User data) : bool
    +Update(User data) : bool
    +Delete(User data) : bool
    +Delete(int key) : bool
}
PgsqlRepo <|-- PgsqlUserRepo : <User, int>

class PgsqlPasswordRepo {
    +PgsqlPasswordRepo(connectionString : string)
    +PgsqlPasswordRepo(server : string, db : string, user : string, password : string, port : int)
    +Select() : List<Password> 
    +Select(int id) : Password? 
    +Insert(Password data) : bool
    +Update(Password data) : bool
    +Delete(Password data) : bool
    +Delete(int key) : bool
}
PgsqlRepo <|-- PgsqlPasswordRepo : <Password, int>

class User {
    +Id : int { get; set; }
    +UserName : string { get; set; }
    +FullName : string? { get; set; }
    +Pswd : string { get; set; }
    +Email : string? { get; set; }
    +User(id : int, userName : string, fullName : string?, pswd : string, email : string?)
}
User <.. PgsqlUserRepo


class Password {
    +Id : int { get; set; }
    +UserId : int { get; set; }
    +Site : string { get; set; }
    +Login : string? { get; set; }
    +Pswd : string { get; set; }
    +Password(id : int, userId : int, site : string, login : string?, pswd : string)
}
Password <.. PgsqlPasswordRepo
````