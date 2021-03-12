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
	// Token: 0x020009A1 RID: 2465
	internal sealed class GetMailboxJunkEmailConfigurationCommand : SingleCmdletCommandBase<object, GetMailboxJunkEmailConfigurationResponse, GetMailboxJunkEmailConfiguration, MailboxJunkEmailConfiguration>
	{
		// Token: 0x06004648 RID: 17992 RVA: 0x000F812B File Offset: 0x000F632B
		public GetMailboxJunkEmailConfigurationCommand(CallContext callContext) : base(callContext, null, "Get-MailboxJunkEmailConfiguration", ScopeLocation.RecipientRead)
		{
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x000F813C File Offset: 0x000F633C
		protected override void PopulateTaskParameters()
		{
			PSLocalTask<GetMailboxJunkEmailConfiguration, MailboxJunkEmailConfiguration> taskWrapper = this.cmdletRunner.TaskWrapper;
			this.cmdletRunner.SetTaskParameter("Identity", taskWrapper.Task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x000F8180 File Offset: 0x000F6380
		protected override void PopulateResponseData(GetMailboxJunkEmailConfigurationResponse response)
		{
			PSLocalTask<GetMailboxJunkEmailConfiguration, MailboxJunkEmailConfiguration> taskWrapper = this.cmdletRunner.TaskWrapper;
			response.Options = new MailboxJunkEmailConfigurationOptions
			{
				Enabled = taskWrapper.Result.Enabled,
				ContactsTrusted = taskWrapper.Result.ContactsTrusted,
				TrustedListsOnly = taskWrapper.Result.TrustedListsOnly,
				TrustedSendersAndDomains = taskWrapper.Result.TrustedSendersAndDomains.ToArray(),
				BlockedSendersAndDomains = taskWrapper.Result.BlockedSendersAndDomains.ToArray()
			};
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x000F8205 File Offset: 0x000F6405
		protected override PSLocalTask<GetMailboxJunkEmailConfiguration, MailboxJunkEmailConfiguration> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateGetMailboxJunkEmailConfigurationTask(base.CallContext.AccessingPrincipal);
		}
	}
}
