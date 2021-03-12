using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200022C RID: 556
	internal enum SenderIdStatus : uint
	{
		// Token: 0x0400103B RID: 4155
		Neutral = 1U,
		// Token: 0x0400103C RID: 4156
		Pass,
		// Token: 0x0400103D RID: 4157
		Fail,
		// Token: 0x0400103E RID: 4158
		SoftFail,
		// Token: 0x0400103F RID: 4159
		None,
		// Token: 0x04001040 RID: 4160
		TempError = 2147483654U,
		// Token: 0x04001041 RID: 4161
		PermError
	}
}
