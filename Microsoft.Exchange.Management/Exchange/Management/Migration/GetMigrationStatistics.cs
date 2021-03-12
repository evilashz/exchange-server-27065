using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004DD RID: 1245
	[Cmdlet("Get", "MigrationStatistics")]
	public sealed class GetMigrationStatistics : MigrationSessionGetTaskBase<MigrationStatisticsIdParameter, MigrationStatistics>
	{
		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06002B54 RID: 11092 RVA: 0x000AD6C9 File Offset: 0x000AB8C9
		public override string Action
		{
			get
			{
				return "GetMigrationStatistics";
			}
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000AD6D0 File Offset: 0x000AB8D0
		protected override IConfigDataProvider CreateSession()
		{
			IConfigDataProvider result = base.CreateSession();
			MigrationLogContext.Current.Source = "Get-MigrationStatistics";
			return result;
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000AD6F4 File Offset: 0x000AB8F4
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (!MapiTaskHelper.IsDatacenter && this.Identity == null)
			{
				this.Identity = new MigrationStatisticsIdParameter();
				this.Identity.Id = new MigrationStatisticsId(OrganizationId.ForestWideOrgId);
				return OrganizationId.ForestWideOrgId;
			}
			if (this.Identity == null)
			{
				this.Identity = new MigrationStatisticsIdParameter();
			}
			if (this.Identity.Id != null)
			{
				return this.Identity.Id.OrganizationId;
			}
			base.Organization = this.Identity.OrganizationIdentifier;
			OrganizationId organizationId = base.ResolveCurrentOrganization();
			this.Identity.Id = new MigrationStatisticsId(organizationId);
			return organizationId;
		}
	}
}
