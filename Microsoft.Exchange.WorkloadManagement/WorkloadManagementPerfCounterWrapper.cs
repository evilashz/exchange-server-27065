using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;
using Microsoft.Exchange.WorkloadManagement.EventLogs;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000011 RID: 17
	internal class WorkloadManagementPerfCounterWrapper
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x000038D4 File Offset: 0x00001AD4
		public WorkloadManagementPerfCounterWrapper()
		{
			string text = null;
			try
			{
				text = ResourceLoadPerfCounterWrapper.GetDefaultInstanceName();
				this.perfCounters = MSExchangeWorkloadManagement.GetInstance(text);
				ExTraceGlobals.CommonTracer.TraceDebug<string>((long)this.GetHashCode(), "[WorkloadManagementPerfCounterWrapper.ctor] Creating perf counter wrapper instance for '{0}'", text);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.CommonTracer.TraceError<string, Exception>((long)this.GetHashCode(), "[WorkloadManagementPerfCounterWrapper.ctor] Failed to create perf counter instance '{0}'.  Exception: {1}", text ?? "<NULL>", ex);
				WorkloadManagerEventLogger.LogEvent(WorkloadManagementEventLogConstants.Tuple_WorkloadManagementPerformanceCounterInitializationFailure, text, new object[]
				{
					ex
				});
				this.perfCounters = null;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003968 File Offset: 0x00001B68
		public void UpdateWorkloadCount(long workloadCount)
		{
			if (this.perfCounters != null)
			{
				this.perfCounters.WorkloadCount.RawValue = workloadCount;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003983 File Offset: 0x00001B83
		public void UpdateActiveClassifications(long activeClassifications)
		{
			if (this.perfCounters != null)
			{
				this.perfCounters.ActiveClassifications.RawValue = activeClassifications;
			}
		}

		// Token: 0x0400004B RID: 75
		private MSExchangeWorkloadManagementInstance perfCounters;
	}
}
