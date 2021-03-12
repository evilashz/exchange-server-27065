using System;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets
{
	// Token: 0x02000015 RID: 21
	internal interface IBirthdayCalendarReference : IEntityReference<IBirthdayCalendar>
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007B RID: 123
		IBirthdayEvents Events { get; }
	}
}
