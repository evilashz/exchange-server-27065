using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000221 RID: 545
	internal enum ConflictResolutionPolicy
	{
		// Token: 0x04001004 RID: 4100
		ClientWins = 1,
		// Token: 0x04001005 RID: 4101
		ServerWins,
		// Token: 0x04001006 RID: 4102
		ConflictMessage,
		// Token: 0x04001007 RID: 4103
		DelegateResolution
	}
}
