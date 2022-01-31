using CSVConsole.DTOs;
using System.Text;

Console.WriteLine("Hello, World!");

using (HttpClient client = new HttpClient())
{
    String year = DateTime.Now.ToString("yyyy");
    String month = DateTime.Now.ToString("MM");
    String day = DateTime.Now.ToString("dd");

    using (var httpRequestHolidays = new HttpRequestMessage(HttpMethod.Get, $"https://apis.digital.gob.cl/fl/feriados/{year}/{month}/{day}"))
    {
        var response = await client.SendAsync(httpRequestHolidays).ConfigureAwait(false);
        var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        try
        {
            var responseDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(responseBody);

            if (responseDTO != null && responseDTO.error)
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://biclabs.eastus.cloudapp.azure.com/v1/ActivationRequest/CreateCSV"))
                {
                    requestMessage.Headers.Add("ApiKey", "51ac497a-a2fb-497b-be90-6eb14923f201");

                    response = await client.SendAsync(requestMessage);
                    responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    responseDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(responseBody);

                    if (responseDTO != null)
                    {
                        using (var httpRequestLog = new HttpRequestMessage(HttpMethod.Post, "https://biclabs.eastus.cloudapp.azure.com/v1/ProcessLog"))
                        {
                            httpRequestLog.Headers.Add("ApiKey", "51ac497a-a2fb-497b-be90-6eb14923f201");
                            

                            ProcessLogDTO processLogDTO = new ProcessLogDTO();
                            processLogDTO.Error = !responseDTO.Succeeded;
                            processLogDTO.Message = responseDTO.message;
                            processLogDTO.ProcessDate = DateTime.Now;
                            processLogDTO.Name = "CSV Process";

                            var json = Newtonsoft.Json.JsonConvert.SerializeObject(processLogDTO);
                            using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                            {
                                httpRequestLog.Content = stringContent;

                                using (var responseLog = await client
                                    .SendAsync(httpRequestLog)
                                    .ConfigureAwait(false))
                                {
                                    responseLog.EnsureSuccessStatusCode();
                                }
                            }                            
                        }
                    }

                    

                    Console.WriteLine("Proceso ejecutado correctamente");
                }
            }
        }
        catch (Exception er)
        {
            Console.WriteLine("Proceso no se ejecuta en feriados");
        }
        
    }
}