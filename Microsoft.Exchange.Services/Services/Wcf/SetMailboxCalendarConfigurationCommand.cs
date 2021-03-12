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
	// Token: 0x02000982 RID: 2434
	internal sealed class SetMailboxCalendarConfigurationCommand : SingleCmdletCommandBase<SetMailboxCalendarConfigurationRequest, OptionsResponseBase, SetMailboxCalendarConfiguration, Microsoft.Exchange.Data.Storage.Management.MailboxCalendarConfiguration>
	{
		// Token: 0x060045AF RID: 17839 RVA: 0x000F4B87 File Offset: 0x000F2D87
		public SetMailboxCalendarConfigurationCommand(CallContext callContext, SetMailboxCalendarConfigurationRequest request) : base(callContext, request, "Set-MailboxCalendarConfiguration", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x000F4B98 File Offset: 0x000F2D98
		protected override void PopulateTaskParameters()
		{
			SetMailboxCalendarConfiguration task = this.cmdletRunner.TaskWrapper.Task;
			Microsoft.Exchange.Services.Core.Types.MailboxCalendarConfiguration options = this.request.Options;
			this.cmdletRunner.SetTaskParameter("Identity", task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
			Microsoft.Exchange.Data.Storage.Management.MailboxCalendarConfiguration taskParameters = (Microsoft.Exchange.Data.Storage.Management.MailboxCalendarConfiguration)task.GetDynamicParameters();
			this.cmdletRunner.SetTaskParameterIfModified("WorkingHoursStartTime", options, taskParameters, new TimeSpan(0, options.WorkingHoursStartTime, 0));
			this.cmdletRunner.SetTaskParameterIfModified("WorkingHoursEndTime", options, taskParameters, new TimeSpan(0, options.WorkingHoursEndTime, 0));
			this.cmdletRunner.SetTaskParameterIfModified("DefaultReminderTime", options, taskParameters, new TimeSpan(0, (int)options.DefaultReminderTime, 0));
			this.cmdletRunner.SetTaskParameterIfModified("WorkingHoursTimeZone", options, taskParameters, (options.WorkingHoursTimeZone != null) ? ExTimeZoneValue.Parse(options.WorkingHoursTimeZone.TimeZoneId) : null);
			this.cmdletRunner.SetRemainingModifiedTaskParameters(options, taskParameters);
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x000F4C9A File Offset: 0x000F2E9A
		protected override PSLocalTask<SetMailboxCalendarConfiguration, Microsoft.Exchange.Data.Storage.Management.MailboxCalendarConfiguration> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateSetMailboxCalendarConfigurationTask(base.CallContext.AccessingPrincipal);
		}
	}
}
