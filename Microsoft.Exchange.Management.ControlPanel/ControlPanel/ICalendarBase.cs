using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000066 RID: 102
	[ServiceContract(Namespace = "ECP", Name = "CalendarBase")]
	public interface ICalendarBase<O, U> : IEditObjectService<O, U>, IGetObjectService<O> where O : CalendarConfigurationBase where U : SetCalendarConfigurationBase
	{
	}
}
