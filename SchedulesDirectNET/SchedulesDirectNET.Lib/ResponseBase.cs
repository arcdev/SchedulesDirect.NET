using System.Runtime.Serialization;

namespace SchedulesDirectNET.Lib
{
	public abstract class ResponseBase
	{
		[DataMember(Name = "code")]
		public int Code { get; set; }

		[DataMember(Name = "serverID")]
		public string ServerId { get; set; }
	}
}