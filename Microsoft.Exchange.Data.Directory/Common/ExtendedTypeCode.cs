using System;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000190 RID: 400
	public enum ExtendedTypeCode : byte
	{
		// Token: 0x040009AB RID: 2475
		Invalid,
		// Token: 0x040009AC RID: 2476
		Boolean,
		// Token: 0x040009AD RID: 2477
		Int16,
		// Token: 0x040009AE RID: 2478
		Int32,
		// Token: 0x040009AF RID: 2479
		Int64,
		// Token: 0x040009B0 RID: 2480
		Single,
		// Token: 0x040009B1 RID: 2481
		Double,
		// Token: 0x040009B2 RID: 2482
		DateTime,
		// Token: 0x040009B3 RID: 2483
		Guid,
		// Token: 0x040009B4 RID: 2484
		String,
		// Token: 0x040009B5 RID: 2485
		Binary,
		// Token: 0x040009B6 RID: 2486
		MVFlag = 16,
		// Token: 0x040009B7 RID: 2487
		MVInt16 = 18,
		// Token: 0x040009B8 RID: 2488
		MVInt32,
		// Token: 0x040009B9 RID: 2489
		MVInt64,
		// Token: 0x040009BA RID: 2490
		MVSingle,
		// Token: 0x040009BB RID: 2491
		MVDouble,
		// Token: 0x040009BC RID: 2492
		MVDateTime,
		// Token: 0x040009BD RID: 2493
		MVGuid,
		// Token: 0x040009BE RID: 2494
		MVString,
		// Token: 0x040009BF RID: 2495
		MVBinary
	}
}
