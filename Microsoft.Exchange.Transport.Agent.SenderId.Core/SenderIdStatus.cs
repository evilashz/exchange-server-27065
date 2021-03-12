using System;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000008 RID: 8
	internal enum SenderIdStatus
	{
		// Token: 0x04000010 RID: 16
		Pass = 1,
		// Token: 0x04000011 RID: 17
		Neutral,
		// Token: 0x04000012 RID: 18
		SoftFail,
		// Token: 0x04000013 RID: 19
		Fail,
		// Token: 0x04000014 RID: 20
		None,
		// Token: 0x04000015 RID: 21
		TempError,
		// Token: 0x04000016 RID: 22
		PermError
	}
}
