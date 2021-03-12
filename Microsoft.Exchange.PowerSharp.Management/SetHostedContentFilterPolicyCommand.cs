using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000717 RID: 1815
	public class SetHostedContentFilterPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<HostedContentFilterPolicy>
	{
		// Token: 0x06005D99 RID: 23961 RVA: 0x0009112D File Offset: 0x0008F32D
		private SetHostedContentFilterPolicyCommand() : base("Set-HostedContentFilterPolicy")
		{
		}

		// Token: 0x06005D9A RID: 23962 RVA: 0x0009113A File Offset: 0x0008F33A
		public SetHostedContentFilterPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005D9B RID: 23963 RVA: 0x00091149 File Offset: 0x0008F349
		public virtual SetHostedContentFilterPolicyCommand SetParameters(SetHostedContentFilterPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005D9C RID: 23964 RVA: 0x00091153 File Offset: 0x0008F353
		public virtual SetHostedContentFilterPolicyCommand SetParameters(SetHostedContentFilterPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000718 RID: 1816
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003B24 RID: 15140
			// (set) Token: 0x06005D9D RID: 23965 RVA: 0x0009115D File Offset: 0x0008F35D
			public virtual SwitchParameter MakeDefault
			{
				set
				{
					base.PowerSharpParameters["MakeDefault"] = value;
				}
			}

			// Token: 0x17003B25 RID: 15141
			// (set) Token: 0x06005D9E RID: 23966 RVA: 0x00091175 File Offset: 0x0008F375
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17003B26 RID: 15142
			// (set) Token: 0x06005D9F RID: 23967 RVA: 0x0009118D File Offset: 0x0008F38D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003B27 RID: 15143
			// (set) Token: 0x06005DA0 RID: 23968 RVA: 0x000911A0 File Offset: 0x0008F3A0
			public virtual string AdminDisplayName
			{
				set
				{
					base.PowerSharpParameters["AdminDisplayName"] = value;
				}
			}

			// Token: 0x17003B28 RID: 15144
			// (set) Token: 0x06005DA1 RID: 23969 RVA: 0x000911B3 File Offset: 0x0008F3B3
			public virtual string AddXHeaderValue
			{
				set
				{
					base.PowerSharpParameters["AddXHeaderValue"] = value;
				}
			}

			// Token: 0x17003B29 RID: 15145
			// (set) Token: 0x06005DA2 RID: 23970 RVA: 0x000911C6 File Offset: 0x0008F3C6
			public virtual string ModifySubjectValue
			{
				set
				{
					base.PowerSharpParameters["ModifySubjectValue"] = value;
				}
			}

			// Token: 0x17003B2A RID: 15146
			// (set) Token: 0x06005DA3 RID: 23971 RVA: 0x000911D9 File Offset: 0x0008F3D9
			public virtual MultiValuedProperty<SmtpAddress> RedirectToRecipients
			{
				set
				{
					base.PowerSharpParameters["RedirectToRecipients"] = value;
				}
			}

			// Token: 0x17003B2B RID: 15147
			// (set) Token: 0x06005DA4 RID: 23972 RVA: 0x000911EC File Offset: 0x0008F3EC
			public virtual MultiValuedProperty<SmtpAddress> TestModeBccToRecipients
			{
				set
				{
					base.PowerSharpParameters["TestModeBccToRecipients"] = value;
				}
			}

			// Token: 0x17003B2C RID: 15148
			// (set) Token: 0x06005DA5 RID: 23973 RVA: 0x000911FF File Offset: 0x0008F3FF
			public virtual MultiValuedProperty<SmtpAddress> FalsePositiveAdditionalRecipients
			{
				set
				{
					base.PowerSharpParameters["FalsePositiveAdditionalRecipients"] = value;
				}
			}

			// Token: 0x17003B2D RID: 15149
			// (set) Token: 0x06005DA6 RID: 23974 RVA: 0x00091212 File Offset: 0x0008F412
			public virtual int QuarantineRetentionPeriod
			{
				set
				{
					base.PowerSharpParameters["QuarantineRetentionPeriod"] = value;
				}
			}

			// Token: 0x17003B2E RID: 15150
			// (set) Token: 0x06005DA7 RID: 23975 RVA: 0x0009122A File Offset: 0x0008F42A
			public virtual int EndUserSpamNotificationFrequency
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationFrequency"] = value;
				}
			}

			// Token: 0x17003B2F RID: 15151
			// (set) Token: 0x06005DA8 RID: 23976 RVA: 0x00091242 File Offset: 0x0008F442
			public virtual SpamFilteringTestModeAction TestModeAction
			{
				set
				{
					base.PowerSharpParameters["TestModeAction"] = value;
				}
			}

			// Token: 0x17003B30 RID: 15152
			// (set) Token: 0x06005DA9 RID: 23977 RVA: 0x0009125A File Offset: 0x0008F45A
			public virtual SpamFilteringOption IncreaseScoreWithImageLinks
			{
				set
				{
					base.PowerSharpParameters["IncreaseScoreWithImageLinks"] = value;
				}
			}

			// Token: 0x17003B31 RID: 15153
			// (set) Token: 0x06005DAA RID: 23978 RVA: 0x00091272 File Offset: 0x0008F472
			public virtual SpamFilteringOption IncreaseScoreWithNumericIps
			{
				set
				{
					base.PowerSharpParameters["IncreaseScoreWithNumericIps"] = value;
				}
			}

			// Token: 0x17003B32 RID: 15154
			// (set) Token: 0x06005DAB RID: 23979 RVA: 0x0009128A File Offset: 0x0008F48A
			public virtual SpamFilteringOption IncreaseScoreWithRedirectToOtherPort
			{
				set
				{
					base.PowerSharpParameters["IncreaseScoreWithRedirectToOtherPort"] = value;
				}
			}

			// Token: 0x17003B33 RID: 15155
			// (set) Token: 0x06005DAC RID: 23980 RVA: 0x000912A2 File Offset: 0x0008F4A2
			public virtual SpamFilteringOption IncreaseScoreWithBizOrInfoUrls
			{
				set
				{
					base.PowerSharpParameters["IncreaseScoreWithBizOrInfoUrls"] = value;
				}
			}

			// Token: 0x17003B34 RID: 15156
			// (set) Token: 0x06005DAD RID: 23981 RVA: 0x000912BA File Offset: 0x0008F4BA
			public virtual SpamFilteringOption MarkAsSpamEmptyMessages
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamEmptyMessages"] = value;
				}
			}

			// Token: 0x17003B35 RID: 15157
			// (set) Token: 0x06005DAE RID: 23982 RVA: 0x000912D2 File Offset: 0x0008F4D2
			public virtual SpamFilteringOption MarkAsSpamJavaScriptInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamJavaScriptInHtml"] = value;
				}
			}

			// Token: 0x17003B36 RID: 15158
			// (set) Token: 0x06005DAF RID: 23983 RVA: 0x000912EA File Offset: 0x0008F4EA
			public virtual SpamFilteringOption MarkAsSpamFramesInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamFramesInHtml"] = value;
				}
			}

			// Token: 0x17003B37 RID: 15159
			// (set) Token: 0x06005DB0 RID: 23984 RVA: 0x00091302 File Offset: 0x0008F502
			public virtual SpamFilteringOption MarkAsSpamObjectTagsInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamObjectTagsInHtml"] = value;
				}
			}

			// Token: 0x17003B38 RID: 15160
			// (set) Token: 0x06005DB1 RID: 23985 RVA: 0x0009131A File Offset: 0x0008F51A
			public virtual SpamFilteringOption MarkAsSpamEmbedTagsInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamEmbedTagsInHtml"] = value;
				}
			}

			// Token: 0x17003B39 RID: 15161
			// (set) Token: 0x06005DB2 RID: 23986 RVA: 0x00091332 File Offset: 0x0008F532
			public virtual SpamFilteringOption MarkAsSpamFormTagsInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamFormTagsInHtml"] = value;
				}
			}

			// Token: 0x17003B3A RID: 15162
			// (set) Token: 0x06005DB3 RID: 23987 RVA: 0x0009134A File Offset: 0x0008F54A
			public virtual SpamFilteringOption MarkAsSpamWebBugsInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamWebBugsInHtml"] = value;
				}
			}

			// Token: 0x17003B3B RID: 15163
			// (set) Token: 0x06005DB4 RID: 23988 RVA: 0x00091362 File Offset: 0x0008F562
			public virtual SpamFilteringOption MarkAsSpamSensitiveWordList
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamSensitiveWordList"] = value;
				}
			}

			// Token: 0x17003B3C RID: 15164
			// (set) Token: 0x06005DB5 RID: 23989 RVA: 0x0009137A File Offset: 0x0008F57A
			public virtual SpamFilteringOption MarkAsSpamSpfRecordHardFail
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamSpfRecordHardFail"] = value;
				}
			}

			// Token: 0x17003B3D RID: 15165
			// (set) Token: 0x06005DB6 RID: 23990 RVA: 0x00091392 File Offset: 0x0008F592
			public virtual SpamFilteringOption MarkAsSpamFromAddressAuthFail
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamFromAddressAuthFail"] = value;
				}
			}

			// Token: 0x17003B3E RID: 15166
			// (set) Token: 0x06005DB7 RID: 23991 RVA: 0x000913AA File Offset: 0x0008F5AA
			public virtual SpamFilteringOption MarkAsSpamBulkMail
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamBulkMail"] = value;
				}
			}

			// Token: 0x17003B3F RID: 15167
			// (set) Token: 0x06005DB8 RID: 23992 RVA: 0x000913C2 File Offset: 0x0008F5C2
			public virtual SpamFilteringOption MarkAsSpamNdrBackscatter
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamNdrBackscatter"] = value;
				}
			}

			// Token: 0x17003B40 RID: 15168
			// (set) Token: 0x06005DB9 RID: 23993 RVA: 0x000913DA File Offset: 0x0008F5DA
			public virtual MultiValuedProperty<string> LanguageBlockList
			{
				set
				{
					base.PowerSharpParameters["LanguageBlockList"] = value;
				}
			}

			// Token: 0x17003B41 RID: 15169
			// (set) Token: 0x06005DBA RID: 23994 RVA: 0x000913ED File Offset: 0x0008F5ED
			public virtual MultiValuedProperty<string> RegionBlockList
			{
				set
				{
					base.PowerSharpParameters["RegionBlockList"] = value;
				}
			}

			// Token: 0x17003B42 RID: 15170
			// (set) Token: 0x06005DBB RID: 23995 RVA: 0x00091400 File Offset: 0x0008F600
			public virtual SpamFilteringAction HighConfidenceSpamAction
			{
				set
				{
					base.PowerSharpParameters["HighConfidenceSpamAction"] = value;
				}
			}

			// Token: 0x17003B43 RID: 15171
			// (set) Token: 0x06005DBC RID: 23996 RVA: 0x00091418 File Offset: 0x0008F618
			public virtual SpamFilteringAction SpamAction
			{
				set
				{
					base.PowerSharpParameters["SpamAction"] = value;
				}
			}

			// Token: 0x17003B44 RID: 15172
			// (set) Token: 0x06005DBD RID: 23997 RVA: 0x00091430 File Offset: 0x0008F630
			public virtual bool EnableEndUserSpamNotifications
			{
				set
				{
					base.PowerSharpParameters["EnableEndUserSpamNotifications"] = value;
				}
			}

			// Token: 0x17003B45 RID: 15173
			// (set) Token: 0x06005DBE RID: 23998 RVA: 0x00091448 File Offset: 0x0008F648
			public virtual bool DownloadLink
			{
				set
				{
					base.PowerSharpParameters["DownloadLink"] = value;
				}
			}

			// Token: 0x17003B46 RID: 15174
			// (set) Token: 0x06005DBF RID: 23999 RVA: 0x00091460 File Offset: 0x0008F660
			public virtual bool EnableRegionBlockList
			{
				set
				{
					base.PowerSharpParameters["EnableRegionBlockList"] = value;
				}
			}

			// Token: 0x17003B47 RID: 15175
			// (set) Token: 0x06005DC0 RID: 24000 RVA: 0x00091478 File Offset: 0x0008F678
			public virtual bool EnableLanguageBlockList
			{
				set
				{
					base.PowerSharpParameters["EnableLanguageBlockList"] = value;
				}
			}

			// Token: 0x17003B48 RID: 15176
			// (set) Token: 0x06005DC1 RID: 24001 RVA: 0x00091490 File Offset: 0x0008F690
			public virtual SmtpAddress EndUserSpamNotificationCustomFromAddress
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationCustomFromAddress"] = value;
				}
			}

			// Token: 0x17003B49 RID: 15177
			// (set) Token: 0x06005DC2 RID: 24002 RVA: 0x000914A8 File Offset: 0x0008F6A8
			public virtual string EndUserSpamNotificationCustomFromName
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationCustomFromName"] = value;
				}
			}

			// Token: 0x17003B4A RID: 15178
			// (set) Token: 0x06005DC3 RID: 24003 RVA: 0x000914BB File Offset: 0x0008F6BB
			public virtual string EndUserSpamNotificationCustomSubject
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationCustomSubject"] = value;
				}
			}

			// Token: 0x17003B4B RID: 15179
			// (set) Token: 0x06005DC4 RID: 24004 RVA: 0x000914CE File Offset: 0x0008F6CE
			public virtual EsnLanguage EndUserSpamNotificationLanguage
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationLanguage"] = value;
				}
			}

			// Token: 0x17003B4C RID: 15180
			// (set) Token: 0x06005DC5 RID: 24005 RVA: 0x000914E6 File Offset: 0x0008F6E6
			public virtual int EndUserSpamNotificationLimit
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationLimit"] = value;
				}
			}

			// Token: 0x17003B4D RID: 15181
			// (set) Token: 0x06005DC6 RID: 24006 RVA: 0x000914FE File Offset: 0x0008F6FE
			public virtual int BulkThreshold
			{
				set
				{
					base.PowerSharpParameters["BulkThreshold"] = value;
				}
			}

			// Token: 0x17003B4E RID: 15182
			// (set) Token: 0x06005DC7 RID: 24007 RVA: 0x00091516 File Offset: 0x0008F716
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003B4F RID: 15183
			// (set) Token: 0x06005DC8 RID: 24008 RVA: 0x00091529 File Offset: 0x0008F729
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003B50 RID: 15184
			// (set) Token: 0x06005DC9 RID: 24009 RVA: 0x00091541 File Offset: 0x0008F741
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003B51 RID: 15185
			// (set) Token: 0x06005DCA RID: 24010 RVA: 0x00091559 File Offset: 0x0008F759
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003B52 RID: 15186
			// (set) Token: 0x06005DCB RID: 24011 RVA: 0x00091571 File Offset: 0x0008F771
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003B53 RID: 15187
			// (set) Token: 0x06005DCC RID: 24012 RVA: 0x00091589 File Offset: 0x0008F789
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000719 RID: 1817
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003B54 RID: 15188
			// (set) Token: 0x06005DCE RID: 24014 RVA: 0x000915A9 File Offset: 0x0008F7A9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new HostedContentFilterPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17003B55 RID: 15189
			// (set) Token: 0x06005DCF RID: 24015 RVA: 0x000915C7 File Offset: 0x0008F7C7
			public virtual SwitchParameter MakeDefault
			{
				set
				{
					base.PowerSharpParameters["MakeDefault"] = value;
				}
			}

			// Token: 0x17003B56 RID: 15190
			// (set) Token: 0x06005DD0 RID: 24016 RVA: 0x000915DF File Offset: 0x0008F7DF
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17003B57 RID: 15191
			// (set) Token: 0x06005DD1 RID: 24017 RVA: 0x000915F7 File Offset: 0x0008F7F7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003B58 RID: 15192
			// (set) Token: 0x06005DD2 RID: 24018 RVA: 0x0009160A File Offset: 0x0008F80A
			public virtual string AdminDisplayName
			{
				set
				{
					base.PowerSharpParameters["AdminDisplayName"] = value;
				}
			}

			// Token: 0x17003B59 RID: 15193
			// (set) Token: 0x06005DD3 RID: 24019 RVA: 0x0009161D File Offset: 0x0008F81D
			public virtual string AddXHeaderValue
			{
				set
				{
					base.PowerSharpParameters["AddXHeaderValue"] = value;
				}
			}

			// Token: 0x17003B5A RID: 15194
			// (set) Token: 0x06005DD4 RID: 24020 RVA: 0x00091630 File Offset: 0x0008F830
			public virtual string ModifySubjectValue
			{
				set
				{
					base.PowerSharpParameters["ModifySubjectValue"] = value;
				}
			}

			// Token: 0x17003B5B RID: 15195
			// (set) Token: 0x06005DD5 RID: 24021 RVA: 0x00091643 File Offset: 0x0008F843
			public virtual MultiValuedProperty<SmtpAddress> RedirectToRecipients
			{
				set
				{
					base.PowerSharpParameters["RedirectToRecipients"] = value;
				}
			}

			// Token: 0x17003B5C RID: 15196
			// (set) Token: 0x06005DD6 RID: 24022 RVA: 0x00091656 File Offset: 0x0008F856
			public virtual MultiValuedProperty<SmtpAddress> TestModeBccToRecipients
			{
				set
				{
					base.PowerSharpParameters["TestModeBccToRecipients"] = value;
				}
			}

			// Token: 0x17003B5D RID: 15197
			// (set) Token: 0x06005DD7 RID: 24023 RVA: 0x00091669 File Offset: 0x0008F869
			public virtual MultiValuedProperty<SmtpAddress> FalsePositiveAdditionalRecipients
			{
				set
				{
					base.PowerSharpParameters["FalsePositiveAdditionalRecipients"] = value;
				}
			}

			// Token: 0x17003B5E RID: 15198
			// (set) Token: 0x06005DD8 RID: 24024 RVA: 0x0009167C File Offset: 0x0008F87C
			public virtual int QuarantineRetentionPeriod
			{
				set
				{
					base.PowerSharpParameters["QuarantineRetentionPeriod"] = value;
				}
			}

			// Token: 0x17003B5F RID: 15199
			// (set) Token: 0x06005DD9 RID: 24025 RVA: 0x00091694 File Offset: 0x0008F894
			public virtual int EndUserSpamNotificationFrequency
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationFrequency"] = value;
				}
			}

			// Token: 0x17003B60 RID: 15200
			// (set) Token: 0x06005DDA RID: 24026 RVA: 0x000916AC File Offset: 0x0008F8AC
			public virtual SpamFilteringTestModeAction TestModeAction
			{
				set
				{
					base.PowerSharpParameters["TestModeAction"] = value;
				}
			}

			// Token: 0x17003B61 RID: 15201
			// (set) Token: 0x06005DDB RID: 24027 RVA: 0x000916C4 File Offset: 0x0008F8C4
			public virtual SpamFilteringOption IncreaseScoreWithImageLinks
			{
				set
				{
					base.PowerSharpParameters["IncreaseScoreWithImageLinks"] = value;
				}
			}

			// Token: 0x17003B62 RID: 15202
			// (set) Token: 0x06005DDC RID: 24028 RVA: 0x000916DC File Offset: 0x0008F8DC
			public virtual SpamFilteringOption IncreaseScoreWithNumericIps
			{
				set
				{
					base.PowerSharpParameters["IncreaseScoreWithNumericIps"] = value;
				}
			}

			// Token: 0x17003B63 RID: 15203
			// (set) Token: 0x06005DDD RID: 24029 RVA: 0x000916F4 File Offset: 0x0008F8F4
			public virtual SpamFilteringOption IncreaseScoreWithRedirectToOtherPort
			{
				set
				{
					base.PowerSharpParameters["IncreaseScoreWithRedirectToOtherPort"] = value;
				}
			}

			// Token: 0x17003B64 RID: 15204
			// (set) Token: 0x06005DDE RID: 24030 RVA: 0x0009170C File Offset: 0x0008F90C
			public virtual SpamFilteringOption IncreaseScoreWithBizOrInfoUrls
			{
				set
				{
					base.PowerSharpParameters["IncreaseScoreWithBizOrInfoUrls"] = value;
				}
			}

			// Token: 0x17003B65 RID: 15205
			// (set) Token: 0x06005DDF RID: 24031 RVA: 0x00091724 File Offset: 0x0008F924
			public virtual SpamFilteringOption MarkAsSpamEmptyMessages
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamEmptyMessages"] = value;
				}
			}

			// Token: 0x17003B66 RID: 15206
			// (set) Token: 0x06005DE0 RID: 24032 RVA: 0x0009173C File Offset: 0x0008F93C
			public virtual SpamFilteringOption MarkAsSpamJavaScriptInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamJavaScriptInHtml"] = value;
				}
			}

			// Token: 0x17003B67 RID: 15207
			// (set) Token: 0x06005DE1 RID: 24033 RVA: 0x00091754 File Offset: 0x0008F954
			public virtual SpamFilteringOption MarkAsSpamFramesInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamFramesInHtml"] = value;
				}
			}

			// Token: 0x17003B68 RID: 15208
			// (set) Token: 0x06005DE2 RID: 24034 RVA: 0x0009176C File Offset: 0x0008F96C
			public virtual SpamFilteringOption MarkAsSpamObjectTagsInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamObjectTagsInHtml"] = value;
				}
			}

			// Token: 0x17003B69 RID: 15209
			// (set) Token: 0x06005DE3 RID: 24035 RVA: 0x00091784 File Offset: 0x0008F984
			public virtual SpamFilteringOption MarkAsSpamEmbedTagsInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamEmbedTagsInHtml"] = value;
				}
			}

			// Token: 0x17003B6A RID: 15210
			// (set) Token: 0x06005DE4 RID: 24036 RVA: 0x0009179C File Offset: 0x0008F99C
			public virtual SpamFilteringOption MarkAsSpamFormTagsInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamFormTagsInHtml"] = value;
				}
			}

			// Token: 0x17003B6B RID: 15211
			// (set) Token: 0x06005DE5 RID: 24037 RVA: 0x000917B4 File Offset: 0x0008F9B4
			public virtual SpamFilteringOption MarkAsSpamWebBugsInHtml
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamWebBugsInHtml"] = value;
				}
			}

			// Token: 0x17003B6C RID: 15212
			// (set) Token: 0x06005DE6 RID: 24038 RVA: 0x000917CC File Offset: 0x0008F9CC
			public virtual SpamFilteringOption MarkAsSpamSensitiveWordList
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamSensitiveWordList"] = value;
				}
			}

			// Token: 0x17003B6D RID: 15213
			// (set) Token: 0x06005DE7 RID: 24039 RVA: 0x000917E4 File Offset: 0x0008F9E4
			public virtual SpamFilteringOption MarkAsSpamSpfRecordHardFail
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamSpfRecordHardFail"] = value;
				}
			}

			// Token: 0x17003B6E RID: 15214
			// (set) Token: 0x06005DE8 RID: 24040 RVA: 0x000917FC File Offset: 0x0008F9FC
			public virtual SpamFilteringOption MarkAsSpamFromAddressAuthFail
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamFromAddressAuthFail"] = value;
				}
			}

			// Token: 0x17003B6F RID: 15215
			// (set) Token: 0x06005DE9 RID: 24041 RVA: 0x00091814 File Offset: 0x0008FA14
			public virtual SpamFilteringOption MarkAsSpamBulkMail
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamBulkMail"] = value;
				}
			}

			// Token: 0x17003B70 RID: 15216
			// (set) Token: 0x06005DEA RID: 24042 RVA: 0x0009182C File Offset: 0x0008FA2C
			public virtual SpamFilteringOption MarkAsSpamNdrBackscatter
			{
				set
				{
					base.PowerSharpParameters["MarkAsSpamNdrBackscatter"] = value;
				}
			}

			// Token: 0x17003B71 RID: 15217
			// (set) Token: 0x06005DEB RID: 24043 RVA: 0x00091844 File Offset: 0x0008FA44
			public virtual MultiValuedProperty<string> LanguageBlockList
			{
				set
				{
					base.PowerSharpParameters["LanguageBlockList"] = value;
				}
			}

			// Token: 0x17003B72 RID: 15218
			// (set) Token: 0x06005DEC RID: 24044 RVA: 0x00091857 File Offset: 0x0008FA57
			public virtual MultiValuedProperty<string> RegionBlockList
			{
				set
				{
					base.PowerSharpParameters["RegionBlockList"] = value;
				}
			}

			// Token: 0x17003B73 RID: 15219
			// (set) Token: 0x06005DED RID: 24045 RVA: 0x0009186A File Offset: 0x0008FA6A
			public virtual SpamFilteringAction HighConfidenceSpamAction
			{
				set
				{
					base.PowerSharpParameters["HighConfidenceSpamAction"] = value;
				}
			}

			// Token: 0x17003B74 RID: 15220
			// (set) Token: 0x06005DEE RID: 24046 RVA: 0x00091882 File Offset: 0x0008FA82
			public virtual SpamFilteringAction SpamAction
			{
				set
				{
					base.PowerSharpParameters["SpamAction"] = value;
				}
			}

			// Token: 0x17003B75 RID: 15221
			// (set) Token: 0x06005DEF RID: 24047 RVA: 0x0009189A File Offset: 0x0008FA9A
			public virtual bool EnableEndUserSpamNotifications
			{
				set
				{
					base.PowerSharpParameters["EnableEndUserSpamNotifications"] = value;
				}
			}

			// Token: 0x17003B76 RID: 15222
			// (set) Token: 0x06005DF0 RID: 24048 RVA: 0x000918B2 File Offset: 0x0008FAB2
			public virtual bool DownloadLink
			{
				set
				{
					base.PowerSharpParameters["DownloadLink"] = value;
				}
			}

			// Token: 0x17003B77 RID: 15223
			// (set) Token: 0x06005DF1 RID: 24049 RVA: 0x000918CA File Offset: 0x0008FACA
			public virtual bool EnableRegionBlockList
			{
				set
				{
					base.PowerSharpParameters["EnableRegionBlockList"] = value;
				}
			}

			// Token: 0x17003B78 RID: 15224
			// (set) Token: 0x06005DF2 RID: 24050 RVA: 0x000918E2 File Offset: 0x0008FAE2
			public virtual bool EnableLanguageBlockList
			{
				set
				{
					base.PowerSharpParameters["EnableLanguageBlockList"] = value;
				}
			}

			// Token: 0x17003B79 RID: 15225
			// (set) Token: 0x06005DF3 RID: 24051 RVA: 0x000918FA File Offset: 0x0008FAFA
			public virtual SmtpAddress EndUserSpamNotificationCustomFromAddress
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationCustomFromAddress"] = value;
				}
			}

			// Token: 0x17003B7A RID: 15226
			// (set) Token: 0x06005DF4 RID: 24052 RVA: 0x00091912 File Offset: 0x0008FB12
			public virtual string EndUserSpamNotificationCustomFromName
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationCustomFromName"] = value;
				}
			}

			// Token: 0x17003B7B RID: 15227
			// (set) Token: 0x06005DF5 RID: 24053 RVA: 0x00091925 File Offset: 0x0008FB25
			public virtual string EndUserSpamNotificationCustomSubject
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationCustomSubject"] = value;
				}
			}

			// Token: 0x17003B7C RID: 15228
			// (set) Token: 0x06005DF6 RID: 24054 RVA: 0x00091938 File Offset: 0x0008FB38
			public virtual EsnLanguage EndUserSpamNotificationLanguage
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationLanguage"] = value;
				}
			}

			// Token: 0x17003B7D RID: 15229
			// (set) Token: 0x06005DF7 RID: 24055 RVA: 0x00091950 File Offset: 0x0008FB50
			public virtual int EndUserSpamNotificationLimit
			{
				set
				{
					base.PowerSharpParameters["EndUserSpamNotificationLimit"] = value;
				}
			}

			// Token: 0x17003B7E RID: 15230
			// (set) Token: 0x06005DF8 RID: 24056 RVA: 0x00091968 File Offset: 0x0008FB68
			public virtual int BulkThreshold
			{
				set
				{
					base.PowerSharpParameters["BulkThreshold"] = value;
				}
			}

			// Token: 0x17003B7F RID: 15231
			// (set) Token: 0x06005DF9 RID: 24057 RVA: 0x00091980 File Offset: 0x0008FB80
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003B80 RID: 15232
			// (set) Token: 0x06005DFA RID: 24058 RVA: 0x00091993 File Offset: 0x0008FB93
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003B81 RID: 15233
			// (set) Token: 0x06005DFB RID: 24059 RVA: 0x000919AB File Offset: 0x0008FBAB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003B82 RID: 15234
			// (set) Token: 0x06005DFC RID: 24060 RVA: 0x000919C3 File Offset: 0x0008FBC3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003B83 RID: 15235
			// (set) Token: 0x06005DFD RID: 24061 RVA: 0x000919DB File Offset: 0x0008FBDB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003B84 RID: 15236
			// (set) Token: 0x06005DFE RID: 24062 RVA: 0x000919F3 File Offset: 0x0008FBF3
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
