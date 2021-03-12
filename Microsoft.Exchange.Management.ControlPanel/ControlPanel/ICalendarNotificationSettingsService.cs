using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200047D RID: 1149
	[ServiceContract(Namespace = "ECP", Name = "CalendarNotificationSettings")]
	public interface ICalendarNotificationSettingsService : IEditObjectService<CalendarNotificationSettings, SetCalendarNotificationSettings>, IGetObjectService<CalendarNotificationSettings>
	{
	}
}
