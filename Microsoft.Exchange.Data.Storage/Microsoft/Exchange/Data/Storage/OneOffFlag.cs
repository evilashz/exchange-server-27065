using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000915 RID: 2325
	[Flags]
	internal enum OneOffFlag : uint
	{
		// Token: 0x04002E77 RID: 11895
		Simple = 0U,
		// Token: 0x04002E78 RID: 11896
		Cont = 1U,
		// Token: 0x04002E79 RID: 11897
		NewEntry = 2U,
		// Token: 0x04002E7A RID: 11898
		NoTnef = 65536U,
		// Token: 0x04002E7B RID: 11899
		DontLookup = 268435456U,
		// Token: 0x04002E7C RID: 11900
		NonTransmittable = 536870912U,
		// Token: 0x04002E7D RID: 11901
		Extended = 1073741824U,
		// Token: 0x04002E7E RID: 11902
		Unicode = 2147483648U,
		// Token: 0x04002E7F RID: 11903
		SendInternetEncodingMask = 8257536U
	}
}
