using System;
using System.Collections.Generic;
using Microsoft.Forefront.RecoveryActionArbiter.Contract;

namespace Microsoft.Exchange.Monitoring.ServiceContextProvider
{
	// Token: 0x02000004 RID: 4
	public class RaaServiceNoOpStrategy : IRaaService
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002628 File Offset: 0x00000828
		public ApprovalResponse RequestApprovalForRecovery(ApprovalRequest request)
		{
			return new ApprovalResponse
			{
				ArbitrationResult = 1
			};
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002643 File Offset: 0x00000843
		public void NotifyRecoveryCompletion(string machineName, bool successfulRecovery)
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002645 File Offset: 0x00000845
		public ICollection<AvailabilityData> GetRoleAvailabilityData(string serviceInstance, string role)
		{
			return new List<AvailabilityData>();
		}
	}
}
