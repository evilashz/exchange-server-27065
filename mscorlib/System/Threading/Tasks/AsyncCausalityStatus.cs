using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000550 RID: 1360
	[FriendAccessAllowed]
	internal enum AsyncCausalityStatus
	{
		// Token: 0x04001AC3 RID: 6851
		Canceled = 2,
		// Token: 0x04001AC4 RID: 6852
		Completed = 1,
		// Token: 0x04001AC5 RID: 6853
		Error = 3,
		// Token: 0x04001AC6 RID: 6854
		Started = 0
	}
}
