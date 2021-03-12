using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200027C RID: 636
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct MapiLtidNative
	{
		// Token: 0x06000B5E RID: 2910 RVA: 0x00033B4F File Offset: 0x00031D4F
		internal unsafe MapiLtidNative(NativeLtid* pLtid)
		{
			this.padding = new byte[2];
			this.globCount = new byte[6];
			Marshal.Copy(new IntPtr((void*)(pLtid + sizeof(Guid) / sizeof(NativeLtid))), this.globCount, 0, 6);
			this.replGuid = pLtid->replGuid;
		}

		// Token: 0x040010F6 RID: 4342
		internal Guid replGuid;

		// Token: 0x040010F7 RID: 4343
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		internal byte[] globCount;

		// Token: 0x040010F8 RID: 4344
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		internal byte[] padding;

		// Token: 0x040010F9 RID: 4345
		internal static readonly int Size = 24;
	}
}
