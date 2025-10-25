# Inventory Management System Improvement Plan

This document outlines potential issues, improvements, and suggestions for the Inventory Management System application.

## 1. Correct the expiration date calculation

*   **Issue:** The expiration date for food items is calculated incorrectly in `MainWindow.xaml.cs`. The code `food.ExpirationDate = food.BoughtDate.AddDays(-food.Item.LifeTime);` subtracts the item's lifetime from the bought date, resulting in an expiration date in the past.
*   **Suggestion:** Change the line to `food.ExpirationDate = food.BoughtDate.AddDays(food.Item.LifeTime);` to correctly calculate the expiration date.

## 2. Fix the data loading logic

*   **Issue:** In `MainWindow.xaml.cs`, the `LoadFile()` method reads data from "Items.xml" and "Foods.xml", but the loaded data is never assigned to the `Items` and `Foods` `ObservableCollection`s. This means that an empty list is always displayed when the application starts.
*   **Suggestion:** Assign the loaded data to the `Items` and `Foods` collections, like this: `Items = new ObservableCollection<Item>(itemsToLoad);` and `Foods = new ObservableCollection<Food>(foodsToLoad);`.

## 3. Remove hardcoded test data

*   **Issue:** The `MainWindow` constructor contains hardcoded test data that creates a "bread" item and adds it to the `Foods` collection multiple times. This is not ideal for a production application.
*   **Suggestion:** Remove the hardcoded test data from the `MainWindow` constructor.

## 4. Refactor to adhere to MVVM

*   **Issue:** The `MainWindow.xaml.cs` file contains a significant amount of logic that should be in a ViewModel, which violates the principles of the MVVM pattern. This makes the code harder to test and maintain.
*   **Suggestion:** Create a `MainViewModel` class and move the `Items` and `Foods` collections, `LoadFile()`, `SaveFile()`, and `OnPropertyChanged()` implementation from `MainWindow.xaml.cs` to the new `MainViewModel`. Then, set the `DataContext` of the `MainWindow` to an instance of the `MainViewModel`.

## 5. Add error handling

*   **Issue:** The `LoadFile()` and `SaveFile()` methods in `MainWindow.xaml.cs` do not have any error handling. If an `IOException` or an `XmlSerialization` exception occurs, the application will crash.
*   **Suggestion:** Implement `try-catch` blocks in the `LoadFile()` and `SaveFile()` methods to handle potential exceptions.

## 6. Remove the static `AppWindow`

*   **Issue:** The `MainWindow` class has a static `AppWindow` property, which creates a tight coupling between the `MainWindow` and other parts of the application. This makes the code harder to test and reuse.
*   **Suggestion:** Eliminate the static `AppWindow` property and use a more appropriate communication mechanism, such as dependency injection, an event aggregator, or passing references.

## 7. Address the commented-out database code

*   **Issue:** The `MainWindow` constructor contains a block of commented-out code related to Entity Framework. This code is not used and adds clutter to the codebase.
*   **Suggestion:** Remove the commented-out Entity Framework code from the `MainWindow` constructor.

## 8. Improve file path handling

*   **Issue:** The `LoadFile()` and `SaveFile()` methods use hardcoded file paths ("Items.xml" and "Foods.xml"). This is not a robust way to handle file paths, as it assumes that the files will always be in the same directory as the executable.
*   **Suggestion:** Modify the `LoadFile()` and `SaveFile()` methods to use a more robust way of determining the file paths, such as using `Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)`.
