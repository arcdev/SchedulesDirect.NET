using System.Net.Http;
using System.Threading.Tasks;

namespace SchedulesDirectNET.Lib
{
	public abstract class PostOperationBase<TResponse, TRequest> : OperationBase<TResponse> where TResponse : class
	{
		protected PostOperationBase(string baseUrl, string relativeUri) : base(baseUrl, relativeUri)
		{
		}

		protected abstract TRequest BuildRequest();

		protected override async Task<HttpResponseMessage> ClientExecute(HttpClient client)
		{
			var request = BuildRequest();
			return await client.PostAsJsonAsync(RelativeUri, request);
		}
	}
}