using System.Runtime.Serialization;

namespace SchedulesDirectNET.Lib
{
	public class TokenOp : PostOperationBase<TokenOp.TokenResponse, TokenOp.TokenRequest>
	{
		private readonly string _username;
		private readonly string _passwordHash;

		[DataContract]
		public class TokenRequest
		{
			[DataMember(Name = "username")]
			public string Username { get; set; }

			[DataMember(Name = "password")]
			public string PasswordHash { get; set; }
		}

		[DataContract]
		public class TokenResponse : ResponseBase
		{
			[DataMember(Name = "message")]
			public string Message { get; set; }


			[DataMember(Name = "token")]
			public string Token { get; set; }
		}

		public TokenOp(string baseUrl, string username, string passwordHash) : base(baseUrl, "token")
		{
			_username = username;
			_passwordHash = passwordHash;
		}

		protected override TokenRequest BuildRequest()
		{
			var rtn = new TokenRequest
			{
				Username = _username,
				PasswordHash = _passwordHash
			};
			return rtn;
		}
	}
}