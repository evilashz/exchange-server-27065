using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000432 RID: 1074
	public abstract class RemoveMailboxPolicyBase<T> : RemoveSystemConfigurationObjectTask<MailboxPolicyIdParameter, T> where T : MailboxPolicy, new()
	{
		// Token: 0x06002600 RID: 9728
		protected abstract bool HandleRemoveWithAssociatedUsers();

		// Token: 0x06002601 RID: 9729 RVA: 0x00097DA8 File Offset: 0x00095FA8
		protected override void InternalProcessRecord()
		{
			T dataObject = base.DataObject;
			if (dataObject.CheckForAssociatedUsers() && !this.HandleRemoveWithAssociatedUsers())
			{
				return;
			}
			base.InternalProcessRecord();
		}
	}
}
