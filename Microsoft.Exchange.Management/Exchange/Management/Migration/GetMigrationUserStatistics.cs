using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004DF RID: 1247
	[Cmdlet("Get", "MigrationUserStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetMigrationUserStatistics : MigrationGetTaskBase<MigrationUserIdParameter, MigrationUserStatistics>
	{
		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06002B6A RID: 11114 RVA: 0x000ADA53 File Offset: 0x000ABC53
		// (set) Token: 0x06002B6B RID: 11115 RVA: 0x000ADA5B File Offset: 0x000ABC5B
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override MigrationUserIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x000ADA64 File Offset: 0x000ABC64
		// (set) Token: 0x06002B6D RID: 11117 RVA: 0x000ADA8A File Offset: 0x000ABC8A
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

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x000ADAA2 File Offset: 0x000ABCA2
		// (set) Token: 0x06002B6F RID: 11119 RVA: 0x000ADAB9 File Offset: 0x000ABCB9
		[Parameter(Mandatory = false)]
		public int? LimitSkippedItemsTo
		{
			get
			{
				return (int?)base.Fields["LimitSkippedItemsTo"];
			}
			set
			{
				base.Fields["LimitSkippedItemsTo"] = value;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06002B70 RID: 11120 RVA: 0x000ADAD1 File Offset: 0x000ABCD1
		public override string Action
		{
			get
			{
				return "GetMigrationUserStatistics";
			}
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000ADAD8 File Offset: 0x000ABCD8
		protected override IConfigDataProvider CreateSession()
		{
			MigrationLogger.Initialize();
			MigrationLogContext.Current.Source = "Get-MigrationUserStatistics";
			MigrationLogContext.Current.Organization = base.CurrentOrganizationId.OrganizationalUnit;
			IConfigDataProvider result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MigrationUserDataProvider migrationUserDataProvider = MigrationUserDataProvider.CreateDataProvider(this.Action, base.TenantGlobalCatalogSession, this.partitionMailbox, null);
				disposeGuard.Add<MigrationUserDataProvider>(migrationUserDataProvider);
				migrationUserDataProvider.LimitSkippedItemsTo = this.LimitSkippedItemsTo;
				migrationUserDataProvider.IncludeReport = this.IncludeReport;
				if (base.Diagnostic || !string.IsNullOrEmpty(base.DiagnosticArgument))
				{
					migrationUserDataProvider.EnableDiagnostics(base.DiagnosticArgument);
				}
				disposeGuard.Success();
				result = migrationUserDataProvider;
			}
			return result;
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000ADBA8 File Offset: 0x000ABDA8
		protected override void WriteResult(IConfigurable dataObject)
		{
			base.WriteResult(dataObject);
			MigrationUserDataProvider migrationUserDataProvider = base.DataSession as MigrationUserDataProvider;
			if (migrationUserDataProvider != null && migrationUserDataProvider.LastError != null)
			{
				this.WriteWarning(Strings.MigrationUserSubscriptionInaccessible(dataObject.Identity.ToString(), migrationUserDataProvider.LastError.LocalizedString));
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06002B73 RID: 11123 RVA: 0x000ADBF4 File Offset: 0x000ABDF4
		protected override QueryFilter InternalFilter
		{
			get
			{
				throw new NotSupportedException();
			}
		}
	}
}
