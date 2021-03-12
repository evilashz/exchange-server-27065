using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200098D RID: 2445
	[Guid("007D8A14-FDF3-363E-9A0B-FEC0618260A2")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(MethodBuilder))]
	[ComVisible(true)]
	public interface _MethodBuilder
	{
		// Token: 0x0600626A RID: 25194
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600626B RID: 25195
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x0600626C RID: 25196
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x0600626D RID: 25197
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
