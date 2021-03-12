using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000E0 RID: 224
	[Flags]
	public enum SubscriptionKind : uint
	{
		// Token: 0x04000514 RID: 1300
		PreCommit = 2U,
		// Token: 0x04000515 RID: 1301
		PostCommit = 12U
	}
}
