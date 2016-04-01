using System;
using System.Runtime.Serialization;

namespace SchedulesDirectNET.Lib
{
	public class StatusOp : GetOperationBase<StatusOp.StatusResponse>
	{
		public StatusOp(string baseUrl, string token) : base(baseUrl, "status")
		{
			Token = token;
		}

		[DataContract]
		public class StatusResponse : ResponseBase
		{
			[DataMember]
			public Account account { get; set; }

			[DataMember]
			public Lineup[] lineups { get; set; }

			[DataMember]
			public DateTimeOffset lastDataUpdate { get; set; }

			[DataMember]
			public string[] notifications { get; set; }

			[DataMember]
			public SystemStatus[] systemStatus { get; set; }
		}

		[DataContract]
		public class Account
		{
			[DataMember]
			public DateTimeOffset expires { get; set; }

			[DataMember]
			public string[] messages { get; set; }

			[DataMember]
			public int maxLineups { get; set; }
		}

		[DataContract]
		public class Lineup
		{
			[DataMember]
			public string lineup { get; set; }

			[DataMember]
			public DateTimeOffset modified { get; set; }

			[DataMember]
			public string uri { get; set; }
		}

		[DataContract]
		public class SystemStatus
		{
			[DataMember]
			public DateTimeOffset date { get; set; }

			[DataMember]
			public string status { get; set; }

			[DataMember]
			public string message { get; set; }
		}
	}
}