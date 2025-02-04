using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace httpValidaCpf
{
    public static class fnvalidacpf
    {
        [FunctionName("fnvalidacpf")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Validando CPF");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            if(data == null)
            {
                return new BadRequestObjectResult("Por favor, informe um CPF");
            }
            string cpf = data?.cpf;

            if (cpf.Length != 11)
            {
                return new BadRequestObjectResult("O CPF deve conter 11 dígitos.");
            }
            
            string responseMessage = string.IsNullOrEmpty(cpf)
                ? "Essa função funcionou corretamente, mas nenhum cpf foi informado. Por favor, informe um CPF."
                : $"CPF: {(ValidaCpf(cpf) ? "CPF válido":"CPF inválido")}.";

            return new OkObjectResult(responseMessage);
        }
        public static bool ValidaCpf(string cpf)
        {
            int[] v = new int[11];
            int j, i, soma = 0, dig1, dig2;
            bool igual = true;

            for (i = 0; i < 11; i++)
            {
                v[i] = int.Parse(cpf[i].ToString());
                if (i > 0 && v[i] != v[i - 1])
                {
                    igual = false;
                }
            }

            if (igual)
            {
                return false;
            }

            for (i = 0, j = 10; i < 9; i++, j--)
            {
                soma += v[i] * j;
            }

            dig1 = soma % 11;
            if (dig1 < 2)
            {
                dig1 = 0;
            }
            else
            {
                dig1 = 11 - dig1;
            }

            soma = 0;
            for (i = 0, j = 11; i < 10; i++, j--)
            {
                soma += v[i] * j;
            }

            dig2 = soma % 11;
            if (dig2 < 2)
            {
                dig2 = 0;
            }
            else
            {
                dig2 = 11 - dig2;
            }

            return v[9] == dig1 && v[10] == dig2;
        }
    }
}
