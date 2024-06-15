using AppMobilenBlog.Services.Abstract;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using Xamarin.Essentials;

namespace AppMobilenBlog.Services
{
    public class LoginService : ADataStore, ILoginService
    {
        public LoginService() : base()
        {
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            Debug.WriteLine($"Login attempt: Email={email}, Password={password}");

            var loginData = new { Email = email, Password = password };
            var jsonContent = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                Debug.WriteLine("Sending request to API...");
                var response = await HttpClient.PostAsync("https://localhost:7056/api/login", content);

                Debug.WriteLine($"Response status code: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

                    // Zapisz UserId i UserName w preferencjach
                    Preferences.Set("UserId", loginResponse.UserId);
                    Preferences.Set("UserName", loginResponse.UserName); // Dodanie zapisu UserName

                    Debug.WriteLine("Login successful");
                    return true;
                }
                else
                {
                    Debug.WriteLine($"Login failed: {response.ReasonPhrase}");
                    return false;
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"Request error: {e.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }

    public class LoginResponse
    {
        public string Message { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } // Dodanie UserName do odpowiedzi
    }
}
