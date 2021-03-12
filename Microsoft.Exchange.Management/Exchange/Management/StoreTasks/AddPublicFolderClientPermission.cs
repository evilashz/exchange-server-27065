using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000CC6 RID: 3270
	[Cmdlet("Add", "PublicFolderClientPermission", SupportsShouldProcess = true)]
	public sealed class AddPublicFolderClientPermission : AddMailboxFolderPermission
	{
		// Token: 0x17002731 RID: 10033
		// (get) Token: 0x06007E06 RID: 32262 RVA: 0x00203698 File Offset: 0x00201898
		// (set) Token: 0x06007E07 RID: 32263 RVA: 0x002036AF File Offset: 0x002018AF
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public new PublicFolderIdParameter Identity
		{
			get
			{
				return (PublicFolderIdParameter)base.Fields["PublicFolderId"];
			}
			set
			{
				base.Fields["PublicFolderId"] = value;
			}
		}

		// Token: 0x17002732 RID: 10034
		// (get) Token: 0x06007E08 RID: 32264 RVA: 0x002036C2 File Offset: 0x002018C2
		protected override ObjectId ResolvedObjectId
		{
			get
			{
				return this.Identity.PublicFolderId;
			}
		}

		// Token: 0x17002733 RID: 10035
		// (get) Token: 0x06007E09 RID: 32265 RVA: 0x002036CF File Offset: 0x002018CF
		protected override bool IsPublicFolderIdentity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007E0A RID: 32266 RVA: 0x002036D4 File Offset: 0x002018D4
		protected override ADUser PrepareMailboxUser()
		{
			OrganizationIdParameter organization = null;
			if (MapiTaskHelper.IsDatacenter)
			{
				organization = MapiTaskHelper.ResolveTargetOrganizationIdParameter(null, this.Identity, base.CurrentOrganizationId, new Task.ErrorLoggerDelegate(base.ThrowTerminatingError), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			base.CurrentOrganizationId = MapiTaskHelper.ResolveTargetOrganization(base.DomainController, organization, ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
			base.Identity = PublicFolderPermissionTaskHelper.GetMailboxFolderIdParameterForPublicFolder(this.ConfigurationSession, this.Identity, Guid.Empty, null, base.CurrentOrganizationId, new Task.ErrorLoggerDelegate(base.WriteError));
			return base.PrepareMailboxUser();
		}

		// Token: 0x06007E0B RID: 32267 RVA: 0x0020376E File Offset: 0x0020196E
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			PublicFolderPermissionTaskHelper.SyncPublicFolder(this.ConfigurationSession, this.Identity.PublicFolderId.StoreObjectId);
		}

		// Token: 0x06007E0C RID: 32268 RVA: 0x00203791 File Offset: 0x00201991
		protected override bool IsKnownException(Exception exception)
		{
			return exception is FormatException || exception is StoragePermanentException || exception is StorageTransientException || base.IsKnownException(exception);
		}
	}
}
