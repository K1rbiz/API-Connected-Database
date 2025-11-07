# API Connected Database – Cat Facts

A lightweight .NET MAUI Blazor Hybrid Application that demonstrates API integration, local data storage, and data flow in C#.
This app connects to a public REST API to fetch interesting cat facts, saves them locally, and displays them through a simple user interface.

This project was created for the API Integration Assignment. It demonstrates a complete data pipeline:
1. Fetch data from a remote REST API using HttpClient.
2. Deserialize JSON into strongly-typed C# model objects.
3. Use the Repository Pattern to save and retrieve data from a local SQLite database.
4. Inject dependencies using Dependency Injection (DI).
5. Display the locally stored data inside a Blazor-based UI.

# How It Works
When the user clicks “Fetch and Save Data”, the following sequence happens:
- The app calls the CatFactsService to fetch data from the public Cat Facts API.
- The service uses an incrementing page counter to fetch new facts each time (ensuring results aren’t repeated).
- The received list of facts is saved into the local SQLite database via the CatFactsRepository.
- The data is then retrieved from the database and displayed on-screen.
The entire process runs asynchronously, ensuring the UI stays responsive.

# Public API Used
- [Cat Facts API](https://catfact.ninja/): A free public API that provides cat facts.