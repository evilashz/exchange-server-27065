using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009B4 RID: 2484
	internal sealed class SetMailboxCommand : SingleCmdletCommandBase<SetMailboxRequest, OptionsResponseBase, SetMailbox, object>
	{
		// Token: 0x06004695 RID: 18069 RVA: 0x000FAA73 File Offset: 0x000F8C73
		public SetMailboxCommand(CallContext callContext, SetMailboxRequest request) : base(callContext, request, "Set-Mailbox", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x000FAA84 File Offset: 0x000F8C84
		protected override void PopulateTaskParameters()
		{
			PSLocalTask<SetMailbox, object> taskWrapper = this.cmdletRunner.TaskWrapper;
			SetMailbox task = taskWrapper.Task;
			MailboxOptions mailbox = this.request.Mailbox;
			this.cmdletRunner.SetTaskParameter("Identity", task, mailbox.Identity.ToIdParameter<MailboxIdParameter>());
			object dynamicParameters = taskWrapper.Task.GetDynamicParameters();
			this.cmdletRunner.SetTaskParameterIfModified("AddressString", "ForwardingSmtpAddress", mailbox, dynamicParameters, string.IsNullOrEmpty(mailbox.AddressString) ? null : ProxyAddress.Parse(mailbox.AddressString));
			this.cmdletRunner.SetRemainingModifiedTaskParameters(mailbox, dynamicParameters);
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x000FAB17 File Offset: 0x000F8D17
		protected override PSLocalTask<SetMailbox, object> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateSetMailboxTask(base.CallContext.AccessingPrincipal, "Identity");
		}
	}
}
