using System;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000065 RID: 101
	public class HealthSetEscalationHelper
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x000199CA File Offset: 0x00017BCA
		internal virtual HealthSetEscalationState LockHealthSetEscalationStateIfRequired(string healthSetName, EscalationState escalationState, string lockOwnerId)
		{
			return null;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000199CD File Offset: 0x00017BCD
		internal virtual bool SetHealthSetEscalationState(string healthSetName, EscalationState escalationState, string lockOwnerId)
		{
			return false;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000199D0 File Offset: 0x00017BD0
		internal virtual void ExtendEscalationMessage(string healthSetName, ref string escalationMessage)
		{
		}
	}
}
