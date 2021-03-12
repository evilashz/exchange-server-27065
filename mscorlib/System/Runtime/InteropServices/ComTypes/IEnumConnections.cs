using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020009FE RID: 2558
	[Guid("B196B287-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IEnumConnections
	{
		// Token: 0x06006507 RID: 25863
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] CONNECTDATA[] rgelt, IntPtr pceltFetched);

		// Token: 0x06006508 RID: 25864
		[__DynamicallyInvokable]
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06006509 RID: 25865
		[__DynamicallyInvokable]
		void Reset();

		// Token: 0x0600650A RID: 25866
		[__DynamicallyInvokable]
		void Clone(out IEnumConnections ppenum);
	}
}
