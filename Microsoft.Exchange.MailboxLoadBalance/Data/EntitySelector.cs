using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Data
{
	// Token: 0x02000040 RID: 64
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class EntitySelector
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600025D RID: 605
		public abstract bool IsEmpty { get; }

		// Token: 0x0600025E RID: 606
		public abstract IEnumerable<LoadEntity> GetEntities(LoadContainer targetContainer);
	}
}
