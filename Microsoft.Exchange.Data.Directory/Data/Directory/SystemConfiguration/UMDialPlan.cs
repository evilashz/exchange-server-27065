using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000609 RID: 1545
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class UMDialPlan : ADConfigurationObject
	{
		// Token: 0x170017F1 RID: 6129
		// (get) Token: 0x060048C8 RID: 18632 RVA: 0x0010EA8A File Offset: 0x0010CC8A
		internal override ADObjectSchema Schema
		{
			get
			{
				return UMDialPlan.schema;
			}
		}

		// Token: 0x170017F2 RID: 6130
		// (get) Token: 0x060048C9 RID: 18633 RVA: 0x0010EA91 File Offset: 0x0010CC91
		internal override string MostDerivedObjectClass
		{
			get
			{
				return UMDialPlan.mostDerivedClass;
			}
		}

		// Token: 0x170017F3 RID: 6131
		// (get) Token: 0x060048CA RID: 18634 RVA: 0x0010EA98 File Offset: 0x0010CC98
		internal override ADObjectId ParentPath
		{
			get
			{
				return UMDialPlan.parentPath;
			}
		}

		// Token: 0x170017F4 RID: 6132
		// (get) Token: 0x060048CB RID: 18635 RVA: 0x0010EA9F File Offset: 0x0010CC9F
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x170017F5 RID: 6133
		// (get) Token: 0x060048CC RID: 18636 RVA: 0x0010EAA6 File Offset: 0x0010CCA6
		internal override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x060048CD RID: 18637 RVA: 0x0010EAA9 File Offset: 0x0010CCA9
		// (set) Token: 0x060048CE RID: 18638 RVA: 0x0010EABB File Offset: 0x0010CCBB
		public int NumberOfDigitsInExtension
		{
			get
			{
				return (int)this[UMDialPlanSchema.NumberOfDigitsInExtension];
			}
			set
			{
				this[UMDialPlanSchema.NumberOfDigitsInExtension] = value;
			}
		}

		// Token: 0x170017F7 RID: 6135
		// (get) Token: 0x060048CF RID: 18639 RVA: 0x0010EACE File Offset: 0x0010CCCE
		// (set) Token: 0x060048D0 RID: 18640 RVA: 0x0010EAE0 File Offset: 0x0010CCE0
		[Parameter(Mandatory = false)]
		public int LogonFailuresBeforeDisconnect
		{
			get
			{
				return (int)this[UMDialPlanSchema.LogonFailuresBeforeDisconnect];
			}
			set
			{
				this[UMDialPlanSchema.LogonFailuresBeforeDisconnect] = value;
			}
		}

		// Token: 0x170017F8 RID: 6136
		// (get) Token: 0x060048D1 RID: 18641 RVA: 0x0010EAF3 File Offset: 0x0010CCF3
		// (set) Token: 0x060048D2 RID: 18642 RVA: 0x0010EB05 File Offset: 0x0010CD05
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> AccessTelephoneNumbers
		{
			get
			{
				return (MultiValuedProperty<string>)this[UMDialPlanSchema.AccessTelephoneNumbers];
			}
			set
			{
				this[UMDialPlanSchema.AccessTelephoneNumbers] = value;
			}
		}

		// Token: 0x170017F9 RID: 6137
		// (get) Token: 0x060048D3 RID: 18643 RVA: 0x0010EB13 File Offset: 0x0010CD13
		// (set) Token: 0x060048D4 RID: 18644 RVA: 0x0010EB25 File Offset: 0x0010CD25
		[Parameter(Mandatory = false)]
		public bool FaxEnabled
		{
			get
			{
				return (bool)this[UMDialPlanSchema.FaxEnabled];
			}
			set
			{
				this[UMDialPlanSchema.FaxEnabled] = value;
			}
		}

		// Token: 0x170017FA RID: 6138
		// (get) Token: 0x060048D5 RID: 18645 RVA: 0x0010EB38 File Offset: 0x0010CD38
		// (set) Token: 0x060048D6 RID: 18646 RVA: 0x0010EB4A File Offset: 0x0010CD4A
		[Parameter(Mandatory = false)]
		public int InputFailuresBeforeDisconnect
		{
			get
			{
				return (int)this[UMDialPlanSchema.InputFailuresBeforeDisconnect];
			}
			set
			{
				this[UMDialPlanSchema.InputFailuresBeforeDisconnect] = value;
			}
		}

		// Token: 0x170017FB RID: 6139
		// (get) Token: 0x060048D7 RID: 18647 RVA: 0x0010EB5D File Offset: 0x0010CD5D
		// (set) Token: 0x060048D8 RID: 18648 RVA: 0x0010EB6F File Offset: 0x0010CD6F
		[Parameter(Mandatory = false)]
		public string OutsideLineAccessCode
		{
			get
			{
				return (string)this[UMDialPlanSchema.OutsideLineAccessCode];
			}
			set
			{
				this[UMDialPlanSchema.OutsideLineAccessCode] = value;
			}
		}

		// Token: 0x170017FC RID: 6140
		// (get) Token: 0x060048D9 RID: 18649 RVA: 0x0010EB7D File Offset: 0x0010CD7D
		// (set) Token: 0x060048DA RID: 18650 RVA: 0x0010EB8F File Offset: 0x0010CD8F
		[Parameter(Mandatory = false)]
		public DialByNamePrimaryEnum DialByNamePrimary
		{
			get
			{
				return (DialByNamePrimaryEnum)this[UMDialPlanSchema.DialByNamePrimary];
			}
			set
			{
				this[UMDialPlanSchema.DialByNamePrimary] = value;
			}
		}

		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x060048DB RID: 18651 RVA: 0x0010EBA2 File Offset: 0x0010CDA2
		// (set) Token: 0x060048DC RID: 18652 RVA: 0x0010EBB4 File Offset: 0x0010CDB4
		[Parameter(Mandatory = false)]
		public DialByNameSecondaryEnum DialByNameSecondary
		{
			get
			{
				return (DialByNameSecondaryEnum)this[UMDialPlanSchema.DialByNameSecondary];
			}
			set
			{
				this[UMDialPlanSchema.DialByNameSecondary] = value;
			}
		}

		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x060048DD RID: 18653 RVA: 0x0010EBC7 File Offset: 0x0010CDC7
		// (set) Token: 0x060048DE RID: 18654 RVA: 0x0010EBD9 File Offset: 0x0010CDD9
		[Parameter(Mandatory = false)]
		public AudioCodecEnum AudioCodec
		{
			get
			{
				return (AudioCodecEnum)this[UMDialPlanSchema.AudioCodec];
			}
			set
			{
				this[UMDialPlanSchema.AudioCodec] = value;
			}
		}

		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x060048DF RID: 18655 RVA: 0x0010EBEC File Offset: 0x0010CDEC
		// (set) Token: 0x060048E0 RID: 18656 RVA: 0x0010EBFE File Offset: 0x0010CDFE
		[Parameter(Mandatory = false)]
		public UMLanguage DefaultLanguage
		{
			get
			{
				return (UMLanguage)this[UMDialPlanSchema.DefaultLanguage];
			}
			set
			{
				this[UMDialPlanSchema.DefaultLanguage] = value;
			}
		}

		// Token: 0x17001800 RID: 6144
		// (get) Token: 0x060048E1 RID: 18657 RVA: 0x0010EC0C File Offset: 0x0010CE0C
		// (set) Token: 0x060048E2 RID: 18658 RVA: 0x0010EC1E File Offset: 0x0010CE1E
		[Parameter(Mandatory = false)]
		public UMVoIPSecurityType VoIPSecurity
		{
			get
			{
				return (UMVoIPSecurityType)this[UMDialPlanSchema.VoIPSecurity];
			}
			set
			{
				this[UMDialPlanSchema.VoIPSecurity] = value;
			}
		}

		// Token: 0x17001801 RID: 6145
		// (get) Token: 0x060048E3 RID: 18659 RVA: 0x0010EC31 File Offset: 0x0010CE31
		// (set) Token: 0x060048E4 RID: 18660 RVA: 0x0010EC43 File Offset: 0x0010CE43
		[Parameter(Mandatory = false)]
		public int MaxCallDuration
		{
			get
			{
				return (int)this[UMDialPlanSchema.MaxCallDuration];
			}
			set
			{
				this[UMDialPlanSchema.MaxCallDuration] = value;
			}
		}

		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x060048E5 RID: 18661 RVA: 0x0010EC56 File Offset: 0x0010CE56
		// (set) Token: 0x060048E6 RID: 18662 RVA: 0x0010EC68 File Offset: 0x0010CE68
		[Parameter(Mandatory = false)]
		public int MaxRecordingDuration
		{
			get
			{
				return (int)this[UMDialPlanSchema.MaxRecordingDuration];
			}
			set
			{
				this[UMDialPlanSchema.MaxRecordingDuration] = value;
			}
		}

		// Token: 0x17001803 RID: 6147
		// (get) Token: 0x060048E7 RID: 18663 RVA: 0x0010EC7B File Offset: 0x0010CE7B
		// (set) Token: 0x060048E8 RID: 18664 RVA: 0x0010EC8D File Offset: 0x0010CE8D
		[Parameter(Mandatory = false)]
		public int RecordingIdleTimeout
		{
			get
			{
				return (int)this[UMDialPlanSchema.RecordingIdleTimeout];
			}
			set
			{
				this[UMDialPlanSchema.RecordingIdleTimeout] = value;
			}
		}

		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x060048E9 RID: 18665 RVA: 0x0010ECA0 File Offset: 0x0010CEA0
		// (set) Token: 0x060048EA RID: 18666 RVA: 0x0010ECB2 File Offset: 0x0010CEB2
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> PilotIdentifierList
		{
			get
			{
				return (MultiValuedProperty<string>)this[UMDialPlanSchema.PilotIdentifierList];
			}
			set
			{
				this[UMDialPlanSchema.PilotIdentifierList] = value;
			}
		}

		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x060048EB RID: 18667 RVA: 0x0010ECC0 File Offset: 0x0010CEC0
		public MultiValuedProperty<ADObjectId> UMServers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[UMDialPlanSchema.UMServers];
			}
		}

		// Token: 0x17001806 RID: 6150
		// (get) Token: 0x060048EC RID: 18668 RVA: 0x0010ECD2 File Offset: 0x0010CED2
		public MultiValuedProperty<ADObjectId> UMMailboxPolicies
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[UMDialPlanSchema.UMMailboxPolicies];
			}
		}

		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x060048ED RID: 18669 RVA: 0x0010ECE4 File Offset: 0x0010CEE4
		public MultiValuedProperty<ADObjectId> UMAutoAttendants
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[UMDialPlanSchema.UMAutoAttendants];
			}
		}

		// Token: 0x17001808 RID: 6152
		// (get) Token: 0x060048EE RID: 18670 RVA: 0x0010ECF6 File Offset: 0x0010CEF6
		// (set) Token: 0x060048EF RID: 18671 RVA: 0x0010ED08 File Offset: 0x0010CF08
		[Parameter(Mandatory = false)]
		public bool WelcomeGreetingEnabled
		{
			get
			{
				return (bool)this[UMDialPlanSchema.WelcomeGreetingEnabled];
			}
			set
			{
				this[UMDialPlanSchema.WelcomeGreetingEnabled] = value;
			}
		}

		// Token: 0x17001809 RID: 6153
		// (get) Token: 0x060048F0 RID: 18672 RVA: 0x0010ED1B File Offset: 0x0010CF1B
		// (set) Token: 0x060048F1 RID: 18673 RVA: 0x0010ED2D File Offset: 0x0010CF2D
		[Parameter(Mandatory = false)]
		public bool AutomaticSpeechRecognitionEnabled
		{
			get
			{
				return (bool)this[UMDialPlanSchema.AutomaticSpeechRecognitionEnabled];
			}
			set
			{
				this[UMDialPlanSchema.AutomaticSpeechRecognitionEnabled] = value;
			}
		}

		// Token: 0x1700180A RID: 6154
		// (get) Token: 0x060048F2 RID: 18674 RVA: 0x0010ED40 File Offset: 0x0010CF40
		// (set) Token: 0x060048F3 RID: 18675 RVA: 0x0010ED52 File Offset: 0x0010CF52
		public string PhoneContext
		{
			get
			{
				return (string)this[UMDialPlanSchema.PhoneContext];
			}
			internal set
			{
				this[UMDialPlanSchema.PhoneContext] = value;
			}
		}

		// Token: 0x1700180B RID: 6155
		// (get) Token: 0x060048F4 RID: 18676 RVA: 0x0010ED60 File Offset: 0x0010CF60
		// (set) Token: 0x060048F5 RID: 18677 RVA: 0x0010ED72 File Offset: 0x0010CF72
		[Parameter(Mandatory = false)]
		public string WelcomeGreetingFilename
		{
			get
			{
				return (string)this[UMDialPlanSchema.WelcomeGreetingFilename];
			}
			set
			{
				this[UMDialPlanSchema.WelcomeGreetingFilename] = value;
			}
		}

		// Token: 0x1700180C RID: 6156
		// (get) Token: 0x060048F6 RID: 18678 RVA: 0x0010ED80 File Offset: 0x0010CF80
		// (set) Token: 0x060048F7 RID: 18679 RVA: 0x0010ED92 File Offset: 0x0010CF92
		[Parameter(Mandatory = false)]
		public string InfoAnnouncementFilename
		{
			get
			{
				return (string)this[UMDialPlanSchema.InfoAnnouncementFilename];
			}
			set
			{
				this[UMDialPlanSchema.InfoAnnouncementFilename] = value;
			}
		}

		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x060048F8 RID: 18680 RVA: 0x0010EDA0 File Offset: 0x0010CFA0
		// (set) Token: 0x060048F9 RID: 18681 RVA: 0x0010EDB2 File Offset: 0x0010CFB2
		[Parameter(Mandatory = false)]
		public string OperatorExtension
		{
			get
			{
				return (string)this[UMDialPlanSchema.OperatorExtension];
			}
			set
			{
				this[UMDialPlanSchema.OperatorExtension] = value;
			}
		}

		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x060048FA RID: 18682 RVA: 0x0010EDC0 File Offset: 0x0010CFC0
		// (set) Token: 0x060048FB RID: 18683 RVA: 0x0010EDD2 File Offset: 0x0010CFD2
		[Parameter(Mandatory = false)]
		public string DefaultOutboundCallingLineId
		{
			get
			{
				return (string)this[UMDialPlanSchema.DefaultOutboundCallingLineId];
			}
			set
			{
				this[UMDialPlanSchema.DefaultOutboundCallingLineId] = value;
			}
		}

		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x060048FC RID: 18684 RVA: 0x0010EDE0 File Offset: 0x0010CFE0
		// (set) Token: 0x060048FD RID: 18685 RVA: 0x0010EDF2 File Offset: 0x0010CFF2
		[Parameter(Mandatory = false)]
		public string Extension
		{
			get
			{
				return (string)this[UMDialPlanSchema.Extension];
			}
			set
			{
				this[UMDialPlanSchema.Extension] = value;
			}
		}

		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x060048FE RID: 18686 RVA: 0x0010EE00 File Offset: 0x0010D000
		// (set) Token: 0x060048FF RID: 18687 RVA: 0x0010EE12 File Offset: 0x0010D012
		[Parameter(Mandatory = false)]
		public DisambiguationFieldEnum MatchedNameSelectionMethod
		{
			get
			{
				return (DisambiguationFieldEnum)this[UMDialPlanSchema.MatchedNameSelectionMethod];
			}
			set
			{
				this[UMDialPlanSchema.MatchedNameSelectionMethod] = value;
			}
		}

		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x06004900 RID: 18688 RVA: 0x0010EE25 File Offset: 0x0010D025
		// (set) Token: 0x06004901 RID: 18689 RVA: 0x0010EE37 File Offset: 0x0010D037
		[Parameter(Mandatory = false)]
		public InfoAnnouncementEnabledEnum InfoAnnouncementEnabled
		{
			get
			{
				return (InfoAnnouncementEnabledEnum)this[UMDialPlanSchema.InfoAnnouncementEnabled];
			}
			set
			{
				this[UMDialPlanSchema.InfoAnnouncementEnabled] = value;
			}
		}

		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x06004902 RID: 18690 RVA: 0x0010EE4A File Offset: 0x0010D04A
		// (set) Token: 0x06004903 RID: 18691 RVA: 0x0010EE5C File Offset: 0x0010D05C
		[Parameter(Mandatory = false)]
		public string InternationalAccessCode
		{
			get
			{
				return (string)this[UMDialPlanSchema.InternationalAccessCode];
			}
			set
			{
				this[UMDialPlanSchema.InternationalAccessCode] = value;
			}
		}

		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x06004904 RID: 18692 RVA: 0x0010EE6A File Offset: 0x0010D06A
		// (set) Token: 0x06004905 RID: 18693 RVA: 0x0010EE7C File Offset: 0x0010D07C
		[Parameter(Mandatory = false)]
		public string NationalNumberPrefix
		{
			get
			{
				return (string)this[UMDialPlanSchema.NationalNumberPrefix];
			}
			set
			{
				this[UMDialPlanSchema.NationalNumberPrefix] = value;
			}
		}

		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x06004906 RID: 18694 RVA: 0x0010EE8A File Offset: 0x0010D08A
		// (set) Token: 0x06004907 RID: 18695 RVA: 0x0010EE9C File Offset: 0x0010D09C
		[Parameter(Mandatory = false)]
		public NumberFormat InCountryOrRegionNumberFormat
		{
			get
			{
				return (NumberFormat)this[UMDialPlanSchema.InCountryOrRegionNumberFormat];
			}
			set
			{
				this[UMDialPlanSchema.InCountryOrRegionNumberFormat] = value;
			}
		}

		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x06004908 RID: 18696 RVA: 0x0010EEAA File Offset: 0x0010D0AA
		// (set) Token: 0x06004909 RID: 18697 RVA: 0x0010EEBC File Offset: 0x0010D0BC
		[Parameter(Mandatory = false)]
		public NumberFormat InternationalNumberFormat
		{
			get
			{
				return (NumberFormat)this[UMDialPlanSchema.InternationalNumberFormat];
			}
			set
			{
				this[UMDialPlanSchema.InternationalNumberFormat] = value;
			}
		}

		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x0600490A RID: 18698 RVA: 0x0010EECA File Offset: 0x0010D0CA
		// (set) Token: 0x0600490B RID: 18699 RVA: 0x0010EEDC File Offset: 0x0010D0DC
		[Parameter(Mandatory = false)]
		public bool CallSomeoneEnabled
		{
			get
			{
				return (bool)this[UMDialPlanSchema.CallSomeoneEnabled];
			}
			set
			{
				this[UMDialPlanSchema.CallSomeoneEnabled] = value;
			}
		}

		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x0600490C RID: 18700 RVA: 0x0010EEEF File Offset: 0x0010D0EF
		// (set) Token: 0x0600490D RID: 18701 RVA: 0x0010EF01 File Offset: 0x0010D101
		[Parameter(Mandatory = false)]
		public CallSomeoneScopeEnum ContactScope
		{
			get
			{
				return (CallSomeoneScopeEnum)this[UMDialPlanSchema.ContactScope];
			}
			set
			{
				this[UMDialPlanSchema.ContactScope] = value;
			}
		}

		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x0600490E RID: 18702 RVA: 0x0010EF14 File Offset: 0x0010D114
		// (set) Token: 0x0600490F RID: 18703 RVA: 0x0010EF26 File Offset: 0x0010D126
		public ADObjectId ContactAddressList
		{
			get
			{
				return (ADObjectId)this[UMDialPlanSchema.ContactAddressList];
			}
			set
			{
				this[UMDialPlanSchema.ContactAddressList] = value;
			}
		}

		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x06004910 RID: 18704 RVA: 0x0010EF34 File Offset: 0x0010D134
		// (set) Token: 0x06004911 RID: 18705 RVA: 0x0010EF46 File Offset: 0x0010D146
		[Parameter(Mandatory = false)]
		public bool SendVoiceMsgEnabled
		{
			get
			{
				return (bool)this[UMDialPlanSchema.SendVoiceMsgEnabled];
			}
			set
			{
				this[UMDialPlanSchema.SendVoiceMsgEnabled] = value;
			}
		}

		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x06004912 RID: 18706 RVA: 0x0010EF59 File Offset: 0x0010D159
		// (set) Token: 0x06004913 RID: 18707 RVA: 0x0010EF6B File Offset: 0x0010D16B
		public ADObjectId UMAutoAttendant
		{
			get
			{
				return (ADObjectId)this[UMDialPlanSchema.UMAutoAttendant];
			}
			set
			{
				this[UMDialPlanSchema.UMAutoAttendant] = value;
			}
		}

		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x06004914 RID: 18708 RVA: 0x0010EF79 File Offset: 0x0010D179
		// (set) Token: 0x06004915 RID: 18709 RVA: 0x0010EF8B File Offset: 0x0010D18B
		[Parameter(Mandatory = false)]
		public bool AllowDialPlanSubscribers
		{
			get
			{
				return (bool)this[UMDialPlanSchema.AllowDialPlanSubscribers];
			}
			set
			{
				this[UMDialPlanSchema.AllowDialPlanSubscribers] = value;
			}
		}

		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x06004916 RID: 18710 RVA: 0x0010EF9E File Offset: 0x0010D19E
		// (set) Token: 0x06004917 RID: 18711 RVA: 0x0010EFB0 File Offset: 0x0010D1B0
		[Parameter(Mandatory = false)]
		public bool AllowExtensions
		{
			get
			{
				return (bool)this[UMDialPlanSchema.AllowExtensions];
			}
			set
			{
				this[UMDialPlanSchema.AllowExtensions] = value;
			}
		}

		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x06004918 RID: 18712 RVA: 0x0010EFC3 File Offset: 0x0010D1C3
		// (set) Token: 0x06004919 RID: 18713 RVA: 0x0010EFD5 File Offset: 0x0010D1D5
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> AllowedInCountryOrRegionGroups
		{
			get
			{
				return (MultiValuedProperty<string>)this[UMDialPlanSchema.AllowedInCountryOrRegionGroups];
			}
			set
			{
				this[UMDialPlanSchema.AllowedInCountryOrRegionGroups] = value;
			}
		}

		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x0600491A RID: 18714 RVA: 0x0010EFE3 File Offset: 0x0010D1E3
		// (set) Token: 0x0600491B RID: 18715 RVA: 0x0010EFF5 File Offset: 0x0010D1F5
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> AllowedInternationalGroups
		{
			get
			{
				return (MultiValuedProperty<string>)this[UMDialPlanSchema.AllowedInternationalGroups];
			}
			set
			{
				this[UMDialPlanSchema.AllowedInternationalGroups] = value;
			}
		}

		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x0600491C RID: 18716 RVA: 0x0010F003 File Offset: 0x0010D203
		// (set) Token: 0x0600491D RID: 18717 RVA: 0x0010F015 File Offset: 0x0010D215
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<DialGroupEntry> ConfiguredInCountryOrRegionGroups
		{
			get
			{
				return (MultiValuedProperty<DialGroupEntry>)this[UMDialPlanSchema.ConfiguredInCountryOrRegionGroups];
			}
			set
			{
				this[UMDialPlanSchema.ConfiguredInCountryOrRegionGroups] = value;
			}
		}

		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x0600491E RID: 18718 RVA: 0x0010F023 File Offset: 0x0010D223
		// (set) Token: 0x0600491F RID: 18719 RVA: 0x0010F035 File Offset: 0x0010D235
		[Parameter(Mandatory = false)]
		public string LegacyPromptPublishingPoint
		{
			get
			{
				return (string)this[UMDialPlanSchema.PromptPublishingPoint];
			}
			set
			{
				this[UMDialPlanSchema.PromptPublishingPoint] = value;
			}
		}

		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x06004920 RID: 18720 RVA: 0x0010F043 File Offset: 0x0010D243
		// (set) Token: 0x06004921 RID: 18721 RVA: 0x0010F055 File Offset: 0x0010D255
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<DialGroupEntry> ConfiguredInternationalGroups
		{
			get
			{
				return (MultiValuedProperty<DialGroupEntry>)this[UMDialPlanSchema.ConfiguredInternationalGroups];
			}
			set
			{
				this[UMDialPlanSchema.ConfiguredInternationalGroups] = value;
			}
		}

		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x06004922 RID: 18722 RVA: 0x0010F063 File Offset: 0x0010D263
		public MultiValuedProperty<ADObjectId> UMIPGateway
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[UMDialPlanSchema.UMIPGateway];
			}
		}

		// Token: 0x17001823 RID: 6179
		// (get) Token: 0x06004923 RID: 18723 RVA: 0x0010F075 File Offset: 0x0010D275
		// (set) Token: 0x06004924 RID: 18724 RVA: 0x0010F087 File Offset: 0x0010D287
		public UMUriType URIType
		{
			get
			{
				return (UMUriType)this[UMDialPlanSchema.URIType];
			}
			internal set
			{
				this[UMDialPlanSchema.URIType] = value;
			}
		}

		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x06004925 RID: 18725 RVA: 0x0010F09A File Offset: 0x0010D29A
		// (set) Token: 0x06004926 RID: 18726 RVA: 0x0010F0AC File Offset: 0x0010D2AC
		public UMSubscriberType SubscriberType
		{
			get
			{
				return (UMSubscriberType)this[UMDialPlanSchema.SubscriberType];
			}
			internal set
			{
				this[UMDialPlanSchema.SubscriberType] = value;
			}
		}

		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x06004927 RID: 18727 RVA: 0x0010F0BF File Offset: 0x0010D2BF
		// (set) Token: 0x06004928 RID: 18728 RVA: 0x0010F0D1 File Offset: 0x0010D2D1
		public UMGlobalCallRoutingScheme GlobalCallRoutingScheme
		{
			get
			{
				return (UMGlobalCallRoutingScheme)this[UMDialPlanSchema.GlobalCallRoutingScheme];
			}
			set
			{
				this[UMDialPlanSchema.GlobalCallRoutingScheme] = value;
			}
		}

		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x06004929 RID: 18729 RVA: 0x0010F0E4 File Offset: 0x0010D2E4
		// (set) Token: 0x0600492A RID: 18730 RVA: 0x0010F0F6 File Offset: 0x0010D2F6
		[Parameter(Mandatory = false)]
		public bool TUIPromptEditingEnabled
		{
			get
			{
				return (bool)this[UMDialPlanSchema.TUIPromptEditingEnabled];
			}
			set
			{
				this[UMDialPlanSchema.TUIPromptEditingEnabled] = value;
			}
		}

		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x0600492B RID: 18731 RVA: 0x0010F109 File Offset: 0x0010D309
		// (set) Token: 0x0600492C RID: 18732 RVA: 0x0010F11B File Offset: 0x0010D31B
		[Parameter(Mandatory = false)]
		public bool CallAnsweringRulesEnabled
		{
			get
			{
				return (bool)this[UMDialPlanSchema.PersonalAutoAttendantEnabled];
			}
			set
			{
				this[UMDialPlanSchema.PersonalAutoAttendantEnabled] = value;
			}
		}

		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x0600492D RID: 18733 RVA: 0x0010F12E File Offset: 0x0010D32E
		// (set) Token: 0x0600492E RID: 18734 RVA: 0x0010F140 File Offset: 0x0010D340
		public bool SipResourceIdentifierRequired
		{
			get
			{
				return (bool)this[UMDialPlanSchema.SipResourceIdentifierRequired];
			}
			internal set
			{
				this[UMDialPlanSchema.SipResourceIdentifierRequired] = value;
			}
		}

		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x0600492F RID: 18735 RVA: 0x0010F153 File Offset: 0x0010D353
		public int FDSPollingInterval
		{
			get
			{
				return (int)this[UMDialPlanSchema.FDSPollingInterval];
			}
		}

		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x06004930 RID: 18736 RVA: 0x0010F165 File Offset: 0x0010D365
		// (set) Token: 0x06004931 RID: 18737 RVA: 0x0010F177 File Offset: 0x0010D377
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> EquivalentDialPlanPhoneContexts
		{
			get
			{
				return (MultiValuedProperty<string>)this[UMDialPlanSchema.EquivalentDialPlanPhoneContexts];
			}
			set
			{
				this[UMDialPlanSchema.EquivalentDialPlanPhoneContexts] = value;
			}
		}

		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x06004932 RID: 18738 RVA: 0x0010F185 File Offset: 0x0010D385
		// (set) Token: 0x06004933 RID: 18739 RVA: 0x0010F197 File Offset: 0x0010D397
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<UMNumberingPlanFormat> NumberingPlanFormats
		{
			get
			{
				return (MultiValuedProperty<UMNumberingPlanFormat>)this[UMDialPlanSchema.NumberingPlanFormats];
			}
			set
			{
				this[UMDialPlanSchema.NumberingPlanFormats] = value;
			}
		}

		// Token: 0x1700182C RID: 6188
		// (get) Token: 0x06004934 RID: 18740 RVA: 0x0010F1A5 File Offset: 0x0010D3A5
		// (set) Token: 0x06004935 RID: 18741 RVA: 0x0010F1B7 File Offset: 0x0010D3B7
		[Parameter(Mandatory = false)]
		public bool AllowHeuristicADCallingLineIdResolution
		{
			get
			{
				return (bool)this[UMDialPlanSchema.AllowHeuristicADCallingLineIdResolution];
			}
			set
			{
				this[UMDialPlanSchema.AllowHeuristicADCallingLineIdResolution] = value;
			}
		}

		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x06004936 RID: 18742 RVA: 0x0010F1CA File Offset: 0x0010D3CA
		// (set) Token: 0x06004937 RID: 18743 RVA: 0x0010F1DC File Offset: 0x0010D3DC
		public string CountryOrRegionCode
		{
			get
			{
				return (string)this[UMDialPlanSchema.CountryOrRegionCode];
			}
			set
			{
				this[UMDialPlanSchema.CountryOrRegionCode] = value;
			}
		}

		// Token: 0x1700182E RID: 6190
		// (get) Token: 0x06004938 RID: 18744 RVA: 0x0010F1EA File Offset: 0x0010D3EA
		// (set) Token: 0x06004939 RID: 18745 RVA: 0x0010F1FC File Offset: 0x0010D3FC
		internal string PromptChangeKey
		{
			get
			{
				return (string)this[UMDialPlanSchema.PromptChangeKey];
			}
			set
			{
				this[UMDialPlanSchema.PromptChangeKey] = value;
			}
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x0010F20C File Offset: 0x0010D40C
		internal bool TryMapNumberingPlan(string number, out string mappedNumber)
		{
			mappedNumber = null;
			string text = null;
			foreach (UMNumberingPlanFormat umnumberingPlanFormat in this.NumberingPlanFormats)
			{
				if (umnumberingPlanFormat.TryMapNumber(number, out text))
				{
					if (mappedNumber != null)
					{
						mappedNumber = null;
						break;
					}
					mappedNumber = text;
				}
			}
			return null != mappedNumber;
		}

		// Token: 0x0600493B RID: 18747 RVA: 0x0010F280 File Offset: 0x0010D480
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (base.IsModified(ADObjectSchema.Name) && base.ObjectState != ObjectState.New)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.DPCantChangeName, base.Id, string.Empty));
			}
			if (base.IsModified(UMDialPlanSchema.URIType) && base.ObjectState != ObjectState.New)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.CantSetDialPlanProperty("URIType"), base.Id, string.Empty));
			}
			if (base.IsModified(UMDialPlanSchema.NumberOfDigitsInExtension) && base.ObjectState != ObjectState.New)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.CantSetDialPlanProperty("NumberOfDigitsInExtension"), base.Id, string.Empty));
			}
			if (this.SubscriberType == UMSubscriberType.Consumer && base.ObjectState != ObjectState.New)
			{
				if (base.IsModified(UMDialPlanSchema.CallSomeoneEnabled) && this.CallSomeoneEnabled)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.InvalidConsumerDialPlanSetting("CallSomeoneEnabled"), base.Id, string.Empty));
				}
				if (base.IsModified(UMDialPlanSchema.SendVoiceMsgEnabled) && this.SendVoiceMsgEnabled)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.InvalidConsumerDialPlanSetting("SendVoiceMsgEnabled"), base.Id, string.Empty));
				}
			}
			if (this.CallSomeoneEnabled || this.SendVoiceMsgEnabled)
			{
				if (this.ContactScope == CallSomeoneScopeEnum.Extension)
				{
					if (string.IsNullOrEmpty(this.Extension))
					{
						errors.Add(new ObjectValidationError(DirectoryStrings.InvalidCallSomeoneScopeSettings("Extension", "Extension"), base.Id, string.Empty));
					}
					else if (this.Extension.Length != this.NumberOfDigitsInExtension)
					{
						errors.Add(new ObjectValidationError(DirectoryStrings.InvalidExtension("Extension", this.NumberOfDigitsInExtension), base.Id, string.Empty));
					}
				}
				if (this.ContactScope == CallSomeoneScopeEnum.AutoAttendantLink && this.UMAutoAttendant == null)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.InvalidCallSomeoneScopeSettings("AutoAttendantLink", "UMAutoAttendant"), base.Id, string.Empty));
				}
				if (this.ContactScope == CallSomeoneScopeEnum.AddressList && this.ContactAddressList == null)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.InvalidCallSomeoneScopeSettings("AddressList", "ContactAddressList"), base.Id, string.Empty));
				}
			}
			if (this.InfoAnnouncementEnabled != InfoAnnouncementEnabledEnum.False && string.IsNullOrEmpty(this.InfoAnnouncementFilename))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.SpecifyAnnouncementFileName, base.Id, string.Empty));
			}
			if (this.WelcomeGreetingEnabled && string.IsNullOrEmpty(this.WelcomeGreetingFilename))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.SpecifyCustomGreetingFileName, base.Id, string.Empty));
			}
			if (!this.SipResourceIdentifierRequired && (this.URIType != UMUriType.E164 || this.SubscriberType != UMSubscriberType.Enterprise))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.SipResourceIdentifierRequiredNotAllowed, base.Id, string.Empty));
			}
			if (string.IsNullOrEmpty(this.DefaultOutboundCallingLineId) && !this.SipResourceIdentifierRequired)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.MissingDefaultOutboundCallingLineId, base.Id, string.Empty));
			}
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x0010F564 File Offset: 0x0010D764
		internal bool CheckForAssociatedUsers()
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, base.Id),
				new ExistsFilter(UMDialPlanSchema.AssociatedUsers)
			});
			UMDialPlan[] array = base.Session.Find<UMDialPlan>(null, QueryScope.SubTree, filter, null, 1);
			return array != null && array.Length > 0;
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x0010F5BC File Offset: 0x0010D7BC
		internal bool CheckForAssociatedPolicies()
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, base.Id),
				new ExistsFilter(UMDialPlanSchema.AssociatedPolicies)
			});
			UMDialPlan[] array = base.Session.Find<UMDialPlan>(null, QueryScope.SubTree, filter, null, 1);
			return array != null && array.Length > 0;
		}

		// Token: 0x0600493E RID: 18750 RVA: 0x0010F614 File Offset: 0x0010D814
		internal string GetDefaultExtension(string phone)
		{
			if (phone != null && phone.Length >= this.NumberOfDigitsInExtension)
			{
				return phone.Substring(phone.Length - this.NumberOfDigitsInExtension, this.NumberOfDigitsInExtension);
			}
			return null;
		}

		// Token: 0x0600493F RID: 18751 RVA: 0x0010F642 File Offset: 0x0010D842
		internal bool SupportsAirSync()
		{
			return this.URIType == UMUriType.E164 && this.SubscriberType == UMSubscriberType.Consumer && !string.IsNullOrEmpty(this.CountryOrRegionCode);
		}

		// Token: 0x040032BD RID: 12989
		private static UMDialPlanSchema schema = ObjectSchema.GetInstance<UMDialPlanSchema>();

		// Token: 0x040032BE RID: 12990
		private static string mostDerivedClass = "msExchUMDialPlan";

		// Token: 0x040032BF RID: 12991
		private static ADObjectId parentPath = new ADObjectId("CN=UM DialPlan Container");
	}
}
