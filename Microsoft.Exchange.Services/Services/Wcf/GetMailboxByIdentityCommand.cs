using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009A0 RID: 2464
	internal sealed class GetMailboxByIdentityCommand : SingleCmdletCommandBase<IdentityRequest, GetMailboxResponse, GetMailbox, Mailbox>
	{
		// Token: 0x06004644 RID: 17988 RVA: 0x000F7FEA File Offset: 0x000F61EA
		public GetMailboxByIdentityCommand(CallContext callContext, IdentityRequest request) : base(callContext, request, "Get-Mailbox", ScopeLocation.RecipientRead)
		{
		}

		// Token: 0x06004645 RID: 17989 RVA: 0x000F7FFC File Offset: 0x000F61FC
		protected override void PopulateTaskParameters()
		{
			GetMailbox task = this.cmdletRunner.TaskWrapper.Task;
			if (this.request.Identity != null)
			{
				this.cmdletRunner.SetTaskParameter("Identity", task, this.request.Identity.ToIdParameter<MailboxIdParameter>());
			}
			else
			{
				this.cmdletRunner.SetTaskParameter("Identity", task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
			}
			this.cmdletRunner.SetTaskParameter("ResultSize", task, new Unlimited<uint>(1U));
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x000F8094 File Offset: 0x000F6294
		protected override void PopulateResponseData(GetMailboxResponse response)
		{
			Mailbox result = this.cmdletRunner.TaskWrapper.Result;
			response.MailboxOptions = ((result == null) ? null : new MailboxOptions
			{
				AddressString = ((result.ForwardingSmtpAddress == null) ? null : result.ForwardingSmtpAddress.AddressString),
				Identity = new Identity(result.Identity.ToString(), result.DisplayName),
				DeliverToMailboxAndForward = result.DeliverToMailboxAndForward
			});
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x000F810F File Offset: 0x000F630F
		protected override PSLocalTask<GetMailbox, Mailbox> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateGetMailboxTask(base.CallContext.AccessingPrincipal, "Identity");
		}
	}
}
