using System;
using System.IO;
using System.Management.Automation;
using System.Web;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004D8 RID: 1240
	[Cmdlet("Export", "MigrationReport", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ExportMigrationReport : ObjectActionTenantADTask<MigrationReportIdParameter, MigrationReport>
	{
		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06002B0F RID: 11023 RVA: 0x000ACBC0 File Offset: 0x000AADC0
		// (set) Token: 0x06002B10 RID: 11024 RVA: 0x000ACBC8 File Offset: 0x000AADC8
		[Parameter(Mandatory = true, ParameterSetName = "StreamBased")]
		public Stream CsvStream { get; set; }

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06002B11 RID: 11025 RVA: 0x000ACBD1 File Offset: 0x000AADD1
		// (set) Token: 0x06002B12 RID: 11026 RVA: 0x000ACBF2 File Offset: 0x000AADF2
		[Parameter(Mandatory = true, ParameterSetName = "Paged")]
		public int StartingRowIndex
		{
			get
			{
				return (int)(base.Fields["StartingRowIndex"] ?? 0);
			}
			set
			{
				base.Fields["StartingRowIndex"] = value;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06002B13 RID: 11027 RVA: 0x000ACC0A File Offset: 0x000AAE0A
		// (set) Token: 0x06002B14 RID: 11028 RVA: 0x000ACC2B File Offset: 0x000AAE2B
		[Parameter(Mandatory = true, ParameterSetName = "Paged")]
		public int RowCount
		{
			get
			{
				return (int)(base.Fields["RowCount"] ?? 0);
			}
			set
			{
				base.Fields["RowCount"] = value;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06002B15 RID: 11029 RVA: 0x000ACC43 File Offset: 0x000AAE43
		// (set) Token: 0x06002B16 RID: 11030 RVA: 0x000ACC4B File Offset: 0x000AAE4B
		[Parameter(Mandatory = true, ParameterSetName = "StreamBased", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "Paged", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override MigrationReportIdParameter Identity
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

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06002B17 RID: 11031 RVA: 0x000ACC54 File Offset: 0x000AAE54
		// (set) Token: 0x06002B18 RID: 11032 RVA: 0x000ACC6B File Offset: 0x000AAE6B
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x000ACC7E File Offset: 0x000AAE7E
		// (set) Token: 0x06002B1A RID: 11034 RVA: 0x000ACC95 File Offset: 0x000AAE95
		[Parameter(Mandatory = false)]
		public MailboxIdParameter Partition
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Partition"];
			}
			set
			{
				base.Fields["Partition"] = value;
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x000ACCA8 File Offset: 0x000AAEA8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageExportMigrationReport(this.TenantName);
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06002B1C RID: 11036 RVA: 0x000ACCB5 File Offset: 0x000AAEB5
		private string TenantName
		{
			get
			{
				if (!(base.CurrentOrganizationId != null) || base.CurrentOrganizationId.OrganizationalUnit == null)
				{
					return string.Empty;
				}
				return base.CurrentOrganizationId.OrganizationalUnit.Name;
			}
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000ACCE8 File Offset: 0x000AAEE8
		protected override IConfigDataProvider CreateSession()
		{
			MigrationLogger.Initialize();
			MigrationLogContext.Current.Source = "Export-MigrationReport";
			MigrationLogContext.Current.Organization = base.CurrentOrganizationId.OrganizationalUnit;
			return MigrationReportDataProvider.CreateDataProvider("ExportMigrationReport", base.TenantGlobalCatalogSession, this.CsvStream, this.StartingRowIndex, this.RowCount, this.partitionMailbox, object.Equals(base.ExecutingUserOrganizationId, base.CurrentOrganizationId));
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000ACD57 File Offset: 0x000AAF57
		protected override void InternalProcessRecord()
		{
			base.WriteObject(this.DataObject);
			base.InternalProcessRecord();
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x000ACD6C File Offset: 0x000AAF6C
		protected override void InternalBeginProcessing()
		{
			if (this.Organization != null)
			{
				base.CurrentOrganizationId = this.ResolveCurrentOrganization();
			}
			this.partitionMailbox = MigrationObjectTaskBase<MigrationReportIdParameter>.ResolvePartitionMailbox(this.Partition, base.TenantGlobalCatalogSession, base.ServerSettings, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.ErrorLoggerDelegate(base.WriteError), base.CurrentOrganizationId == OrganizationId.ForestWideOrgId && MapiTaskHelper.IsDatacenter);
			base.InternalBeginProcessing();
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x000ACDE2 File Offset: 0x000AAFE2
		protected override bool IsKnownException(Exception exception)
		{
			return MigrationBatchDataProvider.IsKnownException(exception) || exception is HttpException || base.IsKnownException(exception);
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x000ACDFD File Offset: 0x000AAFFD
		protected override void InternalStateReset()
		{
			this.DisposeSession();
			base.InternalStateReset();
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x000ACE0C File Offset: 0x000AB00C
		protected override void InternalValidate()
		{
			if ((base.Fields.IsChanged("RowCount") || base.Fields.IsModified("RowCount")) && (this.RowCount < MigrationConstraints.ExportMigrationReportRowCountConstraint.MinimumValue || this.RowCount > MigrationConstraints.ExportMigrationReportRowCountConstraint.MaximumValue))
			{
				this.WriteError(new MigrationPermanentException(Strings.ExportMigrationBatchRowCountOutOfBoundsException(this.RowCount, MigrationConstraints.ExportMigrationReportRowCountConstraint.MinimumValue, MigrationConstraints.ExportMigrationReportRowCountConstraint.MaximumValue)));
			}
			if ((base.Fields.IsChanged("StartingRowIndex") || base.Fields.IsModified("StartingRowIndex")) && this.StartingRowIndex < 0)
			{
				this.WriteError(new MigrationPermanentException(Strings.ExportMigrationBatchStartingRowIndexOutOfBoundException(this.StartingRowIndex)));
			}
			base.InternalValidate();
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x000ACED4 File Offset: 0x000AB0D4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this.disposed)
				{
					if (disposing)
					{
						this.DisposeSession();
					}
					this.disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x000ACF14 File Offset: 0x000AB114
		private void WriteError(LocalizedException exception)
		{
			MigrationLogger.Log(MigrationEventType.Warning, MigrationLogger.GetDiagnosticInfo(exception, null), new object[0]);
			base.WriteError(exception, (ErrorCategory)1000, null);
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x000ACF38 File Offset: 0x000AB138
		private OrganizationId ResolveCurrentOrganization()
		{
			ADObjectId rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerIdForLocalForest, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 362, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Migration\\ExportMigrationReport.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
			return adorganizationalUnit.OrganizationId;
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x000ACFD3 File Offset: 0x000AB1D3
		private void DisposeSession()
		{
			if (base.DataSession is IDisposable)
			{
				MigrationLogger.Close();
				((IDisposable)base.DataSession).Dispose();
			}
		}

		// Token: 0x04002009 RID: 8201
		private const string ParameterSetStream = "StreamBased";

		// Token: 0x0400200A RID: 8202
		private const string ParameterSetPaged = "Paged";

		// Token: 0x0400200B RID: 8203
		private const string ParameterStartingRowIndex = "StartingRowIndex";

		// Token: 0x0400200C RID: 8204
		private const string ParameterRowCount = "RowCount";

		// Token: 0x0400200D RID: 8205
		private const string Action = "ExportMigrationReport";

		// Token: 0x0400200E RID: 8206
		private bool disposed;

		// Token: 0x0400200F RID: 8207
		private ADUser partitionMailbox;
	}
}
