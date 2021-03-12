using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002E3 RID: 739
	[Cmdlet("remove", "ManagedAvailabilityServerGroupMember", SupportsShouldProcess = true)]
	public sealed class RemoveManagedAvailabilityServerGroupMember : ManageManagedAvailabilityServerGroupMember
	{
		// Token: 0x060019A4 RID: 6564 RVA: 0x0007229B File Offset: 0x0007049B
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.RemoveServerFromGroup(this.mas, this.rootDomainRecipientSession);
			this.server.MonitoringInstalled = 0;
			this.gcSession.Save(this.server);
			TaskLogger.LogExit();
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x000722D8 File Offset: 0x000704D8
		private void RemoveServerFromGroup(ADGroup group, IRecipientSession session)
		{
			if (group != null && this.server != null && group.Members.Contains(this.server.Id))
			{
				group.Members.Remove(this.server.Id);
				if (base.ShouldProcess(group.Id.DistinguishedName, Strings.InfoProcessRemoveMember(this.server.Id.DistinguishedName), null))
				{
					SetupTaskBase.Save(group, session);
					base.LogWriteObject(group);
					return;
				}
			}
			else
			{
				this.WriteWarning(Strings.InfoIsNotMemberOfGroup((this.server == null) ? "-" : this.server.Id.DistinguishedName, (group == null) ? "-" : group.Id.DistinguishedName));
			}
		}
	}
}
