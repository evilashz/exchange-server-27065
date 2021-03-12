using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A01 RID: 2561
	[Guid("00020404-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IEnumVARIANT
	{
		// Token: 0x06006513 RID: 25875
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] object[] rgVar, IntPtr pceltFetched);

		// Token: 0x06006514 RID: 25876
		[__DynamicallyInvokable]
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06006515 RID: 25877
		[__DynamicallyInvokable]
		[PreserveSig]
		int Reset();

		// Token: 0x06006516 RID: 25878
		[__DynamicallyInvokable]
		IEnumVARIANT Clone();
	}
}
