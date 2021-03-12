using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200038A RID: 906
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class SecurityConstants
	{
		// Token: 0x04000B66 RID: 2918
		public const int MaxElementCountInMultiValuedProperty = 32768;

		// Token: 0x04000B67 RID: 2919
		public const int MaxInMemoryPropertyStreamLength = 1048576;

		// Token: 0x04000B68 RID: 2920
		public const int MaxPropertyStreamLength = 1073741824;
	}
}
