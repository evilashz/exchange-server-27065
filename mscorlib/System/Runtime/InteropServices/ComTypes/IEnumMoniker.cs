using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020009FC RID: 2556
	[Guid("00000102-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IEnumMoniker
	{
		// Token: 0x06006503 RID: 25859
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] IMoniker[] rgelt, IntPtr pceltFetched);

		// Token: 0x06006504 RID: 25860
		[__DynamicallyInvokable]
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06006505 RID: 25861
		[__DynamicallyInvokable]
		void Reset();

		// Token: 0x06006506 RID: 25862
		[__DynamicallyInvokable]
		void Clone(out IEnumMoniker ppenum);
	}
}
