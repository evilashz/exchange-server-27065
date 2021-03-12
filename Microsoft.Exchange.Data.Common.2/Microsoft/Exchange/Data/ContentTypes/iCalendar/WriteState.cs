using System;

namespace Microsoft.Exchange.Data.ContentTypes.iCalendar
{
	// Token: 0x020000B4 RID: 180
	[Flags]
	internal enum WriteState
	{
		// Token: 0x040005E3 RID: 1507
		Start = 1,
		// Token: 0x040005E4 RID: 1508
		Component = 2,
		// Token: 0x040005E5 RID: 1509
		Property = 4,
		// Token: 0x040005E6 RID: 1510
		Parameter = 8,
		// Token: 0x040005E7 RID: 1511
		Closed = 16
	}
}
