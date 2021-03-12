using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PswsClient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000036 RID: 54
	[Cmdlet("Enable", "EOPMailUser", DefaultParameterSetName = "Identity")]
	public sealed class EnableEOPMailUser : EOPTask
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000CD6A File Offset: 0x0000AF6A
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000CD72 File Offset: 0x0000AF72
		[Parameter(Mandatory = false)]
		public UserIdParameter Identity { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000CD7B File Offset: 0x0000AF7B
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000CD83 File Offset: 0x0000AF83
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x06000285 RID: 645 RVA: 0x0000CD8C File Offset: 0x0000AF8C
		protected override void InternalProcessRecord()
		{
			try
			{
				EnableMailUserCmdlet enableMailUserCmdlet = new EnableMailUserCmdlet();
				ADObjectId executingUserId;
				base.ExchangeRunspaceConfig.TryGetExecutingUserId(out executingUserId);
				enableMailUserCmdlet.Authenticator = Authenticator.Create(base.CurrentOrganizationId, executingUserId);
				enableMailUserCmdlet.HostServerName = EOPRecipient.GetPsWsHostServerName();
				if (string.IsNullOrEmpty(this.ExternalDirectoryObjectId) && this.Identity == null)
				{
					base.ThrowTaskError(new ArgumentException(CoreStrings.MissingIdentityParameter.ToString()));
				}
				EOPRecipient.SetProperty(enableMailUserCmdlet, Parameters.Identity, string.IsNullOrEmpty(this.ExternalDirectoryObjectId) ? this.Identity.ToString() : this.ExternalDirectoryObjectId);
				EOPRecipient.SetProperty(enableMailUserCmdlet, Parameters.Organization, base.Organization);
				enableMailUserCmdlet.Run();
				EOPRecipient.CheckForError(this, enableMailUserCmdlet);
			}
			catch (Exception e)
			{
				base.ThrowAndLogTaskError(e);
			}
		}
	}
}
