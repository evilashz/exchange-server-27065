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
	// Token: 0x02000996 RID: 2454
	internal sealed class DisableInboxRuleCommand : SingleCmdletCommandBase<DisableInboxRuleRequest, DisableInboxRuleResponse, DisableInboxRule, Microsoft.Exchange.Management.Common.InboxRule>
	{
		// Token: 0x0600460E RID: 17934 RVA: 0x000F68A3 File Offset: 0x000F4AA3
		public DisableInboxRuleCommand(CallContext callContext, DisableInboxRuleRequest request) : base(callContext, request, "Disable-InboxRule", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x000F68B4 File Offset: 0x000F4AB4
		protected override void PopulateTaskParameters()
		{
			DisableInboxRule task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("AlwaysDeleteOutlookRulesBlob", task, new SwitchParameter(this.request.AlwaysDeleteOutlookRulesBlob));
			this.cmdletRunner.SetTaskParameter("Force", task, new SwitchParameter(this.request.Force));
			this.cmdletRunner.SetTaskParameter("Identity", task, new InboxRuleIdParameter(this.request.Identity));
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x000F693F File Offset: 0x000F4B3F
		protected override PSLocalTask<DisableInboxRule, Microsoft.Exchange.Management.Common.InboxRule> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateDisableInboxRuleTask(base.CallContext.AccessingPrincipal);
		}
	}
}
