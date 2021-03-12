using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000266 RID: 614
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct _Actions
	{
		// Token: 0x040010DC RID: 4316
		public static readonly int SizeOf = Marshal.SizeOf(typeof(_Actions));

		// Token: 0x040010DD RID: 4317
		internal uint ulVersion;

		// Token: 0x040010DE RID: 4318
		internal uint cActions;

		// Token: 0x040010DF RID: 4319
		internal unsafe _Action* lpAction;
	}
}
