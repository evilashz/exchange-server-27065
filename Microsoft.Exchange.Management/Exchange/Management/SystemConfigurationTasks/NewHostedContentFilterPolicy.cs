using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A41 RID: 2625
	[Cmdlet("New", "HostedContentFilterPolicy", SupportsShouldProcess = true)]
	public sealed class NewHostedContentFilterPolicy : NewMultitenancySystemConfigurationObjectTask<HostedContentFilterPolicy>
	{
		// Token: 0x17001C27 RID: 7207
		// (get) Token: 0x06005DB6 RID: 23990 RVA: 0x0018A20E File Offset: 0x0018840E
		// (set) Token: 0x06005DB7 RID: 23991 RVA: 0x0018A216 File Offset: 0x00188416
		[Parameter]
		public override SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17001C28 RID: 7208
		// (get) Token: 0x06005DB8 RID: 23992 RVA: 0x0018A21F File Offset: 0x0018841F
		// (set) Token: 0x06005DB9 RID: 23993 RVA: 0x0018A227 File Offset: 0x00188427
		[Parameter(Mandatory = true, Position = 0)]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x17001C29 RID: 7209
		// (get) Token: 0x06005DBA RID: 23994 RVA: 0x0018A230 File Offset: 0x00188430
		// (set) Token: 0x06005DBB RID: 23995 RVA: 0x0018A23D File Offset: 0x0018843D
		[Parameter]
		public string AddXHeaderValue
		{
			get
			{
				return this.DataObject.AddXHeaderValue;
			}
			set
			{
				this.DataObject.AddXHeaderValue = value;
			}
		}

		// Token: 0x17001C2A RID: 7210
		// (get) Token: 0x06005DBC RID: 23996 RVA: 0x0018A24B File Offset: 0x0018844B
		// (set) Token: 0x06005DBD RID: 23997 RVA: 0x0018A258 File Offset: 0x00188458
		[Parameter]
		public string ModifySubjectValue
		{
			get
			{
				return this.DataObject.ModifySubjectValue;
			}
			set
			{
				this.DataObject.ModifySubjectValue = value;
			}
		}

		// Token: 0x17001C2B RID: 7211
		// (get) Token: 0x06005DBE RID: 23998 RVA: 0x0018A266 File Offset: 0x00188466
		// (set) Token: 0x06005DBF RID: 23999 RVA: 0x0018A273 File Offset: 0x00188473
		[Parameter]
		public MultiValuedProperty<SmtpAddress> RedirectToRecipients
		{
			get
			{
				return this.DataObject.RedirectToRecipients;
			}
			set
			{
				this.DataObject.RedirectToRecipients = value;
			}
		}

		// Token: 0x17001C2C RID: 7212
		// (get) Token: 0x06005DC0 RID: 24000 RVA: 0x0018A281 File Offset: 0x00188481
		// (set) Token: 0x06005DC1 RID: 24001 RVA: 0x0018A28E File Offset: 0x0018848E
		[Parameter]
		public MultiValuedProperty<SmtpAddress> TestModeBccToRecipients
		{
			get
			{
				return this.DataObject.TestModeBccToRecipients;
			}
			set
			{
				this.DataObject.TestModeBccToRecipients = value;
			}
		}

		// Token: 0x17001C2D RID: 7213
		// (get) Token: 0x06005DC2 RID: 24002 RVA: 0x0018A29C File Offset: 0x0018849C
		// (set) Token: 0x06005DC3 RID: 24003 RVA: 0x0018A2A9 File Offset: 0x001884A9
		[Parameter]
		public MultiValuedProperty<SmtpAddress> FalsePositiveAdditionalRecipients
		{
			get
			{
				return this.DataObject.FalsePositiveAdditionalRecipients;
			}
			set
			{
				this.DataObject.FalsePositiveAdditionalRecipients = value;
			}
		}

		// Token: 0x17001C2E RID: 7214
		// (get) Token: 0x06005DC4 RID: 24004 RVA: 0x0018A2B7 File Offset: 0x001884B7
		// (set) Token: 0x06005DC5 RID: 24005 RVA: 0x0018A2C4 File Offset: 0x001884C4
		[Parameter]
		public SpamFilteringTestModeAction TestModeAction
		{
			get
			{
				return this.DataObject.TestModeAction;
			}
			set
			{
				this.DataObject.TestModeAction = value;
			}
		}

		// Token: 0x17001C2F RID: 7215
		// (get) Token: 0x06005DC6 RID: 24006 RVA: 0x0018A2D2 File Offset: 0x001884D2
		// (set) Token: 0x06005DC7 RID: 24007 RVA: 0x0018A2DF File Offset: 0x001884DF
		[Parameter]
		public string AdminDisplayName
		{
			get
			{
				return this.DataObject.AdminDisplayName;
			}
			set
			{
				this.DataObject.AdminDisplayName = value;
			}
		}

		// Token: 0x17001C30 RID: 7216
		// (get) Token: 0x06005DC8 RID: 24008 RVA: 0x0018A2ED File Offset: 0x001884ED
		// (set) Token: 0x06005DC9 RID: 24009 RVA: 0x0018A2FA File Offset: 0x001884FA
		[Parameter]
		public SpamFilteringAction HighConfidenceSpamAction
		{
			get
			{
				return this.DataObject.HighConfidenceSpamAction;
			}
			set
			{
				this.DataObject.HighConfidenceSpamAction = value;
			}
		}

		// Token: 0x17001C31 RID: 7217
		// (get) Token: 0x06005DCA RID: 24010 RVA: 0x0018A308 File Offset: 0x00188508
		// (set) Token: 0x06005DCB RID: 24011 RVA: 0x0018A315 File Offset: 0x00188515
		[Parameter]
		public SpamFilteringAction SpamAction
		{
			get
			{
				return this.DataObject.SpamAction;
			}
			set
			{
				this.DataObject.SpamAction = value;
			}
		}

		// Token: 0x17001C32 RID: 7218
		// (get) Token: 0x06005DCC RID: 24012 RVA: 0x0018A323 File Offset: 0x00188523
		// (set) Token: 0x06005DCD RID: 24013 RVA: 0x0018A330 File Offset: 0x00188530
		[Parameter]
		public int QuarantineRetentionPeriod
		{
			get
			{
				return this.DataObject.QuarantineRetentionPeriod;
			}
			set
			{
				this.DataObject.QuarantineRetentionPeriod = value;
			}
		}

		// Token: 0x17001C33 RID: 7219
		// (get) Token: 0x06005DCE RID: 24014 RVA: 0x0018A33E File Offset: 0x0018853E
		// (set) Token: 0x06005DCF RID: 24015 RVA: 0x0018A34B File Offset: 0x0018854B
		[Parameter]
		public int EndUserSpamNotificationFrequency
		{
			get
			{
				return this.DataObject.EndUserSpamNotificationFrequency;
			}
			set
			{
				this.DataObject.EndUserSpamNotificationFrequency = value;
			}
		}

		// Token: 0x17001C34 RID: 7220
		// (get) Token: 0x06005DD0 RID: 24016 RVA: 0x0018A359 File Offset: 0x00188559
		// (set) Token: 0x06005DD1 RID: 24017 RVA: 0x0018A366 File Offset: 0x00188566
		[Parameter]
		public MultiValuedProperty<string> LanguageBlockList
		{
			get
			{
				return this.DataObject.LanguageBlockList;
			}
			set
			{
				this.DataObject.LanguageBlockList = value;
			}
		}

		// Token: 0x17001C35 RID: 7221
		// (get) Token: 0x06005DD2 RID: 24018 RVA: 0x0018A374 File Offset: 0x00188574
		// (set) Token: 0x06005DD3 RID: 24019 RVA: 0x0018A381 File Offset: 0x00188581
		[Parameter]
		public MultiValuedProperty<string> RegionBlockList
		{
			get
			{
				return this.DataObject.RegionBlockList;
			}
			set
			{
				this.DataObject.RegionBlockList = value;
			}
		}

		// Token: 0x17001C36 RID: 7222
		// (get) Token: 0x06005DD4 RID: 24020 RVA: 0x0018A38F File Offset: 0x0018858F
		// (set) Token: 0x06005DD5 RID: 24021 RVA: 0x0018A39C File Offset: 0x0018859C
		[Parameter]
		public bool EnableEndUserSpamNotifications
		{
			get
			{
				return this.DataObject.EnableEndUserSpamNotifications;
			}
			set
			{
				this.DataObject.EnableEndUserSpamNotifications = value;
			}
		}

		// Token: 0x17001C37 RID: 7223
		// (get) Token: 0x06005DD6 RID: 24022 RVA: 0x0018A3AA File Offset: 0x001885AA
		// (set) Token: 0x06005DD7 RID: 24023 RVA: 0x0018A3B7 File Offset: 0x001885B7
		[Parameter]
		public bool DownloadLink
		{
			get
			{
				return this.DataObject.DownloadLink;
			}
			set
			{
				this.DataObject.DownloadLink = value;
			}
		}

		// Token: 0x17001C38 RID: 7224
		// (get) Token: 0x06005DD8 RID: 24024 RVA: 0x0018A3C5 File Offset: 0x001885C5
		// (set) Token: 0x06005DD9 RID: 24025 RVA: 0x0018A3D2 File Offset: 0x001885D2
		[Parameter]
		public bool EnableRegionBlockList
		{
			get
			{
				return this.DataObject.EnableRegionBlockList;
			}
			set
			{
				this.DataObject.EnableRegionBlockList = value;
			}
		}

		// Token: 0x17001C39 RID: 7225
		// (get) Token: 0x06005DDA RID: 24026 RVA: 0x0018A3E0 File Offset: 0x001885E0
		// (set) Token: 0x06005DDB RID: 24027 RVA: 0x0018A3ED File Offset: 0x001885ED
		[Parameter]
		public bool EnableLanguageBlockList
		{
			get
			{
				return this.DataObject.EnableLanguageBlockList;
			}
			set
			{
				this.DataObject.EnableLanguageBlockList = value;
			}
		}

		// Token: 0x17001C3A RID: 7226
		// (get) Token: 0x06005DDC RID: 24028 RVA: 0x0018A3FB File Offset: 0x001885FB
		// (set) Token: 0x06005DDD RID: 24029 RVA: 0x0018A408 File Offset: 0x00188608
		[Parameter]
		public SmtpAddress EndUserSpamNotificationCustomFromAddress
		{
			get
			{
				return this.DataObject.EndUserSpamNotificationCustomFromAddress;
			}
			set
			{
				this.DataObject.EndUserSpamNotificationCustomFromAddress = value;
			}
		}

		// Token: 0x17001C3B RID: 7227
		// (get) Token: 0x06005DDE RID: 24030 RVA: 0x0018A416 File Offset: 0x00188616
		// (set) Token: 0x06005DDF RID: 24031 RVA: 0x0018A423 File Offset: 0x00188623
		[Parameter]
		public string EndUserSpamNotificationCustomFromName
		{
			get
			{
				return this.DataObject.EndUserSpamNotificationCustomFromName;
			}
			set
			{
				this.DataObject.EndUserSpamNotificationCustomFromName = value;
			}
		}

		// Token: 0x17001C3C RID: 7228
		// (get) Token: 0x06005DE0 RID: 24032 RVA: 0x0018A431 File Offset: 0x00188631
		// (set) Token: 0x06005DE1 RID: 24033 RVA: 0x0018A43E File Offset: 0x0018863E
		[Parameter]
		public string EndUserSpamNotificationCustomSubject
		{
			get
			{
				return this.DataObject.EndUserSpamNotificationCustomSubject;
			}
			set
			{
				this.DataObject.EndUserSpamNotificationCustomSubject = value;
			}
		}

		// Token: 0x17001C3D RID: 7229
		// (get) Token: 0x06005DE2 RID: 24034 RVA: 0x0018A44C File Offset: 0x0018864C
		// (set) Token: 0x06005DE3 RID: 24035 RVA: 0x0018A459 File Offset: 0x00188659
		[Parameter]
		public EsnLanguage EndUserSpamNotificationLanguage
		{
			get
			{
				return this.DataObject.EndUserSpamNotificationLanguage;
			}
			set
			{
				this.DataObject.EndUserSpamNotificationLanguage = value;
			}
		}

		// Token: 0x17001C3E RID: 7230
		// (get) Token: 0x06005DE4 RID: 24036 RVA: 0x0018A467 File Offset: 0x00188667
		// (set) Token: 0x06005DE5 RID: 24037 RVA: 0x0018A474 File Offset: 0x00188674
		[Parameter]
		public int EndUserSpamNotificationLimit
		{
			get
			{
				return this.DataObject.EndUserSpamNotificationLimit;
			}
			set
			{
				this.DataObject.EndUserSpamNotificationLimit = value;
			}
		}

		// Token: 0x17001C3F RID: 7231
		// (get) Token: 0x06005DE6 RID: 24038 RVA: 0x0018A482 File Offset: 0x00188682
		// (set) Token: 0x06005DE7 RID: 24039 RVA: 0x0018A48F File Offset: 0x0018868F
		[Parameter]
		public SpamFilteringOption IncreaseScoreWithImageLinks
		{
			get
			{
				return this.DataObject.IncreaseScoreWithImageLinks;
			}
			set
			{
				this.DataObject.IncreaseScoreWithImageLinks = value;
			}
		}

		// Token: 0x17001C40 RID: 7232
		// (get) Token: 0x06005DE8 RID: 24040 RVA: 0x0018A49D File Offset: 0x0018869D
		// (set) Token: 0x06005DE9 RID: 24041 RVA: 0x0018A4AA File Offset: 0x001886AA
		[Parameter]
		public SpamFilteringOption IncreaseScoreWithNumericIps
		{
			get
			{
				return this.DataObject.IncreaseScoreWithNumericIps;
			}
			set
			{
				this.DataObject.IncreaseScoreWithNumericIps = value;
			}
		}

		// Token: 0x17001C41 RID: 7233
		// (get) Token: 0x06005DEA RID: 24042 RVA: 0x0018A4B8 File Offset: 0x001886B8
		// (set) Token: 0x06005DEB RID: 24043 RVA: 0x0018A4C5 File Offset: 0x001886C5
		[Parameter]
		public SpamFilteringOption IncreaseScoreWithRedirectToOtherPort
		{
			get
			{
				return this.DataObject.IncreaseScoreWithRedirectToOtherPort;
			}
			set
			{
				this.DataObject.IncreaseScoreWithRedirectToOtherPort = value;
			}
		}

		// Token: 0x17001C42 RID: 7234
		// (get) Token: 0x06005DEC RID: 24044 RVA: 0x0018A4D3 File Offset: 0x001886D3
		// (set) Token: 0x06005DED RID: 24045 RVA: 0x0018A4E0 File Offset: 0x001886E0
		[Parameter]
		public SpamFilteringOption IncreaseScoreWithBizOrInfoUrls
		{
			get
			{
				return this.DataObject.IncreaseScoreWithBizOrInfoUrls;
			}
			set
			{
				this.DataObject.IncreaseScoreWithBizOrInfoUrls = value;
			}
		}

		// Token: 0x17001C43 RID: 7235
		// (get) Token: 0x06005DEE RID: 24046 RVA: 0x0018A4EE File Offset: 0x001886EE
		// (set) Token: 0x06005DEF RID: 24047 RVA: 0x0018A4FB File Offset: 0x001886FB
		[Parameter]
		public SpamFilteringOption MarkAsSpamEmptyMessages
		{
			get
			{
				return this.DataObject.MarkAsSpamEmptyMessages;
			}
			set
			{
				this.DataObject.MarkAsSpamEmptyMessages = value;
			}
		}

		// Token: 0x17001C44 RID: 7236
		// (get) Token: 0x06005DF0 RID: 24048 RVA: 0x0018A509 File Offset: 0x00188709
		// (set) Token: 0x06005DF1 RID: 24049 RVA: 0x0018A516 File Offset: 0x00188716
		[Parameter]
		public SpamFilteringOption MarkAsSpamJavaScriptInHtml
		{
			get
			{
				return this.DataObject.MarkAsSpamJavaScriptInHtml;
			}
			set
			{
				this.DataObject.MarkAsSpamJavaScriptInHtml = value;
			}
		}

		// Token: 0x17001C45 RID: 7237
		// (get) Token: 0x06005DF2 RID: 24050 RVA: 0x0018A524 File Offset: 0x00188724
		// (set) Token: 0x06005DF3 RID: 24051 RVA: 0x0018A531 File Offset: 0x00188731
		[Parameter]
		public SpamFilteringOption MarkAsSpamFramesInHtml
		{
			get
			{
				return this.DataObject.MarkAsSpamFramesInHtml;
			}
			set
			{
				this.DataObject.MarkAsSpamFramesInHtml = value;
			}
		}

		// Token: 0x17001C46 RID: 7238
		// (get) Token: 0x06005DF4 RID: 24052 RVA: 0x0018A53F File Offset: 0x0018873F
		// (set) Token: 0x06005DF5 RID: 24053 RVA: 0x0018A54C File Offset: 0x0018874C
		[Parameter]
		public SpamFilteringOption MarkAsSpamObjectTagsInHtml
		{
			get
			{
				return this.DataObject.MarkAsSpamObjectTagsInHtml;
			}
			set
			{
				this.DataObject.MarkAsSpamObjectTagsInHtml = value;
			}
		}

		// Token: 0x17001C47 RID: 7239
		// (get) Token: 0x06005DF6 RID: 24054 RVA: 0x0018A55A File Offset: 0x0018875A
		// (set) Token: 0x06005DF7 RID: 24055 RVA: 0x0018A567 File Offset: 0x00188767
		[Parameter]
		public SpamFilteringOption MarkAsSpamEmbedTagsInHtml
		{
			get
			{
				return this.DataObject.MarkAsSpamEmbedTagsInHtml;
			}
			set
			{
				this.DataObject.MarkAsSpamEmbedTagsInHtml = value;
			}
		}

		// Token: 0x17001C48 RID: 7240
		// (get) Token: 0x06005DF8 RID: 24056 RVA: 0x0018A575 File Offset: 0x00188775
		// (set) Token: 0x06005DF9 RID: 24057 RVA: 0x0018A582 File Offset: 0x00188782
		[Parameter]
		public SpamFilteringOption MarkAsSpamFormTagsInHtml
		{
			get
			{
				return this.DataObject.MarkAsSpamFormTagsInHtml;
			}
			set
			{
				this.DataObject.MarkAsSpamFormTagsInHtml = value;
			}
		}

		// Token: 0x17001C49 RID: 7241
		// (get) Token: 0x06005DFA RID: 24058 RVA: 0x0018A590 File Offset: 0x00188790
		// (set) Token: 0x06005DFB RID: 24059 RVA: 0x0018A59D File Offset: 0x0018879D
		[Parameter]
		public SpamFilteringOption MarkAsSpamWebBugsInHtml
		{
			get
			{
				return this.DataObject.MarkAsSpamWebBugsInHtml;
			}
			set
			{
				this.DataObject.MarkAsSpamWebBugsInHtml = value;
			}
		}

		// Token: 0x17001C4A RID: 7242
		// (get) Token: 0x06005DFC RID: 24060 RVA: 0x0018A5AB File Offset: 0x001887AB
		// (set) Token: 0x06005DFD RID: 24061 RVA: 0x0018A5B8 File Offset: 0x001887B8
		[Parameter]
		public SpamFilteringOption MarkAsSpamSensitiveWordList
		{
			get
			{
				return this.DataObject.MarkAsSpamSensitiveWordList;
			}
			set
			{
				this.DataObject.MarkAsSpamSensitiveWordList = value;
			}
		}

		// Token: 0x17001C4B RID: 7243
		// (get) Token: 0x06005DFE RID: 24062 RVA: 0x0018A5C6 File Offset: 0x001887C6
		// (set) Token: 0x06005DFF RID: 24063 RVA: 0x0018A5D3 File Offset: 0x001887D3
		[Parameter]
		public SpamFilteringOption MarkAsSpamSpfRecordHardFail
		{
			get
			{
				return this.DataObject.MarkAsSpamSpfRecordHardFail;
			}
			set
			{
				this.DataObject.MarkAsSpamSpfRecordHardFail = value;
			}
		}

		// Token: 0x17001C4C RID: 7244
		// (get) Token: 0x06005E00 RID: 24064 RVA: 0x0018A5E1 File Offset: 0x001887E1
		// (set) Token: 0x06005E01 RID: 24065 RVA: 0x0018A5EE File Offset: 0x001887EE
		[Parameter]
		public SpamFilteringOption MarkAsSpamFromAddressAuthFail
		{
			get
			{
				return this.DataObject.MarkAsSpamFromAddressAuthFail;
			}
			set
			{
				this.DataObject.MarkAsSpamFromAddressAuthFail = value;
			}
		}

		// Token: 0x17001C4D RID: 7245
		// (get) Token: 0x06005E02 RID: 24066 RVA: 0x0018A5FC File Offset: 0x001887FC
		// (set) Token: 0x06005E03 RID: 24067 RVA: 0x0018A609 File Offset: 0x00188809
		[Parameter]
		public SpamFilteringOption MarkAsSpamNdrBackscatter
		{
			get
			{
				return this.DataObject.MarkAsSpamNdrBackscatter;
			}
			set
			{
				this.DataObject.MarkAsSpamNdrBackscatter = value;
			}
		}

		// Token: 0x17001C4E RID: 7246
		// (get) Token: 0x06005E04 RID: 24068 RVA: 0x0018A617 File Offset: 0x00188817
		// (set) Token: 0x06005E05 RID: 24069 RVA: 0x0018A624 File Offset: 0x00188824
		[Parameter]
		public SpamFilteringOption MarkAsSpamBulkMail
		{
			get
			{
				return this.DataObject.MarkAsSpamBulkMail;
			}
			set
			{
				this.DataObject.MarkAsSpamBulkMail = value;
			}
		}

		// Token: 0x17001C4F RID: 7247
		// (get) Token: 0x06005E06 RID: 24070 RVA: 0x0018A632 File Offset: 0x00188832
		// (set) Token: 0x06005E07 RID: 24071 RVA: 0x0018A63F File Offset: 0x0018883F
		[Parameter]
		public int BulkThreshold
		{
			get
			{
				return this.DataObject.BulkThreshold;
			}
			set
			{
				this.DataObject.BulkThreshold = value;
			}
		}

		// Token: 0x17001C50 RID: 7248
		// (get) Token: 0x06005E08 RID: 24072 RVA: 0x0018A64D File Offset: 0x0018884D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewHostedContentFilterPolicy(this.Name);
			}
		}

		// Token: 0x17001C51 RID: 7249
		// (get) Token: 0x06005E09 RID: 24073 RVA: 0x0018A65A File Offset: 0x0018885A
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Dehydrateable;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x06005E0A RID: 24074 RVA: 0x0018A66C File Offset: 0x0018886C
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			HostedContentFilterPolicy hostedContentFilterPolicy = (HostedContentFilterPolicy)base.PrepareDataObject();
			hostedContentFilterPolicy.SetId((IConfigurationSession)base.DataSession, this.Name);
			if (!this.HostedContentFilterPolicyExist())
			{
				this.DataObject.IsDefault = true;
			}
			TaskLogger.LogExit();
			return hostedContentFilterPolicy;
		}

		// Token: 0x06005E0B RID: 24075 RVA: 0x0018A6D4 File Offset: 0x001888D4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.DataObject.LanguageBlockList != null)
			{
				foreach (string text in from x in this.DataObject.LanguageBlockList
				where !HygieneUtils.IsAntispamFilterableLanguage(x)
				select x)
				{
					base.WriteError(new ArgumentException(Strings.ErrorUnsupportedBlockLanguage(text)), ErrorCategory.InvalidArgument, text);
				}
			}
			if (this.DataObject.RegionBlockList != null)
			{
				foreach (string text2 in from x in this.DataObject.RegionBlockList
				where !HygieneUtils.IsValidIso3166Alpha2Code(x)
				select x)
				{
					base.WriteError(new ArgumentException(Strings.ErrorInvalidIso3166Alpha2Code(text2)), ErrorCategory.InvalidArgument, text2);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005E0C RID: 24076 RVA: 0x0018A7F8 File Offset: 0x001889F8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.CreateParentContainerIfNeeded(this.DataObject);
			base.InternalProcessRecord();
			FfoDualWriter.SaveToFfo<HostedContentFilterPolicy>(this, this.DataObject, null);
			TaskLogger.LogExit();
		}

		// Token: 0x06005E0D RID: 24077 RVA: 0x0018A868 File Offset: 0x00188A68
		private bool HostedContentFilterPolicyExist()
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ExchangeVersion, ExchangeObjectVersion.Exchange2012);
			HostedContentFilterPolicy[] array = ((IConfigurationSession)base.DataSession).Find<HostedContentFilterPolicy>(null, QueryScope.SubTree, filter, null, 1);
			return array.Length != 0;
		}
	}
}
