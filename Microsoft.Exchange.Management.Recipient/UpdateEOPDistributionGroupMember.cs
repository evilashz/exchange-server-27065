using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PswsClient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000041 RID: 65
	[Cmdlet("Update", "EOPDistributionGroupMember", DefaultParameterSetName = "Identity")]
	public sealed class UpdateEOPDistributionGroupMember : EOPTask
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000DFF8 File Offset: 0x0000C1F8
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000E000 File Offset: 0x0000C200
		[Parameter(Mandatory = false)]
		public DistributionGroupIdParameter Identity { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000E009 File Offset: 0x0000C209
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000E011 File Offset: 0x0000C211
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000E01A File Offset: 0x0000C21A
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000E022 File Offset: 0x0000C222
		[Parameter(Mandatory = false)]
		public string[] Members { get; set; }

		// Token: 0x06000325 RID: 805 RVA: 0x0000E02C File Offset: 0x0000C22C
		protected override void InternalProcessRecord()
		{
			try
			{
				UpdateDistributionGroupMemberCmdlet updateDistributionGroupMemberCmdlet = new UpdateDistributionGroupMemberCmdlet();
				ADObjectId executingUserId;
				base.ExchangeRunspaceConfig.TryGetExecutingUserId(out executingUserId);
				updateDistributionGroupMemberCmdlet.Authenticator = Authenticator.Create(base.CurrentOrganizationId, executingUserId);
				updateDistributionGroupMemberCmdlet.HostServerName = EOPRecipient.GetPsWsHostServerName();
				if (string.IsNullOrEmpty(this.ExternalDirectoryObjectId) && this.Identity == null)
				{
					base.ThrowTaskError(new ArgumentException(CoreStrings.MissingIdentityParameter.ToString()));
				}
				EOPRecipient.SetProperty(updateDistributionGroupMemberCmdlet, Parameters.Identity, string.IsNullOrEmpty(this.ExternalDirectoryObjectId) ? this.Identity.ToString() : this.ExternalDirectoryObjectId);
				EOPRecipient.SetProperty(updateDistributionGroupMemberCmdlet, Parameters.Members, this.Members);
				EOPRecipient.SetProperty(updateDistributionGroupMemberCmdlet, Parameters.Organization, base.Organization);
				updateDistributionGroupMemberCmdlet.Run();
				EOPRecipient.CheckForError(this, updateDistributionGroupMemberCmdlet);
			}
			catch (Exception e)
			{
				base.ThrowAndLogTaskError(e);
			}
		}
	}
}
