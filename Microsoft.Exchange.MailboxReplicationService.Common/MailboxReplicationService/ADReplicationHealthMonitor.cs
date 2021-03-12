using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000264 RID: 612
	internal class ADReplicationHealthMonitor : WlmResourceHealthMonitor
	{
		// Token: 0x06001F04 RID: 7940 RVA: 0x00040990 File Offset: 0x0003EB90
		public ADReplicationHealthMonitor(WlmResource owner) : base(owner, ADResourceKey.Key)
		{
			WorkloadType wlmWorkloadType = owner.WlmWorkloadType;
			if (wlmWorkloadType == WorkloadType.MailboxReplicationService)
			{
				base.ResourceHealthPerfCounter = MailboxReplicationServicePerformanceCounters.ADReplicationResourceHealth;
				base.DynamicCapacityPerfCounter = MailboxReplicationServicePerformanceCounters.ADReplicationDynamicCapacity;
				return;
			}
			if (wlmWorkloadType == WorkloadType.MailboxReplicationServiceHighPriority)
			{
				base.ResourceHealthPerfCounter = MailboxReplicationServicePerformanceCounters.ADReplicationResourceHealthHiPri;
				base.DynamicCapacityPerfCounter = MailboxReplicationServicePerformanceCounters.ADReplicationDynamicCapacityHiPri;
				return;
			}
			switch (wlmWorkloadType)
			{
			case WorkloadType.MailboxReplicationServiceInternalMaintenance:
				base.ResourceHealthPerfCounter = MailboxReplicationServicePerformanceCounters.ADReplicationResourceHealthInternalMaintenance;
				base.DynamicCapacityPerfCounter = MailboxReplicationServicePerformanceCounters.ADReplicationDynamicCapacityInternalMaintenance;
				return;
			case WorkloadType.MailboxReplicationServiceInteractive:
				base.ResourceHealthPerfCounter = MailboxReplicationServicePerformanceCounters.ADReplicationResourceHealthCustomerExpectation;
				base.DynamicCapacityPerfCounter = MailboxReplicationServicePerformanceCounters.ADReplicationDynamicCapacityCustomerExpectation;
				return;
			default:
				return;
			}
		}
	}
}
