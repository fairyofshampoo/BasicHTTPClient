using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace BasicHTTPClient
{
    public partial class MainWindow : Window
    {
        public HttpContent httpContent;

        public MainWindow()
        {
            InitializeComponent();
            btnDownload.Visibility = Visibility.Collapsed;
        }

        private void BtnConsult_Click(object sender, RoutedEventArgs e)
        {
            lblResponse.Content = "Consultando...";
            string url = txtURL.Text;
            if (ValidateURL(url))
            {
                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    url = "http://" + url;
                }

                ConsultMethod(url);
            } else
            {
                MessageBox.Show("URL is not valid", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool ValidateURL(string url)
        {
            bool isValidUri = true;

            if (string.IsNullOrWhiteSpace(url))
            {
                isValidUri = false;
            }

            return isValidUri;
        }


        private void ConsultMethod(string url)
        {

            string method = GetHttpMethod();

            switch (method)
            {
                case "GET":
                    ConsultGetMethod(url);
                    break;
                case "POST":
                    ConsultPostMethod(url);
                    break;
                case "PUT":
                    ConsultPutMethod(url);
                    break;
                case "DELETE":
                    ConsultDeleteMethod(url);
                    break;
                case "HEAD":
                    ConsultHeadMethod(url);
                    break;
                case "OPTIONS":
                    ConsultOptionsMethod(url);
                    break;
                default:
                    MessageBox.Show("Method not supported");
                    break;
            }
        }

        private void ConsultDeleteMethod(string url)
        {
            System.Net.Http.HttpResponseMessage response = ClientService.DeleteURL(url);
            if (response != null)
            {
                HttpContentHeaders content = null;
                if (response.Content != null)
                {
                    content = response.Content.Headers;
                }

                SetHeader(response.Headers, content);
                SetResponseCode(response.StatusCode);
            }
        }

        private void ConsultPutMethod(string url)
        {
            System.Net.Http.HttpResponseMessage response = ClientService.PutURL(url);
            if (response != null)
            {
                HttpContentHeaders content = null;
                if (response.Content != null)
                {
                    content = response.Content.Headers;
                }
                SetHeader(response.Headers, content);
                SetResponseCode(response.StatusCode);
            }
        }

        private void ConsultPostMethod(string url)
        {
            System.Net.Http.HttpResponseMessage response = ClientService.PostURL(url);
            if (response != null)
            {
                HttpContentHeaders content = null;
                if (response.Content != null)
                {
                    content = response.Content.Headers;
                }
                SetHeader(response.Headers, content);
                SetResponseCode(response.StatusCode);
            }
        }

        private void ConsultOptionsMethod(string url)
        {
            System.Net.Http.HttpResponseMessage response = ClientService.OptionsURL(url);
            if (response != null)
            {
                HttpContentHeaders content = null;
                if (response.Content != null)
                {
                    content = response.Content.Headers;
                }
                SetHeader(response.Headers, content);
                SetResponseCode(response.StatusCode);
            }
        }

        private async void ConsultHeadMethod(string url)
        {
            System.Net.Http.HttpResponseMessage response = await ClientService.HeadURL(url);
            if (response != null)
            {
                HttpContentHeaders content = null;
                if (response.Content != null)
                {
                    content = response.Content.Headers;
                }
                SetHeader(response.Headers, content);
                SetResponseCode(response.StatusCode);
            }

        }

        private async void ConsultGetMethod(string url)
        {
            System.Net.Http.HttpResponseMessage response = await ClientService.GetURL(url);
            if (response != null)
            {
                HttpContentHeaders content = null;
                if (response.Content != null)
                {
                    content = response.Content.Headers;
                }
                SetHeader(response.Headers, content);
                SetContent(response.Content);
                SetResponseCode(response.StatusCode);
            }
        }

        private void SetResponseCode(HttpStatusCode statusCode)
        {
            lblResponse.Content = (int)statusCode + ": " + statusCode;
        }

        private void SetContent(HttpContent content)
        {
            httpContent = content;
            lblContentType.Content = GetContentType();

            if (rbPretty.IsChecked == true)
            {
                SetPrettyContent();
            }
            else if (rbRaw.IsChecked == true)
            {
                SetRawContent();
            }

            btnDownload.Visibility = Visibility.Visible;
        }

        private async void SetRawContent()
        {
            string htmlContent = await httpContent.ReadAsStringAsync();
            txtbxBody.Text = htmlContent;
            txtbxBody.Visibility = Visibility.Visible;
            webBrowser.Visibility = Visibility.Collapsed;
        }

        private object GetContentType()
        {
            var contentType = httpContent.Headers.ContentType?.MediaType;
            return contentType ?? "No Content Type";
        }

        private void SetHeader(HttpResponseHeaders headers, HttpContentHeaders contentHeaders)
        {
            string headersString = string.Empty;
            string contentHeadersString = string.Empty;

            if (contentHeaders != null)
            {
                contentHeadersString = contentHeaders.ToString();
            }

            if(headers != null)
            {
                headersString = headers.ToString();
            }


            txtbxHeader.Text = "****** Generales y de Respuesta******\n" + headersString + "****** De entidad******\n" + contentHeadersString;
        }

        private string GetHttpMethod()
        {
            string method = string.Empty;

            var selectedItem = cbHTTPMethod.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                method = selectedItem.Content.ToString();
            }

            return method;
        }

        private void SetPrettyContent()
        {
            if (httpContent != null)
            {
                string contentType = GetContentType().ToString();

                switch (contentType)
                {
                    case "text/html":
                        ShowHTML();
                        break;
                    case "application/json":
                        ShowJson();
                        break;
                    case "image/png":
                        ShowImage();
                        break;
                    case "image/jpeg":
                        ShowImage();
                        break;
                    case "image/gif":
                        ShowImage();
                        break;
                    default:
                        txtbxBody.Text = "No se puede mostrar este formato, descargálo para visualizarlo";
                        txtbxBody.Visibility = Visibility.Visible;
                        webBrowser.Visibility = Visibility.Collapsed;
                        break;

                }

            }
        }

        private async void ShowJson()
        {
            string jsonContent = await httpContent.ReadAsStringAsync();
            txtbxBody.Text = jsonContent;
            txtbxBody.Visibility = Visibility.Visible;
            webBrowser.Visibility = Visibility.Collapsed;
        }

        private async void ShowHTML()
        {
            string htmlContent = await httpContent.ReadAsStringAsync();
            txtbxBody.Text = htmlContent;
            webBrowser.NavigateToString(htmlContent);
            webBrowser.Visibility = Visibility.Visible;
            txtbxBody.Visibility = Visibility.Collapsed;
        }

        private async void ShowImage()
        {
            byte[] imageContent = await httpContent.ReadAsByteArrayAsync();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new System.IO.MemoryStream(imageContent);
            bitmapImage.EndInit();
            imgContent.Source = bitmapImage;
            imgContent.Visibility = Visibility.Visible;
            txtbxBody.Visibility = Visibility.Collapsed;
        }

        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            SaveContent();
        }

        private async void SaveContent()
        {
            string mimeType = lblContentType.Content.ToString();
            string extension = GetExtension(mimeType);

            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = $"Archivos {extension}|*{extension}|Todos los archivos (*.*)|*.*",
                FileName = $"response{extension}"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                if (extension == ".txt" || extension == ".html" || extension == ".json")
                {
                    string responseBody = txtbxBody.Text;

                    if (string.IsNullOrEmpty(responseBody))
                    {
                        MessageBox.Show("No hay respuesta para guardar.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    System.IO.File.WriteAllText(saveFileDialog.FileName, responseBody);
                }
                else
                {
                    HttpClient httpClient = new HttpClient();
                    var byteArray = await httpClient.GetByteArrayAsync(txtURL.Text);
                    System.IO.File.WriteAllBytes(saveFileDialog.FileName, byteArray);
                }

                MessageBox.Show($"Respuesta guardada en: {saveFileDialog.FileName}", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private string GetExtension(string mimeType)
        {
            switch (mimeType)
            {
                case "text/html":
                    return ".html";
                case "application/json":
                    return ".json";
                case "application/pdf":
                    return ".pdf";
                case "image/jpeg":
                    return ".jpeg";
                case "image/png":
                    return ".png";
                case "image/gif":
                    return ".gif";
                case "text/plain":
                default:
                    return ".txt";
            }
        }
    }
}
