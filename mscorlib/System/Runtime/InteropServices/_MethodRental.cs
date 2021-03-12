using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200098E RID: 2446
	[Guid("C2323C25-F57F-3880-8A4D-12EBEA7A5852")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(MethodRental))]
	[ComVisible(true)]
	public interface _MethodRental
	{
		// Token: 0x0600626E RID: 25198
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600626F RID: 25199
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06006270 RID: 25200
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06006271 RID: 25201
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
