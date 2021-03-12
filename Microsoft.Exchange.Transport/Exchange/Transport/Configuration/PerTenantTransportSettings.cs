using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x020002E2 RID: 738
	internal sealed class PerTenantTransportSettings : TenantConfigurationCacheableItem<TransportConfigContainer>, ITransportSettingsFacade
	{
		// Token: 0x06002090 RID: 8336 RVA: 0x0007C787 File Offset: 0x0007A987
		public PerTenantTransportSettings()
		{
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x0007C78F File Offset: 0x0007A98F
		public PerTenantTransportSettings(TransportConfigContainer transportConfigContainer) : base(true)
		{
			this.SetInternalData(transportConfigContainer);
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06002092 RID: 8338 RVA: 0x0007C79F File Offset: 0x0007A99F
		public MultiValuedProperty<SmtpDomain> TLSReceiveDomainSecureList
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return Components.Configuration.TransportSettings.TransportSettings.TLSReceiveDomainSecureList;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x0007C7BC File Offset: 0x0007A9BC
		public MultiValuedProperty<SmtpDomain> TLSSendDomainSecureList
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return Components.Configuration.TransportSettings.TransportSettings.TLSSendDomainSecureList;
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06002094 RID: 8340 RVA: 0x0007C7D9 File Offset: 0x0007A9D9
		public MultiValuedProperty<EnhancedStatusCode> GenerateCopyOfDSNFor
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return Components.Configuration.TransportSettings.TransportSettings.GenerateCopyOfDSNFor;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x0007C7F6 File Offset: 0x0007A9F6
		public MultiValuedProperty<IPRange> InternalSMTPServers
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return Components.Configuration.TransportSettings.TransportSettings.InternalSMTPServers;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06002096 RID: 8342 RVA: 0x0007C813 File Offset: 0x0007AA13
		public SmtpAddress JournalingReportNdrTo
		{
			get
			{
				return this.journalingReportNdrTo;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x0007C81B File Offset: 0x0007AA1B
		public string OrganizationFederatedMailbox
		{
			get
			{
				return this.organizationFederatedMailbox.ToString();
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002098 RID: 8344 RVA: 0x0007C82E File Offset: 0x0007AA2E
		public ByteQuantifiedSize MaxDumpsterSizePerDatabase
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return Components.Configuration.TransportSettings.TransportSettings.MaxDumpsterSizePerDatabase;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x0007C84B File Offset: 0x0007AA4B
		public EnhancedTimeSpan MaxDumpsterTime
		{
			get
			{
				return Components.Configuration.TransportSettings.TransportSettings.MaxDumpsterTime;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x0600209A RID: 8346 RVA: 0x0007C861 File Offset: 0x0007AA61
		public bool VerifySecureSubmitEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return Components.Configuration.TransportSettings.TransportSettings.VerifySecureSubmitEnabled;
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x0600209B RID: 8347 RVA: 0x0007C87E File Offset: 0x0007AA7E
		public bool ClearCategories
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.clearCategories;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x0600209C RID: 8348 RVA: 0x0007C88D File Offset: 0x0007AA8D
		public bool OpenDomainRoutingEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.openDomainRoutingEnabled;
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x0600209D RID: 8349 RVA: 0x0007C89C File Offset: 0x0007AA9C
		public bool AddressBookPolicyRoutingEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return Components.Configuration.TransportSettings.TransportSettings.AddressBookPolicyRoutingEnabled && this.addressBookPolicyRoutingEnabled;
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x0600209E RID: 8350 RVA: 0x0007C8C3 File Offset: 0x0007AAC3
		public bool VoicemailJournalingEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return Components.Configuration.TransportSettings.TransportSettings.VoicemailJournalingEnabled;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x0600209F RID: 8351 RVA: 0x0007C8E0 File Offset: 0x0007AAE0
		public bool Xexch50Enabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return Components.Configuration.TransportSettings.TransportSettings.Xexch50Enabled;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x0007C8FD File Offset: 0x0007AAFD
		public bool Rfc2231EncodingEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.rfc2231EncodingEnabled;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060020A1 RID: 8353 RVA: 0x0007C90C File Offset: 0x0007AB0C
		public Unlimited<ByteQuantifiedSize> MaxReceiveSize
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.maxReceiveSize;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x060020A2 RID: 8354 RVA: 0x0007C91B File Offset: 0x0007AB1B
		public Unlimited<int> MaxRecipientEnvelopeLimit
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return Components.Configuration.TransportSettings.TransportSettings.MaxRecipientEnvelopeLimit;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060020A3 RID: 8355 RVA: 0x0007C938 File Offset: 0x0007AB38
		public Unlimited<ByteQuantifiedSize> MaxSendSize
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.maxSendSize;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x0007C947 File Offset: 0x0007AB47
		public bool ExternalDelayDsnEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.externalDelayDsnEnabled;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x060020A5 RID: 8357 RVA: 0x0007C956 File Offset: 0x0007AB56
		public CultureInfo ExternalDsnDefaultLanguage
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.externalDsnDefaultLanguage;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060020A6 RID: 8358 RVA: 0x0007C965 File Offset: 0x0007AB65
		public bool ExternalDsnLanguageDetectionEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.externalDsnLanguageDetectionEnabled;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x0007C974 File Offset: 0x0007AB74
		public ByteQuantifiedSize ExternalDsnMaxMessageAttachSize
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.externalDsnMaxMessageAttachSize;
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x0007C983 File Offset: 0x0007AB83
		public SmtpDomain ExternalDsnReportingAuthority
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.externalDsnReportingAuthority;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x0007C992 File Offset: 0x0007AB92
		public bool ExternalDsnSendHtml
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.externalDsnSendHtml;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x0007C9A1 File Offset: 0x0007ABA1
		public SmtpAddress? ExternalPostmasterAddress
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.externalPostmasterAddress;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x0007C9B0 File Offset: 0x0007ABB0
		public bool InternalDelayDsnEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.internalDelayDsnEnabled;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x0007C9BF File Offset: 0x0007ABBF
		public CultureInfo InternalDsnDefaultLanguage
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.internalDsnDefaultLanguage;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060020AD RID: 8365 RVA: 0x0007C9CE File Offset: 0x0007ABCE
		public bool InternalDsnLanguageDetectionEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.internalDsnLanguageDetectionEnabled;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060020AE RID: 8366 RVA: 0x0007C9DD File Offset: 0x0007ABDD
		public ByteQuantifiedSize InternalDsnMaxMessageAttachSize
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.internalDsnMaxMessageAttachSize;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x060020AF RID: 8367 RVA: 0x0007C9EC File Offset: 0x0007ABEC
		public SmtpDomain InternalDsnReportingAuthority
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.internalDsnReportingAuthority;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x060020B0 RID: 8368 RVA: 0x0007C9FB File Offset: 0x0007ABFB
		public bool InternalDsnSendHtml
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.internalDsnSendHtml;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x060020B1 RID: 8369 RVA: 0x0007CA0A File Offset: 0x0007AC0A
		public IList<string> SupervisionTags
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.supervisionTags;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x0007CA19 File Offset: 0x0007AC19
		public override long ItemSize
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return (long)this.estimatedSize;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x0007CA29 File Offset: 0x0007AC29
		public string HygieneSuite
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.hygieneSuite;
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060020B4 RID: 8372 RVA: 0x0007CA38 File Offset: 0x0007AC38
		public bool ConvertReportToMessage
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.convertReportToMessage;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060020B5 RID: 8373 RVA: 0x0007CA47 File Offset: 0x0007AC47
		public bool PreserveReportBodypart
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.preserveReportBodypart;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060020B6 RID: 8374 RVA: 0x0007CA56 File Offset: 0x0007AC56
		public bool MigrationEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.migrationEnabled;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x0007CA65 File Offset: 0x0007AC65
		public bool LegacyJournalingMigrationEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.legacyJournalingMigrationEnabled;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060020B8 RID: 8376 RVA: 0x0007CA74 File Offset: 0x0007AC74
		public bool LegacyArchiveJournalingEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.legacyArchiveJournalingEnabled;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x0007CA83 File Offset: 0x0007AC83
		public bool LegacyArchiveLiveJournalingEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.legacyArchiveLiveJournalingEnabled;
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060020BA RID: 8378 RVA: 0x0007CA92 File Offset: 0x0007AC92
		public bool DropUnprovisionedUserMessagesForLegacyArchiveJournaling
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.dropUnprovisionedUserMessagesForLegacyArchiveJournaling;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x0007CAA1 File Offset: 0x0007ACA1
		public bool RedirectDLMessagesForLegacyArchiveJournaling
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.redirectDLMessagesForLegacyArchiveJournaling;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060020BC RID: 8380 RVA: 0x0007CAB0 File Offset: 0x0007ACB0
		public bool JournalArchivingEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.journalArchivingEnabled;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x0007CABF File Offset: 0x0007ACBF
		public int TransportRuleAttachmentTextScanLimit
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.transportRuleAttachmentTextScanLimit;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060020BE RID: 8382 RVA: 0x0007CACE File Offset: 0x0007ACCE
		public Version TransportRuleMinProductVersion
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.transportRuleMinProductVersion;
			}
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x0007CAE0 File Offset: 0x0007ACE0
		public override void ReadData(IConfigurationSession session)
		{
			TransportConfigContainer[] array = session.Find<TransportConfigContainer>(null, QueryScope.SubTree, null, null, 2);
			if (array == null || array.Length == 0)
			{
				ExTraceGlobals.ConfigurationTracer.TraceError<string>((long)this.GetHashCode(), "Could not find transport settings for {0}", PerTenantTransportSettings.GetOrgIdString(session));
				throw new TenantTransportSettingsNotFoundException(PerTenantTransportSettings.GetOrgIdString(session));
			}
			if (array.Length > 1)
			{
				ExTraceGlobals.ConfigurationTracer.TraceError<string>((long)this.GetHashCode(), "Found more than one transport settings for {0}", PerTenantTransportSettings.GetOrgIdString(session));
				throw new TransportSettingsAmbiguousException(PerTenantTransportSettings.GetOrgIdString(session));
			}
			this.SetInternalData(array[0]);
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x0007CB60 File Offset: 0x0007AD60
		private static string GetOrgIdString(IConfigurationSession session)
		{
			if (!(session.SessionSettings.CurrentOrganizationId != null))
			{
				return "<First Organization>";
			}
			return session.SessionSettings.CurrentOrganizationId.ToString();
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x0007CB8C File Offset: 0x0007AD8C
		private void SetInternalData(TransportConfigContainer transportConfigContainer)
		{
			this.clearCategories = transportConfigContainer.ClearCategories;
			this.rfc2231EncodingEnabled = transportConfigContainer.Rfc2231EncodingEnabled;
			this.openDomainRoutingEnabled = transportConfigContainer.OpenDomainRoutingEnabled;
			this.addressBookPolicyRoutingEnabled = transportConfigContainer.AddressBookPolicyRoutingEnabled;
			this.externalDelayDsnEnabled = transportConfigContainer.ExternalDelayDsnEnabled;
			this.externalDsnDefaultLanguage = transportConfigContainer.ExternalDsnDefaultLanguage;
			this.externalDsnLanguageDetectionEnabled = transportConfigContainer.ExternalDsnLanguageDetectionEnabled;
			this.externalDsnMaxMessageAttachSize = transportConfigContainer.ExternalDsnMaxMessageAttachSize;
			this.externalDsnReportingAuthority = transportConfigContainer.ExternalDsnReportingAuthority;
			this.externalDsnSendHtml = transportConfigContainer.ExternalDsnSendHtml;
			this.externalPostmasterAddress = transportConfigContainer.ExternalPostmasterAddress;
			this.internalDelayDsnEnabled = transportConfigContainer.InternalDelayDsnEnabled;
			this.internalDsnDefaultLanguage = transportConfigContainer.InternalDsnDefaultLanguage;
			this.internalDsnLanguageDetectionEnabled = transportConfigContainer.InternalDsnLanguageDetectionEnabled;
			this.internalDsnMaxMessageAttachSize = transportConfigContainer.InternalDsnMaxMessageAttachSize;
			this.internalDsnReportingAuthority = transportConfigContainer.InternalDsnReportingAuthority;
			this.internalDsnSendHtml = transportConfigContainer.InternalDsnSendHtml;
			this.journalingReportNdrTo = transportConfigContainer.JournalingReportNdrTo;
			this.organizationFederatedMailbox = transportConfigContainer.OrganizationFederatedMailbox;
			this.migrationEnabled = transportConfigContainer.MigrationEnabled;
			this.convertReportToMessage = transportConfigContainer.ConvertReportToMessage;
			this.preserveReportBodypart = transportConfigContainer.PreserveReportBodypart;
			this.legacyJournalingMigrationEnabled = transportConfigContainer.LegacyJournalingMigrationEnabled;
			this.legacyArchiveJournalingEnabled = transportConfigContainer.LegacyArchiveJournalingEnabled;
			this.legacyArchiveLiveJournalingEnabled = transportConfigContainer.LegacyArchiveLiveJournalingEnabled;
			this.dropUnprovisionedUserMessagesForLegacyArchiveJournaling = !transportConfigContainer.RedirectUnprovisionedUserMessagesForLegacyArchiveJournaling;
			this.redirectDLMessagesForLegacyArchiveJournaling = transportConfigContainer.RedirectDLMessagesForLegacyArchiveJournaling;
			this.journalArchivingEnabled = transportConfigContainer.JournalArchivingEnabled;
			this.transportRuleAttachmentTextScanLimit = (int)transportConfigContainer.TransportRuleAttachmentTextScanLimit.ToBytes();
			this.transportRuleMinProductVersion = transportConfigContainer.TransportRuleMinProductVersion;
			this.maxReceiveSize = transportConfigContainer.MaxReceiveSize;
			this.maxSendSize = transportConfigContainer.MaxSendSize;
			switch (transportConfigContainer.HygieneSuite)
			{
			case HygieneSuiteEnum.Premium:
				this.hygieneSuite = "Premium";
				goto IL_1BE;
			}
			this.hygieneSuite = "Standard";
			IL_1BE:
			this.supervisionTags = transportConfigContainer.SupervisionTags;
			int num = (this.internalDsnReportingAuthority != null) ? this.internalDsnReportingAuthority.Domain.Length : 0;
			int num2 = (this.externalDsnReportingAuthority != null) ? this.externalDsnReportingAuthority.Domain.Length : 0;
			int num3 = (this.externalPostmasterAddress != null) ? this.externalPostmasterAddress.Value.Length : 0;
			int num4 = this.journalingReportNdrTo.Length * 2 + 18;
			int num5 = this.organizationFederatedMailbox.Length * 2 + 18;
			int num6 = 0;
			if (this.supervisionTags != null)
			{
				foreach (string text in this.supervisionTags)
				{
					num6 += text.Length;
				}
				num6 *= 2;
			}
			this.estimatedSize = 27 + num + num2 + num3 + num4 + num5 + num6 + 16;
		}

		// Token: 0x0400110E RID: 4366
		private const int CultureInfoReferenceSize = 8;

		// Token: 0x0400110F RID: 4367
		private const string PremiumHygieneSuite = "Premium";

		// Token: 0x04001110 RID: 4368
		private const string StandardHygieneSuite = "Standard";

		// Token: 0x04001111 RID: 4369
		private int estimatedSize;

		// Token: 0x04001112 RID: 4370
		private bool rfc2231EncodingEnabled;

		// Token: 0x04001113 RID: 4371
		private bool clearCategories;

		// Token: 0x04001114 RID: 4372
		private bool openDomainRoutingEnabled;

		// Token: 0x04001115 RID: 4373
		private bool addressBookPolicyRoutingEnabled;

		// Token: 0x04001116 RID: 4374
		private bool externalDelayDsnEnabled;

		// Token: 0x04001117 RID: 4375
		private CultureInfo externalDsnDefaultLanguage;

		// Token: 0x04001118 RID: 4376
		private bool externalDsnLanguageDetectionEnabled;

		// Token: 0x04001119 RID: 4377
		private ByteQuantifiedSize externalDsnMaxMessageAttachSize;

		// Token: 0x0400111A RID: 4378
		private SmtpDomain externalDsnReportingAuthority;

		// Token: 0x0400111B RID: 4379
		private bool externalDsnSendHtml;

		// Token: 0x0400111C RID: 4380
		private SmtpAddress? externalPostmasterAddress;

		// Token: 0x0400111D RID: 4381
		private bool internalDelayDsnEnabled;

		// Token: 0x0400111E RID: 4382
		private CultureInfo internalDsnDefaultLanguage;

		// Token: 0x0400111F RID: 4383
		private bool internalDsnLanguageDetectionEnabled;

		// Token: 0x04001120 RID: 4384
		private ByteQuantifiedSize internalDsnMaxMessageAttachSize;

		// Token: 0x04001121 RID: 4385
		private SmtpDomain internalDsnReportingAuthority;

		// Token: 0x04001122 RID: 4386
		private bool internalDsnSendHtml;

		// Token: 0x04001123 RID: 4387
		private SmtpAddress journalingReportNdrTo;

		// Token: 0x04001124 RID: 4388
		private SmtpAddress organizationFederatedMailbox;

		// Token: 0x04001125 RID: 4389
		private IList<string> supervisionTags;

		// Token: 0x04001126 RID: 4390
		private string hygieneSuite;

		// Token: 0x04001127 RID: 4391
		private bool convertReportToMessage;

		// Token: 0x04001128 RID: 4392
		private bool preserveReportBodypart;

		// Token: 0x04001129 RID: 4393
		private bool migrationEnabled;

		// Token: 0x0400112A RID: 4394
		private bool legacyJournalingMigrationEnabled;

		// Token: 0x0400112B RID: 4395
		private bool legacyArchiveJournalingEnabled;

		// Token: 0x0400112C RID: 4396
		private bool redirectDLMessagesForLegacyArchiveJournaling;

		// Token: 0x0400112D RID: 4397
		private bool dropUnprovisionedUserMessagesForLegacyArchiveJournaling;

		// Token: 0x0400112E RID: 4398
		private bool journalArchivingEnabled;

		// Token: 0x0400112F RID: 4399
		private bool legacyArchiveLiveJournalingEnabled;

		// Token: 0x04001130 RID: 4400
		private int transportRuleAttachmentTextScanLimit;

		// Token: 0x04001131 RID: 4401
		private Version transportRuleMinProductVersion;

		// Token: 0x04001132 RID: 4402
		private Unlimited<ByteQuantifiedSize> maxReceiveSize;

		// Token: 0x04001133 RID: 4403
		private Unlimited<ByteQuantifiedSize> maxSendSize;
	}
}
