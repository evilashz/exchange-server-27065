using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003FD RID: 1021
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IWeeklyPatternInfo
	{
		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06002EAD RID: 11949
		DaysOfWeek DaysOfWeek { get; }

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06002EAE RID: 11950
		DayOfWeek FirstDayOfWeek { get; }
	}
}
