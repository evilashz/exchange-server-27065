using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions
{
	// Token: 0x0200003A RID: 58
	public class ExpandedEvent
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00003A77 File Offset: 0x00001C77
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00003A7F File Offset: 0x00001C7F
		public Event RecurrenceMaster { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00003A88 File Offset: 0x00001C88
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00003A90 File Offset: 0x00001C90
		public IList<Event> Occurrences { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00003A99 File Offset: 0x00001C99
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00003AA1 File Offset: 0x00001CA1
		public IList<string> CancelledOccurrences { get; set; }
	}
}
