using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200099A RID: 2458
	internal sealed class GetCASMailboxCommand : SingleCmdletCommandBase<GetCASMailboxRequest, GetCASMailboxResponse, GetCASMailbox, Microsoft.Exchange.Data.Directory.Management.CASMailbox>
	{
		// Token: 0x06004626 RID: 17958 RVA: 0x000F6ECB File Offset: 0x000F50CB
		public GetCASMailboxCommand(CallContext callContext, GetCASMailboxRequest request) : base(callContext, request, "Get-CASMailbox", ScopeLocation.RecipientRead)
		{
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x000F6EDC File Offset: 0x000F50DC
		protected override void PopulateTaskParameters()
		{
			PSLocalTask<GetCASMailbox, Microsoft.Exchange.Data.Directory.Management.CASMailbox> taskWrapper = this.cmdletRunner.TaskWrapper;
			this.cmdletRunner.SetTaskParameter("ProtocolSettings", taskWrapper.Task, new SwitchParameter(true));
			if (this.request != null)
			{
				this.cmdletRunner.SetTaskParameterIfModified("ActiveSyncDebugLogging", this.request.Options, taskWrapper.Task, new SwitchParameter(this.request.Options.ActiveSyncDebugLogging));
			}
			this.cmdletRunner.SetTaskParameter("Identity", taskWrapper.Task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
		}

		// Token: 0x06004628 RID: 17960 RVA: 0x000F6F84 File Offset: 0x000F5184
		protected override void PopulateResponseData(GetCASMailboxResponse response)
		{
			PSLocalTask<GetCASMailbox, Microsoft.Exchange.Data.Directory.Management.CASMailbox> taskWrapper = this.cmdletRunner.TaskWrapper;
			response.Options = new Microsoft.Exchange.Services.Core.Types.CASMailbox
			{
				ActiveSyncDebugLogging = taskWrapper.Result.ActiveSyncDebugLogging,
				ActiveSyncEnabled = taskWrapper.Result.ActiveSyncEnabled,
				ExternalImapSettings = taskWrapper.Result.ExternalImapSettings,
				ExternalPopSettings = taskWrapper.Result.ExternalPopSettings,
				ExternalSmtpSettings = taskWrapper.Result.ExternalSmtpSettings,
				ImapForceICalForCalendarRetrievalOption = taskWrapper.Result.ImapForceICalForCalendarRetrievalOption,
				ImapSuppressReadReceipt = taskWrapper.Result.ImapSuppressReadReceipt,
				InternalImapSettings = taskWrapper.Result.InternalImapSettings,
				InternalPopSettings = taskWrapper.Result.InternalPopSettings,
				InternalSmtpSettings = taskWrapper.Result.InternalSmtpSettings,
				PopForceICalForCalendarRetrievalOption = taskWrapper.Result.PopForceICalForCalendarRetrievalOption,
				PopSuppressReadReceipt = taskWrapper.Result.PopSuppressReadReceipt,
				ImapEnabled = taskWrapper.Result.ImapEnabled,
				MAPIEnabled = taskWrapper.Result.MAPIEnabled,
				OWAEnabled = taskWrapper.Result.OWAEnabled,
				PopEnabled = taskWrapper.Result.PopEnabled
			};
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x000F70BA File Offset: 0x000F52BA
		protected override PSLocalTask<GetCASMailbox, Microsoft.Exchange.Data.Directory.Management.CASMailbox> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateGetCASMailboxTask(base.CallContext.AccessingPrincipal);
		}
	}
}
