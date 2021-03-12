using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000990 RID: 2448
	[Guid("36329EBA-F97A-3565-BC07-0ED5C6EF19FC")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(ParameterBuilder))]
	[ComVisible(true)]
	public interface _ParameterBuilder
	{
		// Token: 0x06006276 RID: 25206
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06006277 RID: 25207
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06006278 RID: 25208
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06006279 RID: 25209
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
