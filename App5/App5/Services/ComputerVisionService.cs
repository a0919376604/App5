using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CognitiveServices.Models;
using CognitiveServices.Models.Image;
using CognitiveServices.Models.Ocr;
using ComputerVisionApplication.Models;
using Newtonsoft.Json;
using System.Threading;
using System.Diagnostics;
using App5;
using App5.ViewModels;
using ModernHttpClient;

namespace CognitiveServices.Services
{
    /// <summary>
    /// Client for Computer Vision API (Microsoft Cognitive Services).
    /// </summary>
    public class ComputerVisionService
    {

        /// <summary>
        /// Get a subscription key from:
        /// https://www.microsoft.com/cognitive-services/en-us/subscriptions
        /// </summary>
        private readonly string _key = "39b53082123648f4b0595d54b21f5718";

        /// <summary>
        /// Documentation for the API: https://www.microsoft.com/cognitive-services/en-us/computer-vision-api
        /// </summary>
        private readonly string _analyseImageUri = "https://api.projectoxford.ai/vision/v1.0/analyze?" + "visualFeatures=Description,Categories,Tags,Faces,ImageType,Color,Adult&details=Celebrities";
        private StreamContent streamTmpContent;
        private readonly string _extractTextUri = "https://api.projectoxford.ai/vision/v1.0/ocr?" + "language=unk&detectOrientation=true";
/*
        public static HttpClientHandler handler = new HttpClientHandler();
        public static HttpClient httpClient1 = new HttpClient(handler,false);*/
        /// <summary>
        /// Get a subscription key from:
        /// https://www.microsoft.com/cognitive-services/en-us/subscriptions
        /// </summary>
        /// <param name="key">subscription key: required to access the API</param>
        public ComputerVisionService(string key)
        {
            _key = key;
        }
        public class CustomHandler1 : DelegatingHandler
        {
            // Constructors and other code here.
            protected async override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request, CancellationToken cancellationToken)
            {
                // Process the HttpRequestMessage object here.
                Debug.WriteLine("Processing request in Custom Handler 1");

                // Once processing is done, call DelegatingHandler.SendAsync to pass it on the
                // inner handler.
                HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

                // Process the incoming HttpResponseMessage object here.
                Debug.WriteLine("Processing response in Custom Handler 1");

                return response;
            }
        }


        /// <summary>
        /// This operation extracts a rich set of visual features based on the image content. 
        /// </summary>
        /// <param name="stream">The image to be uploaded.</param>
        /// <returns></returns>
        public async Task<ImageResult> AnalyseImageStreamAsync(Stream stream)
        {
            var httpClient = new HttpClient(new NativeMessageHandler());
            httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var streamContent = new StreamContent(stream);
                
                
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    try
                    {
                        var response = await httpClient.PostAsync(_analyseImageUri, streamContent);

                        var json = await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {

                            var imageResult = JsonConvert.DeserializeObject<ImageResult>(json);

                            return imageResult;
                        }

                        throw new Exception(json);
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                   }
                
                   

                return null;
            

           
        }

        public async Task<string> UploadlAsync(Stream _imageStream,string caption)
        {


            var httpClient = new HttpClient(new NativeMessageHandler());
           
                var content = new MultipartFormDataContent();
                httpClient.DefaultRequestHeaders.Accept.Clear();
            _imageStream.Seek(0, SeekOrigin.Begin);
                 var streamContent = new StreamContent(_imageStream) ;

                content.Add(new StreamContent(_imageStream),
                                      "\"file\"",
                                      $"\"{BindablePicker.selectedItem + "\\" + caption + ".jpg"}\"");
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                   
                    var SeviceAddress = "http://lerictestupload.azurewebsites.net/api/Files/Upload";
                    // httpClient.CancelPendingRequests();
                    try
                    {
                        // return httpClient.PostAsync(SeviceAddress, content).Result.Content.ReadAsStringAsync().Result; ;

                        var httpResponseMessage = await httpClient.PostAsync(SeviceAddress, content);
                        string result = await httpResponseMessage.Content.ReadAsStringAsync();
                        if (httpResponseMessage.IsSuccessStatusCode)
                        {


                            return result;
                        }

                        throw new Exception(result);

                    }
                    catch (Exception ex)
                    {
                        return ex.ToString();
                    }


            return null;
        }

        /// <summary>
        /// This operation extracts a rich set of visual features based on the image content. 
        /// </summary>
        /// <param name="imageUrl">The image url.</param>
        /// <returns></returns>
        public async Task<ImageResult> AnalyseImageUrlAsync(string imageUrl)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

                var stringContent = new StringContent(@"{""url"":""" + imageUrl + @"""}");

                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                try
                {
                    var response = await httpClient.PostAsync(_analyseImageUri, stringContent);

                    var json = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {

                        var imageResult = JsonConvert.DeserializeObject<ImageResult>(json);

                        return imageResult;
                    }

                    throw new Exception(json);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }

               

            return null;
        }

        /// <summary>
        /// Optical Character Recognition (OCR) detects text in an image 
        /// and extracts the recognized characters into a machine-usable character stream.
        /// </summary>
        /// <param name="imageUrl">The image url.</param>
        /// <returns></returns>
        public async Task<OcrResult> ExtractTextFromImageUrlAsync(string imageUrl)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

                var stringContent = new StringContent(@"{""url"":""" + imageUrl + @"""}");

                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                try
                {
                    var response = await httpClient.PostAsync(_extractTextUri, stringContent);

                    var json = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {

                        var imageResultOcr = JsonConvert.DeserializeObject<OcrResult>(json);

                        return imageResultOcr;
                    }

                    throw new Exception(json);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }

               

            return null;
        }

        /// <summary>
        /// Optical Character Recognition (OCR) detects text in an image 
        /// and extracts the recognized characters into a machine-usable character stream.
        /// </summary>
        /// <param name="imageUrl">The image url.</param>
        /// <returns></returns>
        public async Task<OcrResult> ExtractTextFromImageStreamAsync(Stream stream)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var streamContent = new StreamContent(stream);

            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            try
            {
                var response = await httpClient.PostAsync(_extractTextUri, streamContent);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    var imageResultOcr = JsonConvert.DeserializeObject<OcrResult>(json);

                    return imageResultOcr;
                }

                throw new Exception(json);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return null;
        }
    }
}
