using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005B1 RID: 1457
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IConversionParticipantList
	{
		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06003BED RID: 15341
		int Count { get; }

		// Token: 0x17001247 RID: 4679
		Participant this[int index]
		{
			get;
			set;
		}

		// Token: 0x06003BF0 RID: 15344
		bool IsConversionParticipantAlwaysResolvable(int index);
	}
}
