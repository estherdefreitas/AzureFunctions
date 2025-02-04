# Azure Functions CPF Validator

This project is an Azure Function App that validates Brazilian CPF numbers.

## Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- [Visual Studio Code](https://code.visualstudio.com/)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)

## Setup

1. Clone the repository:
   ```sh
   git clone https://github.com/estherdefreitas/AzureFunctions.git
   cd AzureFunctions
   ```
2. Install the required .NET dependencies:
  ```sh
  dotnet restore
  ```
3. Run the Azure Function locally:
  ```sh
func start
  ```


## Usage
To validate a CPF number, send a POST request to the function endpoint with the CPF number within a JSON body.

Example:
  ```sh
 curl -X POST "http://localhost:7071/api/ValidateCpf" -H "Content-Type: application/json" -d "{\"cpf\":\"12345678909\"}"
  ```

Deployment
To deploy the function to Azure, use the following command:
  ```sh
 func azure functionapp publish <YourFunctionAppName>
  ```

Make sure you are logged in to Azure CLI before running the publish command:
```sh
az login
```

Contributing
Contributions are welcome! Please open an issue or submit a pull request for any changes.
