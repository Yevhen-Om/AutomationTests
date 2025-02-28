# Automation Tests - NUnit

This repository contains automated tests using **NUnit**, **Selenium**, **RestSharp** and **Dependency Injection**. This guide will help you set up, configure, and run the tests.

## 📌 Prerequisites
Before running the tests, ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (Version 8)
- [Chrome Browser](https://www.google.com/chrome/)
- [ChromeDriver](https://sites.google.com/chromium.org/driver/) (Ensure the version matches your Chrome browser)
- NuGet Packages:
  - `NUnit`
  - `NUnit3TestAdapter`
  - `Microsoft.Extensions.DependencyInjection`
  - `Selenium.WebDriver`
  - `Selenium.WebDriver.ChromeDriver`
  - `NLog`
  - `RestSharp`

## 🚀 Setup Instructions

1. **Install dependencies**
   ```sh
   dotnet restore
   ```

2. **Ensure ChromeDriver is installed and set in the system path**
   - You can manually download ChromeDriver or install it via NuGet:
     ```sh
     dotnet add package Selenium.WebDriver.ChromeDriver
     ```

## 🛠 Running Tests

### 1️⃣ Run Tests Using .NET CLI
Execute the following command from the project root:
```sh
dotnet test
```

### 2️⃣ Run Tests Using Visual Studio
- Open the solution in **Visual Studio**
- Open the **Test Explorer** (`Test > Test Explorer`)
- Click **Run All**

### 3️⃣ Run Specific Tests
To run a specific test:
```sh
dotnet test --filter "FullyQualifiedName=Namespace.ClassName.MethodName"
```

## 🏗 Test Structure

- `DependencyRegister.cs`: Handles **dependency injection** for API Services, and Logging.
- `APITests/`: Contains API test cases.
- `UITests/`: Contains UI tests using Selenium WebDriver.
- `TestContext.CurrentContext.WorkDirectory`: Used to manage test-related files.

## 🛑 Cleaning Up Resources
After tests, WebDriver instances should be closed properly.

- The `DependencyRegister` class includes a `OneTimeTearDown` method:
  ```csharp
  [OneTimeTearDown]
  public static void Cleanup()
  {
      _serviceProvider?.Dispose();
  }
  ```

## 📝 Logging
Logging is configured using **NLog**.
- The configuration file is `NLog.config`
- Logs are stored in `logs/` directory.

## ❓ Troubleshooting

- **Issue: `ChromeDriver not found`**
  - Ensure ChromeDriver is installed and matches your Chrome version.
  - Add it to your system `PATH`.

- **Issue: `System.ArgumentNullException: Value cannot be null. (Parameter 'provider')`**
  - Check if `DependencyRegister.RegisterServices()` is running properly.
  - Ensure `NLog.config` exists in `bin/Debug/netX.Y/`.

## 📌 Additional Notes
- Test cases support **parameterization** using `[TestCase]` attributes.
- Tests use `TestContext.CurrentContext.WorkDirectory` for managing test files.
- To run tests in **parallel**, update the NUnit configuration.