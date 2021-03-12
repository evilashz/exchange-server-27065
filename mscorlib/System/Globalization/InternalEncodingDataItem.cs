using System;
using System.Security;

namespace System.Globalization
{
	// Token: 0x0200038C RID: 908
	internal struct InternalEncodingDataItem
	{
		// Token: 0x04001360 RID: 4960
		[SecurityCritical]
		internal unsafe sbyte* webName;

		// Token: 0x04001361 RID: 4961
		internal ushort codePage;
	}
}
