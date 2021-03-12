﻿using System;

namespace System.Security.AccessControl
{
	// Token: 0x020001FF RID: 511
	[Flags]
	public enum AceFlags : byte
	{
		// Token: 0x04000ADA RID: 2778
		None = 0,
		// Token: 0x04000ADB RID: 2779
		ObjectInherit = 1,
		// Token: 0x04000ADC RID: 2780
		ContainerInherit = 2,
		// Token: 0x04000ADD RID: 2781
		NoPropagateInherit = 4,
		// Token: 0x04000ADE RID: 2782
		InheritOnly = 8,
		// Token: 0x04000ADF RID: 2783
		Inherited = 16,
		// Token: 0x04000AE0 RID: 2784
		SuccessfulAccess = 64,
		// Token: 0x04000AE1 RID: 2785
		FailedAccess = 128,
		// Token: 0x04000AE2 RID: 2786
		InheritanceFlags = 15,
		// Token: 0x04000AE3 RID: 2787
		AuditFlags = 192
	}
}
