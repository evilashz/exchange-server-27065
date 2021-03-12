using System;
using System.Diagnostics;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000004 RID: 4
	public class LatencyTracker
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002214 File Offset: 0x00000414
		public LatencyTracker(LogData logData)
		{
			if (logData == null)
			{
				throw new ArgumentNullException("logData");
			}
			this.logData = logData;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002231 File Offset: 0x00000431
		protected LatencyTracker()
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002239 File Offset: 0x00000439
		public virtual void LogElapsedTimeInDetailedLatencyInfo(string key)
		{
			if (DiagnosticsConfiguration.DetailedLatencyTracingEnabled.Value)
			{
				this.logData.LogElapsedTimeInDetailedLatencyInfo(key);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002253 File Offset: 0x00000453
		public virtual void LogElapsedTimeInMilliseconds(LogKey key)
		{
			this.logData[key] = this.logData.GetElapsedTime();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002274 File Offset: 0x00000474
		public virtual void LogLatency(LogKey key, Action operationToTrack)
		{
			Stopwatch stopwatch = new Stopwatch();
			try
			{
				stopwatch.Start();
				operationToTrack();
			}
			finally
			{
				stopwatch.Stop();
				this.logData[key] = stopwatch.ElapsedMilliseconds;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022C4 File Offset: 0x000004C4
		public virtual T LogLatency<T>(LogKey key, Func<T> operationToTrack)
		{
			Stopwatch stopwatch = new Stopwatch();
			T result;
			try
			{
				stopwatch.Start();
				result = operationToTrack();
			}
			finally
			{
				stopwatch.Stop();
				this.logData[key] = stopwatch.ElapsedMilliseconds;
			}
			return result;
		}

		// Token: 0x0400000E RID: 14
		private LogData logData;
	}
}
