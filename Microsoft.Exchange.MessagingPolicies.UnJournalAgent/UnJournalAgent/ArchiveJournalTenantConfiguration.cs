using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.UnJournalAgent
{
	// Token: 0x02000004 RID: 4
	internal sealed class ArchiveJournalTenantConfiguration
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002740 File Offset: 0x00000940
		public ArchiveJournalTenantConfiguration(OrganizationId orgId, PerTenantTransportSettings perTenantTransportSettings, MicrosoftExchangeRecipientPerTenantSettings perTenantMerConfig)
		{
			if (perTenantTransportSettings == null || perTenantMerConfig == null)
			{
				throw new ArgumentNullException("must specify pertenanttransportsettings and pertenantmerconfig");
			}
			this.merRoutingAddress = RoutingAddress.Parse(perTenantMerConfig.PrimarySmtpAddress.ToString());
			SmtpAddress journalingReportNdrTo = perTenantTransportSettings.JournalingReportNdrTo;
			if (perTenantTransportSettings.JournalingReportNdrTo.IsValidAddress && perTenantTransportSettings.JournalingReportNdrTo != SmtpAddress.NullReversePath)
			{
				this.journalingReportNdrToRoutingAddress = RoutingAddress.Parse(perTenantTransportSettings.JournalingReportNdrTo.ToString());
			}
			else
			{
				this.journalingReportNdrToRoutingAddress = this.merRoutingAddress;
			}
			if (orgId != OrganizationId.ForestWideOrgId || VariantConfiguration.InvariantNoFlightingSnapshot.Ipaed.ProcessForestWideOrgJournal.Enabled)
			{
				this.legacyArchiveJournalingEnabled = perTenantTransportSettings.LegacyArchiveJournalingEnabled;
				this.dropUnprovisionedUserMessages = perTenantTransportSettings.DropUnprovisionedUserMessagesForLegacyArchiveJournaling;
				this.redirectDistributionListsMessages = perTenantTransportSettings.RedirectDLMessagesForLegacyArchiveJournaling;
				this.legacyArchiveLiveJournalingEnabled = perTenantTransportSettings.LegacyArchiveLiveJournalingEnabled;
				this.journalArchivingEnabled = perTenantTransportSettings.JournalArchivingEnabled;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002842 File Offset: 0x00000A42
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000284A File Offset: 0x00000A4A
		internal RoutingAddress EhaMigrationMailboxAddress
		{
			get
			{
				return this.ehaMigrationMailboxAddress;
			}
			set
			{
				this.ehaMigrationMailboxAddress = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002853 File Offset: 0x00000A53
		internal RoutingAddress MSExchangeRecipient
		{
			get
			{
				return this.merRoutingAddress;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000285B File Offset: 0x00000A5B
		internal RoutingAddress JournalReportNdrTo
		{
			get
			{
				return this.journalingReportNdrToRoutingAddress;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002863 File Offset: 0x00000A63
		internal RoutingAddress JournalReportNdrToForEhaMigration
		{
			get
			{
				return this.journalingReportNdrToRoutingAddress;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000286B File Offset: 0x00000A6B
		internal bool LegacyArchiveJournalingEnabled
		{
			get
			{
				return this.legacyArchiveJournalingEnabled;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002873 File Offset: 0x00000A73
		internal bool LegacyArchiveLiveJournalingEnabled
		{
			get
			{
				return this.legacyArchiveLiveJournalingEnabled;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000287B File Offset: 0x00000A7B
		internal bool JournalArchivingEnabled
		{
			get
			{
				return this.journalArchivingEnabled;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002883 File Offset: 0x00000A83
		internal bool RedirectDistributionListMessages
		{
			get
			{
				return this.redirectDistributionListsMessages;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000288B File Offset: 0x00000A8B
		internal bool DropUnprovisionedUsersMessages
		{
			get
			{
				return this.dropUnprovisionedUserMessages;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002893 File Offset: 0x00000A93
		internal bool DropJournalsWithPermanentErrors
		{
			get
			{
				return ArchiveJournalTenantConfiguration.dropJournalsWithPermanentErrors;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000289C File Offset: 0x00000A9C
		internal static ArchiveJournalTenantConfiguration GetTenantConfig(OrganizationId organizationId)
		{
			PerTenantTransportSettings perTenantTransportSettings;
			if (!Components.Configuration.TryGetTransportSettings(organizationId, out perTenantTransportSettings))
			{
				ExTraceGlobals.JournalingTracer.TraceError<OrganizationId>(0L, "UnjournalAgent: Transport Settings could not be loaded for tenant: {0}", organizationId);
				return null;
			}
			MicrosoftExchangeRecipientPerTenantSettings microsoftExchangeRecipientPerTenantSettings;
			if (!Components.Configuration.TryGetMicrosoftExchangeRecipient(organizationId, out microsoftExchangeRecipientPerTenantSettings))
			{
				ExTraceGlobals.JournalingTracer.TraceError<OrganizationId>(0L, "UnjournalAgent: MER config could not be loaded tenant: {0}", organizationId);
				return null;
			}
			SmtpAddress primarySmtpAddress = microsoftExchangeRecipientPerTenantSettings.PrimarySmtpAddress;
			if (!microsoftExchangeRecipientPerTenantSettings.PrimarySmtpAddress.IsValidAddress)
			{
				ExTraceGlobals.JournalingTracer.TraceError<OrganizationId>(0L, "UnjournalAgent: MER config not valid for this tenant: {0}", organizationId);
				return null;
			}
			ExTraceGlobals.JournalingTracer.TraceDebug<OrganizationId>(0L, "Unjournal agent: Loaded tenant config: {0}", organizationId);
			return new ArchiveJournalTenantConfiguration(organizationId, perTenantTransportSettings, microsoftExchangeRecipientPerTenantSettings);
		}

		// Token: 0x04000029 RID: 41
		private static readonly bool dropJournalsWithPermanentErrors = true;

		// Token: 0x0400002A RID: 42
		private readonly bool legacyArchiveJournalingEnabled;

		// Token: 0x0400002B RID: 43
		private readonly bool legacyArchiveLiveJournalingEnabled;

		// Token: 0x0400002C RID: 44
		private readonly bool journalArchivingEnabled;

		// Token: 0x0400002D RID: 45
		private readonly bool dropUnprovisionedUserMessages = true;

		// Token: 0x0400002E RID: 46
		private readonly bool redirectDistributionListsMessages;

		// Token: 0x0400002F RID: 47
		private RoutingAddress journalingReportNdrToRoutingAddress;

		// Token: 0x04000030 RID: 48
		private RoutingAddress ehaMigrationMailboxAddress;

		// Token: 0x04000031 RID: 49
		private RoutingAddress merRoutingAddress;
	}
}
