using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000343 RID: 835
	internal interface ICalendarViewControl
	{
		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001F95 RID: 8085
		// (set) Token: 0x06001F96 RID: 8086
		OwaStoreObjectId SelectedItemId { get; set; }

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001F97 RID: 8087
		int Count { get; }

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001F98 RID: 8088
		CalendarAdapterBase CalendarAdapter { get; }
	}
}
