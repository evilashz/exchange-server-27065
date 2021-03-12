using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000247 RID: 583
	[Flags]
	public enum MakeKeyGrbit
	{
		// Token: 0x040003C7 RID: 967
		None = 0,
		// Token: 0x040003C8 RID: 968
		NewKey = 1,
		// Token: 0x040003C9 RID: 969
		NormalizedKey = 8,
		// Token: 0x040003CA RID: 970
		KeyDataZeroLength = 16,
		// Token: 0x040003CB RID: 971
		StrLimit = 2,
		// Token: 0x040003CC RID: 972
		SubStrLimit = 4,
		// Token: 0x040003CD RID: 973
		FullColumnStartLimit = 256,
		// Token: 0x040003CE RID: 974
		FullColumnEndLimit = 512,
		// Token: 0x040003CF RID: 975
		PartialColumnStartLimit = 1024,
		// Token: 0x040003D0 RID: 976
		PartialColumnEndLimit = 2048
	}
}
