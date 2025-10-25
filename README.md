# Inventory Management System

This is a simple inventory management system built with C# and WPF.
THe goal is to have a databse of items, their locations and a dashboard to view the status of items, maily food items.

Selecting diffrent locatoins like pantry, fridge, frezer, garage frezser etc, you are able to quickly and easily get a overview of what you have, what is about to expire or even opull out a list of what you need to buy or to make a certain recipy.

The end goal would be a rest api, support for a web ui, android app or desktop admin app.
easily scan a product once, and when adding new simpyl press the + button on the item, adding a new item with the default expirydate. allows you to update prices of the items and even calorie informatin if deisred.

Possibilioty of ai usage investigated

**Creation Date:** March 31, 2010

## Getting Started

To run this application, you will need to have Visual Studio installed.

1. Clone the repository to your local machine.
2. Open the `Inventory.sln` file in Visual Studio.
3. Press `F5` or click the "Start" button to build and run the application.

## Features
*   **Add New Items:** Add new items to the inventory with details like name, barcode, lifetime, price, and store.
*   **Add Food Items:** Add food items with a location, bought date, and expiration date.
*   **View Inventory:** The main window displays a list of all food items in the inventory.
*   **Data Persistence:** The application saves and loads inventory data from XML files, or to a database (`Items.xml` and `Foods.xml`).
*   **Overview:** An overview window that shows different locations.
*   **MVVM Architecture:** The application follows the Model-View-ViewModel (MVVM) design pattern.



Features

Functions&behaviours
Description
Checklist
barcode scanning
adding new food based on item
  eaisly add new items, or remove used items
Add new item v1
open window to put in information
select base item from list
get now date and option to fill in own date
calcuate expiration date

add location

save item
add new item to database
enter or scan code
enter name
enter price
enter unit of mesurement
enter store
enter life time

new food has an item, instead of inherit item
