using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000019 RID: 25
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetMembershipAssociations : GetAssociationCommand
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00007C40 File Offset: 0x00005E40
		public GetMembershipAssociations(IAssociationAdaptor adaptor)
		{
			this.AssociationAdaptor = adaptor;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007C4F File Offset: 0x00005E4F
		public override IEnumerable<MailboxAssociation> Execute(int? maxItems = null)
		{
			return this.AssociationAdaptor.GetMembershipAssociations(maxItems);
		}

		// Token: 0x0400005B RID: 91
		public readonly IAssociationAdaptor AssociationAdaptor;
	}
}
