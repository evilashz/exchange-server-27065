using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200098A RID: 2442
	[Guid("CE1A3BF5-975E-30CC-97C9-1EF70F8F3993")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(FieldBuilder))]
	[ComVisible(true)]
	public interface _FieldBuilder
	{
		// Token: 0x0600625E RID: 25182
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600625F RID: 25183
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06006260 RID: 25184
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06006261 RID: 25185
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
