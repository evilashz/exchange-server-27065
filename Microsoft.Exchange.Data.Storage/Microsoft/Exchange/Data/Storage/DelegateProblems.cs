using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000238 RID: 568
	[Flags]
	internal enum DelegateProblems
	{
		// Token: 0x040010EA RID: 4330
		None = 0,
		// Token: 0x040010EB RID: 4331
		NoADUser = 2,
		// Token: 0x040010EC RID: 4332
		NoADPublicDelegate = 4,
		// Token: 0x040010ED RID: 4333
		NoDelegateInfo = 8,
		// Token: 0x040010EE RID: 4334
		InvalidReceiveMeetingMessageCopies = 16
	}
}
