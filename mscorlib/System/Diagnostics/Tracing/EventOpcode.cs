using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200040A RID: 1034
	[FriendAccessAllowed]
	[__DynamicallyInvokable]
	public enum EventOpcode
	{
		// Token: 0x04001736 RID: 5942
		[__DynamicallyInvokable]
		Info,
		// Token: 0x04001737 RID: 5943
		[__DynamicallyInvokable]
		Start,
		// Token: 0x04001738 RID: 5944
		[__DynamicallyInvokable]
		Stop,
		// Token: 0x04001739 RID: 5945
		[__DynamicallyInvokable]
		DataCollectionStart,
		// Token: 0x0400173A RID: 5946
		[__DynamicallyInvokable]
		DataCollectionStop,
		// Token: 0x0400173B RID: 5947
		[__DynamicallyInvokable]
		Extension,
		// Token: 0x0400173C RID: 5948
		[__DynamicallyInvokable]
		Reply,
		// Token: 0x0400173D RID: 5949
		[__DynamicallyInvokable]
		Resume,
		// Token: 0x0400173E RID: 5950
		[__DynamicallyInvokable]
		Suspend,
		// Token: 0x0400173F RID: 5951
		[__DynamicallyInvokable]
		Send,
		// Token: 0x04001740 RID: 5952
		[__DynamicallyInvokable]
		Receive = 240
	}
}
