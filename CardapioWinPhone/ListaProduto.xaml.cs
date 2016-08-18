using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Net.Http;
using Newtonsoft.Json;

namespace CardapioWinPhone
{
    public partial class ListaProduto : PhoneApplicationPage
    {
        //private Models.Produto produtinho;
        private string ip = "http://10.21.0.137";
        public ListaProduto()
        {
            InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            string nome = string.Empty;
            string parameter = string.Empty;

            if (NavigationContext.QueryString.TryGetValue("idprod", out parameter))
            {
                //this.label.Text = parameter;
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ip);
                var response = await httpClient.GetAsync("/20131011110061/api/cardapio/");

                var str = response.Content.ReadAsStringAsync().Result;
                List<Models.Cardapio> obj = JsonConvert.DeserializeObject<List<Models.Cardapio>>(str);
                foreach (var item in obj)
                {
                    if(item.Id == int.Parse(parameter))
                    nome = item.Descricao;
                }

            }

            populate();
            Titulo.Text = nome;
        }

        private async void populate()
        {
            string parameter = string.Empty;

            if (NavigationContext.QueryString.TryGetValue("idprod", out parameter))
            {
                //this.label.Text = parameter;
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ip);
                var response = await httpClient.GetAsync("/20131011110061/api/produto/");

                var str = response.Content.ReadAsStringAsync().Result;
                List<Models.Produto> obj = JsonConvert.DeserializeObject<List<Models.Produto>>(str);
                List<Models.Produto> lista = new List<Models.Produto>();
                foreach (var x in obj)
                {
                    if(x.Cardapio_Id == int.Parse(parameter))
                    {
                        lista.Add(x);
                    }
                }

                ProdutosLista.ItemsSource = null;
                ProdutosLista.ItemsSource = lista;
            }
        }
       
        private async void ProdutosLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110061/api/cardapio");
            int b = (ProdutosLista.SelectedItem as Models.Produto).Id;
            ListaProduto Lproduto = new ListaProduto();
            NavigationService.Navigate(new Uri("/Detalhes.xaml?idind=" + b, UriKind.Relative));
        }
    }
}

