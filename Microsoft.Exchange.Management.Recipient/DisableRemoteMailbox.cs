using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000B1 RID: 177
	[Cmdlet("Disable", "RemoteMailbox", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class DisableRemoteMailbox : DisableMailUserBase<RemoteMailboxIdParameter>
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0002FCB5 File Offset: 0x0002DEB5
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Archive" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageDisableArchive(this.Identity.ToString());
				}
				return Strings.ConfirmationMessageDisableRemoteMailbox(this.Identity.ToString());
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002FCEC File Offset: 0x0002DEEC
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADUser aduser = (ADUser)base.PrepareDataObject();
			if ("Archive" == base.ParameterSetName)
			{
				aduser.RemoteRecipientType = ((aduser.RemoteRecipientType &= ~RemoteRecipientType.ProvisionArchive) | RemoteRecipientType.DeprovisionArchive);
			}
			else
			{
				bool flag = (aduser.RemoteRecipientType & RemoteRecipientType.ProvisionArchive) == RemoteRecipientType.ProvisionArchive;
				aduser.RemoteRecipientType = RemoteRecipientType.DeprovisionMailbox;
				if (flag)
				{
					aduser.RemoteRecipientType |= RemoteRecipientType.DeprovisionArchive;
				}
			}
			TaskLogger.LogExit();
			return aduser;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002FD6A File Offset: 0x0002DF6A
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return RemoteMailbox.FromDataObject((ADUser)dataObject);
		}
	}
}
