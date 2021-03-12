using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000950 RID: 2384
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IConnectionPoint instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIConnectionPoint
	{
		// Token: 0x06006166 RID: 24934
		void GetConnectionInterface(out Guid pIID);

		// Token: 0x06006167 RID: 24935
		void GetConnectionPointContainer(out UCOMIConnectionPointContainer ppCPC);

		// Token: 0x06006168 RID: 24936
		void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int pdwCookie);

		// Token: 0x06006169 RID: 24937
		void Unadvise(int dwCookie);

		// Token: 0x0600616A RID: 24938
		void EnumConnections(out UCOMIEnumConnections ppEnum);
	}
}
