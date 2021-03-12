using System;
using Microsoft.Exchange.MessageDepot;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000123 RID: 291
	public sealed class VariantConfigurationTransportComponent : VariantConfigurationComponent
	{
		// Token: 0x06000D8D RID: 3469 RVA: 0x00020BC4 File Offset: 0x0001EDC4
		internal VariantConfigurationTransportComponent() : base("Transport")
		{
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "VerboseLogging", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "TargetAddressRoutingForRemoteGroupMailbox", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "MessageDepot", typeof(IMessageDepotSettings), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "SelectHubServersForClientProxy", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "TestProcessingQuota", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "SystemMessageOverrides", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "DirectTrustCache", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "UseNewConnectorMatchingOrder", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "TargetAddressDistributionGroupAsExternal", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "ConsolidateAdvancedRouting", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "ClientAuthRequireMailboxDatabase", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "UseTenantPartitionToCreateOrganizationId", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "LimitTransportRules", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "SmtpAcceptAnyRecipient", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "RiskBasedCounters", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "DefaultTransportServiceStateToInactive", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "LimitRemoteDomains", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "IgnoreArbitrationMailboxForModeratedRecipient", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "TransferAdditionalTenantDataThroughXATTR", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "ADExceptionHandling", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "EnforceProcessingQuota", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "SystemProbeDropAgent", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "SetMustDeliverJournalReport", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "SendUserAddressInXproxyCommand", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "UseAdditionalTenantDataFromXATTR", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "DelayDsn", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "ExplicitDeletedObjectNotifications", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "EnforceQueueQuota", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "OrganizationMailboxRouting", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "StringentHeaderTransformationMode", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "SmtpReceiveCountersStripServerName", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "ClientSubmissionToDelivery", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "SmtpXproxyConstructUpnFromSamAccountNameAndParitionFqdn", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "EnforceOutboundConnectorAndAcceptedDomainsRestriction", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "TenantThrottling", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Transport.settings.ini", "SystemProbeLogging", typeof(IFeature), false));
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x0002105C File Offset: 0x0001F25C
		public VariantConfigurationSection VerboseLogging
		{
			get
			{
				return base["VerboseLogging"];
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00021069 File Offset: 0x0001F269
		public VariantConfigurationSection TargetAddressRoutingForRemoteGroupMailbox
		{
			get
			{
				return base["TargetAddressRoutingForRemoteGroupMailbox"];
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00021076 File Offset: 0x0001F276
		public VariantConfigurationSection MessageDepot
		{
			get
			{
				return base["MessageDepot"];
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00021083 File Offset: 0x0001F283
		public VariantConfigurationSection SelectHubServersForClientProxy
		{
			get
			{
				return base["SelectHubServersForClientProxy"];
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00021090 File Offset: 0x0001F290
		public VariantConfigurationSection TestProcessingQuota
		{
			get
			{
				return base["TestProcessingQuota"];
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x0002109D File Offset: 0x0001F29D
		public VariantConfigurationSection SystemMessageOverrides
		{
			get
			{
				return base["SystemMessageOverrides"];
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x000210AA File Offset: 0x0001F2AA
		public VariantConfigurationSection DirectTrustCache
		{
			get
			{
				return base["DirectTrustCache"];
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x000210B7 File Offset: 0x0001F2B7
		public VariantConfigurationSection UseNewConnectorMatchingOrder
		{
			get
			{
				return base["UseNewConnectorMatchingOrder"];
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x000210C4 File Offset: 0x0001F2C4
		public VariantConfigurationSection TargetAddressDistributionGroupAsExternal
		{
			get
			{
				return base["TargetAddressDistributionGroupAsExternal"];
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x000210D1 File Offset: 0x0001F2D1
		public VariantConfigurationSection ConsolidateAdvancedRouting
		{
			get
			{
				return base["ConsolidateAdvancedRouting"];
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x000210DE File Offset: 0x0001F2DE
		public VariantConfigurationSection ClientAuthRequireMailboxDatabase
		{
			get
			{
				return base["ClientAuthRequireMailboxDatabase"];
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x000210EB File Offset: 0x0001F2EB
		public VariantConfigurationSection UseTenantPartitionToCreateOrganizationId
		{
			get
			{
				return base["UseTenantPartitionToCreateOrganizationId"];
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x000210F8 File Offset: 0x0001F2F8
		public VariantConfigurationSection LimitTransportRules
		{
			get
			{
				return base["LimitTransportRules"];
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x00021105 File Offset: 0x0001F305
		public VariantConfigurationSection SmtpAcceptAnyRecipient
		{
			get
			{
				return base["SmtpAcceptAnyRecipient"];
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x00021112 File Offset: 0x0001F312
		public VariantConfigurationSection RiskBasedCounters
		{
			get
			{
				return base["RiskBasedCounters"];
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x0002111F File Offset: 0x0001F31F
		public VariantConfigurationSection DefaultTransportServiceStateToInactive
		{
			get
			{
				return base["DefaultTransportServiceStateToInactive"];
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0002112C File Offset: 0x0001F32C
		public VariantConfigurationSection LimitRemoteDomains
		{
			get
			{
				return base["LimitRemoteDomains"];
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00021139 File Offset: 0x0001F339
		public VariantConfigurationSection IgnoreArbitrationMailboxForModeratedRecipient
		{
			get
			{
				return base["IgnoreArbitrationMailboxForModeratedRecipient"];
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x00021146 File Offset: 0x0001F346
		public VariantConfigurationSection TransferAdditionalTenantDataThroughXATTR
		{
			get
			{
				return base["TransferAdditionalTenantDataThroughXATTR"];
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00021153 File Offset: 0x0001F353
		public VariantConfigurationSection ADExceptionHandling
		{
			get
			{
				return base["ADExceptionHandling"];
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x00021160 File Offset: 0x0001F360
		public VariantConfigurationSection EnforceProcessingQuota
		{
			get
			{
				return base["EnforceProcessingQuota"];
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0002116D File Offset: 0x0001F36D
		public VariantConfigurationSection SystemProbeDropAgent
		{
			get
			{
				return base["SystemProbeDropAgent"];
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0002117A File Offset: 0x0001F37A
		public VariantConfigurationSection SetMustDeliverJournalReport
		{
			get
			{
				return base["SetMustDeliverJournalReport"];
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00021187 File Offset: 0x0001F387
		public VariantConfigurationSection SendUserAddressInXproxyCommand
		{
			get
			{
				return base["SendUserAddressInXproxyCommand"];
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x00021194 File Offset: 0x0001F394
		public VariantConfigurationSection UseAdditionalTenantDataFromXATTR
		{
			get
			{
				return base["UseAdditionalTenantDataFromXATTR"];
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x000211A1 File Offset: 0x0001F3A1
		public VariantConfigurationSection DelayDsn
		{
			get
			{
				return base["DelayDsn"];
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x000211AE File Offset: 0x0001F3AE
		public VariantConfigurationSection ExplicitDeletedObjectNotifications
		{
			get
			{
				return base["ExplicitDeletedObjectNotifications"];
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x000211BB File Offset: 0x0001F3BB
		public VariantConfigurationSection EnforceQueueQuota
		{
			get
			{
				return base["EnforceQueueQuota"];
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x000211C8 File Offset: 0x0001F3C8
		public VariantConfigurationSection OrganizationMailboxRouting
		{
			get
			{
				return base["OrganizationMailboxRouting"];
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x000211D5 File Offset: 0x0001F3D5
		public VariantConfigurationSection StringentHeaderTransformationMode
		{
			get
			{
				return base["StringentHeaderTransformationMode"];
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x000211E2 File Offset: 0x0001F3E2
		public VariantConfigurationSection SmtpReceiveCountersStripServerName
		{
			get
			{
				return base["SmtpReceiveCountersStripServerName"];
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x000211EF File Offset: 0x0001F3EF
		public VariantConfigurationSection ClientSubmissionToDelivery
		{
			get
			{
				return base["ClientSubmissionToDelivery"];
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x000211FC File Offset: 0x0001F3FC
		public VariantConfigurationSection SmtpXproxyConstructUpnFromSamAccountNameAndParitionFqdn
		{
			get
			{
				return base["SmtpXproxyConstructUpnFromSamAccountNameAndParitionFqdn"];
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x00021209 File Offset: 0x0001F409
		public VariantConfigurationSection EnforceOutboundConnectorAndAcceptedDomainsRestriction
		{
			get
			{
				return base["EnforceOutboundConnectorAndAcceptedDomainsRestriction"];
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x00021216 File Offset: 0x0001F416
		public VariantConfigurationSection TenantThrottling
		{
			get
			{
				return base["TenantThrottling"];
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x00021223 File Offset: 0x0001F423
		public VariantConfigurationSection SystemProbeLogging
		{
			get
			{
				return base["SystemProbeLogging"];
			}
		}
	}
}
