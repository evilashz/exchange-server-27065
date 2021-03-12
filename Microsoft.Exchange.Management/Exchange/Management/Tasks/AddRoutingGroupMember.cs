using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000169 RID: 361
	[Cmdlet("Add", "RoutingGroupMember", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class AddRoutingGroupMember : ManageRoutingGroupMember
	{
		// Token: 0x06000D33 RID: 3379 RVA: 0x0003CFF0 File Offset: 0x0003B1F0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.server.HomeRoutingGroup == null)
			{
				this.server.HomeRoutingGroup = this.routingGroup.Id;
				this.configurationSession.Save(this.server);
				if (this.routingGroup.RoutingMasterDN == null)
				{
					this.routingGroup.RoutingMasterDN = this.server.Id;
					this.configurationSession.Save(this.routingGroup);
				}
			}
			TaskLogger.LogExit();
		}
	}
}
