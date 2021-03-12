using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200001A RID: 26
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetMembershipChangedAfterAssociations : GetAssociationCommand
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00007C5D File Offset: 0x00005E5D
		public GetMembershipChangedAfterAssociations(IAssociationAdaptor adaptor, ExDateTime date)
		{
			this.AssociationAdaptor = adaptor;
			this.changeDate = date;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00007C73 File Offset: 0x00005E73
		public override IEnumerable<MailboxAssociation> Execute(int? maxItems = null)
		{
			return this.AssociationAdaptor.GetAssociationsWithMembershipChangedAfter(this.changeDate);
		}

		// Token: 0x0400005C RID: 92
		public readonly IAssociationAdaptor AssociationAdaptor;

		// Token: 0x0400005D RID: 93
		private readonly ExDateTime changeDate;
	}
}
