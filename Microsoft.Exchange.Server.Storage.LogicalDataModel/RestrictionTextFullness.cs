using System;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000C7 RID: 199
	[Flags]
	public enum RestrictionTextFullness : ushort
	{
		// Token: 0x04000505 RID: 1285
		FullString = 0,
		// Token: 0x04000506 RID: 1286
		SubString = 1,
		// Token: 0x04000507 RID: 1287
		Prefix = 2,
		// Token: 0x04000508 RID: 1288
		PrefixOnAnyWord = 16,
		// Token: 0x04000509 RID: 1289
		PhraseMatch = 32
	}
}
