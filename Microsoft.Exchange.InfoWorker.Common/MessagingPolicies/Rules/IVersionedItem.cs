using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000161 RID: 353
	internal interface IVersionedItem
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060009B9 RID: 2489
		string ID { get; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060009BA RID: 2490
		DateTime Version { get; }
	}
}
