using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004EA RID: 1258
	[Cmdlet("Remove", "MigrationUser", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveMigrationUser : RemoveMigrationObjectTaskBase<MigrationUserIdParameter, MigrationUser>
	{
		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06002C60 RID: 11360 RVA: 0x000B18C2 File Offset: 0x000AFAC2
		// (set) Token: 0x06002C61 RID: 11361 RVA: 0x000B18E8 File Offset: 0x000AFAE8
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06002C62 RID: 11362 RVA: 0x000B1900 File Offset: 0x000AFB00
		protected override string Action
		{
			get
			{
				return "Remove-MigrationUser";
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06002C63 RID: 11363 RVA: 0x000B1907 File Offset: 0x000AFB07
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveMigrationUser(this.DataObject.Identity.ToString());
			}
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x000B1920 File Offset: 0x000AFB20
		protected override IConfigDataProvider CreateSession()
		{
			MigrationLogger.Initialize();
			MigrationLogContext.Current.Source = "Remove-MigrationUser";
			MigrationLogContext.Current.Organization = base.CurrentOrganizationId.OrganizationalUnit;
			return MigrationUserDataProvider.CreateDataProvider(this.Action, base.TenantGlobalCatalogSession, this.partitionMailbox, base.ExecutingUserIdentityName);
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x000B1974 File Offset: 0x000AFB74
		protected override void InternalValidate()
		{
			base.InternalValidate();
			MigrationUserDataProvider migrationUserDataProvider = (MigrationUserDataProvider)base.DataSession;
			migrationUserDataProvider.ForceRemoval = this.Force;
			MigrationJob job = migrationUserDataProvider.GetJob(this.DataObject.BatchId);
			if (job != null && job.MigrationType == MigrationType.PublicFolder)
			{
				base.WriteError(new CannotRemoveMigrationUserFromPublicFolderBatchException());
			}
			if (this.Force)
			{
				return;
			}
			if (job == null)
			{
				base.WriteError(new CannotRemoveUserWithoutBatchException(this.DataObject.Identity.ToString()));
			}
			LocalizedString? localizedString;
			if (!job.IsPAW && !job.SupportsRemovingUsers(out localizedString))
			{
				base.WriteError(new CannotRemoveMigrationUserOnCurrentStateException(this.Identity.ToString(), job.JobName));
			}
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x000B1A2C File Offset: 0x000AFC2C
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			MigrationUserDataProvider migrationUserDataProvider = (MigrationUserDataProvider)base.DataSession;
			MigrationObjectTaskBase<MigrationBatchIdParameter>.RegisterMigrationBatch(this, migrationUserDataProvider.MailboxSession, base.CurrentOrganizationId, false, false);
		}
	}
}
