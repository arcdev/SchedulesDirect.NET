using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using SchedulesDirectNET.Lib;

namespace SchedulesDirectNET.Console
{
	class Program
	{
		const string baseUrl = "https://json.schedulesdirect.org/20141201/";

		static void Main(string[] args)
		{
			var username = ConfigurationManager.AppSettings["username"];
			var passwordHash = ConfigurationManager.AppSettings["passwordHash"];

			var tokenResult = AsyncExec(new TokenOp(baseUrl, username, passwordHash));
			var token = tokenResult.Token;

			System.Console.WriteLine("token='{0}'", token);

			var status = AsyncExec(new StatusOp(baseUrl, token));
			System.Console.WriteLine("status.code='{0}'", status.Code);

			var lineupId = status.lineups.First().lineup;
			var lineup = AsyncExec(new LineupsOp(baseUrl, token, lineupId));


			// works
			//			var schedReq = new SchedulesOp.SchedulesOpRequest(lineup.stations[0].stationID);
			//			var schedules = AsyncExec(new SchedulesOp(baseUrl, token, new[] {schedReq}));

			// works
			//			var schedReqs = lineup.stations.Select(s => new SchedulesOp.SchedulesOpRequest(s.stationID));
			//			var schedules = AsyncExec(new SchedulesOp(baseUrl, token, schedReqs));

			// works
			var schedReqs = lineup.stations.Select(s =>
			{
				var schedReq = new SchedulesOp.SchedulesOpRequest(s.stationID);
				schedReq.Dates.Add(DateTimeOffset.Now);
				return schedReq;
			});
			var schedules = AsyncExec(new SchedulesOp(baseUrl, token, schedReqs));

			var programIds = new HashSet<string>();
			foreach (var schedule in schedules)
			{
				foreach (var program in schedule.programs)
				{
					programIds.Add(program.programID);
				}
			}

			//var programIds = schedules.Select(sched => sched.programs.Select(p => p.programID)).Distinct().ToArray();

			//			var programs = AsyncExec(new ProgramsOp(baseUrl, token, new []{ "EP000023483475" }));
			var programs = AsyncExec(new ProgramsOp(baseUrl, token, programIds));

			//var schedules = AsyncExec(new SchedulesOp(baseUrl, token, lineup.stations.First().stationID, new[] {new DateTimeOffset(2016, 03, 31, 0, 0, 0, TimeSpan.Zero)}));

			//var ep = schedules[0].programs.First(s => s.programID == "EP003670780114");

			if (Debugger.IsAttached)
			{
				System.Console.WriteLine("Press ENTER to exit.");
				System.Console.ReadLine();
			}
		}

		private static T AsyncExec<T>(IOperation<T> op) where T : class
		{
			var task = op.Execute();
			task.Wait(1000);
			return task.Result;
		}
	}
}