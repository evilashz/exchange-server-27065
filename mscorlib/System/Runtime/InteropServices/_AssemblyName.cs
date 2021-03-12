using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008E0 RID: 2272
	[Guid("B42B6AAC-317E-34D5-9FA9-093BB4160C50")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(AssemblyName))]
	[ComVisible(true)]
	public interface _AssemblyName
	{
		// Token: 0x06005EC4 RID: 24260
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005EC5 RID: 24261
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005EC6 RID: 24262
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005EC7 RID: 24263
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
