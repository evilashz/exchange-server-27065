using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200098B RID: 2443
	[Guid("A4924B27-6E3B-37F7-9B83-A4501955E6A7")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(ILGenerator))]
	[ComVisible(true)]
	public interface _ILGenerator
	{
		// Token: 0x06006262 RID: 25186
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06006263 RID: 25187
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06006264 RID: 25188
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06006265 RID: 25189
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
