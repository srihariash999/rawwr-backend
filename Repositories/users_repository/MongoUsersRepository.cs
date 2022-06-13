// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Podboi.Api.Entities;
// using MongoDB.Bson;
// using MongoDB.Driver;
// using Podboi.Api.Repositories;
// using Catalog.Api.Dtos;

// namespace Podboi.Api.Repositories
// {
//     public class MongoDbItemsRepository : IUsersRepository
//     {
//         private const string databaseName = "podboi";
//         private const string collectionName = "users";
//         private readonly IMongoCollection<User> usersCollection;
//         private readonly FilterDefinitionBuilder<User> filterBuilder = Builders<User>.Filter;

//         public MongoDbItemsRepository(IMongoClient mongoClient)
//         {
//             IMongoDatabase database = mongoClient.GetDatabase(databaseName);
//             usersCollection = database.GetCollection<User>(collectionName);
//         }





//         // Method to get user by id from mongodb.
//         public async Task<User?> GetUser(Guid id)
//         {
//             var filter = filterBuilder.Eq(user => user.Id, id);
//             return await usersCollection.Find(filter).SingleOrDefaultAsync();
//         }



//         // Method to get List of users from MongoDb
//         public async Task<IEnumerable<User>> GetUsers()
//         {
//             return await usersCollection.Find(new BsonDocument()).ToListAsync();
//         }

//         // Method to get create new user in MongoDb
//         public async Task<Boolean> CreateUser(User user)
//         {

//             try
//             {
//                 await usersCollection.InsertOneAsync(user);
//                 return true;
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine(e);
//                 return false;
//             }

//         }

//         public async Task<bool> CheckExistingUser(string email)
//         {
//             var usersList = await usersCollection.Find(new BsonDocument()).ToListAsync();

//             if (usersList != null)
//             {
//                 foreach (var user in usersList)
//                 {
//                     if (user.Email == email)
//                     {
//                         return true;
//                     }
//                 }
//                 return false;
//             }

//             return true;
//         }

//         // public async Task DeleteItemAsync(Guid id)
//         // {
//         //     var filter = filterBuilder.Eq(item => item.Id, id);
//         //     await itemsCollection.DeleteOneAsync(filter);
//         // }





//         // public async Task UpdateItemAsync(Item item)
//         // {
//         //     var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
//         //     await itemsCollection.ReplaceOneAsync(filter, item);
//         // }
//     }
// }