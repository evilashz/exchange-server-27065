using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200018D RID: 397
	internal enum RunKind : uint
	{
		// Token: 0x040011A1 RID: 4513
		Invalid,
		// Token: 0x040011A2 RID: 4514
		Text = 67108864U,
		// Token: 0x040011A3 RID: 4515
		StartLexicalUnitFlag = 2147483648U,
		// Token: 0x040011A4 RID: 4516
		MajorKindMask = 2080374784U,
		// Token: 0x040011A5 RID: 4517
		MajorKindMaskWithStartLexicalUnitFlag = 4227858432U,
		// Token: 0x040011A6 RID: 4518
		MinorKindMask = 50331648U,
		// Token: 0x040011A7 RID: 4519
		KindMask = 4278190080U
	}
}
