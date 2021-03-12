using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005E1 RID: 1505
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class TransportConfigContainer : ADContainer
	{
		// Token: 0x170016F2 RID: 5874
		// (get) Token: 0x06004578 RID: 17784 RVA: 0x00102455 File Offset: 0x00100655
		internal override ADObjectSchema Schema
		{
			get
			{
				if (TopologyProvider.IsAdamTopology())
				{
					return TransportConfigContainer.adamSchema;
				}
				return TransportConfigContainer.adSchema;
			}
		}

		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x06004579 RID: 17785 RVA: 0x00102469 File Offset: 0x00100669
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x0600457A RID: 17786 RVA: 0x00102471 File Offset: 0x00100671
		// (set) Token: 0x0600457B RID: 17787 RVA: 0x00102483 File Offset: 0x00100683
		[Parameter(Mandatory = false)]
		[ValidateCount(0, 256)]
		public MultiValuedProperty<SmtpDomain> TLSReceiveDomainSecureList
		{
			get
			{
				return (MultiValuedProperty<SmtpDomain>)this[ADAMTransportConfigContainerSchema.TLSReceiveDomainSecureList];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.TLSReceiveDomainSecureList] = value;
			}
		}

		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x0600457C RID: 17788 RVA: 0x00102491 File Offset: 0x00100691
		// (set) Token: 0x0600457D RID: 17789 RVA: 0x001024A3 File Offset: 0x001006A3
		[Parameter(Mandatory = false)]
		[ValidateCount(0, 256)]
		public MultiValuedProperty<SmtpDomain> TLSSendDomainSecureList
		{
			get
			{
				return (MultiValuedProperty<SmtpDomain>)this[ADAMTransportConfigContainerSchema.TLSSendDomainSecureList];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.TLSSendDomainSecureList] = value;
			}
		}

		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x0600457E RID: 17790 RVA: 0x001024B1 File Offset: 0x001006B1
		// (set) Token: 0x0600457F RID: 17791 RVA: 0x001024C3 File Offset: 0x001006C3
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<EnhancedStatusCode> GenerateCopyOfDSNFor
		{
			get
			{
				return (MultiValuedProperty<EnhancedStatusCode>)this[ADAMTransportConfigContainerSchema.GenerateCopyOfDSNFor];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.GenerateCopyOfDSNFor] = value;
			}
		}

		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x06004580 RID: 17792 RVA: 0x001024D1 File Offset: 0x001006D1
		// (set) Token: 0x06004581 RID: 17793 RVA: 0x001024E3 File Offset: 0x001006E3
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPRange> InternalSMTPServers
		{
			get
			{
				return (MultiValuedProperty<IPRange>)this[ADAMTransportConfigContainerSchema.InternalSMTPServers];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.InternalSMTPServers] = value;
			}
		}

		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x06004582 RID: 17794 RVA: 0x001024F1 File Offset: 0x001006F1
		// (set) Token: 0x06004583 RID: 17795 RVA: 0x00102503 File Offset: 0x00100703
		[Parameter(Mandatory = false)]
		public SmtpAddress JournalingReportNdrTo
		{
			get
			{
				return (SmtpAddress)this[ADAMTransportConfigContainerSchema.JournalingReportNdrTo];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.JournalingReportNdrTo] = value;
			}
		}

		// Token: 0x170016F9 RID: 5881
		// (get) Token: 0x06004584 RID: 17796 RVA: 0x00102516 File Offset: 0x00100716
		// (set) Token: 0x06004585 RID: 17797 RVA: 0x00102528 File Offset: 0x00100728
		[Parameter(Mandatory = false)]
		public SmtpAddress OrganizationFederatedMailbox
		{
			get
			{
				return (SmtpAddress)this.GetNonAdamProperty(TransportConfigContainerSchema.OrganizationFederatedMailbox);
			}
			set
			{
				this[TransportConfigContainerSchema.OrganizationFederatedMailbox] = value;
			}
		}

		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x06004586 RID: 17798 RVA: 0x0010253B File Offset: 0x0010073B
		// (set) Token: 0x06004587 RID: 17799 RVA: 0x0010254D File Offset: 0x0010074D
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize MaxDumpsterSizePerDatabase
		{
			get
			{
				return (ByteQuantifiedSize)this.GetNonAdamProperty(TransportConfigContainerSchema.MaxDumpsterSizePerDatabase);
			}
			set
			{
				this[TransportConfigContainerSchema.MaxDumpsterSizePerDatabase] = value;
			}
		}

		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x06004588 RID: 17800 RVA: 0x00102560 File Offset: 0x00100760
		// (set) Token: 0x06004589 RID: 17801 RVA: 0x00102572 File Offset: 0x00100772
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MaxDumpsterTime
		{
			get
			{
				return (EnhancedTimeSpan)this.GetNonAdamProperty(TransportConfigContainerSchema.MaxDumpsterTime);
			}
			set
			{
				this[TransportConfigContainerSchema.MaxDumpsterTime] = value;
			}
		}

		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x0600458A RID: 17802 RVA: 0x00102585 File Offset: 0x00100785
		// (set) Token: 0x0600458B RID: 17803 RVA: 0x00102597 File Offset: 0x00100797
		[Parameter(Mandatory = false)]
		public bool VerifySecureSubmitEnabled
		{
			get
			{
				return (bool)this[ADAMTransportConfigContainerSchema.VerifySecureSubmitEnabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.VerifySecureSubmitEnabled] = value;
			}
		}

		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x0600458C RID: 17804 RVA: 0x001025AA File Offset: 0x001007AA
		// (set) Token: 0x0600458D RID: 17805 RVA: 0x001025BF File Offset: 0x001007BF
		[Parameter(Mandatory = false)]
		public bool ClearCategories
		{
			get
			{
				return !(bool)this[ADAMTransportConfigContainerSchema.KeepCategories];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.KeepCategories] = !value;
			}
		}

		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x0600458E RID: 17806 RVA: 0x001025D5 File Offset: 0x001007D5
		// (set) Token: 0x0600458F RID: 17807 RVA: 0x001025E7 File Offset: 0x001007E7
		[Parameter(Mandatory = false)]
		public bool AddressBookPolicyRoutingEnabled
		{
			get
			{
				return (bool)this[ADAMTransportConfigContainerSchema.AddressBookPolicyRoutingEnabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.AddressBookPolicyRoutingEnabled] = value;
			}
		}

		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x06004590 RID: 17808 RVA: 0x001025FA File Offset: 0x001007FA
		// (set) Token: 0x06004591 RID: 17809 RVA: 0x0010260C File Offset: 0x0010080C
		[Parameter(Mandatory = false)]
		public bool ConvertDisclaimerWrapperToEml
		{
			get
			{
				return (bool)this[ADAMTransportConfigContainerSchema.ConvertDisclaimerWrapperToEml];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.ConvertDisclaimerWrapperToEml] = value;
			}
		}

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x06004592 RID: 17810 RVA: 0x0010261F File Offset: 0x0010081F
		// (set) Token: 0x06004593 RID: 17811 RVA: 0x00102631 File Offset: 0x00100831
		public bool PreserveReportBodypart
		{
			get
			{
				return (bool)this[ADAMTransportConfigContainerSchema.PreserveReportBodypart];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.PreserveReportBodypart] = value;
			}
		}

		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x06004594 RID: 17812 RVA: 0x00102644 File Offset: 0x00100844
		// (set) Token: 0x06004595 RID: 17813 RVA: 0x00102656 File Offset: 0x00100856
		public bool ConvertReportToMessage
		{
			get
			{
				return (bool)this[ADAMTransportConfigContainerSchema.ConvertReportToMessage];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.ConvertReportToMessage] = value;
			}
		}

		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x06004596 RID: 17814 RVA: 0x00102669 File Offset: 0x00100869
		// (set) Token: 0x06004597 RID: 17815 RVA: 0x0010267B File Offset: 0x0010087B
		[Parameter(Mandatory = false)]
		public DSNConversionOption DSNConversionMode
		{
			get
			{
				return (DSNConversionOption)this[ADAMTransportConfigContainerSchema.DSNConversionMode];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.DSNConversionMode] = value;
			}
		}

		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x06004598 RID: 17816 RVA: 0x0010268E File Offset: 0x0010088E
		// (set) Token: 0x06004599 RID: 17817 RVA: 0x001026A3 File Offset: 0x001008A3
		[Parameter(Mandatory = false)]
		public bool VoicemailJournalingEnabled
		{
			get
			{
				return !(bool)this[ADAMTransportConfigContainerSchema.VoicemailJournalingDisabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.VoicemailJournalingDisabled] = !value;
			}
		}

		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x0600459A RID: 17818 RVA: 0x001026B9 File Offset: 0x001008B9
		// (set) Token: 0x0600459B RID: 17819 RVA: 0x001026CB File Offset: 0x001008CB
		[Parameter(Mandatory = false)]
		public HeaderPromotionMode HeaderPromotionModeSetting
		{
			get
			{
				return (HeaderPromotionMode)this[ADAMTransportConfigContainerSchema.HeaderPromotionModeSetting];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.HeaderPromotionModeSetting] = value;
			}
		}

		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x0600459C RID: 17820 RVA: 0x001026DE File Offset: 0x001008DE
		// (set) Token: 0x0600459D RID: 17821 RVA: 0x001026F3 File Offset: 0x001008F3
		[Parameter(Mandatory = false)]
		public bool Xexch50Enabled
		{
			get
			{
				return !(bool)this[ADAMTransportConfigContainerSchema.DisableXexch50];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.DisableXexch50] = !value;
			}
		}

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x0600459E RID: 17822 RVA: 0x00102709 File Offset: 0x00100909
		// (set) Token: 0x0600459F RID: 17823 RVA: 0x0010271B File Offset: 0x0010091B
		[Parameter(Mandatory = false)]
		public bool Rfc2231EncodingEnabled
		{
			get
			{
				return (bool)this[ADAMTransportConfigContainerSchema.Rfc2231EncodingEnabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.Rfc2231EncodingEnabled] = value;
			}
		}

		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x060045A0 RID: 17824 RVA: 0x0010272E File Offset: 0x0010092E
		// (set) Token: 0x060045A1 RID: 17825 RVA: 0x00102740 File Offset: 0x00100940
		[Parameter(Mandatory = false)]
		public bool OpenDomainRoutingEnabled
		{
			get
			{
				return (bool)this[ADAMTransportConfigContainerSchema.OpenDomainRoutingEnabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.OpenDomainRoutingEnabled] = value;
			}
		}

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x060045A2 RID: 17826 RVA: 0x00102753 File Offset: 0x00100953
		// (set) Token: 0x060045A3 RID: 17827 RVA: 0x00102765 File Offset: 0x00100965
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> MaxReceiveSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this.GetNonAdamProperty(TransportConfigContainerSchema.MaxReceiveSize);
			}
			set
			{
				this[TransportConfigContainerSchema.MaxReceiveSize] = value;
			}
		}

		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x060045A4 RID: 17828 RVA: 0x00102778 File Offset: 0x00100978
		// (set) Token: 0x060045A5 RID: 17829 RVA: 0x0010278A File Offset: 0x0010098A
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxRecipientEnvelopeLimit
		{
			get
			{
				return (Unlimited<int>)this.GetNonAdamProperty(TransportConfigContainerSchema.MaxRecipientEnvelopeLimit);
			}
			set
			{
				this[TransportConfigContainerSchema.MaxRecipientEnvelopeLimit] = value;
			}
		}

		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x060045A6 RID: 17830 RVA: 0x0010279D File Offset: 0x0010099D
		// (set) Token: 0x060045A7 RID: 17831 RVA: 0x001027AF File Offset: 0x001009AF
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> MaxSendSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ADAMTransportConfigContainerSchema.MaxSendSize];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.MaxSendSize] = value;
			}
		}

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x060045A8 RID: 17832 RVA: 0x001027C2 File Offset: 0x001009C2
		// (set) Token: 0x060045A9 RID: 17833 RVA: 0x001027D7 File Offset: 0x001009D7
		[Parameter(Mandatory = false)]
		public bool ShadowRedundancyEnabled
		{
			get
			{
				return !(bool)this[ADAMTransportConfigContainerSchema.ShadowRedundancyDisabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.ShadowRedundancyDisabled] = !value;
			}
		}

		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x060045AA RID: 17834 RVA: 0x001027ED File Offset: 0x001009ED
		// (set) Token: 0x060045AB RID: 17835 RVA: 0x001027FF File Offset: 0x001009FF
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ShadowHeartbeatTimeoutInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[ADAMTransportConfigContainerSchema.ShadowHeartbeatTimeoutInterval];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.ShadowHeartbeatTimeoutInterval] = value;
			}
		}

		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x060045AC RID: 17836 RVA: 0x00102812 File Offset: 0x00100A12
		// (set) Token: 0x060045AD RID: 17837 RVA: 0x00102824 File Offset: 0x00100A24
		[Parameter(Mandatory = false)]
		public int ShadowHeartbeatRetryCount
		{
			get
			{
				return (int)this[ADAMTransportConfigContainerSchema.ShadowHeartbeatRetryCount];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.ShadowHeartbeatRetryCount] = value;
			}
		}

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x060045AE RID: 17838 RVA: 0x00102837 File Offset: 0x00100A37
		// (set) Token: 0x060045AF RID: 17839 RVA: 0x00102849 File Offset: 0x00100A49
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ShadowHeartbeatFrequency
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportConfigContainerSchema.ShadowHeartbeatFrequency];
			}
			set
			{
				this[TransportConfigContainerSchema.ShadowHeartbeatFrequency] = value;
			}
		}

		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x060045B0 RID: 17840 RVA: 0x0010285C File Offset: 0x00100A5C
		// (set) Token: 0x060045B1 RID: 17841 RVA: 0x0010286E File Offset: 0x00100A6E
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ShadowResubmitTimeSpan
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportConfigContainerSchema.ShadowResubmitTimeSpan];
			}
			set
			{
				this[TransportConfigContainerSchema.ShadowResubmitTimeSpan] = value;
			}
		}

		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x060045B2 RID: 17842 RVA: 0x00102881 File Offset: 0x00100A81
		// (set) Token: 0x060045B3 RID: 17843 RVA: 0x00102893 File Offset: 0x00100A93
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ShadowMessageAutoDiscardInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[ADAMTransportConfigContainerSchema.ShadowMessageAutoDiscardInterval];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.ShadowMessageAutoDiscardInterval] = value;
			}
		}

		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x060045B4 RID: 17844 RVA: 0x001028A6 File Offset: 0x00100AA6
		// (set) Token: 0x060045B5 RID: 17845 RVA: 0x001028BB File Offset: 0x00100ABB
		[Parameter(Mandatory = false)]
		public bool ExternalDelayDsnEnabled
		{
			get
			{
				return !(bool)this[ADAMTransportConfigContainerSchema.ExternalDelayDsnDisabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.ExternalDelayDsnDisabled] = !value;
			}
		}

		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x060045B6 RID: 17846 RVA: 0x001028D1 File Offset: 0x00100AD1
		// (set) Token: 0x060045B7 RID: 17847 RVA: 0x001028E3 File Offset: 0x00100AE3
		[Parameter(Mandatory = false)]
		public CultureInfo ExternalDsnDefaultLanguage
		{
			get
			{
				return (CultureInfo)this[ADAMTransportConfigContainerSchema.ExternalDsnDefaultLanguage];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.ExternalDsnDefaultLanguage] = value;
			}
		}

		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x060045B8 RID: 17848 RVA: 0x001028F1 File Offset: 0x00100AF1
		// (set) Token: 0x060045B9 RID: 17849 RVA: 0x00102906 File Offset: 0x00100B06
		[Parameter(Mandatory = false)]
		public bool ExternalDsnLanguageDetectionEnabled
		{
			get
			{
				return !(bool)this[ADAMTransportConfigContainerSchema.ExternalDsnLanguageDetectionDisabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.ExternalDsnLanguageDetectionDisabled] = !value;
			}
		}

		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x060045BA RID: 17850 RVA: 0x0010291C File Offset: 0x00100B1C
		// (set) Token: 0x060045BB RID: 17851 RVA: 0x0010292E File Offset: 0x00100B2E
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize ExternalDsnMaxMessageAttachSize
		{
			get
			{
				return (ByteQuantifiedSize)this[ADAMTransportConfigContainerSchema.ExternalDsnMaxMessageAttachSize];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.ExternalDsnMaxMessageAttachSize] = value;
			}
		}

		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x060045BC RID: 17852 RVA: 0x00102941 File Offset: 0x00100B41
		// (set) Token: 0x060045BD RID: 17853 RVA: 0x00102953 File Offset: 0x00100B53
		[Parameter(Mandatory = false)]
		public SmtpDomain ExternalDsnReportingAuthority
		{
			get
			{
				return (SmtpDomain)this[ADAMTransportConfigContainerSchema.ExternalDsnReportingAuthority];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.ExternalDsnReportingAuthority] = value;
			}
		}

		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x060045BE RID: 17854 RVA: 0x00102961 File Offset: 0x00100B61
		// (set) Token: 0x060045BF RID: 17855 RVA: 0x00102976 File Offset: 0x00100B76
		[Parameter(Mandatory = false)]
		public bool ExternalDsnSendHtml
		{
			get
			{
				return !(bool)this[ADAMTransportConfigContainerSchema.ExternalDsnSendHtmlDisabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.ExternalDsnSendHtmlDisabled] = !value;
			}
		}

		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x060045C0 RID: 17856 RVA: 0x0010298C File Offset: 0x00100B8C
		// (set) Token: 0x060045C1 RID: 17857 RVA: 0x0010299E File Offset: 0x00100B9E
		[Parameter(Mandatory = false)]
		public SmtpAddress? ExternalPostmasterAddress
		{
			get
			{
				return (SmtpAddress?)this[ADAMTransportConfigContainerSchema.ExternalPostmasterAddress];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.ExternalPostmasterAddress] = value;
			}
		}

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x060045C2 RID: 17858 RVA: 0x001029B1 File Offset: 0x00100BB1
		// (set) Token: 0x060045C3 RID: 17859 RVA: 0x001029C6 File Offset: 0x00100BC6
		[Parameter(Mandatory = false)]
		public bool InternalDelayDsnEnabled
		{
			get
			{
				return !(bool)this[ADAMTransportConfigContainerSchema.InternalDelayDsnDisabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.InternalDelayDsnDisabled] = !value;
			}
		}

		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x060045C4 RID: 17860 RVA: 0x001029DC File Offset: 0x00100BDC
		// (set) Token: 0x060045C5 RID: 17861 RVA: 0x001029EE File Offset: 0x00100BEE
		[Parameter(Mandatory = false)]
		public CultureInfo InternalDsnDefaultLanguage
		{
			get
			{
				return (CultureInfo)this[ADAMTransportConfigContainerSchema.InternalDsnDefaultLanguage];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.InternalDsnDefaultLanguage] = value;
			}
		}

		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x060045C6 RID: 17862 RVA: 0x001029FC File Offset: 0x00100BFC
		// (set) Token: 0x060045C7 RID: 17863 RVA: 0x00102A11 File Offset: 0x00100C11
		[Parameter(Mandatory = false)]
		public bool InternalDsnLanguageDetectionEnabled
		{
			get
			{
				return !(bool)this[ADAMTransportConfigContainerSchema.InternalDsnLanguageDetectionDisabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.InternalDsnLanguageDetectionDisabled] = !value;
			}
		}

		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x060045C8 RID: 17864 RVA: 0x00102A27 File Offset: 0x00100C27
		// (set) Token: 0x060045C9 RID: 17865 RVA: 0x00102A39 File Offset: 0x00100C39
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize InternalDsnMaxMessageAttachSize
		{
			get
			{
				return (ByteQuantifiedSize)this[ADAMTransportConfigContainerSchema.InternalDsnMaxMessageAttachSize];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.InternalDsnMaxMessageAttachSize] = value;
			}
		}

		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x060045CA RID: 17866 RVA: 0x00102A4C File Offset: 0x00100C4C
		// (set) Token: 0x060045CB RID: 17867 RVA: 0x00102A5E File Offset: 0x00100C5E
		[Parameter(Mandatory = false)]
		public SmtpDomain InternalDsnReportingAuthority
		{
			get
			{
				return (SmtpDomain)this[ADAMTransportConfigContainerSchema.InternalDsnReportingAuthority];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.InternalDsnReportingAuthority] = value;
			}
		}

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x060045CC RID: 17868 RVA: 0x00102A6C File Offset: 0x00100C6C
		// (set) Token: 0x060045CD RID: 17869 RVA: 0x00102A81 File Offset: 0x00100C81
		[Parameter(Mandatory = false)]
		public bool InternalDsnSendHtml
		{
			get
			{
				return !(bool)this[ADAMTransportConfigContainerSchema.InternalDsnSendHtmlDisabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.InternalDsnSendHtmlDisabled] = !value;
			}
		}

		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x060045CE RID: 17870 RVA: 0x00102A97 File Offset: 0x00100C97
		// (set) Token: 0x060045CF RID: 17871 RVA: 0x00102AA9 File Offset: 0x00100CA9
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> SupervisionTags
		{
			get
			{
				return (MultiValuedProperty<string>)this.GetNonAdamProperty(TransportConfigContainerSchema.SupervisionTags);
			}
			set
			{
				this[TransportConfigContainerSchema.SupervisionTags] = value;
			}
		}

		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x060045D0 RID: 17872 RVA: 0x00102AB7 File Offset: 0x00100CB7
		// (set) Token: 0x060045D1 RID: 17873 RVA: 0x00102AC9 File Offset: 0x00100CC9
		[Parameter(Mandatory = false)]
		public HygieneSuiteEnum HygieneSuite
		{
			get
			{
				return (HygieneSuiteEnum)this[ADAMTransportConfigContainerSchema.HygieneSuite];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.HygieneSuite] = value;
			}
		}

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x060045D2 RID: 17874 RVA: 0x00102ADC File Offset: 0x00100CDC
		// (set) Token: 0x060045D3 RID: 17875 RVA: 0x00102AEE File Offset: 0x00100CEE
		[Parameter(Mandatory = false)]
		public bool MigrationEnabled
		{
			get
			{
				return (bool)this[ADAMTransportConfigContainerSchema.MigrationEnabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.MigrationEnabled] = value;
			}
		}

		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x060045D4 RID: 17876 RVA: 0x00102B01 File Offset: 0x00100D01
		// (set) Token: 0x060045D5 RID: 17877 RVA: 0x00102B13 File Offset: 0x00100D13
		[Parameter(Mandatory = false)]
		public bool LegacyJournalingMigrationEnabled
		{
			get
			{
				return (bool)this[ADAMTransportConfigContainerSchema.LegacyJournalingMigrationEnabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.LegacyJournalingMigrationEnabled] = value;
			}
		}

		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x060045D6 RID: 17878 RVA: 0x00102B26 File Offset: 0x00100D26
		// (set) Token: 0x060045D7 RID: 17879 RVA: 0x00102B38 File Offset: 0x00100D38
		[Parameter(Mandatory = false)]
		public bool LegacyArchiveJournalingEnabled
		{
			get
			{
				return (bool)this[ADAMTransportConfigContainerSchema.LegacyArchiveJournalingEnabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.LegacyArchiveJournalingEnabled] = value;
			}
		}

		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x060045D8 RID: 17880 RVA: 0x00102B4B File Offset: 0x00100D4B
		// (set) Token: 0x060045D9 RID: 17881 RVA: 0x00102B5D File Offset: 0x00100D5D
		[Parameter(Mandatory = false)]
		public bool RedirectDLMessagesForLegacyArchiveJournaling
		{
			get
			{
				return (bool)this[ADAMTransportConfigContainerSchema.RedirectDLMessagesForLegacyArchiveJournaling];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.RedirectDLMessagesForLegacyArchiveJournaling] = value;
			}
		}

		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x060045DA RID: 17882 RVA: 0x00102B70 File Offset: 0x00100D70
		// (set) Token: 0x060045DB RID: 17883 RVA: 0x00102B82 File Offset: 0x00100D82
		[Parameter(Mandatory = false)]
		public bool RedirectUnprovisionedUserMessagesForLegacyArchiveJournaling
		{
			get
			{
				return (bool)this[ADAMTransportConfigContainerSchema.RedirectUnprovisionedUserMessagesForLegacyArchiveJournaling];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.RedirectUnprovisionedUserMessagesForLegacyArchiveJournaling] = value;
			}
		}

		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x060045DC RID: 17884 RVA: 0x00102B95 File Offset: 0x00100D95
		// (set) Token: 0x060045DD RID: 17885 RVA: 0x00102BA7 File Offset: 0x00100DA7
		[Parameter(Mandatory = false)]
		public bool LegacyArchiveLiveJournalingEnabled
		{
			get
			{
				return (bool)this[ADAMTransportConfigContainerSchema.LegacyArchiveLiveJournalingEnabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.LegacyArchiveLiveJournalingEnabled] = value;
			}
		}

		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x060045DE RID: 17886 RVA: 0x00102BBA File Offset: 0x00100DBA
		// (set) Token: 0x060045DF RID: 17887 RVA: 0x00102BCC File Offset: 0x00100DCC
		[Parameter(Mandatory = false)]
		public bool JournalArchivingEnabled
		{
			get
			{
				return (bool)this[ADAMTransportConfigContainerSchema.JournalArchivingEnabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.JournalArchivingEnabled] = value;
			}
		}

		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x060045E0 RID: 17888 RVA: 0x00102BDF File Offset: 0x00100DDF
		// (set) Token: 0x060045E1 RID: 17889 RVA: 0x00102BF1 File Offset: 0x00100DF1
		[Parameter(Mandatory = false)]
		public bool RejectMessageOnShadowFailure
		{
			get
			{
				return (bool)this[ADAMTransportConfigContainerSchema.RejectMessageOnShadowFailure];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.RejectMessageOnShadowFailure] = value;
			}
		}

		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x060045E2 RID: 17890 RVA: 0x00102C04 File Offset: 0x00100E04
		// (set) Token: 0x060045E3 RID: 17891 RVA: 0x00102C16 File Offset: 0x00100E16
		[Parameter(Mandatory = false)]
		public ShadowMessagePreference ShadowMessagePreferenceSetting
		{
			get
			{
				return (ShadowMessagePreference)this[ADAMTransportConfigContainerSchema.ShadowMessagePreferenceSetting];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.ShadowMessagePreferenceSetting] = value;
			}
		}

		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x060045E4 RID: 17892 RVA: 0x00102C29 File Offset: 0x00100E29
		// (set) Token: 0x060045E5 RID: 17893 RVA: 0x00102C3B File Offset: 0x00100E3B
		[Parameter(Mandatory = false)]
		public int MaxRetriesForLocalSiteShadow
		{
			get
			{
				return (int)this[ADAMTransportConfigContainerSchema.MaxRetriesForLocalSiteShadow];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.MaxRetriesForLocalSiteShadow] = value;
			}
		}

		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x060045E6 RID: 17894 RVA: 0x00102C4E File Offset: 0x00100E4E
		// (set) Token: 0x060045E7 RID: 17895 RVA: 0x00102C60 File Offset: 0x00100E60
		[Parameter(Mandatory = false)]
		public int MaxRetriesForRemoteSiteShadow
		{
			get
			{
				return (int)this[ADAMTransportConfigContainerSchema.MaxRetriesForRemoteSiteShadow];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.MaxRetriesForRemoteSiteShadow] = value;
			}
		}

		// Token: 0x1700172B RID: 5931
		// (get) Token: 0x060045E8 RID: 17896 RVA: 0x00102C73 File Offset: 0x00100E73
		// (set) Token: 0x060045E9 RID: 17897 RVA: 0x00102C85 File Offset: 0x00100E85
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan SafetyNetHoldTime
		{
			get
			{
				return (EnhancedTimeSpan)this[ADAMTransportConfigContainerSchema.SafetyNetHoldTime];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.SafetyNetHoldTime] = value;
			}
		}

		// Token: 0x1700172C RID: 5932
		// (get) Token: 0x060045EA RID: 17898 RVA: 0x00102C98 File Offset: 0x00100E98
		// (set) Token: 0x060045EB RID: 17899 RVA: 0x00102CAA File Offset: 0x00100EAA
		public MultiValuedProperty<string> TransportRuleConfig
		{
			get
			{
				return (MultiValuedProperty<string>)this[TransportConfigContainerSchema.TransportRuleConfig];
			}
			set
			{
				this[TransportConfigContainerSchema.TransportRuleConfig] = value;
			}
		}

		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x060045EC RID: 17900 RVA: 0x00102CB8 File Offset: 0x00100EB8
		// (set) Token: 0x060045ED RID: 17901 RVA: 0x00102CCA File Offset: 0x00100ECA
		[Parameter(Mandatory = false)]
		public int TransportRuleCollectionAddedRecipientsLimit
		{
			get
			{
				return (int)this[TransportConfigContainerSchema.TransportRuleCollectionAddedRecipientsLimit];
			}
			set
			{
				this[TransportConfigContainerSchema.TransportRuleCollectionAddedRecipientsLimit] = value;
			}
		}

		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x060045EE RID: 17902 RVA: 0x00102CDD File Offset: 0x00100EDD
		// (set) Token: 0x060045EF RID: 17903 RVA: 0x00102CEF File Offset: 0x00100EEF
		[Parameter(Mandatory = false)]
		public int TransportRuleLimit
		{
			get
			{
				return (int)this[TransportConfigContainerSchema.TransportRuleLimit];
			}
			set
			{
				this[TransportConfigContainerSchema.TransportRuleLimit] = value;
			}
		}

		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x060045F0 RID: 17904 RVA: 0x00102D02 File Offset: 0x00100F02
		// (set) Token: 0x060045F1 RID: 17905 RVA: 0x00102D14 File Offset: 0x00100F14
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportRuleCollectionRegexCharsLimit
		{
			get
			{
				return (ByteQuantifiedSize)this[TransportConfigContainerSchema.TransportRuleCollectionRegexCharsLimit];
			}
			set
			{
				this[TransportConfigContainerSchema.TransportRuleCollectionRegexCharsLimit] = value;
			}
		}

		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x060045F2 RID: 17906 RVA: 0x00102D27 File Offset: 0x00100F27
		// (set) Token: 0x060045F3 RID: 17907 RVA: 0x00102D39 File Offset: 0x00100F39
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportRuleSizeLimit
		{
			get
			{
				return (ByteQuantifiedSize)this[TransportConfigContainerSchema.TransportRuleSizeLimit];
			}
			set
			{
				this[TransportConfigContainerSchema.TransportRuleSizeLimit] = value;
			}
		}

		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x060045F4 RID: 17908 RVA: 0x00102D4C File Offset: 0x00100F4C
		// (set) Token: 0x060045F5 RID: 17909 RVA: 0x00102D5E File Offset: 0x00100F5E
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportRuleAttachmentTextScanLimit
		{
			get
			{
				return (ByteQuantifiedSize)this[TransportConfigContainerSchema.TransportRuleAttachmentTextScanLimit];
			}
			set
			{
				this[TransportConfigContainerSchema.TransportRuleAttachmentTextScanLimit] = value;
			}
		}

		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x060045F6 RID: 17910 RVA: 0x00102D71 File Offset: 0x00100F71
		// (set) Token: 0x060045F7 RID: 17911 RVA: 0x00102D83 File Offset: 0x00100F83
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TransportRuleRegexValidationTimeout
		{
			get
			{
				return (EnhancedTimeSpan)this[TransportConfigContainerSchema.TransportRuleRegexValidationTimeout];
			}
			set
			{
				this[TransportConfigContainerSchema.TransportRuleRegexValidationTimeout] = value;
			}
		}

		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x060045F8 RID: 17912 RVA: 0x00102D96 File Offset: 0x00100F96
		// (set) Token: 0x060045F9 RID: 17913 RVA: 0x00102DA8 File Offset: 0x00100FA8
		[Parameter(Mandatory = false)]
		public Version TransportRuleMinProductVersion
		{
			get
			{
				return (Version)this[TransportConfigContainerSchema.TransportRuleMinProductVersion];
			}
			set
			{
				this[TransportConfigContainerSchema.TransportRuleMinProductVersion] = value;
			}
		}

		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x060045FA RID: 17914 RVA: 0x00102DB6 File Offset: 0x00100FB6
		// (set) Token: 0x060045FB RID: 17915 RVA: 0x00102DC8 File Offset: 0x00100FC8
		[Parameter(Mandatory = false)]
		public int AnonymousSenderToRecipientRatePerHour
		{
			get
			{
				return (int)this[TransportConfigContainerSchema.AnonymousSenderToRecipientRatePerHour];
			}
			set
			{
				this[TransportConfigContainerSchema.AnonymousSenderToRecipientRatePerHour] = value;
			}
		}

		// Token: 0x17001735 RID: 5941
		// (get) Token: 0x060045FC RID: 17916 RVA: 0x00102DDB File Offset: 0x00100FDB
		// (set) Token: 0x060045FD RID: 17917 RVA: 0x00102DF2 File Offset: 0x00100FF2
		public EnhancedTimeSpan QueueDiagnosticsAggregationInterval
		{
			get
			{
				return EnhancedTimeSpan.FromTicks((long)this[TransportConfigContainerSchema.QueueDiagnosticsAggregationInterval]);
			}
			set
			{
				this[TransportConfigContainerSchema.QueueDiagnosticsAggregationInterval] = value.Ticks;
			}
		}

		// Token: 0x17001736 RID: 5942
		// (get) Token: 0x060045FE RID: 17918 RVA: 0x00102E0B File Offset: 0x0010100B
		// (set) Token: 0x060045FF RID: 17919 RVA: 0x00102E1D File Offset: 0x0010101D
		[Parameter(Mandatory = false)]
		public bool JournalReportDLMemberSubstitutionEnabled
		{
			get
			{
				return (bool)this[ADAMTransportConfigContainerSchema.JournalReportDLMemberSubstitutionEnabled];
			}
			set
			{
				this[ADAMTransportConfigContainerSchema.JournalReportDLMemberSubstitutionEnabled] = value;
			}
		}

		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x06004600 RID: 17920 RVA: 0x00102E30 File Offset: 0x00101030
		// (set) Token: 0x06004601 RID: 17921 RVA: 0x00102E42 File Offset: 0x00101042
		public int DiagnosticsAggregationServicePort
		{
			get
			{
				return (int)this[TransportConfigContainerSchema.DiagnosticsAggregationServicePort];
			}
			set
			{
				this[TransportConfigContainerSchema.DiagnosticsAggregationServicePort] = value;
			}
		}

		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x06004602 RID: 17922 RVA: 0x00102E55 File Offset: 0x00101055
		// (set) Token: 0x06004603 RID: 17923 RVA: 0x00102E67 File Offset: 0x00101067
		public bool AgentGeneratedMessageLoopDetectionInSubmissionEnabled
		{
			get
			{
				return (bool)this[TransportConfigContainerSchema.AgentGeneratedMessageLoopDetectionInSubmissionEnabled];
			}
			set
			{
				this[TransportConfigContainerSchema.AgentGeneratedMessageLoopDetectionInSubmissionEnabled] = value;
			}
		}

		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x06004604 RID: 17924 RVA: 0x00102E7A File Offset: 0x0010107A
		// (set) Token: 0x06004605 RID: 17925 RVA: 0x00102E8C File Offset: 0x0010108C
		public bool AgentGeneratedMessageLoopDetectionInSmtpEnabled
		{
			get
			{
				return (bool)this[TransportConfigContainerSchema.AgentGeneratedMessageLoopDetectionInSmtpEnabled];
			}
			set
			{
				this[TransportConfigContainerSchema.AgentGeneratedMessageLoopDetectionInSmtpEnabled] = value;
			}
		}

		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x06004606 RID: 17926 RVA: 0x00102E9F File Offset: 0x0010109F
		// (set) Token: 0x06004607 RID: 17927 RVA: 0x00102EB1 File Offset: 0x001010B1
		public uint MaxAllowedAgentGeneratedMessageDepth
		{
			get
			{
				return (uint)this[TransportConfigContainerSchema.MaxAllowedAgentGeneratedMessageDepth];
			}
			set
			{
				this[TransportConfigContainerSchema.MaxAllowedAgentGeneratedMessageDepth] = value;
			}
		}

		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x06004608 RID: 17928 RVA: 0x00102EC4 File Offset: 0x001010C4
		// (set) Token: 0x06004609 RID: 17929 RVA: 0x00102ED6 File Offset: 0x001010D6
		public uint MaxAllowedAgentGeneratedMessageDepthPerAgent
		{
			get
			{
				return (uint)this[TransportConfigContainerSchema.MaxAllowedAgentGeneratedMessageDepthPerAgent];
			}
			set
			{
				this[TransportConfigContainerSchema.MaxAllowedAgentGeneratedMessageDepthPerAgent] = value;
			}
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x00102EEC File Offset: 0x001010EC
		internal static object InternalHeaderPromotionModeGetter(IPropertyBag bag)
		{
			TransportSettingFlags transportSettingFlags = (TransportSettingFlags)bag[ADAMTransportConfigContainerSchema.Flags];
			HeaderPromotionMode headerPromotionMode = (HeaderPromotionMode)((transportSettingFlags & TransportSettingFlags.HeaderPromotionModeSetting) >> 21);
			return EnumValidator.IsValidValue<HeaderPromotionMode>(headerPromotionMode) ? headerPromotionMode : HeaderPromotionMode.NoCreate;
		}

		// Token: 0x0600460B RID: 17931 RVA: 0x00102F28 File Offset: 0x00101128
		internal static void InternalHeaderPromotionModeSetter(object value, IPropertyBag bag)
		{
			TransportSettingFlags transportSettingFlags = (TransportSettingFlags)bag[ADAMTransportConfigContainerSchema.Flags] & ~TransportSettingFlags.HeaderPromotionModeSetting;
			TransportSettingFlags transportSettingFlags2 = (TransportSettingFlags)((int)value << 21 & 6291456);
			bag[ADAMTransportConfigContainerSchema.Flags] = (int)(transportSettingFlags2 | transportSettingFlags);
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x00102F70 File Offset: 0x00101170
		internal static object InternalDsnDefaultLanguageGetter(IPropertyBag propertyBag)
		{
			string cultureString = (string)propertyBag[ADAMTransportConfigContainerSchema.InternalDsnDefaultLanguageStr];
			return TransportConfigContainer.ConvertStringToDefaultDsnCulture(cultureString);
		}

		// Token: 0x0600460D RID: 17933 RVA: 0x00102F94 File Offset: 0x00101194
		internal static void InternalDsnDefaultLanguageSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADAMTransportConfigContainerSchema.InternalDsnDefaultLanguageStr] = ((value != null) ? ((CultureInfo)value).ToString() : string.Empty);
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x00102FB8 File Offset: 0x001011B8
		internal static object ExternalDsnDefaultLanguageGetter(IPropertyBag propertyBag)
		{
			string cultureString = (string)propertyBag[ADAMTransportConfigContainerSchema.ExternalDsnDefaultLanguageStr];
			return TransportConfigContainer.ConvertStringToDefaultDsnCulture(cultureString);
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x00102FDC File Offset: 0x001011DC
		internal static void ExternalDsnDefaultLanguageSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADAMTransportConfigContainerSchema.ExternalDsnDefaultLanguageStr] = ((value != null) ? ((CultureInfo)value).ToString() : string.Empty);
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x00103000 File Offset: 0x00101200
		internal static object InternalShadowMessagePreferenceGetter(IPropertyBag bag)
		{
			TransportSettingFlags transportSettingFlags = (TransportSettingFlags)bag[ADAMTransportConfigContainerSchema.Flags];
			ShadowMessagePreference shadowMessagePreference = (ShadowMessagePreference)((transportSettingFlags & TransportSettingFlags.ShadowMessagePreferenceSetting) >> 27);
			return EnumValidator.IsValidValue<ShadowMessagePreference>(shadowMessagePreference) ? shadowMessagePreference : ShadowMessagePreference.PreferRemote;
		}

		// Token: 0x06004611 RID: 17937 RVA: 0x0010303C File Offset: 0x0010123C
		internal static void InternalShadowMessagePreferenceSetter(object value, IPropertyBag bag)
		{
			TransportSettingFlags transportSettingFlags = (TransportSettingFlags)bag[ADAMTransportConfigContainerSchema.Flags] & ~TransportSettingFlags.ShadowMessagePreferenceSetting;
			TransportSettingFlags transportSettingFlags2 = (TransportSettingFlags)((int)value << 27 & 402653184);
			bag[ADAMTransportConfigContainerSchema.Flags] = (int)(transportSettingFlags2 | transportSettingFlags);
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x00103084 File Offset: 0x00101284
		internal static CultureInfo ConvertStringToDefaultDsnCulture(string cultureString)
		{
			if (string.IsNullOrEmpty(cultureString))
			{
				return null;
			}
			CultureInfo result;
			try
			{
				result = CultureInfo.GetCultureInfo(cultureString);
			}
			catch (ArgumentException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x001030BC File Offset: 0x001012BC
		internal bool IsTLSSendSecureDomain(string domainName)
		{
			if (this.tlsSendDomainSecureDictionary == null)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>(this.TLSSendDomainSecureList.Count * 2, StringComparer.OrdinalIgnoreCase);
				foreach (SmtpDomain smtpDomain in this.TLSSendDomainSecureList)
				{
					dictionary.Add(smtpDomain.Domain, null);
				}
				this.tlsSendDomainSecureDictionary = dictionary;
			}
			return this.tlsSendDomainSecureDictionary.Count > 0 && this.tlsSendDomainSecureDictionary.ContainsKey(domainName);
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x00103158 File Offset: 0x00101358
		internal bool IsTLSReceiveSecureDomain(string domainName)
		{
			if (this.tlsReceiveDomainSecureDictionary == null)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>(this.TLSReceiveDomainSecureList.Count * 2, StringComparer.OrdinalIgnoreCase);
				foreach (SmtpDomain smtpDomain in this.TLSReceiveDomainSecureList)
				{
					dictionary.Add(smtpDomain.Domain, null);
				}
				this.tlsReceiveDomainSecureDictionary = dictionary;
			}
			return this.tlsReceiveDomainSecureDictionary.Count > 0 && this.tlsReceiveDomainSecureDictionary.ContainsKey(domainName);
		}

		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x06004615 RID: 17941 RVA: 0x001031F4 File Offset: 0x001013F4
		internal override string MostDerivedObjectClass
		{
			get
			{
				return TransportConfigContainer.mostDerivedClass;
			}
		}

		// Token: 0x06004616 RID: 17942 RVA: 0x001031FC File Offset: 0x001013FC
		private object GetNonAdamProperty(ADPropertyDefinition propertyDefinition)
		{
			object result;
			if (TopologyProvider.IsAdamTopology())
			{
				result = propertyDefinition.DefaultValue;
			}
			else
			{
				result = this[propertyDefinition];
			}
			return result;
		}

		// Token: 0x04002FCE RID: 12238
		internal const int MaxDomainSecureListCount = 256;

		// Token: 0x04002FCF RID: 12239
		private static readonly TransportConfigContainerSchema adSchema = ObjectSchema.GetInstance<TransportConfigContainerSchema>();

		// Token: 0x04002FD0 RID: 12240
		private static readonly ADAMTransportConfigContainerSchema adamSchema = ObjectSchema.GetInstance<ADAMTransportConfigContainerSchema>();

		// Token: 0x04002FD1 RID: 12241
		private static string mostDerivedClass = "msExchTransportSettings";

		// Token: 0x04002FD2 RID: 12242
		private Dictionary<string, object> tlsSendDomainSecureDictionary;

		// Token: 0x04002FD3 RID: 12243
		private Dictionary<string, object> tlsReceiveDomainSecureDictionary;
	}
}
