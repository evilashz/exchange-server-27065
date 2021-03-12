using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;
using Microsoft.Exchange.WorkloadManagement.EventLogs;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200000F RID: 15
	internal class ClassificationPerfCounterWrapper
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000030F8 File Offset: 0x000012F8
		public ClassificationPerfCounterWrapper(WorkloadClassification classification)
		{
			this.Classification = classification;
			string text = null;
			try
			{
				text = ResourceLoadPerfCounterWrapper.GetDefaultInstanceName();
				text = text + "_" + classification;
				this.perfCounters = MSExchangeWorkloadManagementClassification.GetInstance(text);
				ExTraceGlobals.CommonTracer.TraceDebug<string>((long)this.GetHashCode(), "[ClassificationPerfCounterWrapper.ctor] Creating perf counter wrapper instance for '{0}'", text);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.CommonTracer.TraceError<string, Exception>((long)this.GetHashCode(), "[ClassificationPerfCounterWrapper.ctor] Failed to create perf counter instance '{0}'.  Exception: {1}", text ?? "<NULL>", ex);
				WorkloadManagerEventLogger.LogEvent(WorkloadManagementEventLogConstants.Tuple_ClassificationPerformanceCounterInitializationFailure, classification.ToString(), new object[]
				{
					classification,
					ex
				});
				this.perfCounters = null;
			}
			this.UpdateActiveThreads(0L);
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000031C0 File Offset: 0x000013C0
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000031C8 File Offset: 0x000013C8
		public WorkloadClassification Classification { get; private set; }

		// Token: 0x0600008D RID: 141 RVA: 0x000031D1 File Offset: 0x000013D1
		public void UpdateWorkloadCount(long workloadCount)
		{
			if (this.perfCounters != null)
			{
				this.perfCounters.WorkloadCount.RawValue = workloadCount;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000031EC File Offset: 0x000013EC
		public void UpdateActiveThreads(long activeThreadCount)
		{
			if (this.perfCounters != null)
			{
				this.perfCounters.ActiveThreadCount.RawValue = activeThreadCount;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003207 File Offset: 0x00001407
		public void UpdateFairnessFactor(long fairnessFactor)
		{
			if (this.perfCounters != null)
			{
				this.perfCounters.FairnessFactor.RawValue = fairnessFactor;
			}
		}

		// Token: 0x0400003A RID: 58
		private MSExchangeWorkloadManagementClassificationInstance perfCounters;
	}
}
