using System;

namespace System.Security.AccessControl
{
	// Token: 0x020001FE RID: 510
	public enum AceType : byte
	{
		// Token: 0x04000AC7 RID: 2759
		AccessAllowed,
		// Token: 0x04000AC8 RID: 2760
		AccessDenied,
		// Token: 0x04000AC9 RID: 2761
		SystemAudit,
		// Token: 0x04000ACA RID: 2762
		SystemAlarm,
		// Token: 0x04000ACB RID: 2763
		AccessAllowedCompound,
		// Token: 0x04000ACC RID: 2764
		AccessAllowedObject,
		// Token: 0x04000ACD RID: 2765
		AccessDeniedObject,
		// Token: 0x04000ACE RID: 2766
		SystemAuditObject,
		// Token: 0x04000ACF RID: 2767
		SystemAlarmObject,
		// Token: 0x04000AD0 RID: 2768
		AccessAllowedCallback,
		// Token: 0x04000AD1 RID: 2769
		AccessDeniedCallback,
		// Token: 0x04000AD2 RID: 2770
		AccessAllowedCallbackObject,
		// Token: 0x04000AD3 RID: 2771
		AccessDeniedCallbackObject,
		// Token: 0x04000AD4 RID: 2772
		SystemAuditCallback,
		// Token: 0x04000AD5 RID: 2773
		SystemAlarmCallback,
		// Token: 0x04000AD6 RID: 2774
		SystemAuditCallbackObject,
		// Token: 0x04000AD7 RID: 2775
		SystemAlarmCallbackObject,
		// Token: 0x04000AD8 RID: 2776
		MaxDefinedAceType = 16
	}
}
