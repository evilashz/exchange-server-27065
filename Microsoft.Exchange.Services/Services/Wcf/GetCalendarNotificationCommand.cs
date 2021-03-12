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
	// Token: 0x0200097D RID: 2429
	internal sealed class GetCalendarNotificationCommand : SingleCmdletCommandBase<object, GetCalendarNotificationResponse, GetCalendarNotification, CalendarNotification>
	{
		// Token: 0x0600459D RID: 17821 RVA: 0x000F46A8 File Offset: 0x000F28A8
		public GetCalendarNotificationCommand(CallContext callContext) : base(callContext, null, "Get-CalendarNotification", ScopeLocation.RecipientRead)
		{
		}

		// Token: 0x0600459E RID: 17822 RVA: 0x000F46B8 File Offset: 0x000F28B8
		protected override void PopulateTaskParameters()
		{
			PSLocalTask<GetCalendarNotification, CalendarNotification> taskWrapper = this.cmdletRunner.TaskWrapper;
			this.cmdletRunner.SetTaskParameter("Identity", taskWrapper.Task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
		}

		// Token: 0x0600459F RID: 17823 RVA: 0x000F46FC File Offset: 0x000F28FC
		protected override void PopulateResponseData(GetCalendarNotificationResponse response)
		{
			PSLocalTask<GetCalendarNotification, CalendarNotification> taskWrapper = this.cmdletRunner.TaskWrapper;
			response.Options = new CalendarNotificationOptions
			{
				CalendarUpdateNotification = taskWrapper.Result.CalendarUpdateNotification,
				NextDays = taskWrapper.Result.NextDays,
				CalendarUpdateSendDuringWorkHour = taskWrapper.Result.CalendarUpdateSendDuringWorkHour,
				MeetingReminderNotification = taskWrapper.Result.MeetingReminderNotification,
				MeetingReminderSendDuringWorkHour = taskWrapper.Result.MeetingReminderSendDuringWorkHour,
				DailyAgendaNotification = taskWrapper.Result.DailyAgendaNotification,
				DailyAgendaNotificationSendTime = (int)taskWrapper.Result.DailyAgendaNotificationSendTime.TotalMinutes
			};
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x000F47A2 File Offset: 0x000F29A2
		protected override PSLocalTask<GetCalendarNotification, CalendarNotification> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateGetCalendarNotificationTask(base.CallContext.AccessingPrincipal);
		}
	}
}
