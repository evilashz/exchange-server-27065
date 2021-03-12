using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008D3 RID: 2259
	[Guid("917B14D0-2D9E-38B8-92A9-381ACF52F7C0")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(Attribute))]
	[ComVisible(true)]
	public interface _Attribute
	{
		// Token: 0x06005D3E RID: 23870
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005D3F RID: 23871
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005D40 RID: 23872
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005D41 RID: 23873
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
