using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008DE RID: 2270
	[Guid("993634C4-E47A-32CC-BE08-85F567DC27D6")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(ParameterInfo))]
	[ComVisible(true)]
	public interface _ParameterInfo
	{
		// Token: 0x06005EBC RID: 24252
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005EBD RID: 24253
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005EBE RID: 24254
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005EBF RID: 24255
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
