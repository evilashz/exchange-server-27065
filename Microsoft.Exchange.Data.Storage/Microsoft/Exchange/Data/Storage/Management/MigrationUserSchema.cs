using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A41 RID: 2625
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationUserSchema : SimpleProviderObjectSchema
	{
		// Token: 0x0400368F RID: 13967
		public new static readonly ProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(MigrationUserId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003690 RID: 13968
		public static readonly ProviderPropertyDefinition BatchId = new SimpleProviderPropertyDefinition("BatchId", ExchangeObjectVersion.Exchange2010, typeof(MigrationBatchId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003691 RID: 13969
		public static readonly ProviderPropertyDefinition EmailAddress = new SimpleProviderPropertyDefinition("EmailAddress", ExchangeObjectVersion.Exchange2010, typeof(SmtpAddress), PropertyDefinitionFlags.None, SmtpAddress.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003692 RID: 13970
		public static readonly ProviderPropertyDefinition RecipientType = new SimpleProviderPropertyDefinition("RecipientType", ExchangeObjectVersion.Exchange2010, typeof(MigrationUserRecipientType), PropertyDefinitionFlags.PersistDefaultValue, MigrationUserRecipientType.Mailbox, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003693 RID: 13971
		public static readonly ProviderPropertyDefinition SkippedItemCount = new SimpleProviderPropertyDefinition("SkippedItemCount", ExchangeObjectVersion.Exchange2010, typeof(long), PropertyDefinitionFlags.PersistDefaultValue, 0L, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003694 RID: 13972
		public static readonly ProviderPropertyDefinition SyncedItemCount = new SimpleProviderPropertyDefinition("SyncedItemCount", ExchangeObjectVersion.Exchange2010, typeof(long), PropertyDefinitionFlags.PersistDefaultValue, 0L, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003695 RID: 13973
		public static readonly ProviderPropertyDefinition MailboxGuid = new SimpleProviderPropertyDefinition("MailboxGuid", ExchangeObjectVersion.Exchange2010, typeof(Guid), PropertyDefinitionFlags.PersistDefaultValue, Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003696 RID: 13974
		public static readonly ProviderPropertyDefinition MailboxLegacyDN = new SimpleProviderPropertyDefinition("MailboxLegacyDN", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003697 RID: 13975
		public static readonly ProviderPropertyDefinition MRSId = new SimpleProviderPropertyDefinition("MRSId", ExchangeObjectVersion.Exchange2010, typeof(Guid), PropertyDefinitionFlags.PersistDefaultValue, Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003698 RID: 13976
		public static readonly ProviderPropertyDefinition LastSuccessfulSyncTime = new SimpleProviderPropertyDefinition("LastSuccessfulSyncTime", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003699 RID: 13977
		public static readonly ProviderPropertyDefinition Status = new SimpleProviderPropertyDefinition("Status", ExchangeObjectVersion.Exchange2010, typeof(MigrationUserStatus), PropertyDefinitionFlags.PersistDefaultValue, MigrationUserStatus.Corrupted, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400369A RID: 13978
		public static readonly ProviderPropertyDefinition StatusSummary = new SimpleProviderPropertyDefinition("StatusSummary", ExchangeObjectVersion.Exchange2010, typeof(MigrationUserStatusSummary), PropertyDefinitionFlags.PersistDefaultValue, MigrationUserStatusSummary.Failed, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400369B RID: 13979
		public static readonly ProviderPropertyDefinition SubscriptionLastChecked = new SimpleProviderPropertyDefinition("SubscriptionLastChecked", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
