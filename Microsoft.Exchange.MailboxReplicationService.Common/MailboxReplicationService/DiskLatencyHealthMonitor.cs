using System;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200026B RID: 619
	internal class DiskLatencyHealthMonitor : WlmResourceHealthMonitor
	{
		// Token: 0x06001F3C RID: 7996 RVA: 0x00041608 File Offset: 0x0003F808
		public DiskLatencyHealthMonitor(WlmResource owner, Guid mdbGuid) : base(owner, new DiskLatencyResourceKey(mdbGuid))
		{
			MailboxReplicationServicePerMdbPerformanceCountersInstance perfCounter = MDBPerfCounterHelperCollection.GetMDBHelper(mdbGuid, true).PerfCounter;
			WorkloadType wlmWorkloadType = owner.WlmWorkloadType;
			if (wlmWorkloadType == WorkloadType.MailboxReplicationService)
			{
				base.ResourceHealthPerfCounter = perfCounter.ResourceHealthDiskLatency;
				base.DynamicCapacityPerfCounter = perfCounter.DynamicCapacityDiskLatency;
				return;
			}
			if (wlmWorkloadType == WorkloadType.MailboxReplicationServiceHighPriority)
			{
				base.ResourceHealthPerfCounter = perfCounter.ResourceHealthDiskLatencyHiPri;
				base.DynamicCapacityPerfCounter = perfCounter.DynamicCapacityDiskLatencyHiPri;
				return;
			}
			switch (wlmWorkloadType)
			{
			case WorkloadType.MailboxReplicationServiceInternalMaintenance:
				base.ResourceHealthPerfCounter = perfCounter.ResourceHealthDiskLatencyInternalMaintenance;
				base.DynamicCapacityPerfCounter = perfCounter.DynamicCapacityDiskLatencyInternalMaintenance;
				return;
			case WorkloadType.MailboxReplicationServiceInteractive:
				base.ResourceHealthPerfCounter = perfCounter.ResourceHealthDiskLatencyCustomerExpectation;
				base.DynamicCapacityPerfCounter = perfCounter.DynamicCapacityDiskLatencyCustomerExpectation;
				return;
			default:
				return;
			}
		}
	}
}
