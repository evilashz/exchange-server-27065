using System;
using System.Runtime.Serialization;
using System.Web;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200050A RID: 1290
	[DataContract]
	public class UMMailboxFeatureInfo : MailboxFeatureInfo
	{
		// Token: 0x06003DF5 RID: 15861 RVA: 0x000BA138 File Offset: 0x000B8338
		public UMMailboxFeatureInfo(UMMailbox mailbox) : base(mailbox)
		{
			this.Initialize(true, false);
			this.UMMailboxPolicy = mailbox.UMMailboxPolicy.ToString();
			this.WhenChanged = mailbox.WhenChanged.ToString();
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x000BA17F File Offset: 0x000B837F
		public UMMailboxFeatureInfo(Mailbox mailbox) : base(mailbox)
		{
			this.Initialize(mailbox.UMEnabled, !mailbox.UMEnabled && Utils.UnifiedMessagingAvailable((ADUser)mailbox.DataObject));
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x000BA1B0 File Offset: 0x000B83B0
		private void Initialize(bool isEnabled, bool canEnable)
		{
			base.UseModalDialogForEdit = false;
			base.Name = Strings.UMMailboxFeatureName;
			this.IconAltText = Strings.UMAltText;
			this.SpriteId = CommandSprite.GetCssClass(CommandSprite.SpriteId.UMMailboxFeature);
			base.EnableWizardDialogHeight = new int?(574);
			base.EnableWizardDialogWidth = new int?(600);
			base.PropertiesDialogHeight = new int?(PopupCommand.DefaultBookmarkedPopupHeight);
			base.PropertiesDialogWidth = new int?(PopupCommand.DefaultBookmarkedPopupWidth);
			if (base.Identity != null)
			{
				this.Status = (isEnabled ? ClientStrings.EnabledDisplayText : ClientStrings.DisabledDisplayText);
				base.EnableCommandUrl = "EnableUMMailbox.aspx";
				if (!isEnabled && canEnable && base.IsInRole(UMMailboxFeatureInfo.enableRoles))
				{
					this.CanChangeStatus = true;
				}
				if (isEnabled)
				{
					if (base.IsInRole(UMMailboxFeatureInfo.disableRoles))
					{
						this.CanChangeStatus = true;
					}
					if (base.IsInRole(UMMailboxFeatureInfo.editRoles))
					{
						base.EditCommandUrl = "EditUMMailbox.aspx?id=" + HttpUtility.UrlEncode(base.Identity.RawIdentity);
					}
				}
			}
		}

		// Token: 0x17002452 RID: 9298
		// (get) Token: 0x06003DF8 RID: 15864 RVA: 0x000BA2BD File Offset: 0x000B84BD
		// (set) Token: 0x06003DF9 RID: 15865 RVA: 0x000BA2C5 File Offset: 0x000B84C5
		[DataMember]
		public string UMMailboxPolicy { get; protected set; }

		// Token: 0x17002453 RID: 9299
		// (get) Token: 0x06003DFA RID: 15866 RVA: 0x000BA2CE File Offset: 0x000B84CE
		// (set) Token: 0x06003DFB RID: 15867 RVA: 0x000BA2D6 File Offset: 0x000B84D6
		[DataMember]
		public string WhenChanged { get; set; }

		// Token: 0x0400283C RID: 10300
		private static readonly string[] enableRoles = new string[]
		{
			"Get-Recipient?Identity@R:Organization",
			"Enable-UMMailbox?Identity@W:Organization",
			"Get-UMMailboxPolicy@R:Organization"
		};

		// Token: 0x0400283D RID: 10301
		public static readonly string EnableRolesString = UMMailboxFeatureInfo.enableRoles.StringArrayJoin("+");

		// Token: 0x0400283E RID: 10302
		private static readonly string[] disableRoles = new string[]
		{
			"Disable-UMMailbox?Identity@W:Organization"
		};

		// Token: 0x0400283F RID: 10303
		public static readonly string DisableRolesString = "Disable-UMMailbox?Identity@W:Organization";

		// Token: 0x04002840 RID: 10304
		private static readonly string[] editRoles = new string[]
		{
			"Get-UMMailbox?Identity@R:Organization",
			"Get-UMMailboxPin?Identity@R:Organization"
		};

		// Token: 0x04002841 RID: 10305
		public static readonly string EditRolesString = UMMailboxFeatureInfo.editRoles.StringArrayJoin("+");

		// Token: 0x04002842 RID: 10306
		public static readonly string CanShowRoles = string.Concat(new string[]
		{
			UMMailboxFeatureInfo.DisableRolesString,
			"+",
			UMMailboxFeatureInfo.EnableRolesString,
			",",
			UMMailboxFeatureInfo.EditRolesString
		});
	}
}
