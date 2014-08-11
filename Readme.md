Disclaimer
===================================================

There's nothing new in here! I simply grabbed the ideas from [Optimistic concurrency for MongoDB in .NET][id] and made them a little more fluent.

"Fluent" Optimistic concurrency for MongoDB in .NET
===================================================

*As you can see, also the title is almost completely stolen.*

The reason behind a new mongo optimistic concurrency library are:

- Work with every object implementing **INeedOptimisticConcurrency** interface
- Try to avoid the need of a base "context" class
- No imposed Id element **name** or **type**
- Allow the user to change the Bson element name for the Version Field

An optimistic Update or Remove will be allowed only if requested on the same object version present in the database. 

So, given a class Person implementing the INeedOptimisticConcurrency, it can be updated and removed in the following way:

```C#
var collection = mongoDatabaseInstance.GetCollection("collectionName");

collection.Optimistic<Person>().Update(person);

collection.Optimistic<Person>().Remove(person);
```

Or, with a typed MongoCollection instance:

```C#
var collection = database.GetCollection<Person>("collectionName");

collection.Optimistic().Update(person);

collection.Optimistic().Remove(person);
```

These are the different kinds of entity objects I used with this library:

```C#
public class Person : INeedOptimisticConcurrency
{
    public string Id { get; set; }
    public int Version { get; set; }
}

public class Person : INeedOptimisticConcurrency
{
	[BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public int Version { get; set; }
}

public class Person : INeedOptimisticConcurrency
{
    public ObjectId Id { get; set; }
    public int Version { get; set; }
}

public class Person : INeedOptimisticConcurrency
{
    [BsonId]
    public string Name { get; set; }

	[BsonElement("ver")]				//In case you want to save some space in your mongo database
    public int Version { get; set; }
}
```

[id]: https://github.com/mikeckennedy/optimistic_concurrency_mongodb_dotnet