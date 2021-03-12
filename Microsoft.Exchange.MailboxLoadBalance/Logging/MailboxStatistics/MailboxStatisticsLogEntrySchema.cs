using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Logging.MailboxStatistics
{
	// Token: 0x020000AE RID: 174
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MailboxStatisticsLogEntrySchema : ObjectSchema
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x0000F991 File Offset: 0x0000DB91
		public static SimpleProviderPropertyDefinition CreatePropertyDefinition<TProperty>(string propName)
		{
			return new SimpleProviderPropertyDefinition(propName, ExchangeObjectVersion.Exchange2003, typeof(TProperty), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x04000219 RID: 537
		public static readonly SimpleProviderPropertyDefinition AttachmentTableTotalSizeInBytes = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<ulong?>("AttachmentTableTotalSizeInBytes");

		// Token: 0x0400021A RID: 538
		public static readonly SimpleProviderPropertyDefinition DatabaseName = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<string>("DatabaseName");

		// Token: 0x0400021B RID: 539
		public static readonly SimpleProviderPropertyDefinition DeletedItemCount = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<ulong?>("DeletedItemCount");

		// Token: 0x0400021C RID: 540
		public static readonly SimpleProviderPropertyDefinition DisconnectDate = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<DateTime?>("DisconnectDate");

		// Token: 0x0400021D RID: 541
		public static readonly SimpleProviderPropertyDefinition MailboxState = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<MailboxState?>("MailboxState");

		// Token: 0x0400021E RID: 542
		public static readonly SimpleProviderPropertyDefinition ExternalDirectoryOrganizationId = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<Guid?>("ExternalDirectoryOrganizationId");

		// Token: 0x0400021F RID: 543
		public static readonly SimpleProviderPropertyDefinition IsArchiveMailbox = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<bool?>("IsArchiveMailbox");

		// Token: 0x04000220 RID: 544
		public static readonly SimpleProviderPropertyDefinition IsMoveDestination = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<bool?>("IsMoveDestination");

		// Token: 0x04000221 RID: 545
		public static readonly SimpleProviderPropertyDefinition IsQuarantined = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<bool?>("IsQuarantined");

		// Token: 0x04000222 RID: 546
		public static readonly SimpleProviderPropertyDefinition ItemCount = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<ulong?>("ItemCount");

		// Token: 0x04000223 RID: 547
		public static readonly SimpleProviderPropertyDefinition LastLogonTime = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<DateTime?>("LastLogonTime");

		// Token: 0x04000224 RID: 548
		public static readonly SimpleProviderPropertyDefinition LogicalSizeInM = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<ulong?>("LogicalSizeInM");

		// Token: 0x04000225 RID: 549
		public static readonly SimpleProviderPropertyDefinition MailboxGuid = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<Guid?>("MailboxGuid");

		// Token: 0x04000226 RID: 550
		public static readonly SimpleProviderPropertyDefinition RecipientGuid = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<Guid?>("RecipientGuid");

		// Token: 0x04000227 RID: 551
		public static readonly SimpleProviderPropertyDefinition MailboxType = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<LoadBalanceMailboxType?>("MailboxType");

		// Token: 0x04000228 RID: 552
		public static readonly SimpleProviderPropertyDefinition MessageTableTotalSizeInBytes = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<ulong?>("MessageTableTotalSizeInBytes");

		// Token: 0x04000229 RID: 553
		public static readonly SimpleProviderPropertyDefinition OtherTablesTotalSizeInBytes = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<ulong?>("OtherTablesTotalSizeInBytes");

		// Token: 0x0400022A RID: 554
		public static readonly SimpleProviderPropertyDefinition PhysicalSizeInM = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<ulong?>("PhysicalSizeInM");

		// Token: 0x0400022B RID: 555
		public static readonly SimpleProviderPropertyDefinition TotalDeletedItemSizeInBytes = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<ulong?>("TotalDeletedItemSizeInBytes");

		// Token: 0x0400022C RID: 556
		public static readonly SimpleProviderPropertyDefinition TotalItemSizeInBytes = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<ulong?>("TotalItemSizeInBytes");

		// Token: 0x0400022D RID: 557
		public static readonly SimpleProviderPropertyDefinition CreationTimestamp = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<DateTime?>("CreationTimestamp");

		// Token: 0x0400022E RID: 558
		public static readonly SimpleProviderPropertyDefinition MailboxProvisioningConstraint = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<string>("MailboxProvisioningConstraint");

		// Token: 0x0400022F RID: 559
		public static readonly SimpleProviderPropertyDefinition MailboxProvisioningPreferences = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<string>("MailboxProvisioningPreferences");

		// Token: 0x04000230 RID: 560
		public static readonly SimpleProviderPropertyDefinition MailboxProvisioningConstraintReason = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<string>("MailboxProvisioningConstraintReason");

		// Token: 0x04000231 RID: 561
		public static readonly SimpleProviderPropertyDefinition ItemsPendingUpgrade = MailboxStatisticsLogEntrySchema.CreatePropertyDefinition<int?>("ItemsPendingUpgrade");

		// Token: 0x020000AF RID: 175
		internal class MailboxStatisticsLogSchema : ConfigurableObjectLogSchema<MailboxStatisticsLogEntry, MailboxStatisticsLogEntrySchema>
		{
			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0000FB44 File Offset: 0x0000DD44
			public override string LogType
			{
				get
				{
					return "Mailbox Statistics";
				}
			}

			// Token: 0x170001F7 RID: 503
			// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0000FB4B File Offset: 0x0000DD4B
			public override string Software
			{
				get
				{
					return "Microsoft Exchange Mailbox Load Balancing";
				}
			}
		}
	}
}
