using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000953 RID: 2387
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumMoniker instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("00000102-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIEnumMoniker
	{
		// Token: 0x0600616F RID: 24943
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] UCOMIMoniker[] rgelt, out int pceltFetched);

		// Token: 0x06006170 RID: 24944
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06006171 RID: 24945
		[PreserveSig]
		int Reset();

		// Token: 0x06006172 RID: 24946
		void Clone(out UCOMIEnumMoniker ppenum);
	}
}
