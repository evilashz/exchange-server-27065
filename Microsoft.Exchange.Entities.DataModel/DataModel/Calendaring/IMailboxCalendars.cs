using System;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x0200005D RID: 93
	public interface IMailboxCalendars : ICalendars, IEntitySet<Calendar>
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060002F7 RID: 759
		ICalendarReference Default { get; }
	}
}
