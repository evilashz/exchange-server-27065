using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000265 RID: 613
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct _Action
	{
		// Token: 0x040010D4 RID: 4308
		public static readonly int SizeOf = Marshal.SizeOf(typeof(_Action));

		// Token: 0x040010D5 RID: 4309
		internal uint ActType;

		// Token: 0x040010D6 RID: 4310
		internal uint ActFlavor;

		// Token: 0x040010D7 RID: 4311
		internal IntPtr Zero1;

		// Token: 0x040010D8 RID: 4312
		internal IntPtr Zero2;

		// Token: 0x040010D9 RID: 4313
		internal uint ulFlags;

		// Token: 0x040010DA RID: 4314
		internal uint ulPad;

		// Token: 0x040010DB RID: 4315
		internal ActionUnion union;
	}
}
