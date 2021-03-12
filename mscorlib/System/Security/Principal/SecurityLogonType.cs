using System;

namespace System.Security.Principal
{
	// Token: 0x02000301 RID: 769
	[Serializable]
	internal enum SecurityLogonType
	{
		// Token: 0x04000F93 RID: 3987
		Interactive = 2,
		// Token: 0x04000F94 RID: 3988
		Network,
		// Token: 0x04000F95 RID: 3989
		Batch,
		// Token: 0x04000F96 RID: 3990
		Service,
		// Token: 0x04000F97 RID: 3991
		Proxy,
		// Token: 0x04000F98 RID: 3992
		Unlock
	}
}
