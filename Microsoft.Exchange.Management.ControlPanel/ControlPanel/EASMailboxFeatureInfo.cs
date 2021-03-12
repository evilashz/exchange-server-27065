using System;
using System.Runtime.Serialization;
using System.Web;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000508 RID: 1288
	[DataContract]
	public class EASMailboxFeatureInfo : MailboxFeatureInfo
	{
		// Token: 0x06003DF3 RID: 15859 RVA: 0x000BA01C File Offset: 0x000B821C
		public EASMailboxFeatureInfo(CASMailbox mailbox) : base(mailbox)
		{
			base.UseModalDialogForEdit = false;
			base.Name = Strings.EASMailboxFeatureName;
			base.PropertiesDialogHeight = new int?(460);
			base.PropertiesDialogWidth = new int?(700);
			if (mailbox != null && mailbox.ActiveSyncEnabled)
			{
				this.Status = ClientStrings.EnabledDisplayText;
			}
			else
			{
				this.Status = ClientStrings.DisabledDisplayText;
			}
			base.EnableCommandUrl = null;
			base.EditCommandUrl = "/ecp/UsersGroups/EditMobileMailbox.aspx?id=" + HttpUtility.UrlEncode(base.Identity.RawIdentity) + "&DataTransferMode=Isolation";
			this.CanChangeStatus = RbacPrincipal.Current.IsInRole("Set-CasMailbox?Identity&ActiveSyncEnabled@W:Organization");
		}
	}
}
