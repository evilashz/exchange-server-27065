using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200050B RID: 1291
	[DataContract]
	public class LitigationHoldFeatureInfo : MailboxFeatureInfo
	{
		// Token: 0x06003DFD RID: 15869 RVA: 0x000BA3B0 File Offset: 0x000B85B0
		public LitigationHoldFeatureInfo(Mailbox mailbox) : base(mailbox)
		{
			base.UseModalDialogForEdit = true;
			base.UseModalDialogForEnable = true;
			base.Name = Strings.LitigationHoldFeatureName;
			base.EnableWizardDialogHeight = new int?(450);
			base.EnableWizardDialogWidth = new int?(510);
			base.PropertiesDialogHeight = new int?(450);
			base.PropertiesDialogWidth = new int?(510);
			this.IconAltText = Strings.MailboxAltText;
			this.SpriteId = CommandSprite.GetCssClass(CommandSprite.SpriteId.Mailbox16);
			if (mailbox != null)
			{
				this.Caption = mailbox.DisplayName;
				bool litigationHoldEnabled = mailbox.LitigationHoldEnabled;
				this.Status = (litigationHoldEnabled ? ClientStrings.EnabledDisplayText : ClientStrings.DisabledDisplayText);
				if (base.IsInRole(LitigationHoldFeatureInfo.enableRoles) && !litigationHoldEnabled)
				{
					this.CanChangeStatus = true;
					base.EnableCommandUrl = "EditLitigationHold.aspx";
				}
				if (base.IsInRole(LitigationHoldFeatureInfo.editRoles) && litigationHoldEnabled)
				{
					base.EditCommandUrl = "EditLitigationHold.aspx";
				}
				if (base.IsInRole(LitigationHoldFeatureInfo.disableRoles) && litigationHoldEnabled)
				{
					this.CanChangeStatus = true;
				}
				this.RetentionComment = mailbox.RetentionComment;
				this.RetentionUrl = mailbox.RetentionUrl;
				if (mailbox.LitigationHoldDate != null)
				{
					this.LitigationHoldDate = mailbox.LitigationHoldDate.Value.ToUniversalTime().UtcToUserDateTimeString();
				}
				else
				{
					this.LitigationHoldDate = Strings.LitigationHoldDateNotSet;
				}
				if (!string.IsNullOrEmpty(mailbox.LitigationHoldOwner))
				{
					this.LitigationHoldOwner = mailbox.LitigationHoldOwner;
				}
				else
				{
					this.LitigationHoldOwner = Strings.LitigationHoldOwnerNotSet;
				}
			}
			if (!base.IsReadOnly && mailbox.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
			{
				base.ShowReadOnly = true;
				this.CanChangeStatus = false;
				base.EditCommandUrl = null;
			}
		}

		// Token: 0x17002454 RID: 9300
		// (get) Token: 0x06003DFE RID: 15870 RVA: 0x000BA579 File Offset: 0x000B8779
		// (set) Token: 0x06003DFF RID: 15871 RVA: 0x000BA581 File Offset: 0x000B8781
		[DataMember]
		public string Caption { get; private set; }

		// Token: 0x17002455 RID: 9301
		// (get) Token: 0x06003E00 RID: 15872 RVA: 0x000BA58A File Offset: 0x000B878A
		// (set) Token: 0x06003E01 RID: 15873 RVA: 0x000BA592 File Offset: 0x000B8792
		[DataMember]
		public string RetentionComment { get; private set; }

		// Token: 0x17002456 RID: 9302
		// (get) Token: 0x06003E02 RID: 15874 RVA: 0x000BA59B File Offset: 0x000B879B
		// (set) Token: 0x06003E03 RID: 15875 RVA: 0x000BA5A3 File Offset: 0x000B87A3
		[DataMember]
		public string RetentionUrl { get; private set; }

		// Token: 0x17002457 RID: 9303
		// (get) Token: 0x06003E04 RID: 15876 RVA: 0x000BA5AC File Offset: 0x000B87AC
		// (set) Token: 0x06003E05 RID: 15877 RVA: 0x000BA5B4 File Offset: 0x000B87B4
		[DataMember]
		public string LitigationHoldDate { get; private set; }

		// Token: 0x17002458 RID: 9304
		// (get) Token: 0x06003E06 RID: 15878 RVA: 0x000BA5BD File Offset: 0x000B87BD
		// (set) Token: 0x06003E07 RID: 15879 RVA: 0x000BA5C5 File Offset: 0x000B87C5
		[DataMember]
		public string LitigationHoldOwner { get; private set; }

		// Token: 0x04002845 RID: 10309
		private const string LitigationHoldUrl = "EditLitigationHold.aspx";

		// Token: 0x04002846 RID: 10310
		private static readonly string[] enableRoles = new string[]
		{
			"Get-Mailbox?Identity@R:Organization+Set-Mailbox?Identity&LitigationHoldEnabled&RetentionComment&RetentionUrl@W:Organization"
		};

		// Token: 0x04002847 RID: 10311
		private static readonly string[] editRoles = new string[]
		{
			"Get-Mailbox?Identity@R:Organization",
			"Get-Mailbox?Identity@R:Organization+Set-Mailbox?Identity&RetentionComment&RetentionUrl@W:Organization"
		};

		// Token: 0x04002848 RID: 10312
		private static readonly string[] disableRoles = new string[]
		{
			"Set-Mailbox?Identity&LitigationHoldEnabled@W:Organization"
		};
	}
}
