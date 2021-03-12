using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000377 RID: 887
	internal enum RuleActionType : byte
	{
		// Token: 0x04000B3D RID: 2877
		Move = 1,
		// Token: 0x04000B3E RID: 2878
		Copy,
		// Token: 0x04000B3F RID: 2879
		Reply,
		// Token: 0x04000B40 RID: 2880
		OutOfOfficeReply,
		// Token: 0x04000B41 RID: 2881
		DeferAction,
		// Token: 0x04000B42 RID: 2882
		Bounce,
		// Token: 0x04000B43 RID: 2883
		Forward,
		// Token: 0x04000B44 RID: 2884
		Delegate,
		// Token: 0x04000B45 RID: 2885
		Tag,
		// Token: 0x04000B46 RID: 2886
		Delete,
		// Token: 0x04000B47 RID: 2887
		MarkAsRead
	}
}
