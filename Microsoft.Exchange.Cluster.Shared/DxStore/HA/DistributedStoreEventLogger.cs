using System;
using Microsoft.Exchange.Diagnostics.Components.DxStore;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Exchange.DxStore.HA.Events;

namespace Microsoft.Exchange.DxStore.HA
{
	// Token: 0x02000099 RID: 153
	public class DistributedStoreEventLogger : IDxStoreEventLogger
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x00014C8C File Offset: 0x00012E8C
		public DistributedStoreEventLogger(bool isLogToConsole = false)
		{
			this.isLogToConsole = isLogToConsole;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00014C9C File Offset: 0x00012E9C
		public DxStoreHACrimsonEvent GetEventBySeverity(DxEventSeverity severity)
		{
			switch (severity)
			{
			case DxEventSeverity.Error:
				return DxStoreHACrimsonEvents.ServerOperationError;
			case DxEventSeverity.Warning:
				return DxStoreHACrimsonEvents.ServerOperationWarning;
			default:
				return DxStoreHACrimsonEvents.ServerOperationInfo;
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00014CCC File Offset: 0x00012ECC
		public void Log(DxEventSeverity severity, int id, string formatString, params object[] args)
		{
			DxStoreHACrimsonEvent eventBySeverity = this.GetEventBySeverity(severity);
			string text = string.Format(formatString, args);
			eventBySeverity.LogGeneric(new object[]
			{
				id,
				text
			});
			string text2 = string.Format("[{0}] {1}: {2}", severity, id, text);
			if (this.isLogToConsole)
			{
				Console.WriteLine(text2);
			}
			switch (severity)
			{
			case DxEventSeverity.Error:
				ExTraceGlobals.EventLoggerTracer.TraceError((long)id, text2);
				return;
			case DxEventSeverity.Warning:
				ExTraceGlobals.EventLoggerTracer.TraceWarning((long)id, text2);
				return;
			default:
				ExTraceGlobals.EventLoggerTracer.TraceDebug((long)id, text2);
				return;
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00014D68 File Offset: 0x00012F68
		public void LogPeriodic(string periodicKey, TimeSpan periodicDuration, DxEventSeverity severity, int id, string formatString, params object[] args)
		{
			DxStoreHACrimsonEvent eventBySeverity = this.GetEventBySeverity(severity);
			string text = string.Format(formatString, args);
			eventBySeverity.LogPeriodicGeneric(periodicKey, periodicDuration, new object[]
			{
				id,
				text
			});
			if (this.isLogToConsole)
			{
				Console.WriteLine("[{0}] {1}: (P) {2}", severity, id, text);
			}
		}

		// Token: 0x04000307 RID: 775
		private readonly bool isLogToConsole;
	}
}
