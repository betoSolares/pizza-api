using MongoDB.Driver;
using pizza_api.Models;
using System.Collections.Generic;

namespace pizza_api.Services
{
    public class PizzaService
    {
        private readonly IMongoCollection<Pizza> pizzaCollection;

        /// <summary>Constructor</summary>
        /// <param name="settings">The settings of the database</param>
        public PizzaService(IPizzasDatabaseSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);
            pizzaCollection = database.GetCollection<Pizza>(settings.CollectionName);
        }

        /// <summary>Get all the pizzas</summary>
        /// <returns>Return all the pizzas in a collection</returns>
        public List<Pizza> Get() => pizzaCollection.Find(p => true).ToList();

        /// <summary>Get an specific pizza</summary>
        /// <param name="name">The name of the pizza</param>
        /// <returns>The pizza with the specific name</returns>
        public Pizza Get(string name) => pizzaCollection.Find(p => p.Name == name).FirstOrDefault();

        /// <summary>Add a new pizza in the collection</summary>
        /// <param name="pizza">The pizza to add</param>
        /// <returns>The pizza that was added</returns>
        public Pizza Create(Pizza pizza)
        {
            pizzaCollection.InsertOne(pizza);
            return pizza;
        }

        /// <summary>Update the information of an existing pizza</summary>
        /// <param name="name">The name of the pizza to update</param>
        /// <param name="newPizza">The new information for the pizza</param>
        public Pizza Update(string name, Pizza newPizza)
        {
            pizzaCollection.ReplaceOne(p => p.Name == name, newPizza);
            return newPizza;
        }

        /// <summary>Remove an existing pizza from the collection</summary>
        /// <param name="name">The name of the pizza to remove</param>
        public void Delete(string name) => pizzaCollection.DeleteOne(p => p.Name == name);
    }
}