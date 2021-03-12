using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200001C RID: 28
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetPinAssociations : GetAssociationCommand
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00007CD4 File Offset: 0x00005ED4
		public GetPinAssociations(IAssociationAdaptor adaptor)
		{
			this.AssociationAdaptor = adaptor;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00007CE3 File Offset: 0x00005EE3
		public override IEnumerable<MailboxAssociation> Execute(int? maxItems = null)
		{
			return this.AssociationAdaptor.GetPinAssociations();
		}

		// Token: 0x0400005F RID: 95
		public readonly IAssociationAdaptor AssociationAdaptor;
	}
}
