using System;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x0200001E RID: 30
	[Flags]
	public enum AuthzFlags
	{
		// Token: 0x0400008F RID: 143
		Default = 0,
		// Token: 0x04000090 RID: 144
		AuthzSkipTokenGroups = 2,
		// Token: 0x04000091 RID: 145
		AuthzRequireS4ULogon = 4
	}
}
