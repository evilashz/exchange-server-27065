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
	// Token: 0x0200097F RID: 2431
	internal sealed class GetMailboxCalendarConfigurationCommand : SingleCmdletCommandBase<object, GetMailboxCalendarConfigurationResponse, GetMailboxCalendarConfiguration, Microsoft.Exchange.Data.Storage.Management.MailboxCalendarConfiguration>
	{
		// Token: 0x060045A5 RID: 17829 RVA: 0x000F486C File Offset: 0x000F2A6C
		public GetMailboxCalendarConfigurationCommand(CallContext callContext) : base(callContext, null, "Get-MailboxCalendarConfiguration", ScopeLocation.RecipientRead)
		{
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x000F487C File Offset: 0x000F2A7C
		protected override void PopulateTaskParameters()
		{
			GetMailboxCalendarConfiguration task = this.cmdletRunner.TaskWrapper.Task;
			this.cmdletRunner.SetTaskParameter("Identity", task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x000F48C0 File Offset: 0x000F2AC0
		protected override void PopulateResponseData(GetMailboxCalendarConfigurationResponse response)
		{
			Microsoft.Exchange.Data.Storage.Management.MailboxCalendarConfiguration result = this.cmdletRunner.TaskWrapper.Result;
			response.Options = new Microsoft.Exchange.Services.Core.Types.MailboxCalendarConfiguration
			{
				CurrentTimeZone = new TimeZoneInformation
				{
					TimeZoneId = EWSSettings.RequestTimeZone.Id,
					DisplayName = EWSSettings.RequestTimeZone.LocalizableDisplayName.ToString(base.CallContext.ClientCulture)
				},
				WorkDays = result.WorkDays,
				WorkingHoursStartTime = (int)result.WorkingHoursStartTime.TotalMinutes,
				WorkingHoursEndTime = (int)result.WorkingHoursEndTime.TotalMinutes,
				WorkingHoursTimeZone = new TimeZoneInformation
				{
					TimeZoneId = result.WorkingHoursTimeZone.ExTimeZone.Id,
					DisplayName = result.WorkingHoursTimeZone.ExTimeZone.LocalizableDisplayName.ToString(base.CallContext.ClientCulture)
				},
				WeekStartDay = result.WeekStartDay,
				ShowWeekNumbers = result.ShowWeekNumbers,
				FirstWeekOfYear = result.FirstWeekOfYear,
				TimeIncrement = result.TimeIncrement,
				RemindersEnabled = result.RemindersEnabled,
				ReminderSoundEnabled = result.ReminderSoundEnabled,
				DefaultReminderTime = (CalendarReminder)result.DefaultReminderTime.TotalMinutes
			};
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x000F4A0F File Offset: 0x000F2C0F
		protected override PSLocalTask<GetMailboxCalendarConfiguration, Microsoft.Exchange.Data.Storage.Management.MailboxCalendarConfiguration> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateGetMailboxCalendarConfigurationTask(base.CallContext.AccessingPrincipal);
		}
	}
}
