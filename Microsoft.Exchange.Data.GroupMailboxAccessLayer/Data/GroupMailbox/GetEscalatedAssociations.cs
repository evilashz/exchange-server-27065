using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200001B RID: 27
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetEscalatedAssociations : GetAssociationCommand
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00007C86 File Offset: 0x00005E86
		public GetEscalatedAssociations(IAssociationAdaptor adaptor)
		{
			this.AssociationAdaptor = adaptor;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00007C98 File Offset: 0x00005E98
		public static int GetEscalatedAssociationsCount(IAssociationAdaptor adaptor)
		{
			GetEscalatedAssociations getEscalatedAssociations = new GetEscalatedAssociations(adaptor);
			IEnumerable<MailboxAssociation> source = getEscalatedAssociations.Execute(null);
			return source.ToArray<MailboxAssociation>().Count<MailboxAssociation>();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00007CC7 File Offset: 0x00005EC7
		public override IEnumerable<MailboxAssociation> Execute(int? maxItems = null)
		{
			return this.AssociationAdaptor.GetEscalatedAssociations();
		}

		// Token: 0x0400005E RID: 94
		public readonly IAssociationAdaptor AssociationAdaptor;
	}
}
