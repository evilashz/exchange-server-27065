using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PswsClient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200003C RID: 60
	[Cmdlet("Remove", "EOPMailUser", DefaultParameterSetName = "Identity")]
	public sealed class RemoveEOPMailUser : EOPTask
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000D6DC File Offset: 0x0000B8DC
		[Parameter(Mandatory = false)]
		public MailUserIdParameter Identity { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000D6E5 File Offset: 0x0000B8E5
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000D6ED File Offset: 0x0000B8ED
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x060002C9 RID: 713 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		protected override void InternalProcessRecord()
		{
			try
			{
				RemoveMailUserCmdlet removeMailUserCmdlet = new RemoveMailUserCmdlet();
				ADObjectId executingUserId;
				base.ExchangeRunspaceConfig.TryGetExecutingUserId(out executingUserId);
				removeMailUserCmdlet.Authenticator = Authenticator.Create(base.CurrentOrganizationId, executingUserId);
				removeMailUserCmdlet.HostServerName = EOPRecipient.GetPsWsHostServerName();
				if (string.IsNullOrEmpty(this.ExternalDirectoryObjectId) && this.Identity == null)
				{
					base.ThrowTaskError(new ArgumentException(CoreStrings.MissingIdentityParameter.ToString()));
				}
				EOPRecipient.SetProperty(removeMailUserCmdlet, Parameters.Identity, string.IsNullOrEmpty(this.ExternalDirectoryObjectId) ? this.Identity.ToString() : this.ExternalDirectoryObjectId);
				EOPRecipient.SetProperty(removeMailUserCmdlet, Parameters.Organization, base.Organization);
				removeMailUserCmdlet.Run();
				EOPRecipient.CheckForError(this, removeMailUserCmdlet);
			}
			catch (Exception e)
			{
				base.ThrowAndLogTaskError(e);
			}
		}
	}
}
