using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000472 RID: 1138
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class HostedContentFilterPolicy : ADConfigurationObject
	{
		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x0600326B RID: 12907 RVA: 0x000CBEDF File Offset: 0x000CA0DF
		internal override ADObjectSchema Schema
		{
			get
			{
				return HostedContentFilterPolicy.schema;
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x0600326C RID: 12908 RVA: 0x000CBEE6 File Offset: 0x000CA0E6
		internal override ADObjectId ParentPath
		{
			get
			{
				return HostedContentFilterPolicy.parentPath;
			}
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x0600326D RID: 12909 RVA: 0x000CBEED File Offset: 0x000CA0ED
		internal override string MostDerivedObjectClass
		{
			get
			{
				return HostedContentFilterPolicy.ldapName;
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x0600326E RID: 12910 RVA: 0x000CBEF4 File Offset: 0x000CA0F4
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000CBEFC File Offset: 0x000CA0FC
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.MarkAsSpamSpfRecordHardFail == SpamFilteringOption.Test)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.PropertyCannotBeSetToTest, HostedContentFilterPolicySchema.MarkAsSpamSpfRecordHardFail, this.MarkAsSpamSpfRecordHardFail));
			}
			if (this.MarkAsSpamNdrBackscatter == SpamFilteringOption.Test)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.PropertyCannotBeSetToTest, HostedContentFilterPolicySchema.MarkAsSpamNdrBackscatter, this.MarkAsSpamNdrBackscatter));
			}
			if (this.MarkAsSpamFromAddressAuthFail == SpamFilteringOption.Test)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.PropertyCannotBeSetToTest, HostedContentFilterPolicySchema.MarkAsSpamFromAddressAuthFail, this.MarkAsSpamFromAddressAuthFail));
			}
			if ((this.HighConfidenceSpamAction == SpamFilteringAction.AddXHeader || this.SpamAction == SpamFilteringAction.AddXHeader) && string.IsNullOrEmpty(this.AddXHeaderValue))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.XHeaderValueNotSet, HostedContentFilterPolicySchema.AddXHeaderValue, this.AddXHeaderValue));
			}
			if ((this.HighConfidenceSpamAction == SpamFilteringAction.ModifySubject || this.SpamAction == SpamFilteringAction.ModifySubject) && string.IsNullOrEmpty(this.ModifySubjectValue))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ModifySubjectValueNotSet, HostedContentFilterPolicySchema.ModifySubjectValue, this.ModifySubjectValue));
			}
			if ((this.HighConfidenceSpamAction == SpamFilteringAction.Redirect || this.SpamAction == SpamFilteringAction.Redirect) && (this.RedirectToRecipients == null || this.RedirectToRecipients.Count == 0))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.RedirectToRecipientsNotSet, HostedContentFilterPolicySchema.RedirectToRecipients, this.RedirectToRecipients));
			}
			if (this.EnableLanguageBlockList && (this.LanguageBlockList == null || this.LanguageBlockList.Count == 0))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.LanguageBlockListNotSet, HostedContentFilterPolicySchema.LanguageBlockList, this.LanguageBlockList));
			}
			if (this.EnableRegionBlockList && (this.RegionBlockList == null || this.RegionBlockList.Count == 0))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.RegionBlockListNotSet, HostedContentFilterPolicySchema.RegionBlockList, this.RegionBlockList));
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06003270 RID: 12912 RVA: 0x000CC0B1 File Offset: 0x000CA2B1
		// (set) Token: 0x06003271 RID: 12913 RVA: 0x000CC0C3 File Offset: 0x000CA2C3
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

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06003272 RID: 12914 RVA: 0x000CC0D1 File Offset: 0x000CA2D1
		// (set) Token: 0x06003273 RID: 12915 RVA: 0x000CC0E3 File Offset: 0x000CA2E3
		[Parameter]
		public string AddXHeaderValue
		{
			get
			{
				return (string)this[HostedContentFilterPolicySchema.AddXHeaderValue];
			}
			set
			{
				this[HostedContentFilterPolicySchema.AddXHeaderValue] = value;
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06003274 RID: 12916 RVA: 0x000CC0F1 File Offset: 0x000CA2F1
		// (set) Token: 0x06003275 RID: 12917 RVA: 0x000CC103 File Offset: 0x000CA303
		[Parameter]
		public string ModifySubjectValue
		{
			get
			{
				return (string)this[HostedContentFilterPolicySchema.ModifySubjectValue];
			}
			set
			{
				this[HostedContentFilterPolicySchema.ModifySubjectValue] = value;
			}
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06003276 RID: 12918 RVA: 0x000CC111 File Offset: 0x000CA311
		// (set) Token: 0x06003277 RID: 12919 RVA: 0x000CC123 File Offset: 0x000CA323
		[Parameter]
		public MultiValuedProperty<SmtpAddress> RedirectToRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[HostedContentFilterPolicySchema.RedirectToRecipients];
			}
			set
			{
				this[HostedContentFilterPolicySchema.RedirectToRecipients] = value;
			}
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06003278 RID: 12920 RVA: 0x000CC131 File Offset: 0x000CA331
		// (set) Token: 0x06003279 RID: 12921 RVA: 0x000CC143 File Offset: 0x000CA343
		[Parameter]
		public MultiValuedProperty<SmtpAddress> TestModeBccToRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[HostedContentFilterPolicySchema.TestModeBccToRecipients];
			}
			set
			{
				this[HostedContentFilterPolicySchema.TestModeBccToRecipients] = value;
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x0600327A RID: 12922 RVA: 0x000CC151 File Offset: 0x000CA351
		// (set) Token: 0x0600327B RID: 12923 RVA: 0x000CC163 File Offset: 0x000CA363
		[Parameter]
		public MultiValuedProperty<SmtpAddress> FalsePositiveAdditionalRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[HostedContentFilterPolicySchema.FalsePositiveAdditionalRecipients];
			}
			set
			{
				this[HostedContentFilterPolicySchema.FalsePositiveAdditionalRecipients] = value;
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x0600327C RID: 12924 RVA: 0x000CC171 File Offset: 0x000CA371
		// (set) Token: 0x0600327D RID: 12925 RVA: 0x000CC183 File Offset: 0x000CA383
		[Parameter]
		public int QuarantineRetentionPeriod
		{
			get
			{
				return (int)this[HostedContentFilterPolicySchema.QuarantineRetentionPeriod];
			}
			set
			{
				this[HostedContentFilterPolicySchema.QuarantineRetentionPeriod] = value;
			}
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x0600327E RID: 12926 RVA: 0x000CC196 File Offset: 0x000CA396
		// (set) Token: 0x0600327F RID: 12927 RVA: 0x000CC1A8 File Offset: 0x000CA3A8
		[Parameter]
		public int EndUserSpamNotificationFrequency
		{
			get
			{
				return (int)this[HostedContentFilterPolicySchema.EndUserSpamNotificationFrequency];
			}
			set
			{
				this[HostedContentFilterPolicySchema.EndUserSpamNotificationFrequency] = value;
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06003280 RID: 12928 RVA: 0x000CC1BB File Offset: 0x000CA3BB
		// (set) Token: 0x06003281 RID: 12929 RVA: 0x000CC1CD File Offset: 0x000CA3CD
		[Parameter]
		public SpamFilteringTestModeAction TestModeAction
		{
			get
			{
				return (SpamFilteringTestModeAction)this[HostedContentFilterPolicySchema.TestModeAction];
			}
			set
			{
				this[HostedContentFilterPolicySchema.TestModeAction] = (int)value;
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06003282 RID: 12930 RVA: 0x000CC1E0 File Offset: 0x000CA3E0
		// (set) Token: 0x06003283 RID: 12931 RVA: 0x000CC1F2 File Offset: 0x000CA3F2
		[Parameter]
		public SpamFilteringOption IncreaseScoreWithImageLinks
		{
			get
			{
				return (SpamFilteringOption)this[HostedContentFilterPolicySchema.IncreaseScoreWithImageLinks];
			}
			set
			{
				this[HostedContentFilterPolicySchema.IncreaseScoreWithImageLinks] = value;
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06003284 RID: 12932 RVA: 0x000CC205 File Offset: 0x000CA405
		// (set) Token: 0x06003285 RID: 12933 RVA: 0x000CC217 File Offset: 0x000CA417
		[Parameter]
		public SpamFilteringOption IncreaseScoreWithNumericIps
		{
			get
			{
				return (SpamFilteringOption)this[HostedContentFilterPolicySchema.IncreaseScoreWithNumericIps];
			}
			set
			{
				this[HostedContentFilterPolicySchema.IncreaseScoreWithNumericIps] = value;
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06003286 RID: 12934 RVA: 0x000CC22A File Offset: 0x000CA42A
		// (set) Token: 0x06003287 RID: 12935 RVA: 0x000CC23C File Offset: 0x000CA43C
		[Parameter]
		public SpamFilteringOption IncreaseScoreWithRedirectToOtherPort
		{
			get
			{
				return (SpamFilteringOption)this[HostedContentFilterPolicySchema.IncreaseScoreWithRedirectToOtherPort];
			}
			set
			{
				this[HostedContentFilterPolicySchema.IncreaseScoreWithRedirectToOtherPort] = value;
			}
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06003288 RID: 12936 RVA: 0x000CC24F File Offset: 0x000CA44F
		// (set) Token: 0x06003289 RID: 12937 RVA: 0x000CC261 File Offset: 0x000CA461
		[Parameter]
		public SpamFilteringOption IncreaseScoreWithBizOrInfoUrls
		{
			get
			{
				return (SpamFilteringOption)this[HostedContentFilterPolicySchema.IncreaseScoreWithBizOrInfoUrls];
			}
			set
			{
				this[HostedContentFilterPolicySchema.IncreaseScoreWithBizOrInfoUrls] = value;
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x0600328A RID: 12938 RVA: 0x000CC274 File Offset: 0x000CA474
		// (set) Token: 0x0600328B RID: 12939 RVA: 0x000CC286 File Offset: 0x000CA486
		[Parameter]
		public SpamFilteringOption MarkAsSpamEmptyMessages
		{
			get
			{
				return (SpamFilteringOption)this[HostedContentFilterPolicySchema.MarkAsSpamEmptyMessages];
			}
			set
			{
				this[HostedContentFilterPolicySchema.MarkAsSpamEmptyMessages] = value;
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x0600328C RID: 12940 RVA: 0x000CC299 File Offset: 0x000CA499
		// (set) Token: 0x0600328D RID: 12941 RVA: 0x000CC2AB File Offset: 0x000CA4AB
		[Parameter]
		public SpamFilteringOption MarkAsSpamJavaScriptInHtml
		{
			get
			{
				return (SpamFilteringOption)this[HostedContentFilterPolicySchema.MarkAsSpamJavaScriptInHtml];
			}
			set
			{
				this[HostedContentFilterPolicySchema.MarkAsSpamJavaScriptInHtml] = value;
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x0600328E RID: 12942 RVA: 0x000CC2BE File Offset: 0x000CA4BE
		// (set) Token: 0x0600328F RID: 12943 RVA: 0x000CC2D0 File Offset: 0x000CA4D0
		[Parameter]
		public SpamFilteringOption MarkAsSpamFramesInHtml
		{
			get
			{
				return (SpamFilteringOption)this[HostedContentFilterPolicySchema.MarkAsSpamFramesInHtml];
			}
			set
			{
				this[HostedContentFilterPolicySchema.MarkAsSpamFramesInHtml] = value;
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06003290 RID: 12944 RVA: 0x000CC2E3 File Offset: 0x000CA4E3
		// (set) Token: 0x06003291 RID: 12945 RVA: 0x000CC2F5 File Offset: 0x000CA4F5
		[Parameter]
		public SpamFilteringOption MarkAsSpamObjectTagsInHtml
		{
			get
			{
				return (SpamFilteringOption)this[HostedContentFilterPolicySchema.MarkAsSpamObjectTagsInHtml];
			}
			set
			{
				this[HostedContentFilterPolicySchema.MarkAsSpamObjectTagsInHtml] = value;
			}
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x000CC308 File Offset: 0x000CA508
		// (set) Token: 0x06003293 RID: 12947 RVA: 0x000CC31A File Offset: 0x000CA51A
		[Parameter]
		public SpamFilteringOption MarkAsSpamEmbedTagsInHtml
		{
			get
			{
				return (SpamFilteringOption)this[HostedContentFilterPolicySchema.MarkAsSpamEmbedTagsInHtml];
			}
			set
			{
				this[HostedContentFilterPolicySchema.MarkAsSpamEmbedTagsInHtml] = value;
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06003294 RID: 12948 RVA: 0x000CC32D File Offset: 0x000CA52D
		// (set) Token: 0x06003295 RID: 12949 RVA: 0x000CC33F File Offset: 0x000CA53F
		[Parameter]
		public SpamFilteringOption MarkAsSpamFormTagsInHtml
		{
			get
			{
				return (SpamFilteringOption)this[HostedContentFilterPolicySchema.MarkAsSpamFormTagsInHtml];
			}
			set
			{
				this[HostedContentFilterPolicySchema.MarkAsSpamFormTagsInHtml] = value;
			}
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06003296 RID: 12950 RVA: 0x000CC352 File Offset: 0x000CA552
		// (set) Token: 0x06003297 RID: 12951 RVA: 0x000CC364 File Offset: 0x000CA564
		[Parameter]
		public SpamFilteringOption MarkAsSpamWebBugsInHtml
		{
			get
			{
				return (SpamFilteringOption)this[HostedContentFilterPolicySchema.MarkAsSpamWebBugsInHtml];
			}
			set
			{
				this[HostedContentFilterPolicySchema.MarkAsSpamWebBugsInHtml] = value;
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06003298 RID: 12952 RVA: 0x000CC377 File Offset: 0x000CA577
		// (set) Token: 0x06003299 RID: 12953 RVA: 0x000CC389 File Offset: 0x000CA589
		[Parameter]
		public SpamFilteringOption MarkAsSpamSensitiveWordList
		{
			get
			{
				return (SpamFilteringOption)this[HostedContentFilterPolicySchema.MarkAsSpamSensitiveWordList];
			}
			set
			{
				this[HostedContentFilterPolicySchema.MarkAsSpamSensitiveWordList] = value;
			}
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x0600329A RID: 12954 RVA: 0x000CC39C File Offset: 0x000CA59C
		// (set) Token: 0x0600329B RID: 12955 RVA: 0x000CC3AE File Offset: 0x000CA5AE
		[Parameter]
		public SpamFilteringOption MarkAsSpamSpfRecordHardFail
		{
			get
			{
				return (SpamFilteringOption)this[HostedContentFilterPolicySchema.MarkAsSpamSpfRecordHardFail];
			}
			set
			{
				this[HostedContentFilterPolicySchema.MarkAsSpamSpfRecordHardFail] = value;
			}
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x0600329C RID: 12956 RVA: 0x000CC3C1 File Offset: 0x000CA5C1
		// (set) Token: 0x0600329D RID: 12957 RVA: 0x000CC3D3 File Offset: 0x000CA5D3
		[Parameter]
		public SpamFilteringOption MarkAsSpamFromAddressAuthFail
		{
			get
			{
				return (SpamFilteringOption)this[HostedContentFilterPolicySchema.MarkAsSpamFromAddressAuthFail];
			}
			set
			{
				this[HostedContentFilterPolicySchema.MarkAsSpamFromAddressAuthFail] = value;
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x0600329E RID: 12958 RVA: 0x000CC3E6 File Offset: 0x000CA5E6
		// (set) Token: 0x0600329F RID: 12959 RVA: 0x000CC3F8 File Offset: 0x000CA5F8
		[Parameter]
		public SpamFilteringOption MarkAsSpamBulkMail
		{
			get
			{
				return (SpamFilteringOption)this[HostedContentFilterPolicySchema.MarkAsSpamBulkMail];
			}
			set
			{
				this[HostedContentFilterPolicySchema.MarkAsSpamBulkMail] = value;
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x060032A0 RID: 12960 RVA: 0x000CC40B File Offset: 0x000CA60B
		// (set) Token: 0x060032A1 RID: 12961 RVA: 0x000CC41D File Offset: 0x000CA61D
		[Parameter]
		public SpamFilteringOption MarkAsSpamNdrBackscatter
		{
			get
			{
				return (SpamFilteringOption)this[HostedContentFilterPolicySchema.MarkAsSpamNdrBackscatter];
			}
			set
			{
				this[HostedContentFilterPolicySchema.MarkAsSpamNdrBackscatter] = value;
			}
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x060032A2 RID: 12962 RVA: 0x000CC430 File Offset: 0x000CA630
		// (set) Token: 0x060032A3 RID: 12963 RVA: 0x000CC442 File Offset: 0x000CA642
		public bool IsDefault
		{
			get
			{
				return (bool)this[HostedContentFilterPolicySchema.IsDefault];
			}
			internal set
			{
				this[HostedContentFilterPolicySchema.IsDefault] = value;
			}
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x060032A4 RID: 12964 RVA: 0x000CC455 File Offset: 0x000CA655
		// (set) Token: 0x060032A5 RID: 12965 RVA: 0x000CC467 File Offset: 0x000CA667
		[Parameter]
		public MultiValuedProperty<string> LanguageBlockList
		{
			get
			{
				return (MultiValuedProperty<string>)this[HostedContentFilterPolicySchema.LanguageBlockList];
			}
			set
			{
				this[HostedContentFilterPolicySchema.LanguageBlockList] = value;
			}
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x060032A6 RID: 12966 RVA: 0x000CC475 File Offset: 0x000CA675
		// (set) Token: 0x060032A7 RID: 12967 RVA: 0x000CC487 File Offset: 0x000CA687
		[Parameter]
		public MultiValuedProperty<string> RegionBlockList
		{
			get
			{
				return (MultiValuedProperty<string>)this[HostedContentFilterPolicySchema.RegionBlockList];
			}
			set
			{
				this[HostedContentFilterPolicySchema.RegionBlockList] = value;
			}
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x060032A8 RID: 12968 RVA: 0x000CC495 File Offset: 0x000CA695
		// (set) Token: 0x060032A9 RID: 12969 RVA: 0x000CC4A7 File Offset: 0x000CA6A7
		[Parameter]
		public SpamFilteringAction HighConfidenceSpamAction
		{
			get
			{
				return (SpamFilteringAction)this[HostedContentFilterPolicySchema.HighConfidenceSpamAction];
			}
			set
			{
				this[HostedContentFilterPolicySchema.HighConfidenceSpamAction] = (int)value;
			}
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x060032AA RID: 12970 RVA: 0x000CC4BA File Offset: 0x000CA6BA
		// (set) Token: 0x060032AB RID: 12971 RVA: 0x000CC4CC File Offset: 0x000CA6CC
		[Parameter]
		public SpamFilteringAction SpamAction
		{
			get
			{
				return (SpamFilteringAction)this[HostedContentFilterPolicySchema.SpamAction];
			}
			set
			{
				this[HostedContentFilterPolicySchema.SpamAction] = (int)value;
			}
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x060032AC RID: 12972 RVA: 0x000CC4DF File Offset: 0x000CA6DF
		// (set) Token: 0x060032AD RID: 12973 RVA: 0x000CC4F1 File Offset: 0x000CA6F1
		[Parameter]
		public bool EnableEndUserSpamNotifications
		{
			get
			{
				return (bool)this[HostedContentFilterPolicySchema.EnableEndUserSpamNotifications];
			}
			set
			{
				this[HostedContentFilterPolicySchema.EnableEndUserSpamNotifications] = value;
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x060032AE RID: 12974 RVA: 0x000CC504 File Offset: 0x000CA704
		// (set) Token: 0x060032AF RID: 12975 RVA: 0x000CC516 File Offset: 0x000CA716
		[Parameter]
		public bool DownloadLink
		{
			get
			{
				return (bool)this[HostedContentFilterPolicySchema.DownloadLink];
			}
			set
			{
				this[HostedContentFilterPolicySchema.DownloadLink] = value;
			}
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x060032B0 RID: 12976 RVA: 0x000CC529 File Offset: 0x000CA729
		// (set) Token: 0x060032B1 RID: 12977 RVA: 0x000CC53B File Offset: 0x000CA73B
		[Parameter]
		public bool EnableRegionBlockList
		{
			get
			{
				return (bool)this[HostedContentFilterPolicySchema.EnableRegionBlockList];
			}
			set
			{
				this[HostedContentFilterPolicySchema.EnableRegionBlockList] = value;
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x060032B2 RID: 12978 RVA: 0x000CC54E File Offset: 0x000CA74E
		// (set) Token: 0x060032B3 RID: 12979 RVA: 0x000CC560 File Offset: 0x000CA760
		[Parameter]
		public bool EnableLanguageBlockList
		{
			get
			{
				return (bool)this[HostedContentFilterPolicySchema.EnableLanguageBlockList];
			}
			set
			{
				this[HostedContentFilterPolicySchema.EnableLanguageBlockList] = value;
			}
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x060032B4 RID: 12980 RVA: 0x000CC573 File Offset: 0x000CA773
		// (set) Token: 0x060032B5 RID: 12981 RVA: 0x000CC585 File Offset: 0x000CA785
		[Parameter]
		public SmtpAddress EndUserSpamNotificationCustomFromAddress
		{
			get
			{
				return (SmtpAddress)this[HostedContentFilterPolicySchema.EndUserSpamNotificationCustomFromAddress];
			}
			set
			{
				this[HostedContentFilterPolicySchema.EndUserSpamNotificationCustomFromAddress] = value;
			}
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x060032B6 RID: 12982 RVA: 0x000CC598 File Offset: 0x000CA798
		// (set) Token: 0x060032B7 RID: 12983 RVA: 0x000CC5AA File Offset: 0x000CA7AA
		[Parameter]
		public string EndUserSpamNotificationCustomFromName
		{
			get
			{
				return (string)this[HostedContentFilterPolicySchema.EndUserSpamNotificationCustomFromName];
			}
			set
			{
				this[HostedContentFilterPolicySchema.EndUserSpamNotificationCustomFromName] = value;
			}
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x060032B8 RID: 12984 RVA: 0x000CC5B8 File Offset: 0x000CA7B8
		// (set) Token: 0x060032B9 RID: 12985 RVA: 0x000CC5CA File Offset: 0x000CA7CA
		[Parameter]
		public string EndUserSpamNotificationCustomSubject
		{
			get
			{
				return (string)this[HostedContentFilterPolicySchema.EndUserSpamNotificationCustomSubject];
			}
			set
			{
				this[HostedContentFilterPolicySchema.EndUserSpamNotificationCustomSubject] = value;
			}
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x060032BA RID: 12986 RVA: 0x000CC5D8 File Offset: 0x000CA7D8
		// (set) Token: 0x060032BB RID: 12987 RVA: 0x000CC5EA File Offset: 0x000CA7EA
		[Parameter]
		public EsnLanguage EndUserSpamNotificationLanguage
		{
			get
			{
				return (EsnLanguage)this[HostedContentFilterPolicySchema.EndUserSpamNotificationLanguage];
			}
			set
			{
				this[HostedContentFilterPolicySchema.EndUserSpamNotificationLanguage] = value;
			}
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x060032BC RID: 12988 RVA: 0x000CC5FD File Offset: 0x000CA7FD
		// (set) Token: 0x060032BD RID: 12989 RVA: 0x000CC60F File Offset: 0x000CA80F
		[Parameter]
		public int EndUserSpamNotificationLimit
		{
			get
			{
				return (int)this[HostedContentFilterPolicySchema.EndUserSpamNotificationLimit];
			}
			set
			{
				this[HostedContentFilterPolicySchema.EndUserSpamNotificationLimit] = value;
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x060032BE RID: 12990 RVA: 0x000CC622 File Offset: 0x000CA822
		// (set) Token: 0x060032BF RID: 12991 RVA: 0x000CC634 File Offset: 0x000CA834
		[Parameter]
		public int BulkThreshold
		{
			get
			{
				return (int)this[HostedContentFilterPolicySchema.BulkThreshold];
			}
			set
			{
				this[HostedContentFilterPolicySchema.BulkThreshold] = value;
			}
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x000CC647 File Offset: 0x000CA847
		internal bool IsConflicted()
		{
			return ADSession.IsCNFObject(base.Id);
		}

		// Token: 0x040022F6 RID: 8950
		private static readonly string ldapName = "msExchHostedContentFilterConfig";

		// Token: 0x040022F7 RID: 8951
		private static readonly ADObjectId parentPath = new ADObjectId("CN=Hosted Content Filter,CN=Transport Settings");

		// Token: 0x040022F8 RID: 8952
		private static readonly HostedContentFilterPolicySchema schema = ObjectSchema.GetInstance<HostedContentFilterPolicySchema>();
	}
}
