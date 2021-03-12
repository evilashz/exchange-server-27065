using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000141 RID: 321
	[Flags]
	public enum GlsDomainFlags
	{
		// Token: 0x04000702 RID: 1794
		None = 0,
		// Token: 0x04000703 RID: 1795
		Nego2Enabled = 1,
		// Token: 0x04000704 RID: 1796
		OAuth2ClientProfileEnabled = 2,
		// Token: 0x04000705 RID: 1797
		Both = 3
	}
}
