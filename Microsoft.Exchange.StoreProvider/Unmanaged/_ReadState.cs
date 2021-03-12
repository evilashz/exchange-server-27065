using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200025C RID: 604
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct _ReadState
	{
		// Token: 0x040010B0 RID: 4272
		public static readonly int SizeOf = Marshal.SizeOf(typeof(_ReadState));

		// Token: 0x040010B1 RID: 4273
		internal int cbSourceKey;

		// Token: 0x040010B2 RID: 4274
		internal unsafe byte* pbSourceKey;

		// Token: 0x040010B3 RID: 4275
		internal int ulFlags;
	}
}
