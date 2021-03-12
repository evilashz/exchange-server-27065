using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x020004F4 RID: 1268
	[Cmdlet("Remove", "MigrationEndpoint", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveMigrationEndpoint : RemoveMigrationObjectTaskBase<MigrationEndpointIdParameter, MigrationEndpoint>
	{
		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06002D56 RID: 11606 RVA: 0x000B59ED File Offset: 0x000B3BED
		protected override string Action
		{
			get
			{
				return "RemoveMigrationEndpoint";
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x000B59F4 File Offset: 0x000B3BF4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveMigrationEndpoint(this.Identity.ToString());
			}
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000B5A08 File Offset: 0x000B3C08
		protected override IConfigDataProvider CreateSession()
		{
			MigrationLogger.Initialize();
			MigrationLogContext.Current.Source = "Remove-MigrationEndpoint";
			MigrationLogContext.Current.Organization = base.CurrentOrganizationId.OrganizationalUnit;
			return MigrationEndpointDataProvider.CreateDataProvider(this.Action, base.TenantGlobalCatalogSession, this.partitionMailbox);
		}
	}
}
