using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200098F RID: 2447
	[Guid("D05FFA9A-04AF-3519-8EE1-8D93AD73430B")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(ModuleBuilder))]
	[ComVisible(true)]
	public interface _ModuleBuilder
	{
		// Token: 0x06006272 RID: 25202
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06006273 RID: 25203
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06006274 RID: 25204
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06006275 RID: 25205
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
