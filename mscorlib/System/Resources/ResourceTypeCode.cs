using System;

namespace System.Resources
{
	// Token: 0x0200036E RID: 878
	[Serializable]
	internal enum ResourceTypeCode
	{
		// Token: 0x040011B4 RID: 4532
		Null,
		// Token: 0x040011B5 RID: 4533
		String,
		// Token: 0x040011B6 RID: 4534
		Boolean,
		// Token: 0x040011B7 RID: 4535
		Char,
		// Token: 0x040011B8 RID: 4536
		Byte,
		// Token: 0x040011B9 RID: 4537
		SByte,
		// Token: 0x040011BA RID: 4538
		Int16,
		// Token: 0x040011BB RID: 4539
		UInt16,
		// Token: 0x040011BC RID: 4540
		Int32,
		// Token: 0x040011BD RID: 4541
		UInt32,
		// Token: 0x040011BE RID: 4542
		Int64,
		// Token: 0x040011BF RID: 4543
		UInt64,
		// Token: 0x040011C0 RID: 4544
		Single,
		// Token: 0x040011C1 RID: 4545
		Double,
		// Token: 0x040011C2 RID: 4546
		Decimal,
		// Token: 0x040011C3 RID: 4547
		DateTime,
		// Token: 0x040011C4 RID: 4548
		TimeSpan,
		// Token: 0x040011C5 RID: 4549
		LastPrimitive = 16,
		// Token: 0x040011C6 RID: 4550
		ByteArray = 32,
		// Token: 0x040011C7 RID: 4551
		Stream,
		// Token: 0x040011C8 RID: 4552
		StartOfUserTypes = 64
	}
}
