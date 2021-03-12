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
	// Token: 0x020004E2 RID: 1250
	public abstract class MigrationOrganizationTaskBase : DataAccessTask<ADOrganizationalUnit>
	{
		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06002B94 RID: 11156 RVA: 0x000AE225 File Offset: 0x000AC425
		// (set) Token: 0x06002B95 RID: 11157 RVA: 0x000AE23C File Offset: 0x000AC43C
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

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06002B96 RID: 11158 RVA: 0x000AE24F File Offset: 0x000AC44F
		// (set) Token: 0x06002B97 RID: 11159 RVA: 0x000AE266 File Offset: 0x000AC466
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

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x000AE279 File Offset: 0x000AC479
		internal MigrationBatchDataProvider BatchProvider
		{
			get
			{
				return this.batchDataProvider.Value;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06002B99 RID: 11161 RVA: 0x000AE286 File Offset: 0x000AC486
		// (set) Token: 0x06002B9A RID: 11162 RVA: 0x000AE28E File Offset: 0x000AC48E
		internal MigrationDataProvider DataProvider { get; set; }

		// Token: 0x06002B9B RID: 11163 RVA: 0x000AE298 File Offset: 0x000AC498
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.Organization != null)
			{
				base.CurrentOrganizationId = this.GetCurrentOrganizationId();
			}
			this.partitionMailbox = MigrationObjectTaskBase<OrganizationIdParameter>.ResolvePartitionMailbox(this.Partition, base.TenantGlobalCatalogSession, base.ServerSettings, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.ErrorLoggerDelegate(base.WriteError), base.CurrentOrganizationId == OrganizationId.ForestWideOrgId && MapiTaskHelper.IsDatacenter);
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000AE30E File Offset: 0x000AC50E
		protected override bool IsKnownException(Exception exception)
		{
			return MigrationBatchDataProvider.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000AE348 File Offset: 0x000AC548
		protected override IConfigDataProvider CreateSession()
		{
			MigrationLogger.Initialize();
			MigrationLogContext.Current.Source = base.GetType().Name;
			if (base.CurrentOrganizationId != null)
			{
				MigrationLogContext.Current.Organization = base.CurrentOrganizationId.OrganizationalUnit;
			}
			this.initialized = true;
			this.DataProvider = MigrationDataProvider.CreateProviderForMigrationMailbox(base.GetType().Name, base.TenantGlobalCatalogSession, this.partitionMailbox);
			this.batchDataProvider = new Lazy<MigrationBatchDataProvider>(() => new MigrationBatchDataProvider(this.DataProvider, null));
			MigrationADProvider migrationADProvider = (MigrationADProvider)this.DataProvider.ADProvider;
			return migrationADProvider.RecipientSession;
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000AE3EC File Offset: 0x000AC5EC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this.disposed && disposing)
				{
					this.DisposeSession();
					this.disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000AE430 File Offset: 0x000AC630
		protected override void InternalStateReset()
		{
			if (this.initialized)
			{
				this.DisposeSession();
			}
			base.InternalStateReset();
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000AE446 File Offset: 0x000AC646
		protected virtual void WriteError(LocalizedException exception)
		{
			MigrationLogger.Log(MigrationEventType.Warning, MigrationLogger.GetDiagnosticInfo(exception, null), new object[0]);
			base.WriteError(exception, (ErrorCategory)1000, null);
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x000AE468 File Offset: 0x000AC668
		private OrganizationId GetCurrentOrganizationId()
		{
			OrganizationId organizationId;
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 236, "GetCurrentOrganizationId", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Migration\\MigrationOrganizationTaskBase.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				tenantOrTopologyConfigurationSession.UseGlobalCatalog = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				organizationId = adorganizationalUnit.OrganizationId;
			}
			else
			{
				organizationId = base.CurrentOrganizationId;
			}
			if (organizationId != null && OrganizationId.ForestWideOrgId.Equals(organizationId) && MapiTaskHelper.IsDatacenter)
			{
				organizationId = null;
			}
			return organizationId;
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000AE530 File Offset: 0x000AC730
		private void DisposeSession()
		{
			IDisposable disposable = base.DataSession as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			if (this.DataProvider != null)
			{
				this.DataProvider.Dispose();
				this.DataProvider = null;
			}
			if (this.batchDataProvider.IsValueCreated)
			{
				this.batchDataProvider.Value.Dispose();
			}
			MigrationLogger.Close();
		}

		// Token: 0x0400201F RID: 8223
		protected ADUser partitionMailbox;

		// Token: 0x04002020 RID: 8224
		private bool disposed;

		// Token: 0x04002021 RID: 8225
		private bool initialized;

		// Token: 0x04002022 RID: 8226
		private Lazy<MigrationBatchDataProvider> batchDataProvider;
	}
}
