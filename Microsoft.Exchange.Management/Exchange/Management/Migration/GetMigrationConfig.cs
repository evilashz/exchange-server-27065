using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004DC RID: 1244
	[Cmdlet("Get", "MigrationConfig")]
	public sealed class GetMigrationConfig : MigrationSessionGetTaskBase<MigrationConfigIdParameter, MigrationConfig>
	{
		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06002B4C RID: 11084 RVA: 0x000AD5D4 File Offset: 0x000AB7D4
		// (set) Token: 0x06002B4D RID: 11085 RVA: 0x000AD5DC File Offset: 0x000AB7DC
		private new SwitchParameter Diagnostic
		{
			get
			{
				return base.Diagnostic;
			}
			set
			{
				base.Diagnostic = value;
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06002B4E RID: 11086 RVA: 0x000AD5E5 File Offset: 0x000AB7E5
		// (set) Token: 0x06002B4F RID: 11087 RVA: 0x000AD5ED File Offset: 0x000AB7ED
		private new string DiagnosticArgument
		{
			get
			{
				return base.DiagnosticArgument;
			}
			set
			{
				base.DiagnosticArgument = value;
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06002B50 RID: 11088 RVA: 0x000AD5F6 File Offset: 0x000AB7F6
		public override string Action
		{
			get
			{
				return "GetMigrationConfig";
			}
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000AD600 File Offset: 0x000AB800
		protected override IConfigDataProvider CreateSession()
		{
			IConfigDataProvider result = base.CreateSession();
			MigrationLogContext.Current.Source = "Get-MigrationConfig";
			return result;
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000AD624 File Offset: 0x000AB824
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (!MapiTaskHelper.IsDatacenter && this.Identity == null)
			{
				this.Identity = new MigrationConfigIdParameter();
				this.Identity.Id = new MigrationConfigId(OrganizationId.ForestWideOrgId);
				return OrganizationId.ForestWideOrgId;
			}
			if (this.Identity == null)
			{
				this.Identity = new MigrationConfigIdParameter();
			}
			if (this.Identity.Id != null)
			{
				return this.Identity.Id.OrganizationId;
			}
			base.Organization = this.Identity.OrganizationIdentifier;
			OrganizationId organizationId = base.ResolveCurrentOrganization();
			this.Identity.Id = new MigrationConfigId(organizationId);
			return organizationId;
		}
	}
}
