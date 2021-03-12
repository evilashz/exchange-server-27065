using System;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200026E RID: 622
	internal class LocalCPUHealthMonitor : WlmResourceHealthMonitor
	{
		// Token: 0x06001F40 RID: 8000 RVA: 0x000416B4 File Offset: 0x0003F8B4
		public LocalCPUHealthMonitor(WlmResource owner) : base(owner, ProcessorResourceKey.Local)
		{
			WorkloadType wlmWorkloadType = owner.WlmWorkloadType;
			if (wlmWorkloadType == WorkloadType.MailboxReplicationService)
			{
				base.ResourceHealthPerfCounter = MailboxReplicationServicePerformanceCounters.LocalCPUResourceHealth;
				base.DynamicCapacityPerfCounter = MailboxReplicationServicePerformanceCounters.LocalCPUDynamicCapacity;
				return;
			}
			if (wlmWorkloadType == WorkloadType.MailboxReplicationServiceHighPriority)
			{
				base.ResourceHealthPerfCounter = MailboxReplicationServicePerformanceCounters.LocalCPUResourceHealthHiPri;
				base.DynamicCapacityPerfCounter = MailboxReplicationServicePerformanceCounters.LocalCPUDynamicCapacityHiPri;
				return;
			}
			switch (wlmWorkloadType)
			{
			case WorkloadType.MailboxReplicationServiceInternalMaintenance:
				base.ResourceHealthPerfCounter = MailboxReplicationServicePerformanceCounters.LocalCPUResourceHealthInternalMaintenance;
				base.DynamicCapacityPerfCounter = MailboxReplicationServicePerformanceCounters.LocalCPUDynamicCapacityInternalMaintenance;
				return;
			case WorkloadType.MailboxReplicationServiceInteractive:
				base.ResourceHealthPerfCounter = MailboxReplicationServicePerformanceCounters.LocalCPUResourceHealthCustomerExpectation;
				base.DynamicCapacityPerfCounter = MailboxReplicationServicePerformanceCounters.LocalCPUDynamicCapacityCustomerExpectation;
				return;
			default:
				return;
			}
		}
	}
}
