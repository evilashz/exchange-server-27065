using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000097 RID: 151
	[Flags]
	public enum EcpUserSettings : uint
	{
		// Token: 0x04000331 RID: 817
		Mail = 1U,
		// Token: 0x04000332 RID: 818
		Spelling = 2U,
		// Token: 0x04000333 RID: 819
		Calendar = 4U,
		// Token: 0x04000334 RID: 820
		General = 8U,
		// Token: 0x04000335 RID: 821
		Regional = 16U,
		// Token: 0x04000336 RID: 822
		Language = 32U
	}
}
