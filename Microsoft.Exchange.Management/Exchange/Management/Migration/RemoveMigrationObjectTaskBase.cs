using System;
using System.Management.Automation;
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
	// Token: 0x020004E9 RID: 1257
	public abstract class RemoveMigrationObjectTaskBase<TIdentityParameter, TDataObject> : ObjectActionTenantADTask<TIdentityParameter, TDataObject> where TIdentityParameter : IIdentityParameter, new() where TDataObject : IConfigurable, new()
	{
		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06002C51 RID: 11345 RVA: 0x000B1624 File Offset: 0x000AF824
		// (set) Token: 0x06002C52 RID: 11346 RVA: 0x000B163B File Offset: 0x000AF83B
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

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06002C53 RID: 11347 RVA: 0x000B164E File Offset: 0x000AF84E
		// (set) Token: 0x06002C54 RID: 11348 RVA: 0x000B1665 File Offset: 0x000AF865
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

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06002C55 RID: 11349
		protected abstract string Action { get; }

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06002C56 RID: 11350 RVA: 0x000B1678 File Offset: 0x000AF878
		protected string TenantName
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

		// Token: 0x06002C57 RID: 11351 RVA: 0x000B16AB File Offset: 0x000AF8AB
		protected override void InternalStateReset()
		{
			this.DisposeSession();
			base.InternalStateReset();
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x000B16B9 File Offset: 0x000AF8B9
		protected override bool IsKnownException(Exception exception)
		{
			return MigrationBatchDataProvider.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x000B16CC File Offset: 0x000AF8CC
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

		// Token: 0x06002C5A RID: 11354 RVA: 0x000B170C File Offset: 0x000AF90C
		protected void WriteError(LocalizedException exception)
		{
			MigrationLogger.Log(MigrationEventType.Warning, MigrationLogger.GetDiagnosticInfo(exception, null), new object[0]);
			base.WriteError(exception, (ErrorCategory)1000, null);
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x000B1730 File Offset: 0x000AF930
		protected OrganizationId ResolveCurrentOrganization()
		{
			ADObjectId rootOrgContainerId = ADSystemConfigurationSession.GetRootOrgContainerId(base.DomainController, string.IsNullOrEmpty(base.DomainController) ? null : base.NetCredential);
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 179, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Migration\\RemoveMigrationObjectTaskBase.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
			return adorganizationalUnit.OrganizationId;
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000B17F4 File Offset: 0x000AF9F4
		protected override void InternalBeginProcessing()
		{
			if (this.Organization != null)
			{
				base.CurrentOrganizationId = this.ResolveCurrentOrganization();
			}
			this.partitionMailbox = MigrationObjectTaskBase<TIdentityParameter>.ResolvePartitionMailbox(this.Partition, base.TenantGlobalCatalogSession, base.ServerSettings, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.ErrorLoggerDelegate(base.WriteError), base.CurrentOrganizationId == OrganizationId.ForestWideOrgId && MapiTaskHelper.IsDatacenter);
			base.InternalBeginProcessing();
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x000B186C File Offset: 0x000AFA6C
		protected override void InternalProcessRecord()
		{
			XsoStoreDataProviderBase xsoStoreDataProviderBase = (XsoStoreDataProviderBase)base.DataSession;
			xsoStoreDataProviderBase.Delete(this.DataObject);
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x000B1896 File Offset: 0x000AFA96
		private void DisposeSession()
		{
			if (base.DataSession is IDisposable)
			{
				MigrationLogger.Close();
				((IDisposable)base.DataSession).Dispose();
			}
		}

		// Token: 0x04002051 RID: 8273
		private bool disposed;

		// Token: 0x04002052 RID: 8274
		protected ADUser partitionMailbox;
	}
}
