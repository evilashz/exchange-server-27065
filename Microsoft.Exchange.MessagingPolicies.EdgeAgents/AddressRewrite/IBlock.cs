using System;

namespace Microsoft.Exchange.MessagingPolicies.AddressRewrite
{
	// Token: 0x0200001A RID: 26
	internal abstract class IBlock
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000071 RID: 113
		internal abstract int Written { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000072 RID: 114
		internal abstract int Free { get; }
	}
}
