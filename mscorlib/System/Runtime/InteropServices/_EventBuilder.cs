using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000989 RID: 2441
	[Guid("AADABA99-895D-3D65-9760-B1F12621FAE8")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(EventBuilder))]
	[ComVisible(true)]
	public interface _EventBuilder
	{
		// Token: 0x0600625A RID: 25178
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600625B RID: 25179
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x0600625C RID: 25180
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x0600625D RID: 25181
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
