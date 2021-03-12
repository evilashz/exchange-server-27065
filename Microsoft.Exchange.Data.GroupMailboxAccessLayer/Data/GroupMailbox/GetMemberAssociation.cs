using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000018 RID: 24
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetMemberAssociation
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00007C17 File Offset: 0x00005E17
		public GetMemberAssociation(IAssociationAdaptor adaptor, IMailboxLocator user)
		{
			this.user = user;
			this.associationAdaptor = adaptor;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00007C2D File Offset: 0x00005E2D
		public MailboxAssociation Execute()
		{
			return this.associationAdaptor.GetAssociation(this.user);
		}

		// Token: 0x04000059 RID: 89
		private readonly IAssociationAdaptor associationAdaptor;

		// Token: 0x0400005A RID: 90
		private readonly IMailboxLocator user;
	}
}
