using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.ApplicationLogic.PeopleCentricTriage
{
	// Token: 0x02000173 RID: 371
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PeopleCentricTriageLogger : DisposeTrackableBase, ITracer, IPerformanceDataLogger
	{
		// Token: 0x06000E81 RID: 3713 RVA: 0x0003BEF4 File Offset: 0x0003A0F4
		public PeopleCentricTriageLogger(string loggingPath, TimeSpan logFileMaxAge, long logDirectoryMaxSize, long logFileMaxSize, string logComponentName, string logFileName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("loggingPath", loggingPath);
			ArgumentValidator.ThrowIfNullOrEmpty("logComponentName", logComponentName);
			ArgumentValidator.ThrowIfNullOrEmpty("logFileName", logFileName);
			this.build = ExchangeSetupContext.InstalledVersion.ToString();
			this.log = new Log(logFileName, new LogHeaderFormatter(PeopleCentricTriageLogger.Schema), logComponentName);
			this.log.Configure(loggingPath, logFileMaxAge, logDirectoryMaxSize, logFileMaxSize);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0003BF63 File Offset: 0x0003A163
		public void TraceDebug<T0>(long id, string formatString, T0 arg0)
		{
			this.LogDebug(id, string.Format(formatString, arg0));
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0003BF78 File Offset: 0x0003A178
		public void TraceDebug<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.LogDebug(id, string.Format(formatString, arg0, arg1));
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0003BF94 File Offset: 0x0003A194
		public void TraceDebug<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.LogDebug(id, string.Format(formatString, arg0, arg1, arg2));
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0003BFB7 File Offset: 0x0003A1B7
		public void TraceDebug(long id, string formatString, params object[] args)
		{
			this.LogDebug(id, string.Format(formatString, args));
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0003BFC7 File Offset: 0x0003A1C7
		public void TraceDebug(long id, string message)
		{
			this.LogDebug(id, message);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0003BFD1 File Offset: 0x0003A1D1
		public void TraceWarning<T0>(long id, string formatString, T0 arg0)
		{
			this.LogWarning(id, string.Format(formatString, arg0));
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0003BFE6 File Offset: 0x0003A1E6
		public void TraceWarning(long id, string message)
		{
			this.LogWarning(id, message);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0003BFF0 File Offset: 0x0003A1F0
		public void TraceWarning(long id, string formatString, params object[] args)
		{
			this.LogWarning(id, string.Format(formatString, args));
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003C000 File Offset: 0x0003A200
		public void TraceError(long id, string message)
		{
			this.LogError(id, message);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0003C00A File Offset: 0x0003A20A
		public void TraceError(long id, string formatString, params object[] args)
		{
			this.LogError(id, string.Format(formatString, args));
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0003C01A File Offset: 0x0003A21A
		public void TraceError<T0>(long id, string formatString, T0 arg0)
		{
			this.LogError(id, string.Format(formatString, arg0));
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0003C02F File Offset: 0x0003A22F
		public void TraceError<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.LogError(id, string.Format(formatString, arg0, arg1));
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0003C04B File Offset: 0x0003A24B
		public void TraceError<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.LogError(id, string.Format(formatString, arg0, arg1, arg2));
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003C06E File Offset: 0x0003A26E
		public void TracePerformance(long id, string message)
		{
			this.LogPerformance(id, message);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0003C078 File Offset: 0x0003A278
		public void TracePerformance(long id, string formatString, params object[] args)
		{
			this.LogPerformance(id, string.Format(formatString, args));
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003C088 File Offset: 0x0003A288
		public void TracePerformance<T0>(long id, string formatString, T0 arg0)
		{
			this.LogPerformance(id, string.Format(formatString, arg0));
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0003C09D File Offset: 0x0003A29D
		public void TracePerformance<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.LogPerformance(id, string.Format(formatString, arg0, arg1));
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0003C0B9 File Offset: 0x0003A2B9
		public void TracePerformance<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.LogPerformance(id, string.Format(formatString, arg0, arg1, arg2));
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0003C0DC File Offset: 0x0003A2DC
		public void Dump(TextWriter writer, bool addHeader, bool verbose)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0003C0E3 File Offset: 0x0003A2E3
		public ITracer Compose(ITracer other)
		{
			return new CompositeTracer(this, other);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0003C0EC File Offset: 0x0003A2EC
		public bool IsTraceEnabled(TraceType traceType)
		{
			return true;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0003C0F0 File Offset: 0x0003A2F0
		public void Log(string marker, string counter, TimeSpan dataPoint)
		{
			this.Log(0L, "PERFORMANCE", string.Empty, marker, counter, dataPoint.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0003C125 File Offset: 0x0003A325
		public void Log(string marker, string counter, uint dataPoint)
		{
			this.Log(0L, "PERFORMANCE", string.Empty, marker, counter, dataPoint.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0003C147 File Offset: 0x0003A347
		public void Log(string marker, string counter, string dataPoint)
		{
			this.Log(0L, "PERFORMANCE", string.Empty, marker, counter, dataPoint);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0003C15E File Offset: 0x0003A35E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PeopleCentricTriageLogger>(this);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0003C166 File Offset: 0x0003A366
		protected override void InternalDispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (this.log != null)
			{
				this.log.Close();
				this.log = null;
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0003C186 File Offset: 0x0003A386
		private void LogDebug(long sessionId, string message)
		{
			this.Log(sessionId, "DEBUG", message);
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0003C195 File Offset: 0x0003A395
		private void LogError(long sessionId, string message)
		{
			this.Log(sessionId, "ERROR", message);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0003C1A4 File Offset: 0x0003A3A4
		private void LogWarning(long sessionId, string message)
		{
			this.Log(sessionId, "WARNING", message);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0003C1B3 File Offset: 0x0003A3B3
		private void LogPerformance(long sessionId, string message)
		{
			this.Log(sessionId, "PERFORMANCE", message);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003C1C4 File Offset: 0x0003A3C4
		private void Log(long sessionId, string eventType, string message, string perfMarker, string perfCounter, string perfDatapoint)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(PeopleCentricTriageLogger.Schema, true);
			logRowFormatter[1] = this.build;
			logRowFormatter[2] = sessionId.ToString("X8");
			logRowFormatter[3] = Environment.MachineName;
			logRowFormatter[4] = eventType;
			logRowFormatter[5] = message;
			logRowFormatter[6] = perfMarker;
			logRowFormatter[7] = perfCounter;
			logRowFormatter[8] = perfDatapoint;
			this.log.Append(logRowFormatter, 0);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003C241 File Offset: 0x0003A441
		private void Log(long sessionId, string eventType, string message)
		{
			this.Log(sessionId, eventType, message, string.Empty, string.Empty, string.Empty);
		}

		// Token: 0x040007C4 RID: 1988
		private const long NoSessionId = 0L;

		// Token: 0x040007C5 RID: 1989
		private static readonly string[] LogColumns = new string[]
		{
			"Time",
			"Build",
			"SessionId",
			"Server",
			"EventType",
			"Message",
			"PerfMarker",
			"PerfCounter",
			"PerfDatapoint"
		};

		// Token: 0x040007C6 RID: 1990
		private static readonly LogSchema Schema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "People Centric Triage log", PeopleCentricTriageLogger.LogColumns);

		// Token: 0x040007C7 RID: 1991
		private readonly string build;

		// Token: 0x040007C8 RID: 1992
		private Log log;

		// Token: 0x02000174 RID: 372
		private static class LogColumnIndices
		{
			// Token: 0x040007C9 RID: 1993
			public const int Time = 0;

			// Token: 0x040007CA RID: 1994
			public const int Build = 1;

			// Token: 0x040007CB RID: 1995
			public const int SessionId = 2;

			// Token: 0x040007CC RID: 1996
			public const int Server = 3;

			// Token: 0x040007CD RID: 1997
			public const int EventType = 4;

			// Token: 0x040007CE RID: 1998
			public const int Message = 5;

			// Token: 0x040007CF RID: 1999
			public const int PerfMarker = 6;

			// Token: 0x040007D0 RID: 2000
			public const int PerfCounter = 7;

			// Token: 0x040007D1 RID: 2001
			public const int PerfDatapoint = 8;
		}

		// Token: 0x02000175 RID: 373
		private static class EventTypes
		{
			// Token: 0x040007D2 RID: 2002
			public const string Debug = "DEBUG";

			// Token: 0x040007D3 RID: 2003
			public const string Error = "ERROR";

			// Token: 0x040007D4 RID: 2004
			public const string Warning = "WARNING";

			// Token: 0x040007D5 RID: 2005
			public const string Performance = "PERFORMANCE";
		}
	}
}
