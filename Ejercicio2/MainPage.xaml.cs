using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ejercicio2
{
    public partial class MainPage : ContentPage
    {
        private const string ApiUrl = "https://jsonplaceholder.typicode.com/posts";

        private HttpClient _httpClient;
        private List<Post> _posts;

        public MainPage()
        {
            InitializeComponent();

            _httpClient = new HttpClient();
            _posts = new List<Post>();

            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                var json = await _httpClient.GetStringAsync(ApiUrl);
                _posts = JsonConvert.DeserializeObject<List<Post>>(json);
                listView.ItemsSource = _posts;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            int searchId;
            if (int.TryParse(searchEntry.Text, out searchId))
            {
                var post = _posts.Find(p => p.Id == searchId);
                if (post != null)
                {
                    listView.ItemsSource = new List<Post> { post };
                }
                else
                {
                    DisplayAlert("No encontrado", "No se encontró ningún elemento con ese ID.", "OK");
                }
            }
            else
            {
                DisplayAlert("Error", "Por favor, introduce un ID válido.", "OK");
            }
        }
        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            listView.ItemsSource = _posts;
            searchEntry.Text = string.Empty;
        }

    }

    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
