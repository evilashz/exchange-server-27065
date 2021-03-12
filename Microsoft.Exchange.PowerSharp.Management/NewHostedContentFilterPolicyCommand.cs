using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000712 RID: 1810
	public class NewHostedContentFilterPolicyCommand : SyntheticCommandWithPipelineInput<HostedContentFilterPolicy, HostedContentFilterPolicy>
	{
		// Token: 0x06005D4E RID: 23886 RVA: 0x00090AE1 File Offset: 0x0008ECE1
		private NewHostedContentFilterPolicyCommand() : base("New-HostedContentFilterPolicy")
		{
		}

		// Token: 0x06005D4F RID: 23887 RVA: 0x00090AEE File Offset: 0x0008ECEE
		public NewHostedContentFilterPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005D50 RID: 23888 RVA: 0x00090AFD File Offset: 0x0008ECFD
		public virtual NewHostedContentFilterPolicyCommand SetParameters(NewHostedContentFilterPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000713 RID: 1811
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003AE3 RID: 15075
			// (set) Token: 0x06005D51 RID: 23889 RVA: 0x00090B07 File Offset: 0x0008ED07
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17003AE4 RID: 15076
			// (set) Token: 0x06005D52 RID: 23890 RVA: 0x00090B1F File Offset: 0x0008ED1F
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003AE5 RID: 15077
			// (set) Token: 0x06005D53 RID: 23891 RVA: 0x00090B32 File Offset: 0x0008ED32
			public virtual string AddXHeaderValue
			{
				set
				{
					base.PowerSharpParameters["AddXHeaderValue"] = value;
				}
			}

			// Token: 0x17003AE6 RID: 15078
			// (set) Token: 0x06005D54 RID: 23892 RVA: 0x00090B45 File Offset: 0x0008ED45
			public virtual string ModifySubjectValue
			{
				set
				{
					base.PowerSharpParameters["ModifySubjectValue"] = value;
				}
			}

			// Token: 0x17003AE7 RID: 15079
			// (set) Token: 0x06005D55 RID: 23893 RVA: 0x00090B58 File Offset: 0x0008ED58
			public virtual MultiValuedProperty<SmtpAddress> RedirectToRecipients
			{
				set
				{
					base.PowerSharpParameters["RedirectToRecipients"] = value;
				}
			}

			// Token: 0x17003AE8 RID: 15080
			// (set) Token: 0x06005D56 RID: 23894 RVA: 0x00090B6B File Offset: 0x0008ED6B
			public virtual MultiValuedProperty<SmtpAddress> TestModeBccToRecipients
			{
				set
				{
					base.PowerSharpParameters["TestModeBccToRecipients"] = value;
				}
			}

			// Token: 0x17003AE9 RID: 15081
			// (set) Token: 0x06005D57 RID: 23895 RVA: 0x00090B7E File Offset: 0x0008ED7E
			public virtual MultiValuedProperty<SmtpAddress> FalsePositiveAdditionalRecipients
			{
				set
				{
					base.PowerSharpParameters["FalsePositiveAdditionalRecipients"] = value;
				}
			}

			// Token: 0x17003AEA RID: 15082
			// (set) Token: 0x06005D58 RID: 23896 RVA: 0x00090B91 File Offset: 0x0008ED91
			public virtual SpamFilteringTestModeAction TestModeAction
			{
				set
				{
					base.PowerSharpParameters["TestModeAction"] = value;
				}
			}

			// Token: 0x17003AEB RID: 15083
			// (set) Token: 0x06005D59 RID: 23897 RVA: 0x00090BA9 File Offset: 0x0008EDA9
			public virtual string AdminDisplayName
			{
				set
				{
					base.PowerSharpParameters["AdminDisplayName"] = value;
				}
			}

			// Token: 0x17003AEC RID: 15084
			// (set) Token: 0x06005D5A RID: 23898 RVA: 0x00090BBC File Offset: 0x0008EDBC
			public virtual SpamFilteringAction HighConfidenceSpamAction
			{
				set
				{
					base.PowerSharpParameters["HighConfidenceSpamAction"] = value;
				}
			}

			// Token: 0x17003AED RID: 15085
			// (set) Token: 0x06005D5B RID: 23899 RVA: 0x00090BD4 File Offset: 0x0008EDD4
			public virtual SpamFilteringAction SpamAction
			{
				set
				{
					base.PowerSharpParameters["SpamAction"] = value;
				}
			}

			// Token: 0x17003AEE RID: 15086
			// (set) Token: 0x06005D5C RID: 23900 RVA: 0x00090BEC File Offset: 0x0008EDEC
			public virtual int QuarantineRetentionPeriod
			{
				set
				{
					base.PowerSharpParameters["QuarantineRetentionPeriod"] = value;
				}
			}

			// Token: 0x17003AEF RID: 15087
			// (set) Token: 0x06005D5D RID: 23901 RVA: 0x00090C04 File Offset: 0x0008EE04
			public virtual int EndUserSpamNotificationFrequency
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationFrequency"] = value;
				}
			}

			// Token: 0x17003AF0 RID: 15088
			// (set) Token: 0x06005D5E RID: 23902 RVA: 0x00090C1C File Offset: 0x0008EE1C
			public virtual MultiValuedProperty<string> LanguageBlockList
			{
				set
				{
					base.PowerSharpParameters["LanguageBlockList"] = value;
				}
			}

			// Token: 0x17003AF1 RID: 15089
			// (set) Token: 0x06005D5F RID: 23903 RVA: 0x00090C2F File Offset: 0x0008EE2F
			public virtual MultiValuedProperty<string> RegionBlockList
			{
				set
				{
					base.PowerSharpParameters["RegionBlockList"] = value;
				}
			}

			// Token: 0x17003AF2 RID: 15090
			// (set) Token: 0x06005D60 RID: 23904 RVA: 0x00090C42 File Offset: 0x0008EE42
			public virtual bool EnableEndUserSpamNotifications
			{
				set
				{
					base.PowerSharpParameters["EnableEndUserSpamNotifications"] = value;
				}
			}

			// Token: 0x17003AF3 RID: 15091
			// (set) Token: 0x06005D61 RID: 23905 RVA: 0x00090C5A File Offset: 0x0008EE5A
			public virtual bool DownloadLink
			{
				set
				{
					base.PowerSharpParameters["DownloadLink"] = value;
				}
			}

			// Token: 0x17003AF4 RID: 15092
			// (set) Token: 0x06005D62 RID: 23906 RVA: 0x00090C72 File Offset: 0x0008EE72
			public virtual bool EnableRegionBlockList
			{
				set
				{
					base.PowerSharpParameters["EnableRegionBlockList"] = value;
				}
			}

			// Token: 0x17003AF5 RID: 15093
			// (set) Token: 0x06005D63 RID: 23907 RVA: 0x00090C8A File Offset: 0x0008EE8A
			public virtual bool EnableLanguageBlockList
			{
				set
				{
					base.PowerSharpParameters["EnableLanguageBlockList"] = value;
				}
			}

			// Token: 0x17003AF6 RID: 15094
			// (set) Token: 0x06005D64 RID: 23908 RVA: 0x00090CA2 File Offset: 0x0008EEA2
			public virtual SmtpAddress EndUserSpamNotificationCustomFromAddress
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationCustomFromAddress"] = value;
				}
			}

			// Token: 0x17003AF7 RID: 15095
			// (set) Token: 0x06005D65 RID: 23909 RVA: 0x00090CBA File Offset: 0x0008EEBA
			public virtual string EndUserSpamNotificationCustomFromName
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationCustomFromName"] = value;
				}
			}

			// Token: 0x17003AF8 RID: 15096
			// (set) Token: 0x06005D66 RID: 23910 RVA: 0x00090CCD File Offset: 0x0008EECD
			public virtual string EndUserSpamNotificationCustomSubject
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationCustomSubject"] = value;
				}
			}

			// Token: 0x17003AF9 RID: 15097
			// (set) Token: 0x06005D67 RID: 23911 RVA: 0x00090CE0 File Offset: 0x0008EEE0
			public virtual EsnLanguage EndUserSpamNotificationLanguage
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationLanguage"] = value;
				}
			}

			// Token: 0x17003AFA RID: 15098
			// (set) Token: 0x06005D68 RID: 23912 RVA: 0x00090CF8 File Offset: 0x0008EEF8
			public virtual int EndUserSpamNotificationLimit
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationLimit"] = value;
				}
			}

			// Token: 0x17003AFB RID: 15099
			// (set) Token: 0x06005D69 RID: 23913 RVA: 0x00090D10 File Offset: 0x0008EF10
			public virtual SpamFilteringOption IncreaseScoreWithImageLinks
			{
				set
				{
					base.PowerSharpParameters["IncreaseScoreWithImageLinks"] = value;
				}
			}

			// Token: 0x17003AFC RID: 15100
			// (set) Token: 0x06005D6A RID: 23914 RVA: 0x00090D28 File Offset: 0x0008EF28
			public virtual SpamFilteringOption IncreaseScoreWithNumericIps
			{
				set
				{
					base.PowerSharpParameters["IncreaseScoreWithNumericIps"] = value;
				}
			}

			// Token: 0x17003AFD RID: 15101
			// (set) Token: 0x06005D6B RID: 23915 RVA: 0x00090D40 File Offset: 0x0008EF40
			public virtual SpamFilteringOption IncreaseScoreWithRedirectToOtherPort
			{
				set
				{
					base.PowerSharpParameters["IncreaseScoreWithRedirectToOtherPort"] = value;
				}
			}

			// Token: 0x17003AFE RID: 15102
			// (set) Token: 0x06005D6C RID: 23916 RVA: 0x00090D58 File Offset: 0x0008EF58
			public virtual SpamFilteringOption IncreaseScoreWithBizOrInfoUrls
			{
				set
				{
					base.PowerSharpParameters["IncreaseScoreWithBizOrInfoUrls"] = value;
				}
			}

			// Token: 0x17003AFF RID: 15103
			// (set) Token: 0x06005D6D RID: 23917 RVA: 0x00090D70 File Offset: 0x0008EF70
			public virtual SpamFilteringOption MarkAsSpamEmptyMessages
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamEmptyMessages"] = value;
				}
			}

			// Token: 0x17003B00 RID: 15104
			// (set) Token: 0x06005D6E RID: 23918 RVA: 0x00090D88 File Offset: 0x0008EF88
			public virtual SpamFilteringOption MarkAsSpamJavaScriptInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamJavaScriptInHtml"] = value;
				}
			}

			// Token: 0x17003B01 RID: 15105
			// (set) Token: 0x06005D6F RID: 23919 RVA: 0x00090DA0 File Offset: 0x0008EFA0
			public virtual SpamFilteringOption MarkAsSpamFramesInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamFramesInHtml"] = value;
				}
			}

			// Token: 0x17003B02 RID: 15106
			// (set) Token: 0x06005D70 RID: 23920 RVA: 0x00090DB8 File Offset: 0x0008EFB8
			public virtual SpamFilteringOption MarkAsSpamObjectTagsInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamObjectTagsInHtml"] = value;
				}
			}

			// Token: 0x17003B03 RID: 15107
			// (set) Token: 0x06005D71 RID: 23921 RVA: 0x00090DD0 File Offset: 0x0008EFD0
			public virtual SpamFilteringOption MarkAsSpamEmbedTagsInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamEmbedTagsInHtml"] = value;
				}
			}

			// Token: 0x17003B04 RID: 15108
			// (set) Token: 0x06005D72 RID: 23922 RVA: 0x00090DE8 File Offset: 0x0008EFE8
			public virtual SpamFilteringOption MarkAsSpamFormTagsInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamFormTagsInHtml"] = value;
				}
			}

			// Token: 0x17003B05 RID: 15109
			// (set) Token: 0x06005D73 RID: 23923 RVA: 0x00090E00 File Offset: 0x0008F000
			public virtual SpamFilteringOption MarkAsSpamWebBugsInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamWebBugsInHtml"] = value;
				}
			}

			// Token: 0x17003B06 RID: 15110
			// (set) Token: 0x06005D74 RID: 23924 RVA: 0x00090E18 File Offset: 0x0008F018
			public virtual SpamFilteringOption MarkAsSpamSensitiveWordList
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamSensitiveWordList"] = value;
				}
			}

			// Token: 0x17003B07 RID: 15111
			// (set) Token: 0x06005D75 RID: 23925 RVA: 0x00090E30 File Offset: 0x0008F030
			public virtual SpamFilteringOption MarkAsSpamSpfRecordHardFail
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamSpfRecordHardFail"] = value;
				}
			}

			// Token: 0x17003B08 RID: 15112
			// (set) Token: 0x06005D76 RID: 23926 RVA: 0x00090E48 File Offset: 0x0008F048
			public virtual SpamFilteringOption MarkAsSpamFromAddressAuthFail
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamFromAddressAuthFail"] = value;
				}
			}

			// Token: 0x17003B09 RID: 15113
			// (set) Token: 0x06005D77 RID: 23927 RVA: 0x00090E60 File Offset: 0x0008F060
			public virtual SpamFilteringOption MarkAsSpamNdrBackscatter
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamNdrBackscatter"] = value;
				}
			}

			// Token: 0x17003B0A RID: 15114
			// (set) Token: 0x06005D78 RID: 23928 RVA: 0x00090E78 File Offset: 0x0008F078
			public virtual SpamFilteringOption MarkAsSpamBulkMail
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamBulkMail"] = value;
				}
			}

			// Token: 0x17003B0B RID: 15115
			// (set) Token: 0x06005D79 RID: 23929 RVA: 0x00090E90 File Offset: 0x0008F090
			public virtual int BulkThreshold
			{
				set
				{
					base.PowerSharpParameters["BulkThreshold"] = value;
				}
			}

			// Token: 0x17003B0C RID: 15116
			// (set) Token: 0x06005D7A RID: 23930 RVA: 0x00090EA8 File Offset: 0x0008F0A8
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003B0D RID: 15117
			// (set) Token: 0x06005D7B RID: 23931 RVA: 0x00090EC6 File Offset: 0x0008F0C6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003B0E RID: 15118
			// (set) Token: 0x06005D7C RID: 23932 RVA: 0x00090ED9 File Offset: 0x0008F0D9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003B0F RID: 15119
			// (set) Token: 0x06005D7D RID: 23933 RVA: 0x00090EF1 File Offset: 0x0008F0F1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003B10 RID: 15120
			// (set) Token: 0x06005D7E RID: 23934 RVA: 0x00090F09 File Offset: 0x0008F109
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003B11 RID: 15121
			// (set) Token: 0x06005D7F RID: 23935 RVA: 0x00090F21 File Offset: 0x0008F121
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003B12 RID: 15122
			// (set) Token: 0x06005D80 RID: 23936 RVA: 0x00090F39 File Offset: 0x0008F139
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
