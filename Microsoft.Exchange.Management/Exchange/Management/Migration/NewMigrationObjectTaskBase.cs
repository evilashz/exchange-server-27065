using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004E6 RID: 1254
	public abstract class NewMigrationObjectTaskBase<TDataObject> : NewTenantADTaskBase<TDataObject> where TDataObject : IConfigurable, new()
	{
		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06002BD0 RID: 11216 RVA: 0x000AEF31 File Offset: 0x000AD131
		// (set) Token: 0x06002BD1 RID: 11217 RVA: 0x000AEF48 File Offset: 0x000AD148
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

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06002BD2 RID: 11218 RVA: 0x000AEF5B File Offset: 0x000AD15B
		// (set) Token: 0x06002BD3 RID: 11219 RVA: 0x000AEF72 File Offset: 0x000AD172
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

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000AEF88 File Offset: 0x000AD188
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.Organization != null)
			{
				base.CurrentOrganizationId = this.ResolveCurrentOrganization();
			}
			this.partitionMailbox = MigrationObjectTaskBase<MigrationBatchIdParameter>.ResolvePartitionMailbox(this.Partition, base.TenantGlobalCatalogSession, base.ServerSettings, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.ErrorLoggerDelegate(base.WriteError), base.CurrentOrganizationId == OrganizationId.ForestWideOrgId && MapiTaskHelper.IsDatacenter);
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x000AF000 File Offset: 0x000AD200
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 109, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Migration\\NewMigrationObjectTaskBase.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				return adorganizationalUnit.OrganizationId;
			}
			return base.ResolveCurrentOrganization();
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000AF0A9 File Offset: 0x000AD2A9
		protected void WriteError(LocalizedException exception)
		{
			MigrationLogger.Log(MigrationEventType.Warning, MigrationLogger.GetDiagnosticInfo(exception, null), new object[0]);
			base.WriteError(exception, (ErrorCategory)1000, null);
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x000AF0CB File Offset: 0x000AD2CB
		protected bool IsFieldSet(string fieldName)
		{
			return base.Fields.IsChanged(fieldName) || base.Fields.IsModified(fieldName);
		}

		// Token: 0x0400202B RID: 8235
		protected ADUser partitionMailbox;
	}
}
