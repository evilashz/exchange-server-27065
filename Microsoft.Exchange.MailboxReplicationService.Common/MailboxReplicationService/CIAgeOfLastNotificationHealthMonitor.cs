using System;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000265 RID: 613
	internal class CIAgeOfLastNotificationHealthMonitor : WlmResourceHealthMonitor
	{
		// Token: 0x06001F05 RID: 7941 RVA: 0x00040A28 File Offset: 0x0003EC28
		public CIAgeOfLastNotificationHealthMonitor(WlmResource owner, Guid mdbGuid) : base(owner, new CiAgeOfLastNotificationResourceKey(mdbGuid))
		{
			MailboxReplicationServicePerMdbPerformanceCountersInstance perfCounter = MDBPerfCounterHelperCollection.GetMDBHelper(mdbGuid, true).PerfCounter;
			WorkloadType wlmWorkloadType = owner.WlmWorkloadType;
			if (wlmWorkloadType == WorkloadType.MailboxReplicationService)
			{
				base.ResourceHealthPerfCounter = perfCounter.ResourceHealthCIAgeOfLastNotification;
				base.DynamicCapacityPerfCounter = perfCounter.DynamicCapacityCIAgeOfLastNotification;
				return;
			}
			if (wlmWorkloadType == WorkloadType.MailboxReplicationServiceHighPriority)
			{
				base.ResourceHealthPerfCounter = perfCounter.ResourceHealthCIAgeOfLastNotificationHiPri;
				base.DynamicCapacityPerfCounter = perfCounter.DynamicCapacityCIAgeOfLastNotificationHiPri;
				return;
			}
			switch (wlmWorkloadType)
			{
			case WorkloadType.MailboxReplicationServiceInternalMaintenance:
				base.ResourceHealthPerfCounter = perfCounter.ResourceHealthCIAgeOfLastNotificationInternalMaintenance;
				base.DynamicCapacityPerfCounter = perfCounter.DynamicCapacityCIAgeOfLastNotificationInternalMaintenance;
				return;
			case WorkloadType.MailboxReplicationServiceInteractive:
				base.ResourceHealthPerfCounter = perfCounter.ResourceHealthCIAgeOfLastNotificationCustomerExpectation;
				base.DynamicCapacityPerfCounter = perfCounter.DynamicCapacityCIAgeOfLastNotificationCustomerExpectation;
				return;
			default:
				return;
			}
		}
	}
}
