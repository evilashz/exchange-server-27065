using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002BD RID: 701
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class SafeExInterfaceHandleExtensions
	{
		// Token: 0x06000D21 RID: 3361 RVA: 0x0003468A File Offset: 0x0003288A
		internal static void DisposeIfValid(this SafeExInterfaceHandle safeExInterfaceHandle)
		{
			if (safeExInterfaceHandle != null)
			{
				safeExInterfaceHandle.Dispose();
				safeExInterfaceHandle = null;
			}
		}
	}
}
