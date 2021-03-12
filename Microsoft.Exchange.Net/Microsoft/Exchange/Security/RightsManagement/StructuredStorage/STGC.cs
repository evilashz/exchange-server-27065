using System;

namespace Microsoft.Exchange.Security.RightsManagement.StructuredStorage
{
	// Token: 0x02000A21 RID: 2593
	internal enum STGC
	{
		// Token: 0x04002FC8 RID: 12232
		STGC_DEFAULT,
		// Token: 0x04002FC9 RID: 12233
		STGC_OVERWRITE,
		// Token: 0x04002FCA RID: 12234
		STGC_ONLYIFCURRENT,
		// Token: 0x04002FCB RID: 12235
		STGC_DANGEROUSLYCOMMITMERELYTODISKCACHE = 4,
		// Token: 0x04002FCC RID: 12236
		STGC_CONSOLIDATE = 8
	}
}
