using System;
using System.Runtime.Serialization;

namespace SchedulesDirectNET.Lib
{
	public class LineupsOp : GetOperationBase<LineupsOp.LineupsResponse>
	{
		public LineupsOp(string baseUrl, string token, string lineup) : base(baseUrl, "lineups/" + lineup)
		{
			Token = token;
		}

		[DataContract]
		public class LineupsResponse
		{
			[DataMember]
			public Map[] map { get; set; }

			[DataMember]
			public Station[] stations { get; set; }

			[DataMember]
			public MetaData metadata { get; set; }
		}

		[DataContract]
		public class Map
		{
			[DataMember]
			public string StationID { get; set; }

			[DataMember]
			public int uhfVhf { get; set; }

			[DataMember]
			public int atscMajor { get; set; }

			[DataMember]
			public int atscMinor { get; set; }
		}

		[DataContract]
		public class Station
		{
			[DataMember]
			public string stationID { get; set; }

			[DataMember]
			public string name { get; set; }

			[DataMember]
			public string callsign { get; set; }

			[DataMember]
			public string affiliate { get; set; }

			[DataMember]
			public string[] broadcastLanguage { get; set; }

			[DataMember]
			public string[] descriptionLanguage { get; set; }

			[DataMember]
			public Broadcaster broadcaster { get; set; }

			[DataMember]
			public Logo logo { get; set; }
		}

		[DataContract]
		public class Broadcaster
		{
			[DataMember]
			public string city { get; set; }

			[DataMember]
			public string state { get; set; }

			[DataMember]
			public string postalcode { get; set; }

			[DataMember]
			public string country { get; set; }
		}

		[DataContract]
		public class Logo
		{
			[DataMember]
			public string URL { get; set; }

			[DataMember]
			public int height { get; set; }

			[DataMember]
			public int width { get; set; }

			[DataMember]
			public string md5 { get; set; }
		}

		[DataContract]
		public class MetaData
		{
			[DataMember]
			public string lineup { get; set; }

			[DataMember]
			public DateTimeOffset modified { get; set; }

			[DataMember]
			public string transport { get; set; }
		}
	}
}