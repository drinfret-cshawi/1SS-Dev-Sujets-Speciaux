# Diagramme de classes pour le projet DataRepo4

**À faire** : réviser l'accessibilité (`public`, `protected`, ..., `private`) des propriétés et méthodes

````plantuml
interface IDataSource {
    +Connect() : bool
    +Close()
}

abstract class SqlDataSource {
    +ConnectionString : string { get; set; }
    +Connection : DbConnection? { get; set; }
    +SqlDataSource(connectionString : string)
    +SqlDataSource(server : string, db : string, user : string, password : string, port : int)
    +{abstract} Connect() : bool
    +{abstract} Close()
    +{abstract} GetCommand(sql : string?) : DbCommand
    +{abstract} GetDbParameter(name : string, sqlDbType : SqlDbType, value : string)? :  DbParameter
    +{abstract} GetIntDbParameter(name : string, value : int?) :  DbParameter
}

IDataSource <|.. SqlDataSource

class PgsqlDataSource {
    +PgsqlDataSource(connectionString : string)
    +PgsqlDataSource(server : string, db : string, user : string, password : string, port : int)
    +Connect() : bool
    +Close()
    +GetCommand(sql : string?) : DbCommand
    +GetDbParameter(name : string, sqlDbType : SqlDbType, value : string?) :  DbParameter
    +GetIntDbParameter(name : string, value : int?) :  DbParameter
}
SqlDataSource <|-- PgsqlDataSource

class MySqlDataSource {
    +MySqlDataSource(connectionString : string)
    +MySqlDataSource(server : string, db : string, user : string, password : string, port : int)
    +Connect() : bool
    +Close()
    +GetCommand(sql : string?) : DbCommand
    +GetDbParameter(name : string, sqlDbType : SqlDbType, value : string?) :  DbParameter
    +GetIntDbParameter(name : string, value : int?) :  DbParameter
}
SqlDataSource <|-- MySqlDataSource

interface IDataRepo<TData, TKey> {
    +Select() : List<TData> 
    +Select(TKey id) : TData? 
    +Insert(TData data) : TKey?
    +Update(TData data) : bool
    +Delete(TData data) : bool
    +Delete(TKey key) : bool
}

abstract class SqlDataRepo<TData, TKey> {
    {static} +GetStringOrNull(reader : DbDataReader, col : int)
    +SqlDataSource : SqlDataSource { get; set; }
    +SqlDataRepo(SqlDataSource)
    +{abstract} Select() : List<TData> 
    +{abstract} Select(TKey id) : TData? 
    +{abstract} Insert(TData data) : TKey?
    +{abstract} Update(TData data) : bool
    +{abstract} Delete(TData data) : bool
    +{abstract} Delete(TKey key) : bool
}

IDataRepo <|.. SqlDataRepo : <TData, TKey>
SqlDataSource <- SqlDataRepo

class SqlUserRepo {
    {static} +CreateUser(DbDataReader) : User
    +SqlUserRepo(SqlDataSource)
    +Select() : List<User> 
    +Select(int id) : User? 
    +Insert(User data) : TKey?
    +Update(User data) : bool
    +Delete(User data) : bool
    +Delete(int key) : bool
}

SqlDataRepo <|-- SqlUserRepo : <User, int>

class SqlPasswordRepo {
    {static} +CreatePassword(DbDataReader) : Password
    +SqlPasswordRepo(SqlDataSource)
    +Select() : List<Password> 
    +Select(int id) : Password? 
    +Insert(Password data) : TKey?
    +Update(Password data) : bool
    +Delete(Password data) : bool
    +Delete(int key) : bool
}

SqlDataRepo <|-- SqlPasswordRepo : <Password, int>

class User {
    +Id : int { get; set; }
    +UserName : string { get; set; }
    +FullName : string? { get; set; }
    +Pswd : string { get; set; }
    +Email : string? { get; set; }
    +User(id : int, userName : string, fullName : string?, pswd : string, email : string?)
}
SqlUserRepo ..> User

class Password {
    +Id : int { get; set; }
    +UserId : int { get; set; }
    +Site : string { get; set; }
    +Login : string? { get; set; }
    +Pswd : string { get; set; }
    +Password(id : int, userId : int, site : string, login : string?, pswd : string)
}
SqlPasswordRepo ..> Password
````