using System;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x02000071 RID: 113
	[Flags]
	public enum PermissionCheckResults
	{
		// Token: 0x040001C7 RID: 455
		None = 0,
		// Token: 0x040001C8 RID: 456
		Allow = 1,
		// Token: 0x040001C9 RID: 457
		AdministratorDeny = 2,
		// Token: 0x040001CA RID: 458
		MachineDeny = 4,
		// Token: 0x040001CB RID: 459
		Deny = 6
	}
}
