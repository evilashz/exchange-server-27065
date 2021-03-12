using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000991 RID: 2449
	[Guid("15F9A479-9397-3A63-ACBD-F51977FB0F02")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(PropertyBuilder))]
	[ComVisible(true)]
	public interface _PropertyBuilder
	{
		// Token: 0x0600627A RID: 25210
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600627B RID: 25211
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x0600627C RID: 25212
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x0600627D RID: 25213
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
