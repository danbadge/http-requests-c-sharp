using System;
using System.IO;
using System.Net;
using NUnit.Framework;

namespace http_requests
{
	[TestFixture]
    public class HttpRequestsTests
    {
		[Test]
		public void Should_return_a_release()
		{
			const string url = "http://catalogue-metadata-http-cache.prod.svc.7d/releases/1012124";

			var httpRequest = (HttpWebRequest)WebRequest.Create(url);
			httpRequest.Method = "GET";

			Console.WriteLine(httpRequest.Method + " " + url);

			var response = httpRequest.GetResponse();

			string body;
			using (var stream = response.GetResponseStream())
			{
				body = new StreamReader(stream).ReadToEnd();
			}

			Console.WriteLine(body);

			Assert.That(body, Is.StringContaining("\"id\":1012124"));
			Assert.That(body, Is.StringContaining("\"title\":\"Bon Jovi Greatest Hits\""));
		}
    }
}
