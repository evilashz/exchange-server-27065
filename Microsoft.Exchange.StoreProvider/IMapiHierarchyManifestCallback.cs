using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000065 RID: 101
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMapiHierarchyManifestCallback
	{
		// Token: 0x060002C7 RID: 711
		ManifestCallbackStatus Change(PropValue[] props);

		// Token: 0x060002C8 RID: 712
		ManifestCallbackStatus Delete(byte[] data);
	}
}
