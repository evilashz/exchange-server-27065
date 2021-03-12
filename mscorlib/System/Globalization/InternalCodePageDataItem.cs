using System;
using System.Security;

namespace System.Globalization
{
	// Token: 0x0200038D RID: 909
	internal struct InternalCodePageDataItem
	{
		// Token: 0x04001362 RID: 4962
		internal ushort codePage;

		// Token: 0x04001363 RID: 4963
		internal ushort uiFamilyCodePage;

		// Token: 0x04001364 RID: 4964
		internal uint flags;

		// Token: 0x04001365 RID: 4965
		[SecurityCritical]
		internal unsafe sbyte* Names;
	}
}
