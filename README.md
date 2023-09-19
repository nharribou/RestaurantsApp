# Restaurants API

The Restaurants API is designed to manage and explore restaurant data. It provides a range of features for retrieving, updating, and searching restaurant information, making it easy to integrate restaurant-related data into your applications.

## Endpoints

### Get All Restaurants

Retrieve a list of all restaurants.

- **Endpoint:** `/api/Restaurants`
- **HTTP Method:** GET

### Get Restaurant by ID

Retrieve a restaurant by its unique ID.

- **Endpoint:** `/api/Restaurants/{id}`
- **HTTP Method:** GET
- **Parameters:**
  - `id` (int): The ID of the restaurant to retrieve.

### Search Restaurants by Name

Search for restaurants by their names, returning a list of matching restaurants.

- **Endpoint:** `/api/Restaurants/SearchByName`
- **HTTP Method:** GET
- **Parameters:**
  - `name` (string): The name or part of the name to search for.

### Get Top-Rated Restaurants

Retrieve a list of top-rated restaurants, ordered by their ratings in descending order.

- **Endpoint:** `/api/Restaurants/TopRated`
- **HTTP Method:** GET

### Get Restaurants by Category

Retrieve a list of restaurants belonging to a specific category.

- **Endpoint:** `/api/Restaurants/ByCategory/{categoryId}`
- **HTTP Method:** GET
- **Parameters:**
  - `categoryId` (int): The ID of the category to filter by.

### Update Restaurant

Update an existing restaurant's information.

- **Endpoint:** `/api/Restaurants/{id}`
- **HTTP Method:** PUT
- **Parameters:**
  - `id` (int): The ID of the restaurant to update.
- **Request Body:** JSON object representing the updated restaurant data.

### Create New Restaurant

Create a new restaurant entry in the database.

- **Endpoint:** `/api/Restaurants`
- **HTTP Method:** POST
- **Request Body:** JSON object representing the new restaurant data.

### Delete Restaurant

Delete a restaurant by its unique ID.

- **Endpoint:** `/api/Restaurants/{id}`
- **HTTP Method:** DELETE
- **Parameters:**
  - `id` (int): The ID of the restaurant to delete.

## Categories API

This API also includes endpoints for managing restaurant categories:

### Get All Categories

Retrieve a list of all restaurant categories.

- **Endpoint:** `/api/Categories`
- **HTTP Method:** GET

### Get Category by ID

Retrieve a category by its unique ID.

- **Endpoint:** `/api/Categories/{id}`
- **HTTP Method:** GET
- **Parameters:**
  - `id` (int): The ID of the category to retrieve.

### Update Category

Update an existing category's information.

- **Endpoint:** `/api/Categories/{id}`
- **HTTP Method:** PUT
- **Parameters:**
  - `id` (int): The ID of the category to update.
- **Request Body:** JSON object representing the updated category data.

### Create New Category

Create a new category entry in the database.

- **Endpoint:** `/api/Categories`
- **HTTP Method:** POST
- **Request Body:** JSON object representing the new category data.

### Delete Category

Delete a category by its unique ID.

- **Endpoint:** `/api/Categories/{id}`
- **HTTP Method:** DELETE
- **Parameters:**
  - `id` (int): The ID of the category to delete.

## Prerequisites

Before using this API, ensure that you have the necessary setup and dependencies in place.

- .NET SDK 6.0
- MySQL Server

## Getting Started

Follow these steps to get started with the Restaurants API:

1. Clone this repository to your local machine.
2. Configure the connection string in the `appsettings.json` file to point to your MySQL database:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Port=3306;Database=your_database_name;User=your_username;Password=your_password;"
   }

Follow these steps to get started with the Restaurants API:

1. Clone this repository to your local machine.
2. Configure the connection string in the `appsettings.json` file to point to your database.
3. Build and run the application.
4. Run the migration with the command : dotnet ef database update

## Technologies Used

- .NET Core 6.0
- Entity Framework Core
- Swagger for API documentation

## Author

HARRIBOU Najib : https://github.com/nharribou
AGUDO ANTONIO Francisco : https://github.com/francisco-aa


## License

This project is licensed under the [MIT License](LICENSE).


