using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CardapioWinPhone.Resources;
using System.Net.Http;
using Newtonsoft.Json;

namespace CardapioWinPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string ip = "http://10.21.0.137/";
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Load();

        }

        public async void Load()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("20131011110061/api/cardapio");
            if (response != null) { 
            var str = response.Content.ReadAsStringAsync().Result;
            List<Models.Cardapio> obj = JsonConvert.DeserializeObject<List<Models.Cardapio>>(str);

            CardapiosLista.ItemsSource = null;
            CardapiosLista.ItemsSource = obj;
            }

            else
            {
                MessageBox.Show("Algum problema aconteceu. Chame o Garçom mais próximo");
            }
        }
  

    private async void txt_Click(object sender, RoutedEventArgs e)
        {

            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110061/api/cardapio");
            Button b = sender as Button;
            ListaProduto Lproduto = new ListaProduto();
            NavigationService.Navigate(new Uri("/ListaProduto.xaml?idprod=" + b.CommandParameter, UriKind.Relative));

        }

        private async void CardapiosLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = (CardapiosLista.SelectedItem as Models.Cardapio).Id;

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110061/api/cardapio");
            NavigationService.Navigate(new Uri("/ListaProduto.xaml?idprod=" + id, UriKind.Relative));

        }
    }
}