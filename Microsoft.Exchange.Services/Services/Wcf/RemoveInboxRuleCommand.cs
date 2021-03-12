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
	// Token: 0x020009AD RID: 2477
	internal sealed class RemoveInboxRuleCommand : SingleCmdletCommandBase<RemoveInboxRuleRequest, RemoveInboxRuleResponse, RemoveInboxRule, Microsoft.Exchange.Management.Common.InboxRule>
	{
		// Token: 0x0600467E RID: 18046 RVA: 0x000F9DDF File Offset: 0x000F7FDF
		public RemoveInboxRuleCommand(CallContext callContext, RemoveInboxRuleRequest request) : base(callContext, request, "Remove-InboxRule", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x000F9DF0 File Offset: 0x000F7FF0
		protected override void PopulateTaskParameters()
		{
			RemoveInboxRule task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("AlwaysDeleteOutlookRulesBlob", task, new SwitchParameter(this.request.AlwaysDeleteOutlookRulesBlob));
			this.cmdletRunner.SetTaskParameter("Force", task, new SwitchParameter(this.request.Force));
			this.cmdletRunner.SetTaskParameter("Identity", task, new InboxRuleIdParameter(this.request.Identity));
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x000F9E7B File Offset: 0x000F807B
		protected override PSLocalTask<RemoveInboxRule, Microsoft.Exchange.Management.Common.InboxRule> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateRemoveInboxRuleTask(base.CallContext.AccessingPrincipal);
		}
	}
}
