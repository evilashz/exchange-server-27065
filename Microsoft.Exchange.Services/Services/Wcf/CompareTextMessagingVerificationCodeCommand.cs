using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.StoreTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000989 RID: 2441
	internal sealed class CompareTextMessagingVerificationCodeCommand : SingleCmdletCommandBase<CompareTextMessagingVerificationCodeRequest, CompareTextMessagingVerificationCodeResponse, CompareTextMessagingVerificationCode, object>
	{
		// Token: 0x060045D5 RID: 17877 RVA: 0x000F551F File Offset: 0x000F371F
		public CompareTextMessagingVerificationCodeCommand(CallContext callContext, CompareTextMessagingVerificationCodeRequest request) : base(callContext, request, "Compare-TextMessagingVerificationCode", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x060045D6 RID: 17878 RVA: 0x000F5530 File Offset: 0x000F3730
		protected override void PopulateTaskParameters()
		{
			CompareTextMessagingVerificationCode task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("Identity", task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
			this.cmdletRunner.SetTaskParameter("VerificationCode", task, this.request.VerificationCode);
		}

		// Token: 0x060045D7 RID: 17879 RVA: 0x000F5590 File Offset: 0x000F3790
		protected override PSLocalTask<CompareTextMessagingVerificationCode, object> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateCompareTextMessagingVerificationCodeTask(base.CallContext.AccessingPrincipal);
		}
	}
}
