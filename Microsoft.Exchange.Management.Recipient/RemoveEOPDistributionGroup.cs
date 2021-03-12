using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PswsClient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200003B RID: 59
	[Cmdlet("Remove", "EOPDistributionGroup", DefaultParameterSetName = "Identity")]
	public sealed class RemoveEOPDistributionGroup : EOPTask
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000D5D8 File Offset: 0x0000B7D8
		[Parameter(Mandatory = false)]
		public DistributionGroupIdParameter Identity { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000D5E1 File Offset: 0x0000B7E1
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000D5E9 File Offset: 0x0000B7E9
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x060002C3 RID: 707 RVA: 0x0000D5F4 File Offset: 0x0000B7F4
		protected override void InternalProcessRecord()
		{
			try
			{
				RemoveDistributionGroupCmdlet removeDistributionGroupCmdlet = new RemoveDistributionGroupCmdlet();
				ADObjectId executingUserId;
				base.ExchangeRunspaceConfig.TryGetExecutingUserId(out executingUserId);
				removeDistributionGroupCmdlet.Authenticator = Authenticator.Create(base.CurrentOrganizationId, executingUserId);
				removeDistributionGroupCmdlet.HostServerName = EOPRecipient.GetPsWsHostServerName();
				if (string.IsNullOrEmpty(this.ExternalDirectoryObjectId) && this.Identity == null)
				{
					base.ThrowTaskError(new ArgumentException(CoreStrings.MissingIdentityParameter.ToString()));
				}
				EOPRecipient.SetProperty(removeDistributionGroupCmdlet, Parameters.Identity, string.IsNullOrEmpty(this.ExternalDirectoryObjectId) ? this.Identity.ToString() : this.ExternalDirectoryObjectId);
				EOPRecipient.SetProperty(removeDistributionGroupCmdlet, Parameters.Organization, base.Organization);
				removeDistributionGroupCmdlet.Run();
				EOPRecipient.CheckForError(this, removeDistributionGroupCmdlet);
			}
			catch (Exception e)
			{
				base.ThrowAndLogTaskError(e);
			}
		}
	}
}
