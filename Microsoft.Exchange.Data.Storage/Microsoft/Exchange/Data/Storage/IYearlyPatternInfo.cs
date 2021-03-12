using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003FE RID: 1022
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IYearlyPatternInfo : IMonthlyPatternInfo
	{
		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06002EAF RID: 11951
		bool IsLeapMonth { get; }

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06002EB0 RID: 11952
		int Month { get; }
	}
}
