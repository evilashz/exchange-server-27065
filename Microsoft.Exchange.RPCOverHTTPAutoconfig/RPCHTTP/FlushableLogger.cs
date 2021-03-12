using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.RPCHTTP
{
	// Token: 0x02000002 RID: 2
	internal class FlushableLogger
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public FlushableLogger(ExEventLog log)
		{
			this.eventQueue = new List<FlushableLogger.LogEventParams>();
			this.log = log;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F8 File Offset: 0x000002F8
		public void AddEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			lock (this.eventQueueLock)
			{
				this.eventQueue.Add(new FlushableLogger.LogEventParams(tuple, periodicKey, messageArgs));
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002148 File Offset: 0x00000348
		public void Flush()
		{
			List<FlushableLogger.LogEventParams> list;
			lock (this.eventQueueLock)
			{
				list = this.eventQueue;
				this.eventQueue = new List<FlushableLogger.LogEventParams>();
			}
			foreach (FlushableLogger.LogEventParams logEventParams in list)
			{
				this.log.LogEvent(logEventParams.tuple, logEventParams.periodicKey, logEventParams.messageArgs);
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly ExEventLog log;

		// Token: 0x04000002 RID: 2
		private readonly object eventQueueLock = new object();

		// Token: 0x04000003 RID: 3
		private List<FlushableLogger.LogEventParams> eventQueue;

		// Token: 0x02000003 RID: 3
		private struct LogEventParams
		{
			// Token: 0x06000004 RID: 4 RVA: 0x000021EC File Offset: 0x000003EC
			public LogEventParams(ExEventLog.EventTuple tuple, string periodicKey, object[] messageArgs)
			{
				this.tuple = tuple;
				this.periodicKey = periodicKey;
				this.messageArgs = messageArgs;
			}

			// Token: 0x04000004 RID: 4
			public readonly ExEventLog.EventTuple tuple;

			// Token: 0x04000005 RID: 5
			public readonly string periodicKey;

			// Token: 0x04000006 RID: 6
			public readonly object[] messageArgs;
		}
	}
}
