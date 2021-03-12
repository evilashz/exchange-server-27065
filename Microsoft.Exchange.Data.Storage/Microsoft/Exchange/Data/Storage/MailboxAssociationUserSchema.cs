using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C7F RID: 3199
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxAssociationUserSchema : MailboxAssociationBaseSchema
	{
		// Token: 0x06007033 RID: 28723 RVA: 0x001F0CE5 File Offset: 0x001EEEE5
		private MailboxAssociationUserSchema()
		{
		}

		// Token: 0x17001E26 RID: 7718
		// (get) Token: 0x06007034 RID: 28724 RVA: 0x001F0CED File Offset: 0x001EEEED
		public new static MailboxAssociationUserSchema Instance
		{
			get
			{
				if (MailboxAssociationUserSchema.instance == null)
				{
					MailboxAssociationUserSchema.instance = new MailboxAssociationUserSchema();
				}
				return MailboxAssociationUserSchema.instance;
			}
		}

		// Token: 0x04004C93 RID: 19603
		private static MailboxAssociationUserSchema instance = null;

		// Token: 0x04004C94 RID: 19604
		[Autoload]
		public static readonly StorePropertyDefinition JoinedBy = InternalSchema.MailboxAssociationJoinedBy;

		// Token: 0x04004C95 RID: 19605
		[Autoload]
		public static readonly StorePropertyDefinition LastVisitedDate = InternalSchema.MailboxAssociationLastVisitedDate;
	}
}
