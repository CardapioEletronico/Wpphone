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
using System.Windows.Media.Imaging;

namespace CardapioWinPhone
{
    public partial class Detalhes : PhoneApplicationPage
    {
        private string ip = "http://10.21.0.137";

        public Detalhes()
        {
            InitializeComponent();
        }
        string parameter = string.Empty;
        protected async  override void OnNavigatedTo(NavigationEventArgs e)
        {
            string nome = string.Empty;
            populate();
            if (NavigationContext.QueryString.TryGetValue("idind", out parameter))
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ip);
                var response = await httpClient.GetAsync("/20131011110061/api/produto/");

                var str = response.Content.ReadAsStringAsync().Result;
                List<Models.Produto> obj = JsonConvert.DeserializeObject<List<Models.Produto>>(str);
                foreach (var item in obj)
                {
                    if (item.Id == int.Parse(parameter))
                        nome = item.Descricao;
                }
            }
            
            Titulo.Text = nome;
        }

        /*private void ovo()
        {
           
            if (NavigationContext.QueryString.TryGetValue("idind", out parameter))
            {
                MessageBox.Show(parameter);
            }
        }*/

        private async void populate()
        {
            string parameter = string.Empty;

            if (NavigationContext.QueryString.TryGetValue("idind", out parameter))
            {
                //this.label.Text = parameter;
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ip);
                var response = await httpClient.GetAsync("/20131011110061/api/produto/");
                var str = response.Content.ReadAsStringAsync().Result;
                List<Models.Produto> obj = JsonConvert.DeserializeObject<List<Models.Produto>>(str);
                List<Models.Produto> lista = new List<Models.Produto>();
                foreach (var ota in obj)
                {
                    if (ota.Id == int.Parse(parameter))
                    {
                        TextBlock txtDesc = new TextBlock();
                        txtDesc.Text = "";
                        txtDesc.FontSize = 34;
                        txtDesc.Text += ota.NomeDescricao.ToString();
                        StackLola.Children.Add(txtDesc);

                        TextBlock txtNDesc = new TextBlock();
                        txtNDesc.Text = "Descrição: ";
                        txtNDesc.FontSize = 34;
                        txtNDesc.Text += ota.Descricao.ToString();
                        StackLola.Children.Add(txtNDesc);

                        TextBlock txtPreco = new TextBlock();
                        txtPreco.Text = "Preço: ";
                        txtPreco.FontSize = 34;
                        txtPreco.Text += ota.Preco.ToString();
                        StackLola.Children.Add(txtPreco);

                        Image Imagem = new Image();                        
                        Uri uri = new Uri(ota.Foto);
                        BitmapImage uhul = new BitmapImage();
                        uhul.UriSource = uri;
                        Imagem.Source = uhul;
                        Imagem.Width = 300;
                        Imagem.Height = 300;
                        StackLola.Children.Add(Imagem);

                        Button btn = new Button();
                        btn.Content = "Pedir";
                        StackLola.Children.Add(btn);
                    }
                }
            }
        }
    }
}