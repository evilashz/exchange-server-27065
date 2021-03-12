using System;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000067 RID: 103
	[Serializable]
	internal sealed class HealthSetEscalationState
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x000199DA File Offset: 0x00017BDA
		internal HealthSetEscalationState(string healthSetName, EscalationState escalationState, DateTime stateTransitionTime)
		{
			this.HealthSetName = healthSetName;
			this.EscalationState = escalationState;
			this.StateTransitionTime = stateTransitionTime;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x000199F7 File Offset: 0x00017BF7
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x000199FF File Offset: 0x00017BFF
		internal string HealthSetName { get; private set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00019A08 File Offset: 0x00017C08
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x00019A10 File Offset: 0x00017C10
		internal EscalationState EscalationState { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x00019A19 File Offset: 0x00017C19
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x00019A21 File Offset: 0x00017C21
		internal DateTime StateTransitionTime { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00019A2A File Offset: 0x00017C2A
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x00019A32 File Offset: 0x00017C32
		internal string LockOwnerId { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00019A3B File Offset: 0x00017C3B
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x00019A43 File Offset: 0x00017C43
		internal DateTime LockedUntilTime { get; set; }

		// Token: 0x06000619 RID: 1561 RVA: 0x00019A4C File Offset: 0x00017C4C
		internal void ResetToGreen()
		{
			this.EscalationState = EscalationState.Green;
			this.StateTransitionTime = DateTime.UtcNow;
			this.LockOwnerId = null;
			this.LockedUntilTime = DateTime.MinValue;
		}
	}
}
