using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000988 RID: 2440
	[Guid("C7BD73DE-9F85-3290-88EE-090B8BDFE2DF")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(EnumBuilder))]
	[ComVisible(true)]
	public interface _EnumBuilder
	{
		// Token: 0x06006256 RID: 25174
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06006257 RID: 25175
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06006258 RID: 25176
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06006259 RID: 25177
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
