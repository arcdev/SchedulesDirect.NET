using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SchedulesDirectNET.Lib
{
	public abstract class OperationBase<TResponse> : IOperation<TResponse> where TResponse : class
	{
		protected readonly string BaseUrl;
		protected readonly string RelativeUri;

		protected OperationBase(string baseUrl, string relativeUri)
		{
			BaseUrl = baseUrl;
			RelativeUri = relativeUri;
			UseProxy = true; //todo: remove as this is temporary
		}

		public virtual bool UseProxy { get; set; }
		public virtual string Token { get; set; }

		protected virtual async Task<TResponse> InternalExecute()
		{
			var httpClientHandler = new HttpClientHandler
			{
				Proxy = new WebProxy("http://localhost:8888", false),
				UseProxy = UseProxy
			};

			using (var client = new HttpClient(httpClientHandler))
			{
				client.BaseAddress = new Uri(BaseUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
				client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
//				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue());

				client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.87 Safari/537.36");
				client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

				if (string.IsNullOrWhiteSpace(Token) == false)
				{
					client.DefaultRequestHeaders.Add("Token", Token);
				}

				var responseMessage = await ClientExecute(client);

				if (responseMessage.IsSuccessStatusCode)
				{
					var rtn = await responseMessage.Content.ReadAsAsync<TResponse>();
					return rtn;
				}
			}

			return null;
		}

		protected abstract Task<HttpResponseMessage> ClientExecute(HttpClient client);

		public virtual async Task<TResponse> Execute()
		{
			return await InternalExecute();
		}
	}
}