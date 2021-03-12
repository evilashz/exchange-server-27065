using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000963 RID: 2403
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.ITypeComp instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("00020403-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMITypeComp
	{
		// Token: 0x060061BF RID: 25023
		void Bind([MarshalAs(UnmanagedType.LPWStr)] string szName, int lHashVal, short wFlags, out UCOMITypeInfo ppTInfo, out DESCKIND pDescKind, out BINDPTR pBindPtr);

		// Token: 0x060061C0 RID: 25024
		void BindType([MarshalAs(UnmanagedType.LPWStr)] string szName, int lHashVal, out UCOMITypeInfo ppTInfo, out UCOMITypeComp ppTComp);
	}
}
