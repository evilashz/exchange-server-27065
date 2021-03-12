using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000076 RID: 118
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class CalendarReminder : CalendarBase, ICalendarReminder, ICalendarBase<CalendarReminderConfiguration, SetCalendarReminderConfiguration>, IEditObjectService<CalendarReminderConfiguration, SetCalendarReminderConfiguration>, IGetObjectService<CalendarReminderConfiguration>
	{
		// Token: 0x06001B1F RID: 6943 RVA: 0x000568A1 File Offset: 0x00054AA1
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxCalendarConfiguration?Identity@R:Self")]
		public PowerShellResults<CalendarReminderConfiguration> GetObject(Identity identity)
		{
			return base.GetObject<CalendarReminderConfiguration>(identity);
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x000568AA File Offset: 0x00054AAA
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxCalendarConfiguration?Identity@R:Self+Set-MailboxCalendarConfiguration?Identity@W:Self")]
		public PowerShellResults<CalendarReminderConfiguration> SetObject(Identity identity, SetCalendarReminderConfiguration properties)
		{
			return base.SetObject<CalendarReminderConfiguration, SetCalendarReminderConfiguration>(identity, properties);
		}
	}
}
