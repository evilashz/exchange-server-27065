using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DA1 RID: 3489
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISharingSubscriptionData<TKey> : ISharingSubscriptionData
	{
		// Token: 0x17002014 RID: 8212
		// (get) Token: 0x060077EF RID: 30703
		TKey Key { get; }
	}
}
