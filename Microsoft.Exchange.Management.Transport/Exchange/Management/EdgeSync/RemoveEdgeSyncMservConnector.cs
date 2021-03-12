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
	// Token: 0x0200003A RID: 58
	[Cmdlet("Remove", "EdgeSyncMservConnector", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
	public sealed class RemoveEdgeSyncMservConnector : RemoveSystemConfigurationObjectTask<EdgeSyncMservConnectorIdParameter, EdgeSyncMservConnector>
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000787E File Offset: 0x00005A7E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveEdgeSyncMservConnector(this.Identity.ToString());
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007890 File Offset: 0x00005A90
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 44, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\RemoveEdgeSyncMservConnector.cs");
		}
	}
}
