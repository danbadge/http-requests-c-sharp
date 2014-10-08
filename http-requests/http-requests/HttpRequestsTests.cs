using System;
using System.IO;
using System.Net;
using System.Net.Http;
using NUnit.Framework;

namespace http_requests
{
	[TestFixture]
    public class HttpRequestsTests
    {
		[Test]
		public void Should_return_a_release_using_http_web_request()
		{
			const string url = "http://catalogue-metadata-http-cache.prod.svc.7d/releases/1012124";

			var httpRequest = (HttpWebRequest)WebRequest.Create(url);
			httpRequest.Method = "GET";

			Console.WriteLine(httpRequest.Method + " " + url);

			var response = (HttpWebResponse)httpRequest.GetResponse();
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

			string responseBody;
			using (var stream = response.GetResponseStream())
			{
				responseBody = new StreamReader(stream).ReadToEnd();
			}

			Console.WriteLine(responseBody);

			Assert.That(responseBody, Is.StringContaining("\"id\":1012124"));
			Assert.That(responseBody, Is.StringContaining("\"title\":\"Bon Jovi Greatest Hits\""));
		}

		[Test]
		public async void Should_return_a_release_using_http_client()
		{
			const string url = "http://catalogue-metadata-http-cache.prod.svc.7d/releases/1012124";

			var client = new HttpClient();
			Console.WriteLine("GET " + url);

			var response = await client.GetAsync(url);

			string responseBody;
			
			using (response)
			{
				Assert.True(response.IsSuccessStatusCode);
				responseBody = await response.Content.ReadAsStringAsync();
			}

			Console.WriteLine(responseBody);

			Assert.That(responseBody, Is.StringContaining("\"id\":1012124"));
			Assert.That(responseBody, Is.StringContaining("\"title\":\"Bon Jovi Greatest Hits\""));
		}
    }
}