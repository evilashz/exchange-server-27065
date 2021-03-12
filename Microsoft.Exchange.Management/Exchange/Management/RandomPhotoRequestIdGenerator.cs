using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management
{
	// Token: 0x02000D77 RID: 3447
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class RandomPhotoRequestIdGenerator
	{
		// Token: 0x0600844D RID: 33869 RVA: 0x0021CFCC File Offset: 0x0021B1CC
		internal static string Generate()
		{
			return Guid.NewGuid().ToString();
		}
	}
}
