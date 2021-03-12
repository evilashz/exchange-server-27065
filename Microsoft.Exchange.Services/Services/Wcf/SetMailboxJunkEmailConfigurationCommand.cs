using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.StoreTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009B5 RID: 2485
	internal sealed class SetMailboxJunkEmailConfigurationCommand : SingleCmdletCommandBase<SetMailboxJunkEmailConfigurationRequest, OptionsResponseBase, SetMailboxJunkEmailConfiguration, MailboxJunkEmailConfiguration>
	{
		// Token: 0x06004698 RID: 18072 RVA: 0x000FAB33 File Offset: 0x000F8D33
		public SetMailboxJunkEmailConfigurationCommand(CallContext callContext, SetMailboxJunkEmailConfigurationRequest request) : base(callContext, request, "Set-MailboxJunkEmailConfiguration", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x06004699 RID: 18073 RVA: 0x000FAB44 File Offset: 0x000F8D44
		protected override void PopulateTaskParameters()
		{
			PSLocalTask<SetMailboxJunkEmailConfiguration, MailboxJunkEmailConfiguration> taskWrapper = this.cmdletRunner.TaskWrapper;
			this.cmdletRunner.SetTaskParameter("Identity", taskWrapper.Task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
			MailboxJunkEmailConfiguration taskParameters = (MailboxJunkEmailConfiguration)taskWrapper.Task.GetDynamicParameters();
			this.cmdletRunner.SetTaskParameterIfModified("BlockedSendersAndDomains", this.request.Options, taskParameters, new MultiValuedProperty<string>(this.request.Options.BlockedSendersAndDomains));
			this.cmdletRunner.SetTaskParameterIfModified("TrustedSendersAndDomains", this.request.Options, taskParameters, new MultiValuedProperty<string>(this.request.Options.TrustedSendersAndDomains));
			this.cmdletRunner.SetRemainingModifiedTaskParameters(this.request.Options, taskParameters);
		}

		// Token: 0x0600469A RID: 18074 RVA: 0x000FAC12 File Offset: 0x000F8E12
		protected override PSLocalTask<SetMailboxJunkEmailConfiguration, MailboxJunkEmailConfiguration> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateSetMailboxJunkEmailConfigurationTask(base.CallContext.AccessingPrincipal);
		}
	}
}
