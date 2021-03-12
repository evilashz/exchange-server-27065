using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A00 RID: 2560
	[Guid("00000101-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IEnumString
	{
		// Token: 0x0600650F RID: 25871
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] [Out] string[] rgelt, IntPtr pceltFetched);

		// Token: 0x06006510 RID: 25872
		[__DynamicallyInvokable]
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06006511 RID: 25873
		[__DynamicallyInvokable]
		void Reset();

		// Token: 0x06006512 RID: 25874
		[__DynamicallyInvokable]
		void Clone(out IEnumString ppenum);
	}
}
