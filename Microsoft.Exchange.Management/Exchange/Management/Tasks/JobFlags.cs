using System;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000D9B RID: 3483
	[Flags]
	public enum JobFlags : uint
	{
		// Token: 0x040040AB RID: 16555
		None = 0U,
		// Token: 0x040040AC RID: 16556
		DetectOnly = 1U,
		// Token: 0x040040AD RID: 16557
		Background = 2U,
		// Token: 0x040040AE RID: 16558
		OnDemand = 4U,
		// Token: 0x040040AF RID: 16559
		System = 8U,
		// Token: 0x040040B0 RID: 16560
		Force = 16U,
		// Token: 0x040040B1 RID: 16561
		Verbose = 2147483648U
	}
}
