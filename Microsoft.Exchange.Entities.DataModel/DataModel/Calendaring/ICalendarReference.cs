using System;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000058 RID: 88
	public interface ICalendarReference : IEntityReference<Calendar>
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060002EA RID: 746
		IEvents Events { get; }
	}
}
