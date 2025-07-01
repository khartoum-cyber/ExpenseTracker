# ExpenseTracker

A simple command-line based Expense Tracker application written in C#. This tool allows users to manage their expenses by adding, listing, updating, deleting, and summarizing expense records.

## Project Overview

This application provides a text-based interface for tracking personal or organizational expenses. It supports various commands to interact with the expense data and is designed with modularity and extensibility in mind.

Project Task URL : https://roadmap.sh/projects/expense-tracker

## Features

- Add new expenses with category, amount, and description
- List all recorded expenses
- Update or delete existing expenses
- Summarize total expenses or monthly expenses
- Filter expenses by category
- Clear console and display help information

## Getting Started

To run the application:

1. Clone the repository
2. Build the project using your preferred C# IDE or .NET CLI
3. Run the application

## Usage

Once the application is running, you will be prompted to enter commands. Use the `help` command to see a list of available commands.

## Available Commands

- `add <category> <amount> <description>`: Add a new expense
- `list`: List all expenses
- `update <category> <id> <amount>`: Update an existing expense
- `delete <id>`: Delete an expense by ID
- `sum-all`: Show the total sum of all expenses
- `sum-month <month>`: Show the total expenses for a specific month
- `show-category <category>`: Show expenses filtered by category
- `clear`: Clear the console screen
- `help`: Display help information
- `exit`: Exit the application

## License

This project is licensed under the MIT License.
