using System;
using Microsoft.Exchange.Data.Storage.ResourceHealth;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200027C RID: 636
	internal class MDBLatencyHealthMonitor : WlmResourceHealthMonitor
	{
		// Token: 0x06001F8D RID: 8077 RVA: 0x000424F4 File Offset: 0x000406F4
		public MDBLatencyHealthMonitor(WlmResource owner, Guid mdbGuid) : base(owner, new MdbResourceHealthMonitorKey(mdbGuid))
		{
			MailboxReplicationServicePerMdbPerformanceCountersInstance perfCounter = MDBPerfCounterHelperCollection.GetMDBHelper(mdbGuid, true).PerfCounter;
			WorkloadType wlmWorkloadType = owner.WlmWorkloadType;
			if (wlmWorkloadType == WorkloadType.MailboxReplicationService)
			{
				base.ResourceHealthPerfCounter = perfCounter.ResourceHealthMDBLatency;
				base.DynamicCapacityPerfCounter = perfCounter.DynamicCapacityMDBLatency;
				return;
			}
			if (wlmWorkloadType == WorkloadType.MailboxReplicationServiceHighPriority)
			{
				base.ResourceHealthPerfCounter = perfCounter.ResourceHealthMDBLatencyHiPri;
				base.DynamicCapacityPerfCounter = perfCounter.DynamicCapacityMDBLatencyHiPri;
				return;
			}
			switch (wlmWorkloadType)
			{
			case WorkloadType.MailboxReplicationServiceInternalMaintenance:
				base.ResourceHealthPerfCounter = perfCounter.ResourceHealthMDBLatencyInternalMaintenance;
				base.DynamicCapacityPerfCounter = perfCounter.DynamicCapacityMDBLatencyInternalMaintenance;
				return;
			case WorkloadType.MailboxReplicationServiceInteractive:
				base.ResourceHealthPerfCounter = perfCounter.ResourceHealthMDBLatencyCustomerExpectation;
				base.DynamicCapacityPerfCounter = perfCounter.DynamicCapacityMDBLatencyCustomerExpectation;
				return;
			default:
				return;
			}
		}
	}
}
