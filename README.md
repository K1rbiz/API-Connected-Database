# API Connected Database – Cat Facts ??

This app demonstrates a full data flow:
- Fetch cat facts from the **Cat Facts API**
- Save them to a local SQLite database
- Reload and display them from the database

## API
**Endpoint:** https://catfact.ninja


## Usage
- When running the app, it fetches cat facts from the API.
- The fetched facts are stored in a local SQLite database.
- The app then retrieves and displays the stored cat facts.
- As of right now, the app fetches and displays as many as you specify.
- It ensures you are getting new facts each time by incrementing the page number with each fetch.
