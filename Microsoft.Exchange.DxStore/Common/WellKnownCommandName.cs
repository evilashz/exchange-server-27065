using System;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000009 RID: 9
	public enum WellKnownCommandName
	{
		// Token: 0x0400000F RID: 15
		Unknown,
		// Token: 0x04000010 RID: 16
		CreateKey,
		// Token: 0x04000011 RID: 17
		DeleteKey,
		// Token: 0x04000012 RID: 18
		SetProperty,
		// Token: 0x04000013 RID: 19
		DeleteProperty,
		// Token: 0x04000014 RID: 20
		ExecuteBatch,
		// Token: 0x04000015 RID: 21
		ApplySnapshot,
		// Token: 0x04000016 RID: 22
		PromoteToLeader,
		// Token: 0x04000017 RID: 23
		DummyCmd
	}
}
