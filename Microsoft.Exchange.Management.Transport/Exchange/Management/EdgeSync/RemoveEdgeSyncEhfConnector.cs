using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x02000039 RID: 57
	[Cmdlet("Remove", "EdgeSyncEhfConnector", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
	public sealed class RemoveEdgeSyncEhfConnector : RemoveSystemConfigurationObjectTask<EdgeSyncEhfConnectorIdParameter, EdgeSyncEhfConnector>
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000783A File Offset: 0x00005A3A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveEdgeSyncEhfConnector(this.Identity.ToString());
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000784C File Offset: 0x00005A4C
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 51, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\RemoveEdgeSyncEHFConnector.cs");
		}
	}
}
