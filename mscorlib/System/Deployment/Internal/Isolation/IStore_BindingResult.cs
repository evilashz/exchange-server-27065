using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000683 RID: 1667
	internal struct IStore_BindingResult
	{
		// Token: 0x040021B1 RID: 8625
		[MarshalAs(UnmanagedType.U4)]
		public uint Flags;

		// Token: 0x040021B2 RID: 8626
		[MarshalAs(UnmanagedType.U4)]
		public uint Disposition;

		// Token: 0x040021B3 RID: 8627
		public IStore_BindingResult_BoundVersion Component;

		// Token: 0x040021B4 RID: 8628
		public Guid CacheCoherencyGuid;

		// Token: 0x040021B5 RID: 8629
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr Reserved;
	}
}
