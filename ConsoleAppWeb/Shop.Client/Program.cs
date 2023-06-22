// See https://aka.ms/new-console-template for more information

using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

Console.WriteLine("Hello, World!");

// Get the Token acquirer factory instance. By default it reads an appsettings.json
// file if it exists in the same folder as the app (make sure that the
// "Copy to Output Directory" property of the appsettings.json file is "Copy if newer").
var tokenAcquirerFactory = TokenAcquirerFactory.GetDefaultInstance();

var services = tokenAcquirerFactory.Services;
services.AddMicrosoftGraph();

tokenAcquirerFactory.Build();

// why is this here? ->
tokenAcquirerFactory = TokenAcquirerFactory.GetDefaultInstance();

var acquirer = tokenAcquirerFactory.GetTokenAcquirer();
var tokenResult = await acquirer.GetTokenForAppAsync("https://youtube-example-2.app.innostep-it.com/.default");
var accessToken = tokenResult.AccessToken;

Console.WriteLine("acquired new access token: " + accessToken);


var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

var response = await httpClient.GetAsync("https://localhost:7095/api/Products");
if (response.IsSuccessStatusCode)
{
    Console.WriteLine(await response.Content.ReadAsStringAsync());
}
else
{
    Console.WriteLine("Error: " + response.StatusCode);
}
