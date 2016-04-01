using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SchedulesDirectNET.Lib
{
	public class ProgramsOp : PostOperationBase<ProgramsOp.ProgramsOpResponse[], string[]>
	{
		private readonly IEnumerable<string> _programIds;

		[DataContract]
		public class ProgramsOpResponse
		{
			[DataMember]
			public string programID { get; set; }

			[DataMember]
			public Dictionary<string, string>[] titles { get; set; }

			[DataMember]
			public Descriptions descriptions { get; set; }


			[DataMember]
			public string originalAirDate { get; set; }

			[DataMember]
			public string[] genres { get; set; }

			[DataMember]
			public string episodeTitle150 { get; set; }

			[DataMember]
			public MetaData[] metadata { get; set; }

			[DataMember]
			public ContentRating[] contentRating { get; set; }

			[DataMember]
			public Person[] cast { get; set; }

			[DataMember]
			public Person[] crew { get; set; }

			[DataMember]
			public string showType { get; set; }

			[DataMember]
			public bool hasImageArtwork { get; set; }

			[DataMember]
			public string md5 { get; set; }
		}

		[DataContract]
		public class ContentRating
		{
			[DataMember]
			public string body { get; set; }

			[DataMember]
			public string code { get; set; }
		}

		[DataContract]
		public class Descriptions
		{
			[DataMember]
			public Description[] description1000 { get; set; }
		}

		[DataContract]
		public class Description
		{
			[DataMember(Name = "descriptionLanguage")]
			public string Language { get; set; }

			[DataMember(Name = "description")]
			public string Text { get; set; }
		}

		public class MetaData
		{
			public Gracenote Gracenote { get; set; }
		}

		public class Gracenote
		{
			public int season { get; set; }
			public int episode { get; set; }
		}

		public class Person
		{
			public string personId { get; set; }
			public string nameId { get; set; }
			public string name { get; set; }
			public string role { get; set; }
			public string billingOrder { get; set; }
		}

		public ProgramsOp(string baseUrl, string token, IEnumerable<string> programIds) : base(baseUrl, "programs")
		{
			_programIds = programIds;
			Token = token;
		}

		protected override string[] BuildRequest()
		{
			//return new Request {ProgramIds = new string[] {"one", "two"}};
			//return new string[] {"one", "two"};
			return _programIds.ToArray();
		}
	}
}