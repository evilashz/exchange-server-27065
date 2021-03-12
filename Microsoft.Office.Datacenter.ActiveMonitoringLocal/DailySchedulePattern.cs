using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000054 RID: 84
	internal class DailySchedulePattern
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x00016370 File Offset: 0x00014570
		internal DailySchedulePattern(string dailySchedulePattern)
		{
			this.ParseDailySchedulePattern(dailySchedulePattern);
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0001637F File Offset: 0x0001457F
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x00016387 File Offset: 0x00014587
		internal TimeZoneInfo TimeZoneInfo { get; private set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00016390 File Offset: 0x00014590
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x00016398 File Offset: 0x00014598
		internal HashSet<DayOfWeek> ScheduledDays { get; private set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x000163A1 File Offset: 0x000145A1
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x000163A9 File Offset: 0x000145A9
		internal DateTime StartTime { get; private set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x000163B2 File Offset: 0x000145B2
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x000163BA File Offset: 0x000145BA
		internal DateTime EndTime { get; private set; }

		// Token: 0x06000575 RID: 1397 RVA: 0x000163C4 File Offset: 0x000145C4
		private void ParseDailySchedulePattern(string dailySchedulePattern)
		{
			try
			{
				dailySchedulePattern = ((!string.IsNullOrWhiteSpace(dailySchedulePattern)) ? dailySchedulePattern : "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59");
				string[] array = dailySchedulePattern.Split(new char[]
				{
					'/'
				});
				if (array.Length != 4)
				{
					throw new FormatException("Must have 4 components.");
				}
				this.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(array[0]);
				this.ScheduledDays = new HashSet<DayOfWeek>();
				foreach (string value in array[1].Split(new char[]
				{
					',',
					';'
				}))
				{
					DayOfWeek item;
					if (Enum.TryParse<DayOfWeek>(value, true, out item) && !this.ScheduledDays.Contains(item))
					{
						this.ScheduledDays.Add(item);
					}
				}
				if (this.ScheduledDays.Count == 0)
				{
					throw new FormatException("Found no valid days.");
				}
				this.StartTime = DateTime.ParseExact(array[2], "t", CultureInfo.InvariantCulture, DateTimeStyles.None);
				this.EndTime = DateTime.ParseExact(array[3], "t", CultureInfo.InvariantCulture, DateTimeStyles.None);
				if (this.EndTime <= this.StartTime)
				{
					this.EndTime = this.StartTime.Date.AddDays(1.0).AddMilliseconds(-1.0);
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError<string, string>(ExTraceGlobals.CommonComponentsTracer, DailySchedulePattern.traceContext, "Invalid DailySchedulePattern value '{0}': {1}", dailySchedulePattern, ex.ToString(), null, "ParseDailySchedulePattern", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\DailySchedulePattern.cs", 119);
				if (!(dailySchedulePattern != "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59"))
				{
					throw new Exception("No valid DailySchedulePattern value is available.");
				}
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, DailySchedulePattern.traceContext, "Default value '{0}' will be used.", "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", null, "ParseDailySchedulePattern", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Responders\\DailySchedulePattern.cs", 123);
				this.ParseDailySchedulePattern("Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59");
			}
		}

		// Token: 0x040003B5 RID: 949
		internal const string DefaultDailySchedulePattern = "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59";

		// Token: 0x040003B6 RID: 950
		private static TracingContext traceContext = TracingContext.Default;
	}
}
