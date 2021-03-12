using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.StoreTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000988 RID: 2440
	internal sealed class ClearTextMessagingAccountCommand : SingleCmdletCommandBase<ClearTextMessagingAccountRequest, ClearTextMessagingAccountResponse, ClearTextMessagingAccount, object>
	{
		// Token: 0x060045D2 RID: 17874 RVA: 0x000F54B4 File Offset: 0x000F36B4
		public ClearTextMessagingAccountCommand(CallContext callContext, ClearTextMessagingAccountRequest request) : base(callContext, request, "Clear-TextMessagingAccount", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x060045D3 RID: 17875 RVA: 0x000F54C4 File Offset: 0x000F36C4
		protected override void PopulateTaskParameters()
		{
			ClearTextMessagingAccount task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("Identity", task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
		}

		// Token: 0x060045D4 RID: 17876 RVA: 0x000F5508 File Offset: 0x000F3708
		protected override PSLocalTask<ClearTextMessagingAccount, object> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateClearTextMessagingAccountTask(base.CallContext.AccessingPrincipal);
		}
	}
}
