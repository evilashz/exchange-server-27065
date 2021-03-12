using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020009F8 RID: 2552
	[Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IConnectionPointContainer
	{
		// Token: 0x060064F8 RID: 25848
		[__DynamicallyInvokable]
		void EnumConnectionPoints(out IEnumConnectionPoints ppEnum);

		// Token: 0x060064F9 RID: 25849
		[__DynamicallyInvokable]
		void FindConnectionPoint([In] ref Guid riid, out IConnectionPoint ppCP);
	}
}
