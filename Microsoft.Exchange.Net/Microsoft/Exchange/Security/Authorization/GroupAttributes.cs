using System;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x0200001F RID: 31
	[CLSCompliant(false)]
	[Flags]
	public enum GroupAttributes : uint
	{
		// Token: 0x04000093 RID: 147
		Mandatory = 1U,
		// Token: 0x04000094 RID: 148
		EnabledByDefault = 2U,
		// Token: 0x04000095 RID: 149
		Enabled = 4U,
		// Token: 0x04000096 RID: 150
		Owner = 8U,
		// Token: 0x04000097 RID: 151
		UseForDenyOnly = 16U,
		// Token: 0x04000098 RID: 152
		Integrity = 32U,
		// Token: 0x04000099 RID: 153
		IntegrityEnabled = 64U,
		// Token: 0x0400009A RID: 154
		IntegrityEnabledDesktop = 128U,
		// Token: 0x0400009B RID: 155
		LogonId = 3221225472U,
		// Token: 0x0400009C RID: 156
		Resource = 536870912U
	}
}
