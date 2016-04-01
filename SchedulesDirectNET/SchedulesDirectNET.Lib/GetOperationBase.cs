using System.Net.Http;
using System.Threading.Tasks;

namespace SchedulesDirectNET.Lib
{
	public abstract class GetOperationBase<TResponse> : OperationBase<TResponse> where TResponse : class
	{
		protected GetOperationBase(string baseUrl, string relativeUri) : base(baseUrl, relativeUri)
		{
		}

		protected override async Task<HttpResponseMessage> ClientExecute(HttpClient client)
		{
			return await client.GetAsync(RelativeUri);
		}
	}
}