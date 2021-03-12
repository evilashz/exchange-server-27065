using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020009FF RID: 2559
	[Guid("B196B285-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IEnumConnectionPoints
	{
		// Token: 0x0600650B RID: 25867
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] IConnectionPoint[] rgelt, IntPtr pceltFetched);

		// Token: 0x0600650C RID: 25868
		[__DynamicallyInvokable]
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x0600650D RID: 25869
		[__DynamicallyInvokable]
		void Reset();

		// Token: 0x0600650E RID: 25870
		[__DynamicallyInvokable]
		void Clone(out IEnumConnectionPoints ppenum);
	}
}
