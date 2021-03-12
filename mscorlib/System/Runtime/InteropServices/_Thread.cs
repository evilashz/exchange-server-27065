using System;
using System.Threading;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008D4 RID: 2260
	[Guid("C281C7F1-4AA9-3517-961A-463CFED57E75")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(Thread))]
	[ComVisible(true)]
	public interface _Thread
	{
		// Token: 0x06005D42 RID: 23874
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005D43 RID: 23875
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005D44 RID: 23876
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005D45 RID: 23877
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
