using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004DB RID: 1243
	public abstract class MigrationSessionGetTaskBase<TIdentityParameter, TDataObject> : MigrationGetTaskBase<TIdentityParameter, TDataObject> where TIdentityParameter : IIdentityParameter, new() where TDataObject : IConfigurable, new()
	{
		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06002B45 RID: 11077 RVA: 0x000AD480 File Offset: 0x000AB680
		// (set) Token: 0x06002B46 RID: 11078 RVA: 0x000AD488 File Offset: 0x000AB688
		protected new OrganizationIdParameter Organization
		{
			get
			{
				return base.Organization;
			}
			set
			{
				base.Organization = value;
			}
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x000AD494 File Offset: 0x000AB694
		protected override void InternalStateReset()
		{
			base.CurrentOrganizationId = this.ResolveCurrentOrganization();
			this.partitionMailbox = MigrationObjectTaskBase<TIdentityParameter>.ResolvePartitionMailbox(base.Partition, base.TenantGlobalCatalogSession, base.ServerSettings, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.ErrorLoggerDelegate(base.WriteError), base.CurrentOrganizationId == OrganizationId.ForestWideOrgId && MapiTaskHelper.IsDatacenter);
			base.InternalStateReset();
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x000AD504 File Offset: 0x000AB704
		protected override IConfigDataProvider CreateSession()
		{
			MigrationLogger.Initialize();
			MigrationLogContext.Current.Source = "Get-MigrationSession";
			MigrationLogContext.Current.Organization = base.CurrentOrganizationId.OrganizationalUnit;
			MigrationSessionDataProvider migrationSessionDataProvider = MigrationSessionDataProvider.CreateDataProvider(this.Action, base.TenantGlobalCatalogSession, this.partitionMailbox);
			if (base.Diagnostic || !string.IsNullOrEmpty(base.DiagnosticArgument))
			{
				migrationSessionDataProvider.EnableDiagnostics(base.DiagnosticArgument);
			}
			return migrationSessionDataProvider;
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x000AD579 File Offset: 0x000AB779
		protected override bool IsKnownException(Exception exception)
		{
			return MigrationSessionDataProvider.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x000AD58C File Offset: 0x000AB78C
		protected override void InternalProcessRecord()
		{
			MigrationSessionDataProvider migrationSessionDataProvider = (MigrationSessionDataProvider)base.DataSession;
			if (migrationSessionDataProvider.MigrationSession.HasJobs)
			{
				MigrationObjectTaskBase<MigrationBatchIdParameter>.RegisterMigrationBatch(this, migrationSessionDataProvider.MailboxSession, base.CurrentOrganizationId, false, false);
			}
			base.InternalProcessRecord();
		}
	}
}
