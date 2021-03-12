using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008D2 RID: 2258
	[Guid("03973551-57A1-3900-A2B5-9083E3FF2943")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(Activator))]
	[ComVisible(true)]
	public interface _Activator
	{
		// Token: 0x06005D3A RID: 23866
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005D3B RID: 23867
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005D3C RID: 23868
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005D3D RID: 23869
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
