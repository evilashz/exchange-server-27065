using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.StoreTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000990 RID: 2448
	internal sealed class SendTextMessagingVerificationCodeCommand : SingleCmdletCommandBase<SendTextMessagingVerificationCodeRequest, SendTextMessagingVerificationCodeResponse, SendTextMessagingVerificationCode, object>
	{
		// Token: 0x060045F6 RID: 17910 RVA: 0x000F624E File Offset: 0x000F444E
		public SendTextMessagingVerificationCodeCommand(CallContext callContext, SendTextMessagingVerificationCodeRequest request) : base(callContext, request, "Send-TextMessagingVerificationCode", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x060045F7 RID: 17911 RVA: 0x000F6260 File Offset: 0x000F4460
		protected override void PopulateTaskParameters()
		{
			SendTextMessagingVerificationCode task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("Identity", task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
			this.warningCollector = new CmdletResultsWarningCollector();
			this.cmdletRunner.TaskWrapper.Task.PrependTaskIOPipelineHandler(this.warningCollector);
		}

		// Token: 0x060045F8 RID: 17912 RVA: 0x000F62CA File Offset: 0x000F44CA
		protected override void PopulateResponseData(SendTextMessagingVerificationCodeResponse response)
		{
			response.WarningMessages = this.warningCollector.GetWarningMessagesForResult(0);
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x000F62DE File Offset: 0x000F44DE
		protected override PSLocalTask<SendTextMessagingVerificationCode, object> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateSendTextMessagingVerificationCodeTask(base.CallContext.AccessingPrincipal);
		}

		// Token: 0x0400288A RID: 10378
		private CmdletResultsWarningCollector warningCollector;
	}
}
