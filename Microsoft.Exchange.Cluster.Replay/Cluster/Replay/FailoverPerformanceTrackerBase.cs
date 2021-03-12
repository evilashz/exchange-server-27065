using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002C0 RID: 704
	internal abstract class FailoverPerformanceTrackerBase<TOpCode>
	{
		// Token: 0x06001B40 RID: 6976 RVA: 0x0007579D File Offset: 0x0007399D
		protected FailoverPerformanceTrackerBase(string tracerName)
		{
			this.m_tracerName = tracerName;
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x000757B9 File Offset: 0x000739B9
		public void RecordDuration(TOpCode opCode, TimeSpan duration)
		{
			this.m_durations[opCode] = duration;
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x000757C8 File Offset: 0x000739C8
		public void RunTimedOperation(TOpCode opCode, Action operation)
		{
			bool flag = false;
			ReplayStopwatch replayStopwatch = new ReplayStopwatch();
			replayStopwatch.Start();
			try
			{
				operation();
				flag = true;
			}
			finally
			{
				replayStopwatch.Stop();
				if (flag)
				{
					ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, TOpCode, string>((long)this.GetHashCode(), "{0}: Operation '{1}' completed successfully in {2}.", this.m_tracerName, opCode, replayStopwatch.ToString());
				}
				else
				{
					ExTraceGlobals.ReplicaInstanceTracer.TraceError<string, TOpCode, string>((long)this.GetHashCode(), "{0}: Operation '{1}' completed with an exception in {2}.", this.m_tracerName, opCode, replayStopwatch.ToString());
				}
				this.RecordDuration(opCode, replayStopwatch.Elapsed);
			}
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0007585C File Offset: 0x00073A5C
		protected TimeSpan GetDuration(TOpCode opCode)
		{
			if (this.m_durations.ContainsKey(opCode))
			{
				return this.m_durations[opCode];
			}
			return TimeSpan.Zero;
		}

		// Token: 0x06001B44 RID: 6980
		public abstract void LogEvent();

		// Token: 0x04000B32 RID: 2866
		protected string m_tracerName;

		// Token: 0x04000B33 RID: 2867
		private Dictionary<TOpCode, TimeSpan> m_durations = new Dictionary<TOpCode, TimeSpan>(20);
	}
}
