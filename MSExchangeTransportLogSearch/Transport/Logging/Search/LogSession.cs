using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.LogSearch;
using Microsoft.Exchange.Rpc.LogSearch;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200002F RID: 47
	internal class LogSession
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00006DB8 File Offset: 0x00004FB8
		public LogSession(Guid id, Log log, Version schemaVersion)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "MsExchangeLogSearch construct LogSession with ID {0}", id.ToString());
			this.id = id;
			this.log = log;
			this.schemaVersion = schemaVersion;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00006E19 File Offset: 0x00005019
		public Guid Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00006E24 File Offset: 0x00005024
		public DateTime LastActivity
		{
			get
			{
				DateTime utcNow;
				lock (this.sync)
				{
					if (this.reading || this.search == null)
					{
						utcNow = DateTime.UtcNow;
					}
					else
					{
						utcNow = this.lastActivity;
					}
				}
				return utcNow;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006E80 File Offset: 0x00005080
		public void SetQuery(LogQuery query)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogSession SetQuery");
			LogEvaluator evaluator = LogEvaluator.FromCondition(query.Filter, this.log.Table);
			LogFilter filter = new LogFilter(this.log, this.schemaVersion, evaluator, query.Beginning, query.End);
			this.search = new LogBackgroundReader(filter, 32768);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006EEC File Offset: 0x000050EC
		public void BeginClose()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogSession BeginClose");
			lock (this.sync)
			{
				if (this.search != null)
				{
					this.search.BeginClose();
				}
			}
			this.stopwatch.Stop();
			long elapsedTicks = this.stopwatch.ElapsedTicks;
			ExTraceGlobals.ServiceTracer.TracePerformance<long>((long)this.GetHashCode(), "Total search time: {0}", elapsedTicks);
			PerfCounters.AverageProcessingTime.IncrementBy(elapsedTicks);
			PerfCounters.AverageProcessingTimeBase.Increment();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006F94 File Offset: 0x00005194
		public void EndClose()
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogSession EndClose");
			LogBackgroundReader logBackgroundReader;
			lock (this.sync)
			{
				while (this.reading)
				{
					Monitor.Wait(this.sync);
				}
				logBackgroundReader = this.search;
				this.search = null;
			}
			if (logBackgroundReader != null)
			{
				logBackgroundReader.EndClose();
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007014 File Offset: 0x00005214
		public int Read(byte[] dest, int offset, int count, out bool more, out int progress, bool cancel)
		{
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "MsExchangeLogSearch LogSession Read");
			lock (this.sync)
			{
				while (this.reading)
				{
					Monitor.Wait(this.sync);
				}
				if (this.search == null)
				{
					throw new LogSearchException(LogSearchErrorCode.LOGSEARCH_E_SESSION_CANCELED);
				}
				this.reading = true;
			}
			int result;
			try
			{
				result = this.search.Read(dest, offset, count, out more, out progress, cancel);
			}
			finally
			{
				lock (this.sync)
				{
					this.lastActivity = DateTime.UtcNow;
					this.reading = false;
					Monitor.PulseAll(this.sync);
				}
			}
			return result;
		}

		// Token: 0x04000094 RID: 148
		private const int BackgroundBufferSize = 32768;

		// Token: 0x04000095 RID: 149
		private Guid id;

		// Token: 0x04000096 RID: 150
		private DateTime lastActivity;

		// Token: 0x04000097 RID: 151
		private Log log;

		// Token: 0x04000098 RID: 152
		private LogBackgroundReader search;

		// Token: 0x04000099 RID: 153
		private bool reading;

		// Token: 0x0400009A RID: 154
		private object sync = new object();

		// Token: 0x0400009B RID: 155
		private Stopwatch stopwatch = Stopwatch.StartNew();

		// Token: 0x0400009C RID: 156
		private Version schemaVersion;
	}
}
