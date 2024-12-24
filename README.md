# Credit Management System

The **Credit Management System** is a web-based application designed to manage customer data, credit scores, loans, and transactions for a credit management business. This project utilizes **ASP.NET Core MVC** with **Entity Framework Core** for database management.

---

## Table of Contents

- [Features](#features)
- [Technologies Used](#technologies-used)
- [Installation](#installation)
- [Database Setup](#database-setup)
- [Usage](#usage)
- [Screenshots](#screenshots)
- [Contributing](#contributing)
- [License](#license)

---

## Features

1. **Customer Management**
   - Add, view, edit, and delete customer information.
   - Unique constraints on email and identification numbers.

2. **Account Management**
   - Manage customer accounts with unique account numbers.
   - View transaction histories for individual accounts.

3. **Credit Score Management**
   - Assign credit scores to customers.
   - Maintain assessment history.

4. **Loan Management**
   - Manage loan applications and statuses.
   - Restrict deletion of loans linked to transactions.

5. **Transaction Tracking**
   - Record transactions linked to accounts or loans.
   - Restrict deletion of transactions to maintain data integrity.

6. **Responsive Design**
   - Designed with **Bootstrap 5** for responsiveness and usability.

---

## Technologies Used

- **Backend:**
  - ASP.NET Core MVC 6.0
  - Entity Framework Core

- **Frontend:**
  - Razor Views
  - Bootstrap 5
  - Font Awesome for icons

- **Database:**
  - Microsoft SQL Server

- **Tools:**
  - Visual Studio
  - Git for version control

---

## Installation

### Prerequisites

1. .NET 8 SDK installed ([Download](https://dotnet.microsoft.com/))
2. Microsoft SQL Server installed locally or remotely.
3. Visual Studio 2022 or higher.

### Steps

1. Clone this repository:
   ```bash
   git clone https://github.com/your-username/credit-management-system.git
   cd credit-management-system
   ```

2. Open the solution file (`CreditManagement.sln`) in Visual Studio.

3. Restore dependencies:
   ```bash
   dotnet restore
   ```

4. Update `appsettings.json` with your database connection string:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=CreditManagementDB;Trusted_Connection=True;"
   }
   ```

---

## Database Setup

1. Apply migrations to set up the database:
   ```bash
   dotnet ef database update
   ```

2. (Optional) Seed initial data by implementing seeding logic in `OnModelCreating`.

---

## Usage

1. Run the application:
   ```bash
   dotnet run
   ```

2. Navigate to `https://localhost:5001` (or the specified port).

3. Use the dashboard to:
   - Manage customers, accounts, loans, and transactions.
   - View detailed reports and perform CRUD operations.

---

## Screenshots

### Dashboard
![Dashboard Screenshot](https://via.placeholder.com/800x400)

### Customer Details
![Customer Details Screenshot](https://via.placeholder.com/800x400)

---

## Contributing

Contributions are welcome! Follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature-name`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/your-feature-name`).
5. Open a Pull Request.

---

## License

This project is licensed under the [MIT License](LICENSE).

---

## Acknowledgements

- **Bootstrap** for the responsive frontend framework.
- **Font Awesome** for the icons.
- **Microsoft** for the ASP.NET Core framework.