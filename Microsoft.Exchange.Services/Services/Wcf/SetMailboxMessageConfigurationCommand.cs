using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.StoreTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009B6 RID: 2486
	internal sealed class SetMailboxMessageConfigurationCommand : SingleCmdletCommandBase<SetMailboxMessageConfigurationRequest, OptionsResponseBase, SetMailboxMessageConfiguration, MailboxMessageConfiguration>
	{
		// Token: 0x0600469B RID: 18075 RVA: 0x000FAC29 File Offset: 0x000F8E29
		public SetMailboxMessageConfigurationCommand(CallContext callContext, SetMailboxMessageConfigurationRequest request) : base(callContext, request, "Set-MailboxMessageConfiguration", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x0600469C RID: 18076 RVA: 0x000FAC3C File Offset: 0x000F8E3C
		protected override void PopulateTaskParameters()
		{
			PSLocalTask<SetMailboxMessageConfiguration, MailboxMessageConfiguration> taskWrapper = this.cmdletRunner.TaskWrapper;
			this.cmdletRunner.SetTaskParameter("Identity", taskWrapper.Task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
			MailboxMessageConfiguration taskParameters = (MailboxMessageConfiguration)taskWrapper.Task.GetDynamicParameters();
			this.cmdletRunner.SetRemainingModifiedTaskParameters(this.request.Options, taskParameters);
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x000FACA8 File Offset: 0x000F8EA8
		protected override PSLocalTask<SetMailboxMessageConfiguration, MailboxMessageConfiguration> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateSetMailboxMessageConfigurationTask(base.CallContext.AccessingPrincipal);
		}
	}
}
