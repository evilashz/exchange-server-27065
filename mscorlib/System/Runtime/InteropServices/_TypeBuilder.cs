using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000993 RID: 2451
	[Guid("7E5678EE-48B3-3F83-B076-C58543498A58")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(TypeBuilder))]
	[ComVisible(true)]
	public interface _TypeBuilder
	{
		// Token: 0x06006282 RID: 25218
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06006283 RID: 25219
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06006284 RID: 25220
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06006285 RID: 25221
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
