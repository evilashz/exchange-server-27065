using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002E2 RID: 738
	[Cmdlet("remove", "ExchangeServerGroupMember", SupportsShouldProcess = true)]
	public sealed class RemoveExchangeServerGroupMember : ManageExchangeServerGroupMember
	{
		// Token: 0x060019A1 RID: 6561 RVA: 0x00072180 File Offset: 0x00070380
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.RemoveServerFromGroup(this.e12ds, this.recipientSession);
			this.RemoveServerFromGroup(this.exs, this.rootDomainRecipientSession);
			this.RemoveServerFromGroup(this.ets, this.rootDomainRecipientSession);
			TaskLogger.LogExit();
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x000721D0 File Offset: 0x000703D0
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
