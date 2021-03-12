using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000067 RID: 103
	[ServiceContract(Namespace = "ECP", Name = "CalendarAppearance")]
	public interface ICalendarAppearance : ICalendarBase<CalendarAppearanceConfiguration, SetCalendarAppearanceConfiguration>, IEditObjectService<CalendarAppearanceConfiguration, SetCalendarAppearanceConfiguration>, IGetObjectService<CalendarAppearanceConfiguration>
	{
		// Token: 0x06001A80 RID: 6784
		[OperationContract]
		PowerShellResults<CalendarAppearanceConfiguration> UpdateObject(Identity identity);
	}
}
