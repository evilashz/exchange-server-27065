using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000CCC RID: 3276
	[Cmdlet("Remove", "PublicFolderClientPermission", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemovePublicFolderClientPermission : RemoveMailboxFolderPermission
	{
		// Token: 0x17002744 RID: 10052
		// (get) Token: 0x06007E47 RID: 32327 RVA: 0x0020425A File Offset: 0x0020245A
		// (set) Token: 0x06007E48 RID: 32328 RVA: 0x00204271 File Offset: 0x00202471
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

		// Token: 0x06007E49 RID: 32329 RVA: 0x00204284 File Offset: 0x00202484
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

		// Token: 0x06007E4A RID: 32330 RVA: 0x0020431E File Offset: 0x0020251E
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			PublicFolderPermissionTaskHelper.SyncPublicFolder(this.ConfigurationSession, this.Identity.PublicFolderId.StoreObjectId);
		}

		// Token: 0x06007E4B RID: 32331 RVA: 0x00204341 File Offset: 0x00202541
		protected override bool IsKnownException(Exception exception)
		{
			return exception is FormatException || exception is StoragePermanentException || exception is StorageTransientException || base.IsKnownException(exception);
		}
	}
}
