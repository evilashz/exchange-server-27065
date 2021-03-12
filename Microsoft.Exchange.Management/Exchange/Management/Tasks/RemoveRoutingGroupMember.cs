using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000226 RID: 550
	[Cmdlet("Remove", "RoutingGroupMember", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class RemoveRoutingGroupMember : ManageRoutingGroupMember
	{
		// Token: 0x060012B4 RID: 4788 RVA: 0x000522B1 File Offset: 0x000504B1
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			this.add = false;
			base.InternalBeginProcessing();
			TaskLogger.LogExit();
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x000522CC File Offset: 0x000504CC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.routingGroup != null)
			{
				this.server.HomeRoutingGroup = null;
				this.configurationSession.Save(this.server);
				if (this.routingGroup.RoutingMasterDN != null && this.routingGroup.RoutingMasterDN.Equals(this.server.Id))
				{
					ADObjectId routingMasterDN = null;
					foreach (ADObjectId adobjectId in this.routingGroup.RoutingMembersDN)
					{
						if (!adobjectId.Equals(this.server.Id))
						{
							routingMasterDN = adobjectId;
							break;
						}
					}
					this.routingGroup.RoutingMasterDN = routingMasterDN;
					this.configurationSession.Save(this.routingGroup);
				}
			}
			TaskLogger.LogExit();
		}
	}
}
