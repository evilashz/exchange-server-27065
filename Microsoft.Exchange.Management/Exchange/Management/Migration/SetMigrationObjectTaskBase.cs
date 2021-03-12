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
	// Token: 0x020004EB RID: 1259
	public abstract class SetMigrationObjectTaskBase<TIdentity, TPublicObject, TDataObject> : SetTenantADTaskBase<TIdentity, TPublicObject, TDataObject> where TIdentity : IIdentityParameter, new() where TPublicObject : IConfigurable, new() where TDataObject : IConfigurable, new()
	{
		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06002C68 RID: 11368 RVA: 0x000B1A67 File Offset: 0x000AFC67
		// (set) Token: 0x06002C69 RID: 11369 RVA: 0x000B1A7E File Offset: 0x000AFC7E
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

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06002C6A RID: 11370 RVA: 0x000B1A91 File Offset: 0x000AFC91
		// (set) Token: 0x06002C6B RID: 11371 RVA: 0x000B1AA8 File Offset: 0x000AFCA8
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

		// Token: 0x06002C6C RID: 11372 RVA: 0x000B1ABC File Offset: 0x000AFCBC
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.Organization != null)
			{
				base.CurrentOrganizationId = this.ResolveCurrentOrganization();
			}
			this.partitionMailbox = MigrationObjectTaskBase<TIdentity>.ResolvePartitionMailbox(this.Partition, base.TenantGlobalCatalogSession, base.ServerSettings, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.ErrorLoggerDelegate(base.WriteError), base.CurrentOrganizationId == OrganizationId.ForestWideOrgId && MapiTaskHelper.IsDatacenter);
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x000B1B34 File Offset: 0x000AFD34
		protected virtual OrganizationId ResolveCurrentOrganization()
		{
			ADObjectId rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerIdForLocalForest, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, sessionSettings, 119, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Migration\\SetMigrationObjectTaskBase.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
			return adorganizationalUnit.OrganizationId;
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x000B1BC0 File Offset: 0x000AFDC0
		protected bool IsFieldSet(string fieldName)
		{
			return base.Fields.IsChanged(fieldName) || base.Fields.IsModified(fieldName);
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x000B1BDE File Offset: 0x000AFDDE
		protected void WriteError(LocalizedException exception)
		{
			MigrationLogger.Log(MigrationEventType.Warning, MigrationLogger.GetDiagnosticInfo(exception, null), new object[0]);
			base.WriteError(exception, (ErrorCategory)1000, null);
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x000B1C00 File Offset: 0x000AFE00
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

		// Token: 0x06002C71 RID: 11377 RVA: 0x000B1C40 File Offset: 0x000AFE40
		protected virtual void DisposeSession()
		{
			if (base.DataSession is IDisposable)
			{
				MigrationLogger.Close();
				((IDisposable)base.DataSession).Dispose();
			}
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x000B1C64 File Offset: 0x000AFE64
		protected override bool IsKnownException(Exception exception)
		{
			return MigrationBatchDataProvider.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x04002053 RID: 8275
		private const string ParameterNameOrganization = "Organization";

		// Token: 0x04002054 RID: 8276
		private bool disposed;

		// Token: 0x04002055 RID: 8277
		protected ADUser partitionMailbox;
	}
}
