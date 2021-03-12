using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000682 RID: 1666
	internal struct IStore_BindingResult_BoundVersion
	{
		// Token: 0x040021AD RID: 8621
		[MarshalAs(UnmanagedType.U2)]
		public ushort Revision;

		// Token: 0x040021AE RID: 8622
		[MarshalAs(UnmanagedType.U2)]
		public ushort Build;

		// Token: 0x040021AF RID: 8623
		[MarshalAs(UnmanagedType.U2)]
		public ushort Minor;

		// Token: 0x040021B0 RID: 8624
		[MarshalAs(UnmanagedType.U2)]
		public ushort Major;
	}
}
