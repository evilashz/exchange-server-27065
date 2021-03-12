using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000477 RID: 1143
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class HostedSpamFilterConfig : ADConfigurationObject
	{
		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x060032DF RID: 13023 RVA: 0x000CD604 File Offset: 0x000CB804
		internal override ADObjectSchema Schema
		{
			get
			{
				return HostedSpamFilterConfig.SchemaObject;
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x060032E0 RID: 13024 RVA: 0x000CD60B File Offset: 0x000CB80B
		internal override ADObjectId ParentPath
		{
			get
			{
				return HostedSpamFilterConfig.HostedSpamFilteringContainer;
			}
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x060032E1 RID: 13025 RVA: 0x000CD612 File Offset: 0x000CB812
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchHostedContentFilterConfig";
			}
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000CD61C File Offset: 0x000CB81C
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (base.ObjectState == ObjectState.Changed)
			{
				if (this.MarkAsSpamSpfRecordHardFail == SpamFilteringOption.Test)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.PropertyCannotBeSetToTest, HostedSpamFilterConfigSchema.MarkAsSpamSpfRecordHardFail, this.MarkAsSpamSpfRecordHardFail));
				}
				if (this.MarkAsSpamNdrBackscatter == SpamFilteringOption.Test)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.PropertyCannotBeSetToTest, HostedSpamFilterConfigSchema.MarkAsSpamNdrBackscatter, this.MarkAsSpamNdrBackscatter));
				}
				if (this.MarkAsSpamFromAddressAuthFail == SpamFilteringOption.Test)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.PropertyCannotBeSetToTest, HostedSpamFilterConfigSchema.MarkAsSpamFromAddressAuthFail, this.MarkAsSpamFromAddressAuthFail));
				}
			}
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x060032E3 RID: 13027 RVA: 0x000CD6B4 File Offset: 0x000CB8B4
		// (set) Token: 0x060032E4 RID: 13028 RVA: 0x000CD6C6 File Offset: 0x000CB8C6
		[Parameter]
		public new string AdminDisplayName
		{
			get
			{
				return (string)this[ADConfigurationObjectSchema.AdminDisplayName];
			}
			set
			{
				this[ADConfigurationObjectSchema.AdminDisplayName] = value;
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x060032E5 RID: 13029 RVA: 0x000CD6D4 File Offset: 0x000CB8D4
		// (set) Token: 0x060032E6 RID: 13030 RVA: 0x000CD6E6 File Offset: 0x000CB8E6
		[Parameter]
		public string AddXHeaderValue
		{
			get
			{
				return (string)this[HostedSpamFilterConfigSchema.AddXHeaderValue];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.AddXHeaderValue] = value;
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x060032E7 RID: 13031 RVA: 0x000CD6F4 File Offset: 0x000CB8F4
		// (set) Token: 0x060032E8 RID: 13032 RVA: 0x000CD706 File Offset: 0x000CB906
		[Parameter]
		public string ModifySubjectValue
		{
			get
			{
				return (string)this[HostedSpamFilterConfigSchema.ModifySubjectValue];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.ModifySubjectValue] = value;
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x060032E9 RID: 13033 RVA: 0x000CD714 File Offset: 0x000CB914
		// (set) Token: 0x060032EA RID: 13034 RVA: 0x000CD726 File Offset: 0x000CB926
		[Parameter]
		public MultiValuedProperty<SmtpAddress> RedirectToRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[HostedSpamFilterConfigSchema.RedirectToRecipients];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.RedirectToRecipients] = value;
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x060032EB RID: 13035 RVA: 0x000CD734 File Offset: 0x000CB934
		// (set) Token: 0x060032EC RID: 13036 RVA: 0x000CD746 File Offset: 0x000CB946
		[Parameter]
		public MultiValuedProperty<SmtpAddress> TestModeBccToRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[HostedSpamFilterConfigSchema.TestModeBccToRecipients];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.TestModeBccToRecipients] = value;
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x060032ED RID: 13037 RVA: 0x000CD754 File Offset: 0x000CB954
		// (set) Token: 0x060032EE RID: 13038 RVA: 0x000CD766 File Offset: 0x000CB966
		[Parameter]
		public MultiValuedProperty<SmtpAddress> FalsePositiveAdditionalRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[HostedSpamFilterConfigSchema.FalsePositiveAdditionalRecipients];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.FalsePositiveAdditionalRecipients] = value;
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x060032EF RID: 13039 RVA: 0x000CD774 File Offset: 0x000CB974
		// (set) Token: 0x060032F0 RID: 13040 RVA: 0x000CD786 File Offset: 0x000CB986
		[Parameter]
		public MultiValuedProperty<SmtpAddress> BccSuspiciousOutboundAdditionalRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[HostedSpamFilterConfigSchema.BccSuspiciousOutboundAdditionalRecipients];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.BccSuspiciousOutboundAdditionalRecipients] = value;
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x060032F1 RID: 13041 RVA: 0x000CD794 File Offset: 0x000CB994
		// (set) Token: 0x060032F2 RID: 13042 RVA: 0x000CD7A6 File Offset: 0x000CB9A6
		[ValidateRange(1, 30)]
		[Parameter]
		public int QuarantineRetentionPeriod
		{
			get
			{
				return (int)this[HostedSpamFilterConfigSchema.QuarantineRetentionPeriod];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.QuarantineRetentionPeriod] = value;
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x060032F3 RID: 13043 RVA: 0x000CD7B9 File Offset: 0x000CB9B9
		// (set) Token: 0x060032F4 RID: 13044 RVA: 0x000CD7CB File Offset: 0x000CB9CB
		[Parameter]
		[ValidateRange(3, 30)]
		public int DigestFrequency
		{
			get
			{
				return (int)this[HostedSpamFilterConfigSchema.DigestFrequency];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.DigestFrequency] = value;
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x060032F5 RID: 13045 RVA: 0x000CD7DE File Offset: 0x000CB9DE
		// (set) Token: 0x060032F6 RID: 13046 RVA: 0x000CD7F0 File Offset: 0x000CB9F0
		[Parameter]
		public SpamFilteringTestModeActions TestModeAction
		{
			get
			{
				return (SpamFilteringTestModeActions)this[HostedSpamFilterConfigSchema.TestModeAction];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.TestModeAction] = (int)value;
			}
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x060032F7 RID: 13047 RVA: 0x000CD803 File Offset: 0x000CBA03
		// (set) Token: 0x060032F8 RID: 13048 RVA: 0x000CD815 File Offset: 0x000CBA15
		[Parameter]
		public SpamFilteringOption IncreaseScoreWithImageLinks
		{
			get
			{
				return (SpamFilteringOption)this[HostedSpamFilterConfigSchema.IncreaseScoreWithImageLinks];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.IncreaseScoreWithImageLinks] = value;
			}
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x060032F9 RID: 13049 RVA: 0x000CD828 File Offset: 0x000CBA28
		// (set) Token: 0x060032FA RID: 13050 RVA: 0x000CD83A File Offset: 0x000CBA3A
		[Parameter]
		public SpamFilteringOption IncreaseScoreWithNumericIps
		{
			get
			{
				return (SpamFilteringOption)this[HostedSpamFilterConfigSchema.IncreaseScoreWithNumericIps];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.IncreaseScoreWithNumericIps] = value;
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x060032FB RID: 13051 RVA: 0x000CD84D File Offset: 0x000CBA4D
		// (set) Token: 0x060032FC RID: 13052 RVA: 0x000CD85F File Offset: 0x000CBA5F
		[Parameter]
		public SpamFilteringOption IncreaseScoreWithRedirectToOtherPort
		{
			get
			{
				return (SpamFilteringOption)this[HostedSpamFilterConfigSchema.IncreaseScoreWithRedirectToOtherPort];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.IncreaseScoreWithRedirectToOtherPort] = value;
			}
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x060032FD RID: 13053 RVA: 0x000CD872 File Offset: 0x000CBA72
		// (set) Token: 0x060032FE RID: 13054 RVA: 0x000CD884 File Offset: 0x000CBA84
		[Parameter]
		public SpamFilteringOption IncreaseScoreWithBizOrInfoUrls
		{
			get
			{
				return (SpamFilteringOption)this[HostedSpamFilterConfigSchema.IncreaseScoreWithBizOrInfoUrls];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.IncreaseScoreWithBizOrInfoUrls] = value;
			}
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x000CD897 File Offset: 0x000CBA97
		// (set) Token: 0x06003300 RID: 13056 RVA: 0x000CD8A9 File Offset: 0x000CBAA9
		[Parameter]
		public SpamFilteringOption MarkAsSpamEmptyMessages
		{
			get
			{
				return (SpamFilteringOption)this[HostedSpamFilterConfigSchema.MarkAsSpamEmptyMessages];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.MarkAsSpamEmptyMessages] = value;
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x000CD8BC File Offset: 0x000CBABC
		// (set) Token: 0x06003302 RID: 13058 RVA: 0x000CD8CE File Offset: 0x000CBACE
		[Parameter]
		public SpamFilteringOption MarkAsSpamJavaScriptInHtml
		{
			get
			{
				return (SpamFilteringOption)this[HostedSpamFilterConfigSchema.MarkAsSpamJavaScriptInHtml];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.MarkAsSpamJavaScriptInHtml] = value;
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06003303 RID: 13059 RVA: 0x000CD8E1 File Offset: 0x000CBAE1
		// (set) Token: 0x06003304 RID: 13060 RVA: 0x000CD8F3 File Offset: 0x000CBAF3
		[Parameter]
		public SpamFilteringOption MarkAsSpamFramesInHtml
		{
			get
			{
				return (SpamFilteringOption)this[HostedSpamFilterConfigSchema.MarkAsSpamFramesInHtml];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.MarkAsSpamFramesInHtml] = value;
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06003305 RID: 13061 RVA: 0x000CD906 File Offset: 0x000CBB06
		// (set) Token: 0x06003306 RID: 13062 RVA: 0x000CD918 File Offset: 0x000CBB18
		[Parameter]
		public SpamFilteringOption MarkAsSpamObjectTagsInHtml
		{
			get
			{
				return (SpamFilteringOption)this[HostedSpamFilterConfigSchema.MarkAsSpamObjectTagsInHtml];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.MarkAsSpamObjectTagsInHtml] = value;
			}
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06003307 RID: 13063 RVA: 0x000CD92B File Offset: 0x000CBB2B
		// (set) Token: 0x06003308 RID: 13064 RVA: 0x000CD93D File Offset: 0x000CBB3D
		[Parameter]
		public SpamFilteringOption MarkAsSpamEmbedTagsInHtml
		{
			get
			{
				return (SpamFilteringOption)this[HostedSpamFilterConfigSchema.MarkAsSpamEmbedTagsInHtml];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.MarkAsSpamEmbedTagsInHtml] = value;
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06003309 RID: 13065 RVA: 0x000CD950 File Offset: 0x000CBB50
		// (set) Token: 0x0600330A RID: 13066 RVA: 0x000CD962 File Offset: 0x000CBB62
		[Parameter]
		public SpamFilteringOption MarkAsSpamFormTagsInHtml
		{
			get
			{
				return (SpamFilteringOption)this[HostedSpamFilterConfigSchema.MarkAsSpamFormTagsInHtml];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.MarkAsSpamFormTagsInHtml] = value;
			}
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x0600330B RID: 13067 RVA: 0x000CD975 File Offset: 0x000CBB75
		// (set) Token: 0x0600330C RID: 13068 RVA: 0x000CD987 File Offset: 0x000CBB87
		[Parameter]
		public SpamFilteringOption MarkAsSpamWebBugsInHtml
		{
			get
			{
				return (SpamFilteringOption)this[HostedSpamFilterConfigSchema.MarkAsSpamWebBugsInHtml];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.MarkAsSpamWebBugsInHtml] = value;
			}
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x0600330D RID: 13069 RVA: 0x000CD99A File Offset: 0x000CBB9A
		// (set) Token: 0x0600330E RID: 13070 RVA: 0x000CD9AC File Offset: 0x000CBBAC
		[Parameter]
		public SpamFilteringOption MarkAsSpamSensitiveWordList
		{
			get
			{
				return (SpamFilteringOption)this[HostedSpamFilterConfigSchema.MarkAsSpamSensitiveWordList];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.MarkAsSpamSensitiveWordList] = value;
			}
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x0600330F RID: 13071 RVA: 0x000CD9BF File Offset: 0x000CBBBF
		// (set) Token: 0x06003310 RID: 13072 RVA: 0x000CD9D1 File Offset: 0x000CBBD1
		[Parameter]
		public SpamFilteringOption MarkAsSpamSpfRecordHardFail
		{
			get
			{
				return (SpamFilteringOption)this[HostedSpamFilterConfigSchema.MarkAsSpamSpfRecordHardFail];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.MarkAsSpamSpfRecordHardFail] = value;
			}
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06003311 RID: 13073 RVA: 0x000CD9E4 File Offset: 0x000CBBE4
		// (set) Token: 0x06003312 RID: 13074 RVA: 0x000CD9F6 File Offset: 0x000CBBF6
		[Parameter]
		public SpamFilteringOption MarkAsSpamFromAddressAuthFail
		{
			get
			{
				return (SpamFilteringOption)this[HostedSpamFilterConfigSchema.MarkAsSpamFromAddressAuthFail];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.MarkAsSpamFromAddressAuthFail] = value;
			}
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06003313 RID: 13075 RVA: 0x000CDA09 File Offset: 0x000CBC09
		// (set) Token: 0x06003314 RID: 13076 RVA: 0x000CDA1B File Offset: 0x000CBC1B
		[Parameter]
		public SpamFilteringOption MarkAsSpamNdrBackscatter
		{
			get
			{
				return (SpamFilteringOption)this[HostedSpamFilterConfigSchema.MarkAsSpamNdrBackscatter];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.MarkAsSpamNdrBackscatter] = value;
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x000CDA2E File Offset: 0x000CBC2E
		// (set) Token: 0x06003316 RID: 13078 RVA: 0x000CDA40 File Offset: 0x000CBC40
		[Parameter]
		public MultiValuedProperty<IPRange> IPAllowList
		{
			get
			{
				return (MultiValuedProperty<IPRange>)this[HostedSpamFilterConfigSchema.IPAllowList];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.IPAllowList] = value;
			}
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06003317 RID: 13079 RVA: 0x000CDA4E File Offset: 0x000CBC4E
		// (set) Token: 0x06003318 RID: 13080 RVA: 0x000CDA60 File Offset: 0x000CBC60
		[Parameter]
		public MultiValuedProperty<IPRange> IPBlockList
		{
			get
			{
				return (MultiValuedProperty<IPRange>)this[HostedSpamFilterConfigSchema.IPBlockList];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.IPBlockList] = value;
			}
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06003319 RID: 13081 RVA: 0x000CDA6E File Offset: 0x000CBC6E
		// (set) Token: 0x0600331A RID: 13082 RVA: 0x000CDA80 File Offset: 0x000CBC80
		public bool IsDefault
		{
			get
			{
				return (bool)this[HostedSpamFilterConfigSchema.IsDefault];
			}
			internal set
			{
				this[HostedSpamFilterConfigSchema.IsDefault] = value;
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x0600331B RID: 13083 RVA: 0x000CDA93 File Offset: 0x000CBC93
		// (set) Token: 0x0600331C RID: 13084 RVA: 0x000CDAA5 File Offset: 0x000CBCA5
		[Parameter]
		public MultiValuedProperty<SmtpAddress> NotifyOutboundSpamRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[HostedSpamFilterConfigSchema.NotifyOutboundSpamRecipients];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.NotifyOutboundSpamRecipients] = value;
			}
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x0600331D RID: 13085 RVA: 0x000CDAB3 File Offset: 0x000CBCB3
		// (set) Token: 0x0600331E RID: 13086 RVA: 0x000CDAC5 File Offset: 0x000CBCC5
		[Parameter]
		public MultiValuedProperty<string> LanguageBlockList
		{
			get
			{
				return (MultiValuedProperty<string>)this[HostedSpamFilterConfigSchema.LanguageBlockList];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.LanguageBlockList] = value;
			}
		}

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x0600331F RID: 13087 RVA: 0x000CDAD3 File Offset: 0x000CBCD3
		// (set) Token: 0x06003320 RID: 13088 RVA: 0x000CDAE5 File Offset: 0x000CBCE5
		[Parameter]
		public MultiValuedProperty<string> CountryBlockList
		{
			get
			{
				return (MultiValuedProperty<string>)this[HostedSpamFilterConfigSchema.CountryBlockList];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.CountryBlockList] = value;
			}
		}

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06003321 RID: 13089 RVA: 0x000CDAF3 File Offset: 0x000CBCF3
		// (set) Token: 0x06003322 RID: 13090 RVA: 0x000CDB05 File Offset: 0x000CBD05
		[Parameter]
		public SpamFilteringAction HighConfidenceAction
		{
			get
			{
				return (SpamFilteringAction)this[HostedSpamFilterConfigSchema.HighConfidenceAction];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.HighConfidenceAction] = (int)value;
			}
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06003323 RID: 13091 RVA: 0x000CDB18 File Offset: 0x000CBD18
		// (set) Token: 0x06003324 RID: 13092 RVA: 0x000CDB2A File Offset: 0x000CBD2A
		[Parameter]
		public SpamFilteringAction MediumConfidenceAction
		{
			get
			{
				return (SpamFilteringAction)this[HostedSpamFilterConfigSchema.MediumConfidenceAction];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.MediumConfidenceAction] = (int)value;
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06003325 RID: 13093 RVA: 0x000CDB3D File Offset: 0x000CBD3D
		// (set) Token: 0x06003326 RID: 13094 RVA: 0x000CDB4F File Offset: 0x000CBD4F
		[Parameter]
		public SpamFilteringAction LowConfidenceAction
		{
			get
			{
				return (SpamFilteringAction)this[HostedSpamFilterConfigSchema.LowConfidenceAction];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.LowConfidenceAction] = (int)value;
			}
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06003327 RID: 13095 RVA: 0x000CDB62 File Offset: 0x000CBD62
		// (set) Token: 0x06003328 RID: 13096 RVA: 0x000CDB74 File Offset: 0x000CBD74
		[Parameter]
		public bool BccSuspiciousOutboundMail
		{
			get
			{
				return (bool)this[HostedSpamFilterConfigSchema.BccSuspiciousOutboundMail];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.BccSuspiciousOutboundMail] = value;
			}
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06003329 RID: 13097 RVA: 0x000CDB87 File Offset: 0x000CBD87
		// (set) Token: 0x0600332A RID: 13098 RVA: 0x000CDB99 File Offset: 0x000CBD99
		[Parameter]
		public bool NotifyOutboundSpam
		{
			get
			{
				return (bool)this[HostedSpamFilterConfigSchema.NotifyOutboundSpam];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.NotifyOutboundSpam] = value;
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x0600332B RID: 13099 RVA: 0x000CDBAC File Offset: 0x000CBDAC
		// (set) Token: 0x0600332C RID: 13100 RVA: 0x000CDBBE File Offset: 0x000CBDBE
		[Parameter]
		public bool MoveToJmfEnableHostedQuarantine
		{
			get
			{
				return (bool)this[HostedSpamFilterConfigSchema.MoveToJmfEnableHostedQuarantine];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.MoveToJmfEnableHostedQuarantine] = value;
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x0600332D RID: 13101 RVA: 0x000CDBD1 File Offset: 0x000CBDD1
		// (set) Token: 0x0600332E RID: 13102 RVA: 0x000CDBE3 File Offset: 0x000CBDE3
		[Parameter]
		public bool EnableDigests
		{
			get
			{
				return (bool)this[HostedSpamFilterConfigSchema.EnableDigests];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.EnableDigests] = value;
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x0600332F RID: 13103 RVA: 0x000CDBF6 File Offset: 0x000CBDF6
		// (set) Token: 0x06003330 RID: 13104 RVA: 0x000CDC08 File Offset: 0x000CBE08
		[Parameter]
		public bool DownloadLink
		{
			get
			{
				return (bool)this[HostedSpamFilterConfigSchema.DownloadLink];
			}
			set
			{
				this[HostedSpamFilterConfigSchema.DownloadLink] = value;
			}
		}

		// Token: 0x04002349 RID: 9033
		internal const string LdapName = "msExchHostedContentFilterConfig";

		// Token: 0x0400234A RID: 9034
		internal static readonly ADObjectId HostedSpamFilteringContainer = new ADObjectId("CN=Hosted Spam Filtering,CN=Transport Settings");

		// Token: 0x0400234B RID: 9035
		private static readonly HostedSpamFilterConfigSchema SchemaObject = ObjectSchema.GetInstance<HostedSpamFilterConfigSchema>();
	}
}
