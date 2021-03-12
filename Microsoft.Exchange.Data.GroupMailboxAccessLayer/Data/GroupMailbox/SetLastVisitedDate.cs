using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000022 RID: 34
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetLastVisitedDate : UpdateAssociationCommand
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00008C84 File Offset: 0x00006E84
		public SetLastVisitedDate(IExtensibleLogger logger, ExDateTime lastVisitedDate, IUserAssociationAdaptor masterAdaptor, UserMailboxLocator itemLocator) : base(logger, masterAdaptor, new IMailboxLocator[]
		{
			itemLocator
		})
		{
			this.lastVisitedDate = lastVisitedDate;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00008CAD File Offset: 0x00006EAD
		protected override bool UpdateAssociation(MailboxAssociation association)
		{
			association.LastVisitedDate = this.lastVisitedDate;
			return true;
		}

		// Token: 0x0400007D RID: 125
		private readonly ExDateTime lastVisitedDate;
	}
}
