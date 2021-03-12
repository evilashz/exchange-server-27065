using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000987 RID: 2439
	[Guid("BE9ACCE8-AAFF-3B91-81AE-8211663F5CAD")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(CustomAttributeBuilder))]
	[ComVisible(true)]
	public interface _CustomAttributeBuilder
	{
		// Token: 0x06006252 RID: 25170
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06006253 RID: 25171
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06006254 RID: 25172
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06006255 RID: 25173
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
