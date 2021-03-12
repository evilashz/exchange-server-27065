using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000075 RID: 117
	[ServiceContract(Namespace = "ECP", Name = "CalendarReminder")]
	public interface ICalendarReminder : ICalendarBase<CalendarReminderConfiguration, SetCalendarReminderConfiguration>, IEditObjectService<CalendarReminderConfiguration, SetCalendarReminderConfiguration>, IGetObjectService<CalendarReminderConfiguration>
	{
	}
}
