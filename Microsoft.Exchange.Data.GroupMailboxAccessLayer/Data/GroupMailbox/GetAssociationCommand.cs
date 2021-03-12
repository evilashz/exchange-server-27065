using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class GetAssociationCommand
	{
		// Token: 0x060000F5 RID: 245
		public abstract IEnumerable<MailboxAssociation> Execute(int? maxItems = null);
	}
}
