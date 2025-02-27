# Car Auction Management System

## Overview
This project is a **.NET API** developed using **Clean Architecture** principles. The API serves as a **cars auction management system**, allowing users to manage vehicles and auctions efficiently.

## Project Structure
The repository is organized using the following directory structure:

- **`/source`** - Contains the main application code, including domain, api, application, and infrastructure layers.
- **`/tests`** - Includes unit tests written with **xUnit**, ensuring the correctness of business logic and application behavior.

## Features
The API consists of these two primary services:

### Vehicle
- **`GET` Get Vehicle by ID** - Retrieve a specific vehicle by its ID.
- **`GET` Get All Vehicles** - Retrieve all available vehicles.
- **`POST` Search Vehicles** - Retrieve available vehicles based on filters.

  ```json
    {
        "type": "string",
        "manufacturer": "string",
        "model": "string",
        "year": 0
    }
- **`POST` Create Vehicle** - Register new vehicle in the system

  ```json
    {
    "type": "string",
    "manufacturer": "string",
    "model": "string",
    "year": 0,
    "startingBidInEuros": 0,
    "numberOfDoors": 0,
    "numberOfSeats": 0,
    "loadCapacityInKg": 0
    }

### Auction
- **`GET` Get Auction by ID** - Retrieve a specific auction by its ID.
- **`GET` Get all auction** - Retrieve all auctions.
- **`POST` Start Auction** - Initiate an auction for a specific vehicle by vehicle id.
- **`PUT` Stop Auction** - Close an ongoing auction by auction id.
- **`PUT` Place Bid** -Submit bid for vehicles in active auctions by auction id.

    ```json
        {
        "amountInEuros": 0
        }
        
## Validation
To ensure data integrity and request correctness, **FluentValidation** has been implemented for upfront request validation. 

## Technologies Used
- **.NET** (C#)
- **Clean Architecture**
- **xUnit** (Unit Testing)
- **FluentValidation** (Request Validation)

## Contributing

## Authors

* **Ricardo Pacheco** - *Development* - [ricardop17](https://github.com/ricardop17)
