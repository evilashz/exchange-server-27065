using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C47 RID: 3143
	[Cmdlet("Set", "HostedEncryptionVirtualDirectory", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetHostedEncryptionVirtualDirectory : SetWebAppVirtualDirectory<ADE4eVirtualDirectory>
	{
		// Token: 0x170024A5 RID: 9381
		// (get) Token: 0x060076E6 RID: 30438 RVA: 0x001E515C File Offset: 0x001E335C
		// (set) Token: 0x060076E7 RID: 30439 RVA: 0x001E5173 File Offset: 0x001E3373
		[Parameter]
		public MultiValuedProperty<string> AllowedFileTypes
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["AllowedFileTypes"];
			}
			set
			{
				base.Fields["AllowedFileTypes"] = value;
			}
		}

		// Token: 0x170024A6 RID: 9382
		// (get) Token: 0x060076E8 RID: 30440 RVA: 0x001E5186 File Offset: 0x001E3386
		// (set) Token: 0x060076E9 RID: 30441 RVA: 0x001E519D File Offset: 0x001E339D
		[Parameter]
		public MultiValuedProperty<string> AllowedMimeTypes
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["AllowedMimeTypes"];
			}
			set
			{
				base.Fields["AllowedMimeTypes"] = value;
			}
		}

		// Token: 0x170024A7 RID: 9383
		// (get) Token: 0x060076EA RID: 30442 RVA: 0x001E51B0 File Offset: 0x001E33B0
		// (set) Token: 0x060076EB RID: 30443 RVA: 0x001E51C7 File Offset: 0x001E33C7
		[Parameter]
		public MultiValuedProperty<string> BlockedFileTypes
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["BlockedFileTypes"];
			}
			set
			{
				base.Fields["BlockedFileTypes"] = value;
			}
		}

		// Token: 0x170024A8 RID: 9384
		// (get) Token: 0x060076EC RID: 30444 RVA: 0x001E51DA File Offset: 0x001E33DA
		// (set) Token: 0x060076ED RID: 30445 RVA: 0x001E51F1 File Offset: 0x001E33F1
		[Parameter]
		public MultiValuedProperty<string> BlockedMimeTypes
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["BlockedMimeTypes"];
			}
			set
			{
				base.Fields["BlockedMimeTypes"] = value;
			}
		}

		// Token: 0x170024A9 RID: 9385
		// (get) Token: 0x060076EE RID: 30446 RVA: 0x001E5204 File Offset: 0x001E3404
		// (set) Token: 0x060076EF RID: 30447 RVA: 0x001E521B File Offset: 0x001E341B
		[Parameter]
		public MultiValuedProperty<string> ForceSaveFileTypes
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["ForceSaveFileTypes"];
			}
			set
			{
				base.Fields["ForceSaveFileTypes"] = value;
			}
		}

		// Token: 0x170024AA RID: 9386
		// (get) Token: 0x060076F0 RID: 30448 RVA: 0x001E522E File Offset: 0x001E342E
		// (set) Token: 0x060076F1 RID: 30449 RVA: 0x001E5245 File Offset: 0x001E3445
		[Parameter]
		public MultiValuedProperty<string> ForceSaveMimeTypes
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["ForceSaveMimeTypes"];
			}
			set
			{
				base.Fields["ForceSaveMimeTypes"] = value;
			}
		}

		// Token: 0x170024AB RID: 9387
		// (get) Token: 0x060076F2 RID: 30450 RVA: 0x001E5258 File Offset: 0x001E3458
		// (set) Token: 0x060076F3 RID: 30451 RVA: 0x001E526F File Offset: 0x001E346F
		[Parameter]
		public bool? AlwaysShowBcc
		{
			get
			{
				return (bool?)base.Fields["AlwaysShowBcc"];
			}
			set
			{
				base.Fields["AlwaysShowBcc"] = value;
			}
		}

		// Token: 0x170024AC RID: 9388
		// (get) Token: 0x060076F4 RID: 30452 RVA: 0x001E5287 File Offset: 0x001E3487
		// (set) Token: 0x060076F5 RID: 30453 RVA: 0x001E529E File Offset: 0x001E349E
		[Parameter]
		public bool? CheckForForgottenAttachments
		{
			get
			{
				return (bool?)base.Fields["CheckForForgottenAttachments"];
			}
			set
			{
				base.Fields["CheckForForgottenAttachments"] = value;
			}
		}

		// Token: 0x170024AD RID: 9389
		// (get) Token: 0x060076F6 RID: 30454 RVA: 0x001E52B6 File Offset: 0x001E34B6
		// (set) Token: 0x060076F7 RID: 30455 RVA: 0x001E52CD File Offset: 0x001E34CD
		[Parameter]
		public bool? HideMailTipsByDefault
		{
			get
			{
				return (bool?)base.Fields["HideMailTipsByDefault"];
			}
			set
			{
				base.Fields["HideMailTipsByDefault"] = value;
			}
		}

		// Token: 0x170024AE RID: 9390
		// (get) Token: 0x060076F8 RID: 30456 RVA: 0x001E52E5 File Offset: 0x001E34E5
		// (set) Token: 0x060076F9 RID: 30457 RVA: 0x001E52FC File Offset: 0x001E34FC
		[Parameter]
		public uint? MailTipsLargeAudienceThreshold
		{
			get
			{
				return (uint?)base.Fields["MailTipsLargeAudienceThreshold"];
			}
			set
			{
				base.Fields["MailTipsLargeAudienceThreshold"] = value;
			}
		}

		// Token: 0x170024AF RID: 9391
		// (get) Token: 0x060076FA RID: 30458 RVA: 0x001E5314 File Offset: 0x001E3514
		// (set) Token: 0x060076FB RID: 30459 RVA: 0x001E532B File Offset: 0x001E352B
		[Parameter]
		public int? MaxRecipientsPerMessage
		{
			get
			{
				return (int?)base.Fields["MaxRecipientsPerMessage"];
			}
			set
			{
				base.Fields["MaxRecipientsPerMessage"] = value;
			}
		}

		// Token: 0x170024B0 RID: 9392
		// (get) Token: 0x060076FC RID: 30460 RVA: 0x001E5343 File Offset: 0x001E3543
		// (set) Token: 0x060076FD RID: 30461 RVA: 0x001E535A File Offset: 0x001E355A
		[Parameter]
		public int? MaxMessageSizeInKb
		{
			get
			{
				return (int?)base.Fields["MaxMessageSizeInKb"];
			}
			set
			{
				base.Fields["MaxMessageSizeInKb"] = value;
			}
		}

		// Token: 0x170024B1 RID: 9393
		// (get) Token: 0x060076FE RID: 30462 RVA: 0x001E5372 File Offset: 0x001E3572
		// (set) Token: 0x060076FF RID: 30463 RVA: 0x001E5389 File Offset: 0x001E3589
		[Parameter]
		public string ComposeFontColor
		{
			get
			{
				return (string)base.Fields["ComposeFontColor"];
			}
			set
			{
				base.Fields["ComposeFontColor"] = value;
			}
		}

		// Token: 0x170024B2 RID: 9394
		// (get) Token: 0x06007700 RID: 30464 RVA: 0x001E539C File Offset: 0x001E359C
		// (set) Token: 0x06007701 RID: 30465 RVA: 0x001E53B3 File Offset: 0x001E35B3
		[Parameter]
		public string ComposeFontName
		{
			get
			{
				return (string)base.Fields["ComposeFontName"];
			}
			set
			{
				base.Fields["ComposeFontName"] = value;
			}
		}

		// Token: 0x170024B3 RID: 9395
		// (get) Token: 0x06007702 RID: 30466 RVA: 0x001E53C6 File Offset: 0x001E35C6
		// (set) Token: 0x06007703 RID: 30467 RVA: 0x001E53DD File Offset: 0x001E35DD
		[Parameter]
		public int? ComposeFontSize
		{
			get
			{
				return (int?)base.Fields["ComposeFontSize"];
			}
			set
			{
				base.Fields["ComposeFontSize"] = value;
			}
		}

		// Token: 0x170024B4 RID: 9396
		// (get) Token: 0x06007704 RID: 30468 RVA: 0x001E53F5 File Offset: 0x001E35F5
		// (set) Token: 0x06007705 RID: 30469 RVA: 0x001E540C File Offset: 0x001E360C
		[Parameter]
		public int? MaxImageSizeKB
		{
			get
			{
				return (int?)base.Fields["MaxImageSizeKB"];
			}
			set
			{
				base.Fields["MaxImageSizeKB"] = value;
			}
		}

		// Token: 0x170024B5 RID: 9397
		// (get) Token: 0x06007706 RID: 30470 RVA: 0x001E5424 File Offset: 0x001E3624
		// (set) Token: 0x06007707 RID: 30471 RVA: 0x001E543B File Offset: 0x001E363B
		[Parameter]
		public int? MaxAttachmentSizeKB
		{
			get
			{
				return (int?)base.Fields["MaxAttachmentSizeKB"];
			}
			set
			{
				base.Fields["MaxAttachmentSizeKB"] = value;
			}
		}

		// Token: 0x170024B6 RID: 9398
		// (get) Token: 0x06007708 RID: 30472 RVA: 0x001E5453 File Offset: 0x001E3653
		// (set) Token: 0x06007709 RID: 30473 RVA: 0x001E546A File Offset: 0x001E366A
		[Parameter]
		public int? MaxEncryptedContentSizeKB
		{
			get
			{
				return (int?)base.Fields["MaxEncryptedContentSizeKB"];
			}
			set
			{
				base.Fields["MaxEncryptedContentSizeKB"] = value;
			}
		}

		// Token: 0x170024B7 RID: 9399
		// (get) Token: 0x0600770A RID: 30474 RVA: 0x001E5482 File Offset: 0x001E3682
		// (set) Token: 0x0600770B RID: 30475 RVA: 0x001E5499 File Offset: 0x001E3699
		[Parameter]
		public int? MaxEmailStringSize
		{
			get
			{
				return (int?)base.Fields["MaxEmailStringSize"];
			}
			set
			{
				base.Fields["MaxEmailStringSize"] = value;
			}
		}

		// Token: 0x170024B8 RID: 9400
		// (get) Token: 0x0600770C RID: 30476 RVA: 0x001E54B1 File Offset: 0x001E36B1
		// (set) Token: 0x0600770D RID: 30477 RVA: 0x001E54C8 File Offset: 0x001E36C8
		[Parameter]
		public int? MaxPortalStringSize
		{
			get
			{
				return (int?)base.Fields["MaxPortalStringSize"];
			}
			set
			{
				base.Fields["MaxPortalStringSize"] = value;
			}
		}

		// Token: 0x170024B9 RID: 9401
		// (get) Token: 0x0600770E RID: 30478 RVA: 0x001E54E0 File Offset: 0x001E36E0
		// (set) Token: 0x0600770F RID: 30479 RVA: 0x001E54F7 File Offset: 0x001E36F7
		[Parameter]
		public int? MaxFwdAllowed
		{
			get
			{
				return (int?)base.Fields["MaxFwdAllowed"];
			}
			set
			{
				base.Fields["MaxFwdAllowed"] = value;
			}
		}

		// Token: 0x170024BA RID: 9402
		// (get) Token: 0x06007710 RID: 30480 RVA: 0x001E550F File Offset: 0x001E370F
		// (set) Token: 0x06007711 RID: 30481 RVA: 0x001E5526 File Offset: 0x001E3726
		[Parameter]
		public int? PortalInactivityTimeout
		{
			get
			{
				return (int?)base.Fields["PortalInactivityTimeout"];
			}
			set
			{
				base.Fields["PortalInactivityTimeout"] = value;
			}
		}

		// Token: 0x170024BB RID: 9403
		// (get) Token: 0x06007712 RID: 30482 RVA: 0x001E553E File Offset: 0x001E373E
		// (set) Token: 0x06007713 RID: 30483 RVA: 0x001E5555 File Offset: 0x001E3755
		[Parameter]
		public int? TDSTimeOut
		{
			get
			{
				return (int?)base.Fields["TDSTimeOut"];
			}
			set
			{
				base.Fields["TDSTimeOut"] = value;
			}
		}

		// Token: 0x170024BC RID: 9404
		// (get) Token: 0x06007714 RID: 30484 RVA: 0x001E556D File Offset: 0x001E376D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetHostedEncryptionVirtualDirectory(this.Identity.ToString());
			}
		}

		// Token: 0x06007715 RID: 30485 RVA: 0x001E5580 File Offset: 0x001E3780
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			ADE4eVirtualDirectory dataObject = this.DataObject;
			WebAppVirtualDirectoryHelper.UpdateMetabase(dataObject, dataObject.MetabasePath, true);
			TaskLogger.LogExit();
		}

		// Token: 0x06007716 RID: 30486 RVA: 0x001E55B4 File Offset: 0x001E37B4
		protected override void UpdateDataObject(ADE4eVirtualDirectory dataObject)
		{
			if (base.Fields.Contains("AllowedFileTypes"))
			{
				dataObject.AllowedFileTypes = this.AllowedFileTypes;
			}
			if (base.Fields.Contains("AllowedMimeTypes"))
			{
				dataObject.AllowedMimeTypes = this.AllowedMimeTypes;
			}
			if (base.Fields.Contains("BlockedFileTypes"))
			{
				dataObject.BlockedFileTypes = this.BlockedFileTypes;
			}
			if (base.Fields.Contains("BlockedMimeTypes"))
			{
				dataObject.BlockedMimeTypes = this.BlockedMimeTypes;
			}
			if (base.Fields.Contains("ForceSaveFileTypes"))
			{
				dataObject.ForceSaveFileTypes = this.ForceSaveFileTypes;
			}
			if (base.Fields.Contains("ForceSaveMimeTypes"))
			{
				dataObject.ForceSaveMimeTypes = this.ForceSaveMimeTypes;
			}
			if (base.Fields.Contains("AlwaysShowBcc"))
			{
				dataObject.AlwaysShowBcc = this.AlwaysShowBcc;
			}
			if (base.Fields.Contains("CheckForForgottenAttachments"))
			{
				dataObject.CheckForForgottenAttachments = this.CheckForForgottenAttachments;
			}
			if (base.Fields.Contains("HideMailTipsByDefault"))
			{
				dataObject.HideMailTipsByDefault = this.HideMailTipsByDefault;
			}
			if (base.Fields.Contains("MailTipsLargeAudienceThreshold"))
			{
				dataObject.MailTipsLargeAudienceThreshold = this.MailTipsLargeAudienceThreshold;
			}
			if (base.Fields.Contains("MaxRecipientsPerMessage"))
			{
				dataObject.MaxRecipientsPerMessage = this.MaxRecipientsPerMessage;
			}
			if (base.Fields.Contains("MaxMessageSizeInKb"))
			{
				dataObject.MaxMessageSizeInKb = this.MaxMessageSizeInKb;
			}
			if (base.Fields.Contains("ComposeFontColor"))
			{
				dataObject.ComposeFontColor = this.ComposeFontColor;
			}
			if (base.Fields.Contains("ComposeFontName"))
			{
				dataObject.ComposeFontName = this.ComposeFontName;
			}
			if (base.Fields.Contains("ComposeFontSize"))
			{
				dataObject.ComposeFontSize = this.ComposeFontSize;
			}
			if (base.Fields.Contains("MaxImageSizeKB"))
			{
				dataObject.MaxImageSizeKB = this.MaxImageSizeKB;
			}
			if (base.Fields.Contains("MaxAttachmentSizeKB"))
			{
				dataObject.MaxAttachmentSizeKB = this.MaxAttachmentSizeKB;
			}
			if (base.Fields.Contains("MaxEncryptedContentSizeKB"))
			{
				dataObject.MaxEncryptedContentSizeKB = this.MaxEncryptedContentSizeKB;
			}
			if (base.Fields.Contains("MaxEmailStringSize"))
			{
				dataObject.MaxEmailStringSize = this.MaxEmailStringSize;
			}
			if (base.Fields.Contains("MaxPortalStringSize"))
			{
				dataObject.MaxPortalStringSize = this.MaxPortalStringSize;
			}
			if (base.Fields.Contains("MaxFwdAllowed"))
			{
				dataObject.MaxFwdAllowed = this.MaxFwdAllowed;
			}
			if (base.Fields.Contains("PortalInactivityTimeout"))
			{
				dataObject.PortalInactivityTimeout = this.PortalInactivityTimeout;
			}
			if (base.Fields.Contains("TDSTimeOut"))
			{
				dataObject.TDSTimeOut = this.TDSTimeOut;
			}
			dataObject.SaveSettings();
			if (base.Fields.Contains("GzipLevel") && base.GzipLevel != dataObject.GzipLevel)
			{
				dataObject.GzipLevel = base.GzipLevel;
				base.CheckGzipLevelIsNotError(dataObject.GzipLevel);
			}
			base.UpdateDataObject(dataObject);
		}
	}
}
