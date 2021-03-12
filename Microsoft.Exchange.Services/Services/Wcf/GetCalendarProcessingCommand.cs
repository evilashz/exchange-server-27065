using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.StoreTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200097E RID: 2430
	internal sealed class GetCalendarProcessingCommand : SingleCmdletCommandBase<object, GetCalendarProcessingResponse, GetCalendarProcessing, CalendarConfiguration>
	{
		// Token: 0x060045A1 RID: 17825 RVA: 0x000F47B9 File Offset: 0x000F29B9
		public GetCalendarProcessingCommand(CallContext callContext) : base(callContext, null, "Get-CalendarProcessing", ScopeLocation.RecipientRead)
		{
		}

		// Token: 0x060045A2 RID: 17826 RVA: 0x000F47CC File Offset: 0x000F29CC
		protected override void PopulateTaskParameters()
		{
			PSLocalTask<GetCalendarProcessing, CalendarConfiguration> taskWrapper = this.cmdletRunner.TaskWrapper;
			this.cmdletRunner.SetTaskParameter("Identity", taskWrapper.Task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x000F4810 File Offset: 0x000F2A10
		protected override void PopulateResponseData(GetCalendarProcessingResponse response)
		{
			PSLocalTask<GetCalendarProcessing, CalendarConfiguration> taskWrapper = this.cmdletRunner.TaskWrapper;
			CalendarConfiguration result = taskWrapper.Result;
			response.Options = new CalendarProcessingOptions
			{
				RemoveOldMeetingMessages = result.RemoveOldMeetingMessages,
				RemoveForwardedMeetingNotifications = result.RemoveForwardedMeetingNotifications
			};
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x000F4855 File Offset: 0x000F2A55
		protected override PSLocalTask<GetCalendarProcessing, CalendarConfiguration> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateGetCalendarProcessingTask(base.CallContext.AccessingPrincipal);
		}
	}
}
