using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C7E RID: 3198
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxAssociationGroupSchema : MailboxAssociationBaseSchema
	{
		// Token: 0x06007030 RID: 28720 RVA: 0x001F0CB3 File Offset: 0x001EEEB3
		private MailboxAssociationGroupSchema()
		{
		}

		// Token: 0x17001E25 RID: 7717
		// (get) Token: 0x06007031 RID: 28721 RVA: 0x001F0CBB File Offset: 0x001EEEBB
		public new static MailboxAssociationGroupSchema Instance
		{
			get
			{
				if (MailboxAssociationGroupSchema.instance == null)
				{
					MailboxAssociationGroupSchema.instance = new MailboxAssociationGroupSchema();
				}
				return MailboxAssociationGroupSchema.instance;
			}
		}

		// Token: 0x04004C91 RID: 19601
		private static MailboxAssociationGroupSchema instance = null;

		// Token: 0x04004C92 RID: 19602
		[Autoload]
		public static readonly StorePropertyDefinition PinDate = InternalSchema.MailboxAssociationPinDate;
	}
}
