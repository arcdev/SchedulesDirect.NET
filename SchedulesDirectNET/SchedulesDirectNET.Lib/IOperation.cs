using System.Threading.Tasks;

namespace SchedulesDirectNET.Lib
{
	public interface IOperation<TResponse> where TResponse : class
	{
		Task<TResponse> Execute();
	}
}