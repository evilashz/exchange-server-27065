using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001F4 RID: 500
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoGarbageCollectionLogger : DisposeTrackableBase, ITracer, IPerformanceDataLogger
	{
		// Token: 0x06001238 RID: 4664 RVA: 0x0004D36C File Offset: 0x0004B56C
		public PhotoGarbageCollectionLogger(PhotosConfiguration configuration, string logFileName)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNullOrEmpty("logFileName", logFileName);
			this.build = ExchangeSetupContext.InstalledVersion.ToString();
			this.log = new Log(logFileName, new LogHeaderFormatter(PhotoGarbageCollectionLogger.Schema), "PhotoGarbageCollector");
			this.log.Configure(configuration.GarbageCollectionLoggingPath, configuration.GarbageCollectionLogFileMaxAge, configuration.GarbageCollectionLogDirectoryMaxSize, configuration.GarbageCollectionLogFileMaxSize);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0004D3E3 File Offset: 0x0004B5E3
		public void TraceDebug<T0>(long id, string formatString, T0 arg0)
		{
			this.LogDebug(id, string.Format(formatString, arg0));
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0004D3F8 File Offset: 0x0004B5F8
		public void TraceDebug<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.LogDebug(id, string.Format(formatString, arg0, arg1));
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0004D414 File Offset: 0x0004B614
		public void TraceDebug<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.LogDebug(id, string.Format(formatString, arg0, arg1, arg2));
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0004D437 File Offset: 0x0004B637
		public void TraceDebug(long id, string formatString, params object[] args)
		{
			this.LogDebug(id, string.Format(formatString, args));
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0004D447 File Offset: 0x0004B647
		public void TraceDebug(long id, string message)
		{
			this.LogDebug(id, message);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0004D451 File Offset: 0x0004B651
		public void TraceWarning<T0>(long id, string formatString, T0 arg0)
		{
			this.LogWarning(id, string.Format(formatString, arg0));
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0004D466 File Offset: 0x0004B666
		public void TraceWarning(long id, string message)
		{
			this.LogWarning(id, message);
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0004D470 File Offset: 0x0004B670
		public void TraceWarning(long id, string formatString, params object[] args)
		{
			this.LogWarning(id, string.Format(formatString, args));
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0004D480 File Offset: 0x0004B680
		public void TraceError(long id, string message)
		{
			this.LogError(id, message);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0004D48A File Offset: 0x0004B68A
		public void TraceError(long id, string formatString, params object[] args)
		{
			this.LogError(id, string.Format(formatString, args));
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0004D49A File Offset: 0x0004B69A
		public void TraceError<T0>(long id, string formatString, T0 arg0)
		{
			this.LogError(id, string.Format(formatString, arg0));
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0004D4AF File Offset: 0x0004B6AF
		public void TraceError<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.LogError(id, string.Format(formatString, arg0, arg1));
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0004D4CB File Offset: 0x0004B6CB
		public void TraceError<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.LogError(id, string.Format(formatString, arg0, arg1, arg2));
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0004D4EE File Offset: 0x0004B6EE
		public void TracePerformance(long id, string message)
		{
			this.LogPerformance(id, message);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0004D4F8 File Offset: 0x0004B6F8
		public void TracePerformance(long id, string formatString, params object[] args)
		{
			this.LogPerformance(id, string.Format(formatString, args));
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0004D508 File Offset: 0x0004B708
		public void TracePerformance<T0>(long id, string formatString, T0 arg0)
		{
			this.LogPerformance(id, string.Format(formatString, arg0));
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0004D51D File Offset: 0x0004B71D
		public void TracePerformance<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			this.LogPerformance(id, string.Format(formatString, arg0, arg1));
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0004D539 File Offset: 0x0004B739
		public void TracePerformance<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			this.LogPerformance(id, string.Format(formatString, arg0, arg1, arg2));
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0004D55C File Offset: 0x0004B75C
		public void Dump(TextWriter writer, bool addHeader, bool verbose)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0004D563 File Offset: 0x0004B763
		public ITracer Compose(ITracer other)
		{
			return new CompositeTracer(this, other);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0004D56C File Offset: 0x0004B76C
		public bool IsTraceEnabled(TraceType traceType)
		{
			return true;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0004D570 File Offset: 0x0004B770
		public void Log(string marker, string counter, TimeSpan dataPoint)
		{
			this.Log(0L, "PERFORMANCE", string.Empty, marker, counter, dataPoint.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0004D5A5 File Offset: 0x0004B7A5
		public void Log(string marker, string counter, uint dataPoint)
		{
			this.Log(0L, "PERFORMANCE", string.Empty, marker, counter, dataPoint.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0004D5C7 File Offset: 0x0004B7C7
		public void Log(string marker, string counter, string dataPoint)
		{
			this.Log(0L, "PERFORMANCE", string.Empty, marker, counter, dataPoint);
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0004D5DE File Offset: 0x0004B7DE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PhotoGarbageCollectionLogger>(this);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0004D5E6 File Offset: 0x0004B7E6
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

		// Token: 0x06001253 RID: 4691 RVA: 0x0004D606 File Offset: 0x0004B806
		private void LogDebug(long sessionId, string message)
		{
			this.Log(sessionId, "DEBUG", message);
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0004D615 File Offset: 0x0004B815
		private void LogError(long sessionId, string message)
		{
			this.Log(sessionId, "ERROR", message);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0004D624 File Offset: 0x0004B824
		private void LogWarning(long sessionId, string message)
		{
			this.Log(sessionId, "WARNING", message);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0004D633 File Offset: 0x0004B833
		private void LogPerformance(long sessionId, string message)
		{
			this.Log(sessionId, "PERFORMANCE", message);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0004D644 File Offset: 0x0004B844
		private void Log(long sessionId, string eventType, string message, string perfMarker, string perfCounter, string perfDatapoint)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(PhotoGarbageCollectionLogger.Schema, true);
			logRowFormatter[1] = this.build;
			logRowFormatter[2] = sessionId.ToString("X");
			logRowFormatter[3] = Environment.MachineName;
			logRowFormatter[4] = eventType;
			logRowFormatter[5] = message;
			logRowFormatter[6] = perfMarker;
			logRowFormatter[7] = perfCounter;
			logRowFormatter[8] = perfDatapoint;
			this.log.Append(logRowFormatter, 0);
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0004D6C1 File Offset: 0x0004B8C1
		private void Log(long sessionId, string eventType, string message)
		{
			this.Log(sessionId, eventType, message, string.Empty, string.Empty, string.Empty);
		}

		// Token: 0x040009BF RID: 2495
		private const string LogComponentName = "PhotoGarbageCollector";

		// Token: 0x040009C0 RID: 2496
		private const long NoSessionId = 0L;

		// Token: 0x040009C1 RID: 2497
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

		// Token: 0x040009C2 RID: 2498
		private static readonly LogSchema Schema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Photo Garbage Collection log", PhotoGarbageCollectionLogger.LogColumns);

		// Token: 0x040009C3 RID: 2499
		private readonly string build;

		// Token: 0x040009C4 RID: 2500
		private Log log;

		// Token: 0x020001F5 RID: 501
		private static class LogColumnIndices
		{
			// Token: 0x040009C5 RID: 2501
			public const int Time = 0;

			// Token: 0x040009C6 RID: 2502
			public const int Build = 1;

			// Token: 0x040009C7 RID: 2503
			public const int SessionId = 2;

			// Token: 0x040009C8 RID: 2504
			public const int Server = 3;

			// Token: 0x040009C9 RID: 2505
			public const int EventType = 4;

			// Token: 0x040009CA RID: 2506
			public const int Message = 5;

			// Token: 0x040009CB RID: 2507
			public const int PerfMarker = 6;

			// Token: 0x040009CC RID: 2508
			public const int PerfCounter = 7;

			// Token: 0x040009CD RID: 2509
			public const int PerfDatapoint = 8;
		}

		// Token: 0x020001F6 RID: 502
		private static class EventTypes
		{
			// Token: 0x040009CE RID: 2510
			public const string Debug = "DEBUG";

			// Token: 0x040009CF RID: 2511
			public const string Error = "ERROR";

			// Token: 0x040009D0 RID: 2512
			public const string Warning = "WARNING";

			// Token: 0x040009D1 RID: 2513
			public const string Performance = "PERFORMANCE";
		}
	}
}
