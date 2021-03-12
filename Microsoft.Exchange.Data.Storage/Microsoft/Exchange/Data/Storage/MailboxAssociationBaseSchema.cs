using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C7D RID: 3197
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxAssociationBaseSchema : ItemSchema
	{
		// Token: 0x17001E24 RID: 7716
		// (get) Token: 0x0600702D RID: 28717 RVA: 0x001F0BF3 File Offset: 0x001EEDF3
		public new static MailboxAssociationBaseSchema Instance
		{
			get
			{
				if (MailboxAssociationBaseSchema.instance == null)
				{
					MailboxAssociationBaseSchema.instance = new MailboxAssociationBaseSchema();
				}
				return MailboxAssociationBaseSchema.instance;
			}
		}

		// Token: 0x04004C82 RID: 19586
		private static MailboxAssociationBaseSchema instance = null;

		// Token: 0x04004C83 RID: 19587
		[Autoload]
		public static readonly StorePropertyDefinition LegacyDN = InternalSchema.MailboxAssociationLegacyDN;

		// Token: 0x04004C84 RID: 19588
		[Autoload]
		public static readonly StorePropertyDefinition ExternalId = InternalSchema.MailboxAssociationExternalId;

		// Token: 0x04004C85 RID: 19589
		[Autoload]
		public static readonly StorePropertyDefinition IsMember = InternalSchema.MailboxAssociationIsMember;

		// Token: 0x04004C86 RID: 19590
		[Autoload]
		public static readonly StorePropertyDefinition ShouldEscalate = InternalSchema.MailboxAssociationShouldEscalate;

		// Token: 0x04004C87 RID: 19591
		[Autoload]
		public static readonly StorePropertyDefinition IsAutoSubscribed = InternalSchema.MailboxAssociationIsAutoSubscribed;

		// Token: 0x04004C88 RID: 19592
		[Autoload]
		public static readonly StorePropertyDefinition IsPin = InternalSchema.MailboxAssociationIsPin;

		// Token: 0x04004C89 RID: 19593
		[Autoload]
		public static readonly StorePropertyDefinition JoinDate = InternalSchema.MailboxAssociationJoinDate;

		// Token: 0x04004C8A RID: 19594
		[Autoload]
		public static readonly StorePropertyDefinition SmtpAddress = InternalSchema.MailboxAssociationSmtpAddress;

		// Token: 0x04004C8B RID: 19595
		[Autoload]
		public static readonly StorePropertyDefinition SyncedIdentityHash = InternalSchema.MailboxAssociationSyncedIdentityHash;

		// Token: 0x04004C8C RID: 19596
		[Autoload]
		public static readonly StorePropertyDefinition CurrentVersion = InternalSchema.MailboxAssociationCurrentVersion;

		// Token: 0x04004C8D RID: 19597
		[Autoload]
		public static readonly StorePropertyDefinition SyncedVersion = InternalSchema.MailboxAssociationSyncedVersion;

		// Token: 0x04004C8E RID: 19598
		[Autoload]
		public static readonly StorePropertyDefinition LastSyncError = InternalSchema.MailboxAssociationLastSyncError;

		// Token: 0x04004C8F RID: 19599
		[Autoload]
		public static readonly StorePropertyDefinition SyncAttempts = InternalSchema.MailboxAssociationSyncAttempts;

		// Token: 0x04004C90 RID: 19600
		[Autoload]
		public static readonly StorePropertyDefinition SyncedSchemaVersion = InternalSchema.MailboxAssociationSyncedSchemaVersion;
	}
}
