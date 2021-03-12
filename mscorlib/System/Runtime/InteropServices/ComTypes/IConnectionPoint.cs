using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020009F9 RID: 2553
	[Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IConnectionPoint
	{
		// Token: 0x060064FA RID: 25850
		[__DynamicallyInvokable]
		void GetConnectionInterface(out Guid pIID);

		// Token: 0x060064FB RID: 25851
		[__DynamicallyInvokable]
		void GetConnectionPointContainer(out IConnectionPointContainer ppCPC);

		// Token: 0x060064FC RID: 25852
		[__DynamicallyInvokable]
		void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int pdwCookie);

		// Token: 0x060064FD RID: 25853
		[__DynamicallyInvokable]
		void Unadvise(int dwCookie);

		// Token: 0x060064FE RID: 25854
		[__DynamicallyInvokable]
		void EnumConnections(out IEnumConnections ppEnum);
	}
}
