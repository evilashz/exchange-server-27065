using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Data
{
	// Token: 0x0200004A RID: 74
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NullEntitySelector : EntitySelector
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x000092A1 File Offset: 0x000074A1
		public override bool IsEmpty
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00009344 File Offset: 0x00007544
		public override IEnumerable<LoadEntity> GetEntities(LoadContainer targetContainer)
		{
			yield break;
		}
	}
}
