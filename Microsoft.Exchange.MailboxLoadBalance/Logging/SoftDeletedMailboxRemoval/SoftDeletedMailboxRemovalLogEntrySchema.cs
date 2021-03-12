using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Logging.MailboxStatistics;

namespace Microsoft.Exchange.MailboxLoadBalance.Logging.SoftDeletedMailboxRemoval
{
	// Token: 0x020000B2 RID: 178
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SoftDeletedMailboxRemovalLogEntrySchema : ObjectSchema
	{
		// Token: 0x04000232 RID: 562
		public static readonly ProviderPropertyDefinition CurrentDatabaseSize = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<ByteQuantifiedSize?>("CurrentDatabaseSize");

		// Token: 0x04000233 RID: 563
		public static readonly ProviderPropertyDefinition DatabaseName = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<string>("DatabaseName");

		// Token: 0x04000234 RID: 564
		public static readonly ProviderPropertyDefinition Error = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<Exception>("Error");

		// Token: 0x04000235 RID: 565
		public static readonly ProviderPropertyDefinition MailboxGuid = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<Guid?>("MailboxGuid");

		// Token: 0x04000236 RID: 566
		public static readonly ProviderPropertyDefinition OriginalDatabaseSize = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<ByteQuantifiedSize?>("OriginalDatabaseSize");

		// Token: 0x04000237 RID: 567
		public static readonly ProviderPropertyDefinition RemovalAllowed = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<bool?>("RemovalAllowed");

		// Token: 0x04000238 RID: 568
		public static readonly ProviderPropertyDefinition RemovalDisallowedReason = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<string>("RemovalDisallowedReason");

		// Token: 0x04000239 RID: 569
		public static readonly ProviderPropertyDefinition Removed = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<bool?>("Removed");

		// Token: 0x0400023A RID: 570
		public static readonly ProviderPropertyDefinition TargetDatabaseSize = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<ByteQuantifiedSize?>("TargetDatabaseSize");

		// Token: 0x0400023B RID: 571
		public static readonly ProviderPropertyDefinition MailboxSize = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<ByteQuantifiedSize?>("MailboxSize");

		// Token: 0x020000B3 RID: 179
		internal class SoftDeletedMailboxRemovalLogEntryLogSchema : ConfigurableObjectLogSchema<SoftDeletedMailboxRemovalLogEntry, SoftDeletedMailboxRemovalLogEntrySchema>
		{
			// Token: 0x170001F9 RID: 505
			// (get) Token: 0x06000601 RID: 1537 RVA: 0x0000FC2F File Offset: 0x0000DE2F
			public override string LogType
			{
				get
				{
					return "Soft Deleted Mailbox Removal";
				}
			}
		}
	}
}
