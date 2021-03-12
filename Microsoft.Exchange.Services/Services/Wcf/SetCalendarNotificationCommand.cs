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
	// Token: 0x02000980 RID: 2432
	internal sealed class SetCalendarNotificationCommand : SingleCmdletCommandBase<SetCalendarNotificationRequest, OptionsResponseBase, SetCalendarNotification, CalendarNotification>
	{
		// Token: 0x060045A9 RID: 17833 RVA: 0x000F4A26 File Offset: 0x000F2C26
		public SetCalendarNotificationCommand(CallContext callContext, SetCalendarNotificationRequest request) : base(callContext, request, "Set-CalendarNotification", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x060045AA RID: 17834 RVA: 0x000F4A38 File Offset: 0x000F2C38
		protected override void PopulateTaskParameters()
		{
			PSLocalTask<SetCalendarNotification, CalendarNotification> taskWrapper = this.cmdletRunner.TaskWrapper;
			this.cmdletRunner.SetTaskParameter("Identity", taskWrapper.Task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
			CalendarNotification taskParameters = (CalendarNotification)taskWrapper.Task.GetDynamicParameters();
			this.cmdletRunner.SetTaskParameterIfModified("DailyAgendaNotificationSendTime", this.request.Options, taskParameters, new TimeSpan(0, this.request.Options.DailyAgendaNotificationSendTime, 0));
			this.cmdletRunner.SetRemainingModifiedTaskParameters(this.request.Options, taskParameters);
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x000F4ADC File Offset: 0x000F2CDC
		protected override PSLocalTask<SetCalendarNotification, CalendarNotification> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateSetCalendarNotificationTask(base.CallContext.AccessingPrincipal);
		}
	}
}
