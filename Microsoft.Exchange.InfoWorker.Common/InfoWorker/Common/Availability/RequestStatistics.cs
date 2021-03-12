using System;
using System.Globalization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000C4 RID: 196
	internal sealed class RequestStatistics
	{
		// Token: 0x060004EE RID: 1262 RVA: 0x000161BC File Offset: 0x000143BC
		public static RequestStatistics Create(RequestStatisticsType tag, long timeTaken)
		{
			return new RequestStatistics
			{
				info = RequestStatistics.Info.TimeTaken,
				Tag = tag,
				TimeTaken = timeTaken
			};
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000161E8 File Offset: 0x000143E8
		public static RequestStatistics Create(RequestStatisticsType tag, long timeTaken, string destination)
		{
			return new RequestStatistics
			{
				info = (RequestStatistics.Info.TimeTaken | RequestStatistics.Info.Destination),
				Tag = tag,
				TimeTaken = timeTaken,
				Destination = destination
			};
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00016218 File Offset: 0x00014418
		public static RequestStatistics Create(RequestStatisticsType tag, long timeTaken, int requestCount)
		{
			return new RequestStatistics
			{
				info = (RequestStatistics.Info.TimeTaken | RequestStatistics.Info.RequestCount),
				Tag = tag,
				TimeTaken = timeTaken,
				RequestCount = requestCount
			};
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00016248 File Offset: 0x00014448
		public static RequestStatistics Create(RequestStatisticsType tag, long timeTaken, int requestCount, string destination)
		{
			return new RequestStatistics
			{
				info = (RequestStatistics.Info.TimeTaken | RequestStatistics.Info.RequestCount | RequestStatistics.Info.Destination),
				Tag = tag,
				TimeTaken = timeTaken,
				RequestCount = requestCount,
				Destination = destination
			};
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0001627F File Offset: 0x0001447F
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x00016287 File Offset: 0x00014487
		public RequestStatisticsType Tag { get; private set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00016290 File Offset: 0x00014490
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x00016298 File Offset: 0x00014498
		public long TimeTaken { get; private set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x000162A1 File Offset: 0x000144A1
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x000162A9 File Offset: 0x000144A9
		public int RequestCount { get; private set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x000162B2 File Offset: 0x000144B2
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x000162BA File Offset: 0x000144BA
		public string Destination { get; private set; }

		// Token: 0x060004FA RID: 1274 RVA: 0x000162C4 File Offset: 0x000144C4
		public void Log(RequestLogger requestLogger)
		{
			if (requestLogger == null)
			{
				throw new ArgumentNullException("requestLogger");
			}
			if (this.info == RequestStatistics.Info.TimeTaken)
			{
				requestLogger.AppendToLog<long>(this.Tag.Name, this.TimeTaken);
				return;
			}
			if (this.info == RequestStatistics.Info.RequestCount)
			{
				requestLogger.AppendToLog<int>(this.Tag.Name, this.RequestCount);
				return;
			}
			if ((this.info & RequestStatistics.Info.TimeTaken) != (RequestStatistics.Info)0)
			{
				requestLogger.AppendToLog<long>(this.Tag.Name + ".T", this.TimeTaken);
			}
			if ((this.info & RequestStatistics.Info.RequestCount) != (RequestStatistics.Info)0)
			{
				requestLogger.AppendToLog<int>(this.Tag.Name + ".R", this.RequestCount);
			}
			if ((this.info & RequestStatistics.Info.Destination) != (RequestStatistics.Info)0)
			{
				requestLogger.AppendToLog<string>(this.Tag.Name + ".D", this.Destination);
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000163B4 File Offset: 0x000145B4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:TimeTaken={1}, RequestCount={2}, Destination={3}", new object[]
			{
				this.Tag.Name,
				this.TimeTaken,
				this.RequestCount,
				this.Destination
			});
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001640E File Offset: 0x0001460E
		private RequestStatistics()
		{
		}

		// Token: 0x040002DC RID: 732
		private RequestStatistics.Info info;

		// Token: 0x020000C5 RID: 197
		[Flags]
		private enum Info
		{
			// Token: 0x040002E2 RID: 738
			TimeTaken = 1,
			// Token: 0x040002E3 RID: 739
			RequestCount = 2,
			// Token: 0x040002E4 RID: 740
			Destination = 4
		}
	}
}
