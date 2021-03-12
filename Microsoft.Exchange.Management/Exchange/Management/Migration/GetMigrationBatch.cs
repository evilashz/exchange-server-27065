using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004DA RID: 1242
	[Cmdlet("Get", "MigrationBatch", DefaultParameterSetName = "Identity")]
	public sealed class GetMigrationBatch : MigrationGetTaskBase<MigrationBatchIdParameter, MigrationBatch>
	{
		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x000AD27A File Offset: 0x000AB47A
		// (set) Token: 0x06002B3A RID: 11066 RVA: 0x000AD291 File Offset: 0x000AB491
		[Parameter(Mandatory = false, ParameterSetName = "BatchesFromEndpoint")]
		public MigrationEndpointIdParameter Endpoint
		{
			get
			{
				return (MigrationEndpointIdParameter)base.Fields["Endpoint"];
			}
			set
			{
				base.Fields["Endpoint"] = value;
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x000AD2A4 File Offset: 0x000AB4A4
		public override string Action
		{
			get
			{
				return "GetMigrationBatch";
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x000AD2AB File Offset: 0x000AB4AB
		// (set) Token: 0x06002B3D RID: 11069 RVA: 0x000AD2C2 File Offset: 0x000AB4C2
		[Parameter(Mandatory = false)]
		public MigrationBatchStatus? Status
		{
			get
			{
				return (MigrationBatchStatus?)base.Fields["Status"];
			}
			set
			{
				base.Fields["Status"] = value;
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x000AD2DA File Offset: 0x000AB4DA
		// (set) Token: 0x06002B3F RID: 11071 RVA: 0x000AD300 File Offset: 0x000AB500
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeReport
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeReport"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IncludeReport"] = value;
			}
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x000AD318 File Offset: 0x000AB518
		protected override void WriteResult(IConfigurable dataObject)
		{
			MigrationBatch migrationBatch = dataObject as MigrationBatch;
			if (migrationBatch != null && migrationBatch.Status == MigrationBatchStatus.Corrupted)
			{
				this.WriteWarning(Strings.MigrationBatchIsCorrupt(migrationBatch.Identity.ToString()));
			}
			base.WriteResult(dataObject);
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x000AD358 File Offset: 0x000AB558
		protected override IConfigDataProvider CreateSession()
		{
			MigrationLogger.Initialize();
			MigrationLogContext.Current.Source = "Get-MigrationBatch";
			MigrationLogContext.Current.Organization = base.CurrentOrganizationId.OrganizationalUnit;
			IConfigDataProvider result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MigrationBatchDataProvider migrationBatchDataProvider = MigrationBatchDataProvider.CreateDataProvider(this.Action, base.TenantGlobalCatalogSession, this.Status, this.partitionMailbox);
				disposeGuard.Add<MigrationBatchDataProvider>(migrationBatchDataProvider);
				if (base.Diagnostic || !string.IsNullOrEmpty(base.DiagnosticArgument))
				{
					migrationBatchDataProvider.EnableDiagnostics(base.DiagnosticArgument);
				}
				if (this.IncludeReport)
				{
					migrationBatchDataProvider.IncludeReport = true;
				}
				disposeGuard.Success();
				result = migrationBatchDataProvider;
			}
			return result;
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x000AD424 File Offset: 0x000AB624
		protected override bool IsKnownException(Exception exception)
		{
			return MigrationBatchDataProvider.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x000AD438 File Offset: 0x000AB638
		protected override void InternalProcessRecord()
		{
			MigrationBatchDataProvider migrationBatchDataProvider = (MigrationBatchDataProvider)base.DataSession;
			if (migrationBatchDataProvider.MigrationSession.HasJobs)
			{
				MigrationObjectTaskBase<MigrationBatchIdParameter>.RegisterMigrationBatch(this, migrationBatchDataProvider.MailboxSession, base.CurrentOrganizationId, false, false);
			}
			base.InternalProcessRecord();
		}

		// Token: 0x04002013 RID: 8211
		private const string ParameterSetNameBatchesFromEndpoint = "BatchesFromEndpoint";
	}
}
