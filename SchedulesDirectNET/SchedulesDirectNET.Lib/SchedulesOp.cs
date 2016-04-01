using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SchedulesDirectNET.Lib
{
	public class SchedulesOp : PostOperationBase<SchedulesOp.SchedulesResponse[], SchedulesOp.SchedulesRequest[]>
	{
		private readonly IEnumerable<SchedulesOpRequest> _request;
//		private readonly IEnumerable<DateTimeOffset> _dates;
//		private readonly IEnumerable<string> _stationId;

		public SchedulesOp(string baseUrl, string token, IEnumerable<SchedulesOpRequest> request) : base(baseUrl, "schedules")
		{
			Token = token;
			_request = request;
		}

//		public SchedulesOp(string baseUrl, string token, IEnumerable<string> stationId, IEnumerable<DateTimeOffset> dates) : base(baseUrl, "schedules")
//		{
//			Token = token;
//			_stationId = stationId;
//			_dates = dates;
//		}

		public class SchedulesOpRequest
		{
			public SchedulesOpRequest(string stationId)
			{
				StationId = stationId;
				Dates = new List<DateTimeOffset>();
			}

			public string StationId { get; set; }

			public List<DateTimeOffset> Dates { get; }
		}

		protected override SchedulesRequest[] BuildRequest()
		{
			var rtn = _request.Select(m =>
			{
				var req = new SchedulesRequest
				{
					stationID = m.StationId
				};
				if (m.Dates != null && m.Dates.Any())
				{
					req.date = m.Dates.Select(date => date.ToString("yyyy-MM-dd")).ToArray();
				}
				return req;
			}).ToArray();


//			var rtn = new[]
//			{
//				new SchedulesRequest
//				{
//					stationID = _stationId,
//					date = _dates.Select(date => date.ToString("yyyy-MM-dd")).ToArray()
//				}
//			};
			return rtn;
		}

		[DataContract]
		public class SchedulesRequest
		{
			[DataMember]
			public string stationID { get; set; }

			[DataMember]
			public string[] date { get; set; }
		}

		[DataContract]
		public class SchedulesResponse
		{
			[DataMember]
			public string stationID { get; set; }

			[DataMember]
			public Program[] programs { get; set; }

			[DataMember]
			public MetaData metadata { get; set; }
		}

		[DataContract]
		public class Program
		{
			[DataMember]
			public string programID { get; set; }

			[DataMember]
			public DateTimeOffset airDateTime { get; set; }

			[DataMember]
			public int duration { get; set; }

			[DataMember]
			public string md5 { get; set; }

			[DataMember(Name = "new")]
			public bool IsNew { get; set; }

			[DataMember]
			public string[] audioProperties { get; set; }

			[DataMember]
			public string[] videoProperties { get; set; }

			[DataMember]
			public Rating[] ratings { get; set; }
		}

		[DataContract]
		public class Rating
		{
			[DataMember]
			public string body { get; set; }

			[DataMember]
			public string code { get; set; }
		}

		[DataContract]
		public class MetaData
		{
			[DataMember]
			public DateTimeOffset modified { get; set; }

			[DataMember]
			public string md5 { get; set; }

			[DataMember]
			public string startDate { get; set; }
		}
	}
}