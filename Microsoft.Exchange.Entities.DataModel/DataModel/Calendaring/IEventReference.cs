using System;
using Microsoft.Exchange.Entities.DataModel.Items;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x0200005B RID: 91
	public interface IEventReference : IItemReference<Event>, IEntityReference<Event>
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060002EE RID: 750
		ICalendarReference Calendar { get; }
	}
}
