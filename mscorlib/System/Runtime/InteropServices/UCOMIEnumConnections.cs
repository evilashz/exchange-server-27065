using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000955 RID: 2389
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumConnections instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("B196B287-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIEnumConnections
	{
		// Token: 0x06006173 RID: 24947
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] CONNECTDATA[] rgelt, out int pceltFetched);

		// Token: 0x06006174 RID: 24948
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06006175 RID: 24949
		[PreserveSig]
		void Reset();

		// Token: 0x06006176 RID: 24950
		void Clone(out UCOMIEnumConnections ppenum);
	}
}
