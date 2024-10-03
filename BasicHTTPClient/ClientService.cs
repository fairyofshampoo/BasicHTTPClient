using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows;

namespace BasicHTTPClient
{
    public class ClientService
    {
        public static async Task<HttpResponseMessage> GetURL(string url)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var httpRequestMessage = new HttpRequestMessage()
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(url)
                    };

                    HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);

                    if(httpResponseMessage != null && httpResponseMessage.IsSuccessStatusCode)
                    {
                        result = httpResponseMessage;
                    }
                } catch (HttpRequestException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return result;
        }

        public static async Task<HttpResponseMessage> HeadURL(string url)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var httpRequestMessage = new HttpRequestMessage()
                    {
                        Method = HttpMethod.Head,
                        RequestUri = new Uri(url)
                    };

                    HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);

                    if (httpResponseMessage != null && httpResponseMessage.IsSuccessStatusCode)
                    {
                        result = httpResponseMessage;
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            return result;
        }

        public static HttpResponseMessage OptionsURL(string url)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var httpRequestMessage = new HttpRequestMessage()
                    {
                        Method = HttpMethod.Options,
                        RequestUri = new Uri(url)
                    };

                    HttpResponseMessage httpResponseMessage = client.SendAsync(httpRequestMessage).Result;

                    if (httpResponseMessage != null && httpResponseMessage.IsSuccessStatusCode)
                    {
                        result = httpResponseMessage;
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            return result;
        }

        public static HttpResponseMessage DeleteURL(string url)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var httpRequestMessage = new HttpRequestMessage()
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(url)
                    };

                    HttpResponseMessage httpResponseMessage = client.SendAsync(httpRequestMessage).Result;

                    if (httpResponseMessage != null && httpResponseMessage.IsSuccessStatusCode)
                    {
                        result = httpResponseMessage;
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            };

            return result;
        }

        public static HttpResponseMessage PostURL(string url)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var httpRequestMessage = new HttpRequestMessage()
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(url)
                    };

                    HttpResponseMessage httpResponseMessage = client.SendAsync(httpRequestMessage).Result;

                    if (httpResponseMessage != null && httpResponseMessage.IsSuccessStatusCode)
                    {
                        result = httpResponseMessage;
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            return result;
        }

        public static HttpResponseMessage PutURL(string url)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var httpRequestMessage = new HttpRequestMessage()
                    {
                        Method = HttpMethod.Put,
                        RequestUri = new Uri(url)
                    };

                    HttpResponseMessage httpResponseMessage = client.SendAsync(httpRequestMessage).Result;

                    if (httpResponseMessage != null && httpResponseMessage.IsSuccessStatusCode)
                    {
                        result = httpResponseMessage;
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return result;
        }
    }
}
