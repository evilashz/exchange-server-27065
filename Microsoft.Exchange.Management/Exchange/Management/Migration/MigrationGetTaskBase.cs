using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004D9 RID: 1241
	public abstract class MigrationGetTaskBase<TIdentityParameter, TDataObject> : GetTenantADObjectWithIdentityTaskBase<TIdentityParameter, TDataObject> where TIdentityParameter : IIdentityParameter, new() where TDataObject : IConfigurable, new()
	{
		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06002B28 RID: 11048
		public abstract string Action { get; }

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06002B29 RID: 11049 RVA: 0x000ACFFF File Offset: 0x000AB1FF
		// (set) Token: 0x06002B2A RID: 11050 RVA: 0x000AD016 File Offset: 0x000AB216
		[Parameter(Mandatory = false)]
		public virtual OrganizationIdParameter Organization
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

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06002B2B RID: 11051 RVA: 0x000AD029 File Offset: 0x000AB229
		// (set) Token: 0x06002B2C RID: 11052 RVA: 0x000AD040 File Offset: 0x000AB240
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

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06002B2D RID: 11053 RVA: 0x000AD053 File Offset: 0x000AB253
		// (set) Token: 0x06002B2E RID: 11054 RVA: 0x000AD079 File Offset: 0x000AB279
		[Parameter(Mandatory = false)]
		public SwitchParameter Diagnostic
		{
			get
			{
				return (SwitchParameter)(base.Fields["Diagnostic"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Diagnostic"] = value;
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x000AD091 File Offset: 0x000AB291
		// (set) Token: 0x06002B30 RID: 11056 RVA: 0x000AD0A8 File Offset: 0x000AB2A8
		[Parameter(Mandatory = false)]
		public string DiagnosticArgument
		{
			get
			{
				return (string)base.Fields["DiagnosticArgument"];
			}
			set
			{
				base.Fields["DiagnosticArgument"] = value;
			}
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x000AD0BB File Offset: 0x000AB2BB
		protected override bool IsKnownException(Exception exception)
		{
			return MigrationBatchDataProvider.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x000AD0D0 File Offset: 0x000AB2D0
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			this.partitionMailbox = MigrationObjectTaskBase<TIdentityParameter>.ResolvePartitionMailbox(this.Partition, base.TenantGlobalCatalogSession, base.ServerSettings, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.ErrorLoggerDelegate(base.WriteError), base.CurrentOrganizationId == OrganizationId.ForestWideOrgId && MapiTaskHelper.IsDatacenter);
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x000AD132 File Offset: 0x000AB332
		protected override void InternalStateReset()
		{
			this.DisposeSession();
			base.InternalStateReset();
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x000AD140 File Offset: 0x000AB340
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

		// Token: 0x06002B35 RID: 11061 RVA: 0x000AD180 File Offset: 0x000AB380
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 204, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Migration\\MigrationGetTaskBase.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				return adorganizationalUnit.OrganizationId;
			}
			return base.ResolveCurrentOrganization();
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x000AD22C File Offset: 0x000AB42C
		protected void WriteError(LocalizedException exception)
		{
			MigrationLogger.Log(MigrationEventType.Warning, MigrationLogger.GetDiagnosticInfo(exception, null), new object[0]);
			base.WriteError(exception, (ErrorCategory)1000, null);
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x000AD24E File Offset: 0x000AB44E
		private void DisposeSession()
		{
			if (base.DataSession is IDisposable)
			{
				MigrationLogger.Close();
				((IDisposable)base.DataSession).Dispose();
			}
		}

		// Token: 0x04002011 RID: 8209
		private bool disposed;

		// Token: 0x04002012 RID: 8210
		protected ADUser partitionMailbox;
	}
}
