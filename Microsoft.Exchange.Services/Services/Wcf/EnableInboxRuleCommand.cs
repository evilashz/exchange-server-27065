using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000997 RID: 2455
	internal sealed class EnableInboxRuleCommand : SingleCmdletCommandBase<EnableInboxRuleRequest, EnableInboxRuleResponse, EnableInboxRule, Microsoft.Exchange.Management.Common.InboxRule>
	{
		// Token: 0x06004611 RID: 17937 RVA: 0x000F6956 File Offset: 0x000F4B56
		public EnableInboxRuleCommand(CallContext callContext, EnableInboxRuleRequest request) : base(callContext, request, "Enable-InboxRule", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x000F6968 File Offset: 0x000F4B68
		protected override void PopulateTaskParameters()
		{
			EnableInboxRule task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("AlwaysDeleteOutlookRulesBlob", task, new SwitchParameter(this.request.AlwaysDeleteOutlookRulesBlob));
			this.cmdletRunner.SetTaskParameter("Force", task, new SwitchParameter(this.request.Force));
			this.cmdletRunner.SetTaskParameter("Identity", task, new InboxRuleIdParameter(this.request.Identity));
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x000F69F3 File Offset: 0x000F4BF3
		protected override PSLocalTask<EnableInboxRule, Microsoft.Exchange.Management.Common.InboxRule> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateEnableInboxRuleTask(base.CallContext.AccessingPrincipal);
		}
	}
}
