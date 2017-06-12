using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBSamples
{
	class Program
	{
		static void Main(string[] args)
		{
			MainAsinc().Wait();
			Console.ReadLine();
		}

		static async Task MainAsinc()
		{
			var client = new MongoClient();

			var db = client.GetDatabase("test");
			var col = db.GetCollection<BsonDocument>("people");

			//--- 1
			var doc = new BsonDocument
			{
				{"name", "martin" }
			};
			doc.Add("age", 46);

			//Console.WriteLine(doc);

			//--- 2
			var person = new Person
			{
				Name = "Martin",
				Age = 46,
				Colors = new List<string> { "red", "blue" },
				Pets = new List<Pet> { new Pet { Name = "Casi", Type = "Dog" } },
				ExtraElements = new BsonDocument("anotherName", "anotherValue")
			};

			//using (var writer = new JsonWriter(Console.Out))
			//{
			//	BsonSerializer.Serialize(writer, person);
			//}

			//--- 3
			var filter = new BsonDocument();
			var list = await col.Find(filter).ToListAsync();

			foreach (var docu in list)
			{
				Console.WriteLine(docu);
			}
		}

		class Person
		{
			public ObjectId Id { get; set; }
			public string Name { get; set; }
			public int Age { get; set; }
			public List<string> Colors { get; set; }
			public List<Pet> Pets { get; set; }
			public BsonDocument ExtraElements { get; set; }
		}

		class Pet
		{
			public string Name { get; set; }
			public string Type { get; set; }
		}
	}
}
